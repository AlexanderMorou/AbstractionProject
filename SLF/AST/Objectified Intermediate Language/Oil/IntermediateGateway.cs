using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal;
using AllenCopeland.Abstraction.Slf._Internal.Abstract;
using AllenCopeland.Abstraction.Slf._Internal.Ast;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Oil;
//using AllenCopeland.Abstraction.Slf._Internal.Oil;
using AllenCopeland.Abstraction.Slf.Oil.Expressions;
using AllenCopeland.Abstraction.Slf.Oil.Members;
using AllenCopeland.Abstraction.Slf.Oil.Modules;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Languages;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
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
        private delegate IType DisambiguateFromSelector(IIntermediateDeclaration target, string symbolReference, IType referenceType);
        private const string nullNamespace_Pattern = ".<NULL>.<{0}>";

        /// <summary>
        /// Randomly generate the 'null namespace' pattern every run.
        /// </summary>
        private readonly static string nullNamespace = string.Format(nullNamespace_Pattern, Guid.NewGuid());
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
        public static ITypeReferenceExpression GetTypeExpression(this IType target)
        {
            if (target == null)
                throw new ArgumentNullException("target");
            if (target is SymbolType)
                return (SymbolType)target;
            lock (typeReferenceCache)
            {
                if (!typeReferenceCache.ContainsKey(target))
                {
                    target.Disposed += typeExpressionTarget_Disposed;
                    typeReferenceCache.Add(target, new TypeReferenceExpression(target));
                }
                return typeReferenceCache[target];
            }
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
                lock (typeReferenceCache)
                {
                    if (typeReferenceCache.ContainsKey(tSender))
                    {
                        typeReferenceCache.Remove(tSender);
                        tSender.Disposed -= typeExpressionTarget_Disposed;
                    }
                }
            }
        }

        /// <summary>
        /// Creates a new <see cref="CommonIntermediateAssembly"/> instance
        /// with the <paramref name="name"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/>
        /// that represents the name of the <see cref="IIntermediateAssembly"/>
        /// created.</param>
        /// <returns>An <see cref="IIntermediateAssembly"/> instance.</returns>
        public static IIntermediateAssembly CreateAssembly(string name)
        {
            return CreateAssembly<CommonIntermediateAssembly>(name);
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

        public static TAssembly CreateAssembly<TAssembly, TLanguage, TProvider>(string name, TProvider provider)
            where TAssembly :
                IIntermediateAssembly<TLanguage, TProvider>
            where TLanguage :
                ILanguage
            where TProvider :
                ILanguageProvider
        {
            return ((TAssembly)(CreateAssemblyBridgeCache<TAssembly>.Bridge.ctor<TLanguage, TProvider>(name, provider).Parts.Add()));
        }

        /// <summary>
        /// Obtains a <see cref="IMethodPointerReferenceExpression"/>
        /// with the <paramref name="signature"/> provided.
        /// </summary>
        /// <param name="target">The <see cref="IMethodReferenceStub"/>
        /// on which the pointer is obtained.</param>
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
            IIntermediateTypeParent typeParent = target.Parent;
            while (typeParent != null)
            {
                if (typeParent is IIntermediateAssembly)
                    return null;
                if (typeParent is IIntermediateNamespaceDeclaration)
                    return ((IIntermediateNamespaceDeclaration)(typeParent)).FullName;
                if (typeParent is IIntermediateType)
                    typeParent = ((IIntermediateType)(typeParent)).Parent;
                else
                    return null;
            }
            return null;
        }
                //*
        #region Symbol Types
        /// <summary>
        /// Obtains a <see cref="ISymbolType">symbol type</see>
        /// for the <paramref name="typeSymbol">type symbol</paramref>
        /// provided.
        /// </summary>
        /// <param name="typeSymbol">The <see cref="String"/> value which
        /// represents the symbol.</param>
        /// <returns>A <see cref="ISymbolType">symbol type</see>
        /// for the <paramref name="typeSymbol">type symbol</paramref>
        /// provided.</returns>
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

        public static ISymbolType GetSymbolType(this string typeSymbol, int genericParameterCount)
        {
            if (typeSymbol == null)
                throw new ArgumentNullException("typeSymbol");
            lock (SymbolTypeCache)
            {
                if (!SymbolTypeCache.ContainsKey(typeSymbol))
                    SymbolTypeCache.Add(typeSymbol, new Dictionary<string, IDictionary<int, ISymbolType>>());
                if (!SymbolTypeCache[typeSymbol].ContainsKey(nullNamespace))
                    SymbolTypeCache[typeSymbol].Add(nullNamespace, new Dictionary<int, ISymbolType>());
                if (!SymbolTypeCache[typeSymbol][nullNamespace].ContainsKey(genericParameterCount))
                    SymbolTypeCache[typeSymbol][nullNamespace].Add(genericParameterCount, new SymbolType(typeSymbol, genericParameterCount));
                return SymbolTypeCache[typeSymbol][nullNamespace][genericParameterCount];
            }
        }

        public static ISymbolType GetSymbolType(this string typeSymbol, params IType[] genericParameters)
        {
            return typeSymbol.GetSymbolType(genericParameters.ToCollection());
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
                    SymbolTypeCache[typeSymbol][nullNamespace].Add(count, new SymbolType(typeSymbol, genericParameters.Count).MakeGenericClosure(genericParameters));
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
                    SymbolTypeCache[typeSymbol][@namespace].Add(count, new SymbolType(typeSymbol, genericParameters.Count, @namespace).MakeGenericClosure(genericParameters));
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

        public static ISymbolType GetSymbolType(this string typeSymbol, params string[] typeParameters)
        {
            return typeSymbol.GetSymbolType(typeParameters.OnAll<string, ISymbolType>(GetSymbolType).ToCollection());
        }

        public static IMethodReferenceStub GetMethod(this ISymbolType symbolType, string methodName, params string[] typeParameterNames)
        {
            var st = symbolType as SymbolType;
            if (st != null)
                return st.GetMethod(methodName, typeParameterNames.ToTypeCollection());
            return symbolType.GetTypeExpression().GetMethod(methodName, typeParameterNames.ToTypeCollection());
        }

        public static ISymbolType GetSymbolType(this string typeSymbol, string @namespace, string[] typeParameters)
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
                    return nameAndType.TypeReference;
                case TypedNameSource.SymbolReference:
                    return nameAndType.SymbolReference.GetSymbolType();
                case TypedNameSource.InvalidReference:
                default:
                    throw new ArgumentException("nameAndType");
            }
        }

        public static ITypeCollection ToTypeCollection(this string[] target)
        {
            if (target == null)
                throw new ArgumentNullException("target");
            IType[] result = new IType[target.Length];
            for (int i = 0; i < target.Length; i++)
                result[i] = target[i].GetSymbolType();
            return result.ToCollection();
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

        internal static IEventSignatureReferenceExpression<TEvent, TEventParent> GetEventSignatureReference<TEvent, TEventParent>(this TEvent target, IMemberParentReferenceExpression source)
            where TEvent :
                IEventSignatureMember<TEvent, TEventParent>
            where TEventParent :
                IEventSignatureParent<TEvent, TEventParent>
        {
            return new EventSignatureReferenceExpression<TEvent, TEventParent>(source, target);
        }

        internal static IEventReferenceExpression<TEvent, TEventParent> GetEventReference<TEvent, TEventParent>(this TEvent target, IMemberParentReferenceExpression source)
            where TEvent :
                IEventMember<TEvent, TEventParent>
            where TEventParent :
                IEventParent<TEvent, TEventParent>
        {
            return new EventReferenceExpression<TEvent, TEventParent>(source, target);
        }

        internal static IEventReferenceExpression GetEventReference(this IEventSignatureMember target, IMemberParentReferenceExpression source)
        {
            var targetParent = target.Parent;
            if (target is IEventMember)
                return ((IEventMember)(target)).GetEventReference(source);
            else if (targetParent is IInterfaceType)
                return ((IInterfaceEventMember)(target)).GetEventSignatureReference<IInterfaceEventMember, IInterfaceType>(source);
            else
                return new UnboundEventReferenceExpression(target.Name, source);
        }

        internal static IEventReferenceExpression GetEventReference(this IEventMember target, IMemberParentReferenceExpression source)
        {
            var targetParent = target.Parent;
            if (targetParent is IClassType)
                return ((IClassEventMember)target).GetEventReference<IClassEventMember, IClassType>(source);
            else if (targetParent is IStructType)
                return ((IStructEventMember)target).GetEventReference<IStructEventMember, IStructType>(source);
            else
                return new UnboundEventReferenceExpression(target.Name, source);
        }

        internal static IPropertyReferenceExpression GetPropertyReference(this IPropertySignatureMember target, IMemberParentReferenceExpression source)
        {
            var targetParent = target.Parent;
            if (target is IPropertyMember)
                return ((IPropertyMember)target).GetPropertyReference(source);
            else if (targetParent is IInterfaceType)
                return ((IInterfacePropertyMember)target).GetPropertySignatureReference<IInterfacePropertyMember, IInterfaceType>(source);
            else
                return new UnboundPropertyReferenceExpression(target.Name, source);
        }

        internal static IPropertyReferenceExpression GetPropertyReference(this IPropertyMember target, IMemberParentReferenceExpression source)
        {
            var targetParent = target.Parent;
            if (targetParent is IClassType)
                return ((IClassPropertyMember)target).GetPropertyReference<IClassPropertyMember, IClassType>(source);
            else if (targetParent is IStructType)
                return ((IStructPropertyMember)target).GetPropertyReference<IStructPropertyMember, IStructType>(source);
            else
                return new UnboundPropertyReferenceExpression(target.Name, source);
        }

        public static IMethodReferenceStub GetReference(this IMethodSignatureMember target, IMemberParentReferenceExpression source)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            if (target is IInterfaceMethodMember)
                return GetMethodReference<IMethodSignatureParameterMember<IInterfaceMethodMember, IInterfaceType>, IInterfaceMethodMember, IInterfaceType>((IInterfaceMethodMember)target, source);
            return new UnboundMethodReferenceStub(source, target.Name);
        }

        public static IMethodReferenceStub GetReference(this IMethodMember target, IMemberParentReferenceExpression source = null)
        {
            if (target is IIntermediateInstanceMember)
            {
                if (source == null)
                    source = new AutoContextMemberSource((IIntermediateInstanceMember)target);
            }
            else if (source == null)
                throw new ArgumentNullException("source");
            if (target is IClassMethodMember)
                return GetMethodReference<IMethodParameterMember<IClassMethodMember, IClassType>, IClassMethodMember, IClassType>(target as IClassMethodMember, source);
            else if (target is IStructMethodMember)
                return GetMethodReference<IMethodParameterMember<IStructMethodMember, IStructType>, IStructMethodMember, IStructType>(target as IStructMethodMember, source);
            else
                return new UnboundMethodReferenceStub(source, target.Name);
        }

        public static IMethodReferenceStub<TSignatureParameter, TSignature, TParent> GetMethodReference<TSignatureParameter, TSignature, TParent>(this TSignature target, IMemberParentReferenceExpression source)
            where TSignatureParameter :
                IMethodSignatureParameterMember<TSignatureParameter, TSignature, TParent>
            where TSignature :
                IMethodSignatureMember<TSignatureParameter, TSignature, TParent>
            where TParent :
                ISignatureParent<TSignature, TSignatureParameter, TParent>
        {
            if (target == null)
                throw new ArgumentNullException("target");
            return new MethodReferenceStub<TSignatureParameter, TSignature, TParent>(source, target, () => new MethodPointerReferenceExpression<TSignatureParameter, TSignature, TParent>.SignatureTypes(target));
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
            else if (targetParent is IEnumType)
                return ((IEnumFieldMember)target).GetFieldReference<IEnumFieldMember, IEnumType>(source);
            else
                return new UnboundFieldReferenceExpression(target.Name, source);
        }

        internal static IFieldReferenceExpression GetFieldReference(this IIntermediateFieldMember target, IMemberParentReferenceExpression source = null)
        {
            if (source == null)
                if (target is IIntermediateInstanceMember)
                    source = new AutoContextMemberSource((IIntermediateInstanceMember)target);
            return GetFieldReference((IFieldMember)target, source);
        }

        /// <summary>
        /// Creates a new <see cref="ICreateInstanceExpression"/> with the
        /// <paramref name="target"/> <see cref="IType"/> and the <paramref name="parameters"/> 
        /// that the new instance to be created.
        /// </summary>
        /// <param name="target">The <see cref="IType"/> which needs a new
        /// instance expression.</param>
        /// <param name="parameters">The <see cref="IExpression"/> series
        /// which denote the constructor parameters.</param>
        /// <returns>A new <see cref="ICreateInstanceExpression"/>
        /// relative to the <paramref name="target"/> and its
        /// constructor's <paramref name="parameters"/>.</returns>
        public static ICreateInstanceExpression NewExpression(this IType target, params IExpression[] parameters)
        {
            return new CreateInstanceExpression(new ConstructorPointerReferenceExpression(new ConstructorReferenceStub(target)), parameters);
        }

        /// <summary>
        /// Creates a new <see cref="ICreateInstanceExpression"/> with the
        /// <paramref name="target"/> <see cref="IType"/> and the <paramref name="parameters"/> 
        /// that the new instance to be created.
        /// </summary>
        /// <param name="target">The <see cref="IType"/> which needs a new
        /// instance expression.</param>
        /// <param name="parameters">The <see cref="IExpressionCollection"/>
        /// which denote the constructor parameters.</param>
        /// <returns>A new <see cref="ICreateInstanceExpression"/>
        /// relative to the <paramref name="target"/> and its
        /// constructor's <paramref name="parameters"/>.</returns>
        public static ICreateInstanceExpression NewExpression(this IType target, IExpressionCollection parameters)
        {
            return new CreateInstanceExpression(new ConstructorPointerReferenceExpression(new ConstructorReferenceStub(target)), parameters);
        }

        /// <summary>
        /// Creates a new <see cref="ICreateInstanceExpression"/> with the
        /// <paramref name="target"/> <see cref="IType"/> and the <paramref name="parameters"/> 
        /// that the new instance to be created.
        /// </summary>
        /// <param name="target">The <see cref="IType"/> which needs a new
        /// instance expression.</param>
        /// <param name="parameters">The <see cref="IEnumerable{T}"/>
        /// of <see cref="IExpression"/> instances which denote the
        /// constructor parameters.</param>
        /// <returns>A new <see cref="ICreateInstanceExpression"/>
        /// relative to the <paramref name="target"/> and its
        /// constructor's <paramref name="parameters"/>.</returns>
        public static ICreateInstanceExpression NewExpression(this IType target, IEnumerable<IExpression> parameters)
        {
            return new CreateInstanceExpression(new ConstructorPointerReferenceExpression(new ConstructorReferenceStub(target)), parameters.ToArray());
        }
        internal static IType AscertainType(this TypedName typedName, IIntermediateType containingType)
        {
            string symbolReference;
            IType referenceType = null;
            switch (typedName.Source)
            {
                case TypedNameSource.TypeReference:
                    /* *
                     * A type is explicitly provided.
                     * */
                    referenceType = typedName.TypeReference;
                    if (!referenceType.IsGenericConstruct && referenceType is ISymbolType &&
                        referenceType.Namespace == null)
                    {
                        symbolReference = referenceType.Name;
                        goto ObtainSymbol;
                    }
                    return typedName.TypeReference;
                case TypedNameSource.SymbolReference:
                    /* *
                     * Evaluate the member hierarchy and determine whether
                     * there are type-parameters that are available.
                     * */
                    symbolReference = typedName.SymbolReference;
            ObtainSymbol:
                    return DisambiguateFromType(containingType, symbolReference, referenceType);
            }
            return null;
        }

        internal static IType AscertainType(this TypedName typedName, IIntermediateMember containingMember)
        {
            string symbolReference;
            IType referenceType = null;
            switch (typedName.Source)
            {
                case TypedNameSource.TypeReference:
                    /* *
                     * A type is explicitly provided.
                     * */
                    referenceType = typedName.TypeReference;
                    if (!referenceType.IsGenericConstruct && referenceType is ISymbolType &&
                        referenceType.Namespace == null)
                    {
                        symbolReference = referenceType.Name;
                        goto ObtainSymbol;
                    }
                    return typedName.TypeReference;
                case TypedNameSource.SymbolReference:
                    /* *
                     * Evaluate the member hierarchy and determine whether
                     * there are type-parameters that are available.
                     * */
                    symbolReference = typedName.SymbolReference;
                ObtainSymbol:
                    return DisambiguateFromMember(containingMember, symbolReference, referenceType);
            }
            return null;
        }

        private static IType DisambiguateFromType(IIntermediateType containingType, string symbolReference, IType referenceType)
        {
            while (containingType != null)
            {
                /* *
                    * In cases where the containing type is a generic capable type.
                    * */
                if (containingType is IIntermediateGenericType)
                {
                    var topScopeGenericType = (IIntermediateGenericType)containingType;
                    if (topScopeGenericType.TypeParameters.ContainsKey(symbolReference))
                        return (IIntermediateGenericParameter)topScopeGenericType.TypeParameters[symbolReference];
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
                            if (topScopeGenericMember.TypeParameters.ContainsKey(symbolReference))
                                return (IIntermediateGenericParameter)topScopeGenericMember.TypeParameters[symbolReference];
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
            if (referenceType != null)
                return referenceType;
            return symbolReference.GetSymbolType();
        }

        private static IType DisambiguateFromMember(IIntermediateMember containingMember, string symbolReference, IType referenceType)
        {
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
                    if (topScopeGenericMember.TypeParameters.ContainsKey(symbolReference))
                        return (IIntermediateGenericParameter)topScopeGenericMember.TypeParameters[symbolReference];
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
                            if (topScopeGenericType.TypeParameters.ContainsKey(symbolReference))
                                return (IIntermediateGenericParameter)topScopeGenericType.TypeParameters[symbolReference];
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
            if (referenceType != null)
                return referenceType;
            return symbolReference.GetSymbolType();
        }

        private static DisambiguateFromSelector GetSelector(this IIntermediateDeclaration declaration)
        {
            if (declaration is IIntermediateType)
                return TypeSelector;
            else if (declaration is IIntermediateMember)
                return MemberSelector;
            else
                return OtherSelector;
        }

        private static DisambiguateFromSelector TypeSelector = 
            (target, symbolReference, referenceType) => 
                DisambiguateFromType((IIntermediateType)target, symbolReference, referenceType);
        private static DisambiguateFromSelector MemberSelector = 
            (target, symbolReference, referenceType) => 
                DisambiguateFromMember((IIntermediateMember)target, symbolReference, referenceType);
        private static DisambiguateFromSelector OtherSelector =
            (target, symbolReference, referenceType) =>
                referenceType;

        /// <summary>
        /// Performs a simple symbol disambiguation from the type-parameters available in the
        /// generic make-up of the originPoint's hierarchy.
        /// </summary>
        /// <param name="sourceType"></param>
        /// <param name="originPoint"></param>
        /// <returns></returns>
        internal static IType SimpleSymbolDisambiguation(this IType sourceType, IIntermediateDeclaration originPoint)
        {
            var selector = originPoint.GetSelector();
            switch (sourceType.ElementClassification)
            {
                case TypeElementClassification.None:
                    if (sourceType is ISymbolType && !sourceType.IsGenericConstruct)
                        return selector(originPoint, sourceType.Name, sourceType);
                    break;
                case TypeElementClassification.Array:
                    var arrayType = sourceType as IArrayType;
                    if (arrayType != null)
                        return sourceType.ElementType.SimpleSymbolDisambiguation(originPoint).MakeArray(arrayType.LowerBounds);
                    else
                        break;
                case TypeElementClassification.Nullable:
                    return sourceType.ElementType.SimpleSymbolDisambiguation(originPoint).MakeNullable();
                case TypeElementClassification.Pointer:
                    return sourceType.ElementType.SimpleSymbolDisambiguation(originPoint).MakePointer();
                case TypeElementClassification.Reference:
                    return sourceType.ElementType.SimpleSymbolDisambiguation(originPoint).MakeByReference();
                case TypeElementClassification.GenericTypeDefinition:
                    var genericSource = sourceType as IGenericType;

                    IType[] gParamCopy = genericSource.GenericParameters.ToArray();
                    bool varies = false;
                    for (int i = 0; i < gParamCopy.Length; i++)
                    {
                        var recent = gParamCopy[i];
                        var current = recent.SimpleSymbolDisambiguation(originPoint);
                        if (current != recent)
                            varies = true;
                        gParamCopy[i] = current;
                    }
                    if (varies)
                        return ((IGenericType)(genericSource.ElementType)).MakeGenericClosure(gParamCopy.ToCollection());
                    else
                        break;
            }
            return sourceType;
        }

        public static bool ContainsSymbols(this IType type)
        {
            if (type is ISymbolType)
                return true;
            else if (type is IGenericType)
            {
                var genericVariant = type as IGenericType;
                if (genericVariant.IsGenericConstruct && !genericVariant.IsGenericDefinition)
                {
                    foreach (var genericParam in genericVariant.GenericParameters)
                        if (genericParam.ContainsSymbols())
                            return true;
                }
                else if (genericVariant.ElementClassification != TypeElementClassification.None && genericVariant.ElementClassification != TypeElementClassification.GenericTypeDefinition)
                    if (genericVariant.ElementType.ContainsSymbols())
                        return true;
            }
            return false;
        }

        ///// <summary>
        ///// Creates a new <see cref="IIntermediateDynamicHandler"/> which manages a series of 
        ///// intermediate dynamic methods and the potential containing assembly required to handle
        ///// statement and expression expansion.
        ///// </summary>
        ///// <param name="autoCollect">Determines whether the <see cref="IIntermediateDynamicHandler"/>
        ///// and associated intermediate assembly should be automatically unloaded when <see cref="IDisposable.Dispose"/>
        ///// is called on it.</param>
        ///// <returns>A new <see cref="IIntermediateDynamicHandler"/>.</returns>
        //public static IIntermediateDynamicHandler CreateDynamicHandler(bool autoCollect = false)
        //{
        //    return new IntermediateDynamicHandler(autoCollect);
        //}

        public static ICreateInstanceExpression GetNew(this IType target, params IExpression[] parameters)
        {
            return new CreateInstanceExpression(new ConstructorPointerReferenceExpression(new ConstructorReferenceStub(target)), parameters);
        }

        public static ICreateInstanceExpression GetNewExpression(this Type target, params IExpression[] parameters)
        {
            return target.GetTypeReference().GetNew(parameters);
        }

        internal static IEnumerable<string> GetTypeNames(this IIntermediateFullTypeDictionary types)
        {
            return (from type in types.Values
                    select type.Entry.Name).Distinct();
        }

        internal static IEnumerable<string> GetNamespaceNames(this IIntermediateNamespaceDictionary namespaces)
        {
            return (from @namespace in namespaces.Values
                    select @namespace.Name);
        }

        internal static IEnumerable<string> GetNamespaceParentIdentifiers(this IIntermediateNamespaceParent parent)
        {
            return (from name in
                        ((from ns in parent.Namespaces.GetNamespaceNames()
                          select ns).Concat(
                            from t in parent.Types.GetTypeNames()
                            select t).Concat(
                              from member in parent.Members.Values
                              select member.Entry.Name)).Distinct()
                    orderby name
                    select name);
        }


        
    }
}