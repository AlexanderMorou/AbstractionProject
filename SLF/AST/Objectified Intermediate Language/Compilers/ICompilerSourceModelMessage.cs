using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Compilers
{
    public interface ICompilerSourceModelMessage :
        ICompilerModelMessage,
        ICompilerSourceMessage
    {
        /// <summary>
        /// Returns the <see cref="String"/> associated to the 
        /// <see cref="ICompilerSourceModelMessage"/>.
        /// </summary>
        new string Message { get; }
    }
}
