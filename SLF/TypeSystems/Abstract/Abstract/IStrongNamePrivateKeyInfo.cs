using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// Defines properties and methods for working with a information
    /// about a strong name's private key.
    /// </summary>
    public interface IStrongNamePrivateKeyInfo :
        IStrongNameKeyInfo
    {
        /// <summary>
        /// Returns the <see cref="IStrongNamePublicKeyInfo"/>
        /// which is the public key derived from the 
        /// <see cref="IStrongNamePrivateKeyInfo"/>.
        /// </summary>
        IStrongNamePublicKeyInfo PublicKey { get; }
        /// <summary>
        /// Returns a new <see cref="StrongNameKeyPair"/> that represents
        /// the private key aspect of the strong name.
        /// </summary>
        /// <returns>A new <see cref="StrongNameKeyPair"/> instance
        /// which represents the 
        /// <see cref="IStrongNamePrivateKeyInfo"/>.</returns>
        StrongNameKeyPair CreateStrongName();
    }
}
