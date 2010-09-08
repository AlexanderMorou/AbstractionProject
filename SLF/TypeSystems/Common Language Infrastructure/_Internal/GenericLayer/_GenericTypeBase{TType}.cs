using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Properties;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Members;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.GenericLayer
{
    /* *
     * The primary focus of the generic over layer is to provide
     * a series of closed type functionality for the type core.
     * *
     * It makes more sense to use the same layer for generics
     * since the generics of the intermediate system are not 
     * malleable like the generic definitions are.
     * */
    internal abstract class _GenericTypeBase<TType> :
        TypeBase<TType>,
        IGenericType<TType>,
        _IGenericType
        where TType :
            class,
            IGenericType<TType>
    {
        private bool disposed;
        /// <summary>
        /// Data member for <see cref="Original"/>.
        /// </summary>
        private TType original;
        /// <summary>
        /// Data member for <see cref="BaseType"/>.
        /// </summary>
        /// <remarks>Caches the base type should</remarks>
        private IType baseType;
        /// <summary>
        /// The <see cref="IType"/>
        /// </summary>
        private IType declaringType;
        private ILockedTypeCollection genericParameters;

        /// <summary>
        /// Creates a new <see cref="_GenericTypeBase{TType}"/> with the
        /// <paramref name="original"/> provided.
        /// </summary>
        /// <param name="original">The <typeparamref name="TType"/>
        /// from which the <see cref="_GenericTypeBase{TType}"/> operates.</param>
        public _GenericTypeBase(TType original, ITypeCollectionBase genericParameters)
            : base()
        {
            if (!(genericParameters is ILockedTypeCollection))
                genericParameters = genericParameters.ToLockedCollection();
            this.original = original;
            this.genericParameters = (ILockedTypeCollection)genericParameters;
            /* *
             * Allow the original to cache this series of generic
             * parameters to the current instance.
             * */
            if (original is _IGenericTypeRegistrar)
                ((_IGenericTypeRegistrar)(original)).RegisterGenericType(this, genericParameters);
            foreach (var type in this.genericParameters)
                type.Disposed += new EventHandler(genericParameter_Disposed);
            
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

        public override TypeElementClassification ElementClassification
        {
            get
            {
                return TypeElementClassification.GenericTypeDefinition;
            }
        }

        protected override IType OnGetElementType()
        {
            return this.Original;
        }

        #region IGenericType<TType> Members

        public TType MakeGenericType(ITypeCollectionBase typeParameters)
        {
            throw new InvalidOperationException(Resources.MakeGenericTypeError_IsGenericTypeDefFalse);
        }

        public TType MakeGenericType(params IType[] typeParameters)
        {
            throw new InvalidOperationException(Resources.MakeGenericTypeError_IsGenericTypeDefFalse);
        }

        #endregion

        #region IGenericParamParent<IGenericTypeParameter<TType>,IGenericTypeGenericCtorMember<TType>,IGenericTypeGenericCtorParameterMember<TType>,TType> Members

        public IGenericParameterDictionary<IGenericTypeParameter<TType>, TType> TypeParameters
        {
            get { throw new InvalidOperationException(Resources.MakeGenericTypeError_IsGenericTypeDefFalse); }
        }

        #endregion

        #region IGenericParamParent Members

        IGenericParameterDictionary IGenericParamParent.TypeParameters
        {
            get { throw new InvalidOperationException(Resources.MakeGenericTypeError_IsGenericTypeDefFalse); }
        }

        #endregion

        #region IGenericType Members

        public bool IsGenericTypeDefinition
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
            throw new InvalidOperationException(Resources.MakeGenericTypeError_IsGenericTypeDefFalse);
        }

        #endregion

        protected override bool Equals(TType other)
        {
            if (this.Equals(other))
                return true;
            return false;
        }

        protected override IType OnGetDeclaringType()
        {
            if (this.IsDisposed)
                throw new InvalidOperationException(Utilities.Properties.Resources.ObjectStateThrowMessage);
            if (this.declaringType == null)
                this.declaringType = this.OnGetDeclaringTypeImpl();
            return this.declaringType;
        }

        private IType OnGetDeclaringTypeImpl()
        {
            IType declType = this.Original.DeclaringType;
            if (declType == null)
                return null;
            else
            {
                if (this.Original.IsGenericType && declType is IGenericType)
                {
                    IGenericType genericParent = ((IGenericType)(declType));
                    if (genericParent.IsGenericType)
                    {
                        if (!genericParent.IsGenericTypeDefinition)
                            genericParent = (IGenericType)genericParent.ElementType;
                        return genericParent.MakeGenericType(this.GenericParameters.Take(genericParent.GenericParameters.Count).ToCollection());
                    }
                    else
                        return genericParent;
                }
                else
                    return declType;
            }
        }

        protected override bool CanCacheImplementsList
        {
            get
            {
                if (this.IsDisposed)
                    throw new InvalidOperationException(Utilities.Properties.Resources.ObjectStateThrowMessage);
                /* *
                 * Certifiable that compiled types won't change during the
                 * active runtime lifetime.
                 * */
                return this.Original is ICompiledType;
            }
        }

        protected override ILockedTypeCollection OnGetImplementedInterfaces()
        {
            if (this.IsDisposed)
                throw new InvalidOperationException(Utilities.Properties.Resources.ObjectStateThrowMessage);
            return this.Original.ImplementedInterfaces.OnAll(q => q.Disambiguify(this.GenericParameters, null, TypeParameterSources.Type)).ToLockedCollection();
        }

        protected override INamespaceDeclaration OnGetNameSpace()
        {
            if (this.IsDisposed)
                throw new InvalidOperationException(Utilities.Properties.Resources.ObjectStateThrowMessage);
            return this.original.Namespace;
        }

        protected override AccessLevelModifiers OnGetAccessLevel()
        {
            if (this.IsDisposed)
                throw new InvalidOperationException(Utilities.Properties.Resources.ObjectStateThrowMessage);
            return this.original.AccessLevel;
        }

        protected override IAssembly OnGetAssembly()
        {
            if (this.IsDisposed)
                throw new InvalidOperationException(Utilities.Properties.Resources.ObjectStateThrowMessage);
            return this.original.Assembly;
        }

        protected override IType BaseTypeImpl
        {
            get {
                if (this.IsDisposed)
                    throw new InvalidOperationException(Utilities.Properties.Resources.ObjectStateThrowMessage);
                /* *
                 * Perform a quick check on the base type
                 * to ensure that it hasn't changed
                 * (valid on intermediate cases).
                 * */
                this.BaseTypeCheck();
                if (this.baseType == null)
                    this.baseType = this._BaseTypeImpl;
                return this.baseType;
            }
        }

        private void BaseTypeCheck()
        {
            if (this.IsDisposed)
                return;
            if (this.baseType == null)
                return;
            IType bt = this.baseType;
            if (bt.ElementClassification == TypeElementClassification.GenericTypeDefinition)
                bt = bt.ElementType;
            /* *
             * If the two are inequal, then nullify
             * the local reference to ensure that the
             * base type is reassigned to the proper
             * instance (see get_BaseType).
             * */
            if (bt != this.original.BaseType)
                this.baseType = null;
        }

        private IType _BaseTypeImpl
        {
            get
            {
                if (this.IsDisposed)
                    throw new InvalidOperationException(Utilities.Properties.Resources.ObjectStateThrowMessage);
                if (this.original.BaseType == null)
                    return null;
                return this.Original.BaseType.Disambiguify(this.GenericParameters, null, TypeParameterSources.Type);
            }
        }

        protected override string OnGetName()
        {
            if (this.IsDisposed)
                throw new InvalidOperationException(Utilities.Properties.Resources.ObjectStateThrowMessage);
            return this.original.Name;
        }

        internal TType Original
        {
            get
            {
                return this.original;
            }
        }

        private object disposeLock = new object();
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
                    if (original is _IGenericTypeRegistrar)
                        ((_IGenericTypeRegistrar)(original)).UnregisterGenericType(this.genericParameters);
                    this.genericParameters = null;
                    this.original = null;
                }
            }
            base.Dispose(dispose);
        }

        public override bool IsGenericType
        {
            get { return true; }
        }

        public void ReverifyTypeParameters()
        {
            if (this.IsDisposed)
                return;
            this.ElementType.VerifyTypeParameters(this.GenericParameters);
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
            var unlockedGenericParams = ((LockedTypeCollection)this.genericParameters);

            unlockedGenericParams._Clear();
            unlockedGenericParams._AddRange(items);

        }

        #endregion

        protected bool IsDisposed
        {
            get
            {
                return this.disposed;
            }
        }
    }
}
