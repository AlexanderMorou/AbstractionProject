using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.GenericLayer.Members
{
    internal abstract class _MethodMembersBase<TMethod, TMethodParent> :
        _MethodSignatureMembersBase<IMethodParameterMember<TMethod, TMethodParent>, TMethod, TMethodParent, IMethodMemberDictionary<TMethod, TMethodParent>>,
        IMethodMemberDictionary<TMethod, TMethodParent>,
        IMethodMemberDictionary
        where TMethod :
            class,
            IMethodMember<TMethod, TMethodParent>
        where TMethodParent :
            IMethodParent<TMethod, TMethodParent>
    {
        protected _MethodMembersBase(_FullMembersBase master, IMethodMemberDictionary<TMethod, TMethodParent> originalSet, TMethodParent parent)
            : base(master, originalSet, parent)
        {
        }

        #region IMethodMemberDictionary Members

        IMethodParent IMethodMemberDictionary.Parent
        {
            get { return this.Parent; }
        }

        int IMethodMemberDictionary.IndexOf(IMethodMember method)
        {
            if (method is TMethod)
                return this.IndexOf((TMethod)method);
            return -1;
        }

        #endregion

    }
}
