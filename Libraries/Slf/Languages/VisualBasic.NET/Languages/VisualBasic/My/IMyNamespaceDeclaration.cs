using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Ast;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Languages.VisualBasic.My
{
    /// <summary>
    /// Defines properties and methods for working with a namespace
    /// declaration which exposes easy to access information about the
    /// application, hardware, and other facets defined through extensions
    /// to the 'My' namespace.
    /// </summary>
    public interface IMyNamespaceDeclaration :
        IIntermediateNamespaceDeclaration
    {
        /// <summary>
        /// Returns the <see cref="IMyApplicationClass"/> which
        /// denotes information about the active application at run-time.
        /// </summary>
        IMyApplicationClass MyApplication { get; }
        /// <summary>
        /// Returns the <see cref="IMyComputerClass"/> which denotes information
        /// about the computer hardware available at run-time.
        /// </summary>
        IMyComputerClass MyComputer { get; }
        /// <summary>
        /// Returns the <see cref="IMyProjectClass"/> which denotes information about
        /// the active project, exposing the <see cref="MyApplication"/> and <paramref name="MyComputer"/>
        /// classes as properties.
        /// </summary>
        IMyProjectClass MyProject { get; }
    }
}
