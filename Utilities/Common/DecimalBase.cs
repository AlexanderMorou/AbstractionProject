using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Utilities.Common
{
    public class DecimalBase :
        NumericBase
    {
        private static Controller controller = new Controller();
        public DecimalBase()
            : base(controller)
        {

        }
        public DecimalBase(ulong value)
            : base(controller, value)
        {
        }
        public DecimalBase(string value)
            : base(controller, value)
        {
        }

        protected override NumericBase GetNew(ulong value)
        {
            return new DuosexagesimalBase(value);
        }

        public static implicit operator DecimalBase(ulong value)
        {
            return new DecimalBase(value);
        }

        private class Controller :
            NumericBaseController
        {
            public Controller()
                : base("0123456789")
            {
            }
        }

    }
}
