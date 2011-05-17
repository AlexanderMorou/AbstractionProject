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
    public class SexadecimalBase :
        NumericBase
    {
        private static Controller controller = new Controller();
        public SexadecimalBase()
            : base(controller)
        {

        }
        public SexadecimalBase(ulong value)
            : base(controller, value)
        {
        }
        public SexadecimalBase(string value)
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

        protected override NumericBase GetNew(ulong value)
        {
            return new SexadecimalBase(value);
        }

        public static implicit operator SexadecimalBase(ulong value)
        {
            return new SexadecimalBase(value);
        }
    }
}
