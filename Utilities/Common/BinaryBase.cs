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
    public class BinaryBase :
        NumericBase
    {
        private static Controller controller = new Controller();
        public BinaryBase()
            : base(controller)
        {

        }
        public BinaryBase(ulong value)
            : base(controller, value)
        {
        }
        private class Controller :
            NumericBaseController
        {
            public Controller()
                : base("01")
            {
            }
        }

        protected override NumericBase GetNew(ulong value)
        {
            return new BinaryBase(value);
        }

        public static implicit operator BinaryBase(ulong value)
        {
            return new BinaryBase(value);
        }
    }
}
