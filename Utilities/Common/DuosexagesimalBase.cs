using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Utilities.Common
{
    public class DuosexagesimalBase :
        NumericBase
    {
        private static Controller controller = new Controller();
        public DuosexagesimalBase()
            : base(controller)
        {

        }
        public DuosexagesimalBase(ulong value)
            : base(controller, value)
        {
        }
        public DuosexagesimalBase(string value)
            : base(controller, value)
        {
        }

        protected override NumericBase GetNew(ulong value)
        {
            return new DuosexagesimalBase(value);
        }

        public static implicit operator DuosexagesimalBase(ulong value)
        {
            return new DuosexagesimalBase(value);
        }

        private class Controller :
            NumericBaseController
        {
            public Controller()
                : base("0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz")
            {
            }
        }

    }
}
