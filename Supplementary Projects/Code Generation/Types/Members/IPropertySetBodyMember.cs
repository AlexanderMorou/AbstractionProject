using System;
using System.Collections.Generic;
using System.Text;
using System.CodeDom;
using AllenCopeland.Abstraction.OldCodeGen.Expression;
namespace AllenCopeland.Abstraction.OldCodeGen.Types.Members
{
    public interface IPropertySetBodyMember :
        IPropertyBodyMember
    {
        IPropertySetValueReferenceExpression ValueLocal { get; }
    }
}
