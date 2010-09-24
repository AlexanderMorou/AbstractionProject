using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.GenericLayer.Members
{
    partial class _IndexerMemberBase<TIndexer, TIndexerParent>
    {
        private class _Parameters :
            _ParametersBase<TIndexer, IIndexerParameterMember<TIndexer, TIndexerParent>>
        {
            internal _Parameters(_IndexerMemberBase<TIndexer, TIndexerParent> parent, IParameterMemberDictionary<TIndexer, IIndexerParameterMember<TIndexer, TIndexerParent>> original)
                : base((TIndexer)(object)parent, original)
            {
            }
            internal new class _Parameter :
                _ParametersBase<TIndexer, IIndexerParameterMember<TIndexer, TIndexerParent>>._Parameter,
                IIndexerParameterMember<TIndexer, TIndexerParent>
            {
                internal _Parameter(IIndexerParameterMember<TIndexer, TIndexerParent> original, _IndexerMemberBase<TIndexer, TIndexerParent> parent)
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

            protected new _IndexerMemberBase<TIndexer, TIndexerParent> Parent
            {
                get
                {
                    return (_IndexerMemberBase<TIndexer, TIndexerParent>)(object)base.Parent;
                }
            }

            protected override IIndexerParameterMember<TIndexer, TIndexerParent> GetWrapper(IIndexerParameterMember<TIndexer, TIndexerParent> original, TIndexer parent)
            {
                return new _Parameter(original, this.Parent);
            }
        }
    }
}
