using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using AllenCopeland.Abstraction.Slf._Internal.Ast;
using System.IO;

namespace AllenCopeland.Abstraction.Slf.Ast
{
    partial class StrongNamePrivateKeyInfo 
    {
        private class LinkedPublicKeyInfo :
            StrongNamePublicKeyInfo
        {
            private StrongNamePrivateKeyInfo privateKey;
            /// <summary>
            /// Creates a new <see cref="LinkedPublicKeyInfo"/> instance
            /// with the owner <paramref name="privateKey"/>.
            /// </summary>
            /// <param name="privateKey">The <see cref="StrongNamePrivateKeyInfo"/>
            /// from which the <see cref="LinkedPublicKeyInfo"/> is derived.</param>
            public LinkedPublicKeyInfo(StrongNamePrivateKeyInfo privateKey)
                : base()
            {
                this.privateKey = privateKey;
            }

            /// <summary>
            /// Returns the <see cref="Int32"/> value denoting the number of bits
            /// that should be within the product of the Rivest Shamir Adleman (RSA)
            /// primes.
            /// </summary>
            public override int KeySize
            {
                get
                {
                    return this.privateKey.keySize;
                }
            }


            public override bool PrivateKeyAvailable
            {
                get { return true; }
            }

            public override IStrongNamePrivateKeyInfo PrivateKey
            {
                get { return this.privateKey; }
            }

            #region IStrongNameKeyInfo Members

            public override void WriteTo(string filename)
            {
                FileStream fstream = new FileStream(filename, FileMode.Create, FileAccess.Write);
                var bytes = StrongNameKeyPairHelper.CreateNewPublicKey(StrongNameKeyPairHelper.LoadKeyData(StrongNameKeyPairHelper.DecryptRsaData(this.privateKey.data.Item2, this.privateKey.data.Item1), this.privateKey.data.Item3), this.privateKey.keySize);
                fstream.Write(bytes, 0, bytes.Length);
                fstream.Close();
                fstream.Dispose();
            }

            #endregion

        }
    }
}
