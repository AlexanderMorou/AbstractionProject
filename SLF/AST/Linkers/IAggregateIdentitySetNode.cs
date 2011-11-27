using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;

namespace AllenCopeland.Abstraction.Slf.Linkers
{
    /// <summary>
    /// Defines properties and methods for working with an set of identities
    /// within an
    /// </summary>
    public interface IAggregateIdentitySetNode :
        IControlledStateCollection<IAggregateIdentityNode>,
        IAggregateIdentityNode
    {
    }
}
