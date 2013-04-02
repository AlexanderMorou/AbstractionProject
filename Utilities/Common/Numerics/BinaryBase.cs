using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Numerics
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

        public BinaryBase(string value)
            : base(controller, value)
        {
        }

        public BinaryBase(BigInteger value)
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

        protected override NumericBase GetNew(BigInteger value)
        {
            return new BinaryBase(value);
        }

        public static implicit operator BinaryBase(BigInteger value)
        {
            return new BinaryBase(value);
        }
    }
}
