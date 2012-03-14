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

namespace AllenCopeland.Abstraction.Slf.Cli
{
    partial class CliCommon
    {
        /// <summary>
        /// Defines a series of constant string values which denote the
        /// names of the methods used to represent unary operator overloads.
        /// </summary>
        public static class UnaryOperatorNames
        {
            /// <summary>
            /// The name used to determine the unary plus operator
            /// for overload.
            /// </summary>
            public const string Plus = "op_UnaryPlus";
            /// <summary>
            /// The name used to determine the unary negation operator
            /// for overload.
            /// </summary>
            public const string Negation = "op_UnaryNegation";
            /// <summary>
            /// The name used to determine the unary false operator
            /// for overload.
            /// </summary>
            public const string False = "op_False";
            /// <summary>
            /// The name used to determine the unary true operator
            /// for overload.
            /// </summary>
            public const string True = "op_True";
            /// <summary>
            /// The name used to determine the unary logical not operator
            /// for overload.
            /// </summary>
            public const string LogicalNot = "op_LogicalNot";
            /// <summary>
            /// The name used to determine the unary bitwise inversion operator
            /// for overload.
            /// </summary>
            public const string OnesComplement = "op_OnesComplement";
            /// <summary>
            /// The name used to determine the unary value increment operator
            /// for overload.
            /// </summary>
            public const string Increment = "op_Increment";
            /// <summary>
            /// The name used to determine the unary value decrement operator
            /// for overload.
            /// </summary>
            public const string Decrement = "op_Decrement";
        }
    }
}
