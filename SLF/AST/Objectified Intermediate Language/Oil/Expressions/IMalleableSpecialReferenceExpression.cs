using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2011 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

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
