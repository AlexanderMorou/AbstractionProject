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
    /// Provides a base 62 handler for numbers which use the character
    /// set 0-9, A-Z and a-z.
    /// </summary>
    public class DuosexagesimalBase :
        NumericBase
    {
        private static Controller controller = new Controller();
        /// <summary>
        /// Creates a new <see cref="DuosexagesimalBase"/> initialized to its default
        /// state (a value of zero).
        /// </summary>
        public DuosexagesimalBase()
            : base(controller)
        {
        }

        /// <summary>
        /// Creates a new <see cref="DuosexagesimalBase"/> initialized with the <paramref name="value"/>
        /// provided.
        /// </summary>
        /// <param name="value">The <see cref="BigInteger"/> value which denotes
        /// the intrinsic value of the base-62 number.</param>
        public DuosexagesimalBase(BigInteger value)
            : base(controller, value)
        {
        }
        /// <summary>
        /// Creates a new <see cref="DuosexagesimalBase"/> value which denotes the
        /// <paramref name="value"/> to parse in base-62.
        /// </summary>
        /// <param name="value">The base-62 number as a <see cref="String"/> to parse.</param>
        public DuosexagesimalBase(string value)
            : base(controller, value)
        {
        }

        /// <summary>
        /// Obtains a new <see cref="DuosexagesimalBase"/> with the intrinsic 
        /// <paramref name="value"/> provided.
        /// </summary>
        /// <param name="value">The <see cref="BigInteger"/> value used to 
        /// represent the new instance.</param>
        /// <returns>A new <see cref="DuosexagesimalBase"/> instance.</returns>
        protected override NumericBase GetNew(BigInteger value)
        {
            return new DuosexagesimalBase(value);
        }

        /// <summary>
        /// Converts the <see cref="BigInteger"/> <paramref name="value"/> 
        /// into a <see cref="DuosexagesimalBase"/> instance.
        /// </summary>
        /// <param name="value">The <see cref="BigInteger"/> value that represents
        /// the intrinsic value of the new instance.</param>
        /// <returns>A new <see cref="DuosexagesimalBase"/> instance.</returns>
        public static implicit operator DuosexagesimalBase(BigInteger value)
        {
            return new DuosexagesimalBase(value);
        }

        /// <summary>
        /// Implicitly converts the <see cref="String"/>  value into a 
        /// <see cref="DuosexagesimalBase"/> numeric value.
        /// </summary>
        /// <param name="value">The <see cref="String"/> value which represents
        /// a number in base-62.</param>
        /// <returns>A <see cref="DuosexagesimalBase"/> instance
        /// which has parsed the <paramref name="value"/>
        /// provided.</returns>
        public static implicit operator DuosexagesimalBase(string value)
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
