using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Compilers
{
    /// <summary>
    /// Defines properties and methods for working with a message, from the
    /// compiler, relative to an element, from the object-model, that defines
    /// the structure of general purpose code.
    /// </summary>
    public interface ICompilerModelMessage :
        ICompilerMessage
    {
        /// <summary>
        /// Returns the <see cref="String"/> associated to the 
        /// <see cref="ICompilerModelMessage"/>.
        /// </summary>
        string Message { get; }
    }
}
