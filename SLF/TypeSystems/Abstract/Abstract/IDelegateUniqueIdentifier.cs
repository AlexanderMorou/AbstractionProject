using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;

namespace AllenCopeland.Abstraction.Slf.Abstract
{
    public interface IDelegateUniqueIdentifier  :
        IGenericSignatureMemberUniqueIdentifier<IDelegateUniqueIdentifier>,
        IGenericTypeUniqueIdentifier<IDelegateUniqueIdentifier>,
        IGeneralGenericTypeUniqueIdentifier,
        IGeneralGenericSignatureMemberUniqueIdentifier
    {
    }
}
