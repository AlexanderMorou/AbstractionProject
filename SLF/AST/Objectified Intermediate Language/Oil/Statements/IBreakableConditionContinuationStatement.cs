using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Oil.Statements
{
    /// <summary>
    /// Defines properties and methods for working with a
    /// condition continuation statement contained within a 
    /// breakable block of code.
    /// </summary>
    public interface IBreakableConditionContinuationStatement :
        IBreakableBlockStatement,
        IConditionContinuationStatement
    {
    }
}
