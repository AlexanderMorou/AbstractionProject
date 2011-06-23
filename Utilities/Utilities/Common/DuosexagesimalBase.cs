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
    public class DuosexagesimalBase :
        NumericBase
    {
        private static Controller controller = new Controller();
        public DuosexagesimalBase()
            : base(controller)
        {

        }
        public DuosexagesimalBase(BigInteger value)
            : base(controller, value)
        {
        }
        public DuosexagesimalBase(string value)
            : base(controller, value)
        {
        }

        protected override NumericBase GetNew(BigInteger value)
        {
            return new DuosexagesimalBase(value);
        }

        public static implicit operator DuosexagesimalBase(BigInteger value)
        {
            return new DuosexagesimalBase(value);
        }

        private class Controller :
            NumericBaseController
        {
            public Controller()
                : base("0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz")
            {
            }
        }

    }
}
