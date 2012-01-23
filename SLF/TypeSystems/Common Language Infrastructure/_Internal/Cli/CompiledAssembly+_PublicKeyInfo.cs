using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    partial class CompiledAssembly
    {
        private class _PublicKeyInfo :
            IStrongNamePublicKeyInfo
        {
            private CompiledAssembly owner;
            private PublicKeyTokenData? publicToken;
            public _PublicKeyInfo(CompiledAssembly owner)
            {
                this.owner = owner;
            }

            #region IStrongNamePublicKeyInfo Members

            public bool PrivateKeyAvailable
            {
                get { return false; }
            }

            public IStrongNamePrivateKeyInfo PrivateKey
            {
                get { throw new NotSupportedException(); }
            }

            #endregion

            #region IStrongNameKeyInfo Members

            public StrongNameKeyInfoType InformationType
            {
                get { return StrongNameKeyInfoType.Public; }
            }

            public int KeySize
            {
                get { throw new NotImplementedException(); }
            }

            public void WriteTo(string filename)
            {
                throw new NotImplementedException();
            }

            #endregion

            #region IStrongNamePublicKeyInfo Members

            public PublicKeyTokenData PublicToken
            {
                get {
                    if (this.publicToken == null)
                    {
                        var tokenData = this.owner.PublicKeyToken;
                        this.publicToken = new PublicKeyTokenData(tokenData[0], tokenData[1], tokenData[2], tokenData[3], tokenData[4], tokenData[5], tokenData[6], tokenData[7]);
                    }
                    return this.publicToken.Value;
                }
            }

            #endregion
        }
    }
}
