using System.Collections.Generic;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
/*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf._Internal.Abstract
{
    internal class LockedParameterMembersBase<TParent, TParameter> :
        LockedMembersBase<TParent, TParameter>,
        IParameterMemberDictionary<TParent, TParameter>,
        IParameterMemberDictionary
        where TParent :
            IParameterParent<TParent, TParameter>
        where TParameter :
            IParameterMember<TParent>
    {

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

    }
}
