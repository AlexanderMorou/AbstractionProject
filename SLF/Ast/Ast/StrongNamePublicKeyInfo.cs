using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace AllenCopeland.Abstraction.Slf.Ast
{
    internal abstract class StrongNamePublicKeyInfo :
        IStrongNamePublicKeyInfo
    {

        #region IStrongNamePublicKeyInfo Members

        public abstract bool PrivateKeyAvailable { get; }

        public abstract IStrongNamePrivateKeyInfo PrivateKey { get; }

        #endregion

        #region IStrongNameKeyInfo Members

        public StrongNameKeyInfoType InformationType
        {
            get { return StrongNameKeyInfoType.Public; }
        }

        /// <summary>
        /// Returns the <see cref="Int32"/> value denoting the number of bits
        /// that should be within the product of the Rivest Shamir Adleman (RSA)
        /// primes.
        /// </summary>
        public abstract int KeySize { get; }

        #endregion

        #region IStrongNameKeyInfo Members

        public abstract void WriteTo(string filename);

        #endregion
    }
}
