using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Linkers
{
    public interface ISubScopeAggregate :
        IScopeAggregate
    {
        /// <summary>
        /// Returns the <see cref="IScopeAggregate"/>
        /// to which the current <see cref="ISubScopeAggregate"/>
        /// creates an identity union with.
        /// </summary>
        IScopeAggregate Parent { get; }
    }
}
