using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Abstract.Members
{
    /// <summary>
    /// Defines properties and methods for working with a member, which contains
    /// a signature, identifier that differentiates it from its siblings. 
    /// </summary>
	public interface IGeneralSignatureMemberUniqueIdentifier :
        ISignatureMemberUniqueIdentifier,
        IGeneralMemberUniqueIdentifier,
        IEquatable<IGeneralSignatureMemberUniqueIdentifier>
	{
	}
}
