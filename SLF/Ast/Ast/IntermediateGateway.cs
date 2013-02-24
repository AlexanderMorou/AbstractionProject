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
using AllenCopeland.Abstraction.Slf.Ast;
//using AllenCopeland.Abstraction.Slf._Internal.Oil;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using AllenCopeland.Abstraction.Slf.Ast.Modules;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Languages;
using AllenCopeland.Abstraction.Slf.Languages.Cil;
using System.Runtime.CompilerServices;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast
{
    /// <summary>
    /// Provides instantiation members for common dynamic elements.
    /// </summary>
    public static partial class IntermediateGateway
    {
        private delegate IType DisambiguateFromSelector(IIntermediateDeclaration target, string symbolReference, IType referenceType);
        private const string nullNamespace_Pattern = ".<NULL>.<{0}>";

        /// <summary>
        /// The randomly generated the 'null namespace' pattern.
        /// </summary>
        private readonly static string nullNamespace = string.Format(nullNamespace_Pattern, Guid.NewGuid());
        /// <summary>
        /// Contains the <see cref="ISymbolType"/> lookup table that
        /// starts with the symbol, the namespace, then the number of generic parameters.
        /// </summary>
        private static IMultikeyedDictionary<string, string, int, ISymbolType> SymbolTypeCache = new MultikeyedDictionary<string, string, int, ISymbolType>();
        /// <summary>
        /// Represents a null value primitive expression.
        /// </summary>
        public static readonly IPrimitiveExpression NullValue = new PrimitiveNullExpression();
        /// <summary>
        /// Represents a number zero expression.
        /// </summary>
        public static readonly IPrimitiveExpression<int> NumberZero = 0.ToPrimitive();

        /// <summary>
        /// GenericParameter-reference expression cache.
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
        /// Creates a new <see cref="ICommonIntermediateAssembly"/> instance
        /// with the <paramref name="name"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/>
        /// that represents the name of the <see cref="ICommonIntermediateAssembly"/>
        /// created.</param>
        /// <returns>An <see cref="IIntermediateAssembly"/> instance.</returns>
        public static ICommonIntermediateAssembly CreateAssembly(string name)
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

        internal static IIntermediateNamespaceDeclaration GetNamespace(this IIntermediateType target)
        {
            IIntermediateTypeParent typeParent = target.Parent;
            while (typeParent != null)
            {
                if (typeParent is IIntermediateAssembly)
                    return null;
                if (typeParent is IIntermediateNamespaceDeclaration)
                    return ((IIntermediateNamespaceDeclaration)(typeParent));
                if (typeParent is IIntermediateMethodMember)
                {
                    IIntermediateMethodMember method = (IIntermediateMethodMember)typeParent;
                    if (method.Parent is IIntermediateTypeParent)
                        typeParent = ((IIntermediateTypeParent)method.Parent);
                    else
                        return null;
                }
                else if (typeParent is IIntermediateType)
                    typeParent = ((IIntermediateType)(typeParent)).Parent;
                else
                    return null;
            }
            return null;
        }

        internal static string GetNamespaceName(this IIntermediateType target)
        {
            var targetNamespace = target.GetNamespace();
            if (targetNamespace == null)
                return null;
            return targetNamespace.FullName;
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
            ISymbolType result;
            if (!SymbolTypeCache.TryGetValue(typeSymbol, nullNamespace, 0, out result))
                SymbolTypeCache.Add(typeSymbol, nullNamespace, 0, result = new SymbolType(typeSymbol));
            return result;
        }

        /// <summary>
        /// Obtains a symbol type for the given <paramref name="typeSymbol"/>
        /// and the <paramref name="genericParameterCount"/> provided.
        /// </summary>
        /// <param name="typeSymbol">The <see cref="String"/> value representing the
        /// symbol to use within the type.</param>
        /// <param name="genericParameterCount">The number of generic parameters
        /// represented by the type involved.</param>
        /// <returns>A <see cref="ISymbolType"/> that contains the 
        /// <paramref name="typeSymbol"/> and <paramref name="genericParameterCount"/>
        /// number of type-parameters.</returns>
        /// <remarks>Used in cases where the source language needs a symbol representing
        /// a known type, typically a situation where the symbol or set of symbols is known
        /// to be a type by syntax.</remarks>
        public static ISymbolType GetSymbolType(this string typeSymbol, int genericParameterCount)
        {
            return GetSymbolType(typeSymbol, nullNamespace);
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
            ISymbolType genericDefinition;
            if (!SymbolTypeCache.TryGetValue(typeSymbol, nullNamespace, count, out genericDefinition))
                SymbolTypeCache.Add(typeSymbol, nullNamespace, count, genericDefinition = new SymbolType(typeSymbol, count));
            return genericDefinition.MakeGenericClosure(genericParameters);
        }

        public static ISymbolType GetSymbolType(this string typeSymbol, string @namespace)
        {
            if (typeSymbol == null)
                throw new ArgumentNullException("typeSymbol");
            if (@namespace == null)
                throw new ArgumentNullException("namespace");
            ISymbolType result;
            if (!SymbolTypeCache.TryGetValue(typeSymbol, @namespace, 0, out result))
                SymbolTypeCache.Add(typeSymbol, @namespace, 0, result = new SymbolType(typeSymbol, @namespace));
            return result;
        }

        public static ISymbolType GetSymbolType(this string typeSymbol, string @namespace, ITypeCollection genericParameters)
        {
            if (typeSymbol == null)
                throw new ArgumentNullException("typeSymbol");
            if (genericParameters == null)
                throw new ArgumentNullException("genericParameters");
            int count = genericParameters.Count;
            ISymbolType genericDefinition;
            if (!SymbolTypeCache.TryGetValue(typeSymbol, @namespace, count, out genericDefinition))
                SymbolTypeCache.Add(typeSymbol, @namespace, count, genericDefinition = new SymbolType(typeSymbol, count, @namespace));
            return genericDefinition.MakeGenericClosure(genericParameters);
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

        /// <summary>
        /// Obtains a proper <see cref="IType"/> instance
        /// for a <see cref="TypedName"/>.
        /// </summary>
        /// <param name="nameAndType">The <see cref="TypedName"/>
        /// which represents a type and name pairing.</param>
        /// <returns>The <see cref="IType"/>
        /// associated to the <paramref name="nameAndType"/>
        /// provided.</returns>
        /// <remarks>Yields a <see cref="ISymbolType"/> when 
        /// <see cref="TypedName.Source"/> is 
        /// <see cref="TypedNameSource.SymbolReference"/>.</remarks>
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

        /// <summary>
        /// Obtains a <see cref="ITypeCollection"/> for a series
        /// of <see cref="String"/> symbol types.
        /// </summary>
        /// <param name="target">A series of <see cref="String"/> values
        /// which represent the symbol types to obtain a type collection of.</param>
        /// <returns>A <see cref="ITypeCollection"/> which contains the 
        /// series of symbol types.</returns>
        public static ITypeCollection ToTypeCollection(this string[] target)
        {
            if (target == null)
                throw new ArgumentNullException("target");
            IType[] result = new IType[target.Length];
            for (int i = 0; i < target.Length; i++)
                result[i] = target[i].GetSymbolType();
            return result.ToCollection();
        }

        internal static IIndexerSignatureReferenceExpression<TIndexer, TIndexerParent> GetIndexerSignatureReference<TIndexer, TIndexerParent>(this TIndexer target, IMemberParentReferenceExpression source, IEnumerable<IExpression> parameters)
            where TIndexer :
                IIndexerSignatureMember<TIndexer, TIndexerParent>
            where TIndexerParent :
                IIndexerSignatureParent<TIndexer, TIndexerParent>
        {
            return new IndexerSignatureReferenceExpression<TIndexer, TIndexerParent>(target, parameters, source);
        }

        internal static IPropertySignatureReferenceExpression<TProperty, TPropertyParent> GetPropertySignatureReference<TProperty, TPropertyParent>(this TProperty target, IMemberParentReferenceExpression source)
            where TProperty :
                IPropertySignatureMember<TProperty, TPropertyParent>
            where TPropertyParent :
                IPropertySignatureParent<TProperty, TPropertyParent>
        {
            return new PropertySignatureReferenceExpression<TProperty, TPropertyParent>(source, target);
        }

        internal static IPropertyReferenceExpression<TProperty, TPropertyParent> GetPropertyReference<TProperty, TPropertyParent>(this TProperty target, IMemberParentReferenceExpression source)
            where TProperty :
                IPropertyMember<TProperty, TPropertyParent>
            where TPropertyParent :
                IPropertyParent<TProperty, TPropertyParent>
        {
            return new PropertyReferenceExpression<TProperty, TPropertyParent>(source, target);
        }

        internal static IEventReferenceExpression<TEvent, TEventParameter, TEventParent> GetEventReference<TEvent, TEventParameter, TEventParent>(this TEvent @event, IMemberParentReferenceExpression source = null)
            where TEvent :
                IEventSignatureMember<TEvent, TEventParameter, TEventParent>
            where TEventParameter :
                IEventSignatureParameterMember<TEvent, TEventParameter, TEventParent>
            where TEventParent :
                IEventSignatureParent<TEvent, TEventParameter, TEventParent>
        {
            if (@event is IIntermediateInstanceMember)
            {
                if (source == null)
                    source = new AutoContextMemberSource((IIntermediateInstanceMember)@event);
            }
            return new EventReferenceExpression<TEvent, TEventParameter, TEventParent>(source, @event);
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

        public static IFieldReferenceExpression<IClassFieldMember, IClassType> GetReference(this IClassFieldMember target, IMemberParentReferenceExpression source = null)
        {
            return target.GetFieldReference<IClassFieldMember, IClassType>(source);
        }
        public static IFieldReferenceExpression<IEnumFieldMember, IEnumType> GetReference(this IEnumFieldMember target, IMemberParentReferenceExpression source = null)
        {
            return target.GetFieldReference<IEnumFieldMember, IEnumType>(source);
        }
        public static IFieldReferenceExpression<IStructFieldMember, IStructType> GetReference(this IStructFieldMember target, IMemberParentReferenceExpression source = null)
        {
            return target.GetFieldReference<IStructFieldMember, IStructType>(source);
        }

        public static IPropertyReferenceExpression<IClassPropertyMember, IClassType> GetReference(this IClassPropertyMember target, IMemberParentReferenceExpression source = null)
        {
            return GetPropertyReference<IClassPropertyMember, IClassType>(target, source);
        }

        public static IPropertyReferenceExpression<IStructPropertyMember, IStructType> GetReference(this IStructPropertyMember target, IMemberParentReferenceExpression source = null)
        {
            return GetPropertyReference<IStructPropertyMember, IStructType>(target, source);
        }

        public static IPropertySignatureReferenceExpression<IInterfacePropertyMember, IInterfaceType> GetReference(this IInterfacePropertyMember target, IMemberParentReferenceExpression source = null)
        {
            return GetPropertySignatureReference<IInterfacePropertyMember, IInterfaceType>(target, source);
        }

        public static IMethodReferenceStub<IMethodParameterMember<IClassMethodMember, IClassType>, IClassMethodMember, IClassType> GetReference(this IClassMethodMember target, IMemberParentReferenceExpression source = null)
        {
            return GetMethodReference<IMethodParameterMember<IClassMethodMember, IClassType>, IClassMethodMember, IClassType>(target, source);
        }

        public static IMethodReferenceStub<IMethodParameterMember<IStructMethodMember, IStructType>, IStructMethodMember, IStructType> GetReference(this IStructMethodMember target, IMemberParentReferenceExpression source = null)
        {
            return GetMethodReference<IMethodParameterMember<IStructMethodMember, IStructType>, IStructMethodMember, IStructType>(target, source);
        }

        public static IMethodReferenceStub<IMethodSignatureParameterMember<IInterfaceMethodMember, IInterfaceType>, IInterfaceMethodMember, IInterfaceType> GetReference(this IInterfaceMethodMember target, IMemberParentReferenceExpression source = null)
        {
            return GetMethodReference<IMethodSignatureParameterMember<IInterfaceMethodMember, IInterfaceType>, IInterfaceMethodMember, IInterfaceType>(target, source);
        }

        public static IEventReferenceExpression<IClassEventMember, IEventParameterMember<IClassEventMember, IClassType>, IClassType> GetReference(this IClassEventMember target, IMemberParentReferenceExpression source = null)
        {
            return GetEventReference<IClassEventMember, IEventParameterMember<IClassEventMember, IClassType>, IClassType>(target, source);
        }

        public static IEventReferenceExpression<IStructEventMember, IEventParameterMember<IStructEventMember, IStructType>, IStructType> GetReference(this IStructEventMember target, IMemberParentReferenceExpression source = null)
        {
            return GetEventReference<IStructEventMember, IEventParameterMember<IStructEventMember, IStructType>, IStructType>(target, source);
        }

        public static IEventReferenceExpression<IInterfaceEventMember, IEventSignatureParameterMember<IInterfaceEventMember, IInterfaceType>, IInterfaceType> GetReference(this IInterfaceEventMember target, IMemberParentReferenceExpression source = null)
        {
            return GetEventReference<IInterfaceEventMember, IEventSignatureParameterMember<IInterfaceEventMember, IInterfaceType>, IInterfaceType>(target, source);
        }

        internal static IMethodReferenceStub<TSignatureParameter, TSignature, TParent> GetMethodReference<TSignatureParameter, TSignature, TParent>(this TSignature target, IMemberParentReferenceExpression source = null)
            where TSignatureParameter :
                IMethodSignatureParameterMember<TSignatureParameter, TSignature, TParent>
            where TSignature :
                IMethodSignatureMember<TSignatureParameter, TSignature, TParent>
            where TParent :
                ISignatureParent<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TSignatureParameter, TParent>
        {
            if (target is IIntermediateInstanceMember)
            {
                if (source == null)
                    source = new AutoContextMemberSource((IIntermediateInstanceMember)target);
            }
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
            if (target is IIntermediateInstanceMember)
            {
                if (source == null)
                    source = new AutoContextMemberSource((IIntermediateInstanceMember)target);
            }
            return new FieldReferenceExpression<TField, TFieldParent>(target, source);
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
        public static ICreateInstanceExpression GetNewExpression(this IType target, params IExpression[] parameters)
        {
            return target.GetNewExpression(parameters.ToCollection());
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
        public static ICreateInstanceExpression GetNewExpression<T>(this IType target, IExpressionCollection<T> parameters)
            where T :
                IExpression
        {
            return CreateInstanceExpression.GetByExpressionCollection(new ConstructorPointerReferenceExpression(new ConstructorReferenceStub(target)), parameters);
            //return new CreateInstanceExpression(new ConstructorPointerReferenceExpression(new ConstructorReferenceStub(target)), parameters);
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
        public static ICreateInstanceExpression GetNewExpression(this IType target, IEnumerable<IExpression> parameters)
        {
            return target.GetNewExpression(parameters.ToCollection());
        }

        public static ICreateInstanceExpression GetNewExpression(this Type target, ICliManager identityManager, params IExpression[] parameters)
        {
            return identityManager.ObtainTypeReference(target).GetNewExpression(parameters);
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
                    if (topScopeGenericType.TypeParameters.ContainsName(symbolReference))
                        return (IIntermediateGenericParameter)topScopeGenericType.TypeParameters[AstIdentifier.GetGenericParameterIdentifier(symbolReference, true)];
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
                            if (topScopeGenericMember.TypeParameters.ContainsName(symbolReference))
                                return (IIntermediateGenericParameter)topScopeGenericMember.TypeParameters[AstIdentifier.GetGenericParameterIdentifier(symbolReference, false)];
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
                    if (topScopeGenericMember.TypeParameters.ContainsName(symbolReference))
                        return (IIntermediateGenericParameter)topScopeGenericMember.TypeParameters[AstIdentifier.GetGenericParameterIdentifier(symbolReference, false)];
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
                            if (topScopeGenericType.TypeParameters.ContainsName(symbolReference))
                                return (IIntermediateGenericParameter)topScopeGenericType.TypeParameters[AstIdentifier.GetGenericParameterIdentifier(symbolReference, true)];
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
            return SimpleTypeDisambiguationWorker.Disambiguate(sourceType, originPoint);
        }

        /// <summary>
        /// Returns whether the <paramref name="type"/> contains symbols as a
        /// part of its definition.
        /// </summary>
        /// <param name="type">The <see cref="IType"/> to check for symbols.</param>
        /// <returns>true if the type is a <see cref="ISymbolType"/> or
        /// contains generic-parameters which are symbols, or the element
        /// type of the <paramref name="type"/> is a symbol.</returns>
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
            else if (type.ElementClassification != TypeElementClassification.None && type.ElementClassification != TypeElementClassification.GenericTypeDefinition)
                return type.ElementType.ContainsSymbols();
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

        internal static IEnumerable<string> GetTypeNames(this IIntermediateFullTypeDictionary types)
        {
            return (from type in types.Values
                    select type.Entry.Name).Distinct();
        }

        internal static IEnumerable<IGeneralDeclarationUniqueIdentifier> GetTypeIdentifiers<TParent>(this TParent parent, bool types, bool members)
            where TParent :
                IType,
                ITypeParent
        {
            if (types)
                if (members)
                    return parent.Members.Keys.Concat<IGeneralDeclarationUniqueIdentifier>(parent.Types.Keys);
                else
                    return parent.Types.Keys;
            else if (members)
                return parent.Members.Keys;
            else
                return Enumerable.Empty<IGeneralDeclarationUniqueIdentifier>();
        }

        internal static IEnumerable<IGeneralDeclarationUniqueIdentifier> GetNamespaceParentIdentifiers(this IIntermediateNamespaceParent parent, bool namespaces, bool types, bool members)
        {
            if (namespaces)
                if (types)
                    if (members)
                        return parent.Namespaces.Keys.Concat(parent.Types.Keys).Concat(parent.Members.Keys);
                    else
                        return parent.Namespaces.Keys.Concat(parent.Types.Keys);
                else if (members)
                    return parent.Namespaces.Keys.Concat(parent.Members.Keys);
                else
                    return parent.Namespaces.Keys;
            else if (types)
                if (members)
                    return parent.Members.Keys.Concat<IGeneralDeclarationUniqueIdentifier>(parent.Types.Keys);
                else
                    return parent.Types.Keys;
            else if (members)
                return parent.Members.Keys;
            else
                return Enumerable.Empty<IGeneralDeclarationUniqueIdentifier>();
        }

        public static ICreateArrayExpression MakeArrayExpression(this Type underlyingSystemType, ICliManager identityManager, IExpression size = null)
        {
            if (size == null)
                return new MalleableCreateArrayExpression(underlyingSystemType.GetTypeReference(identityManager));
            return new MalleableCreateArrayExpression(underlyingSystemType.GetTypeReference(identityManager), size);
        }

        public static ICreateArrayExpression MakeArrayExpression(this Type underlyingSystemType, int size, ICliManager identityManager)
        {
            return underlyingSystemType.MakeArrayExpression(identityManager, size.ToPrimitive());
        }

        public static ICreateArrayExpression MakeArrayExpression(this Type underlyingSystemType, ICliManager identityManager, params int[] sizes)
        {
            if (sizes == null)
                throw new ArgumentNullException("sizes");
            return underlyingSystemType.MakeArrayExpression(identityManager, (from s in sizes
                                                                              select s.ToPrimitive()).ToArray());
        }

        public static ICreateArrayExpression MakeArrayExpression(this Type underlyingSystemType, ICliManager identityManager, params IExpression[] sizes)
        {
            if (sizes == null)
                throw new ArgumentNullException("sizes");
            return new MalleableCreateArrayExpression(underlyingSystemType.GetTypeReference(identityManager), sizes);
        }

        /// <summary>
        /// Returns the number of elements within the <paramref name="dictionary"/> which
        /// belong to the <paramref name="dictionary"/>'s owning parent partial instance.
        /// </summary>
        /// <typeparam name="TMemberIdentifier"></typeparam>
        /// <typeparam name="TMember"></typeparam>
        /// <typeparam name="TIntermediateMember"></typeparam>
        /// <typeparam name="TParent"></typeparam>
        /// <typeparam name="TIntermediateParent"></typeparam>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public static int PartialCount<TMemberIdentifier, TMember, TIntermediateMember, TParent, TIntermediateParent>(this IIntermediateGroupedMemberDictionary<TParent, TIntermediateParent, TMemberIdentifier, TMember, TIntermediateMember> dictionary)
            where TMemberIdentifier :
                IMemberUniqueIdentifier,
                IGeneralMemberUniqueIdentifier
            where TMember :
                IMember<TMemberIdentifier, TParent>
            where TIntermediateMember :
                IIntermediateMember<TMemberIdentifier, TParent, TIntermediateParent>,
                TMember
            where TParent :
                IMemberParent
            where TIntermediateParent :
                IIntermediateMemberParent,
                IIntermediateSegmentableDeclaration,
                TParent
        {
            int count = 0;
            var parent = dictionary.Parent;
            foreach (var item in dictionary.Values)
                if (object.ReferenceEquals(item.Parent, parent))
                    count++;
            return count;
        }

        public static int PartialCount<TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TSignatureParent, TIntermediateSignatureParent>(this IIntermediateMethodSignatureMemberDictionary<TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TSignatureParent, TIntermediateSignatureParent> dictionary)
            where TSignatureParameter :
                IMethodSignatureParameterMember<TSignatureParameter, TSignature, TSignatureParent>
            where TIntermediateSignatureParameter :
                IIntermediateMethodSignatureParameterMember<TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TSignatureParent, TIntermediateSignatureParent>,
                TSignatureParameter
            where TSignature :
                IMethodSignatureMember<TSignatureParameter, TSignature, TSignatureParent>
            where TIntermediateSignature :
                IIntermediateMethodSignatureMember<TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TSignatureParent, TIntermediateSignatureParent>,
                TSignature
            where TSignatureParent :
                ISignatureParent<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TSignatureParameter, TSignatureParent>
            where TIntermediateSignatureParent :
                TSignatureParent,
                IIntermediateSegmentableDeclaration,
                IIntermediateSignatureParent<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter, TSignatureParent, TIntermediateSignatureParent>
        {
            int count = 0;
            var parent = dictionary.Parent;
            foreach (var item in dictionary.Values)
                if (object.ReferenceEquals(item.Parent, parent))
                    count++;
            return count;
        }

        public static IEnumerable<KeyValuePair<IGeneralGenericSignatureMemberUniqueIdentifier, TIntermediateSignature>> GetPartialItems<TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TSignatureParent, TIntermediateSignatureParent>(this IIntermediateMethodSignatureMemberDictionary<TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TSignatureParent, TIntermediateSignatureParent> dictionary)
            where TSignatureParameter :
                IMethodSignatureParameterMember<TSignatureParameter, TSignature, TSignatureParent>
            where TIntermediateSignatureParameter :
                IIntermediateMethodSignatureParameterMember<TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TSignatureParent, TIntermediateSignatureParent>,
                TSignatureParameter
            where TSignature :
                IMethodSignatureMember<TSignatureParameter, TSignature, TSignatureParent>
            where TIntermediateSignature :
                IIntermediateMethodSignatureMember<TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TSignatureParent, TIntermediateSignatureParent>,
                TSignature
            where TSignatureParent :
                ISignatureParent<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TSignatureParameter, TSignatureParent>
            where TIntermediateSignatureParent :
                TSignatureParent,
                IIntermediateSegmentableDeclaration,
                IIntermediateSignatureParent<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter, TSignatureParent, TIntermediateSignatureParent>
        {
            var parent = dictionary.Parent;
            foreach (var item in dictionary.Values)
                if (object.ReferenceEquals(item.Parent, parent))
                    yield return new KeyValuePair<IGeneralGenericSignatureMemberUniqueIdentifier, TIntermediateSignature>(item.UniqueIdentifier, item);
        }

        public static IEnumerable<IGeneralGenericSignatureMemberUniqueIdentifier> GetPartialKeys<TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TSignatureParent, TIntermediateSignatureParent>(this IIntermediateMethodSignatureMemberDictionary<TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TSignatureParent, TIntermediateSignatureParent> dictionary)
            where TSignatureParameter :
                IMethodSignatureParameterMember<TSignatureParameter, TSignature, TSignatureParent>
            where TIntermediateSignatureParameter :
                IIntermediateMethodSignatureParameterMember<TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TSignatureParent, TIntermediateSignatureParent>,
                TSignatureParameter
            where TSignature :
                IMethodSignatureMember<TSignatureParameter, TSignature, TSignatureParent>
            where TIntermediateSignature :
                IIntermediateMethodSignatureMember<TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TSignatureParent, TIntermediateSignatureParent>,
                TSignature
            where TSignatureParent :
                ISignatureParent<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TSignatureParameter, TSignatureParent>
            where TIntermediateSignatureParent :
                TSignatureParent,
                IIntermediateSegmentableDeclaration,
                IIntermediateSignatureParent<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter, TSignatureParent, TIntermediateSignatureParent>
        {
            var parent = dictionary.Parent;
            foreach (var item in dictionary.Values)
                if (object.ReferenceEquals(item.Parent, parent))
                    yield return item.UniqueIdentifier;
        }

        public static IEnumerable<TIntermediateSignature> GetPartialValues<TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TSignatureParent, TIntermediateSignatureParent>(this IIntermediateMethodSignatureMemberDictionary<TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TSignatureParent, TIntermediateSignatureParent> dictionary)
            where TSignatureParameter :
                IMethodSignatureParameterMember<TSignatureParameter, TSignature, TSignatureParent>
            where TIntermediateSignatureParameter :
                IIntermediateMethodSignatureParameterMember<TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TSignatureParent, TIntermediateSignatureParent>,
                TSignatureParameter
            where TSignature :
                IMethodSignatureMember<TSignatureParameter, TSignature, TSignatureParent>
            where TIntermediateSignature :
                IIntermediateMethodSignatureMember<TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TSignatureParent, TIntermediateSignatureParent>,
                TSignature
            where TSignatureParent :
                ISignatureParent<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TSignatureParameter, TSignatureParent>
            where TIntermediateSignatureParent :
                TSignatureParent,
                IIntermediateSegmentableDeclaration,
                IIntermediateSignatureParent<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter, TSignatureParent, TIntermediateSignatureParent>
        {
            var parent = dictionary.Parent;
            foreach (var item in dictionary.Values)
                if (object.ReferenceEquals(item.Parent, parent))
                    yield return item;
        }

        internal static bool IsDeclarationGenericConstruct(this IIntermediateDeclaration target)
        {
            var current = target;
            while (current != null)
            {
                {
                    var currentType = current as IIntermediateType;
                    if (currentType == null)
                        goto notType;
                    if (currentType != null && currentType.IsGenericConstruct)
                        return true;
                    var currentTypeParent = currentType.Parent;
                    if (currentTypeParent is IIntermediateDeclaration)
                    {
                        current = (IIntermediateDeclaration)currentTypeParent;
                        continue;
                    }
                }
            notType:
                {
                    var currentGenericParent = current as IIntermediateGenericParameterParent;
                    if (currentGenericParent != null && currentGenericParent.IsGenericConstruct)
                        return true;
                }
                {
                    var currentMember = current as IIntermediateMember;
                    if (currentMember == null)
                        break;
                    var currentMemberParent = currentMember.Parent;
                    if (currentMemberParent is IIntermediateDeclaration)
                    {
                        current = currentMemberParent as IIntermediateDeclaration;
                        continue;
                    }
                    break;
                }
            }
            return false;
        }

        internal static IType AdjustParameterType(TypedName nameAndType, IIntermediateDeclaration originPoint)
        {
            IType resultType;
            switch (nameAndType.Source)
            {
                case TypedNameSource.TypeReference:
                    if (nameAndType.TypeReference.ContainsSymbols())
                        resultType = nameAndType.TypeReference.SimpleSymbolDisambiguation(originPoint);
                    else
                        resultType = nameAndType.TypeReference;
                    break;
                case TypedNameSource.SymbolReference:
                    resultType = nameAndType.SymbolReference.GetSymbolType().SimpleSymbolDisambiguation(originPoint);
                    break;
                case TypedNameSource.InvalidReference:
                default:
                    throw new ArgumentException("nameAndType");
            }
            if (nameAndType.Direction != ParameterCoercionDirection.In)
                if (resultType.ElementClassification != TypeElementClassification.Reference)
                    resultType = resultType.MakeByReference();
            return resultType;
        }

        internal static void SetAttributeUsage(IIntermediateClassType targetAttribute, AttributeTargets targets, IIntermediateCliManager identityManager)
        {
            if (targetAttribute == null)
                throw new ArgumentNullException();
            var rootMetadatum = identityManager.ObtainTypeReference(identityManager.RuntimeEnvironment.GetCoreIdentifier(CliRuntimeCoreType.RootMetadatum), targetAttribute.Assembly);
            if (!targetAttribute.IsSubclassOf(rootMetadatum))
                throw new ArgumentException(string.Format("targetAttribute must be derived from {0}", rootMetadatum.FullName), "targetAttribute");

        }

        public static IEnumType AttributeTargetsEnum(IIntermediateAssembly assembly, IIntermediateCliManager identityManager)
        {
            var attributeTargets = typeof(AttributeTargets);
            var targetsInternal = identityManager.RuntimeEnvironment.UseCoreLibrary ?
                    identityManager.ObtainTypeReference(identityManager.RuntimeEnvironment.CoreLibraryIdentifier.GetTypeIdentifier(attributeTargets.Namespace, attributeTargets.Name)) :
                    identityManager.ObtainTypeReference(AstIdentifier.GetTypeIdentifier(attributeTargets.Namespace, attributeTargets.Name), assembly);
            return targetsInternal as IEnumType;
        }

    }
}