using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Abstract.Members
{
    /// <summary>
    /// Defines properties and methods for working with a member
    /// unique identifier which specifies the name of a member and
    /// differentiates it between other kinds of declarations.
    /// </summary>
    public interface IGeneralMemberUniqueIdentifier :
        IMemberUniqueIdentifier<IGeneralMemberUniqueIdentifier>,
        IGeneralDeclarationUniqueIdentifier
    {
    }
}
