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
using System.ComponentModel;
using AllenCopeland.Abstraction.Slf.Cli;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public abstract partial class IntermediateGenericTypeBase<TTypeIdentifier, TType, TIntermediateType> :
        IntermediateTypeBase<TTypeIdentifier, TType, TIntermediateType>,
        IIntermediateGenericType<TTypeIdentifier, TType, TIntermediateType>,
        _IGenericClosureRegistrar,
        _IIntermediateGenericType<TTypeIdentifier>,
        IMassTargetHandler
        where TTypeIdentifier :
            IGenericTypeUniqueIdentifier<TTypeIdentifier>,
            IGeneralDeclarationUniqueIdentifier
        where TType :
            class,
            IGenericType<TTypeIdentifier, TType>
        where TIntermediateType :
            class,
            IIntermediateGenericType<TTypeIdentifier, TType, TIntermediateType>,
            TType
    {
        private GenericTypeCache genericCache = null;
        private GenericParameterCollection genericParameters;
        private IIntermediateGenericParameterDictionary<IGenericTypeParameter<TTypeIdentifier, TType>, IIntermediateGenericTypeParameter<TTypeIdentifier, TType, TIntermediateType>, TType, TIntermediateType> typeParameters;
        private byte disposeState;

        /// <summary>
        /// Creates a new <see cref="IntermediateGenericTypeBase{TTypeIdentifier, TType, TIntermediateType}"/>
        /// with the <paramref name="name"/> and <paramref name="parent"/> 
        /// provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> in which the <see cref="IntermediateGenericTypeBase{TTypeIdentifier, TType, TIntermediateType}"/>
        /// is referred to by.</param>
        /// <param name="parent">The <see cref="IIntermediateTypeParent"/> which 
        /// contains the <see cref="IntermediateGenericTypeBase{TTypeIdentifier, TType, TIntermediateType}"/>.</param>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="name"/>, or <paramref name="parent"/>, is null.</exception>
        /// <exception cref="System.ArgumentException">thrown when <paramref name="name"/> is 
        /// <see cref="String.Empty"/></exception>
        public IntermediateGenericTypeBase(string name, IIntermediateTypeParent parent)
            : base(name, parent)
        {
        }

        /// <summary>
        /// Creates a new <see cref="IntermediateGenericTypeBase{TTypeIdentifier, TType, TIntermediateType}"/>
        /// with the <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="parent">The <see cref="IIntermediateTypeParent"/>
        /// which contians the <see cref="IntermediateGenericTypeBase{TTypeIdentifier, TType, TIntermediateType}"/>.</param>
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
                lock (this.SyncObject)
                    if (this.typeParameters == null || this.TypeParameters.Count == 0)
                        if (this.Parent is IIntermediateDeclaration)
                            return ((IIntermediateDeclaration)(this.Parent)).IsDeclarationGenericConstruct();
                        else
                            return false;
                    else
                        return true;
            }
        }

        #region IIntermediateGenericParameterParent<IGenericTypeParameter<TTypeIdentifier, TType>,IIntermediateGenericTypeParameter<TType,TIntermediateType>,TType,TIntermediateType> Members

        /// <summary>
        /// Occurs when a type-parameter is inserted into the 
        /// <see cref="IntermediateGenericTypeBase{TTypeIdentifier, TType, TIntermediateType}"/>.
        /// </summary>
        public event EventHandler<EventArgsR1<IIntermediateGenericTypeParameter<TTypeIdentifier, TType, TIntermediateType>>> TypeParameterAdded;

        /// <summary>
        /// Occurs when a type-parameter is removed from the 
        /// <see cref="IntermediateGenericTypeBase{TTypeIdentifier, TType, TIntermediateType}"/>.
        /// </summary>
        public event EventHandler<EventArgsR1<IIntermediateGenericTypeParameter<TTypeIdentifier, TType, TIntermediateType>>> TypeParameterRemoved;

        /// <summary>
        /// Returns the type parameter dictionary which manages
        /// the current generic type's type-parameters.
        /// </summary>
        public IIntermediateGenericParameterDictionary<IGenericTypeParameter<TTypeIdentifier, TType>, IIntermediateGenericTypeParameter<TTypeIdentifier, TType, TIntermediateType>, TType, TIntermediateType> TypeParameters
        {
            get
            {
                lock (this.SyncObject)
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
        }

        #endregion

        #region IGenericParamParent<IGenericTypeParameter<TTypeIdentifier, TType>,TType> Members

        IGenericParameterDictionary<IGenericTypeParameter<TTypeIdentifier, TType>, TType> IGenericParamParent<IGenericTypeParameter<TTypeIdentifier, TType>, TType>.TypeParameters
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

        #region IGenericType<TTypeIdentifier, TType> Members

        /// <summary>
        /// Returns a <typeparamref name="TType"/> instance that is the 
        /// closed generic form of the current <see cref="IntermediateGenericTypeBase{TTypeIdentifier, TType, TIntermediateType}"/>
        /// using the <paramref name="typeParameters"/> provided.
        /// </summary>
        /// <param name="typeParameters">The <see cref="ILockedTypeCollection"/> 
        /// used to fill in the type-parameters.</param>
        /// <returns>A new closed <typeparamref name="TType"/> instance with
        /// the <paramref name="typeParameters"/> provided.</returns>
        /// <exception cref="System.InvalidOperationException">
        /// The current <see cref="IntermediateGenericTypeBase{TTypeIdentifier, TType, TIntermediateType}"/>'s 
        /// <seealso cref="IsGenericDefinition"/>
        /// is false.</exception>
        public TType MakeGenericClosure(ITypeCollectionBase typeParameters)
        {
            LockedTypeCollection lockedTypeParameters = typeParameters.ToLockedCollection();
            IGenericType genericResult;
            lock (this.SyncObject)
                if (this.genericCache != null && genericCache.TryObtainGenericClosure(lockedTypeParameters, out genericResult))
                    return (TType)genericResult;
            return this.OnMakeGenericClosure(lockedTypeParameters);
        }

        /// <summary>
        /// Returns a <typeparamref name="TType"/> instance that is the 
        /// closed generic form of the current <see cref="IntermediateGenericTypeBase{TTypeIdentifier, TType, TIntermediateType}"/> 
        /// using the <paramref name="typeParameters"/> provided.
        /// </summary>
        /// <param name="typeParameters">The <see cref="IType"/> 
        /// collection used to fill in the type-parameters.</param>
        /// <returns>A new closed <typeparamref name="TType"/> instance with 
        /// the <paramref name="typeParameters"/> provided.</returns>
        /// <exception cref="System.InvalidOperationException">
        /// The current <see cref="IntermediateGenericTypeBase{TTypeIdentifier, TType, TIntermediateType}"/>'s 
        /// <seealso cref="IsGenericDefinition"/>
        /// is false.</exception>
        public TType MakeGenericClosure(params IType[] typeParameters)
        {
            return this.MakeGenericClosure(typeParameters.ToLockedCollection());
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
        /// Returns whether the <see cref="IntermediateGenericTypeBase{TTypeIdentifier, TType, TIntermediateType}"/> contains
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
        /// of the parent which defined the <see cref="IntermediateGenericTypeBase{TTypeIdentifier, TType, TIntermediateType}"/>.</remarks>
        public ILockedTypeCollection GenericParameters
        {
            get
            {
                lock (this.SyncObject)
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
        /// contained within the <see cref="IntermediateGenericTypeBase{TTypeIdentifier, TType, TIntermediateType}"/>.</returns>
        /// <remarks>Performs no type-parameter check.</remarks>
        protected abstract TType OnMakeGenericClosure(ITypeCollectionBase typeParameters);

        /// <summary>
        /// Disposes the <see cref="IntermediateGenericTypeBase{TTypeIdentifier, TType, TIntermediateType}"/>.
        /// </summary>
        public override void Dispose()
        {
            const int DISP_STATE_NONE = 0;
            const int DISP_STATE_DISPOSING = 1;
            const int DISP_STATE_DISPOSED = 2;
            lock (this.SyncObject)
            {
                if (this.disposeState != DISP_STATE_NONE)
                    return;
                this.disposeState = DISP_STATE_DISPOSING;
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
                    this.disposeState = DISP_STATE_DISPOSED;
                }
                finally
                {
                    base.Dispose();
                }
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
                lock (this.SyncObject)
                    return this.typeParameters != null;
            }
        }

        #region _IGenericClosureRegistrar Members

        bool _IGenericClosureRegistrar.ContainsGenericClosure(ILockedTypeCollection typeParameters)
        {
            lock (this.SyncObject)
            {
                if (this.genericCache == null)
                    return false;
                return this.genericCache.ContainsGenericClosure(typeParameters);
            }
        }

        IGenericType _IGenericClosureRegistrar.ObtainGenericClosure(ILockedTypeCollection typeParameters)
        {
            lock (this.SyncObject)
            {
                if (this.genericCache == null)
                    return null;
                return this.genericCache.ObtainGenericClosure(typeParameters);
            }
        }


        bool _IGenericClosureRegistrar.TryObtainGenericClosure(ILockedTypeCollection typeParameters, out IGenericType genericClosure)
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
        void _IGenericClosureRegistrar.RegisterGenericClosure(IGenericType targetType, ILockedTypeCollection typeParameters)
        {
            lock (this.SyncObject)
            {
                if (this.genericCache == null)
                    this.genericCache = new GenericTypeCache();
                this.genericCache.RegisterGenericType(targetType, typeParameters);
            }
        }

        void _IGenericClosureRegistrar.UnregisterGenericClosure(ILockedTypeCollection typeParameters)
        {
            lock (this.SyncObject)
            {
                if (this.genericCache == null)
                    return;
                this.genericCache.UnregisterGenericType(typeParameters);
            }
        }

        #endregion

        internal void OnRearranged(GenericParameterMovedEventArgs e)
        {
            OnRearrangedInner(e.From, e.To);
        }

        internal virtual void OnRearrangedInner(int from, int to)
        {
            lock (this.SyncObject)
            {
                if (this.genericCache == null)
                    return;
                int gpC = this.GenericParameters.Count;
                int baseLine = (gpC - this.TypeParameters.Count);
                int realFrom = baseLine + from,
                    realTo = baseLine + to;
                foreach (var element in this.genericCache.Cast<_IGenericType>())
                    element.PositionalShift(realFrom, realTo);
            }
        }

        #region _IIntermediateGenericType Members

        void _IIntermediateGenericType<TTypeIdentifier>.CardinalityChanged(TTypeIdentifier oldIdentifier)
        {
            this.OnIdentifierChanged(oldIdentifier, DeclarationChangeCause.IdentityCardinality);
        }
        void _IIntermediateGenericType<TTypeIdentifier>.Rearranged(int from, int to)
        {
            this.OnRearrangedInner(from, to);
        }

        void _IIntermediateGenericType<TTypeIdentifier>.ItemAdded(IGenericParameter parameter)
        {
            this.OnTypeParameterAdded(arg1: (IIntermediateGenericTypeParameter<TTypeIdentifier, TType, TIntermediateType>)parameter);
        }

        void _IIntermediateGenericType<TTypeIdentifier>.ItemRemoved(IGenericParameter parameter)
        {
            this.OnTypeParameterRemoved(arg1: (IIntermediateGenericTypeParameter<TTypeIdentifier, TType, TIntermediateType>)parameter);
        }

        protected virtual void OnTypeParameterAdded(IIntermediateGenericTypeParameter<TTypeIdentifier, TType, TIntermediateType> arg1)
        {
            var _typeParameterAdded = this._TypeParameterAdded;
            if (_typeParameterAdded != null)
                _typeParameterAdded(this, new EventArgsR1<IIntermediateGenericParameter>(arg1));
            var typeParameterAdded = this.TypeParameterAdded;
            if (typeParameterAdded != null)
                typeParameterAdded(this, new EventArgsR1<IIntermediateGenericTypeParameter<TTypeIdentifier, TType, TIntermediateType>>(arg1));
        }

        protected virtual void OnTypeParameterRemoved(IIntermediateGenericTypeParameter<TTypeIdentifier, TType, TIntermediateType> arg1)
        {
            var _typeParameterRemoved = this._TypeParameterRemoved;
            if (_typeParameterRemoved != null)
                _typeParameterRemoved(this, new EventArgsR1<IIntermediateGenericParameter>(arg1));
            var typeParameterRemoved = this.TypeParameterRemoved;
            if (typeParameterRemoved != null)
                typeParameterRemoved(this, new EventArgsR1<IIntermediateGenericTypeParameter<TTypeIdentifier, TType, TIntermediateType>>(arg1));
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
            lock (SyncObject)
            {
                if (this.genericCache == null)
                    return;
                this.genericCache.BeginExodus();
            }
        }

        /// <summary>
        /// Ends an exodus upon the <see cref="IMassTargetHandler"/>.
        /// </summary>
        public void EndExodus()
        {
            lock (SyncObject)
            {
                if (this.genericCache == null)
                    return;
                this.genericCache.EndExodus();
            }
        }

        #endregion

    }
}
