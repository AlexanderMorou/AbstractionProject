using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Languages.VisualBasic.My
{
    /// <summary>
    /// Defines properties and methods for working with a class dedicated
    /// to providing access to common characteristics of the application at run-time.
    /// </summary>
    public interface IMyApplicationClass :
        IIntermediateClassType
    {
        /// <summary>
        /// Returns the <see cref="IMyVisualBasicAssembly"/> in which
        /// the <see cref="IMyApplicationClass"/> is defined.
        /// </summary>
        new IMyVisualBasicAssembly Assembly { get; }

        /// <summary>
        /// Returns the <see cref="IMyNamespaceDeclaration"/> in which the
        /// <see cref="IMyApplicationClass"/> is defined.
        /// </summary>
        new IMyNamespaceDeclaration Parent { get; }

        /// <summary>
        /// Returns the <see cref="IClassPropertyMember"/> which denotes the culture
        /// of the running thread.
        /// </summary>
        IClassPropertyMember Culture { get; }

        /// <summary>
        /// Returns the <see cref="IClassPropertyMember"/> which denotes the entrypoint assembly's 
        /// information.
        /// </summary>
        IClassPropertyMember Info { get; }

        /// <summary>
        /// Returns the <see cref="IClassPropertyMember"/> which denotes the Log
        /// instance for common logging functionality.
        /// </summary>
        IClassPropertyMember Log { get; }

        /// <summary>
        /// Returns the <see cref="IClassPropertyMember"/> which denotes the user-interface
        /// culture of the running thread.
        /// </summary>
        IClassPropertyMember UICulture { get; }
    }
}
