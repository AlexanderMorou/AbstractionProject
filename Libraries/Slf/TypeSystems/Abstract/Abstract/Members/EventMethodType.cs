using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Abstract.Members
{
    public enum EventMethodType
    {
        /// <summary>
        /// The event is an add handler method.
        /// </summary>
        Add,
        /// <summary>
        /// The event is a remove handler method.
        /// </summary>
        Remove,
        /// <summary>
        /// The event is a fire handler method.
        /// </summary>
        Fire,
    }
}
