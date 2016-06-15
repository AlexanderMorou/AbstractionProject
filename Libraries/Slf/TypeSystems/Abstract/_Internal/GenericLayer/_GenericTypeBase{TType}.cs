using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract.Properties;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf._Internal.Abstract;
/*---------------------------------------------------------------------\
| Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
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
    internal abstract class _GenericTypeBase<TTypeIdentifier, TType> :
        TypeBase<TTypeIdentifier, TType>,
        IGenericType<TTypeIdentifier, TType>,
        _IGenericParamParent,
        IMassTargetHandler
        where TTypeIdentifier :
            IGenericTypeUniqueIdentifier
        where TType :
            class,
            IGenericType<TTypeIdentifier, TType>
    {
        private bool disposed;
        /// <summary>
        /// Data member for <see cref="Original"/>.
        /// </summary>
        private TType original;
        /// <summary>
        /// Data member for <see cref="BaseTypeImpl"/>.
        /// </summary>
        /// <remarks>Caches the base type should</remarks>
        private IType baseType;
        /// <summary>
        /// The <see cref="IType"/>
        /// </summary>
        private IType declaringType;
        private LockedTypeCollection genericParameters;

        /// <summary>
        /// Creates a new <see cref="_GenericTypeBase{TIdentifier, TType}"/> with the
        /// <paramref name="original"/> provided.
        /// </summary>
        /// <param name="original">The <typeparamref name="TType"/>
        /// from which the <see cref="_GenericTypeBase{TIdentifier, TType}"/> operates.</param>
        public _GenericTypeBase(TType original, IControlledTypeCollection genericParameters)
            : base()
        {
            if (!(genericParameters is LockedTypeCollection))
                genericParameters = genericParameters.ToLockedCollection();
            this.original = original;
            this.genericParameters = (LockedTypeCollection)genericParameters;
            /* *
             * Allow the original to cache this series of generic
             * parameters to the current instance.
             * */
            if (original is _IGenericClosureRegistrar)
                ((_IGenericClosureRegistrar)(original)).RegisterGenericClosure(this, this.genericParameters);
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


        public override IEnumerable<IGeneralDeclarationUniqueIdentifier> AggregateIdentifiers
        {
            get { return this.Original.AggregateIdentifiers; }
        }

        #region IGenericType<TTypeIdentifier, TType> Members

        public TType MakeGenericClosure(IControlledTypeCollection typeParameters)
        {
            throw new InvalidOperationException(Resources.MakeGenericTypeError_IsGenericTypeDefFalse);
        }

        public TType MakeGenericClosure(params IType[] typeParameters)
        {
            throw new InvalidOperationException(Resources.MakeGenericTypeError_IsGenericTypeDefFalse);
        }

        #endregion

        #region IGenericParamParent<IGenericTypeParameter<TTypeIdentifier, TType>,IGenericTypeGenericCtorMember<TType>,IGenericTypeGenericCtorParameterMember<TType>,TType> Members

        public IGenericParameterDictionary<IGenericTypeParameter<TTypeIdentifier, TType>, TType> TypeParameters
        {
            get { throw new InvalidOperationException(Resources.MakeGenericTypeError_IsGenericTypeDefFalse); }
        }

        #endregion

        #region IGenericParamParent Members

        IGenericParamParent IGenericParamParent.MakeGenericClosure(IControlledTypeCollection typeParameters)
        {
            return this.MakeGenericClosure(typeParameters);
        }

        IGenericParamParent IGenericParamParent.MakeGenericClosure(params IType[] typeParameters)
        {
            return this.MakeGenericClosure(typeParameters);
        }

        IGenericParameterDictionary IGenericParamParent.TypeParameters
        {
            get { throw new InvalidOperationException(Resources.MakeGenericTypeError_IsGenericTypeDefFalse); }
        }

        #endregion

        #region IGenericType Members

        public bool IsGenericDefinition
        {
            get { return false; }
        }

        public bool ContainsGenericParameters
        {
            get { return ((IType)this).ContainsGenericParameters(); }
        }

        public ILockedTypeCollection GenericParameters
        {
            get { return this.genericParameters; }
        }

        IGenericType IGenericType.MakeGenericClosure(IControlledTypeCollection typeParameters)
        {
            return this.MakeGenericClosure(typeParameters);
        }

        IGenericType IGenericType.MakeGenericClosure(params IType[] typeParameters)
        {
            return this.MakeGenericClosure(typeParameters);
        }


        #endregion

        protected override bool Equals(TType other)
        {
            if (object.ReferenceEquals(this, other))
                return true;
            return false;
        }

        protected override ITypeParent OnGetParent()
        {
            if (this.IsDisposed)
                throw new InvalidOperationException(Utilities.Properties.Resources.ObjectStateThrowMessage);
            //if (this.original is ICliType)
            //{
            //    if (this.declaringType == null)
            //        this.declaringType = this.OnGetParentImpl();
            //    return this.declaringType;
            //}
            //else
            /* *
             * Can't predict the volatility of non-compiled types.
             * */
            return this.OnGetParentImpl();
        }

        private ITypeParent OnGetParentImpl()
        {

            ITypeParent declParent = this.Original.Parent;
            if (declParent == null)
                return null;
            else
            {
                if (this.Original.IsGenericConstruct)
                {
                    if (declParent is IGenericType)
                    {
                        IGenericType genericParent = ((IGenericType)(declParent));
                        if (genericParent.IsGenericConstruct)
                        {
                            if (!genericParent.IsGenericDefinition)
                                genericParent = (IGenericType)genericParent.ElementType;
                            return (ITypeParent)genericParent.MakeGenericClosure(this.GenericParameters.Take(genericParent.GenericParameters.Count).ToCollection());
                        }
                        else
                            return (ITypeParent)genericParent;
                    }
                    else if (declParent is IMethodMember)
                    {
                        IMethodMember genericParent = ((IMethodMember)(declParent));
                        if (genericParent.IsGenericConstruct)
                        {
                            if (!genericParent.IsGenericDefinition)
                                genericParent = (IMethodMember)genericParent.GetGenericDefinition();
                            return (ITypeParent)genericParent.MakeGenericClosure(this.GenericParameters.Take(genericParent.GenericParameters.Count).ToCollection());
                        }
                    }
                }
                return declParent;
            }
        }

        protected override bool CanCacheImplementsList
        {
            get
            {
                if (this.IsDisposed)
                    throw new InvalidOperationException(Utilities.Properties.Resources.ObjectStateThrowMessage);
                /* *
                 * Cheat using internal knowledge of the original, if it's available.
                 * */
                var tbOrig = original as TypeBase<TTypeIdentifier>;
                if (tbOrig != null)
                    return tbOrig._CanCacheImplementsList;
                return false;
            }
        }

        protected override ILockedTypeCollection OnGetImplementedInterfaces()
        {
            if (this.IsDisposed)
                throw new InvalidOperationException(Utilities.Properties.Resources.ObjectStateThrowMessage);
            return this.Original.ImplementedInterfaces.OnAll(q => q.Disambiguify(this.GenericParameters, null, TypeParameterSources.Type)).ToLockedCollection();
        }

        protected override ILockedTypeCollection OnGetDirectImplementedInterfaces()
        {
            if (this.IsDisposed)
                throw new InvalidOperationException(Utilities.Properties.Resources.ObjectStateThrowMessage);
            return this.Original.GetDirectlyImplementedInterfaces().OnAll(q => q.Disambiguify(this.GenericParameters, null, TypeParameterSources.Type)).ToLockedCollection();
        }

        protected override TTypeIdentifier OnGetUniqueIdentifier()
        {
            throw new NotImplementedException();
        }

        protected override INamespaceDeclaration OnGetNamespace()
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
            get
            {
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
        public override void Dispose()
        {
            if (this.IsDisposed)
                return;
            lock (disposeLock)
            {
                this.disposed = true;
                if (original is _IGenericClosureRegistrar)
                    ((_IGenericClosureRegistrar)(original)).UnregisterGenericClosure(this.genericParameters);
                this.genericParameters = null;
                this.original = null;
            }
            base.Dispose();
        }

        public override bool IsGenericConstruct
        {
            get { return true; }
        }

        protected override bool IsSubclassOfImpl(IType other)
        {
            if (this.IsDisposed)
                throw new InvalidOperationException(Utilities.Properties.Resources.ObjectStateThrowMessage);
            return other.Equals(this.IdentityManager.ObtainTypeReference(RuntimeCoreType.RootType));
        }

        protected override IMetadataCollection InitializeMetadata()
        {
            if (this.IsDisposed)
                throw new InvalidOperationException(Utilities.Properties.Resources.ObjectStateThrowMessage);
            return this.Original.Metadata;
        }

        protected override string OnGetNamespaceName()
        {
            if (this.IsDisposed)
                throw new InvalidOperationException(Utilities.Properties.Resources.ObjectStateThrowMessage);
            return this.Original.NamespaceName;
        }


        #region _IGenericParamParent Members

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
            this.genericParameters = new LockedTypeCollection(items);
        }

        #endregion

        protected bool IsDisposed
        {
            get
            {
                lock (this.disposeLock)
                    return this.disposed;
            }
        }

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

        protected override IIdentityManager OnGetManager()
        {
            return this.Original.IdentityManager;
        }

        protected override TypeKind TypeImpl
        {
            get { throw new NotImplementedException(); }
        }

        public override bool Equals(IType other)
        {
            if (other == null)
                return false;
            if (!other.IsGenericConstruct)
                return false;

            if (base.Equals(other))
                return true;
            var otherGT = other as IGenericType;
            if (other == this.ElementType && otherGT != null)
            {
                if (otherGT.GenericParameters.Count == this.GenericParameters.Count)
                    return otherGT.GenericParameters.SequenceEqual(this.GenericParameters);
            }
            if (other.ElementClassification == TypeElementClassification.GenericTypeDefinition &&
                other.ElementType == this.ElementType && otherGT != null)
                return this.GenericParameters.SequenceEqual(otherGT.GenericParameters);
            return false;
        }

    }
}
