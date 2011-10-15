using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Abstract.Members
{
    public interface IGenericSignatureMemberUniqueIdentifier<TIdentifier> :
        ISignatureMemberUniqueIdentifier<TIdentifier>,
        IGenericParamParentUniqueIdentifier<TIdentifier>
        where TIdentifier :
            IGenericSignatureMemberUniqueIdentifier<TIdentifier>
    {
    }
}
