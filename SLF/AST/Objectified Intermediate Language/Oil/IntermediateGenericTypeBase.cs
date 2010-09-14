using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Oil.Members;
using System.Linq;
using AllenCopeland.Abstraction.Slf.Abstract.Properties;
using AllenCopeland.Abstraction.Utilities.Collections;
using System.Globalization;
using AllenCopeland.Abstraction.Slf._Internal.GenericLayer;
using AllenCopeland.Abstraction.Slf._Internal.Ast;
using AllenCopeland.Abstraction.Utilities.Events;
using System.Threading.Tasks;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
{
    public abstract partial class IntermediateGenericTypeBase<TType, TIntermediateType> :
        IntermediateTypeBase<TType, TIntermediateType>,
        IIntermediateGenericType<TType, TIntermediateType>,
        _IGenericTypeRegistrar,
        _IIntermediateGenericType,
        IMassTargetHandler
        where TType :
            class,
            IGenericType<TType>
        where TIntermediateType :
            class,
            TType,
            IIntermediateGenericType<TType, TIntermediateType>
    {
        private GenericTypeCache<TType> genericCache = null;
        private GenericParameterCollection genericParameters;
        private IIntermediateGenericParameterDictionary<IGenericTypeParameter<TType>, IIntermediateGenericTypeParameter<TType, TIntermediateType>, TType, TIntermediateType> typeParameters;
        private bool disposing;
        private object disposeSynch = new object();
        /// <summary>
        /// Creates a new <see cref="IntermediateGenericTypeBase{TType, TIntermediateType}"/>
        /// with the <paramref name="name"/> and <paramref name="parent"/> 
        /// provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> in which the <see cref="IntermediateGenericTypeBase{TType, TIntermediateType}"/>
        /// is referred to by.</param>
        /// <param name="parent">The <see cref="IIntermediateTypeParent"/> which 
        /// contains the <see cref="IntermediateGenericTypeBase{TType, TIntermediateType}"/>.</param>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="name"/>, or <paramref name="parent"/>, is null.</exception>
        /// <exception cref="System.ArgumentException">thrown when <paramref name="name"/> is 
        /// <see cref="String.Empty"/></exception>
        public IntermediateGenericTypeBase(string name, IIntermediateTypeParent parent)
            : base(name, parent)
        {
        }

        /// <summary>
        /// Creates a new <see cref="IntermediateGenericTypeBase{TType, TIntermediateType}"/>
        /// with the <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="parent">The <see cref="IIntermediateTypeParent"/>
        /// which contians the <see cref="IntermediateGenericTypeBase{TType, TIntermediateType}"/>.</param>
        /// <exception cref="System.ArgumentNullException">thrown when
        /// <paramref name="parent"/> is null.</exception>
        internal IntermediateGenericTypeBase(IIntermediateTypeParent parent)
            : base(parent)
        {
        }

        public override bool IsGenericType
        {
            get
            {
                if (this.typeParameters == null || this.TypeParameters.Count == 0)
                    if (this.Parent is IGenericType)
                        return ((IGenericType)(this.Parent)).IsGenericType;
                    else
                        return false;
                else
                    return true;
            }
        }

        #region IIntermediateGenericParameterParent<IGenericTypeParameter<TType>,IIntermediateGenericTypeParameter<TType,TIntermediateType>,TType,TIntermediateType> Members
        /// <summary>
        /// Returns the type parameter dictionary which manages
        /// the current generic type's type-parameters.
        /// </summary>
        public IIntermediateGenericParameterDictionary<IGenericTypeParameter<TType>, IIntermediateGenericTypeParameter<TType, TIntermediateType>, TType, TIntermediateType> TypeParameters
        {
            get
            {
                if (this.typeParameters == null)
                    this.typeParameters = this.InitializeTypeParameters();
                return this.typeParameters;
            }
        }

        #endregion

        #region IGenericParamParent<IGenericTypeParameter<TType>,TType> Members

        IGenericParameterDictionary<IGenericTypeParameter<TType>, TType> IGenericParamParent<IGenericTypeParameter<TType>, TType>.TypeParameters
        {
            get { return this.TypeParameters; }
        }

        #endregion

        #region IGenericParamParent Members

        IGenericParameterDictionary IGenericParamParent.TypeParameters
        {
            get { return (IGenericParameterDictionary)this.TypeParameters; }
        }

        #endregion

        #region IIntermediateGenericParameterParent Members

        IIntermediateGenericParameterDictionary IIntermediateGenericParameterParent.TypeParameters
        {
            get { return (IIntermediateGenericParameterDictionary)this.TypeParameters; }
        }

        #endregion

        #region IGenericType<TType> Members

        public TType MakeGenericType(ITypeCollectionBase typeParameters)
        {
            if (this.genericCache != null)
            {
                TType r;
                if (this.genericCache.ContainsGenericType(typeParameters, out r))
                    return r;
            }
            return this.OnMakeGenericType(typeParameters);
        }

        public TType MakeGenericType(params IType[] typeParameters)
        {
            return this.MakeGenericType(typeParameters.ToCollection());
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
                if (this.genericParameters == null)
                    this.genericParameters = new GenericParameterCollection(this);
                return this.genericParameters;
            }
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
            if (!this.IsGenericTypeDefinition)
                throw new System.InvalidOperationException();
            if (typeParameters.Count != this.GenericParameters.Count)
                throw new ArgumentException("typeParameters");
            if (this.genericCache != null)
            {
                TType r = null;
                if (this.genericCache.ContainsGenericType(typeParameters, out r))
                    return r;
            }
            return this.OnMakeGenericType(typeParameters);
        }

        public void ReverifyTypeParameters()
        {
            throw new InvalidOperationException(Resources.TypeConstraintFailure_GenericTypeDefinition);
        }

        #endregion

        protected virtual TypeParameterDictionary InitializeTypeParameters()
        {
            return new TypeParameterDictionary(this);
        }

        /// <summary>
        /// Obtains the <typeparamref name="TType"/> relative to the
        /// <paramref name="typeParameters"/> provided.
        /// </summary>
        /// <param name="typeParameters">The <see cref="IType"/>
        /// series from which to create the generic type.</param>
        /// <returns>A <typeparamref name="TType"/>
        /// instance which replaces the type-parameters
        /// contained within the <see cref="IntermediateGenericType{TType, TIntermediateType}"/>.</returns>
        /// <remarks>Performs no type-parameter check.</remarks>
        protected abstract TType OnMakeGenericType(ITypeCollectionBase typeParameters);

        protected override string OnGetName()
        {
            return base.OnGetName();
        }

        /// <summary>
        /// Disposes the <see cref="IntermediateGenericTypeBase{TType, TIntermediateType}"/>
        /// and <paramref name="dispose"/>s the managed data as needed.
        /// </summary>
        /// <param name="dispose">whether to dispose all data, including the managed data (true), or just the
        /// unmanaged data (false).</param>
        protected override void Dispose(bool dispose)
        {
            if (this.disposeSynch == null)
                return;
            lock (this.disposeSynch)
            {
                if (this.disposing)
                    return;
                this.disposing = true;
            }
            try
            {
                if (genericCache != null)
                {
                    this.genericCache.Dispose();
                    this.genericCache = null;
                }
                if (this.genericParameters != null)
                {
                    this.genericParameters.Dispose();
                    this.genericParameters = null;
                }
                if (this.typeParameters != null)
                {
                    this.typeParameters.Dispose();
                    this.typeParameters = null;
                }
                lock (this.disposeSynch)
                    this.disposing = false;
                this.disposeSynch = null;
            }
            finally
            {
                base.Dispose(dispose);
            }
        }

        /// <summary>
        /// Determines whether the type-parameters of the generic type have
        /// been instantiated yet.
        /// </summary>
        protected bool TypeParametersInitialized
        {
            get
            {
                return this.typeParameters != null;
            }
        }

        #region _IGenericTypeRegistrar Members

        void _IGenericTypeRegistrar.RegisterGenericType(IGenericType targetType, LockedTypeCollection typeParameters)
        {
            if (this.genericCache == null)
                this.genericCache = new GenericTypeCache<TType>();
            this.genericCache.RegisterGenericType(targetType, typeParameters);
        }

        void _IGenericTypeRegistrar.UnregisterGenericType(LockedTypeCollection typeParameters)
        {
            if (this.genericCache == null || this.disposing)
                return;
            this.genericCache.UnregisterGenericType(typeParameters);
        }

        #endregion

        internal void OnRearranged(GenericParameterMovedEventArgs e)
        {
            OnRearrangedInner(e.From, e.To);
        }

        internal virtual void OnRearrangedInner(int from, int to)
        {
            if (this.genericCache == null)
                return;
            int gpC = this.GenericParameters.Count;
            int baseLine = (gpC - this.TypeParameters.Count);
            int realFrom = baseLine + from;
            int realTo = baseLine + to;
            foreach (var element in this.genericCache.Cast<_IGenericType>())
                element.PositionalShift(realFrom, realTo);
        }

        #region _IIntermediateGenericType Members

        void _IIntermediateGenericType.Rearranged(int from, int to)
        {
            this.OnRearrangedInner(from, to);
        }

        void _IIntermediateGenericType.ItemAdded(IGenericParameter parameter)
        {
            this.OnTypeParameterAdded(arg1:(IIntermediateGenericTypeParameter<TType, TIntermediateType>)parameter);
        }

        void _IIntermediateGenericType.ItemRemoved(IGenericParameter parameter)
        {
            this.OnTypeParameterRemoved(arg1: (IIntermediateGenericTypeParameter<TType, TIntermediateType>)parameter);
        }

        protected virtual void OnTypeParameterAdded(IIntermediateGenericTypeParameter<TType, TIntermediateType> arg1)
        {
            if (this._TypeParameterAdded != null)
                this._TypeParameterAdded(this, new EventArgsR1<IIntermediateGenericParameter>(arg1));
            if (this.TypeParameterAdded != null)
                this.TypeParameterAdded(this, new EventArgsR1<IIntermediateGenericTypeParameter<TType, TIntermediateType>>(arg1));
        }

        protected virtual void OnTypeParameterRemoved(IIntermediateGenericTypeParameter<TType, TIntermediateType> arg1)
        {
            if (this._TypeParameterRemoved != null)
                this._TypeParameterRemoved(this, new EventArgsR1<IIntermediateGenericParameter>(arg1));
            if (this.TypeParameterRemoved != null)
                this.TypeParameterRemoved(this, new EventArgsR1<IIntermediateGenericTypeParameter<TType, TIntermediateType>>(arg1));
        }
        #endregion

        #region IIntermediateGenericType<TType,TIntermediateType> Members

        public event EventHandler<EventArgsR1<IIntermediateGenericTypeParameter<TType, TIntermediateType>>> TypeParameterAdded;

        public event EventHandler<EventArgsR1<IIntermediateGenericTypeParameter<TType, TIntermediateType>>> TypeParameterRemoved;

        #endregion

        #region IIntermediateGenericType Members
        private event EventHandler<EventArgsR1<IIntermediateGenericParameter>> _TypeParameterAdded;
        private event EventHandler<EventArgsR1<IIntermediateGenericParameter>> _TypeParameterRemoved;
        event EventHandler<EventArgsR1<IIntermediateGenericParameter>> IIntermediateGenericParameterParent.TypeParameterAdded
        {
            add { _TypeParameterAdded += value; }
            remove { _TypeParameterAdded -= value; }
        }

        event EventHandler<EventArgsR1<IIntermediateGenericParameter>> IIntermediateGenericParameterParent.TypeParameterRemoved
        {
            add { _TypeParameterRemoved += value; }
            remove { _TypeParameterRemoved -= value; }
        }

        #endregion

        #region IMassTargetHandler Members

        public void BeginExodus()
        {
            if (this.genericCache == null || this.disposing || this.disposeSynch == null)
                return;
            this.genericCache.BeginExodus();
        }

        public void EndExodus()
        {
            this.genericCache.EndExodus();
        }

        #endregion
    }
}
