using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract.Modules;

namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// Defines properties and methods for working with a top-level method
    /// defined at a top level (outside of a class.)
    /// </summary>
    public interface ITopLevelMethod :
        IMethodMember<ITopLevelMethod, INamespaceParent>
    {
        /// <summary>
        /// Returns the <see cref="IModule"/> which declares the 
        /// <see cref="ITopLevelMethod"/>.
        /// </summary>
        IModule DeclaringModule { get; }
    }
}
