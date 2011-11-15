using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Utilities.Events;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Members
{
    /// <summary>
    /// Provides an implementation of a parameter parent as a member.
    /// </summary>
    /// <typeparam name="TParentIdentifier">The kind of identifier used to differentiate
    /// the <typeparamref name="TIntermediateParent"/> from its siblings.</typeparam>
    /// <typeparam name="TParent">The type which parents the <typeparamref name="TParameter"/> 
    /// in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateParent">The type which parents the <typeparamref name="TIntermediateParameter"/>
    /// in the intermediate abstract syntax tree.</typeparam>
    /// <typeparam name="TParameter">The type of parameter in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateParameter">The type of parameter in the intermediate
    /// abstract syntax tree.</typeparam>
    /// <typeparam name="TGrandParent">The type which contains the parameter parent in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateGrandParent">The type which contains the
    /// parameter parent in the intermediate abstract syntax tree.</typeparam>
    public abstract class IntermediateParameterParentMemberBase<TParentIdentifier, TParent, TIntermediateParent, TParameter, TIntermediateParameter, TGrandParent, TIntermediateGrandParent> :
        IntermediateMemberBase<TParentIdentifier, TGrandParent, TIntermediateGrandParent>,
        IIntermediateParameterParent<TParent, TIntermediateParent, TParameter, TIntermediateParameter>
        where TParentIdentifier :
            IMemberUniqueIdentifier,
            IGeneralMemberUniqueIdentifier
        where TParent :
            IParameterParent<TParent, TParameter>,
            IMember<TParentIdentifier, TGrandParent>
        where TIntermediateParent :
            IIntermediateParameterParent<TParent, TIntermediateParent, TParameter, TIntermediateParameter>,
            IIntermediateMember<TParentIdentifier, TGrandParent, TIntermediateGrandParent>,
            TParent
        where TParameter :
            IParameterMember<TParent>
        where TIntermediateParameter :
            IIntermediateParameterMember<TParent, TIntermediateParent>,
            TParameter
        where TGrandParent :
            IMemberParent
        where TIntermediateGrandParent :
            IIntermediateMemberParent,
            TGrandParent
    {
        private IntermediateParameterMemberDictionary<TParent, TIntermediateParent, TParameter, TIntermediateParameter> parameters;

        /// <summary>
        /// Creates a new <see cref="IntermediateParameterParentMemberBase{TParentIdentifier, TParent, TIntermediateParent, TParameter, TIntermediateParameter, TGrandParent, TIntermediateGrandParent}"/>
        /// with the <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="parent">The <typeparamref name="TIntermediateGrandParent"/>
        /// which contains the <see cref="IntermediateParameterParentMemberBase{TParentIdentifier, TParent, TIntermediateParent, TParameter, TIntermediateParameter, TGrandParent, TIntermediateGrandParent}"/>.</param>
        public IntermediateParameterParentMemberBase(TIntermediateGrandParent parent)
            : base(parent)
        {
        }

        /// <summary>
        /// Creates a new <see cref="IntermediateParameterParentMemberBase{TParentIdentifier, TParent, TIntermediateParent, TParameter, TIntermediateParameter, TGrandParent, TIntermediateGrandParent}"/>
        /// with the <paramref name="name"/> and <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> representing the unique identifier of the 
        /// <see cref="IntermediateParameterParentMemberBase{TParentIdentifier, TParent, TIntermediateParent, TParameter, TIntermediateParameter, TGrandParent, TIntermediateGrandParent}"/>.</param>
        /// <param name="parent">The <typeparamref name="TIntermediateGrandParent"/>
        /// which contains the <see cref="IntermediateParameterParentMemberBase{TParentIdentifier, TParent, TIntermediateParent, TParameter, TIntermediateParameter, TGrandParent, TIntermediateGrandParent}"/>.</param>
        public IntermediateParameterParentMemberBase(string name, TIntermediateGrandParent parent)
            : base(name, parent)
        {
        }

        /// <summary>
        /// Returns the dictionary of <typeparamref name="TIntermediateParameter"/> instances for the current <see cref="IntermediateParameterParentMemberBase{TParentIdentifier, TParent, TIntermediateParent, TParameter, TIntermediateParameter, TGrandParent, TIntermediateGrandParent}"/>.
        /// </summary>
        public IntermediateParameterMemberDictionary<TParent, TIntermediateParent, TParameter, TIntermediateParameter> Parameters
        {
            get
            {
                this.CheckParameters();
                return this.parameters;
            }
        }

        /// <summary>
        /// Returns whether the <see cref="Parameters"/> have been
        /// initialized.
        /// </summary>
        protected bool AreParametersInitialized { get { return this.parameters != null; } }

        private void CheckParameters()
        {
            if (this.parameters == null)
            {
                this.parameters = this.InitializeParameters();
                this.parameters.ItemAdded += new EventHandler<EventArgsR1<TIntermediateParameter>>(parameters_ItemAdded);
                this.parameters.ItemRemoved += new EventHandler<EventArgsR1<TIntermediateParameter>>(parameters_ItemRemoved);
            }
        }

        private EventHandler<EventArgsR1R2<IType, IType>> _parameter_ParameterTypeChangedPtr;
        private EventHandler<EventArgsR1R2<IType, IType>> parameter_ParameterTypeChangedPtr
        {
            get
            {
                if (!this.IsDisposed && this._parameter_ParameterTypeChangedPtr == null)
                    this._parameter_ParameterTypeChangedPtr = this.parameter_ParameterTypeChanged;
                return this._parameter_ParameterTypeChangedPtr;
            }
        }

        void parameters_ItemRemoved(object sender, EventArgsR1<TIntermediateParameter> e)
        {
            this.OnParameterRemoved(e);
            if (e != null && e.Arg1 != null)
            {
                var ptr = parameter_ParameterTypeChangedPtr;
                if (ptr != null)
                {
                    var parameter = e.Arg1;
                    parameter.ParameterTypeChanged -= parameter_ParameterTypeChangedPtr;
                }
            }
        }

        protected virtual void OnParameterAdded(EventArgsR1<TIntermediateParameter> e)
        {
            var _parameterAdded = this._ParameterAdded;
            if (_parameterAdded != null)
                _parameterAdded(this, new EventArgsR1<IIntermediateParameterMember>(e.Arg1));
            var parameterAdded = this.ParameterAdded;
            if (parameterAdded != null)
                parameterAdded(this, new EventArgsR1<TIntermediateParameter>(e.Arg1));
            this.OnIdentifierChanged(this.UniqueIdentifier, DeclarationChangeCause.IdentityCardinality);
        }

        void parameters_ItemAdded(object sender, EventArgsR1<TIntermediateParameter> e)
        {
            if (e != null && e.Arg1 != null)
            {
                var ptr = parameter_ParameterTypeChangedPtr;
                if (ptr != null)
                {
                    var parameter = e.Arg1;
                    parameter.ParameterTypeChanged += parameter_ParameterTypeChangedPtr;
                }
            }
            this.OnParameterAdded(e);
        }

        void parameter_ParameterTypeChanged(object sender, EventArgsR1R2<IType, IType> e)
        {
            if ((e == null || e.Arg1 == null || e.Arg2 == null) && sender is TIntermediateParameter)
                this.OnIdentifierChanged(this.UniqueIdentifier, DeclarationChangeCause.Signature);
        }

        protected virtual void OnParameterRemoved(EventArgsR1<TIntermediateParameter> e)
        {
            var _parameterRemoved = this._ParameterRemoved;
            if (_parameterRemoved != null)
                _parameterRemoved(this, new EventArgsR1<IIntermediateParameterMember>(e.Arg1));
            var parameterRemoved = this.ParameterRemoved;
            if (parameterRemoved != null)
                parameterRemoved(this, new EventArgsR1<TIntermediateParameter>(e.Arg1));
            this.OnIdentifierChanged(this.UniqueIdentifier, DeclarationChangeCause.IdentityCardinality);
        }

        /// <summary>
        /// Initializes the <see cref="Parameters"/> property.
        /// </summary>
        /// <returns>An instance of an implementation of <see cref="IntermediateParameterMemberDictionary{TParent, TIntermediateParent, TParameter, TIntermediateParameter}"/>.</returns>
        protected abstract IntermediateParameterMemberDictionary<TParent, TIntermediateParent, TParameter, TIntermediateParameter> InitializeParameters();

        #region IIntermediateParameterParent<TParent,TIntermediateParent,TParameter,TIntermediateParameter> Members

        IIntermediateParameterMemberDictionary<TParent, TIntermediateParent, TParameter, TIntermediateParameter> IIntermediateParameterParent<TParent,TIntermediateParent,TParameter,TIntermediateParameter>.Parameters
        {
            get { return this.Parameters; }
        }

        #endregion

        #region IIntermediateParameterParent Members

        IIntermediateParameterMemberDictionary IIntermediateParameterParent.Parameters
        {
            get { return this.Parameters; }
        }

        /// <summary>
        /// Returns whether the last element in <see cref="Parameters"/> is a parameter
        /// array.
        /// </summary>
        public bool LastIsParams
        {
            get
            {
                var lastParam = this.parameters.Values.LastOrDefault();
                if (lastParam == null)
                    return false;
                return lastParam.CustomAttributes.Contains(CommonTypeRefs.ParameterArrayAttribute);
            }
        }

        #endregion

        #region IParameterParent Members

        IParameterMemberDictionary IParameterParent.Parameters
        {
            get { return this.Parameters; }
        }

        #endregion

        #region IParameterParent<TParent,TParameter> Members

        IParameterMemberDictionary<TParent, TParameter> IParameterParent<TParent, TParameter>.Parameters
        {
            get { return this.Parameters; }
        }

        #endregion

        /// <summary>
        /// Disposes the <see cref="IntermediateParameterParentMemberBase{TParentIdentifier, TParent, TIntermediateParent, TParameter, TIntermediateParameter, TGrandParent, TIntermediateGrandParent}"/>
        /// </summary>
        /// <param name="disposing">whether to dispose the managed 
        /// resources as well as the unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing)
                {
                    if (this._parameter_ParameterTypeChangedPtr != null)
                        this._parameter_ParameterTypeChangedPtr = null;
                    if (this.parameters != null)
                    {
                        this.parameters.ItemAdded -= new EventHandler<EventArgsR1<TIntermediateParameter>>(parameters_ItemAdded);
                        this.parameters.ItemRemoved -= new EventHandler<EventArgsR1<TIntermediateParameter>>(parameters_ItemRemoved);
                        this.parameters.Dispose();
                        this.parameters = null;
                    }
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }


        #region IIntermediateParameterParent<TParent,TIntermediateParent,TParameter,TIntermediateParameter> Members

        /// <summary>
        /// Occurs when a parameter is added.
        /// </summary>
        public event EventHandler<EventArgsR1<TIntermediateParameter>> ParameterAdded;

        /// <summary>
        /// Occurs when a parameter is removed.
        /// </summary>
        public event EventHandler<EventArgsR1<TIntermediateParameter>> ParameterRemoved;

        #endregion

        #region IIntermediateParameterParent Members
        private EventHandler<EventArgsR1<IIntermediateParameterMember>> _ParameterAdded;
        private EventHandler<EventArgsR1<IIntermediateParameterMember>> _ParameterRemoved;

        event EventHandler<EventArgsR1<IIntermediateParameterMember>> IIntermediateParameterParent.ParameterAdded
        {
            add { this._ParameterAdded += value; }
            remove { this._ParameterAdded -= value; }
        }

        event EventHandler<EventArgsR1<IIntermediateParameterMember>> IIntermediateParameterParent.ParameterRemoved
        {
            add { this._ParameterRemoved += value; }
            remove { this._ParameterRemoved -= value; }
        }

        #endregion
    }
}
