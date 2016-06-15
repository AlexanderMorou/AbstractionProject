using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/*----------------------------------------\
| Copyright © 2012 Allen Copeland Jr.     |
|-----------------------------------------|
| The Abstraction Project's code is prov- |
| -ided under a contract-release basis.   |
| DO NOT DISTRIBUTE and do not use beyond |
| the contract terms.                     |
\--------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Abstract.Members
{
    /// <summary>
    /// The means to which an <see cref="IEventSignatureMember"/>'s signature is sourced.
    /// </summary>
    public enum EventSignatureSource :
        byte
    {
        /// <summary>
        /// The <see cref="IEventSignatureMember"/>'s signature is
        /// declared explicitly.
        /// </summary>
        Declared,
        /// <summary>
        /// The <see cref="IEventSignatureMember"/>'s signature is
        /// defined through an <see cref="IDelegateType"/>.
        /// </summary>
        Delegate,
    }
}
