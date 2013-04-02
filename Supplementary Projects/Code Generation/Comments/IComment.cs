using System;
using System.Collections.Generic;
using System.Text;
using System.CodeDom;
using AllenCopeland.Abstraction.OldCodeGen.Translation;

namespace AllenCopeland.Abstraction.OldCodeGen.Comments
{
    /// <summary>
    /// Defines properties and methods for working with a comment.
    /// </summary>
    public interface IComment
    {
        /// <summary>
        /// Generates the <see cref="CodeComment"/> that represents the <see cref="IComment"/>.
        /// </summary>
        /// <param name="options">The CodeDOM generator options that directs the generation
        /// process.</param>
        /// <returns>A new <see cref="CodeComment"/> instance if successful.-null- otherwise.</returns>
        CodeComment GenerateCodeDom(ICodeDOMTranslationOptions options);
        string BuildCommentBody(ICodeTranslationOptions options);
    }
}
