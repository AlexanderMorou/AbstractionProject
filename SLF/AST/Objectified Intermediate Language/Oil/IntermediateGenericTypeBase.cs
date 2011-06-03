using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AllenCopeland.Abstraction.Slf._Internal.Ast;
using AllenCopeland.Abstraction.Slf._Internal.GenericLayer;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Properties;
using AllenCopeland.Abstraction.Slf.Oil.Members;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Utilities.Events;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
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
        private GenericTypeCache genericCache = null;
        private GenericParameterCollection genericParameters;
        private IIntermediateGenericParameterDictionary<IGenericTypeParameter<TType>, IIntermediateGenericTypeParameter<TType, TIntermediateType>, TType, TIntermediateType> typeParameters;
        private byte disposeState;
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

        /// <summary>
        /// Returns whether the current type is a generic type with <see cref="GenericParameters"/>.
        /// </summary>
        public override bool IsGenericConstruct
        {
            get
            {
                if (this.typeParameters == null || this.TypeParameters.Count == 0)
                    if (this.Parent is IGenericType)
                        return ((IGenericType)(this.Parent)).IsGenericConstruct;
                    else
                        return false;
                else
                    return true;
            }
        }

        #region IIntermediateGenericParameterParent<IGenericTypeParameter<TType>,IIntermediateGenericTypeParameter<TType,TIntermediateType>,TType,TIntermediateType> Members

        /// <summary>
        /// Occurs when a type-parameter is inserted into the 
        /// <see cref="IntermediateGenericTypeBase{TType, TIntermediateType}"/>.
        /// </summary>
        public event EventHandler<EventArgsR1<IIntermediateGenericTypeParameter<TType, TIntermediateType>>> TypeParameterAdded;

        /// <summary>
        /// Occurs when a type-parameter is removed from the 
        /// <see cref="IntermediateGenericTypeBase{TType, TIntermediateType}"/>.
        /// </summary>
        public event EventHandler<EventArgsR1<IIntermediateGenericTypeParameter<TType, TIntermediateType>>> TypeParameterRemoved;

        /// <summary>
        /// Returns the type parameter dictionary which manages
        /// the current generic type's type-parameters.
        /// </summary>
        public IIntermediateGenericParameterDictionary<IGenericTypeParameter<TType>, IIntermediateGenericTypeParameter<TType, TIntermediateType>, TType, TIntermediateType> TypeParameters
        {
            get
            {
                if (this.typeParameters == null)
                {
                    if (this.IsDisposed)
                        throw new InvalidOperationException(Utilities.Properties.Resources.ObjectStateThrowMessage);
                    this.typeParameters = this.InitializeTypeParameters();
                }
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

        /// <summary>
        /// Returns a <typeparamref name="TType"/> instance that is the 
        /// closed generic form of the current <see cref="IntermediateGenericTypeBase{TType, TIntermediateType}"/>
        /// using the <paramref name="typeParameters"/> provided.
        /// </summary>
        /// <param name="typeParameters">The <see cref="ILockedTypeCollection"/> 
        /// used to fill in the type-parameters.</param>
        /// <returns>A new closed <typeparamref name="TType"/> instance with
        /// the <paramref name="typeParameters"/> provided.</returns>
        /// <exception cref="System.InvalidOperationException">
        /// The current <see cref="IntermediateGenericTypeBase{TType, TIntermediateType}"/>'s 
        /// <seealso cref="IsGenericDefinition"/>
        /// is false.</exception>
        public TType MakeGenericClosure(ITypeCollectionBase typeParameters)
        {
            if (this.genericCache != null)
            {
                IGenericType r;
                if (this.genericCache.ContainsGenericType(typeParameters, out r))
                    return (TType)r;
            }
            return this.OnMakeGenericClosure(typeParameters);
        }

        /// <summary>
        /// Returns a <typeparamref name="TType"/> instance that is the 
        /// closed generic form of the current <see cref="IntermediateGenericTypeBase{TType, TIntermediateType}"/> 
        /// using the <paramref name="typeParameters"/> provided.
        /// </summary>
        /// <param name="typeParameters">The <see cref="IType"/> 
        /// collection used to fill in the type-parameters.</param>
        /// <returns>A new closed <typeparamref name="TType"/> instance with 
        /// the <paramref name="typeParameters"/> provided.</returns>
        /// <exception cref="System.InvalidOperationException">
        /// The current <see cref="IntermediateGenericTypeBase{TType, TIntermediateType}"/>'s 
        /// <seealso cref="IsGenericDefinition"/>
        /// is false.</exception>
        public TType MakeGenericClosure(params IType[] typeParameters)
        {
            return this.MakeGenericClosure(typeParameters.ToCollection());
        }

        #endregion

        #region IGenericType Members

        /// <summary>
        /// Returns whether the current type is an open generic 
        /// type that can be made into other closed generic type 
        /// instances.
        /// </summary>
        public bool IsGenericDefinition
        {
            get { return this.IsGenericConstruct; }
        }

        /// <summary>
        /// Returns whether the <see cref="IntermediateGenericTypeBase{TType, TIntermediateType}"/> contains
        /// generic parameters.
        /// </summary>
        public bool ContainsGenericParameters
        {
            get { return ((IType)this).ContainsGenericParameters(); }
        }

        /// <summary>
        /// Returns a <see cref="ITypeCollection"/> which relates 
        /// to the current generic type's type-parameters.
        /// </summary>
        /// <remarks>Differs from <see cref="TypeParameters"/>
        /// by containing a full series of type-parameters, including those
        /// of the parent which defined the <see cref="IntermediateGenericTypeBase{TType, TIntermediateType}"/>.</remarks>
        public ILockedTypeCollection GenericParameters
        {
            get
            {
                if (this.genericParameters == null)
                {
                    if (this.IsDisposed)
                        throw new InvalidOperationException(Utilities.Properties.Resources.ObjectStateThrowMessage);
                    this.genericParameters = new GenericParameterCollection(this);
                }
                return this.genericParameters;
            }
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
        /// contained within the <see cref="IntermediateGenericTypeBase{TType, TIntermediateType}"/>.</returns>
        /// <remarks>Performs no type-parameter check.</remarks>
        protected abstract TType OnMakeGenericClosure(ITypeCollectionBase typeParameters);

        /// <summary>
        /// Disposes the <see cref="IntermediateGenericTypeBase{TType, TIntermediateType}"/>
        /// and <paramref name="dispose"/>s the managed data as needed.
        /// </summary>
        /// <param name="dispose">whether to dispose all data, including the managed data (true), or just the
        /// unmanaged data (false).</param>
        protected override void Dispose(bool dispose)
        {
            const int DISP_STATE_NONE = 0;
            const int DISP_STATE_DISPOSING = 1;
            const int DISP_STATE_DISPOSED = 2;
            lock (this.disposeSynch)
            {
                if (this.disposeState != DISP_STATE_NONE)
                    return;
                this.disposeState = DISP_STATE_DISPOSING;
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
                    this.disposeState = DISP_STATE_DISPOSED;
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
                this.genericCache = new GenericTypeCache();
            this.genericCache.RegisterGenericType(targetType, typeParameters);
        }

        void _IGenericTypeRegistrar.UnregisterGenericType(LockedTypeCollection typeParameters)
        {
            const int DISP_STATE_NONE = 0; //Used inside method to avoid emitting fields.
            lock (this.disposeSynch)
                if (this.genericCache == null || this.disposeState != DISP_STATE_NONE)
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
            int realFrom = baseLine + from,
                realTo   = baseLine + to;
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
            var _typeParameterAdded = this._TypeParameterAdded;
            if (_typeParameterAdded != null)
                _typeParameterAdded(this, new EventArgsR1<IIntermediateGenericParameter>(arg1));
            var typeParameterAdded = this.TypeParameterAdded;
            if (typeParameterAdded != null)
                typeParameterAdded(this, new EventArgsR1<IIntermediateGenericTypeParameter<TType, TIntermediateType>>(arg1));
        }

        protected virtual void OnTypeParameterRemoved(IIntermediateGenericTypeParameter<TType, TIntermediateType> arg1)
        {
            var _typeParameterRemoved = this._TypeParameterRemoved;
            if (_typeParameterRemoved != null)
                _typeParameterRemoved(this, new EventArgsR1<IIntermediateGenericParameter>(arg1));
            var typeParameterRemoved = this.TypeParameterRemoved;
            if (typeParameterRemoved != null)
                typeParameterRemoved(this, new EventArgsR1<IIntermediateGenericTypeParameter<TType, TIntermediateType>>(arg1));
        }
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

        /// <summary>
        /// Begins an exodus upon the <see cref="IMassTargetHandler"/>.
        /// </summary>
        public void BeginExodus()
        {
            const int DISP_STATE_NONE = 0; //Used inside method to avoid emitting fields.
            lock(disposeSynch)
                if (this.genericCache == null || this.disposeState != DISP_STATE_NONE)
                    return;
            this.genericCache.BeginExodus();
        }

        /// <summary>
        /// Ends an exodus upon the <see cref="IMassTargetHandler"/>.
        /// </summary>
        public void EndExodus()
        {
            this.genericCache.EndExodus();
        }

        #endregion


    }
}
