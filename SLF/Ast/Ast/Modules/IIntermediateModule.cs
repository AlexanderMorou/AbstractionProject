using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Modules;
using AllenCopeland.Abstraction.Slf.Ast.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Modules
{
    /// <summary>
    /// Defines properties and methods for working with an intermediate
    /// module.
    /// </summary>
    public interface IIntermediateModule :
        IIntermediateMethodParent<IModuleGlobalMethod, IIntermediateModuleGlobalMethod, IModule, IIntermediateModule>,
        IIntermediateFieldParent<IModuleGlobalField, IIntermediateModuleGlobalField, IModule, IIntermediateModule>,
        IModule,
        IIntermediateDeclaration
    {
        /// <summary>
        /// Returns the global methods defined on the current
        /// <see cref="IIntermediateModule"/>.
        /// </summary>
        new IIntermediateModuleGlobalMethodDictionary Methods { get; }
        /// <summary>
        /// Returns the global fields defined on the current
        /// <see cref="IIntermediateModule"/>.
        /// </summary>
        new IIntermediateModuleGlobalFieldDictionary Fields { get; }
        /// <summary>
        /// Returns the <see cref="IIntermediateAssembly"/> in
        /// which the <see cref="IIntermediateModule"/> was defined.
        /// </summary>
        new IIntermediateAssembly Parent { get; }
        /// <summary>
        /// Returns/sets whether the <see cref="IIntermediateModule"/>
        /// is the manifest module defined on the <see cref="Parent"/>
        /// <see cref="IIntermediateAssembly"/>.
        /// </summary>
        bool IsManifestModule { get; set; }
        /// <summary>
        /// Returns the full member dictionary which contains
        /// the combined set of <see cref="Methods"/> and
        /// <see cref="Fields"/> associated to the
        /// <see cref="IIntermediateModule"/>.
        /// </summary>
        IIntermediateFullMemberDictionary Members { get; }
    }
}
