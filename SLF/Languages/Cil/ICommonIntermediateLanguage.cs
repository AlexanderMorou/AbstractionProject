using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Ast;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Languages.Cil
{
    /// <summary>
    /// Defines properties and methods for working with the
    /// common intermediate language, the root language of the 
    /// common language infrastructure.
    /// </summary>
    public interface ICommonIntermediateLanguage :
        ILanguage<ICommonIntermediateLanguage, ICommonIntermediateProvider>
    {
        /// <summary>
        /// Returns the <see cref="IMicrosoftLanguageVendor">Microsoft</see> vendor.
        /// </summary>
        new IMicrosoftLanguageVendor Vendor { get; }
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
        new ICommonIntermediateAssembly CreateAssembly(string name);
        /// <summary>
        /// Returns a new <see cref="ICommonIntermediateProvider"/> with the
        /// <paramref name="identityManager"/> provided.
        /// </summary>
        /// <param name="identityManager">The <see cref="IIntermediateCliManager"/>
        /// which marshalls type and assembly identities through the current type
        /// system.</param>
        /// <returns>A new <see cref="ICommonIntermediateProvider"/> with
        /// the <paramref name="identityManager"/> provided.</returns>
        ICommonIntermediateProvider GetProvider(IIntermediateCliManager identityManager);
    }
}
