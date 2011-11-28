using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Statements
{
    public interface IIterationDeclarationBlockStatement :
        IIterationBlockBaseStatement
    {
        /// <summary>
        /// Returns/sets the <see cref="ILocalDeclarationStatement"/> which executes once at the initialization
        /// of the iteration process.
        /// </summary>
        ILocalDeclarationStatement LocalDeclaration { get; set; }
    }
}
