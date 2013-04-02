using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.OldCodeGen.Expression;

namespace AllenCopeland.Abstraction.OldCodeGen.Comments
{
    /// <summary>
    /// Defines properties and methods for working with a reference comment that refers to a 
    /// field.
    /// </summary>
    public interface IFieldReferenceComment :
        IMemberReferenceComment<IFieldReferenceExpression>
    {
    }
}
