using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf._Internal.Abstract;
/*---------------------------------------------------------------------\
| Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
|----------------------------------------------------------------------|
| The Abstraction Project's code is provided under a contract-release  |
| basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
\-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    internal class LockedMethodSignatureMembersBase<TSignature, TSignatureParent> :
        LockedMethodSignatureMembersBase<IMethodSignatureParameterMember<TSignature, TSignatureParent>, TSignature, TSignatureParent>,
        IMethodSignatureMemberDictionary<TSignature, TSignatureParent>
        where TSignature :
            class,
            IMethodSignatureMember<TSignature, TSignatureParent>
        where TSignatureParent :
            IMethodSignatureParent<TSignature, TSignatureParent>
    {
        /// <summary>
        /// Creates a new <see cref="LockedMethodSignatureMembersBase{TSignature, TSignatureParent}"/> initialized to a default
        /// state.
        /// </summary>
        public LockedMethodSignatureMembersBase(LockedFullMembersBase master)
            : base(master)
        {
        }

        /// <summary>
        /// Creates a new <see cref="LockedMethodSignatureMembersBase{TSignature, TSignatureParent}"/> with the <paramref name="parent"/>
        /// provided.
        /// </summary>
        /// <param name="parent">The <typeparamref name="TSignatureParent"/> which contains the 
        /// <see cref="LockedMethodSignatureMembersBase{TSignature, TSignatureParent}"/>.</param>
        public LockedMethodSignatureMembersBase(LockedFullMembersBase master, TSignatureParent parent)
            : base(master, parent)
        {
        }

        /// <summary>
        /// Creates a new <see cref="LockedMethodSignatureMembersBase{TSignature, TSignatureParent}"/> with the <paramref name="parent"/>
        /// provided.
        /// </summary>
        /// <param name="parent">The <typeparamref name="TSignatureParent"/> which contains the 
        /// <see cref="LockedMethodSignatureMembersBase{TSignature, TSignatureParent}"/>.</param>
        /// <param name="series">The <see cref="IEnumerable{T}"/> which contains the <paramref name="parent"/>
        /// contains.</param>
        public LockedMethodSignatureMembersBase(LockedFullMembersBase master, TSignatureParent parent, IEnumerable<TSignature> series)
            : base(master, parent, series)
        {
        }
        /// <summary>
        /// Creates a new <see cref="LockedMethodSignatureMembersBase{TSignature, TSignatureParent}"/> with the <paramref name="parent"/>
        /// provided.
        /// </summary>
        /// <param name="parent">The <typeparamref name="TSignatureParent"/> which contains the 
        /// <see cref="LockedMethodSignatureMembersBase{TSignature, TSignatureParent}"/>.</param>
        /// <param name="seriesData">The <see cref="MethodInfo"/> array which contains the methods <paramref name="parent"/>
        /// contains.</param>
        public LockedMethodSignatureMembersBase(LockedFullMembersBase master, TSignatureParent parent, MethodInfo[] seriesData, Func<MethodInfo, TSignature> fetchImpl)
            : base(master, parent, seriesData, fetchImpl)
        {
        }
    }
}
