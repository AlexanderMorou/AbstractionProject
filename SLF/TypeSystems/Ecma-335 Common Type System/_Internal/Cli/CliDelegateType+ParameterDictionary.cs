using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Members;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    internal partial class CliDelegateType
    {
        private class ParameterDictionary :
            CliParameterMemberDictionary<IDelegateType, IDelegateTypeParameterMember>,
            IDelegateTypeParameterDictionary
        {
            private CliDelegateType parent;
            internal ParameterDictionary(CliDelegateType parent)
                : base(parent.IdentityManager, parent.InvokeMethodIndex, parent.MetadataEntry.MetadataRoot)
            {
                this.parent = parent;
            }

            protected override IDelegateTypeParameterMember CreateElementFrom(int index, ICliMetadataParameterTableRow metadata)
            {
                return new ParameterMember(metadata, this.parent, index);
            }
        }
    }
}
