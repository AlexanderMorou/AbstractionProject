using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Cst;
using AllenCopeland.Abstraction.Slf.Translation;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Oil;
using AllenCopeland.Abstraction.Slf.Oil.VisualBasic;
using AllenCopeland.Abstraction.Slf.Compilers;

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

        /// <summary>
        /// Returns the <see cref="IVisualBasicLanguage">Visual Basic.NET
        /// language</see>
        /// associated to the <see cref="IVisualBasicProvider"/>.
        /// </summary>
        public IVisualBasicLanguage Language
        {
            get { return VisualBasicLanguage.Singleton; }
        }

        /// <summary>
        /// Creates a new <see cref="IVisualBasicAssembly"/>
        /// with the <paramref name="name"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> value
        /// representing part of the identity of the assembly.</param>
        /// <returns>A new <see cref="IVisualBasicAssembly"/>
        /// with the <paramref name="name"/> provided.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when 
        /// <paramref name="name"/> is null.</exception>
        /// <exception cref="System.ArgumentException">thrown when
        /// <paramref name="name"/> is <see cref="String.Empty"/>.</exception>
        public IVisualBasicAssembly CreateAssembly(string name)
        {
            VisualBasicAssemblyBridge.Register();
            return IntermediateGateway.CreateAssembly<VisualBasicAssembly, IVisualBasicLanguage, IVisualBasicProvider>(name, this);
        }

        #endregion

        #region IVersionedHighLevelLanguageProvider<VisualBasicVersion,IVisualBasicStart> Members

        IVersionedHighLevelLanguage<VisualBasicVersion, IVisualBasicStart> IVersionedHighLevelLanguageProvider<VisualBasicVersion, IVisualBasicStart>.Language
        {
            get { return this.Language; }
        }

        #endregion

        #region IHighLevelLanguageProvider<IVisualBasicStart> Members

        /// <summary>
        /// Returns the <see cref="ILanguageParser{TRootNode}"/>
        /// of the current high level language provider instance.
        /// </summary>
        public ILanguageParser<IVisualBasicStart> Parser
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Returns the <see cref="ILanguageCSTTranslator{TRootNode}"/>
        /// of the current high level language provider instance.
        /// </summary>
        public ILanguageCSTTranslator<IVisualBasicStart> ASTTranslator
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Returns the <see cref="IIntermediateCodeTranslator"/>
        /// of the current language provider.
        /// </summary>
        public IIntermediateCodeTranslator Translator
        {
            get { throw new NotImplementedException(); }
        }

        IHighLevelLanguage<IVisualBasicStart> IHighLevelLanguageProvider<IVisualBasicStart>.Language
        {
            get { return this.Language; }
        }

        /// <summary>
        /// Returns the <see cref="IAnonymousTypePatternAid"/> which
        /// provides anonymous types defined within an assembly with
        /// a formatting guideline.
        /// </summary>
        public IAnonymousTypePatternAid AnonymousTypePattern
        {
            get { return IntermediateGateway.VBPattern; }
        }

        #endregion

        #region IVersionedLanguageProvider<VisualBasicVersion> Members

        /// <summary>
        /// Returns the <see cref="VisualBasicVersion"/>
        /// of the current <see cref="IVisualBasicProvider"/>
        /// which denotes the specific levels of functional support provided
        /// by the current 
        /// <see cref="IVisualBasicLanguage">Visual Basic.NET language</see>.
        /// </summary>
        public VisualBasicVersion Version { get; private set; }

        #endregion

        #region ILanguageProvider Members

        ILanguage ILanguageProvider.Language
        {
            get { return this.Language; }
        }

        IIntermediateAssembly ILanguageProvider.CreateAssembly(string name)
        {
            return this.CreateAssembly(name);
        }

        #endregion
    }
}
