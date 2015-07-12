using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// Defines properties and methods for working with a unique identifier
    /// which is equatable to another.
    /// </summary>
    public interface IGeneralDeclarationUniqueIdentifier :
        IDeclarationUniqueIdentifier,
        IEquatable<IGeneralDeclarationUniqueIdentifier>
    {
    }
}
