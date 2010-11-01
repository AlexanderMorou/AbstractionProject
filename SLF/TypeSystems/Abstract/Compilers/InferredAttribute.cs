using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
