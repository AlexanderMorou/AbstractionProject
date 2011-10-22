using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    /// <summary>
    /// Provides a root implementation of <see cref="IMethodMemberDictionary{TMethod, TMethodParent}"/> for working
    /// with a locked series of <typeparamref name="TMethod"/> instances contained by a 
    /// <typeparamref name="TMethodParent"/> instance.
    /// </summary>
    /// <typeparam name="TMethod">The type of <see cref="IMethodMember{TMethod, TMethodParent}"/>
    /// in the current implementation.</typeparam>
    /// <typeparam name="TMethodParent">The type of <see cref="IMethodParent{TMethod, TType}"/> in the current
    /// implementation.</typeparam>
    internal class LockedMethodMembersBase<TMethod, TMethodParent> :
        LockedMethodSignatureMembersBase<IMethodParameterMember<TMethod, TMethodParent>, TMethod, TMethodParent>,
        IMethodMemberDictionary<TMethod, TMethodParent>,
        IMethodMemberDictionary
        where TMethod :
            class,
            IMethodMember<TMethod, TMethodParent>
        where TMethodParent :
            IMethodParent<TMethod, TMethodParent>
    {

        /// <summary>
        /// Creates a new <see cref="LockedMethodMembersBase{TMethod, TMethodParent}"/> with the 
        /// <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="parent">The <typeparamref name="TMethodParent"/> which contains the 
        /// <see cref="LockedMethodMembersBase{TMethod, TMethodParent}"/>.</param>
        internal LockedMethodMembersBase(LockedFullMembersBase master, TMethodParent parent)
            : base(master, parent)
        {
        }
        /// <summary>
        /// Creates a new <see cref="LockedMethodMembersBase{TMethod, TMethodParent}"/> with the 
        /// <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="parent">The <typeparamref name="TMethodParent"/> which contains the 
        /// <see cref="LockedMethodMembersBase{TMethod, TMethodParent}"/>.</param>
        /// <param name="series">The <see cref="IEnumerable{T}"/> which contains the <paramref name="parent"/>
        /// contains.</param>
        internal LockedMethodMembersBase(LockedFullMembersBase master, TMethodParent parent, IEnumerable<TMethod> series)
            : base(master, parent, series)
        {
        }

        /// <summary>
        /// Creates a new <see cref="LockedMethodMembersBase{TMethod, TMethodParent}"/> with the 
        /// <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="parent">The <typeparamref name="TMethodParent"/> which contains the 
        /// <see cref="LockedMethodMembersBase{TMethod, TMethodParent}"/>.</param>
        /// <param name="fetchImpl">The <see cref="Func{T, TResult}"/>
        /// which is used to instantiate <typeparamref name="TMethod"/> instances
        /// from <see cref="MethodInfo"/> stock to propagate the <see cref="LockedMethodMembersBase{TMethod, TMethodParent}"/>.</param>
        internal LockedMethodMembersBase(LockedFullMembersBase master, TMethodParent parent, MethodInfo[] seriesData, Func<MethodInfo, TMethod> fetchImpl)
            : base(master, parent, seriesData, fetchImpl)
        {

        }

        #region IMethodMemberDictionary Members
        IMethodParent IMethodMemberDictionary.Parent
        {
            get
            {
                return (IMethodParent)base.Parent;
            }
        }
        int IMethodMemberDictionary.IndexOf(IMethodMember method)
        {
            if (!(method is TMethod))
                return -1;
            return this.IndexOf((TMethod)(method));
        }
        #endregion

        protected override IGeneralGenericSignatureMemberUniqueIdentifier FetchKey(MethodInfo item)
        {
            return item.GetUniqueIdentifier();
        }

    }
}
