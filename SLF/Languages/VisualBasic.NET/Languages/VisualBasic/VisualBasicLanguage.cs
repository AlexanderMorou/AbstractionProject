using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.SymbolStore;
using AllenCopeland.Abstraction.Slf.Compilers;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Cst;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf._Internal.Ast;
using AllenCopeland.Abstraction.Slf.Cli;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Languages.VisualBasic
{
    internal class VisualBasicLanguage :
        IVisualBasicLanguage
    {
        public const VisualBasicVersion DefaultVersion = VisualBasicVersion.Version11;
        public static readonly IVisualBasicLanguage Singleton = new VisualBasicLanguage();

        private VisualBasicLanguage()
        {
        }

        #region IVisualBasicLanguage Members

        public ICoreVisualBasicProvider GetProvider()
        {
            return this.GetProvider(VisualBasicLanguage.DefaultVersion);
        }

        public ICoreVisualBasicProvider GetProvider(VisualBasicVersion version)
        {
            return new CoreVisualBasicProvider(version, GetIdentityManager(version));
        }

        public ICoreVisualBasicProvider GetProvider(VisualBasicVersion version, IIntermediateCliManager identityManager)
        {
            return new CoreVisualBasicProvider(version, identityManager);
        }

        public IMyVisualBasicProvider GetMyProvider()
        {
            return GetMyProvider(VisualBasicLanguage.DefaultVersion);
        }

        public IMyVisualBasicProvider GetMyProvider(VisualBasicVersion version)
        {
            return GetMyProvider(version, GetIdentityManager(version));
        }

        private static IntermediateCliManager GetIdentityManager(VisualBasicVersion version)
        {
            CliFrameworkVersion frameworkVersion = CliGateway.CurrentVersion;
            switch (version)
            {
                case VisualBasicVersion.Version08:
                    frameworkVersion = CliFrameworkVersion.v2_0_50727;
                    break;
                case VisualBasicVersion.Version09:
                    frameworkVersion = CliFrameworkVersion.v3_5;
                    break;
                case VisualBasicVersion.Version10:
                    frameworkVersion = CliFrameworkVersion.v4_0_30319;
                    break;
                case VisualBasicVersion.Version11:
                    frameworkVersion = CliFrameworkVersion.v4_5;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("version");
            }
            var identityManager = new IntermediateCliManager(CliGateway.GetRuntimeEnvironmentInfo(CliGateway.CurrentPlatform, frameworkVersion));
            return identityManager;
        }

        public IMyVisualBasicProvider GetMyProvider(VisualBasicVersion version, IIntermediateCliManager identityManager)
        {
            return new MyVisualBasicProvider(version, identityManager);
        }

        public ICoreVisualBasicAssembly CreateAssembly(string name, VisualBasicVersion version)
        {
            return this.GetProvider(version).CreateAssembly(name);
        }

        public ICoreVisualBasicAssembly CreateAssembly(string name)
        {
            return this.GetProvider(DefaultVersion).CreateAssembly(name);
        }

        #endregion

        #region IVersionedLanguage<VisualBasicVersion,IVisualBasicStart> Members

        IVersionedLanguageProvider<VisualBasicVersion> IVersionedLanguage<VisualBasicVersion>.GetProvider(VisualBasicVersion version)
        {
            return this.GetProvider(version);
        }

        IAssembly ILanguage.CreateAssembly(string name)
        {
            return this.CreateAssembly(name);
        }

        #endregion

        #region ILanguage Members

        public string Name
        {
            get { return "Visual Basic.NET"; }
        }

        ILanguageProvider ILanguage.GetProvider()
        {
            return this.GetProvider();
        }

        public CompilerSupport CompilerSupport
        {
            get {
                return this.GetCompilerSupport(VisualBasicLanguage.DefaultVersion);
            }
        }

        public ILanguageVendor Vendor
        {
            get { return LanguageVendors.Microsoft; }
        }

        public Guid Guid
        {
            get { return LanguageGuids.VisualBasic; }
        }

        #endregion

        #region IVersionedLanguage<VisualBasicVersion> Members

        public CompilerSupport GetCompilerSupport(VisualBasicVersion version)
        {
            CompilerSupport result = CompilerSupport.FullSupport ^ (CompilerSupport.Win32Resources | CompilerSupport.PrimaryInteropEmbedding | CompilerSupport.StructuralTyping | Compilers.CompilerSupport.Unsafe);
            if (((int)version) >= (int)VisualBasicVersion.Version10)
                result |= CompilerSupport.PrimaryInteropEmbedding;
            return result;
        }

        public IEnumerable<VisualBasicVersion> Versions
        {
            get
            {
                yield return VisualBasicVersion.Version08;
                yield return VisualBasicVersion.Version09;
                yield return VisualBasicVersion.Version10;
                yield return VisualBasicVersion.Version11;
            }
        }

        #endregion

        #region ILanguage<IVisualBasicLanguage,IMyVisualBasicProvider> Members

        IMyVisualBasicProvider ILanguage<IVisualBasicLanguage, IMyVisualBasicProvider>.GetProvider()
        {
            return this.GetMyProvider(DefaultVersion);
        }

        #endregion

    }
}
