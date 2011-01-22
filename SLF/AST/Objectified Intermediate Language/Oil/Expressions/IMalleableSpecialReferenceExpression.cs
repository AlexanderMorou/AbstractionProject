using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    public interface IMalleableSpecialReferenceExpression : 
        ISpecialReferenceExpression
    {
        /// <summary>
        /// Returns the kind of special reference the reference is.
        /// </summary>
        new SpecialReferenceKind Kind { get; set; }
    }
}
