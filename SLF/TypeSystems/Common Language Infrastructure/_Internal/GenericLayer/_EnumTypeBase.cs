using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Abstract.Properties;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf._Internal.GenericLayer.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.GenericLayer
{
    internal partial class _EnumTypeBase :
        TypeBase<IEnumType>,
        _DummyEnumType
    {
        private object disposeLock = new object();
        private IEnumType original;
        private bool disposed = false;
        /// <summary>
        /// The <see cref="IType"/>
        /// </summary>
        private IType declaringType;
        private LockedTypeCollection genericParameters;
        private FieldMemberDictionary fields;
        private _FullMembersBase members;
        protected IEnumType Original { get; private set; }

        internal _EnumTypeBase(IEnumType original, ITypeCollectionBase genericParameters)
        {
            if (!(genericParameters is LockedTypeCollection))
                genericParameters = genericParameters.ToLockedCollection();
            this.original = original;
            this.genericParameters = (LockedTypeCollection)genericParameters;
            /* *
             * Allow the original to cache this series of generic
             * parameters to the current instance.
             * */
            if (original is _IGenericTypeRegistrar)
                ((_IGenericTypeRegistrar)(original)).RegisterGenericType(this, this.genericParameters);
            foreach (var type in this.genericParameters)
                type.Disposed += new EventHandler(genericParameter_Disposed);
            this.Original = original;
        }

        void genericParameter_Disposed(object sender, EventArgs e)
        {
            if (this.IsDisposed && sender is IType)
            {
                var typeSender = sender as IType;
                typeSender.Disposed -= new EventHandler(genericParameter_Disposed);
                return;
            }
            this.Dispose();
        }

        protected override bool Equals(IEnumType other)
        {
            if (object.ReferenceEquals(this, other))
                return true;
            return false;
        }

        protected override IType OnGetDeclaringType()
        {
            if (this.IsDisposed)
                throw new InvalidOperationException(Utilities.Properties.Resources.ObjectStateThrowMessage);
            if (this.Original is ICompiledType)
            {
                if (this.declaringType == null)
                    this.declaringType = this.OnGetDeclaringTypeImpl();
                return this.declaringType;
            }
            else
                /* *
                 * Can't predict the volatility of non-compiled types.
                 * */
                return this.OnGetDeclaringTypeImpl();
        }

        protected bool IsDisposed
        {
            get
            {
                lock (this.disposeLock)
                    return this.disposed;
            }
        }
        protected override void Dispose(bool dispose)
        {
            if (CLIGateway.CompiledTypeCache.Values.Contains(this))
                this.RemoveFromCache();
            if (this.IsDisposed)
                return;
            lock (disposeLock)
            {
                if (dispose)
                {
                    this.disposed = true;
                    if (Original is _IGenericTypeRegistrar)
                        ((_IGenericTypeRegistrar)(Original)).UnregisterGenericType(this.genericParameters);
                    this.genericParameters = null;
                    this.Original = null;
                }
            }
            base.Dispose(dispose);
        }

        private IType OnGetDeclaringTypeImpl()
        {
            IType declType = this.Original.DeclaringType;
            if (declType == null)
                return null;
            else
            {
                if (this.Original.IsGenericConstruct && declType is IGenericType)
                {
                    IGenericType genericParent = ((IGenericType)(declType));
                    if (genericParent.IsGenericConstruct)
                    {
                        if (!genericParent.IsGenericDefinition)
                            genericParent = (IGenericType)genericParent.ElementType;
                        return genericParent.MakeGenericClosure(this.GenericParameters.Take(genericParent.GenericParameters.Count).ToCollection());
                    }
                    else
                        return genericParent;
                }
                else
                    return declType;
            }
        }

        #region IGenericType Members

        public bool IsGenericDefinition
        {
            get { return false; }
        }

        public bool ContainsGenericParameters
        {
            get { return this.ContainsGenericParameters(); }
        }

        public ILockedTypeCollection GenericParameters
        {
            get { return this.genericParameters; }
        }

        IGenericType IGenericType.MakeGenericClosure(ITypeCollectionBase typeParameters)
        {
            return this.MakeGenericClosure(typeParameters);
        }

        IGenericType IGenericType.MakeGenericClosure(params IType[] typeParameters)
        {
            return this.MakeGenericClosure(typeParameters);
        }

        #endregion

        public _DummyEnumType MakeGenericClosure(ITypeCollectionBase typeParameters)
        {
            throw new InvalidOperationException(Resources.MakeGenericTypeError_IsGenericTypeDefFalse);
        }

        public _DummyEnumType MakeGenericClosure(params IType[] typeParameters)
        {
            throw new InvalidOperationException(Resources.MakeGenericTypeError_IsGenericTypeDefFalse);
        }

        protected override IArrayType OnMakeArray(int rank)
        {
            if (this.IsDisposed)
                throw new InvalidOperationException(Utilities.Properties.Resources.ObjectStateThrowMessage);
            return new ArrayType(this, rank);
        }

        protected override IArrayType OnMakeArray(params int[] lowerBounds)
        {
            if (this.IsDisposed)
                throw new InvalidOperationException(Utilities.Properties.Resources.ObjectStateThrowMessage);
            return new ArrayType(this, lowerBounds);
        }

        protected override IType OnMakeByReference()
        {
            if (this.IsDisposed)
                throw new InvalidOperationException(Utilities.Properties.Resources.ObjectStateThrowMessage);
            return new ByRefType(this);
        }

        protected override IType OnMakePointer()
        {
            if (this.IsDisposed)
                throw new InvalidOperationException(Utilities.Properties.Resources.ObjectStateThrowMessage);
            return new PointerType(this);
        }

        protected override IType OnMakeNullable()
        {
            if (this.IsDisposed)
                throw new InvalidOperationException(Utilities.Properties.Resources.ObjectStateThrowMessage);
            return new NullableType(this);
        }

        protected override bool IsSubclassOfImpl(IType other)
        {
            if (this.IsDisposed)
                throw new InvalidOperationException(Utilities.Properties.Resources.ObjectStateThrowMessage);
            return other.Equals(typeof(object).GetTypeReference());
        }

        protected override ICustomAttributeCollection InitializeCustomAttributes()
        {
            if (this.IsDisposed)
                throw new InvalidOperationException(Utilities.Properties.Resources.ObjectStateThrowMessage);
            return this.Original.CustomAttributes;
        }

        protected override string OnGetNamespaceName()
        {
            if (this.IsDisposed)
                throw new InvalidOperationException(Utilities.Properties.Resources.ObjectStateThrowMessage);
            return this.Original.NamespaceName;
        }

        #region _IGenericType Members

        public void PositionalShift(int from, int to)
        {
            if (this.IsDisposed)
                return;
            if (from < 0 || from >= this.genericParameters.Count)
                throw new ArgumentOutOfRangeException("from");
            if (to < 0 || to >= this.genericParameters.Count)
                throw new ArgumentOutOfRangeException("to");
            if (from == to)
                return;
            var items = this.genericParameters.ToArray();
            bool backwards = from > to;
            var item = items[from];
            if (backwards)
                for (int i = from; i > to; i--)
                    items[i] = items[i - 1];
            else
                for (int i = from; i < to; i++)
                    items[i] = items[i + 1];

            items[to] = item;
            var unlockedGenericParams = this.genericParameters.copy;

            unlockedGenericParams.Clear();
            unlockedGenericParams.AddRange(items);
        }
        #endregion

        public override TypeElementClassification ElementClassification
        {
            get
            {
                return TypeElementClassification.GenericTypeDefinition;
            }
        }

        protected override IType OnGetElementType()
        {
            return this.original;
        }

        protected override TypeKind TypeImpl
        {
            get { return TypeKind.Enumerator; }
        }

        protected override bool CanCacheImplementsList
        {
            get {
                return true;
            }
        }

        protected override ILockedTypeCollection OnGetImplementedInterfaces()
        {
            return LockedTypeCollection.Empty;
        }

        protected override IFullMemberDictionary OnGetMembers()
        {
            this.CheckFields();
            return this._Members;
        }

        protected override INamespaceDeclaration OnGetNameSpace()
        {
            if (this.IsDisposed)
                throw new InvalidOperationException(Utilities.Properties.Resources.ObjectStateThrowMessage);
            return Original.Namespace;
        }

        protected override AccessLevelModifiers OnGetAccessLevel()
        {
            if (this.IsDisposed)
                throw new InvalidOperationException(Utilities.Properties.Resources.ObjectStateThrowMessage);
            return Original.AccessLevel;
        }

        protected override IAssembly OnGetAssembly()
        {
            if (this.IsDisposed)
                throw new InvalidOperationException(Utilities.Properties.Resources.ObjectStateThrowMessage);
            return Original.Assembly;
        }

        public override bool IsGenericConstruct
        {
            get { return true; }
        }

        protected override IType BaseTypeImpl
        {
            get { return Original.BaseType; }
        }

        protected override string OnGetName()
        {
            if (this.IsDisposed)
                throw new InvalidOperationException(Utilities.Properties.Resources.ObjectStateThrowMessage);
            return this.Original.Name;
        }

        #region IGenericType Members

        public void ReverifyTypeParameters()
        {
            return;
        }

        #endregion

        #region IGenericParamParent Members

        IGenericParameterDictionary IGenericParamParent.TypeParameters
        {
            get { throw new InvalidOperationException(Resources.MakeGenericTypeError_IsGenericTypeDefFalse); }
        }

        #endregion

        #region IMassTargetHandler Members

        public void BeginExodus()
        {
            throw new NotSupportedException("Generic type instances are singletons which are part of an exodus, not the harbinger.");
        }

        public void EndExodus()
        {
            throw new NotSupportedException("Generic type instances are singletons which are part of an exodus, not the harbinger.");
        }

        #endregion

        private FieldMemberDictionary Fields
        {
            get
            {
                CheckFields();
                return this.fields;
            }
        }

        private void CheckFields()
        {
            if (this.fields == null)
                this.fields = new FieldMemberDictionary(this._Members, this.original.Fields, this);
        }

        #region IFieldParent<IEnumFieldMember,IEnumType> Members

        IFieldMemberDictionary<IEnumFieldMember, IEnumType> IFieldParent<IEnumFieldMember,IEnumType>.Fields
        {
            get { return this.Fields; }
        }

        #endregion

        #region IFieldParent Members

        IFieldMemberDictionary IFieldParent.Fields
        {
            get { return this.Fields; }
        }

        #endregion

        public _FullMembersBase _Members
        {
            get
            {
                if (this.members == null)
                    this.members = new _FullMembersBase();
                return this.members;
            }
        }

        #region IGenericParamParent Members


        IGenericParamParent IGenericParamParent.MakeGenericClosure(ITypeCollectionBase typeParameters)
        {
            throw new InvalidOperationException(Resources.MakeGenericTypeError_IsGenericTypeDefFalse);
        }

        IGenericParamParent IGenericParamParent.MakeGenericClosure(params IType[] typeParameters)
        {
            throw new InvalidOperationException(Resources.MakeGenericTypeError_IsGenericTypeDefFalse);
        }

        #endregion
    }
}
