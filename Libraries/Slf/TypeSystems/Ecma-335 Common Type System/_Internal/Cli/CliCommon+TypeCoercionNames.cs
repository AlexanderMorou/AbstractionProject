﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
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
        /// names of the methods used to represent type coercion overloads.
        /// </summary>
        public static class TypeCoercionNames
        {
            /// <summary>
            /// The name used to define implicit type coercions.
            /// </summary>
            public const string Implicit = "op_Implicit";
            /// <summary>
            /// The name used to define explicit type coercions.
            /// </summary>
            public const string Explicit = "op_Explicit";
        }
    }
}
