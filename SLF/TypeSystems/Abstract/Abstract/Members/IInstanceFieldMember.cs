using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Abstract.Members
{
    public interface IInstanceFieldMember :
        IInstanceMember,
        IFieldMember
    {
        /// <summary>
        /// Returns the <see cref="InstanceFieldMemberAttributes"/> that determine how the
        /// <see cref="IInstanceFieldMember"/> is shown in its scope and inherited scopes.
        /// </summary>
        InstanceFieldMemberAttributes Attributes { get; }
    }
}
