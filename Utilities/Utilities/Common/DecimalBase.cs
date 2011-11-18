using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
 /*---------------------------------------------------------------------\
 | Copyright © 2011 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

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
        public DecimalBase(BigInteger value)
            : base(controller, value)
        {
        }
        public DecimalBase(string value)
            : base(controller, value)
        {
        }

        protected override NumericBase GetNew(BigInteger value)
        {
            return new DecimalBase(value);
        }

        public static implicit operator DecimalBase(BigInteger value)
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
