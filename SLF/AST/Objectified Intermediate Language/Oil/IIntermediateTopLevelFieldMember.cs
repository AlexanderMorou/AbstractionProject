using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Oil.Members;
using AllenCopeland.Abstraction.Slf.Oil.Modules;

namespace AllenCopeland.Abstraction.Slf.Oil
{
    public interface IIntermediateTopLevelFieldMember :
        IIntermediateFieldMember<ITopLevelFieldMember, IIntermediateTopLevelFieldMember, INamespaceParent, IIntermediateNamespaceParent>,
        ITopLevelFieldMember
    {
        /// <summary>
        /// Returns/sets the <see cref="IIntermediateModule"/> in which the 
        /// <see cref="IIntermediateTopLevelFieldMember"/> should be declared.
        /// </summary>
        IIntermediateModule DeclaringModule { get; set; }
    }
}
