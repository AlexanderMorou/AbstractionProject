using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Compilers;
using System.Diagnostics.SymbolStore;

namespace AllenCopeland.Abstraction.Slf.Languages
{
    internal class CommonIntermediateLanguage :
        ICommonIntermediateLanguage
    {
        internal static readonly ICommonIntermediateLanguage Singleton = new CommonIntermediateLanguage();

        /* *
         * Private constructor for the language.
         * */
        private CommonIntermediateLanguage() { }

        #region ILanguage Members

        public string Name
        {
            get { return "Common Intermediate Language"; }
        }

        ILanguageProvider ILanguage.GetProvider()
        {
            return this.GetProvider();
        }

        public CompilerSupport CompilerSupport
        {
            get { return CompilerSupport.DebuggerSupport | CompilerSupport.COMInterop | CompilerSupport.Resources | CompilerSupport.Win32Resources | CompilerSupport.Unsafe | CompilerSupport.XMLDocumentation; }
        }

        public ILanguageVendor Vendor
        {
            get { return LanguageVendors.Microsoft; }
        }

        public Guid Guid
        {
            get { return SymLanguageType.ILAssembly; }
        }

        #endregion

        #region ICommonIntermediateLanguage Members

        public ICommonIntermediateProvider GetProvider()
        {
            return new CommonIntermediateProvider();
        }

        #endregion
    }
}
