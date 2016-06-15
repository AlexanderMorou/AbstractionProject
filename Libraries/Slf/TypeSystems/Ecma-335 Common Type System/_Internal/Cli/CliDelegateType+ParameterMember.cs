using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    partial class CliDelegateType
    {
        private class ParameterMember :
            CliParameterMember<IDelegateType, CliDelegateType>,
            IDelegateTypeParameterMember
        {
            internal ParameterMember(ICliMetadataParameterTableRow metadataEntry, CliDelegateType @delegate, int index)
                : base(metadataEntry, @delegate, index)
            {
            }

            protected override IMethodSignatureMember ActiveMethod
            {
                get { return null; }
            }

            protected override IType ActiveType
            {
                get { return this.Parent; }
            }
        }
    }
}
