using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.Abstract;
using AllenCopeland.Abstraction.Slf._Internal.GenericLayer;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract.Properties;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Oil;
using AllenCopeland.Abstraction.Slf.Oil.Expressions;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.Ast
{
    internal partial class SymbolType :
        TypeBase<IGeneralGenericTypeUniqueIdentifier, ISymbolType>,
        ISymbolType,
        ITypeReferenceExpression,
        _IGenericClosureRegistrar,
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
        //private IClassType baseType;

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

        #region IGenericType<IGeneralGenericTypeUniqueIdentifier, ISymbolType> Members

        public ISymbolType MakeGenericClosure(ITypeCollectionBase typeParameters)
        {
            if (typeParameters == null)
                throw new ArgumentNullException("typeParameters");
            if (!this.IsGenericDefinition)
                throw new System.InvalidOperationException();
            if (typeParameters.Count != this.GenericParameters.Count)
                throw ThrowHelper.ObtainArgumentException(ArgumentWithException.typeParameters, ArgumentExceptionMessage.GenericClosureReplacementCount);
            var lockedTypeParameters = typeParameters.ToLockedCollection();
            lock (this.SyncObject)
                if (this.genericCache != null && this.genericCache.ContainsGenericClosure(lockedTypeParameters))
                    return (ISymbolType)genericCache.ObtainGenericClosure(lockedTypeParameters);
            return this.OnMakeGenericClosure(lockedTypeParameters);
        }

        public ISymbolType MakeGenericClosure(params IType[] typeParameters)
        {
            return MakeGenericClosure(typeParameters.ToCollection());
        }

        #endregion

        protected ISymbolType OnMakeGenericClosure(ITypeCollectionBase typeParameters)
        {
            return new _SymbolType(this, typeParameters);
        }

        public GenericParameterDictionary TypeParameters
        {
            get
            {
                lock (this.SyncObject)
                {
                    if (this.typeParameters == null)
                        this.typeParameters = new GenericParameterDictionary(this);
                    return this.typeParameters;
                }
            }
        }

        #region IGenericParamParent<IGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, ISymbolType>,ISymbolType> Members

        IGenericParameterDictionary<IGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, ISymbolType>, ISymbolType> IGenericParamParent<IGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, ISymbolType>, ISymbolType>.TypeParameters
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

        public bool IsGenericDefinition
        {
            get { return this.IsGenericConstruct; }
        }

        public bool ContainsGenericParameters
        {
            get { return this.IsGenericConstruct; }
        }

        public ILockedTypeCollection GenericParameters
        {
            get { return new LockedTypeCollection(this.TypeParameters.Values.Cast<IType>().ToArray()); }
        }

        IGenericType IGenericType.MakeGenericClosure(ITypeCollectionBase typeParameters)
        {
            return this.MakeGenericClosure(typeParameters);
        }

        IGenericType IGenericType.MakeGenericClosure(params IType[] typeParameters)
        {
            return this.MakeGenericClosure(typeParameters);
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

        protected override INamespaceDeclaration OnGetNamespace()
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

        public override bool IsGenericConstruct
        {
            get
            {
                lock (this.SyncObject)
                    return this.typeParameters == null ? false : this.TypeParameters.Count > 0;
            }
        }

        protected override bool IsSubclassOfImpl(IType other)
        {
            return false;
        }

        protected override IType BaseTypeImpl
        {
            get
            {
                return null;
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
            get { return ExpressionKind.TypeReference; }
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
        /// through <see cref="IExpressionVisitor.Visit(ITypeReferenceExpression)"/>.</remarks>
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

        #region _IGenericClosureRegistrar Members

        public void RegisterGenericClosure(IGenericType targetType, ILockedTypeCollection typeParameters)
        {
            lock (this.SyncObject)
            {
                if (this.genericCache == null)
                    this.genericCache = new GenericTypeCache();
                this.genericCache.RegisterGenericType(targetType, typeParameters);
            }
        }

        public void UnregisterGenericClosure(ILockedTypeCollection typeParameters)
        {
            lock (this.SyncObject)
            {
                if (this.genericCache == null)
                    return;
                this.genericCache.UnregisterGenericType(typeParameters);
            }
        }

        public bool TryObtainGenericClosure(ILockedTypeCollection typeParameters, out IGenericType genericClosure)
        {
            lock (this.SyncObject)
            {
                if (this.genericCache == null)
                {
                    genericClosure = null;
                    return false;
                }
                return this.genericCache.TryObtainGenericClosure(typeParameters, out genericClosure);
            }
        }

        public bool ContainsGenericClosure(ILockedTypeCollection typeParameters)
        {
            lock (this.SyncObject)
            {
                if (this.genericCache == null)
                    return false;
                return this.genericCache.ContainsGenericClosure(typeParameters);
            }
        }

        public IGenericType ObtainGenericClosure(ILockedTypeCollection typeParameters)
        {
            lock (this.SyncObject)
            {
                if (this.genericCache == null)
                    return null;
                return this.genericCache.ObtainGenericClosure(typeParameters);
            }
        }

        #endregion

        #region IMassTargetHandler Members

        public void BeginExodus()
        {
            lock (this.SyncObject)
                if (this.genericCache != null)
                    this.genericCache.BeginExodus();
        }

        public void EndExodus()
        {
            lock (this.SyncObject)
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
            return new UnboundMethodReferenceStub(this, name);
        }

        public IMethodReferenceStub GetMethod(string name, ITypeCollection genericParameters)
        {
            return new UnboundMethodReferenceStub(this, name, genericParameters);
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
            return new UnboundIndexerReferenceExpression(name, parameters, this);
        }

        public IPropertyReferenceExpression GetProperty(string name)
        {
            return new UnboundPropertyReferenceExpression(name, this);
        }

        public IIndexerReferenceExpression GetIndexer(params IExpression[] parameters)
        {
            return GetIndexer(null, parameters);
        }

        public IFieldReferenceExpression GetField(string name)
        {
            return new UnboundFieldReferenceExpression(name, this);
        }

        public IEventReferenceExpression GetEvent(string name)
        {
            return new UnboundEventReferenceExpression(name, this);
        }

        #endregion

        #region IGenericParamParent Members


        IGenericParamParent IGenericParamParent.MakeGenericClosure(ITypeCollectionBase typeParameters)
        {
            return this.MakeGenericClosure(typeParameters);
        }

        IGenericParamParent IGenericParamParent.MakeGenericClosure(params IType[] typeParameters)
        {
            return this.MakeGenericClosure(typeParameters);
        }

        #endregion

        #region ISourceElement Members

        public string FileName { get; set; }

        public LineColumnPair? Start { get; set; }

        public LineColumnPair? End { get; set; }

        #endregion

        public override IEnumerable<string> AggregateIdentifiers
        {
            get { return TypeBase.EmptyIdentifiers; }
        }
    }
}
