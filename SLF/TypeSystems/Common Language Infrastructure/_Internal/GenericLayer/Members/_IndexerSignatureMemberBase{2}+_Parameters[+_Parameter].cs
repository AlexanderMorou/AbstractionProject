using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.GenericLayer.Members
{
    partial class _IndexerSignatureMemberBase<TIndexer, TIndexerParent>
    {
        private class _Parameters :
            _ParametersBase<TIndexer, IIndexerSignatureParameterMember<TIndexer, TIndexerParent>>
        {
            internal _Parameters(_IndexerSignatureMemberBase<TIndexer, TIndexerParent> parent, IParameterMemberDictionary<TIndexer, IIndexerSignatureParameterMember<TIndexer, TIndexerParent>> original)
                : base((TIndexer)(object)parent, original)
            {
            }
            internal new class _Parameter :
                _ParametersBase<TIndexer, IIndexerSignatureParameterMember<TIndexer, TIndexerParent>>._Parameter,
                IIndexerSignatureParameterMember<TIndexer, TIndexerParent>
            {
                internal _Parameter(IIndexerSignatureParameterMember<TIndexer, TIndexerParent> original, _IndexerSignatureMemberBase<TIndexer, TIndexerParent> parent)
                    : base(original, (TIndexer)(object)parent)
                {
                }

                #region ISignatureParameterMember Members

                ISignatureMember ISignatureParameterMember.Parent
                {
                    get { return this.Parent; }
                }

                #endregion
            }
            protected new _IndexerSignatureMemberBase<TIndexer, TIndexerParent> Parent
            {
                get
                {
                    return (_IndexerSignatureMemberBase<TIndexer, TIndexerParent>)(object)base.Parent;
                }
            }

            protected override IIndexerSignatureParameterMember<TIndexer, TIndexerParent> GetWrapper(IIndexerSignatureParameterMember<TIndexer, TIndexerParent> original, TIndexer parent)
            {
                return new _Parameter(original, this.Parent);
            }
        }
    }
}
