using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Abstract
{
    public interface IDeclarationUniqueIdentifier<TIdentifier> :
        IDeclarationUniqueIdentifier,
        IEquatable<TIdentifier>
        where TIdentifier :
            IDeclarationUniqueIdentifier<TIdentifier>
    {
    }
    public interface IDeclarationUniqueIdentifier
    {
        string Name { get; }
    }
}
