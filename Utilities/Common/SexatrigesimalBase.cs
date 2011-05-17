using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Utilities.Common
{
    public class SexatrigesimalBase :
        NumericBase
    {
        private static Controller controller = new Controller();
        public SexatrigesimalBase()
            : base(controller)
        {

        }
        public SexatrigesimalBase(ulong value)
            : base(controller, value)
        {
        }
        public SexatrigesimalBase(string value)
            : base(controller, value)
        {
        }

        protected override NumericBase GetNew(ulong value)
        {
            return new SexatrigesimalBase(value);
        }

        public static implicit operator SexatrigesimalBase(ulong value)
        {
            return new SexatrigesimalBase(value);
        }

        private class Controller :
            NumericBaseController
        {
            public Controller()
                : base("0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ", false)
            {
            }
        }

    }
}
