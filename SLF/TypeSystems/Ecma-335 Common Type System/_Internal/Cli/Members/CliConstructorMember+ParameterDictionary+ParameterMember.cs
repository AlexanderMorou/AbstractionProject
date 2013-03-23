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
    partial class CliConstructorMember<TCtor, TCtorParent>
    {
        private class ParameterDictionary :
            CliParameterMemberDictionary<TCtor, IConstructorParameterMember<TCtor, TCtorParent>>
        {
            public ParameterDictionary(_ICliManager manager, uint methodIndex, ICliMetadataRoot metadataRoot, TCtor parent)
                : base(manager, methodIndex, metadataRoot, parent)
            {
            }

            protected override IConstructorParameterMember<TCtor, TCtorParent> CreateElementFrom(int index, ICliMetadataParameterTableRow metadata)
            {
                return new ParameterMember(metadata, (CliConstructorMember<TCtor, TCtorParent>)(object)this.Parent, index);
            }

            private class ParameterMember :
                CliParameterMember<TCtor, CliConstructorMember<TCtor, TCtorParent>>,
                IConstructorParameterMember<TCtor, TCtorParent>
            {
                internal ParameterMember(ICliMetadataParameterTableRow metadataEntry, CliConstructorMember<TCtor, TCtorParent> parent, int index)
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
                ISignatureMember ISignatureParameterMember.Parent { get { return this.Parent; } }
            }
        }
    }
}
