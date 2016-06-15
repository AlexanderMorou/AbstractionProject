using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace AllenCopeland.Abstraction.Utilities.Security
{
    [StructLayout(LayoutKind.Sequential, Size = 8, Pack = 1)]
    public struct PublicKeyTokenData :
        IEnumerable<byte>
    {
        private byte a, b, c, d, e, f, g, h;

        public PublicKeyTokenData(byte a, byte b, byte c, byte d, byte e, byte f, byte g, byte h)
        {
            this.a = a;
            this.b = b;
            this.c = c;
            this.d = d;
            this.e = e;
            this.f = f;
            this.g = g;
            this.h = h;
        }

        public byte[] Token {
            get
            {
                byte[] result = new byte[8];
                result[0] = this.a;
                result[1] = this.b;
                result[2] = this.c;
                result[3] = this.d;
                result[4] = this.e;
                result[5] = this.f;
                result[6] = this.g;
                result[7] = this.h;
                return result;
            }
        }

        public IEnumerator<byte> GetEnumerator()
        {
            yield return this.a;
            yield return this.b;
            yield return this.c;
            yield return this.d;
            yield return this.e;
            yield return this.f;
            yield return this.g;
            yield return this.h;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0:X2}", this.a);
            sb.AppendFormat("{0:X2}", this.b);
            sb.AppendFormat("{0:X2}", this.c);
            sb.AppendFormat("{0:X2}", this.d);
            sb.AppendFormat("{0:X2}", this.e);
            sb.AppendFormat("{0:X2}", this.f);
            sb.AppendFormat("{0:X2}", this.g);
            sb.AppendFormat("{0:X2}", this.h);
            return sb.ToString();
        }

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion
    }
}
