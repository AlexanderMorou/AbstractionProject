using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil;

namespace AllenCopeland.Abstraction.Slf.Languages
{
    /// <summary>
    /// Defines properties and methods for working with the
    /// common intermediate language, the root language of the 
    /// common language infrastructure.
    /// </summary>
    public interface ICommonIntermediateLanguage :
        ILanguage
    {
        /// <summary>
        /// Returns the <see cref="IMicrosoftLanguageVendor">Microsoft</see> vendor.
        /// </summary>
        IMicrosoftLanguageVendor Vendor { get; }
        /// <summary>
        /// Returns a new <see cref="ICommonIntermediateProvider"/> associated to the current
        /// <see cref="ILanguage"/>.
        /// </summary>
        /// <returns>A new <see cref="ICommonIntermediateProvider"/> for the
        /// <see cref="ICommonIntermediateLanguage">Common Intermediate Language</see>.</returns>
        new ICommonIntermediateProvider GetProvider();
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
        ICommonIntermediateAssembly CreateAssembly(string name);
    }
}
