using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Ast.Cli;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Compilers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Languages.ToySharp
{
    internal class ToySharpLanguage :
        IToySharpLanguage
    {
        public static IToySharpLanguage Singleton = new ToySharpLanguage();

        private ToySharpLanguage() { }

        #region IVersionedLanguage<ToySharpLanguageVersion> Members

        IVersionedLanguageProvider<ToySharpLanguageVersion> IVersionedLanguage<ToySharpLanguageVersion>.GetProvider(ToySharpLanguageVersion version)
        {
            return this.GetProvider(version);
        }

        public CompilerSupport GetCompilerSupport(ToySharpLanguageVersion version)
        {
            return this.CompilerSupport;
        }

        public IEnumerable<ToySharpLanguageVersion> Versions
        {
            get { yield return ToySharpLanguageVersion.Version1; }
        }

        #endregion

        #region ILanguage Members

        public string Name
        {
            get { return "T*y++"; }
        }

        ILanguageProvider ILanguage.GetProvider()
        {
            return this.GetProvider();
        }

        public Compilers.CompilerSupport CompilerSupport
        {
            get { return Compilers.CompilerSupport.FullSupport; }
        }

        public ILanguageVendor Vendor
        {
            get { return LanguageVendors.AllenCopeland; }
        }

        public Guid Guid
        {
            get { return LanguageGuids.ToySharp; }
        }

        public IAssembly CreateAssembly(string name)
        {
            return this.GetProvider().CreateAssembly(name);
        }

        #endregion

        #region ILanguage<IToySharpLanguage,IToySharpProvider> Members

        public IToySharpProvider GetProvider()
        {
            return this.GetProvider(ToySharpLanguageVersion.Version1);
        }

        #endregion

        #region IToySharpLanguage Members

        public IToySharpProvider GetProvider(ToySharpLanguageVersion version)
        {
            return GetProvider(version, CliGateway.CurrentPlatform, CliFrameworkVersion.CurrentVersion);
        }
        
        public IToySharpProvider GetProvider(ToySharpLanguageVersion version, IIntermediateCliManager identityManager)
        {
            return new ToySharpProvider(version, identityManager);
        }

        public IToySharpProvider GetProvider(IIntermediateCliManager identityManager)
        {
            return GetProvider(ToySharpLanguageVersion.CurrentVersion, identityManager);
        }

        public IToySharpProvider GetProvider(ToySharpLanguageVersion version, CliFrameworkPlatform platform, CliFrameworkVersion frameworkVersion)
        {
            return this.GetProvider(version, IntermediateCliGateway.CreateIdentityManager(platform, frameworkVersion));
        }
        #endregion

        #region ILanguage<IToySharpLanguage,IToySharpProvider> Members

        public IToySharpProvider GetProvider(IIdentityManager identityManager)
        {
            if (!(identityManager is IIntermediateCliManager))
                throw new ArgumentException("Wrong kind of identity manager", "identityManager");
            return new ToySharpProvider(ToySharpLanguageVersion.CurrentVersion, (IIntermediateCliManager)identityManager);
        }

        #endregion
    }
}
