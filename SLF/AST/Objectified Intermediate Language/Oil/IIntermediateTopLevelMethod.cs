using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Oil.Modules;

namespace AllenCopeland.Abstraction.Slf.Oil
{
    /// <summary>
    /// Defines properties and methods for working with an intermediate 
    /// top-level method.
    /// </summary>
    public interface IIntermediateTopLevelMethod :
        ITopLevelMethod
    {
        /// <summary>
        /// Returns/sets the <see cref="IIntermediateModule"/> in which the 
        /// <see cref="IIntermediateTopLevelMethod"/> should be declared.
        /// </summary>
        IIntermediateModule DeclaringModule { get; set; }
    }
}
