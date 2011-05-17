using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Utilities.Common
{
    public class OctodecimalBase :
        NumericBase
    {
        private static Controller controller = new Controller();
        public OctodecimalBase()
            : base(controller)
        {

        }
        public OctodecimalBase(ulong value)
            : base(controller, value)
        {
        }
        private class Controller :
            NumericBaseController
        {
            public Controller()
                : base("0123456789ABCDEFGH", false)
            {
            }
        }

        protected override NumericBase GetNew(ulong value)
        {
            return new OctodecimalBase(value);
        }

        public static implicit operator OctodecimalBase(ulong value)
        {
            return new OctodecimalBase(value);
        }
    }
}
