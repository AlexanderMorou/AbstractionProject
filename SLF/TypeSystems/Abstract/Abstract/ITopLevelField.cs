using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;

namespace AllenCopeland.Abstraction.Slf.Abstract
{
    public interface ITopLevelField :
        IFieldMember<ITopLevelField, INamespaceParent>
    {
    }
}
