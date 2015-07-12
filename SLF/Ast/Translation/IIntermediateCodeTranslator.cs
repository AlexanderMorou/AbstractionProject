using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using AllenCopeland.Abstraction.Slf.Ast.Expressions.Lambda;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Translation
{
    /// <summary>
    /// Defines properties and methods for working with an intermediate
    /// code translator.
    /// </summary>
    public interface IIntermediateCodeTranslator :
        IIntermediateCodeVisitor,
        IIntermediateDeclarationReferenceHandler
    {
        /// <summary>
        /// Returns the <see cref="IIntermediateCodeTranslatorOptions"/> associated
        /// to the current <see cref="IIntermediateCodeTranslator"/>.
        /// </summary>
        IIntermediateCodeTranslatorOptions Options { get; }
        /// <summary>
        /// Returns the <see cref="IControlledCollection{T}"/>
        /// of intermediate declarations presently being built.
        /// </summary>
        IControlledCollection<IIntermediateDeclaration> BuildTrail { get; }
        /// <summary>
        /// Writes text to the output stream.
        /// </summary>
        /// <param name="text">The <see cref="String"/> to write to the
        /// output stream.</param>
        void Write(string text);
        /// <summary>
        /// Begins a new line.
        /// </summary>
        void WriteLine();
        /// <summary>
        /// Returns the <see cref="IIntermediateCodeTranslatorFormatter"/>
        /// responsible for formatting the document.
        /// </summary>
        IIntermediateCodeTranslatorFormatter Formatter { get; }
        /// <summary>
        /// Returns the <see cref="IIntermediateCodeNameProvider"/> which handles
        /// display name, file name and other naming conventions.
        /// </summary>
        IIntermediateCodeNameProvider NameProvider { get; }
    }
}
