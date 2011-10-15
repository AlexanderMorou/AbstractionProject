using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Members;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    internal abstract class LockedSignatureMembersBase<TSignatureIdentifier, TSignature, TSignatureParameter, TSignatureParent, TSourceItem> :
        LockedGroupedMembersBase<TSignatureParent, TSignatureIdentifier, TSignature, TSourceItem>,
        ISignatureMemberDictionary<TSignatureIdentifier, TSignature, TSignatureParameter, TSignatureParent>,
        ISignatureMemberDictionary
        where TSignatureIdentifier :
            ISignatureMemberUniqueIdentifier<TSignatureIdentifier>,
            IGeneralMemberUniqueIdentifier
        where TSignature :
            class,
            ISignatureMember<TSignatureIdentifier, TSignature, TSignatureParameter, TSignatureParent>
        where TSignatureParameter :
            ISignatureParameterMember<TSignatureIdentifier, TSignature, TSignatureParameter, TSignatureParent>
        where TSignatureParent :
            ISignatureParent<TSignatureIdentifier, TSignature, TSignatureParameter, TSignatureParent>
        where TSourceItem :
            class
    {
        protected LockedSignatureMembersBase(LockedFullMembersBase master, TSignatureParent parent)
            : base(master, parent)
        {
        }
        protected LockedSignatureMembersBase(LockedFullMembersBase master)
            : base(master)
        {
        }

        protected LockedSignatureMembersBase(LockedFullMembersBase master, TSignatureParent parent, TSourceItem[] sourceData, Func<TSourceItem, TSignature> fetchImpl, Func<TSourceItem, string> nameImpl)
            : base(master, parent, sourceData, fetchImpl, nameImpl)
        {
        }

        protected LockedSignatureMembersBase(LockedFullMembersBase master, IEnumerable<TSignature> series)
            : base(master, series)
        {
        }

        protected LockedSignatureMembersBase(LockedFullMembersBase master, TSignatureParent parent, IEnumerable<TSignature> series)
            : base(master, parent, series)
        {
        }


        #region ISignatureMemberDictionary<TSignature,TSignatureParameter,TSignatureParent> Members

        public virtual IFilteredSignatureMemberDictionary<TSignatureIdentifier, TSignature, TSignatureParameter, TSignatureParent> Find(bool strict, ITypeCollectionBase search)
        {
            return CLICommon.FindCache<TSignatureIdentifier, TSignature, TSignatureParameter, TSignatureParent>(this.Values, search, strict);
        }

        public virtual IFilteredSignatureMemberDictionary<TSignatureIdentifier, TSignature, TSignatureParameter, TSignatureParent> Find(bool strict, params IType[] search)
        {
            return CLICommon.FindCache<TSignatureIdentifier, TSignature, TSignatureParameter, TSignatureParent>(this.Values, search, strict);
        }

        public virtual IFilteredSignatureMemberDictionary<TSignatureIdentifier, TSignature, TSignatureParameter, TSignatureParent> Find(ITypeCollectionBase search)
        {
            return this.Find(true, search);
        }

        public virtual IFilteredSignatureMemberDictionary<TSignatureIdentifier, TSignature, TSignatureParameter, TSignatureParent> Find(params IType[] search)
        {
            return this.Find(true, search);
        }

        #endregion

        #region ISignatureMemberDictionary Members

        IFilteredSignatureMemberDictionary ISignatureMemberDictionary.Find(bool strict, ITypeCollection search)
        {
            return (IFilteredSignatureMemberDictionary)this.Find(strict, search);
        }

        IFilteredSignatureMemberDictionary ISignatureMemberDictionary.Find(ITypeCollection search)
        {
            return (IFilteredSignatureMemberDictionary)this.Find(search);
        }

        IFilteredSignatureMemberDictionary ISignatureMemberDictionary.Find(bool strict, params IType[] search)
        {
            return (IFilteredSignatureMemberDictionary)this.Find(strict, search);
        }

        IFilteredSignatureMemberDictionary ISignatureMemberDictionary.Find(params IType[] search)
        {
            return (IFilteredSignatureMemberDictionary)this.Find(search);
        }

        #endregion
    }
}
