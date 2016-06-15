using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Utilities.Security
{
    /// <summary>
    /// The kind of information stored within the strong name
    /// key pair info.
    /// </summary>
    public enum StrongNameKeyInfoType
    {
        /// <summary>
        /// The key information represents data about a public key.
        /// </summary>
        Public,
        /// <summary>
        /// The key information represents data about a private key.
        /// </summary>
        Private
    }
}
