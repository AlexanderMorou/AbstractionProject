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
    /// <summary>
    /// Provides a base-27 handler for numbers which use the character
    /// set 0-9 and A-Q.
    /// </summary>
    public class SeptemvigesimalBase :
        NumericBase
    {
        private static Controller controller = new Controller();
        /// <summary>
        /// Creates a new <see cref="SeptemvigesimalBase"/> initialized to its default
        /// state (a value of zero).
        /// </summary>
        public SeptemvigesimalBase()
            : base(controller)
        {

        }
        /// <summary>
        /// Creates a new <see cref="SeptemvigesimalBase"/> initialized with the <paramref name="value"/>
        /// provided.
        /// </summary>
        /// <param name="value">The <see cref="BigInteger"/> value which denotes
        /// the intrinsic value of the base-27 number.</param>
        public SeptemvigesimalBase(BigInteger value)
            : base(controller, value)
        {
        }

        /// <summary>
        /// Creates a new <see cref="SeptemvigesimalBase"/> value which denotes the
        /// <paramref name="value"/> to parse in base-62.
        /// </summary>
        /// <param name="value">The base-27 number as a <see cref="String"/> to parse.</param>
        public SeptemvigesimalBase(string value)
            : base(controller, value)
        {
        }

        /// <summary>
        /// Obtains a new <see cref="SeptemvigesimalBase"/> with the intrinsic 
        /// <paramref name="value"/> provided.
        /// </summary>
        /// <param name="value">The <see cref="BigInteger"/> value used to 
        /// represent the new instance.</param>
        /// <returns>A new <see cref="SeptemvigesimalBase"/> instance.</returns>
        protected override NumericBase GetNew(BigInteger value)
        {
            return new SeptemvigesimalBase(value);
        }

        /// <summary>
        /// Converts the <see cref="BigInteger"/> <paramref name="value"/> 
        /// into a <see cref="SeptemvigesimalBase"/> instance.
        /// </summary>
        /// <param name="value">The <see cref="BigInteger"/> value that represents
        /// the intrinsic value of the new instance.</param>
        /// <returns>A new <see cref="SeptemvigesimalBase"/> instance.</returns>
        public static implicit operator SeptemvigesimalBase(BigInteger value)
        {
            return new SeptemvigesimalBase(value);
        }

        /// <summary>
        /// Implicitly converts the <see cref="String"/>  value into a 
        /// <see cref="SeptemvigesimalBase"/> numeric value.
        /// </summary>
        /// <param name="value">The <see cref="String"/> value which represents
        /// a number in base-62.</param>
        /// <returns>A <see cref="SeptemvigesimalBase"/> instance
        /// which has parsed the <paramref name="value"/>
        /// provided.</returns>
        public static implicit operator SeptemvigesimalBase(string value)
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
