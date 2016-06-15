using AllenCopeland.Abstraction._Internal.Utilities.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Utilities.Security
{
    public static class PublicPrivateKeyExtensions
    {
        /// <summary>Creates and loads a new <see cref="IStrongNamePublicKeyInfo"/> which contains the <paramref name=""/></summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static IStrongNamePublicKeyInfo GetPublicKeyInfo(Stream data)
        {
            return (IStrongNamePublicKeyInfo)StrongNameKeyPairHelper.LoadStrongNameKeyData(data, false);
        }

        public static IStrongNamePrivateKeyInfo GetPrivateKeyInfo(Stream data)
        {
            return (IStrongNamePrivateKeyInfo)StrongNameKeyPairHelper.LoadStrongNameKeyData(data, true);
        }

        public static IStrongNamePrivateKeyInfo CreateNewKey(int keySize)
        {
            return new StrongNamePrivateKeyInfo(keySize);
        }
    }
}
