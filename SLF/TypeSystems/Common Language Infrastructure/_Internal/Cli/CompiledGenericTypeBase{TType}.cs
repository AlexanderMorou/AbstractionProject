using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.Abstract;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Members;
using AllenCopeland.Abstraction.Slf._Internal.GenericLayer;
using AllenCopeland.Abstraction.Slf.Abstract.Properties;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Members;
using AllenCopeland.Abstraction.Utilities.Collections;
using System.Threading.Tasks;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    internal abstract partial class CompiledGenericTypeBase<TType> :
        CompiledTypeBase<TType>,
        IGenericType<TType>,
        _IGenericTypeRegistrar
        where TType :
            class,
            IGenericType<TType>
    {
        private object typeParamSynch = new object();
        /// <summary>
        /// Data member for <see cref="GenericParameters"/>
        /// </summary>
        private IGenericParameterDictionary<IGenericTypeParameter<TType>, TType> subSet;
        /// <summary>
        /// Data member for <see cref="TypeParameters"/>
        /// </summary>
        private IGenericParameterDictionary<IGenericTypeParameter<TType>, TType> typeParameters;
        private LockedTypeCollection _typeParameters;
        /// <summary>
        /// Data member for <see cref="GenericParameters"/>.
        /// </summary>
        private LockedTypeCollection genericParamsCache;
        /// <summary>
        /// Data member for the generic parameters cache.
        /// </summary>
        private Dictionary<ITypeCollectionBase, TType> genericCache = null;

        /// <summary>
        /// Creates a new <see cref="CompiledGenericTypeBase{TType}"/> with the 
        /// <paramref name="underlyingSystemType"/> provided.
        /// </summary>
        /// <param name="underlyingSystemType">The <see cref="System.Type"/> from which the current
        /// <see cref="CompiledGenericTypeBase{TType}"/> is based.</param>
        public CompiledGenericTypeBase(System.Type underlyingSystemType)
            : base(underlyingSystemType)
        {
        }

        #region IGenericParamParent<IGenericTypeParameter<TType>,TType> Members

        public IGenericParameterDictionary<IGenericTypeParameter<TType>, TType> TypeParameters
        {
            get
            {
                if (this.subSet == null)
                    this.subSet = new LockedGenericParameters<IGenericTypeParameter<TType>, TType>((TType)(object)this, this.OnGetGenericParameters());
                return this.subSet;
            }
        }

        private IEnumerable<IGenericTypeParameter<TType>> OnGetGenericParameters()
        {
            ITypeCollection itc = GenericParameters.ToArray().ToCollection();
            if (this.DeclaringType != null && this.DeclaringType.IsGenericType && this.DeclaringType is IGenericType)
            {
                IGenericType genericDeclaringType = ((IGenericType)(this.DeclaringType));
                int parentCollectionCount = genericDeclaringType.GenericParameters.Count;
                for (int i = 0; i < parentCollectionCount; i++)
                    itc.RemoveAt(0);
            }
            foreach (IGenericTypeParameter<TType> param in itc)
                yield return param;
            yield break;
        }

        #endregion

        #region IGenericParamParent Members

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
                LockedGenericParameters<IGenericTypeParameter<TType>, TType> typeParams = new LockedGenericParameters<IGenericTypeParameter<TType>, TType>(((TType)(object)(this)));
                foreach (Type t in parameters)
                {
                    GenericTypeParameter param = new GenericTypeParameter(((TType)(object)(this)), t);
                    typeParams._AddInternal(param.Name, param);
                }
                int parentParamCount = 0;
                if (this.DeclaringType != null && this.DeclaringType.IsGenericType && this.DeclaringType is IGenericType)
                    parentParamCount = ((IGenericType)(this.DeclaringType)).GenericParameters.Count;
                if (this is IGenericType)
                {
                    this.typeParameters = new LockedGenericParameters<IGenericTypeParameter<TType>, TType>(((TType)(object)(this)));
                    typeParams.Values.Filter(tParam => tParam.Position >= parentParamCount).OnAllP<IGenericTypeParameter<TType>>(param => ((LockedGenericParameters<IGenericTypeParameter<TType>, TType>)(this.typeParameters))._AddInternal(param.Name, param));
                }
                else
                    this.typeParameters = null;
                this._typeParameters = (LockedTypeCollection)typeParams.Values.ToLockedCollection();
                return _typeParameters;
            }
        }

        protected override void Dispose(bool dispose)
        {
            if (dispose)
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
                    this.genericParamsCache.OnAll(q => q.Dispose());
                    this.genericParamsCache._Clear();
                    this.genericParamsCache = null;
                }
                if (genericCache != null)
                {
                    this.genericCache.Values.OnAllP(q => q.Dispose());
                    this.genericCache.Clear();
                    this.genericCache = null;
                }
            }
            base.Dispose(dispose);
        }

        #region IGenericType<TType> Members

        public TType MakeGenericType(ITypeCollectionBase typeParameters)
        {
            if (typeParameters == null)
                throw new ArgumentNullException("typeParameters");
            if (!this.IsGenericTypeDefinition)
                throw new System.InvalidOperationException();
            IType r = null;
            if (this.ContainsGenericType(typeParameters, ref r))
                return (TType)r;
            /* *
             * Make the generic type *before* verifying the 
             * type-parameters.
             * *
             * The cause for this is the verification stage
             * creates test cases for every constraint on every
             * type-parameter contained within the type; therefore,
             * if the constraints contain an instance **where the 
             * type-parameters are equal to the ones passed to
             * this method**, it creates a duplicate that's inserted
             * into the cache and causes the verification stage
             * to fail.  This way, the generic instance auto-inserts
             * itself into the cache on the call below;
             * the verification stage calls up that instance
             * and thereby making IsAssignableFrom validate 
             * properly.
             * */
            TType result = this.OnMakeGenericType(typeParameters);
            try
            {
                this.VerifyTypeParameters(typeParameters);
            }
            catch (ArgumentException e)
            {
                result.Dispose();
                if (this.genericCache.ContainsKey(typeParameters))
                    this.genericCache.Remove(typeParameters);
                throw e;
            }
            return result;
        }

        private bool ContainsGenericType(ITypeCollectionBase typeParameters, ref IType r)
        {
            if (this.genericCache == null)
                return false;
            if (typeParameters == null)
                return false;
            ITypeCollectionBase fd = null;
            ITypeCollectionBase[] keyCopy = null;
            lock (genericCache)
                keyCopy = genericCache.Keys.ToArray();
            Parallel.For(0, keyCopy.Length, (i, parallelLoopState) =>
            //for (int i = 0; i < keyCopy.Length; i++)
            {
                var currentSet = keyCopy[i];
                if (currentSet.Count != typeParameters.Count)
                    return;
                bool allFound = true;
                for (int j = 0; j < typeParameters.Count; j++)
                {
                    var currentElement = typeParameters[j];
                    IType currentAlternate;
                    lock (currentSet)
                        currentAlternate = currentSet[j];
                    if (!currentAlternate.Equals(currentElement))
                    {
                        allFound = false;
                        break;
                    }
                }
                if (allFound)
                {
                    var currentLocked = currentSet as ILockedTypeCollection;
                    if (currentLocked != null && currentLocked.IsDisposed)
                        return;
                    fd = currentSet;
                    parallelLoopState.Stop();
                }
            });
            if (fd == null)
                return false;
            r = this.genericCache[fd];
            return true;
        }

        public TType MakeGenericType(params IType[] typeParameters)
        {
            return MakeGenericType(typeParameters.ToCollection());
        }

        #endregion

        #region IGenericType Members

        public bool IsGenericTypeDefinition
        {
            get { return this.IsGenericType; }
        }

        public bool ContainsGenericParameters
        {
            get { return this.ContainsGenericParameters(); }
        }

        public ILockedTypeCollection GenericParameters
        {
            get
            {
                if (this.genericParamsCache == null)
                    this.genericParamsCache = new LockedTypeCollection(this.GetGenericParameters());
                return this.genericParamsCache;
            }
        }

        IGenericType IGenericType.MakeGenericType(ITypeCollectionBase typeParameters)
        {
            return this.MakeGenericType(typeParameters);
        }

        IGenericType IGenericType.MakeGenericType(params IType[] typeParameters)
        {
            return this.MakeGenericType(typeParameters.ToCollection());
        }

        public IGenericType MakeVerifiedGenericType(ITypeCollectionBase typeParameters)
        {
            IType r = null;
            if (!this.IsGenericTypeDefinition)
                throw new System.InvalidOperationException();
            if (typeParameters.Count != this.GenericParameters.Count)
                throw new ArgumentException("typeParameters");
            if (this.ContainsGenericType(typeParameters, ref r))
                return (TType)r;
            return this.OnMakeGenericType(typeParameters);
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
        /// contained within the <see cref="CompiledGenericTypeBase{TType}"/>.</returns>
        /// <remarks>Performs no type-parameter check.</remarks>
        protected abstract TType OnMakeGenericType(ITypeCollectionBase typeParameters);

        #region _IGenericTypeRegistrar Members

        public void RegisterGenericType(IGenericType targetType, ITypeCollectionBase typeParameters)
        {
            if (this.genericCache == null)
                this.genericCache = new Dictionary<ITypeCollectionBase, TType>();
            IType required = null;
            if (this.ContainsGenericType(typeParameters, ref required))
                return;
            lock (genericCache)
                genericCache.Add(new LockedTypeCollection(typeParameters), (TType)targetType);
        }

        public void UnregisterGenericType(ITypeCollectionBase typeParameters)
        {
            if (this.genericCache == null)
                return;
            ITypeCollectionBase match = null;
            ITypeCollectionBase[] keyCopy;
            lock (this.genericCache)
                keyCopy = this.genericCache.Keys.ToArray();
            Parallel.For(0, keyCopy.Length, (i, parallelLoopState) =>
            {
                var currentSet = keyCopy[i];
                if (currentSet.Count != typeParameters.Count)
                    return;
                bool allFound = true;
                for (int j = 0; j < typeParameters.Count; j++)
                {
                    var currentElement = typeParameters[j];
                    IType currentAlternate;
                    lock (currentSet)
                        currentAlternate = currentSet[j];
                    if (!currentAlternate.Equals(currentElement))
                    {
                        allFound = false;
                        break;
                    }
                }
                if (allFound)
                {
                    var currentLocked = currentSet as ILockedTypeCollection;
                    if (currentLocked != null && currentLocked.IsDisposed)
                        return;
                    match = currentSet;
                    parallelLoopState.Stop();
                }
            });
            if (match == null)
                return;
            /* *
             * Multi-threading requirement, if the generic type which has been
             * disposed is the result of this disposing, this very well could
             * occur.
             * */
            if (this.genericCache == null)
                return;
            lock (this.genericCache)
                genericCache.Remove(match);
        }

        #endregion

        public void ReverifyTypeParameters()
        {
            throw new InvalidOperationException(Resources.TypeConstraintFailure_GenericTypeDefinition);
        }
    }
}
