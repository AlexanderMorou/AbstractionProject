using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Numerics
{
    /// <summary>
    /// Provides a base 60 handler for numbers which use the character
    /// set 0-9, A-H, J-N, P-Z and a-z.
    /// </summary>
    public class SexagesimalBase :
        NumericBase
    {
        private static Controller controller = new Controller();
        /// <summary>
        /// Creates a new <see cref="SexagesimalBase"/> initialized to its default
        /// state (a value of zero).
        /// </summary>
        public SexagesimalBase()
            : base(controller)
        {
        }

        /// <summary>
        /// Creates a new <see cref="SexagesimalBase"/> initialized with the <paramref name="value"/>
        /// provided.
        /// </summary>
        /// <param name="value">The <see cref="BigInteger"/> value which denotes
        /// the intrinsic value of the base-60 number.</param>
        public SexagesimalBase(BigInteger value)
            : base(controller, value)
        {
        }
        /// <summary>
        /// Creates a new <see cref="SexagesimalBase"/> value which denotes the
        /// <paramref name="value"/> to parse in base-60.
        /// </summary>
        /// <param name="value">The base-60 number as a <see cref="String"/> to parse.</param>
        public SexagesimalBase(string value)
            : base(controller, value)
        {
        }

        /// <summary>
        /// Obtains a new <see cref="SexagesimalBase"/> with the intrinsic 
        /// <paramref name="value"/> provided.
        /// </summary>
        /// <param name="value">The <see cref="BigInteger"/> value used to 
        /// represent the new instance.</param>
        /// <returns>A new <see cref="SexagesimalBase"/> instance.</returns>
        protected override NumericBase GetNew(BigInteger value)
        {
            return new SexagesimalBase(value);
        }

        /// <summary>
        /// Converts the <see cref="BigInteger"/> <paramref name="value"/> 
        /// into a <see cref="SexagesimalBase"/> instance.
        /// </summary>
        /// <param name="value">The <see cref="BigInteger"/> value that represents
        /// the intrinsic value of the new instance.</param>
        /// <returns>A new <see cref="SexagesimalBase"/> instance.</returns>
        public static implicit operator SexagesimalBase(BigInteger value)
        {
            return new SexagesimalBase(value);
        }

        /// <summary>
        /// Implicitly converts the <see cref="String"/>  value into a 
        /// <see cref="SexagesimalBase"/> numeric value.
        /// </summary>
        /// <param name="value">The <see cref="String"/> value which represents
        /// a number in base-60.</param>
        /// <returns>A <see cref="SexagesimalBase"/> instance
        /// which has parsed the <paramref name="value"/>
        /// provided.</returns>
        public static implicit operator SexagesimalBase(string value)
        {
            return new SexagesimalBase(value);
        }

        private class Controller :
            NumericBaseController
        {
            public Controller()
                : base("0123456789ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz")
            {
            }
        }

    }
}
