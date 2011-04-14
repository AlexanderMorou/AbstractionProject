using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract.Modules;

namespace AllenCopeland.Abstraction.Slf.Abstract
{
    public interface ITopLevelFieldMember :
        IFieldMember<ITopLevelFieldMember, INamespaceParent>
    {
        /// <summary>
        /// Returns the <see cref="IModule"/> which declares the 
        /// <see cref="ITopLevelMethodMember"/>.
        /// </summary>
        IModule DeclaringModule { get; }
    }
}
