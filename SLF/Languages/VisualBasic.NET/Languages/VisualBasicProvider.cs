using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Cst;
using AllenCopeland.Abstraction.Slf.Translation;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Oil;
using AllenCopeland.Abstraction.Slf.Oil.VisualBasic;

namespace AllenCopeland.Abstraction.Slf.Languages
{
    internal class VisualBasicProvider :
        IVisualBasicProvider
    {
        internal VisualBasicProvider(VisualBasicVersion version)
        {
            this.Version = version;
        }

        #region IVisualBasicProvider Members

        public IVisualBasicLanguage Language
        {
            get { return VisualBasicLanguage.Singleton; }
        }

        #endregion

        #region IVersionedHighLevelLanguageProvider<VisualBasicVersion,IVisualBasicStart> Members

        IVersionedHighLevelLanguage<VisualBasicVersion, IVisualBasicStart> IVersionedHighLevelLanguageProvider<VisualBasicVersion, IVisualBasicStart>.Language
        {
            get { return this.Language; }
        }

        #endregion

        #region IHighLevelLanguageProvider<IVisualBasicStart> Members

        public ILanguageParser<IVisualBasicStart> Parser
        {
            get { throw new NotImplementedException(); }
        }

        public ILanguageASTTranslator<IVisualBasicStart> ASTTranslator
        {
            get { throw new NotImplementedException(); }
        }

        public IIntermediateCodeTranslator Translator
        {
            get { throw new NotImplementedException(); }
        }

        IHighLevelLanguage<IVisualBasicStart> IHighLevelLanguageProvider<IVisualBasicStart>.Language
        {
            get { return this.Language; }
        }

        public IAnonymousTypePatternAid AnonymousTypePattern
        {
            get { return IntermediateGateway.VBPattern; }
        }


        IIntermediateAssembly IHighLevelLanguageProvider<IVisualBasicStart>.CreateAssembly(string name)
        {
            return this.CreateAssembly(name);
        }

        #endregion

        #region IVisualBasicProvider Members


        public IVisualBasicAssembly CreateAssembly(string name)
        {
            return new VisualBasicAssembly(name, this);
        }

        #endregion
        #region IVersionedLanguageProvider<VisualBasicVersion> Members

        public VisualBasicVersion Version { get; private set; }
        #endregion

        #region ILanguageProvider Members

        ILanguage ILanguageProvider.Language
        {
            get { return this.Language; }
        }

        #endregion
    }
}
