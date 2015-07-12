using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Languages;
using AllenCopeland.Abstraction.Slf.Cst;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Languages.VisualBasic.My;
using AllenCopeland.Abstraction.Slf.Ast.Cli;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Languages.VisualBasic
{
    /// <summary>
    /// Defines properties and methods for working with an assembly
    /// from the Visual Basic language.
    /// </summary>
    /// <remarks>Relative to Start in Visual Basic Language 
    /// Specification Version 10.0</remarks>
    public interface IVisualBasicAssembly<TAssembly, TProvider> :
        IVersionedIntermediateAssembly<IVisualBasicLanguage, TProvider, VisualBasicVersion>,
        IIntermediateCliAssembly
        where TAssembly :
            IVisualBasicAssembly<TAssembly, TProvider>
        where TProvider :
            IVersionedLanguageProvider<VisualBasicVersion>,
            ILanguageProvider
    {

    }

    public interface IMyVisualBasicAssembly :
        IVisualBasicAssembly<IMyVisualBasicAssembly, IMyVisualBasicProvider>
    {
        /// <summary>
        /// Returns the <see cref="IMyNamespaceDeclaration"/> which
        /// relates to the special My namespace within Visual Basic.
        /// </summary>
        IMyNamespaceDeclaration MyNamespace { get; }
    }
}
