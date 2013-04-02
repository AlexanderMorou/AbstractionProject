using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
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
        /// Returns the <see cref="ILocalDeclarationStatement"/> associated to the
        /// <see cref="ITypeNamedCatchExceptionBlockStatement"/>.
        /// </summary>
        ILocalDeclarationStatement LocalVariable { get; }
    }
}
