using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Security;

namespace AllenCopeland.Abstraction._Internal.Utilities.Security
{
    partial class StrongNamePrivateKeyInfo 
    {
        private class LinkedPublicKeyInfo :
            StrongNamePublicKeyInfo
        {
            private StrongNamePrivateKeyInfo privateKey;
            private PublicKeyTokenData? publicToken;
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
                WriteTo(fstream);
            }

            public override void WriteTo(Stream fstream)
            {
              var bytes = StrongNameKeyPairHelper.CreateNewPublicKey(StrongNameKeyPairHelper.LoadKeyData(StrongNameKeyPairHelper.DecryptRsaData(this.privateKey.data.Item2, this.privateKey.data.Item1), this.privateKey.data.Item3), this.privateKey.keySize);
              fstream.Write(bytes, 0, bytes.Length);
              fstream.Close();
              fstream.Dispose();
            }

            #endregion

            public override PublicKeyTokenData PublicToken
            {
                get {
                    if (publicToken == null)
                    {
                        var shaHash = SHA1.Create();
                        var bytes = StrongNameKeyPairHelper.CreateNewPublicKey(StrongNameKeyPairHelper.LoadKeyData(StrongNameKeyPairHelper.DecryptRsaData(this.privateKey.data.Item2, this.privateKey.data.Item1), this.privateKey.data.Item3), this.privateKey.keySize);
                        var shaResult = shaHash.ComputeHash(bytes);
                        int shaLength = shaResult.Length;
                        publicToken = new PublicKeyTokenData(shaResult[shaLength - 8], shaResult[shaLength - 7], shaResult[shaLength - 6], shaResult[shaLength - 5], shaResult[shaLength - 4], shaResult[shaLength - 3], shaResult[shaLength - 2], shaResult[shaLength - 1]);
                    }
                    return this.publicToken.Value;
                }
            }

            public override byte[] EncryptData(byte[] data)
            {
                using (var rsaParams = new RSACryptoServiceProvider(this.KeySize))
                {
                    var parameters = StrongNameKeyPairHelper.LoadKeyData(StrongNameKeyPairHelper.DecryptRsaData(this.privateKey.data.Item2, this.privateKey.data.Item1), this.privateKey.data.Item3);
                    rsaParams.ImportParameters(parameters);
                    return rsaParams.Encrypt(data, true);
                }
            }
        }
    }
}
