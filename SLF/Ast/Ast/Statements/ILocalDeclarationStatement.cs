using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Ast.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Statements
{
    public interface ILocalDeclarationStatement :
        IStatement
    {
        /// <summary>
        /// Returns the <see cref="ILocalMember"/> declared by the 
        /// <see cref="ILocalDeclarationStatement"/>.
        /// </summary>
        ILocalMember DeclaredLocal { get; }
    }
}
