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
    partial class CliIndexerMember<TIndexer, TIndexerParent>
    {
        private class ParameterDictionary :
            CliParameterMemberDictionary<TIndexer, IIndexerParameterMember<TIndexer, TIndexerParent>>
        {
            internal ParameterDictionary(_ICliManager manager, uint methodIndex, ICliMetadataRoot metadataRoot, TIndexer parent, bool fromSetter)
                : base(manager, methodIndex, metadataRoot, parent, fromSetter ? 1 : 0)
            { }

            protected override IIndexerParameterMember<TIndexer, TIndexerParent> CreateElementFrom(int index, ICliMetadataParameterTableRow metadata)
            {
                return new Parameter(metadata, (CliIndexerMember<TIndexer, TIndexerParent>)(object)this.Parent, index);
            }
            private class Parameter :
                CliParameterMember<TIndexer, CliIndexerMember<TIndexer, TIndexerParent>>,
                IIndexerParameterMember<TIndexer, TIndexerParent>
            {
                internal Parameter(ICliMetadataParameterTableRow metadataEntry, CliIndexerMember<TIndexer, TIndexerParent> parent, int index)
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
