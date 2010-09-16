using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf._Internal.GenericLayer;
using AllenCopeland.Abstraction.Slf.Abstract.Properties;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using System.Globalization;
using AllenCopeland.Abstraction.Slf.Oil;
using AllenCopeland.Abstraction.Slf.Oil.Expressions;
using AllenCopeland.Abstraction.Slf._Internal.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.Ast
{
    internal partial class SymbolType :
        TypeBase<ISymbolType>,
        ISymbolType,
        ITypeReferenceExpression,
        _IGenericTypeRegistrar,
        IMassTargetHandler
    {
        private int sourceSelector = NameSelection;
        private const int NameSelection = 1;
        private const int ExpressionSelection = 2;
        private IExpression sourceExpression;
        private string name;
        private GenericParameterDictionary typeParameters;
        private GenericTypeCache genericCache = null;
        private string _namespace;
        private IClassType baseType;

        internal SymbolType(IExpression sourceExpression)
        {
            this.sourceExpression = sourceExpression;
            this.sourceSelector = ExpressionSelection;
        }

        internal SymbolType(IExpression sourceExpression, int tParamCount)
            : this(sourceExpression, ExpandTParamNames(tParamCount))
        {
        }


        internal SymbolType(IExpression sourceExpression, params string[] tParamNames)
            : this(sourceExpression)
        {
            if (tParamNames == null)
                return;
            this.typeParameters = new GenericParameterDictionary(this, tParamNames);
        }

        internal SymbolType(string name)
        {
            this.name = name;
        }

        internal SymbolType(string name, int tParamCount)
            : this(name, ExpandTParamNames(tParamCount))
        {
        }

        internal SymbolType(string name, string _namespace)
            : this(name)
        {
            this._namespace = _namespace;
        }

        internal SymbolType(string name, int tParamCount, string _namespace)
            : this(name, tParamCount)
        {
            this._namespace = _namespace;
        }

        private static string[] ExpandTParamNames(int tParamCount)
        {
            string[] result = new string[tParamCount];
            for (int i = 0; i < tParamCount; i++)
                result[i] = string.Format(CultureInfo.CurrentCulture, "T{0}", i);
            return result;
        }


        internal SymbolType(string name, params string[] tParamNames)
        {
            this.name = name;
            if (tParamNames ==  null)
                return;
            this.typeParameters = new GenericParameterDictionary(this, tParamNames);
        }

        #region IGenericType<ISymbolType> Members

        public ISymbolType MakeGenericType(ITypeCollectionBase typeParameters)
        {
            if (typeParameters == null)
                throw new ArgumentNullException("typeParameters");
            if (!this.IsGenericTypeDefinition)
                throw new System.InvalidOperationException();
            if (typeParameters.Count != this.GenericParameters.Count)
                throw new ArgumentException("typeParameters");
            if (this.genericCache != null)
            {
                IGenericType r = null;
                if (this.genericCache.ContainsGenericType(typeParameters, out r))
                    return (ISymbolType)r;
            }
            ISymbolType result = this.OnMakeGenericType(typeParameters);
            return result;
        }

        public ISymbolType MakeGenericType(params IType[] typeParameters)
        {
            return MakeGenericType(typeParameters.ToCollection());
        }

        #endregion

        protected ISymbolType OnMakeGenericType(ITypeCollectionBase typeParameters)
        {
            return new _SymbolType(this, typeParameters);
        }

        public GenericParameterDictionary TypeParameters
        {
            get
            {
                if (this.typeParameters == null)
                    this.typeParameters = new GenericParameterDictionary(this);
                return this.typeParameters;
            }
        }

        #region IGenericParamParent<IGenericTypeParameter<ISymbolType>,ISymbolType> Members

        IGenericParameterDictionary<IGenericTypeParameter<ISymbolType>, ISymbolType> IGenericParamParent<IGenericTypeParameter<ISymbolType>, ISymbolType>.TypeParameters
        {
            get { return this.TypeParameters; }
        }

        #endregion

        #region IGenericParamParent Members

        IGenericParameterDictionary IGenericParamParent.TypeParameters
        {
            get { return this.TypeParameters; }
        }

        #endregion

        #region IGenericType Members

        public bool IsGenericTypeDefinition
        {
            get { return this.IsGenericType; }
        }

        public bool ContainsGenericParameters
        {
            get { return this.IsGenericType; }
        }

        public ILockedTypeCollection GenericParameters
        {
            get { return new LockedTypeCollection(this.TypeParameters.Values.Cast<IType>().ToArray()); }
        }

        IGenericType IGenericType.MakeGenericType(ITypeCollectionBase typeParameters)
        {
            return this.MakeGenericType(typeParameters);
        }

        IGenericType IGenericType.MakeGenericType(params IType[] typeParameters)
        {
            return this.MakeGenericType(typeParameters);
        }

        public IGenericType MakeVerifiedGenericType(ITypeCollectionBase typeParameters)
        {
            return this.MakeGenericType(typeParameters);
        }

        public void ReverifyTypeParameters()
        {
            throw new InvalidOperationException(Resources.TypeConstraintFailure_GenericTypeDefinition);
        }

        #endregion

        protected override bool Equals(ISymbolType other)
        {
            return ReferenceEquals(other, this);
        }

        protected override IType OnGetDeclaringType()
        {
            return null;
        }

        protected override TypeKind TypeImpl
        {
            get { return TypeKind.Other; }
        }

        protected override bool CanCacheImplementsList
        {
            get { return true; }
        }

        protected override ILockedTypeCollection OnGetImplementedInterfaces()
        {
            return LockedTypeCollection.Empty;
        }

        protected override IFullMemberDictionary OnGetMembers()
        {
            return LockedFullMembersBase.Empty;
        }

        protected override INamespaceDeclaration OnGetNameSpace()
        {
            return null;
        }

        protected override AccessLevelModifiers OnGetAccessLevel()
        {
            return AccessLevelModifiers.PrivateScope;
        }

        protected override IAssembly OnGetAssembly()
        {
            return null;
        }

        protected override IArrayType OnMakeArray(int rank)
        {
            return new ArrayType(this, rank);
        }

        protected override IArrayType OnMakeArray(params int[] lowerBounds)
        {
            return new ArrayType(this, lowerBounds);
        }

        protected override IType OnMakeByReference()
        {
            return new ByRefType(this);
        }

        protected override IType OnMakePointer()
        {
            return new PointerType(this);
        }

        protected override IType OnMakeNullable()
        {
            return new NullableType(this);
        }

        public override bool IsGenericType
        {
            get { return this.typeParameters == null ? false : this.TypeParameters.Count > 0; }
        }

        protected override bool IsSubclassOfImpl(IType other)
        {
            return false;
        }

        protected override IType BaseTypeImpl
        {
            get
            {
                if (this.baseType == null)
                    return typeof(System.Object).GetTypeReference();
                return this.baseType;
            }
        }

        protected override ICustomAttributeCollection InitializeCustomAttributes()
        {
            return new CustomAttributeCollection(this);
        }

        protected override string OnGetName()
        {
            if (this.sourceSelector == NameSelection)
            {
                return this.name;
            }
            else if (this.sourceSelector == ExpressionSelection)
            {
                return this.sourceExpression.ToString();
            }
            else
                throw new InvalidOperationException("Invalid state.");
        }

        #region IExpression Members

        ExpressionKind IExpression.Type
        {
            get { return ExpressionKinds.TypeReference; }
        }

        #endregion

        #region IExpression Members

        /// <summary>
        /// Visits the <paramref name="visitor"/> based upon the type of the
        /// <see cref="IExpression"/>.
        /// </summary>
        /// <param name="visitor">The <see cref="IIntermediateCodeVisitor"/> 
        /// to visit.</param>
        /// <remarks>In this instance visits the <paramref name="visitor"/>
        /// through <see cref="IIntermediateCodeVisitor.Visit(ICallFusionStatement)"/>.</remarks>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="visitor"/>
        /// is null.</exception>
        public void Visit(IExpressionVisitor visitor)
        {
            //ToDo: Fix #2.
            visitor.Visit(this);
        }

        #endregion

        protected override string OnGetNamespaceName()
        {
            return this._namespace;
        }

        #region _IGenericTypeRegistrar Members

        public void RegisterGenericType(IGenericType targetType, LockedTypeCollection typeParameters)
        {
            if (this.genericCache == null)
                this.genericCache = new GenericTypeCache();
            this.genericCache.RegisterGenericType(targetType, typeParameters);
        }

        public void UnregisterGenericType(LockedTypeCollection typeParameters)
        {
            if (this.genericCache == null)
                return;
            this.genericCache.UnregisterGenericType(typeParameters);
        }

        #endregion

        #region IMassTargetHandler Members

        public void BeginExodus()
        {
            if (this.genericCache != null)
                this.genericCache.BeginExodus();
        }

        public void EndExodus()
        {
            if (this.genericCache != null)
                this.genericCache.EndExodus();
        }

        #endregion

        #region ITypeReferenceExpression Members

        public IType ReferenceType
        {
            get { return this; }
        }

        #endregion

        #region IMemberParentReferenceExpression Members

        public IMethodReferenceStub GetMethod(string name)
        {
            return new MethodReferenceStub(this, name);
        }

        public IMethodReferenceStub GetMethod(string name, ITypeCollection genericParameters)
        {
            return new MethodReferenceStub(this, name, genericParameters);
        }

        public IMethodPointerReferenceExpression GetMethodPointer(string name, ITypeCollection signature)
        {
            return this.GetMethod(name).GetPointer(signature);
        }

        public IMethodPointerReferenceExpression GetMethodPointer(string name, ITypeCollection signature, ITypeCollection genericParameters)
        {
            return this.GetMethod(name, genericParameters).GetPointer(signature);
        }

        public IIndexerReferenceExpression GetIndexer(string name, params IExpression[] parameters)
        {
            return new IndexerReferenceExpression(name, parameters, this);
        }

        public IPropertyReferenceExpression GetProperty(string name)
        {
            return new PropertyReferenceExpression(name, this);
        }

        public IIndexerReferenceExpression GetIndexer(params IExpression[] parameters)
        {
            return GetIndexer(null, parameters);
        }

        public IFieldReferenceExpression GetField(string name)
        {
            return new FieldReferenceExpression(name, this);
        }

        #endregion
    }
}
