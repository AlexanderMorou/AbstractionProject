using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Members
{
    partial class IntermediateMethodMemberBase<TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent>
        where TMethod :
            class,
            IMethodMember<TMethod, TMethodParent>
        where TIntermediateMethod :
            IIntermediateMethodMember<TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent>,
            TMethod
        where TMethodParent :
            IMethodParent<TMethod, TMethodParent>
        where TIntermediateMethodParent :
            IIntermediateMethodParent<TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent>,
            TMethodParent
    {
        /// <summary>
        /// Provides a parameter member for a method member.
        /// </summary>
        protected new class ParameterMember :
            IntermediateMethodSignatureMemberBase<IMethodParameterMember<TMethod, TMethodParent>, IIntermediateMethodParameterMember<TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent>, TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent>.ParameterMember,
            IIntermediateMethodParameterMember<TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent>,
            IIntermediateMethodParameterMember
        {
            /// <summary>
            /// Creates a new <see cref="ParameterMember"/> with the 
            /// <paramref name="parent"/> provided.
            /// </summary>
            /// <param name="parent">The <typeparamref name="TIntermediateMethod"/>
            /// which contains the <see cref="ParameterMember"/>.</param>
            public ParameterMember(TIntermediateMethod parent)
                : base(parent)
            {
            }

            #region IMethodParameterMember Members

            IMethodMember IMethodParameterMember.Parent
            {
                get { return base.Parent; }
            }

            #endregion
        }
    }
}
