using System.Collections.Generic;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
/*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
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
    internal class ParameterMembersBase<TParent, TParameter> :
        MembersBase<TParent, TParameter>,
        IParameterMemberDictionary<TParent, TParameter>,
        IParameterMemberDictionary
        where TParent :
            IParameterParent<TParent, TParameter>
        where TParameter :
            IParameterMember<TParent>
    {
        internal ParameterMembersBase(IEnumerable<TParameter> parameters)
            : base()
        {
            foreach (TParameter p in parameters)
                base._Add(p.UniqueIdentifier, p);
        }
        internal ParameterMembersBase(TParent parent)
            : base(parent)
        {

        }
        internal ParameterMembersBase(TParent parent, IEnumerable<TParameter> parameters)
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

    }
}
