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
    [CLSCompliant(false)]
    public class CharacterBase :
        NumericBase
    {
        private static Controller controller = new Controller();
        public CharacterBase()
            : base(controller)
        {

        }

        public CharacterBase(ulong value)
            : base(controller, value)
        {
        }
        public CharacterBase(string value)
            : base(controller, value)
        {
        }
        private class Controller :
            NumericBaseController
        {
            public Controller()
                : base(GetCharRange(), true)
            {
            }

            private static IEnumerable<char> GetCharRange()
            {
                char[] k = new char[char.MaxValue+1];
                for (int i = 0; i < k.Length; i++)
                    k[i] = (char)i;
                return k;
            }
        }

        protected override NumericBase GetNew(ulong value)
        {
            return new CharacterBase(value);
        }

        public static implicit operator CharacterBase(ulong value)
        {
            return new CharacterBase(value);
        }
    }
}
