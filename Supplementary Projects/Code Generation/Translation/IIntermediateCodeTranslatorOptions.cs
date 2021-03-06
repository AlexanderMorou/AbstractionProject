using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.OldCodeGen.Types;

namespace AllenCopeland.Abstraction.OldCodeGen.Translation
{
    /// <summary>
    /// Defines properties and methods which direct the code generation
    /// process.
    /// </summary>
    public interface IIntermediateCodeTranslatorOptions :
        ICodeTranslationOptions
    {
        /// <summary>
        /// Returns/sets the code formatter used to manage the special format for the generation process.
        /// </summary>
        IIntermediateCodeTranslatorFormatter Formatter { get; set; }

        Func<IType, string> GetFileNameOf { get; set; }
        Func<IIntermediateProject, int> GetLineNumber { get; set; }
        bool SuppressTailGenerationMessage { get; set; }
    }
}
