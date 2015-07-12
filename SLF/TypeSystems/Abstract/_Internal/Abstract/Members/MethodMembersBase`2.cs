using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf._Internal.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf._Internal.Abstract.Members
{
    /// <summary>
    /// Provides a root implementation of <see cref="IMethodMemberDictionary{TMethod, TMethodParent}"/> for working
    /// with a series of <typeparamref name="TMethod"/> instances contained by a 
    /// <typeparamref name="TMethodParent"/> instance.
    /// </summary>
    /// <typeparam name="TMethod">The type of <see cref="IMethodMember{TMethod, TMethodParent}"/>
    /// in the current implementation.</typeparam>
    /// <typeparam name="TMethodParent">The type of <see cref="IMethodParent{TMethod, TType}"/> in the current
    /// implementation.</typeparam>
    internal class MethodMembersBase<TMethod, TMethodParent> :
        MethodSignatureMembersBase<IMethodParameterMember<TMethod, TMethodParent>, TMethod, TMethodParent>,
        IMethodMemberDictionary<TMethod, TMethodParent>,
        IMethodMemberDictionary
        where TMethod :
            IMethodMember<TMethod, TMethodParent>
        where TMethodParent :
            IMethodParent<TMethod, TMethodParent>
    {
        /// <summary>
        /// Creates a new <see cref="MethodMembersBase{TMethod, TMethodParent}"/> initialized to a
        /// default state.
        /// </summary>
        protected MethodMembersBase(FullMembersBase master)
            : base(master)
        {
        }

        /// <summary>
        /// Creates a new <see cref="MethodMembersBase{TMethod, TMethodParent}"/> with the 
        /// <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="parent">The <typeparamref name="TMethodParent"/> which contains the 
        /// <see cref="MethodMembersBase{TMethod, TMethodParent}"/>.</param>
        internal MethodMembersBase(FullMembersBase master, TMethodParent parent) 
            : base(master, parent)
        {
        }

        #region IMethodMemberDictionary Members

        int IMethodMemberDictionary.IndexOf(IMethodMember method)
        {
            if (!(method is TMethod))
                return -1;
            return this.IndexOf((TMethod)(method));
        }

        IMethodParent IMethodMemberDictionary.Parent
        {
            get
            {
                return (IMethodParent)base.Parent;
            }
        }
        #endregion
    }
}
