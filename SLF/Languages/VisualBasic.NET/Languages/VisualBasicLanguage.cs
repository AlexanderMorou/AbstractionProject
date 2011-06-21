using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.SymbolStore;
using AllenCopeland.Abstraction.Slf.Compilers;
using AllenCopeland.Abstraction.Slf.Oil.VisualBasic;
using AllenCopeland.Abstraction.Slf.Oil;
using AllenCopeland.Abstraction.Slf.Cst;
 /*---------------------------------------------------------------------\
 | Copyright © 2011 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Languages
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

        public IVisualBasicProvider GetProvider()
        {
            return this.GetProvider(VisualBasicLanguage.DefaultVersion);
        }

        public IVisualBasicProvider GetProvider(VisualBasicVersion version)
        {
            return new VisualBasicProvider(version);
        }

        #endregion

        #region IVersionedHighLevelLanguage<VisualBasicVersion,IVisualBasicStart> Members

        IVersionedHighLevelLanguageProvider<VisualBasicVersion, Cst.IVisualBasicStart> IVersionedHighLevelLanguage<VisualBasicVersion, Cst.IVisualBasicStart>.GetProvider(VisualBasicVersion version)
        {
            return new VisualBasicProvider(version);
        }

        IIntermediateAssembly IVersionedHighLevelLanguage<VisualBasicVersion,IVisualBasicStart>.CreateAssembly(string name, VisualBasicVersion version)
        {
            return this.CreateAssembly(name, version);
        }

        #endregion

        #region IHighLevelLanguage<IVisualBasicStart> Members

        IHighLevelLanguageProvider<Cst.IVisualBasicStart> IHighLevelLanguage<Cst.IVisualBasicStart>.GetProvider()
        {
            return this.GetProvider();
        }

        IIntermediateAssembly ILanguage.CreateAssembly(string name)
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
            get { return SymLanguageType.Basic; }
        }

        #endregion

        #region IVersionedLanguage<VisualBasicVersion> Members

        IVersionedLanguageProvider<VisualBasicVersion> IVersionedLanguage<VisualBasicVersion>.GetProvider(VisualBasicVersion version)
        {
            return this.GetProvider(version);
        }

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

        #region IVisualBasicLanguage Members


        public IVisualBasicAssembly CreateAssembly(string name, VisualBasicVersion version)
        {
            return this.GetProvider(version).CreateAssembly(name);
        }

        public IVisualBasicAssembly CreateAssembly(string name)
        {
            return this.GetProvider(DefaultVersion).CreateAssembly(name);
        }

        #endregion
    }
}
