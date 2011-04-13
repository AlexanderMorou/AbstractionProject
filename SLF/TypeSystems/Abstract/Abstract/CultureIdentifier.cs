using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// Provides a base <see cref="ICultureIdentifier"/> implementation which supports 
    /// the standard set.
    /// </summary>
    internal sealed class CultureIdentifier :
        ICultureIdentifier
    {
        /// <summary>
        /// Data member for <see cref="Name"/>.
        /// </summary>
        private readonly string name;
        /// <summary>
        /// Data member for <see cref="Culture"/>.
        /// </summary>
        private readonly int culture;
        /// <summary>
        /// Data member for <see cref="CountryRegion"/>.
        /// </summary>
        private readonly string countryRegion;
        /// <summary>
        /// Data member for <see cref="LocalizedName"/>
        /// </summary>
        private readonly string localizedName;

        internal CultureIdentifier(string name, int culture, string localizedName, string countryRegion)
            : this(name, culture, countryRegion)
        {
            this.localizedName = localizedName;
        }

        internal CultureIdentifier(string name, int culture, string countryRegion)
        {
            this.name = name;
            this.culture = culture;
            this.countryRegion = countryRegion;
            this.localizedName = null;
        }

        #region ICultureIdentifier Members

        /// <summary>
        /// Returns the short-hand name of the culture.
        /// </summary>
        public string Name
        {
            get { return this.name; ; }
        }

        /// <summary>
        /// Returns the numerical identifier of the culture.
        /// </summary>
        public int Culture
        {
            get { return this.culture; }
        }

        /// <summary>
        /// Returns the readable user-friendly description of the country/region.
        /// </summary>
        public string CountryRegion
        {
            get { return this.countryRegion; }
        }

        /// <summary>
        /// Obtains a <see cref="CultureInfo"/> instance relative to the <see cref="ICultureIdentifier"/>.
        /// </summary>
        /// <returns>A <see cref="CultureInfo"/> relative to the <see cref="Culture"/> of the
        /// <see cref="CultureIdentifier"/> instance.</returns>
        public CultureInfo GetCultureInfo()
        {
            return CultureInfo.GetCultureInfo(this.Culture);
        }

        public string LocalizedName
        {
            get
            {
                return this.localizedName;
            }
        }

        #endregion

        public override string ToString()
        {
            return this.LocalizedName == null ? this.CountryRegion : this.LocalizedName;
        }
    }
}
