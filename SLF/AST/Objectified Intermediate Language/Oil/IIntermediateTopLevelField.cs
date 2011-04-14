using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;

namespace AllenCopeland.Abstraction.Slf.Oil
{
    public interface IIntermediateTopLevelField :
        IFieldMember<ITopLevelField, INamespaceParent>,
        ITopLevelField
    {
    }
}
