using AllenCopeland.Abstraction.Slf.Ast.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Expressions
{
    public interface IBoundLocalReferenceExpression :
        ILocalReferenceExpression,
        IBoundMemberReference
    {
        /// <summary>
        /// Returns the <see cref="ILocalMember"/> associated to the
        /// typed member reference.
        /// </summary>
        new ILocalMember Member { get; }
    }
}
