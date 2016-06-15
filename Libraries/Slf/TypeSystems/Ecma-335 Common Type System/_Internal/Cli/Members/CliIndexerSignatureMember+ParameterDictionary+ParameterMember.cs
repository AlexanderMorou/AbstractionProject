using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli.Metadata;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Members
{
    partial class CliIndexerSignatureMember<TIndexer, TIndexerParent>
    {
        private class ParameterDictionary :
            CliParameterMemberDictionary<TIndexer, IIndexerSignatureParameterMember<TIndexer, TIndexerParent>>
        {
            internal ParameterDictionary(_ICliManager manager, uint methodIndex, ICliMetadataRoot metadataRoot, TIndexer parent, bool fromSetter)
                : base(manager, methodIndex, metadataRoot, parent, fromSetter ? 1 : 0)
            { }

            protected override IIndexerSignatureParameterMember<TIndexer, TIndexerParent> CreateElementFrom(int index, ICliMetadataParameterTableRow metadata)
            {
                return new Parameter(metadata, (CliIndexerSignatureMember<TIndexer, TIndexerParent>)(object)this.Parent, index);
            }
            private class Parameter :
                CliParameterMember<TIndexer, CliIndexerSignatureMember<TIndexer, TIndexerParent>>,
                IIndexerSignatureParameterMember<TIndexer, TIndexerParent>
            {
                internal Parameter(ICliMetadataParameterTableRow metadataEntry, CliIndexerSignatureMember<TIndexer, TIndexerParent> parent, int index)
                    : base(metadataEntry, parent, index)
                {
                }

                protected override IMethodSignatureMember ActiveMethod
                {
                    get { return null; }
                }

                protected override IType ActiveType
                {
                    get { return this.Parent.Parent; }
                }

                ISignatureMember ISignatureParameterMember.Parent
                {
                    get { return this.Parent; }
                }
            }
        }
    }
}
