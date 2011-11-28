using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Members
{
    /// <summary>
    /// Provides an implementation of a parameter parent as a declaration.
    /// </summary>
    /// <typeparam name="TParent">The type which parents the <typeparamref name="TParameter"/> 
    /// in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateParent">The type which parents the <typeparamref name="TIntermediateParameter"/>
    /// in the intermediate abstract syntax tree.</typeparam>
    /// <typeparam name="TParameter">The type of parameter in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateParameter">The type of parameter in the intermediate
    /// abstract syntax tree.</typeparam>
    public abstract partial class IntermediateParameterParentDeclarationBase<TParent, TIntermediateParent, TParameter, TIntermediateParameter> :
        IntermediateDeclarationBase,
        IIntermediateParameterParent<TParent, TIntermediateParent, TParameter, TIntermediateParameter>
        where TParent :
            IParameterParent<TParent, TParameter>
        where TIntermediateParent :
            TParent,
            IIntermediateParameterParent<TParent, TIntermediateParent, TParameter, TIntermediateParameter>
        where TParameter :
            IParameterMember<TParent>
        where TIntermediateParameter :
            TParameter,
            IIntermediateParameterMember<TParent, TIntermediateParent>
    {
        private IntermediateParameterMemberDictionary<TParent, TIntermediateParent, TParameter, TIntermediateParameter> parameters;

        /// <summary>
        /// Creates a new <see cref="IntermediateParameterParentDeclarationBase{TParent, TIntermediateParent, TParameter, TIntermediateParameter}"/>
        /// initialized to a default state.
        /// </summary>
        public IntermediateParameterParentDeclarationBase()
            : base()
        {
        }

        /// <summary>
        /// Returns the dictionary of <typeparamref name="TIntermediateParameter"/> instances for the current <see cref="IntermediateParameterParentDeclarationBase{TParent, TIntermediateParent, TParameter, TIntermediateParameter}"/>.
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
                this.parameters = this.InitializeParameters();
        }

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
    }
}
