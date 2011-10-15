using System;
using System.Collections.Generic;
using System.Linq;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using System.Collections;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Members;
/*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    internal class LockedParameterMembersBase<TParent, TParameter> :
        LockedMembersBase<TParent, IGeneralMemberUniqueIdentifier, TParameter>,
        IParameterMemberDictionary<TParent, TParameter>,
        IParameterMemberDictionary
        where TParent :
            IParameterParent<TParent, TParameter>
        where TParameter :
            IParameterMember<TParent>
    {
        private ParameterMemberDictionaryTypes<TParent, TParameter> parameterTypes;
        internal LockedParameterMembersBase(IEnumerable<TParameter> parameters)
            : base(parameters)
        {
        }

        internal LockedParameterMembersBase(TParent parent, IEnumerable<TParameter> parameters)
            : base(parameters)
        {
        }

        #region IParameterMemberDictionary Members

        IParameterParent IParameterMemberDictionary.Parent
        {
            get { return this.Parent; }
        }

        #endregion

        #region IParameterMemberDictionary Members


        public ITypeCollectionBase ParameterTypes
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
