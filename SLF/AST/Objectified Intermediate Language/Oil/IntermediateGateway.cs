using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal;
using AllenCopeland.Abstraction.Slf._Internal.Abstract;
//using AllenCopeland.Abstraction.Slf._Internal.Oil;
using AllenCopeland.Abstraction.Slf.Oil.Expressions;
using AllenCopeland.Abstraction.Slf.Oil.Modules;
using AllenCopeland.Abstraction.Slf.Oil;
using AllenCopeland.Abstraction.Slf.Oil.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf._Internal.Ast;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
{
    /// <summary>
    /// Provides instantiation members for common dynamic elements.
    /// </summary>
    public static partial class IntermediateGateway
    {
        private const string nullNamespace = ".<NULL>.";
        /// <summary>
        /// Contains the <see cref="ISymbolType"/> lookup table that
        /// starts with the symbol, the namespace, then the number of generic parameters.
        /// </summary>
        private static IDictionary<string, IDictionary<string, IDictionary<int, ISymbolType>>> SymbolTypeCache = new Dictionary<string, IDictionary<string, IDictionary<int, ISymbolType>>>();
        /// <summary>
        /// Represents a null value primitive expression.
        /// </summary>
        public static readonly IPrimitiveExpression NullValue = new PrimitiveNullExpression();
        /// <summary>
        /// Represents a number zero expression.
        /// </summary>
        public static readonly IPrimitiveExpression<int> NumberZero = 0.ToPrimitive();

        /* *
         * Anonymous type cache works like so:
         * First key is the base-type.
         * Second key is the hash list of all 
         * the members of the anonymous type.
         * */
        private static IDictionary<IClassType, IDictionary<HashList<AnonymousTypeMember>, IAnonymousType>> AnonymousTypeCache = new Dictionary<IClassType, IDictionary<HashList<AnonymousTypeMember>, IAnonymousType>>();
        /// <summary>
        /// The index used for anonymous types for name indexing.
        /// </summary>
        private static int anonymousTypeIndex = 0;
        /// <summary>
        /// The display style of the anonymous types.
        /// </summary>
        private static AnonymousTypeDisplayStyles anonymousDisplayStyle = AnonymousTypeDisplayStyles.CSharp;
        /// <summary>
        /// The pattern aid used to adjust an anonymous type's 
        /// member name patterns.
        /// </summary>
        private static IAnonymousTypePatternAid patternAid = null;
        /// <summary>
        /// The default Anonymous Type Pattern aid.
        /// </summary>
        private static DefaultATPatternAid defaultAid = new DefaultATPatternAid();
        /// <summary>
        /// Type-reference expression cache.
        /// </summary>
        private static IDictionary<IType, TypeReferenceExpression> typeReferenceCache = new Dictionary<IType, TypeReferenceExpression>();

        /// <summary>
        /// Obtains a <see cref="ITypeReferenceExpression"/>
        /// relative to the curent <see cref="IType"/>.
        /// </summary>
        /// <returns>An <see cref="ITypeReferenceExpression"/> 
        /// pertinent to the current <see cref="IType"/>.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="target"/> is null.</exception>
        public static TypeReferenceExpression GetTypeExpression(this IType target)
        {
            if (target == null)
                throw new ArgumentNullException("target");
            if (!typeReferenceCache.ContainsKey(target))
            {
                target.Disposed += typeExpressionTarget_Disposed;
                typeReferenceCache.Add(target, new TypeReferenceExpression(target));
            }
            return typeReferenceCache[target];
        }

        static void typeExpressionTarget_Disposed(object sender, EventArgs e)
        {
            if (sender is IType)
            {
                var tSender = ((IType)(sender));
                /* *
                 * If it doesn't exist in the cache, some other source
                 * called the handler and passed a different type.
                 * */
                if (typeReferenceCache.ContainsKey(tSender))
                {
                    typeReferenceCache.Remove(tSender);
                    tSender.Disposed -= typeExpressionTarget_Disposed;
                }
            }
        }
#if !NO_ANONYMOUS
        internal static void AnonymousType_Disposal(AnonymousType target)
        {
            IClassType baseType = ((IClassType)(target.BaseType));
            if (AnonymousTypeCache.ContainsKey(baseType) &&
                AnonymousTypeCache[baseType].ContainsKey(target.members))
            {
                AnonymousTypeCache[baseType].Remove(target.members);
                if (AnonymousTypeCache[baseType].Count == 0)
                    AnonymousTypeCache.Remove(baseType);
            }
        }
#endif

        /// <summary>
        /// Creates a new <see cref="IntermediateAssembly"/> instance
        /// with the <paramref name="name"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/>
        /// that represents the name of the <see cref="IIntermediateAssembly"/>
        /// created.</param>
        /// <returns>An <see cref="IIntermediateAssembly"/> instance.</returns>
        public static IIntermediateAssembly CreateAssembly(string name)
        {
            return CreateAssembly<IntermediateAssembly>(name);
        }

        /// <summary>
        /// Creates a new <typeparamref name="TAssembly"/> with
        /// the <paramref name="name"/> provided.
        /// </summary>
        /// <typeparam name="TAssembly">The type of the <see cref="IIntermediateAssembly"/>
        /// to create a new instance of.</typeparam>
        /// <param name="name">The <see cref="String"/> value representing
        /// the name of the new <typeparamref name="TAssembly"/>.</param>
        /// <returns>A new <typeparamref name="TAssembly"/> if successful.</returns>
        /// <exception cref="System.ArgumentException">thrown when the
        /// type <typeparamref name="TAssembly"/> does not have a public
        /// constructor which accepts a <see cref="String"/> value.</exception>
        /// <remarks><para>The resulted instance is a part of a new <typeparamref name="TAssembly"/>
        /// instance created.</para><para>To allow for internal type variations to execute 
        /// smoothly, refer to <see cref="RegisterCreateAssemblyBridge{T}(ICreateAssemblyBridge{T})"/>.</para></remarks>
        public static TAssembly CreateAssembly<TAssembly>(string name)
            where TAssembly :
                IIntermediateAssembly/*,
                new(string) */
        {
            return ((TAssembly)(CreateAssemblyBridgeCache<TAssembly>.Bridge.ctor(name).Parts.Add()));
        }

        /// <summary>
        /// Creates a new <see cref="IIntermediateType"/>
        /// instance unbound to a specific parent/assembly.
        /// </summary>
        /// <param name="name">The <see cref="String"/> name 
        /// of the <see cref="IIntermediateType"/>.</param>
        /// <param name="kind">The <see cref="TypeKind"/> 
        /// that determines the kind of type to create.</param>
        /// <returns></returns>
        public static IIntermediateType CreateType(string name, TypeKind kind)
        {
            switch (kind)
            {
                case TypeKind.Class:
                    return new IntermediateClassType(name, null);
                case TypeKind.Delegate:
                    return new IntermediateDelegateType(name, null);
                case TypeKind.Enumerator:
                    return new IntermediateEnumType(name, null);
                case TypeKind.Interface:
                    return new IntermediateInterfaceType(name, null);
                case TypeKind.Struct:
                    return new IntermediateStructType(name, null);
                case TypeKind.Other:
                case TypeKind.Ambiguity:
                case TypeKind.Dynamic:
                    throw new NotSupportedException();
                default:
                    break;
            }
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates a new <see cref="IIntermediateType"/>
        /// instance unbound to a specific parent/assembly.
        /// </summary>
        /// <param name="name">The <see cref="String"/> name 
        /// of the <see cref="IIntermediateType"/>.</param>
        /// <param name="kind">The <see cref="TypeKind"/> 
        /// that determines the kind of type to create.</param>
        /// <param name="module">The <see cref="IIntermediateModule"/>
        /// to which the <see cref="IIntermediateType"/> will belong.</param>
        /// <returns>A new <see cref="IIntermediateType"/> contained within
        /// the <paramref name="module"/> provided.</returns>
        public static IIntermediateType CreateType(string name, IIntermediateModule module, TypeKind kind)
        {
            if (name == null)
                throw new ArgumentNullException("name");
            if (name == string.Empty)
                throw new ArgumentOutOfRangeException("name");
            if (module == null)
                throw new ArgumentNullException("module");
            IIntermediateAssembly assembly = module.Parent;
            switch (kind)
            {
                case TypeKind.Class:
                    return assembly.Classes.Add(name, module);
                case TypeKind.Delegate:
                    return assembly.Delegates.Add(name, module);
                case TypeKind.Enumerator:
                    return assembly.Enums.Add(name, module);
                case TypeKind.Interface:
                    return assembly.Interfaces.Add(name, module);
                case TypeKind.Struct:
                    return assembly.Structs.Add(name, module);
                default:
                case TypeKind.Other:
                case TypeKind.Ambiguity:
                    break;
            }
            throw new NotSupportedException(string.Format("Type {0} not supported", kind));
        }

        public static IIntermediateClassType CreateClassType(string name)
        {
            return (IIntermediateClassType)CreateType(name, TypeKind.Class);
        }

        public static IIntermediateClassType CreateClassType(string name, IIntermediateModule module)
        {
            throw new NotImplementedException();
            //return new IntermediateClassType(name, null, module);
        }

        public static IIntermediateClassType CreateClassType(string name, params GenericParameterData[] genParamData)
        {
            var result = CreateClassType(name);
            /*//
            if (result != null)
                foreach (var gpData in genParamData)
                    result.TypeParameters.Add(gpData);
            //*/
            return result;
        }

        public static IIntermediateClassType CreateClassType(string name, IIntermediateModule module, params GenericParameterData[] genParamData)
        {
            var result = CreateClassType(name, module);
            /*//
            if (result != null)
                foreach (var gpData in genParamData)
                    result.TypeParameters.Add(gpData);
            //*/
            return result;
        }

        /// <summary>
        /// Creates a new generic type of the proper <paramref name="kind"/>
        /// given the <paramref name="genParamData"/> to define the 
        /// generic's type parameters.
        /// </summary>
        /// <param name="name">The <see cref="String"/> that defines
        /// the name of the new type.</param>
        /// <param name="kind">The <see cref="TypeKindGeneric"/>
        /// which defines the kind of type to create.</param>
        /// <param name="genParamData">
        /// The <see cref="GenericParameterData"/> series which
        /// defines the type-parameters on the generic type.</param>
        /// <returns>A new <see cref="IIntermediateGenericType"/>
        /// of the proper <paramref name="kind"/> with the 
        /// <paramref name="genParamData"/> as provided.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">thrown when
        /// <paramref name="kind"/> is any other value not defined in
        /// <see cref="TypeKindGeneric"/>.</exception>
        /// <exception cref="System.ArgumentNullException">
        /// thrown when <paramref name="name"/> is <see cref="String.Empty"/>
        /// or null, or when <paramref name="genParamData"/> is null.</exception>
        public static IIntermediateGenericType CreateType(string name, TypeKindGeneric kind, params GenericParameterData[] genParamData)
        {
            if (name.IsEmptyOrNull())
                throw new ArgumentNullException("name");
            if (genParamData == null)
                throw new ArgumentNullException("genParamData");
            var result = (IIntermediateGenericType)null;
            switch (kind)
            {
                case TypeKindGeneric.Class:
                    //result = new IntermediateClassType(name, null);
                    break;
                case TypeKindGeneric.Struct:
                    //result = new IntermediateStructType(name, null);
                    break;
                case TypeKindGeneric.Interface:
                    break;
                case TypeKindGeneric.Delegate:
                    //result = new IntermediateDelegateType(name, null);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("kind");
            }
            //if (result != null)
                /*
                foreach (var gpData in genParamData)
                    result.TypeParameters.AddNew(gpData);
                //*/
            return result;
        }

        public static IMember CreateMember(MemberKind kind)
        {
            throw new NotImplementedException();
        }
#if !NO_ANONYMOUS
        /// <summary>
        /// Obtains a new <see cref="IAnonymmousType"/> with the 
        /// <paramref name="members"/> provided.
        /// </summary>
        /// <param name="members">The <see cref="AnonymousTypeMembers"/>
        /// which describe the layout of the anonymous type.</param>
        /// <returns>An <see cref="IAnonymousType"/> instance.</returns>
        public static IAnonymousType GetAnonymousType(params AnonymousTypeMember[] members)
        {
#if TYPESYSTEM_CLI
            return GetAnonymousType((IClassType)typeof(object).GetTypeReference(), members);
#endif
        }
#endif

#if TYPESYSTEM_CLI
        public static IAnonymousType GetAnonymousType(Type baseType, params AnonymousTypeMember[] members)
        {
            if (!baseType.IsClass)
                throw new ArgumentException("baseType");
            return GetAnonymousType(((IClassType)(baseType.GetTypeReference())), members);
        }
#endif
#if !NO_ANONYMOUS
        /// <summary>
        /// Obtains a <see cref="IAnonymousType"/> for the 
        /// <paramref name="baseType"/> and <paramref name="members"/>
        /// provided.
        /// </summary>
        /// <param name="baseType">The <see cref="IClassType"/> with a public
        /// empty constructor that acts as the root type of the <see cref="IAnonymousType"/>.</param>
        /// <param name="members">The <see cref="AnonymousTypeMember"/> array which
        /// defines the names and immutability status of the elements within the 
        /// <see cref="IAnonymousType"/>.</param>
        /// <returns>A <see cref="IAnonymousType"/> instance with the 
        /// <paramref name="baseType"/> and <paramref name="members"/>
        /// provided.</returns>
        public static IAnonymousType GetAnonymousType(IClassType baseType, params AnonymousTypeMember[] members)
        {
            if (!AnonymousTypeCache.ContainsKey(baseType))
                AnonymousTypeCache.Add(baseType, new Dictionary<HashList<AnonymousTypeMember>, IAnonymousType>());
            if (baseType.Constructors.Find().Count == 0)
                throw new ArgumentException("Base type must have public empty constructor.");
            if (baseType.IsGenericType && baseType.IsGenericTypeDefinition)
                throw new ArgumentException(string.Format("baseType {0} cannot be a generic definition.", baseType.Name), "baseType");
            HashList<AnonymousTypeMember> key = new HashList<AnonymousTypeMember>(members);
            if (!AnonymousTypeCache[baseType].ContainsKey(key))
                AnonymousTypeCache[baseType].Add(key, new AnonymousType(anonymousTypeIndex++, baseType, key));
            return AnonymousTypeCache[baseType][key];
        }
#endif
        /// <summary>
        /// Returns/sets the <see cref="AnonymousTypeDisplayStyles"/>
        /// associated to anonymous types and how they present their values.
        /// </summary>
        public static AnonymousTypeDisplayStyles AnonymousDisplayStyle
        {
            get
            {
                return IntermediateGateway.anonymousDisplayStyle;
            }
            set
            {
                if ((value & AnonymousTypeDisplayStyles.Other) == AnonymousTypeDisplayStyles.Other)
                    throw new ArgumentException("value");
                IntermediateGateway.anonymousDisplayStyle = value;
            }
        }

        public static IAnonymousTypePatternAid PatternAid
        {
            get
            {
                if ((anonymousDisplayStyle & AnonymousTypeDisplayStyles.Other) == AnonymousTypeDisplayStyles.Other)
                {
                    if (patternAid == null)
                        anonymousDisplayStyle = AnonymousTypeDisplayStyles.CSharp;
                    return defaultAid;
                }
                else if ((anonymousDisplayStyle & AnonymousTypeDisplayStyles.Other) != AnonymousTypeDisplayStyles.Other &&
                    patternAid == null)
                    patternAid = defaultAid;
                return patternAid;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");
                if (value == defaultAid)
                {
                    if ((anonymousDisplayStyle & AnonymousTypeDisplayStyles.Other) == AnonymousTypeDisplayStyles.Other)
                        anonymousDisplayStyle ^= AnonymousTypeDisplayStyles.Other;
                    return;
                }
                else if ((anonymousDisplayStyle & AnonymousTypeDisplayStyles.Other) != AnonymousTypeDisplayStyles.Other)
                    anonymousDisplayStyle ^= AnonymousTypeDisplayStyles.Other;
                patternAid = value;
            }
        }

        /// <summary>
        /// Obtains a <see cref="IMethodPointerReferenceExpression"/>
        /// with the <paramref name="signature"/> provided.
        /// </summary>
        /// <param name="signature">The series if <see cref="System.Type"/>
        /// elements relative to the type-signature of the 
        /// <see cref="IMethodPointerReferenceExpression"/>
        /// to obtain.</param>
        /// <returns>A new <see cref="IMethodPointerReferenceExpression"/>
        /// relative to the <paramref name="signature"/>
        /// provided.</returns>
        public static IMethodPointerReferenceExpression GetPointer(this IMethodReferenceStub target, params Type[] signature)
        {
            return target.GetPointer(signature.ToCollection());
        }

        internal static string GetNamespace(this IIntermediateType target)
        {
            IIntermediateTypeParent iitp = target.Parent;
            while (iitp != null)
            {
                if (iitp is IIntermediateAssembly)
                    return null;
                if (iitp is IIntermediateNamespaceDeclaration)
                    return ((IIntermediateNamespaceDeclaration)(iitp)).FullName;
                if (iitp is IIntermediateType)
                    iitp = ((IIntermediateType)(iitp)).Parent;
                else
                    return null;
            }
            return null;
        }
                //*
        #region Symbol Types
        public static ISymbolType GetSymbolType(this string typeSymbol)
        {
            if (typeSymbol == null)
                throw new ArgumentNullException("typeSymbol");
            lock (SymbolTypeCache)
            {
                if (!SymbolTypeCache.ContainsKey(typeSymbol))
                    SymbolTypeCache.Add(typeSymbol, new Dictionary<string, IDictionary<int, ISymbolType>>());
                if (!SymbolTypeCache[typeSymbol].ContainsKey(nullNamespace))
                    SymbolTypeCache[typeSymbol].Add(nullNamespace, new Dictionary<int, ISymbolType>());
                if (!SymbolTypeCache[typeSymbol][nullNamespace].ContainsKey(0))
                    SymbolTypeCache[typeSymbol][nullNamespace].Add(0, new SymbolType(typeSymbol));
                return SymbolTypeCache[typeSymbol][nullNamespace][0];
            }
        }

        public static ISymbolType GetSymbolType(this string typeSymbol, ITypeCollection genericParameters)
        {
            if (typeSymbol == null)
                throw new ArgumentNullException("typeSymbol");
            if (genericParameters == null)
                throw new ArgumentNullException("genericParameters");
            int count = genericParameters.Count;
            lock (SymbolTypeCache)
            {
                if (!SymbolTypeCache.ContainsKey(typeSymbol))
                    SymbolTypeCache.Add(typeSymbol, new Dictionary<string, IDictionary<int, ISymbolType>>());
                if (!SymbolTypeCache[typeSymbol].ContainsKey(nullNamespace))
                    SymbolTypeCache[typeSymbol].Add(nullNamespace, new Dictionary<int, ISymbolType>());
                if (!SymbolTypeCache[typeSymbol][nullNamespace].ContainsKey(count))
                    SymbolTypeCache[typeSymbol][nullNamespace].Add(count, new SymbolType(typeSymbol, genericParameters.Count).MakeGenericType(genericParameters));
                return SymbolTypeCache[typeSymbol][nullNamespace][count];
            }
        }

        public static ISymbolType GetSymbolType(this string typeSymbol, string @namespace)
        {
            if (typeSymbol == null)
                throw new ArgumentNullException("typeSymbol");
            if (@namespace == null)
                throw new ArgumentNullException("namespace");
            lock (SymbolTypeCache)
            {
                if (!SymbolTypeCache.ContainsKey(typeSymbol))
                    SymbolTypeCache.Add(typeSymbol, new Dictionary<string, IDictionary<int, ISymbolType>>());
                if (!SymbolTypeCache[typeSymbol].ContainsKey(@namespace))
                    SymbolTypeCache[typeSymbol].Add(@namespace, new Dictionary<int, ISymbolType>());
                if (!SymbolTypeCache[typeSymbol][@namespace].ContainsKey(0))
                    SymbolTypeCache[typeSymbol][@namespace].Add(0, new SymbolType(typeSymbol, @namespace));
                return SymbolTypeCache[typeSymbol][@namespace][0];
            }
            //return new SymbolType(typeSymbol, @namespace);
        }

        public static ISymbolType GetSymbolType(this string typeSymbol, string @namespace, ITypeCollection genericParameters)
        {
            if (typeSymbol == null)
                throw new ArgumentNullException("typeSymbol");
            if (genericParameters == null)
                throw new ArgumentNullException("genericParameters");
            int count = genericParameters.Count;
            lock (SymbolTypeCache)
            {
                if (!SymbolTypeCache.ContainsKey(typeSymbol))
                    SymbolTypeCache.Add(typeSymbol, new Dictionary<string, IDictionary<int, ISymbolType>>());
                if (!SymbolTypeCache[typeSymbol].ContainsKey(@namespace))
                    SymbolTypeCache[typeSymbol].Add(@namespace, new Dictionary<int, ISymbolType>());
                if (!SymbolTypeCache[typeSymbol][@namespace].ContainsKey(count))
                    SymbolTypeCache[typeSymbol][@namespace].Add(count, new SymbolType(typeSymbol, genericParameters.Count, @namespace).MakeGenericType(genericParameters));
                return SymbolTypeCache[typeSymbol][@namespace][count];
            }
        }

        public static ITypeReferenceExpression GetSymbolTypeExpression(this string typeSymbol, string @namespace, ITypeCollection genericParameters)
        {
            return typeSymbol.GetSymbolType(@namespace, genericParameters).GetTypeExpression();
        }

        public static ITypeReferenceExpression GetSymbolTypeExpression(this string typeSymbol, string @namespace)
        {
            return typeSymbol.GetSymbolType(@namespace).GetTypeExpression();
        }

        public static ITypeReferenceExpression GetSymbolTypeExpression(this string typeSymbol, ITypeCollection genericParameters)
        {
            return typeSymbol.GetSymbolType(genericParameters).GetTypeExpression();
        }

        public static ITypeReferenceExpression GetSymbolTypeExpression(this string typeSymbol)
        {
            return typeSymbol.GetSymbolType().GetTypeExpression();
        }

        public static IType GetSymbolType(this string typeSymbol, params string[] typeParameters)
        {
            return typeSymbol.GetSymbolType(new TypeCollection(typeParameters.OnAll<string, ISymbolType>(GetSymbolType).Cast<IType>().ToArray()));
        }

        public static IType GetSymbolType(this string typeSymbol, string @namespace, string[] typeParameters)
        {
            return typeSymbol.GetSymbolType(@namespace, new TypeCollection(typeParameters.OnAll<string, ISymbolType>(GetSymbolType).Cast<IType>().ToArray()));
        }
        #endregion//*/

        /// <summary>
        /// Returns a primitive boolean expression for true.
        /// </summary>
        /// <remarks>Instance not a singleton, new element retrieved per access.</remarks>
        public static IPrimitiveExpression<bool> TrueValue { get { return new PrimitiveExpression<bool>(true); } }

        /// <summary>
        /// Returns a primitive boolean expression for false.
        /// </summary>
        /// <remarks>Instance not a singleton, new element retrieved per access.</remarks>
        public static IPrimitiveExpression<bool> FalseValue { get { return new PrimitiveExpression<bool>(false); } }
        public static IType GetTypeRef(this TypedName nameAndType)
        {
            switch (nameAndType.Source)
            {
                case TypedNameSource.TypeReference:
                    return nameAndType.Reference;
                case TypedNameSource.SymbolReference:
                    return nameAndType.SymbolReference.GetSymbolType();
                case TypedNameSource.InvalidReference:
                default:
                    throw new ArgumentException("nameAndType");
            }
        }

        internal static IPropertySignatureReferenceExpression<TProperty, TPropertyParent> GetPropertySignatureReference<TProperty, TPropertyParent>(this TProperty target, IMemberParentReferenceExpression source)
            where TProperty :
                IPropertySignatureMember<TProperty, TPropertyParent>
            where TPropertyParent :
                IPropertySignatureParentType<TProperty, TPropertyParent>
        {
            return new PropertySignatureReferenceExpression<TProperty, TPropertyParent>(source, target);
        }

        internal static IPropertyReferenceExpression<TProperty, TPropertyParent> GetPropertyReference<TProperty, TPropertyParent>(this TProperty target, IMemberParentReferenceExpression source)
            where TProperty :
                IPropertyMember<TProperty, TPropertyParent>
            where TPropertyParent :
                IPropertyParentType<TProperty, TPropertyParent>
        {
            return new PropertyReferenceExpression<TProperty, TPropertyParent>(source, target);
        }

        internal static IPropertyReferenceExpression GetPropertyReference(this IPropertySignatureMember target, IMemberParentReferenceExpression source)
        {
            var targetParent = target.Parent;
            if (targetParent is IInterfaceType)
                return ((IInterfacePropertyMember)target).GetPropertySignatureReference<IInterfacePropertyMember, IInterfaceType>(source);
            else
                return new PropertyReferenceExpression(target.Name, source);
        }

        internal static IPropertyReferenceExpression GetPropertyReference(this IPropertyMember target, IMemberParentReferenceExpression source)
        {
            var targetParent = target.Parent;
            if (targetParent is IClassType)
                return ((IClassPropertyMember)target).GetPropertyReference<IClassPropertyMember, IClassType>(source);
            else if (targetParent is IStructType)
                return ((IStructPropertyMember)target).GetPropertyReference<IStructPropertyMember, IStructType>(source);
            else if (targetParent is IInterfaceType)
                return ((IInterfacePropertyMember)target).GetPropertySignatureReference<IInterfacePropertyMember, IInterfaceType>(source);
            else
                return new PropertyReferenceExpression(target.Name, source);
        }

        internal static IPropertyReferenceExpression GetPropertyReference(this IIntermediatePropertyMember target, IMemberParentReferenceExpression source = null)
        {
            if (source == null)
                source = new AutoContextMemberSource(target);
            return GetPropertyReference((IPropertyMember)target, source);
        }

        internal static IFieldReferenceExpression<TField, TFieldParent> GetFieldReference<TField, TFieldParent>(this TField target, IMemberParentReferenceExpression source)
            where TField :
                IFieldMember<TField, TFieldParent>
            where TFieldParent :
                IFieldParent<TField, TFieldParent>
        {
            return new FieldReferenceExpression<TField, TFieldParent>(target, source);
        }

        internal static IFieldReferenceExpression GetFieldReference(this IFieldMember target, IMemberParentReferenceExpression source)
        {
            var targetParent = target.Parent;
            if (targetParent is IClassType)
                return ((IClassFieldMember)target).GetFieldReference<IClassFieldMember, IClassType>(source);
            else if (targetParent is IStructType)
                return ((IStructFieldMember)target).GetFieldReference<IStructFieldMember, IStructType>(source);
            else
                return new FieldReferenceExpression(target.Name, source);
        }

        internal static IFieldReferenceExpression GetFieldReference(this IIntermediateFieldMember target, IMemberParentReferenceExpression source = null)
        {
            if (source == null)
                if (target is IIntermediateInstanceMember)
                    source = new AutoContextMemberSource((IIntermediateInstanceMember)target);
            return GetFieldReference((IFieldMember)target, source);
        }

        public static ICreateInstanceExpression NewExpression(this IType target, params IExpression[] parameters)
        {
            var result = new CreateInstanceExpression(new ConstructorPointerReferenceExpression(new ConstructorReferenceStub(target)), parameters);
            return result;
        }

        public static ICreateInstanceExpression NewExpression(this IType target, IExpressionCollection parameters)
        {
            var result = new CreateInstanceExpression(new ConstructorPointerReferenceExpression(new ConstructorReferenceStub(target)), parameters);
            return result;
        }

        public static ICreateInstanceExpression NewExpression(this IType target, IEnumerable<IExpression> parameters)
        {
            var result = new CreateInstanceExpression(new ConstructorPointerReferenceExpression(new ConstructorReferenceStub(target)), parameters.ToArray());
            return result;
        }
        internal static IType AscertainType(this TypedName typedName, IIntermediateType containingType)
        {
            switch (typedName.Source)
            {
                case TypedNameSource.TypeReference:
                    /* *
                     * A type is explicitly provided.
                     * */
                    return typedName.Reference;
                case TypedNameSource.SymbolReference:
                    /* *
                     * Evaluate the member hierarchy and determine whether
                     * there are type-parameters that are available.
                     * */
                    while (containingType != null)
                    {
                        /* *
                            * In cases where the containing type is a generic capable type.
                            * */
                        if (containingType is IIntermediateGenericType)
                        {
                            var topScopeGenericType = (IIntermediateGenericType)containingType;
                            if (topScopeGenericType.TypeParameters.ContainsKey(typedName.SymbolReference))
                                return (IIntermediateGenericParameter)topScopeGenericType.TypeParameters[typedName.SymbolReference];
                        }
                        if (containingType.Parent is IIntermediateType)
                            containingType = (IIntermediateType)containingType.Parent;
                        else if (containingType.Parent is IIntermediateMember)
                        {
                            var containingMember = (IIntermediateMember)containingType.Parent;
                            while (containingMember != null)
                            {
                                /* *
                                 * In cases where the member itself contains type-parameters,
                                 * i.e. methods.
                                 * */
                                if (containingMember is IIntermediateGenericParameterParent)
                                {
                                    var topScopeGenericMember = (IIntermediateGenericParameterParent)containingMember;
                                    if (topScopeGenericMember.TypeParameters.ContainsKey(typedName.SymbolReference))
                                        return (IIntermediateGenericParameter)topScopeGenericMember.TypeParameters[typedName.SymbolReference];
                                }
                                if (containingMember.Parent == null)
                                    break;
                                else if (containingMember.Parent is IIntermediateMember)
                                    containingMember = (IIntermediateMember)containingMember.Parent;
                                else if (containingMember.Parent is IIntermediateType)
                                {
                                    /* *
                                     * When the parent is a type, obtain the type variant of the
                                     * current member's parent.
                                     * */
                                    containingType = (IIntermediateType)containingMember.Parent;
                                    break;
                                }
                                else
                                    goto breakBoth;
                            }
                        }
                        else
                            break;
                    }
                breakBoth:
                    return typedName.SymbolReference.GetSymbolType();
            }
            return null;
        }

        internal static IType AscertainType(this TypedName typedName, IIntermediateMember containingMember)
        {
            switch (typedName.Source)
            {
                case TypedNameSource.TypeReference:
                    /* *
                     * A type is explicitly provided.
                     * */
                    return typedName.Reference;
                case TypedNameSource.SymbolReference:
                    /* *
                     * Evaluate the member hierarchy and determine whether
                     * there are type-parameters that are available.
                     * */
                    while (containingMember != null)
                    {
                        /* *
                         * In cases where the member itself contains type-parameters,
                         * i.e. methods.
                         * */
                        if (containingMember is IIntermediateGenericParameterParent)
                        {
                            var topScopeGenericMember = (IIntermediateGenericParameterParent)containingMember;
                            if (topScopeGenericMember.TypeParameters.ContainsKey(typedName.SymbolReference))
                                return (IIntermediateGenericParameter)topScopeGenericMember.TypeParameters[typedName.SymbolReference];
                        }
                        if (containingMember.Parent == null)
                            break;
                        else if (containingMember.Parent is IIntermediateMember)
                            containingMember = (IIntermediateMember)containingMember.Parent;
                        else if (containingMember.Parent is IIntermediateType)
                        {
                            /* *
                             * When the parent is a type, obtain the type variant of the
                             * current member's parent.
                             * */
                            var topScopeType = (IIntermediateType)containingMember.Parent;
                            while (topScopeType != null)
                            {
                                /* *
                                 * In cases where the containing type is a generic capable type.
                                 * */
                                if (topScopeType is IIntermediateGenericType)
                                {
                                    var topScopeGenericType = (IIntermediateGenericType)topScopeType;
                                    if (topScopeGenericType.TypeParameters.ContainsKey(typedName.SymbolReference))
                                        return (IIntermediateGenericParameter)topScopeGenericType.TypeParameters[typedName.SymbolReference];
                                }
                                if (topScopeType.Parent is IIntermediateType)
                                    topScopeType = (IIntermediateType)topScopeType.Parent;
                                else if (topScopeType.Parent is IIntermediateMember)
                                {
                                    containingMember = (IIntermediateMember)topScopeType.Parent;
                                    break;
                                }
                                else
                                    goto breakBoth;
                            }
                        }
                        else
                            break;
                    }
            breakBoth:
                    return typedName.SymbolReference.GetSymbolType();
            }
            return null;
        }

    }
}