using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Cst;
using AllenCopeland.Abstraction.Slf.Compilers;
using AllenCopeland.Abstraction.Slf.Parsers;
using AllenCopeland.Abstraction.Slf.Translation;
using AllenCopeland.Abstraction.Slf.Ast;

namespace AllenCopeland.Abstraction.Slf.Languages
{
    internal class CSharpProvider :
        ICSharpProvider
    {
        private ICSharpCompiler compiler;
        #region ICSharpProvider Members

        public ICSharpParser Parser
        {
            get { throw new NotImplementedException(); }
        }

        public ICSharpASTTranslator ASTTranslator
        {
            get { throw new NotImplementedException(); }
        }

        public ICSharpCompiler Compiler
        {
            get
            {
                if (this.compiler == null)
                    this.compiler = new CSharpCompiler(this);
                return this.compiler;
            }
        }

        public ICSharpCodeTranslator Translator
        {
            get { throw new NotImplementedException(); }
        }

        public ICSharpLanguage Language
        {
            get { return CSharpGateway.Language; }
        }

        #endregion

        #region IHighLevelLanguageProvider<ICSharpCompilationUnit> Members

        ILanguageParser<ICSharpCompilationUnit> IHighLevelLanguageProvider<ICSharpCompilationUnit>.Parser
        {
            get { return this.Parser; }
        }

        ILanguageASTTranslator<ICSharpCompilationUnit> IHighLevelLanguageProvider<ICSharpCompilationUnit>.ASTTranslator
        {
            get { return this.ASTTranslator; }
        }

        IIntermediateCompiler<ICSharpCompilationUnit> IHighLevelLanguageProvider<ICSharpCompilationUnit>.Compiler
        {
            get { return this.Compiler; }
        }

        IIntermediateCodeTranslator IHighLevelLanguageProvider<ICSharpCompilationUnit>.Translator
        {
            get { return this.Translator; }
        }

        IHighLevelLanguage<ICSharpCompilationUnit> IHighLevelLanguageProvider<ICSharpCompilationUnit>.Language
        {
            get { return this.Language; ; }
        }

        #endregion
    }
}
