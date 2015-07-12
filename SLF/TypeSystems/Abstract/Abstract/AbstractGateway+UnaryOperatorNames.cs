using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
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
        /// names of the methods used to represent unary operator overloads.
        /// </summary>
        public static class UnaryOperatorNames
        {
            /// <summary>
            /// The name used to determine the unary plus operator
            /// for overload.
            /// </summary>
            public const string Plus           = "+ operator";
            /// <summary>
            /// The name used to determine the unary negation operator
            /// for overload.
            /// </summary>
            public const string Negation       = "- operator";
            /// <summary>
            /// The name used to determine the unary false operator
            /// for overload.
            /// </summary>
            public const string False          = "false operator";
            /// <summary>
            /// The name used to determine the unary true operator
            /// for overload.
            /// </summary>
            public const string True           = "true operator";
            /// <summary>
            /// The name used to determine the unary logical not operator
            /// for overload.
            /// </summary>
            public const string LogicalNot     = "! operator";
            /// <summary>
            /// The name used to determine the unary bitwise inversion operator
            /// for overload.
            /// </summary>
            public const string OnesComplement = "~ operator";
            /// <summary>
            /// The name used to determine the unary value increment operator
            /// for overload.
            /// </summary>
            public const string Increment      = "++ operator";
            /// <summary>
            /// The name used to determine the unary value decrement operator
            /// for overload.
            /// </summary>
            public const string Decrement      = "-- operator";
        }
    }
}
