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

namespace AllenCopeland.Abstraction.Utilities.Miscellaneous
{
    public class SexatrigesimalBase :
        NumericBase
    {
        private static Controller controller = new Controller();
        public SexatrigesimalBase()
            : base(controller)
        {

        }
        public SexatrigesimalBase(BigInteger value)
            : base(controller, value)
        {
        }
        public SexatrigesimalBase(string value)
            : base(controller, value)
        {
        }

        protected override NumericBase GetNew(BigInteger value)
        {
            return new SexatrigesimalBase(value);
        }

        public static implicit operator SexatrigesimalBase(BigInteger value)
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
