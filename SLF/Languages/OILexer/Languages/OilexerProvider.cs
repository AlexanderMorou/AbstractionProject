﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Parsers.Oilexer;
using AllenCopeland.Abstraction.Slf.Compilers;
using AllenCopeland.Abstraction.Slf.Translation;
using AllenCopeland.Abstraction.Slf.Compilers.Oilexer;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Languages
{
    internal partial class OilexerProvider :
        IHighLevelLanguageProvider<IGDFile>
    {
        private OilexerCompiler compiler;
        #region IHighLevelLanguageProvider<IGDFile> Members

        public ILanguageParser<IGDFile> Parser
        {
            get { throw new NotImplementedException(); }
        }

        public ILanguageASTTranslator<IGDFile> ASTTranslator
        {
            get { throw new NotImplementedException(); }
        }

        public IIntermediateCompiler<IGDFile> Compiler
        {
            get
            {
                if (this.compiler == null)
                    this.compiler = new OilexerCompiler(this);
                return this.compiler;
            }
        }

        public IIntermediateCodeTranslator Translator
        {
            get { throw new NotSupportedException(); }
        }

        public IHighLevelLanguage<IGDFile> Language
        {
            get { return OilexerLanguage.LanguageInstance; }
        }

        #endregion
    }
}