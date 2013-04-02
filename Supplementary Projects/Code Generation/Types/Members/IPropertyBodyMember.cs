using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.OldCodeGen.Statements;

namespace AllenCopeland.Abstraction.OldCodeGen.Types.Members
{
    public interface IPropertyBodyMember :
        IBlockParent,
        IStatementBlockInsertBase,
        IAttributeDeclarationTarget
    {
        /// <summary>
        /// Returns the property that the body belongs to.
        /// </summary>
        new IPropertyMember ParentTarget { get;set; }
        PropertyBodyMemberPart Part { get; }
    }
}
