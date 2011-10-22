using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AllenCopeland.Abstraction.Slf._Internal.Abstract;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Members;
using AllenCopeland.Abstraction.Slf._Internal.GenericLayer;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract.Properties;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Members;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    internal abstract partial class CompiledGenericTypeBase<TTypeIdentifier, TType> :
        CompiledTypeBase<TTypeIdentifier, TType>,
        IGenericType<TTypeIdentifier, TType>,
        _IGenericClosureRegistrar
        where TTypeIdentifier :
            IGenericTypeUniqueIdentifier<TTypeIdentifier>
        where TType :
            class,
            IGenericType<TTypeIdentifier, TType>
    {
        private bool disposing;
        private object typeParamSynch = new object();
        /// <summary>
        /// Data member for <see cref="GenericParameters"/>
        /// </summary>
        private IGenericParameterDictionary<IGenericTypeParameter<TTypeIdentifier, TType>, TType> subSet;
        /// <summary>
        /// Data member for <see cref="TypeParameters"/>
        /// </summary>
        private IGenericParameterDictionary<IGenericTypeParameter<TTypeIdentifier, TType>, TType> typeParameters;
        private LockedTypeCollection _typeParameters;
        /// <summary>
        /// Data member for <see cref="GenericParameters"/>.
        /// </summary>
        private LockedTypeCollection genericParamsCache;
        /// <summary>
        /// Data member for the generic parameters cache.
        /// </summary>
        private GenericTypeCache genericCache;

        /// <summary>
        /// Creates a new <see cref="CompiledGenericTypeBase{TIdentifier, TType}"/> with the 
        /// <paramref name="underlyingSystemType"/> provided.
        /// </summary>
        /// <param name="underlyingSystemType">The <see cref="System.Type"/> from which the current
        /// <see cref="CompiledGenericTypeBase{TIdentifier, TType}"/> is based.</param>
        public CompiledGenericTypeBase(System.Type underlyingSystemType)
            : base(underlyingSystemType)
        {
        }

        #region IGenericParamParent<IGenericTypeParameter<TTypeIdentifier, TType>,TType> Members

        public IGenericParameterDictionary<IGenericTypeParameter<TTypeIdentifier, TType>, TType> TypeParameters
        {
            get
            {
                if (this.subSet == null)
                    this.subSet = new LockedGenericParameters<IGenericTypeParameter<TTypeIdentifier, TType>, TType>((TType)(object)this, this.GetGenericTypeParameterSubset());
                return this.subSet;
            }
        }

        private IEnumerable<IGenericTypeParameter<TTypeIdentifier, TType>> GetGenericTypeParameterSubset()
        {
            ITypeCollection itc = GenericParameters.ToArray().ToCollection();
            if (this.DeclaringType != null && this.DeclaringType.IsGenericConstruct && this.DeclaringType is IGenericType)
            {
                IGenericType genericDeclaringType = ((IGenericType)(this.DeclaringType));
                int parentCollectionCount = genericDeclaringType.GenericParameters.Count;
                itc.RemoveRange(0, parentCollectionCount);
            }
            foreach (IGenericTypeParameter<TTypeIdentifier, TType> param in itc)
                yield return param;
            yield break;
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

        IGenericParameterDictionary IGenericParamParent.TypeParameters
        {
            get { return (IGenericParameterDictionary)this.TypeParameters; ; }
        }

        #endregion

        protected ITypeCollection GetGenericParameters()
        {
            lock (typeParamSynch)
            {
                if (this._typeParameters != null)
                    return this._typeParameters;
                Type[] parameters = this.UnderlyingSystemType.GetGenericArguments();
                LockedGenericParameters<IGenericTypeParameter<TTypeIdentifier, TType>, TType> typeParams = new LockedGenericParameters<IGenericTypeParameter<TTypeIdentifier, TType>, TType>(((TType)(object)(this)));
                foreach (Type t in parameters)
                {
                    GenericTypeParameter param = new GenericTypeParameter(((TType)(object)(this)), t);
                    typeParams._AddInternal(param.UniqueIdentifier, param);
                }
                int parentParamCount = 0;
                if (this.DeclaringType != null && this.DeclaringType.IsGenericConstruct && this.DeclaringType is IGenericType)
                    parentParamCount = ((IGenericType)(this.DeclaringType)).GenericParameters.Count;
                if (this is IGenericType)
                {
                    this.typeParameters = new LockedGenericParameters<IGenericTypeParameter<TTypeIdentifier, TType>, TType>(((TType)(object)(this)));
                    typeParams.Values.Filter(tParam => tParam.Position >= parentParamCount).OnAllP<IGenericTypeParameter<TTypeIdentifier, TType>>(param => ((LockedGenericParameters<IGenericTypeParameter<TTypeIdentifier, TType>, TType>)(this.typeParameters))._AddInternal(param.UniqueIdentifier, param));
                }
                else
                    this.typeParameters = null;
                this._typeParameters = (LockedTypeCollection)typeParams.Values.ToLockedCollection();
                return _typeParameters;
            }
        }

        public override void Dispose()
        {
            if (this.SyncObject == null)
                return;
            lock (this.SyncObject)
            {
                if (this.disposing)
                    return;
                this.disposing = true;
            }
            try
            {
                if (this.subSet != null)
                {
                    this.subSet.Dispose();
                    this.subSet = null;
                }
                if (this.typeParameters != null)
                {
                    this.typeParameters = null;
                }
                if (this.genericParamsCache != null)
                {
                    var genericParamsCacheCopy = this.genericParamsCache.ToArray();
                    foreach (var type in genericParamsCacheCopy)
                        type.Dispose();
                    this.genericParamsCache = null;
                }
                if (genericCache != null)
                {
                    this.genericCache.Dispose();
                    this.genericCache = null;
                }
            }
            finally
            {
                lock (this.SyncObject)
                    this.disposing = false;
                base.Dispose();
            }
        }

        #region IGenericType<TTypeIdentifier, TType> Members

        public TType MakeGenericClosure(ITypeCollectionBase typeParameters)
        {
            if (typeParameters == null)
                throw new ArgumentNullException("typeParameters");
            if (!this.IsGenericDefinition)
                throw new InvalidOperationException();
            var lockedTypeParameters = typeParameters.ToLockedCollection();
            IGenericType genericResult;
            lock (this.SyncObject)
                if (this.genericCache != null && genericCache.TryObtainGenericClosure(lockedTypeParameters, out genericResult))
                    return (TType)genericResult;
            return this.OnMakeGenericClosure(lockedTypeParameters);
        }

        public TType MakeGenericClosure(params IType[] typeParameters)
        {
            return MakeGenericClosure(typeParameters.ToLockedCollection());
        }

        #endregion

        #region IGenericType Members

        public bool IsGenericDefinition
        {
            get { return this.IsGenericConstruct; }
        }

        public bool ContainsGenericParameters
        {
            get { return ((IType)this).ContainsGenericParameters(); }
        }

        public ILockedTypeCollection GenericParameters
        {
            get
            {
                lock (this.SyncObject)
                {
                    if (this.genericParamsCache == null)
                        this.genericParamsCache = new LockedTypeCollection(this.GetGenericParameters());
                    return this.genericParamsCache;
                }
            }
        }

        IGenericType IGenericType.MakeGenericClosure(ITypeCollectionBase typeParameters)
        {
            return this.MakeGenericClosure(typeParameters);
        }

        IGenericType IGenericType.MakeGenericClosure(params IType[] typeParameters)
        {
            return this.MakeGenericClosure(typeParameters.ToCollection());
        }

        #endregion

        /// <summary>
        /// Obtains the <typeparamref name="TType"/> relative to the
        /// <paramref name="typeParameters"/> provided.
        /// </summary>
        /// <param name="typeParameters">The <see cref="IType"/>
        /// series from which to create the generic type.</param>
        /// <returns>A <typeparamref name="TType"/>
        /// instance which replaces the type-parameters
        /// contained within the <see cref="CompiledGenericTypeBase{TIdentifier, TType}"/>.</returns>
        /// <remarks>Performs no type-parameter check.</remarks>
        protected abstract TType OnMakeGenericClosure(ITypeCollectionBase typeParameters);

        #region _IGenericClosureRegistrar Members

        public bool ContainsGenericClosure(ILockedTypeCollection typeParameters)
        {
            lock (this.SyncObject)
            {
                if (this.disposing || this.genericCache == null)
                    return false;
                return this.genericCache.ContainsGenericClosure(typeParameters);
            }
        }

        public IGenericType ObtainGenericClosure(ILockedTypeCollection typeParameters)
        {
            lock (this.SyncObject)
            {
                if (this.disposing || this.genericCache == null)
                    return null;
                return this.genericCache.ObtainGenericClosure(typeParameters);
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
        public void RegisterGenericClosure(IGenericType targetType, ILockedTypeCollection typeParameters)
        {
            if (this.disposing)
                return;
            if (this.genericCache == null)
                this.genericCache = new GenericTypeCache();
            this.genericCache.RegisterGenericType(targetType, typeParameters);
        }

        public void UnregisterGenericClosure(ILockedTypeCollection typeParameters)
        {
            if (this.genericCache == null || this.disposing)
                return;
            this.genericCache.UnregisterGenericType(typeParameters);
        }

        #endregion

        public void ReverifyTypeParameters()
        {
            throw new InvalidOperationException(Resources.TypeConstraintFailure_GenericTypeDefinition);
        }

        public void BeginExodus()
        {
            lock (this.SyncObject)
            {
                if (this.genericCache == null || this.disposing)
                    return;
                this.genericCache.BeginExodus();
            }
        }

        public void EndExodus()
        {
            lock (this.SyncObject)
            {
                if (this.genericCache == null || this.disposing)
                    return;
                this.genericCache.EndExodus();
            }
        }

    }
}
