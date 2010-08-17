using System;
using System.Collections.Generic;
using System.Text;
/*----------------------------------------\
| Copyright © 2009 Allen Copeland Jr.     |
|-----------------------------------------|
| The Abstraction Project's code is prov- |
| -ided under a contract-release basis.   |
| DO NOT DISTRIBUTE and do not use beyond |
| the contract terms.                     |
\--------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Abstract.Members
{
    /// <summary>
    /// Determines the direction a parameter is coerced.
    /// </summary>
    public enum ParameterDirection
    {
        /// <summary>
        /// The parameter is not coerced in any manner, 
        /// it is merely sent to the method in question.
        /// </summary>
        In,
        /// <summary>
        /// The parameter is sent by address and will 
        /// be altered in the course of the call's 
        /// execution.
        /// </summary>
        Out,
        /// <summary>
        /// The parameter is sent by address and may 
        /// be changed by the call's execution, but 
        /// not necessarily in all cases.
        /// </summary>
        Reference,
    }
}
