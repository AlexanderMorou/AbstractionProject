using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Oil.Modules;
using AllenCopeland.Abstraction.Slf.Oil.Members;

namespace AllenCopeland.Abstraction.Slf.Oil
{
    /// <summary>
    /// Defines properties and methods for working with an intermediate 
    /// top-level method.
    /// </summary>
    public interface IIntermediateTopLevelMethodMember :
        IIntermediateMethodMember<ITopLevelMethodMember, IIntermediateTopLevelMethodMember, INamespaceParent, IIntermediateNamespaceParent>,
        ITopLevelMethodMember
    {
        /// <summary>
        /// Returns/sets the <see cref="IIntermediateModule"/> in which the 
        /// <see cref="IIntermediateTopLevelMethodMember"/> should be declared.
        /// </summary>
        new IIntermediateModule DeclaringModule { get; set; }
    }
}
