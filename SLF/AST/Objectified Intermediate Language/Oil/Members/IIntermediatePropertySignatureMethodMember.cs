using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;

namespace AllenCopeland.Abstraction.Slf.Oil.Members
{
    /// <summary>
    /// Defines properties and methods for working with the
    /// method of an intermediate property signature.
    /// </summary>
    public interface IIntermediatePropertySignatureMethodMember :
        IIntermediateMethodSignatureMember,
        IPropertySignatureMethodMember
    {
    }
}
