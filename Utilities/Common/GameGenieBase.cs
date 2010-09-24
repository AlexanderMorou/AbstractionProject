using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Utilities.Common
{
    /// <summary>
    /// Provides a simplistic Game-genie base transformer which
    /// goes to->from the code as needed.
    /// </summary>
    [CLSCompliant(false)]
    public class GameGenieBase :
        NumericBase
    {
        private static Controller controller = new Controller();

        public GameGenieBase()
            : base(controller)
        {
        }
        public GameGenieBase(ulong value)
            : base(controller, value)
        {
        }
        public GameGenieBase(string value)
            : base(controller, value)
        {
        }
        private class Controller :
            NumericBaseController
        {
            public Controller()
                : base("APZLGITYEOXUKSVN")
            {

            }
        }

        protected override NumericBase GetNew(ulong value)
        {
            return new GameGenieBase(value);
        }
    }
}
