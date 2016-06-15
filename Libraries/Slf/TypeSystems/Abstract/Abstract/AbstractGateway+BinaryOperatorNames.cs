using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Abstract
{
    partial class AbstractGateway
    {
        /// <summary>
        /// Defines a series of constant string values which denote the
        /// names of the methods used to represent binary operator 
        /// overloads.
        /// </summary>
        internal static class BinaryOperatorNames
        {
            /// <summary>
            /// The name used to determine the binary addition operator
            /// for overloading.
            /// </summary>
            public const string Addition = "a + b operator";
            /// <summary>
            /// The name used to determine the binary subtraction operator
            /// for overloading.
            /// </summary>
            public const string Subtraction = "a - b operator";
            /// <summary>
            /// The name used to determine the binary multiplication operator
            /// for overloading.
            /// </summary>
            public const string Multiply = "a * b operator";
            /// <summary>
            /// The name used to determine the binary division operator
            /// for overloading.
            /// </summary>
            public const string Division = "a / b operator";
            /// <summary>
            /// The name used to determine the binary modulus operator
            /// for overloading.
            /// </summary>
            public const string Modulus = "a % b operator";
            /// <summary>
            /// The name used to determine the binary bitwise intersection operator
            /// for overloading.
            /// </summary>
            public const string BitwiseAnd = "a & b operator";
            /// <summary>
            /// The name used to determine the binary bitwise inclusion operator
            /// for overloading.
            /// </summary>
            public const string BitwiseOr = "a | b operator";
            /// <summary>
            /// The name used to determine the binary bitwise exclusivity operator
            /// for overloading.
            /// </summary>
            public const string ExclusiveOr = "a ^ b operator";
            /// <summary>
            /// The name used to determine the binary bitwise left shift operator
            /// for overloading.
            /// </summary>
            public const string LeftShift = "a << b operator";
            /// <summary>
            /// The name used to determine the binary bitwise right shift operator
            /// for overloading.
            /// </summary>
            public const string RightShift = "a >> b operator";
            /// <summary>
            /// The name used to determine the binary equality operator
            /// for overloading.
            /// </summary>
            public const string Equality = "a == b operator";
            /// <summary>
            /// The name used to determine the binary inequality operator
            /// for overloading.
            /// </summary>
            public const string Inequality = "a != b operator";
            /// <summary>
            /// The name used to determine the binary relational less than operator
            /// for overloading.
            /// </summary>
            public const string LessThan = "a < b operator";
            /// <summary>
            /// The name used to determine the binary relational greater than operator
            /// for overloading.
            /// </summary>
            public const string GreaterThan = "a > b operator";
            /// <summary>
            /// The name used to determine the binary relational less than or equal to operator
            /// for overloading.
            /// </summary>
            public const string LessThanOrEqual = "a <= b operator";
            /// <summary>
            /// The name used to determine the binary relational greater than or equal to operator
            /// for overloading.
            /// </summary>
            public const string GreaterThanOrEqual = "a >= b operator";
        }

    }
}
