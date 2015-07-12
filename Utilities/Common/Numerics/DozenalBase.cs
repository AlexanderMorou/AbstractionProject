using AllenCopeland.Abstraction.Numerics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Numerics
{
    class DozenalBase :
        NumericBase
    {
        private static Controller controller = new Controller();

        public DozenalBase()
            : base(controller)
        {

        }

        public DozenalBase(BigInteger value)
            : base(controller, value)
        {
        }
        public DozenalBase(string value)
            : base(controller, value)
        {
        }
        private class Controller :
            NumericBaseController
        {
            internal Controller()
                : base("0123456789XE", false)
            {

            }

        }

        protected override NumericBase GetNew(BigInteger value)
        {
            return new DozenalBase(value);
        }
    }
}
