using System.Collections.Generic;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using System;
/*---------------------------------------------------------------------\
 | Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf._Internal.Abstract.Members
{
    /// <summary>
    /// Provides a base implementation of <see cref="IParameterMemberDictionary{TParent, TParameter}"/> which
    /// provides a dictionary of <typeparamref name="TParameter"/> instances.
    /// </summary>
    /// <typeparam name="TParent">The type of parent that contains the <typeparamref name="TParameter"/> instances
    /// in the current implementation.</typeparam>
    /// <typeparam name="TParameter">The type of <see cref="IParameterMember{TParent}"/> which is contained by the 
    /// <typeparamref name="TParent"/> in the current implementation.</typeparam>
    internal class ParameterMemberDictionaryBase<TParent, TParameter> :
        MembersBase<TParent, IGeneralMemberUniqueIdentifier, TParameter>,
        IParameterMemberDictionary<TParent, TParameter>,
        IParameterMemberDictionary
        where TParent :
            IParameterParent<TParent, TParameter>
        where TParameter :
            IParameterMember<TParent>
    {
        private ParameterMemberDictionaryTypes<TParent, TParameter> parameterTypes;
        internal ParameterMemberDictionaryBase(IEnumerable<TParameter> parameters)
            : base()
        {
            foreach (TParameter p in parameters)
                base._Add(p.UniqueIdentifier, p);
        }
        internal ParameterMemberDictionaryBase(TParent parent)
            : base(parent)
        {

        }
        internal ParameterMemberDictionaryBase(TParent parent, IEnumerable<TParameter> parameters)
            : base(parent)
        {
            foreach (TParameter p in parameters)
                this._Add(p.UniqueIdentifier, p);
        }

        #region IParameterMemberDictionary Members

        IParameterParent IParameterMemberDictionary.Parent
        {
            get { return this.Parent; }
        }

        #endregion

        #region IParameterMemberDictionary Members


        public IControlledTypeCollection ParameterTypes
        {
            get {
                if (this.parameterTypes == null)
                    this.parameterTypes = new ParameterMemberDictionaryTypes<TParent, TParameter>(this);
                return this.parameterTypes;
            }
        }

        #endregion
    }
}
