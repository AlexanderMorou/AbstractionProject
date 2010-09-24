using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.GenericLayer.Members
{
    internal abstract class _SignatureMembersBase<TSignature, TSignatureParameter, TSignatureParent, TSignatureMembers> :
        _GroupedMembersBase<TSignatureParent, TSignature, TSignatureMembers>,
        ISignatureMemberDictionary<TSignature, TSignatureParameter, TSignatureParent>,
        ISignatureMemberDictionary
        where TSignature :
            class,
            ISignatureMember<TSignature, TSignatureParameter, TSignatureParent>
        where TSignatureParameter :
            ISignatureParameterMember<TSignature, TSignatureParameter, TSignatureParent>
        where TSignatureParent :
            ISignatureParent<TSignature, TSignatureParameter, TSignatureParent>
        where TSignatureMembers :
            class,
            ISignatureMemberDictionary<TSignature, TSignatureParameter, TSignatureParent>,
            IGroupedMemberDictionary<TSignatureParent, TSignature>
    {
        internal _SignatureMembersBase(_FullMembersBase master, TSignatureMembers originalSet, TSignatureParent parent)
            : base(master, originalSet, parent)
        {
        }

        #region ISignatureMemberDictionary<TSignature,TSignatureParameter,TSignatureParent> Members

        public virtual IFilteredSignatureMemberDictionary<TSignature, TSignatureParameter, TSignatureParent> Find(bool strict, ITypeCollection search)
        {
            return CLIGateway.FindCache<TSignature, TSignatureParameter, TSignatureParent>(this.Values, search, strict);
        }

        public virtual IFilteredSignatureMemberDictionary<TSignature, TSignatureParameter, TSignatureParent> Find(bool strict, params IType[] search)
        {
            return CLIGateway.FindCache<TSignature, TSignatureParameter, TSignatureParent>(this.Values, search, strict);
        }

        public virtual IFilteredSignatureMemberDictionary<TSignature, TSignatureParameter, TSignatureParent> Find(ITypeCollection search)
        {
            return this.Find(true, search);
        }

        public virtual IFilteredSignatureMemberDictionary<TSignature, TSignatureParameter, TSignatureParent> Find(params IType[] search)
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
