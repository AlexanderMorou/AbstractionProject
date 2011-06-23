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
    public class SeptemvigesimalBase :
        NumericBase
    {
        private static Controller controller = new Controller();
        public SeptemvigesimalBase()
            : base(controller)
        {

        }
        public SeptemvigesimalBase(BigInteger value)
            : base(controller, value)
        {
        }
        public SeptemvigesimalBase(string value)
            : base(controller, value)
        {
        }

        protected override NumericBase GetNew(BigInteger value)
        {
            return new SeptemvigesimalBase(value);
        }

        public static implicit operator SeptemvigesimalBase(BigInteger value)
        {
            return new SeptemvigesimalBase(value);
        }

        private class Controller :
            NumericBaseController
        {
            public Controller()
                : base("0123456789ABCDEFGHIJKLMNOPQ", false)
            {
            }
        }

    }
}
