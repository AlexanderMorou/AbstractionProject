using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Numerics
{
    public class OctodecimalBase :
        NumericBase
    {
        private static Controller controller = new Controller();
        public OctodecimalBase()
            : base(controller)
        {

        }
        public OctodecimalBase(BigInteger value)
            : base(controller, value)
        {
        }

        public OctodecimalBase(string value)
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

        protected override NumericBase GetNew(BigInteger value)
        {
            return new OctodecimalBase(value);
        }

        public static implicit operator OctodecimalBase(BigInteger value)
        {
            return new OctodecimalBase(value);
        }
    }
}
