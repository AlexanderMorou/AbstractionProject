using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    partial class CliCommon
    {
        /// <summary>
        /// Defines a series of constant string values which denote the
        /// names of the methods used to represent binary operator 
        /// overloads.
        /// </summary>
        public static class BinaryOperatorNames
        {
            /// <summary>
            /// The name used to determine the binary addition operator
            /// for overloading.
            /// </summary>
            public const string Addition = "op_Addition";
            /// <summary>
            /// The name used to determine the binary subtraction operator
            /// for overloading.
            /// </summary>
            public const string Subtraction = "op_Subtraction";
            /// <summary>
            /// The name used to determine the binary multiplication operator
            /// for overloading.
            /// </summary>
            public const string Multiply = "op_Multiply";
            /// <summary>
            /// The name used to determine the binary division operator
            /// for overloading.
            /// </summary>
            public const string Division = "op_Division";
            /// <summary>
            /// The name used to determine the binary modulus operator
            /// for overloading.
            /// </summary>
            public const string Modulus = "op_Modulus";
            /// <summary>
            /// The name used to determine the binary bitwise intersection operator
            /// for overloading.
            /// </summary>
            public const string BitwiseAnd = "op_BitwiseAnd";
            /// <summary>
            /// The name used to determine the binary bitwise inclusion operator
            /// for overloading.
            /// </summary>
            public const string BitwiseOr = "op_BitwiseOr";
            /// <summary>
            /// The name used to determine the binary bitwise exclusivity operator
            /// for overloading.
            /// </summary>
            public const string ExclusiveOr = "op_ExclusiveOr";
            /// <summary>
            /// The name used to determine the binary bitwise left shift operator
            /// for overloading.
            /// </summary>
            public const string LeftShift = "op_LeftShift";
            /// <summary>
            /// The name used to determine the binary bitwise right shift operator
            /// for overloading.
            /// </summary>
            public const string RightShift = "op_RightShift";
            /// <summary>
            /// The name used to determine the binary equality operator
            /// for overloading.
            /// </summary>
            public const string Equality = "op_Equality";
            /// <summary>
            /// The name used to determine the binary inequality operator
            /// for overloading.
            /// </summary>
            public const string Inequality = "op_Inequality";
            /// <summary>
            /// The name used to determine the binary relational less than operator
            /// for overloading.
            /// </summary>
            public const string LessThan = "op_LessThan";
            /// <summary>
            /// The name used to determine the binary relational greater than operator
            /// for overloading.
            /// </summary>
            public const string GreaterThan = "op_GreaterThan";
            /// <summary>
            /// The name used to determine the binary relational less than or equal to operator
            /// for overloading.
            /// </summary>
            public const string LessThanOrEqual = "op_LessThanOrEqual";
            /// <summary>
            /// The name used to determine the binary relational greater than or equal to operator
            /// for overloading.
            /// </summary>
            public const string GreaterThanOrEqual = "op_GreaterThanOrEqual";
        }
    }
}
