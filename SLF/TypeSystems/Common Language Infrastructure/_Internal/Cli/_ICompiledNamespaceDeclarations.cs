using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    internal interface _ICompiledNamespaceDeclarations :
        INamespaceDictionary
    {
        /// <summary>
        /// Returns the <see cref="_ICompiledNamespaceParent"/>
        /// which contains the 
        /// <see cref="_ICompiledNamespaceDeclarations"/>.
        /// </summary>
        new _ICompiledNamespaceParent Parent { get; }
    }
}
