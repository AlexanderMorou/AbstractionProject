using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Utilities.Common
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
        public HexadecimalBase(ulong value)
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
                : base("0123456789ABCDEF")
            {
            }
        }

        protected override NumericBase GetNew(ulong value)
        {
            return new HexadecimalBase(value);
        }

        public static implicit operator HexadecimalBase(ulong value)
        {
            return new HexadecimalBase(value);
        }
    }
}
