using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;

namespace AllenCopeland.Abstraction.Slf.Abstract
{
    public interface IGroupedTypeDictionary<TIdentifier, TType> :
        ISubordinateDictionary<TIdentifier, IGeneralTypeUniqueIdentifier, TType, IType>,
        IGroupedDeclarationDictionary<TIdentifier, TType>
        where TIdentifier :
            ITypeUniqueIdentifier,
            IGeneralTypeUniqueIdentifier
        where TType :
            IType<TIdentifier, TType>
    {
    }
}
