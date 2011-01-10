using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Cst;
using AllenCopeland.Abstraction.Slf.Compilers;
using AllenCopeland.Abstraction.Slf.Parsers;
using AllenCopeland.Abstraction.Slf.Translation;
using AllenCopeland.Abstraction.Slf.Ast;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Languages
{
    internal class CSharpProvider :
        ICSharpProvider
    {
        private CSharpLanguageVersion versionCompatability;
        internal CSharpProvider(CSharpLanguageVersion versionCompatability)
        {
            this.versionCompatability = versionCompatability;
        }

        #region ICSharpProvider Members

        public ICSharpParser Parser
        {
            get { throw new NotImplementedException(); }
        }

        public ICSharpASTTranslator ASTTranslator
        {
            get { throw new NotImplementedException(); }
        }
        public ICSharpCodeTranslator Translator
        {
            get { throw new NotImplementedException(); }
        }

        public ICSharpLanguage Language
        {
            get {
                switch (versionCompatability)
                {
                    case CSharpLanguageVersion.CSharp_v2:
                        return CSharpGateway.Language.Version2;
                    case CSharpLanguageVersion.CSharp_v3:
                        return CSharpGateway.Language.Version3;
                    case CSharpLanguageVersion.CSharp_v3_5:
                        return CSharpGateway.Language.Version3_5;
                    case CSharpLanguageVersion.CSharp_v4:
                        return CSharpGateway.Language.Version4;
                    case CSharpLanguageVersion.CSharp_v5:
                        return CSharpGateway.Language.Version5;
                    default:
                        throw new NotSupportedException("Version not supported.");
                }
            }
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
