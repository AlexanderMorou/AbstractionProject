using System;
using System.Collections.Generic;
using System.Text;
using System.CodeDom;
using AllenCopeland.Abstraction.OldCodeGen.Translation;

namespace AllenCopeland.Abstraction.OldCodeGen.Types.Members
{
    public interface IAutoCommentFragmentMembers
    {
        CodeCommentStatementCollection GenerateCommentCodeDom(ICodeDOMTranslationOptions options);
    }
}
