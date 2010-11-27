using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Utilities.Events;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Members
{
    public abstract class IntermediateMethodMemberDictionary<TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent> :
        IntermediateGroupedMethodSignatureMemberDictionary<IMethodParameterMember<TMethod, TMethodParent>, IIntermediateMethodParameterMember<TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent>, TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent>,
        IIntermediateMethodMemberDictionary<TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent>,
        IIntermediateMethodMemberDictionary
        where TMethod :
            IMethodMember<TMethod, TMethodParent>
        where TIntermediateMethod :
            TMethod,
            IIntermediateMethodMember<TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent>
        where TMethodParent :
            IMethodParent<TMethod, TMethodParent>
        where TIntermediateMethodParent :
            TMethodParent,
            IIntermediateMethodParent<TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent>
    {
        protected IntermediateMethodMemberDictionary(IntermediateFullMemberDictionary master, TIntermediateMethodParent parent)
            : base(master, parent)
        {
        }
        protected IntermediateMethodMemberDictionary(IntermediateFullMemberDictionary master, TIntermediateMethodParent parent, IntermediateMethodMemberDictionary<TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent> root)
            : base(master, parent, root)
        {
        }

        #region IMethodMemberDictionary Members

        IMethodParent IMethodMemberDictionary.Parent
        {
            get { return this.Parent; }
        }

        int IMethodMemberDictionary.IndexOf(IMethodMember method)
        {
            if (!(method is TIntermediateMethod))
                throw new ArgumentException("method");
            return this.IndexOf((TIntermediateMethod)method);
        }

        #endregion

    }
}
