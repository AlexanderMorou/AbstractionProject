using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Cst;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Ast.Cli;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Languages.CSharp
{
    public interface ICSharpLanguage :
        IVersionedLanguage<CSharpLanguageVersion>,
        ILanguage<ICSharpLanguage, ICSharpProvider>
    {
        /// <summary>
        /// Returns a new <see cref="ICSharpProvider"/> associated to the
        /// <see cref="ICSharpLanguage">C&#9839; language</see>.
        /// </summary>
        /// <returns>A new <see cref="ICSharpProvider"/> for the 
        /// <see cref="ICSharpLanguage">C&#9839; language</see>.</returns>
        new ICSharpProvider GetProvider();
        /// <summary>
        /// Returns a new <see cref="ICSharpProvider"/> associated to the
        /// <see cref="ICSharpLanguage">C&#9839; language</see> relative
        /// to the <paramref name="version"/>.
        /// </summary>
        /// <param name="version">The <see cref="CSharpLanguageVersion"/>
        /// value which denotes what version of the language to return 
        /// the provider for.</param>
        /// <returns>A new <see cref="ICSharpProvider"/> for the 
        /// <see cref="ICSharpLanguage">C&#9839; language</see>.</returns>
        new ICSharpProvider GetProvider(CSharpLanguageVersion version);
        /// <summary>
        /// Returns a new <see cref="ICSharpProvider"/> associated to the
        /// <see cref="ICSharpLanguage">C&#9839; language</see> relative
        /// to the <paramref name="version"/>.
        /// </summary>
        /// <param name="version">The <see cref="CSharpLanguageVersion"/>
        /// value which denotes what version of the language to return 
        /// the provider for.</param>
        /// <param name="identityManager">The <see cref="IIntermediateCliManager"/>
        /// which is used to marshal type identities in the current type model.</param>
        /// <returns>A new <see cref="ICSharpProvider"/> for the 
        /// <see cref="ICSharpLanguage">C&#9839; language</see>.</returns>
        ICSharpProvider GetProvider(CSharpLanguageVersion version, IIntermediateCliManager identityManager);
        /// <summary>
        /// Returns a new <see cref="ICSharpProvider"/> associated to the
        /// <see cref="ICSharpLanguage">C&#9839; language</see> relative
        /// to the current version and the <paramref name="identityManager"/> provided.
        /// </summary>
        /// <param name="identityManager">The <see cref="IIntermediateCliManager"/>
        /// which is used to marshal type identities in the current type model.</param>
        /// <returns>A new <see cref="ICSharpProvider"/> for the 
        /// <see cref="ICSharpLanguage">C&#9839; language</see>.</returns>
        new ICSharpProvider GetProvider(IIntermediateCliManager identityManager);
        /// <summary>
        /// Creates a new <see cref="ICSharpAssembly"/>
        /// with the <paramref name="name"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> value
        /// representing part of the identity of the assembly.</param>
        /// <returns>A new <see cref="ICSharpAssembly"/>
        /// with the <paramref name="name"/> provided.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when 
        /// <paramref name="name"/> is null.</exception>
        /// <exception cref="System.ArgumentException">thrown when
        /// <paramref name="name"/> is <see cref="String.Empty"/>.</exception>
        new ICSharpAssembly CreateAssembly(string name);
        /// <summary>
        /// Creates a new <see cref="IIntermediateAssembly"/>
        /// with the <paramref name="name"/> and 
        /// <paramref name="version"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> value
        /// representing part of the identity of the assembly.</param>
        /// <param name="identityManager">The <see cref="IIntermediateCliManager"/>
        /// which is used to marshal type identities in the current type model.</param>
        /// <param name="version">The <see cref="CSharpLanguageVersion"/>
        /// to which the <see cref="ICSharpAssembly"/>
        /// is built against.</param>
        /// <returns>A new <see cref="ICSharpAssembly"/>
        /// with the <paramref name="name"/> and 
        /// <paramref name="version"/> provided.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when 
        /// <paramref name="name"/> is null.</exception>
        /// <exception cref="System.ArgumentException">thrown when
        /// <paramref name="name"/> is <see cref="String.Empty"/>
        /// or <paramref name="version"/> is not one of 
        /// <see cref="CSharpLanguageVersion"/>.</exception>
        ICSharpAssembly CreateAssembly(string name, IIntermediateCliManager identityManager, CSharpLanguageVersion version);
        /// <summary>
        /// Returns the <see cref="ILanguageVendor"/> of a particular language.
        /// </summary>
        new IMicrosoftLanguageVendor Vendor { get; }
    }
}
