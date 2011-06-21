using System.Globalization;
/*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Globalization
{
    /// <summary>
    /// Defines properties and methods for working with 
    /// a culture identifier which denotes the neutral 
    /// language of an assembly.
    /// </summary>
    //[TypeConverter(typeof(CultureIdentifierTypeConverter))]
    public interface ICultureIdentifier
    {
        /// <summary>
        /// Returns the short-hand name of the culture.
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Returns the numerical identifier of the culture.
        /// </summary>
        int Culture { get; }
        /// <summary>
        /// Returns the readable user-friendly description of the country/region.
        /// </summary>
        string CountryRegion { get; }
        /// <summary>
        /// Obtains a <see cref="CultureInfo"/> instance relative to the <see cref="ICultureIdentifier"/>.
        /// </summary>
        /// <returns>A <see cref="CultureInfo"/> relative to the <see cref="Culture"/> of the
        /// <see cref="ICultureIdentifier"/> implementation.</returns>
        CultureInfo GetCultureInfo();
    }
}
