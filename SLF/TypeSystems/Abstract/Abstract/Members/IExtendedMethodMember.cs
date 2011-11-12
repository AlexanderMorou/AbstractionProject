using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Abstract.Members
{
    public interface IExtendedMethodMember :
        IExtendedInstanceMember,
        IMethodMember
    {
        /// <summary>
        /// Returns whether the <see cref="IExtendedMethodMember"/> is
        /// asynchronous.
        /// </summary>
        bool IsAsynchronous { get; }
        /// <summary>
        /// Returns the <see cref="ExtendedMethodMemberFlags"/> that determine how the
        /// <see cref="IExtendedMethodMember"/> is shown in its scope and inherited scopes.
        /// </summary>
        new ExtendedMethodMemberFlags InstanceFlags { get; }
    }
}
