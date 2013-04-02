using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.OldCodeGen.Utilities.Collections;
using System.CodeDom;
using AllenCopeland.Abstraction.OldCodeGen.Translation;

namespace AllenCopeland.Abstraction.OldCodeGen.Comments
{
    /// <summary>
    /// Defines properties and methods for working with a series of <see cref="IComment"/>
    /// implementation instances.
    /// </summary>
    public interface IComments :
        IControlledStateCollection<IComment>
    {
        IComment AddComment(string comment);
        IDocumentationComment AddDocComment(string comment);
        /// <summary>
        /// Generates a <see cref="CodeCommentStatementCollection"/> which correlates to the series
        /// of <see cref="IComment"/> elements in the <see cref="IComments"/> list.
        /// </summary>
        /// <param name="options">The CodeDOM generator options that directs the generation
        /// process.</param>
        /// <returns>A <see cref="CodeAttributeDeclarationCollection"/> pertinent to the <see cref="IAttributeDeclarations"/>
        /// entries.</returns>
        CodeCommentStatementCollection GenerateCodeDom(ICodeDOMTranslationOptions options);
    }
}
