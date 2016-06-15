using AllenCopeland.Abstraction.Slf.Ast.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Statements
{
    /// <summary>
    /// Defines properties and methods for working with a 
    /// <see cref="ITypeNamedCatchExceptionBlockStatement"/>.
    /// </summary>
    public interface ITypeNamedCatchExceptionBlockStatement :
        ITypedCatchExceptionBlockStatement
    {
        /// <summary>
        /// Returns the <see cref="ILocalDeclarationsStatement"/> associated to the
        /// <see cref="ITypeNamedCatchExceptionBlockStatement"/>.
        /// </summary>
        ILocalDeclarationsStatement LocalVariableDeclaration { get; }
        ITypedLocalMember LocalVariable { get; }
    }
}
