using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// Defines properties and methods for working with a information
    /// about a strong name's public key.
    /// </summary>
    public interface IStrongNamePublicKeyInfo :
        IStrongNameKeyInfo
    {
        /// <summary>
        /// Returns whether the <see cref="IStrongNamePublicKeyInfo"/>
        /// was returned by a private key and can thus point back to it.
        /// </summary>
        bool PrivateKeyAvailable { get; }
        /// <summary>
        /// Returns the <see cref="IStrongNamePrivateKeyInfo"/> 
        /// from which the <see cref="IStrongNamePublicKeyInfo"/>
        /// was derived.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">
        /// thrown when <see cref="PrivateKeyAvailable"/> is false.
        /// </exception>
        IStrongNamePrivateKeyInfo PrivateKey { get; }
        /// <summary>
        /// Returns the <see cref="PublicKeyTokenData"/>
        /// </summary>
        PublicKeyTokenData PublicToken { get; }
    }
}
