using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Compilers;
using System.Diagnostics.SymbolStore;
using AllenCopeland.Abstraction.Slf.Oil;
 /*---------------------------------------------------------------------\
 | Copyright © 2011 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Languages
{
    /// <summary>
    /// Provides a base implementation for the 
    /// <see cref="CommonIntermediateLanguage">Common Intermediate Language</see>.
    /// </summary>
    internal class CommonIntermediateLanguage :
        ICommonIntermediateLanguage
    {
        internal static readonly ICommonIntermediateLanguage Singleton = new CommonIntermediateLanguage();

        /* *
         * Private constructor for the language.
         * */
        private CommonIntermediateLanguage() { }

        #region ILanguage Members

        /// <summary>
        /// Returns the <see cref="String"/> value representing
        /// the unique name of the language.
        /// </summary>
        public string Name
        {
            get { return "Common Intermediate Language"; }
        }

        ILanguageProvider ILanguage.GetProvider()
        {
            return this.GetProvider();
        }

        /// <summary>
        /// Returns the level of functionality support the compiler contains.
        /// </summary>
        public CompilerSupport CompilerSupport
        {
            get { return CompilerSupport.DebuggerSupport | CompilerSupport.COMInterop | CompilerSupport.Resources | CompilerSupport.Win32Resources | CompilerSupport.Unsafe | CompilerSupport.XMLDocumentation; }
        }

        ILanguageVendor ILanguage.Vendor
        {
            get { return this.Vendor; }
        }

        /// <summary>
        /// Returns the <see cref="Guid"/> associated to the language when
        /// defining symbolic documents associated to a given assembly derived
        /// from the language.
        /// </summary>
        public Guid Guid
        {
            get { return SymLanguageType.ILAssembly; }
        }

        IIntermediateAssembly ILanguage.CreateAssembly(string name)
        {
            return this.CreateAssembly(name);
        }

        #endregion

        #region ICommonIntermediateLanguage Members

        /// <summary>
        /// Returns the <see cref="IMicrosoftLanguageVendor">Microsoft</see> vendor.
        /// </summary>
        public IMicrosoftLanguageVendor Vendor
        {
            get { return LanguageVendors.Microsoft; }
        }


        /// <summary>
        /// Returns a new <see cref="ICommonIntermediateProvider">provider</see> associated to the current
        /// <see cref="ILanguage"/>.
        /// </summary>
        /// <returns>A new <see cref="ICommonIntermediateProvider">provider</see> for the
        /// <see cref="CommonIntermediateLanguage">Common Intermediate Language</see>.</returns>
        public ICommonIntermediateProvider GetProvider()
        {
            return new CommonIntermediateProvider();
        }

        /// <summary>
        /// Creates a new <see cref="ICommonIntermediateAssembly"/>
        /// with the <paramref name="name"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> value
        /// representing part of the identity of the assembly.</param>
        /// <returns>A new <see cref="ICommonIntermediateAssembly"/>
        /// with the <paramref name="name"/> provided.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when 
        /// <paramref name="name"/> is null.</exception>
        /// <exception cref="System.ArgumentException">thrown when
        /// <paramref name="name"/> is <see cref="String.Empty"/>.</exception>
        public ICommonIntermediateAssembly CreateAssembly(string name)
        {
            if (name == null)
                throw new ArgumentNullException(name);
            if (name == string.Empty)
                throw new ArgumentException("name");
            return this.GetProvider().CreateAssembly(name);
        }

        #endregion
    }
}
