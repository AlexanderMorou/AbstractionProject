using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.OldCodeGen.Expression;

namespace AllenCopeland.Abstraction.OldCodeGen.Types.Members
{
    /// <summary>
    /// Defines properties and methods for working with the source of the data for an 
    /// expression coercion for a unary operator.
    /// </summary>
    public interface IUnaryOperatorOverloadSource :
        IParameterReferenceExpression
    {
    }
}
