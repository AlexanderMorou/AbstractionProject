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
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
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
        private object disposeSynch = new object();
        private bool disposing;
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
        private GenericTypeCache genericCache;

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
            if (this.DeclaringType != null && this.DeclaringType.IsGenericConstruct && this.DeclaringType is IGenericType)
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
                LockedGenericParameters<IGenericTypeParameter<TType>, TType> typeParams = new LockedGenericParameters<IGenericTypeParameter<TType>, TType>(((TType)(object)(this)));
                foreach (Type t in parameters)
                {
                    GenericTypeParameter param = new GenericTypeParameter(((TType)(object)(this)), t);
                    typeParams._AddInternal(param.Name, param);
                }
                int parentParamCount = 0;
                if (this.DeclaringType != null && this.DeclaringType.IsGenericConstruct && this.DeclaringType is IGenericType)
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
            if (this.disposeSynch == null)
                return;
            lock (disposeSynch)
            {
                if (this.disposing)
                    return;
                this.disposing = true;
            }
            try
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
                        this.genericCache.Dispose();
                        this.genericCache = null;
                    }
                }
            }
            finally
            {
                lock (this.disposeSynch)
                    this.disposing = false;
                this.disposeSynch = null;
                base.Dispose(dispose);
            }
        }

        #region IGenericType<TType> Members

        public TType MakeGenericClosure(ITypeCollectionBase typeParameters)
        {
            if (typeParameters == null)
                throw new ArgumentNullException("typeParameters");
            if (!this.IsGenericDefinition)
                throw new System.InvalidOperationException();
            if (this.genericCache != null)
            {
                IGenericType r;
                if (this.genericCache.ContainsGenericType(typeParameters, out r))
                    return (TType)r;
            }
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
            TType result = this.OnMakeGenericClosure(typeParameters);
            try
            {
                this.VerifyTypeParameters(typeParameters);
            }
            catch (ArgumentException e)
            {
                result.Dispose();
                if (typeParameters is LockedTypeCollection)
                    this.genericCache.UnregisterGenericType((LockedTypeCollection)typeParameters);
                else
                    this.genericCache.UnregisterGenericType(new LockedTypeCollection(typeParameters));
                throw e;
            }
            return result;
        }

        public TType MakeGenericClosure(params IType[] typeParameters)
        {
            return MakeGenericClosure(typeParameters.ToCollection());
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
                if (this.genericParamsCache == null)
                    this.genericParamsCache = new LockedTypeCollection(this.GetGenericParameters());
                return this.genericParamsCache;
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
        /// contained within the <see cref="CompiledGenericTypeBase{TType}"/>.</returns>
        /// <remarks>Performs no type-parameter check.</remarks>
        protected abstract TType OnMakeGenericClosure(ITypeCollectionBase typeParameters);

        #region _IGenericTypeRegistrar Members

        public void RegisterGenericType(IGenericType targetType, LockedTypeCollection typeParameters)
        {
            if (this.disposing || this.disposeSynch == null)
                return;
            if (this.genericCache == null)
                this.genericCache = new GenericTypeCache();
            this.genericCache.RegisterGenericType(targetType, typeParameters);
        }

        public void UnregisterGenericType(LockedTypeCollection typeParameters)
        {
            if (this.genericCache == null || this.disposing || this.disposeSynch == null)
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
            if (this.genericCache == null || this.disposing || this.disposeSynch == null)
                return;
            this.genericCache.BeginExodus();
        }

        public void EndExodus()
        {
            if (this.genericCache == null || this.disposing || this.disposeSynch == null)
                return;
            //Console.Write("Beginning exodus for {0}: ", this.ToString());
            this.genericCache.EndExodus();
        }
    }
}
