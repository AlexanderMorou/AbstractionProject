using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Cst;
using AllenCopeland.Abstraction.Slf.Compilers;
using AllenCopeland.Abstraction.Slf.Translation;
/*---------------------------------------------------------------------\
| Copyright © 2010 Allen Copeland Jr.                                  |
|----------------------------------------------------------------------|
| The Abstraction Project's code is provided under a contract-release  |
| basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
\-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Languages
{
    public class TPPLanguageProvider :
        IHighLevelLanguageProvider<ITPPFile>
    {
        #region IHighLevelLanguageProvider<ITPPFile> Members

        public ILanguageParser<ITPPFile> Parser
        {
            get { throw new NotImplementedException(); }
        }

        public ILanguageASTTranslator<ITPPFile> ASTTranslator
        {
            get { throw new NotImplementedException(); }
        }

        public IIntermediateCompiler<ITPPFile> Compiler
        {
            get { throw new NotImplementedException(); }
        }

        public IIntermediateCodeTranslator Translator
        {
            get { throw new NotImplementedException(); }
        }

        public IHighLevelLanguage<ITPPFile> Language
        {
            get { throw new NotImplementedException(); }
        }

        #endregion
    }
}
