using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using AllenCopeland.Abstraction.Slf.Compilers;
using AllenCopeland.Abstraction.Slf.Cst;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
using AllenCopeland.Abstraction.Slf.Ast.Statements;
using AllenCopeland.Abstraction.Utilities.Collections;
using System.Diagnostics.SymbolStore;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf._Internal.Ast;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Ast.Cli;
/*---------------------------------------------------------------------\
| Copyright � 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
|----------------------------------------------------------------------|
| The Abstraction Project's code is provided under a contract-release  |
| basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
\-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Languages.CSharp
{

    internal class CSharpLanguage :
        ICSharpLanguage
    {
        internal static readonly CSharpLanguage Singleton = new CSharpLanguage();
        public CSharpLanguage()
        {
        }

        ICSharpProvider ILanguage<ICSharpLanguage, ICSharpProvider>.GetProvider(IIdentityManager identityManager)
        {
            if (!(identityManager is IIntermediateCliManager))
                throw new ArgumentException("Wrong kind of identity manager", "identityManager");
            return this.GetProvider((IIntermediateCliManager)identityManager);
        }

        /// <summary>
        /// The types which are automatically translated into their
        /// C&#9839; short forms.
        /// </summary>
        internal static readonly ControlledCollection<Type> AutoFormTypes = new ControlledCollection<Type>
            (
                new List<Type>
                (
                    new Type[]
                        {
                            typeof(byte),
                            typeof(sbyte),
                            typeof(ushort),
                            typeof(short),
                            typeof(uint),
                            typeof(int),
                            typeof(ulong),
                            typeof(long),
                            typeof(void),
                            typeof(bool),
                            typeof(char),
                            typeof(decimal),
                            typeof(float),
                            typeof(double),
                            typeof(object),
                            typeof(string)
                        }
                )
            );

        public IMicrosoftLanguageVendor Vendor
        {
            get
            {
                return LanguageVendors.Microsoft;
            }
        }

        #region ILanguage Members

        /// <summary>
        /// Returns the <see cref="String"/> value representing
        /// the unique name of the language.
        /// </summary>
        /// <remarks>Returns "C&#9839;".</remarks>
        public string Name
        {
            get { return "C\u266f"; }
        }

        ILanguageProvider ILanguage.GetProvider()
        {
            return this.GetProvider();
        }

        /// <summary>
        /// Returns the level of functionality support the 
        /// compiler contains.
        /// </summary>
        public CompilerSupport CompilerSupport
        {
            get
            {
                return GetCompilerSupport(CSharpLanguageVersion.CurrentVersion);
            }
        }

        ILanguageVendor ILanguage.Vendor
        {
            get { return this.Vendor; }
        }
        #endregion

        #region IVersionedLanguage Members

        public CompilerSupport GetCompilerSupport(CSharpLanguageVersion version)
        {

            CompilerSupport result = CompilerSupport.FullSupport ^ (CompilerSupport.Win32Resources | CompilerSupport.PrimaryInteropEmbedding | CompilerSupport.StructuralTyping);
            if (((int)version) >= (int)CSharpLanguageVersion.Version4)
                result |= CompilerSupport.PrimaryInteropEmbedding;
            return result;
        }

        IVersionedLanguageProvider<CSharpLanguageVersion> IVersionedLanguage<CSharpLanguageVersion>.GetProvider(CSharpLanguageVersion version)
        {
            return GetProvider(version);
        }

        /// <summary>
        /// Returns a <see cref="IEnumerable{T}"/> of
        /// elements which denotes the various 
        /// <see cref="CSharpLanguageVersion">versions</see> of 
        /// the <see cref="CSharpLanguage">C&#9839; language</see>.
        /// </summary>
        public IEnumerable<CSharpLanguageVersion> Versions
        {
            get
            {
                yield return CSharpLanguageVersion.Version2;
                yield return CSharpLanguageVersion.Version3;
                yield return CSharpLanguageVersion.Version4;
                yield return CSharpLanguageVersion.Version5;
            }
        }

        #endregion

        #region IVersionedLanguage<CSharpLanguageVersion,ICSharpCompilationUnit> Members

        IAssembly ILanguage.CreateAssembly(string name)
        {
            return this.CreateAssembly(name);
        }

        #endregion

        #region ICSharpLanguage Members

        /// <summary>
        /// Returns a new <see cref="ICSharpProvider"/> associated to the
        /// <see cref="CSharpLanguage">C&#9839; language</see>.
        /// </summary>
        /// <returns>A new <see cref="ICSharpProvider"/> for the 
        /// <see cref="CSharpLanguage">C&#9839; language</see>.</returns>
        public ICSharpProvider GetProvider()
        {
            return this.GetProvider(CSharpLanguageVersion.CurrentVersion);
        }

        /// <summary>
        /// Returns a new <see cref="ICSharpProvider"/> associated to the
        /// <see cref="CSharpLanguage">C&#9839; language</see> relative
        /// to the <paramref name="version"/>.
        /// </summary>
        /// <param name="version">The <see cref="CSharpLanguageVersion"/>
        /// value which denotes what version of the language to return 
        /// the provider for.</param>
        /// <returns>A new <see cref="ICSharpProvider"/> for the 
        /// <see cref="CSharpLanguage">C&#9839; language</see>.</returns>
        public ICSharpProvider GetProvider(CSharpLanguageVersion version)
        {
            CliFrameworkVersion frameworkVersion = CliGateway.CurrentVersion;
            switch (version)
            {
                case CSharpLanguageVersion.Version2:
                    frameworkVersion = CliFrameworkVersion.v2_0_50727;
                    break;
                case CSharpLanguageVersion.Version3:
                    frameworkVersion = CliFrameworkVersion.v3_5;
                    break;
                case CSharpLanguageVersion.Version4:
                    frameworkVersion = CliFrameworkVersion.v4_0_30319;
                    break;
                case CSharpLanguageVersion.Version5:
                    frameworkVersion = CliFrameworkVersion.v4_5;
                    break;
                case CSharpLanguageVersion.Version6:
                    frameworkVersion = CliFrameworkVersion.v4_6;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("version");
            }
            return this.GetProvider(version, new IntermediateCliManager(IntermediateCliGateway.GetRuntimeEnvironmentInfo(CliGateway.CurrentPlatform, frameworkVersion)));
        }

        /// <summary>
        /// Creates a new <see cref="ICSharpAssembly"/> with the <paramref name="name"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> value representing part of the identity of the assembly.</param>
        /// <returns>A new <see cref="ICSharpAssembly"/> with the <paramref name="name"/> provided.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="name"/> is null.</exception>
        /// <exception cref="System.ArgumentException">thrown when <paramref name="name"/> is <see cref="String.Empty"/>.</exception>
        public ICSharpAssembly CreateAssembly(string name)
        {
            var provider = this.GetProvider();
            IIntermediateAssemblyCtorLanguageService<ICSharpProvider, ICSharpLanguage, ICSharpAssembly> creatorService;
            if (provider.TryGetService<IIntermediateAssemblyCtorLanguageService<ICSharpProvider, ICSharpLanguage, ICSharpAssembly>>(LanguageGuids.Services.IntermediateAssemblyCreatorService, out creatorService))
                return creatorService.New(name);
            var resultAssembly = new CSharpAssembly(name, provider, provider.IdentityManager.RuntimeEnvironment);
            provider.IdentityManager.AssemblyCreated(resultAssembly);
            return resultAssembly;
        }

        /// <summary>
        /// Creates a new <see cref="IIntermediateAssembly"/> with the <paramref name="name"/> and 
        /// <paramref name="version"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> value representing part of the identity of the assembly.</param>
        /// <param name="identityManager">The <see cref="IIntermediateCliManager"/> which is used to marshal type identities
        /// in the current type model.</param>
        /// <param name="version">The <see cref="CSharpLanguageVersion"/> to which the <see cref="ICSharpAssembly"/>
        /// is built against.</param>
        /// <returns>A new <see cref="ICSharpAssembly"/>
        /// with the <paramref name="name"/> and <paramref name="version"/> provided.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="name"/> is null.</exception>
        /// <exception cref="System.ArgumentException">thrown when <paramref name="name"/> is <see cref="String.Empty"/>
        /// or <paramref name="version"/> is not one of <see cref="CSharpLanguageVersion"/>.</exception>
        public ICSharpAssembly CreateAssembly(string name, IIntermediateCliManager identityManager, CSharpLanguageVersion version)
        {
            var provider = this.GetProvider(version);
            IIntermediateAssemblyCtorLanguageService<ICSharpProvider, ICSharpLanguage, ICSharpAssembly> creatorService;
            if (provider.TryGetService<IIntermediateAssemblyCtorLanguageService<ICSharpProvider, ICSharpLanguage, ICSharpAssembly>>(LanguageGuids.Services.IntermediateAssemblyCreatorService, out creatorService))
                return creatorService.New(name);
            var resultAssembly = new CSharpAssembly(name, provider, provider.IdentityManager.RuntimeEnvironment);
            provider.IdentityManager.AssemblyCreated(resultAssembly);
            return resultAssembly;
        }

        #endregion

        #region IHighLevelLanguage<ICSharpCompilationUnit> Members

        public Guid Guid
        {
            get { return LanguageGuids.CSharp; }
        }

        #endregion

        public ICSharpProvider GetProvider(CSharpLanguageVersion version, IIntermediateCliManager identityManager)
        {
            return new CSharpProvider(version, identityManager);
        }

        public ICSharpProvider GetProvider(IIntermediateCliManager identityManager)
        {
            return GetProvider(CSharpLanguageVersion.CurrentVersion, identityManager);
        }
    }
}
