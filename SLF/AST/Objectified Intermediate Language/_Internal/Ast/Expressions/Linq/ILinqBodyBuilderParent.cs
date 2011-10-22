using System;
using System.Collections.Generic;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.Ast.Expressions.Linq
{
    internal interface ILinqBodyBuilderParent
    {
        /// <summary>
        /// Returns the <see cref="ILinqBodyBuilderParent"/> which contains
        /// the current <see cref="ILinqBodyBuilderParent"/>.
        /// </summary>
        ILinqBodyBuilderParent Parent { get; }
    }
}
