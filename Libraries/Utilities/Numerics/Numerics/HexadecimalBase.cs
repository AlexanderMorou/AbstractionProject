using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace AllenCopeland.Abstraction.Numerics
{
    [CLSCompliant(false)]
    public class HexadecimalBase :
        NumericBase
    {
        private static Controller controller = new Controller();
        public HexadecimalBase()
            : base(controller)
        {

        }
        public HexadecimalBase(BigInteger value)
            : base(controller, value)
        {
        }
        public HexadecimalBase(string value)
            : base(controller, value)
        {
        }
        private class Controller :
            NumericBaseController
        {
            public Controller()
                : base("0123456789ABCDEF", false)
            {
            }
        }

        protected override NumericBase GetNew(BigInteger value)
        {
            return new HexadecimalBase(value);
        }

        public static implicit operator HexadecimalBase(BigInteger value)
        {
            return new HexadecimalBase(value);
        }
    }
}
