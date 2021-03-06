using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.OldCodeGen.Types.Members;
using AllenCopeland.Abstraction.OldCodeGen.Expression;

namespace AllenCopeland.Abstraction.OldCodeGen.Types
{
    public interface IFieldParentType :
        IDeclaredType
    {
        /// <summary>
        /// Returns the fields defined on the <see cref="IFieldParentType"/>.
        /// </summary>
        IFieldMembersBase Fields { get; }
        /// <summary>
        /// Returns the number of members contained within the <see cref="IFieldParentType"/>.
        /// </summary>
        /// <returns>A <see cref="System.Int32"/> relating to the number of members within
        /// the <see cref="IFieldParentType"/></returns>
        int GetMemberCount();
    }
}
