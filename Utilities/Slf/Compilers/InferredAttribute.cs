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

namespace AllenCopeland.Abstraction.Slf.Compilers
{
    /// <summary>
    /// Provides an attribute that designates a generic parameter as
    /// inferred by the compiler tools that observe the generic 
    /// parameter.
    /// </summary>
    /// <remarks>Used in type-parameter reduction practices
    /// where the type-parameter is emitted by compilers which
    /// have to do so in order to relate functional aspects
    /// of dynamic parameter types, such as a return type of a method.</remarks>
    [AttributeUsage(AttributeTargets.GenericParameter)]
    public class InferredAttribute :
        Attribute
    {
    }
}
