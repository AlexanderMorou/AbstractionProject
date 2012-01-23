using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.Abstract;

namespace AllenCopeland.Abstraction.Slf.Abstract
{
    internal partial class StrongNamePrivateKeyInfo :
        IStrongNamePrivateKeyInfo
    {
        private IStrongNamePublicKeyInfo publicKey;
        private Tuple<int, byte[], bool> data;
        private int keySize;
        public StrongNamePrivateKeyInfo(int keySize)
        {
            this.keySize = keySize;
            this.data = StrongNameKeyPairHelper.GetNewStrongNameData(keySize);
        }

        internal StrongNamePrivateKeyInfo(bool o, byte[] data, int l, int ks) {
            this.keySize = keySize;
            this.data = new Tuple<int, byte[], bool>(l, data, o);
        }

        #region IStrongNamePrivateKeyInfo Members

        public IStrongNamePublicKeyInfo PublicKey
        {
            get
            {
                if (this.publicKey == null)
                    this.publicKey = new LinkedPublicKeyInfo(this);
                return this.publicKey;
            }
        }

        #endregion

        public StrongNameKeyPair CreateStrongName()
        {
            return StrongNameKeyPairHelper.CreateNewStrongNamePrivateKey(StrongNameKeyPairHelper.LoadKeyData(StrongNameKeyPairHelper.DecryptRsaData(data.Item2, data.Item1), data.Item3), keySize);
        }

        #region IStrongNameKeyInfo Members

        public StrongNameKeyInfoType InformationType
        {
            get { return StrongNameKeyInfoType.Private; }
        }

        /// <summary>
        /// Returns the <see cref="Int32"/> value denoting the number of bits
        /// that should be within the product of the Rivest Shamir Adleman (RSA)
        /// primes.
        /// </summary>
        public int KeySize
        {
            get { return keySize; }
        }

        #endregion

        #region IStrongNameKeyInfo Members

        public void WriteTo(string filename)
        {
            FileStream fstream = new FileStream(filename, FileMode.Create, FileAccess.Write);
            var bytes = StrongNameKeyPairHelper.CreateNewPrivateKey(StrongNameKeyPairHelper.LoadKeyData(StrongNameKeyPairHelper.DecryptRsaData(data.Item2, data.Item1), data.Item3), keySize);
            fstream.Write(bytes, 0, bytes.Length);
            fstream.Close();
            fstream.Dispose();
        }

        #endregion
    }
}
