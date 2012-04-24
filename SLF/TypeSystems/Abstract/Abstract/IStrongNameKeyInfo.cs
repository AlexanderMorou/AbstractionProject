using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// Defines properties and methods for working with information about a 
    /// <see cref="StrongNameKeyPair"/>.
    /// </summary>
    public interface IStrongNameKeyInfo
    {
        /// <summary>
        /// Returns the <see cref="StrongNameKeyInfoType"/> associated to the
        /// <see cref="IStrongNameKeyInfo"/> which determines whether the key
        /// information is public or private.
        /// </summary>
        StrongNameKeyInfoType InformationType { get; }

        /// <summary>
        /// Returns the <see cref="Int32"/> value denoting the number of bits
        /// that should be within the product of the Rivest Shamir Adleman (RSA)
        /// primes.
        /// </summary>
        int KeySize { get; }

        /// <summary>
        /// Writes the <see cref="IStrongNameKeyInfo"/> in its internal format
        /// to the <paramref name="filename"/> provided.
        /// </summary>
        /// <param name="filename">The <see cref="String"/> that denotes
        /// the path of the file to write to.</param>
        void WriteTo(string filename);
    }
}
