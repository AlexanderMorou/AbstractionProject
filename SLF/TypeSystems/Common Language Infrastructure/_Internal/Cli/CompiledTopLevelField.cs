using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Members;
using System.Reflection;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    internal class CompiledTopLevelField :
        CompiledFieldMemberBase<ITopLevelField, INamespaceParent>,
        ITopLevelField
    {
        public CompiledTopLevelField(FieldInfo memberInfo, INamespaceParent parent)
            : base(memberInfo, parent)
        {

        }
    }
}
