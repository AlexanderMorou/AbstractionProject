using System;
using System.Collections.Generic;
using System.Text;
using System.CodeDom;
using AllenCopeland.Abstraction.OldCodeGen.Translation;
/* * 
 * Oilexer is an open-source project and must be released
 * as per the license associated to the project.
 * */
namespace AllenCopeland.Abstraction.OldCodeGen.Statements
{
    public class BlockStatement :
        BlockedStatement<CodeSnippetStatement>,
        IBlockStatement
    {
        public BlockStatement(IStatementBlock sourceBlock)
            : base(sourceBlock)
        {
        }

        public BlockStatement()
            : base()
        {
        }

        public override CodeSnippetStatement GenerateCodeDom(ICodeDOMTranslationOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
