using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Ast.Cli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Languages.ToySharp
{
    /// <summary>
    /// Provides the base T*y++ provider implementation.
    /// </summary>
    public class ToySharpProvider :
        VersionedLanguageProvider<IToySharpLanguage, IToySharpProvider, ToySharpLanguageVersion, IIntermediateCliManager, Type, Assembly>,
        IToySharpProvider
    {

        internal ToySharpProvider(ToySharpLanguageVersion version, IIntermediateCliManager identityManager)
            : base(version, identityManager)
        {

        }
        protected override IToySharpLanguage OnGetLanguage()
        {
            return ToySharpLanguage.Singleton;
        }

        #region IToySharpProvider Members

        /// <summary>
        /// Returns the <see cref="IIntermediateCliManager"/> which marshalls
        /// the identities of intermediate and non-intermediate (compiled)
        /// assemblies and types.
        /// </summary>
        public new IIntermediateCliManager IdentityManager
        {
            get { return (IIntermediateCliManager)base.IdentityManager; }
        }

        #endregion
    }
}
