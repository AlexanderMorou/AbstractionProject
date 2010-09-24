using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Utilities.Events;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Members
{
    /// <summary>
    /// Provides an implementation of a parameter parent as a member.
    /// </summary>
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
    public abstract class IntermediateParameterParentMemberBase<TParent, TIntermediateParent, TParameter, TIntermediateParameter, TGrandParent, TIntermediateGrandParent> :
        IntermediateMemberBase<TGrandParent, TIntermediateGrandParent>,
        IIntermediateParameterParent<TParent, TIntermediateParent, TParameter, TIntermediateParameter>
        where TParent :
            IParameterParent<TParent, TParameter>,
            IMember<TGrandParent>
        where TIntermediateParent :
            IIntermediateParameterParent<TParent, TIntermediateParent, TParameter, TIntermediateParameter>,
            IIntermediateMember<TGrandParent, TIntermediateGrandParent>,
            TParent
        where TParameter :
            IParameterMember<TParent>
        where TIntermediateParameter :
            TParameter,
            IIntermediateParameterMember<TParent, TIntermediateParent>
        where TGrandParent :
            IMemberParent
        where TIntermediateGrandParent :
            IIntermediateMemberParent,
            TGrandParent
    {
        private IntermediateParameterMemberDictionary<TParent, TIntermediateParent, TParameter, TIntermediateParameter> parameters;

        /// <summary>
        /// Creates a new <see cref="IntermediateParameterParentMemberBase{TParent, TIntermediateParent, TParameter, TIntermediateParameter, TGrandParent, TIntermediateGrandParent}"/>
        /// with the <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="parent">The <typeparamref name="TIntermediateGrandParent"/>
        /// which contains the <see cref="IntermediateParameterParentMemberBase{TParent, TIntermediateParent, TParameter, TIntermediateParameter, TGrandParent, TIntermediateGrandParent}"/>.</param>
        public IntermediateParameterParentMemberBase(TIntermediateGrandParent parent)
            : base(parent)
        {
        }

        /// <summary>
        /// Creates a new <see cref="IntermediateParameterParentMemberBase{TParent, TIntermediateParent, TParameter, TIntermediateParameter, TGrandParent, TIntermediateGrandParent}"/>
        /// with the <paramref name="name"/> and <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> representing the unique identifier of the 
        /// <see cref="IntermediateParameterParentMemberBase{TParent, TIntermediateParent, TParameter, TIntermediateParameter, TGrandParent, TIntermediateGrandParent}"/>.</param>
        /// <param name="parent">The <typeparamref name="TIntermediateGrandParent"/>
        /// which contains the <see cref="IntermediateParameterParentMemberBase{TParent, TIntermediateParent, TParameter, TIntermediateParameter, TGrandParent, TIntermediateGrandParent}"/>.</param>
        public IntermediateParameterParentMemberBase(string name, TIntermediateGrandParent parent)
            : base(name, parent)
        {
        }

        /// <summary>
        /// Returns the dictionary of <typeparamref name="TIntermediateParameter"/> instances for the current <see cref="IntermediateParameterParentMemberBase{TParent, TIntermediateParent, TParameter, TIntermediateParameter, TGrandParent, TIntermediateGrandParent}"/>.
        /// </summary>
        public IntermediateParameterMemberDictionary<TParent, TIntermediateParent, TParameter, TIntermediateParameter> Parameters
        {
            get
            {
                this.CheckParameters();
                return this.parameters;
            }
        }

        private void CheckParameters()
        {
            if (this.parameters == null)
            {
                this.parameters = this.InitializeParameters();
                this.parameters.ItemAdded += new EventHandler<EventArgsR1<TIntermediateParameter>>(parameters_ItemAdded);
                this.parameters.ItemRemoved += new EventHandler<EventArgsR1<TIntermediateParameter>>(parameters_ItemRemoved);
            }
        }

        void parameters_ItemRemoved(object sender, EventArgsR1<TIntermediateParameter> e)
        {
            this.OnParameterRemoved(e);
        }

        protected virtual void OnParameterAdded(EventArgsR1<TIntermediateParameter> e)
        {
            if (this._ParameterAdded != null)
                this._ParameterAdded(this, new EventArgsR1<IIntermediateParameterMember>(e.Arg1));
            if (this.ParameterAdded != null)
                this.ParameterAdded(this, new EventArgsR1<TIntermediateParameter>(e.Arg1));
        }

        void parameters_ItemAdded(object sender, EventArgsR1<TIntermediateParameter> e)
        {
            this.OnParameterAdded(e);
        }

        protected virtual void OnParameterRemoved(EventArgsR1<TIntermediateParameter> e)
        {
            if (this._ParameterRemoved != null)
                this._ParameterRemoved(this, new EventArgsR1<IIntermediateParameterMember>(e.Arg1));
            if (this.ParameterRemoved != null)
                this.ParameterRemoved(this, new EventArgsR1<TIntermediateParameter>(e.Arg1));
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
        /// Returns whether the last element in <paramref name="Parameters"/> is a parameter
        /// array.
        /// </summary>
        public bool LastIsParams
        {
            get
            {
                throw new NotImplementedException();
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
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing)
                {
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


        public event EventHandler<EventArgsR1<TIntermediateParameter>> ParameterAdded;

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
