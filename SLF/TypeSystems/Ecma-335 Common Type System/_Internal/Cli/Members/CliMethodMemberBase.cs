using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Members
{
    internal abstract partial class CliMethodMemberBase<TMethod, TMethodParent> :
        CliMethodSignatureBase<IMethodParameterMember<TMethod, TMethodParent>, TMethod, TMethodParent>,
        IMethodMember<TMethod, TMethodParent>
        where TMethod :
            IMethodMember<TMethod, TMethodParent>
        where TMethodParent :
            IMethodParent<TMethod, TMethodParent>
    {
        protected CliMethodMemberBase(ICliMetadataMethodDefinitionTableRow metadataEntry, _ICliAssembly assembly, TMethodParent parent)
            : base(metadataEntry, assembly, parent)
        {
        }

        public AccessLevelModifiers AccessLevel
        {
            get { return CliCommon.GetMethodAccessLevel(this.MetadataEntry.Flags); }
        }
    }
}
