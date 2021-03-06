using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace AllenCopeland.Abstraction.OldCodeGen
{
    /// <summary>
    /// Provides access to all variants of the <see cref="CultureIdentifier"/> instances.
    /// </summary>
    public static partial class CultureIdentifiers
    {
        internal static IDictionary<int, CultureIdentifier> defaultCultureIDByCultureNumber;
        internal static IDictionary<string, CultureIdentifier> defaultCultureIDByCultureName;
        internal static IDictionary<int, CultureIdentifier> DefaultCultureIDByCultureNumber
        {
            get
            {
                if (CultureIdentifiers.defaultCultureIDByCultureNumber == null)
                    CultureIdentifiers.StaticCheck();
                return CultureIdentifiers.defaultCultureIDByCultureNumber;
            }
        }

        internal static IDictionary<string, CultureIdentifier> DefaultCultureIDByCultureName
        {
            get
            {
                if (CultureIdentifiers.defaultCultureIDByCultureName == null)
                    CultureIdentifiers.StaticCheck();
                return CultureIdentifiers.defaultCultureIDByCultureName;
            }
        }

        private static void StaticCheck()
        {
            if (CultureIdentifiers.defaultCultureIDByCultureNumber == null)
                CultureIdentifiers.AcquireCultures();
        }

        /// <summary>
        /// Obtains a <see cref="ICultureIdentifier"/> by its
        /// culture <paramref name="name"/>.
        /// </summary>
        /// <param name="name">The name of the culture to retrieve
        /// the culture identifier of.</param>
        /// <returns>The <see cref="ICultureIdentifier"/> instance relative
        /// to the culture <paramref name="name"/> provided.</returns>
        public static ICultureIdentifier GetIdentifierByName(string name)
        {
            if (CultureIdentifiers.DefaultCultureIDByCultureName.ContainsKey(name))
                return CultureIdentifiers.DefaultCultureIDByCultureName[name];
            else
                throw new ArgumentOutOfRangeException("name");
        }

        /// <summary>
        /// Obtains a <see cref="ICultureIdentifier"/> by its
        /// culture <paramref name="id"/>.
        /// </summary>
        /// <param name="id">The <see cref="Int32"/> value relative
        /// to the culture identifier to retrieve.</param>
        /// <returns>The <see cref="ICultureIdentifier"/> instance relative
        /// to the culture <paramref name="id"/> provided.</returns>
        public static ICultureIdentifier GetIdentifierById(int id)
        {
            if (CultureIdentifiers.DefaultCultureIDByCultureNumber.ContainsKey(id))
                return CultureIdentifiers.DefaultCultureIDByCultureNumber[id];
            else
                throw new ArgumentOutOfRangeException("id");
        }

        ///<summary>
        ///Static culture identifier instance for None.
        ///</summary>
        ///<remarks>Culture ID: 0x007F
        ///Culture Name: <see cref="System.String.Empty"/></remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        public static readonly ICultureIdentifier None = new CultureIdentifier(CultureIdentifiers.CultureCodes.None, CultureIdentifiers.NumericIdentifiers.None, CultureIdentifiers.LocalizedNames.None, CultureIdentifiers.CountryRegions.None);
        ///<summary>
        ///Static culture identifier instance for Afrikaans.
        ///</summary>
        ///<remarks>Culture ID: 0x0036
        ///Culture Name:af</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        public static readonly ICultureIdentifier Afrikaans = new CultureIdentifier(CultureIdentifiers.CultureCodes.Afrikaans, CultureIdentifiers.NumericIdentifiers.Afrikaans, CultureIdentifiers.LocalizedNames.Afrikaans, CultureIdentifiers.CountryRegions.Afrikaans);
        ///<summary>
        ///Static culture identifier instance for Afrikaans - South Africa.
        ///</summary>
        ///<remarks>Culture ID: 0x0436
        ///Culture Name:af-ZA</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Afrikaans_SouthAfrica = new CultureIdentifier(CultureIdentifiers.CultureCodes.Afrikaans_SouthAfrica, CultureIdentifiers.NumericIdentifiers.Afrikaans_SouthAfrica, CultureIdentifiers.LocalizedNames.Afrikaans_SouthAfrica, CultureIdentifiers.CountryRegions.Afrikaans_SouthAfrica);
        ///<summary>
        ///Static culture identifier instance for Albanian.
        ///</summary>
        ///<remarks>Culture ID: 0x001C
        ///Culture Name:sq</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Albanian = new CultureIdentifier(CultureIdentifiers.CultureCodes.Albanian, CultureIdentifiers.NumericIdentifiers.Albanian, CultureIdentifiers.LocalizedNames.Albanian, CultureIdentifiers.CountryRegions.Albanian);
        ///<summary>
        ///Static culture identifier instance for Albanian - Albania.
        ///</summary>
        ///<remarks>Culture ID: 0x041C
        ///Culture Name:sq-AL</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Albanian_Albania = new CultureIdentifier(CultureIdentifiers.CultureCodes.Albanian_Albania, CultureIdentifiers.NumericIdentifiers.Albanian_Albania, CultureIdentifiers.LocalizedNames.Albanian_Albania, CultureIdentifiers.CountryRegions.Albanian_Albania);
        ///<summary>
        ///Static culture identifier instance for Arabic.
        ///</summary>
        ///<remarks>Culture ID: 0x0001
        ///Culture Name:ar</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        public static readonly ICultureIdentifier Arabic = new CultureIdentifier(CultureIdentifiers.CultureCodes.Arabic, CultureIdentifiers.NumericIdentifiers.Arabic, CultureIdentifiers.LocalizedNames.Arabic, CultureIdentifiers.CountryRegions.Arabic);
        ///<summary>
        ///Static culture identifier instance for Arabic - Algeria.
        ///</summary>
        ///<remarks>Culture ID: 0x1401
        ///Culture Name:ar-DZ</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Arabic_Algeria = new CultureIdentifier(CultureIdentifiers.CultureCodes.Arabic_Algeria, CultureIdentifiers.NumericIdentifiers.Arabic_Algeria, CultureIdentifiers.LocalizedNames.Arabic_Algeria, CultureIdentifiers.CountryRegions.Arabic_Algeria);
        ///<summary>
        ///Static culture identifier instance for Arabic - Bahrain.
        ///</summary>
        ///<remarks>Culture ID: 0x3C01
        ///Culture Name:ar-BH</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Arabic_Bahrain = new CultureIdentifier(CultureIdentifiers.CultureCodes.Arabic_Bahrain, CultureIdentifiers.NumericIdentifiers.Arabic_Bahrain, CultureIdentifiers.LocalizedNames.Arabic_Bahrain, CultureIdentifiers.CountryRegions.Arabic_Bahrain);
        ///<summary>
        ///Static culture identifier instance for Arabic - Egypt.
        ///</summary>
        ///<remarks>Culture ID: 0x0C01
        ///Culture Name:ar-EG</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Arabic_Egypt = new CultureIdentifier(CultureIdentifiers.CultureCodes.Arabic_Egypt, CultureIdentifiers.NumericIdentifiers.Arabic_Egypt, CultureIdentifiers.LocalizedNames.Arabic_Egypt, CultureIdentifiers.CountryRegions.Arabic_Egypt);
        ///<summary>
        ///Static culture identifier instance for Arabic - Iraq.
        ///</summary>
        ///<remarks>Culture ID: 0x0801
        ///Culture Name:ar-IQ</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Arabic_Iraq = new CultureIdentifier(CultureIdentifiers.CultureCodes.Arabic_Iraq, CultureIdentifiers.NumericIdentifiers.Arabic_Iraq, CultureIdentifiers.LocalizedNames.Arabic_Iraq, CultureIdentifiers.CountryRegions.Arabic_Iraq);
        ///<summary>
        ///Static culture identifier instance for Arabic - Jordan.
        ///</summary>
        ///<remarks>Culture ID: 0x2C01
        ///Culture Name:ar-JO</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Arabic_Jordan = new CultureIdentifier(CultureIdentifiers.CultureCodes.Arabic_Jordan, CultureIdentifiers.NumericIdentifiers.Arabic_Jordan, CultureIdentifiers.LocalizedNames.Arabic_Jordan, CultureIdentifiers.CountryRegions.Arabic_Jordan);
        ///<summary>
        ///Static culture identifier instance for Arabic - Kuwait.
        ///</summary>
        ///<remarks>Culture ID: 0x3401
        ///Culture Name:ar-KW</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Arabic_Kuwait = new CultureIdentifier(CultureIdentifiers.CultureCodes.Arabic_Kuwait, CultureIdentifiers.NumericIdentifiers.Arabic_Kuwait, CultureIdentifiers.LocalizedNames.Arabic_Kuwait, CultureIdentifiers.CountryRegions.Arabic_Kuwait);
        ///<summary>
        ///Static culture identifier instance for Arabic - Lebanon.
        ///</summary>
        ///<remarks>Culture ID: 0x3001
        ///Culture Name:ar-LB</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Arabic_Lebanon = new CultureIdentifier(CultureIdentifiers.CultureCodes.Arabic_Lebanon, CultureIdentifiers.NumericIdentifiers.Arabic_Lebanon, CultureIdentifiers.LocalizedNames.Arabic_Lebanon, CultureIdentifiers.CountryRegions.Arabic_Lebanon);
        ///<summary>
        ///Static culture identifier instance for Arabic - Libya.
        ///</summary>
        ///<remarks>Culture ID: 0x1001
        ///Culture Name:ar-LY</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Arabic_Libya = new CultureIdentifier(CultureIdentifiers.CultureCodes.Arabic_Libya, CultureIdentifiers.NumericIdentifiers.Arabic_Libya, CultureIdentifiers.LocalizedNames.Arabic_Libya, CultureIdentifiers.CountryRegions.Arabic_Libya);
        ///<summary>
        ///Static culture identifier instance for Arabic - Morocco.
        ///</summary>
        ///<remarks>Culture ID: 0x1801
        ///Culture Name:ar-MA</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Arabic_Morocco = new CultureIdentifier(CultureIdentifiers.CultureCodes.Arabic_Morocco, CultureIdentifiers.NumericIdentifiers.Arabic_Morocco, CultureIdentifiers.LocalizedNames.Arabic_Morocco, CultureIdentifiers.CountryRegions.Arabic_Morocco);
        ///<summary>
        ///Static culture identifier instance for Arabic - Oman.
        ///</summary>
        ///<remarks>Culture ID: 0x2001
        ///Culture Name:ar-OM</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Arabic_Oman = new CultureIdentifier(CultureIdentifiers.CultureCodes.Arabic_Oman, CultureIdentifiers.NumericIdentifiers.Arabic_Oman, CultureIdentifiers.LocalizedNames.Arabic_Oman, CultureIdentifiers.CountryRegions.Arabic_Oman);
        ///<summary>
        ///Static culture identifier instance for Arabic - Qatar.
        ///</summary>
        ///<remarks>Culture ID: 0x4001
        ///Culture Name:ar-QA</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Arabic_Qatar = new CultureIdentifier(CultureIdentifiers.CultureCodes.Arabic_Qatar, CultureIdentifiers.NumericIdentifiers.Arabic_Qatar, CultureIdentifiers.LocalizedNames.Arabic_Qatar, CultureIdentifiers.CountryRegions.Arabic_Qatar);
        ///<summary>
        ///Static culture identifier instance for Arabic - Saudi Arabia.
        ///</summary>
        ///<remarks>Culture ID: 0x0401
        ///Culture Name:ar-SA</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Arabic_SaudiArabia = new CultureIdentifier(CultureIdentifiers.CultureCodes.Arabic_SaudiArabia, CultureIdentifiers.NumericIdentifiers.Arabic_SaudiArabia, CultureIdentifiers.LocalizedNames.Arabic_SaudiArabia, CultureIdentifiers.CountryRegions.Arabic_SaudiArabia);
        ///<summary>
        ///Static culture identifier instance for Arabic - Syria.
        ///</summary>
        ///<remarks>Culture ID: 0x2801
        ///Culture Name:ar-SY</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Arabic_Syria = new CultureIdentifier(CultureIdentifiers.CultureCodes.Arabic_Syria, CultureIdentifiers.NumericIdentifiers.Arabic_Syria, CultureIdentifiers.LocalizedNames.Arabic_Syria, CultureIdentifiers.CountryRegions.Arabic_Syria);
        ///<summary>
        ///Static culture identifier instance for Arabic - Tunisia.
        ///</summary>
        ///<remarks>Culture ID: 0x1C01
        ///Culture Name:ar-TN</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Arabic_Tunisia = new CultureIdentifier(CultureIdentifiers.CultureCodes.Arabic_Tunisia, CultureIdentifiers.NumericIdentifiers.Arabic_Tunisia, CultureIdentifiers.LocalizedNames.Arabic_Tunisia, CultureIdentifiers.CountryRegions.Arabic_Tunisia);
        ///<summary>
        ///Static culture identifier instance for Arabic - United Arab Emirates.
        ///</summary>
        ///<remarks>Culture ID: 0x3801
        ///Culture Name:ar-AE</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Arabic_UnitedArabEmirates = new CultureIdentifier(CultureIdentifiers.CultureCodes.Arabic_UnitedArabEmirates, CultureIdentifiers.NumericIdentifiers.Arabic_UnitedArabEmirates, CultureIdentifiers.LocalizedNames.Arabic_UnitedArabEmirates, CultureIdentifiers.CountryRegions.Arabic_UnitedArabEmirates);
        ///<summary>
        ///Static culture identifier instance for Arabic - Yemen.
        ///</summary>
        ///<remarks>Culture ID: 0x2401
        ///Culture Name:ar-YE</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Arabic_Yemen = new CultureIdentifier(CultureIdentifiers.CultureCodes.Arabic_Yemen, CultureIdentifiers.NumericIdentifiers.Arabic_Yemen, CultureIdentifiers.LocalizedNames.Arabic_Yemen, CultureIdentifiers.CountryRegions.Arabic_Yemen);
        ///<summary>
        ///Static culture identifier instance for Armenian.
        ///</summary>
        ///<remarks>Culture ID: 0x002B
        ///Culture Name:hy</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        public static readonly ICultureIdentifier Armenian = new CultureIdentifier(CultureIdentifiers.CultureCodes.Armenian, CultureIdentifiers.NumericIdentifiers.Armenian, CultureIdentifiers.LocalizedNames.Armenian, CultureIdentifiers.CountryRegions.Armenian);
        ///<summary>
        ///Static culture identifier instance for Armenian - Armenia.
        ///</summary>
        ///<remarks>Culture ID: 0x042B
        ///Culture Name:hy-AM</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Armenian_Armenia = new CultureIdentifier(CultureIdentifiers.CultureCodes.Armenian_Armenia, CultureIdentifiers.NumericIdentifiers.Armenian_Armenia, CultureIdentifiers.LocalizedNames.Armenian_Armenia, CultureIdentifiers.CountryRegions.Armenian_Armenia);
        ///<summary>
        ///Static culture identifier instance for Azeri.
        ///</summary>
        ///<remarks>Culture ID: 0x002C
        ///Culture Name:az</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Azeri = new CultureIdentifier(CultureIdentifiers.CultureCodes.Azeri, CultureIdentifiers.NumericIdentifiers.Azeri, CultureIdentifiers.LocalizedNames.Azeri, CultureIdentifiers.CountryRegions.Azeri);
        ///<summary>
        ///Static culture identifier instance for Azeri (Cyrillic) - Azerbaijan.
        ///</summary>
        ///<remarks>Culture ID: 0x082C
        ///Culture Name:az-AZ-Cyrl</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Azeri_Cyrillic_Azerbaijan = new CultureIdentifier(CultureIdentifiers.CultureCodes.Azeri_Cyrillic_Azerbaijan, CultureIdentifiers.NumericIdentifiers.Azeri_Cyrillic_Azerbaijan, CultureIdentifiers.LocalizedNames.Azeri_Cyrillic_Azerbaijan, CultureIdentifiers.CountryRegions.Azeri_Cyrillic_Azerbaijan);
        ///<summary>
        ///Static culture identifier instance for Azeri (Latin) - Azerbaijan.
        ///</summary>
        ///<remarks>Culture ID: 0x042C
        ///Culture Name:az-AZ-Latn</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Azeri_Latin_Azerbaijan = new CultureIdentifier(CultureIdentifiers.CultureCodes.Azeri_Latin_Azerbaijan, CultureIdentifiers.NumericIdentifiers.Azeri_Latin_Azerbaijan, CultureIdentifiers.LocalizedNames.Azeri_Latin_Azerbaijan, CultureIdentifiers.CountryRegions.Azeri_Latin_Azerbaijan);
        ///<summary>
        ///Static culture identifier instance for Basque.
        ///</summary>
        ///<remarks>Culture ID: 0x002D
        ///Culture Name:eu</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        public static readonly ICultureIdentifier Basque = new CultureIdentifier(CultureIdentifiers.CultureCodes.Basque, CultureIdentifiers.NumericIdentifiers.Basque, CultureIdentifiers.LocalizedNames.Basque, CultureIdentifiers.CountryRegions.Basque);
        ///<summary>
        ///Static culture identifier instance for Basque - Basque.
        ///</summary>
        ///<remarks>Culture ID: 0x042D
        ///Culture Name:eu-ES</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Basque_Basque = new CultureIdentifier(CultureIdentifiers.CultureCodes.Basque_Basque, CultureIdentifiers.NumericIdentifiers.Basque_Basque, CultureIdentifiers.LocalizedNames.Basque_Basque, CultureIdentifiers.CountryRegions.Basque_Basque);
        ///<summary>
        ///Static culture identifier instance for Belarusian.
        ///</summary>
        ///<remarks>Culture ID: 0x0023
        ///Culture Name:be</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Belarusian = new CultureIdentifier(CultureIdentifiers.CultureCodes.Belarusian, CultureIdentifiers.NumericIdentifiers.Belarusian, CultureIdentifiers.LocalizedNames.Belarusian, CultureIdentifiers.CountryRegions.Belarusian);
        ///<summary>
        ///Static culture identifier instance for Belarusian - Belarus.
        ///</summary>
        ///<remarks>Culture ID: 0x0423
        ///Culture Name:be-BY</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Belarusian_Belarus = new CultureIdentifier(CultureIdentifiers.CultureCodes.Belarusian_Belarus, CultureIdentifiers.NumericIdentifiers.Belarusian_Belarus, CultureIdentifiers.LocalizedNames.Belarusian_Belarus, CultureIdentifiers.CountryRegions.Belarusian_Belarus);
        ///<summary>
        ///Static culture identifier instance for Bulgarian.
        ///</summary>
        ///<remarks>Culture ID: 0x0002
        ///Culture Name:bg</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        public static readonly ICultureIdentifier Bulgarian = new CultureIdentifier(CultureIdentifiers.CultureCodes.Bulgarian, CultureIdentifiers.NumericIdentifiers.Bulgarian, CultureIdentifiers.LocalizedNames.Bulgarian, CultureIdentifiers.CountryRegions.Bulgarian);
        ///<summary>
        ///Static culture identifier instance for Bulgarian - Bulgaria.
        ///</summary>
        ///<remarks>Culture ID: 0x0402
        ///Culture Name:bg-BG</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Bulgarian_Bulgaria = new CultureIdentifier(CultureIdentifiers.CultureCodes.Bulgarian_Bulgaria, CultureIdentifiers.NumericIdentifiers.Bulgarian_Bulgaria, CultureIdentifiers.LocalizedNames.Bulgarian_Bulgaria, CultureIdentifiers.CountryRegions.Bulgarian_Bulgaria);
        ///<summary>
        ///Static culture identifier instance for Catalan.
        ///</summary>
        ///<remarks>Culture ID: 0x0003
        ///Culture Name:ca</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        public static readonly ICultureIdentifier Catalan = new CultureIdentifier(CultureIdentifiers.CultureCodes.Catalan, CultureIdentifiers.NumericIdentifiers.Catalan, CultureIdentifiers.LocalizedNames.Catalan, CultureIdentifiers.CountryRegions.Catalan);
        ///<summary>
        ///Static culture identifier instance for Catalan - Catalan.
        ///</summary>
        ///<remarks>Culture ID: 0x0403
        ///Culture Name:ca-ES</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Catalan_Catalan = new CultureIdentifier(CultureIdentifiers.CultureCodes.Catalan_Catalan, CultureIdentifiers.NumericIdentifiers.Catalan_Catalan, CultureIdentifiers.LocalizedNames.Catalan_Catalan, CultureIdentifiers.CountryRegions.Catalan_Catalan);
        ///<summary>
        ///Static culture identifier instance for Chinese - Hong Kong SAR.
        ///</summary>
        ///<remarks>Culture ID: 0x0C04
        ///Culture Name:zh-HK</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "SAR")]
        public static readonly ICultureIdentifier Chinese_HongKongSAR = new CultureIdentifier(CultureIdentifiers.CultureCodes.Chinese_HongKongSAR, CultureIdentifiers.NumericIdentifiers.Chinese_HongKongSAR, CultureIdentifiers.LocalizedNames.Chinese_HongKongSAR, CultureIdentifiers.CountryRegions.Chinese_HongKongSAR);
        ///<summary>
        ///Static culture identifier instance for Chinese - Macao SAR.
        ///</summary>
        ///<remarks>Culture ID: 0x1404
        ///Culture Name:zh-MO</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "SAR")]
        public static readonly ICultureIdentifier Chinese_MacaoSAR = new CultureIdentifier(CultureIdentifiers.CultureCodes.Chinese_MacaoSAR, CultureIdentifiers.NumericIdentifiers.Chinese_MacaoSAR, CultureIdentifiers.LocalizedNames.Chinese_MacaoSAR, CultureIdentifiers.CountryRegions.Chinese_MacaoSAR);
        ///<summary>
        ///Static culture identifier instance for Chinese - China.
        ///</summary>
        ///<remarks>Culture ID: 0x0804
        ///Culture Name:zh-CN</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Chinese_China = new CultureIdentifier(CultureIdentifiers.CultureCodes.Chinese_China, CultureIdentifiers.NumericIdentifiers.Chinese_China, CultureIdentifiers.LocalizedNames.Chinese_China, CultureIdentifiers.CountryRegions.Chinese_China);
        ///<summary>
        ///Static culture identifier instance for Chinese (Simplified).
        ///</summary>
        ///<remarks>Culture ID: 0x0004
        ///Culture Name:zh-CHS</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Chinese_Simplified = new CultureIdentifier(CultureIdentifiers.CultureCodes.Chinese_Simplified, CultureIdentifiers.NumericIdentifiers.Chinese_Simplified, CultureIdentifiers.LocalizedNames.Chinese_Simplified, CultureIdentifiers.CountryRegions.Chinese_Simplified);
        ///<summary>
        ///Static culture identifier instance for Chinese - Singapore.
        ///</summary>
        ///<remarks>Culture ID: 0x1004
        ///Culture Name:zh-SG</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Chinese_Singapore = new CultureIdentifier(CultureIdentifiers.CultureCodes.Chinese_Singapore, CultureIdentifiers.NumericIdentifiers.Chinese_Singapore, CultureIdentifiers.LocalizedNames.Chinese_Singapore, CultureIdentifiers.CountryRegions.Chinese_Singapore);
        ///<summary>
        ///Static culture identifier instance for Chinese - Taiwan.
        ///</summary>
        ///<remarks>Culture ID: 0x0404
        ///Culture Name:zh-TW</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Chinese_Taiwan = new CultureIdentifier(CultureIdentifiers.CultureCodes.Chinese_Taiwan, CultureIdentifiers.NumericIdentifiers.Chinese_Taiwan, CultureIdentifiers.LocalizedNames.Chinese_Taiwan, CultureIdentifiers.CountryRegions.Chinese_Taiwan);
        ///<summary>
        ///Static culture identifier instance for Chinese (Traditional).
        ///</summary>
        ///<remarks>Culture ID: 0x7C04
        ///Culture Name:zh-CHT</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Chinese_Traditional = new CultureIdentifier(CultureIdentifiers.CultureCodes.Chinese_Traditional, CultureIdentifiers.NumericIdentifiers.Chinese_Traditional, CultureIdentifiers.LocalizedNames.Chinese_Traditional, CultureIdentifiers.CountryRegions.Chinese_Traditional);
        ///<summary>
        ///Static culture identifier instance for Croatian.
        ///</summary>
        ///<remarks>Culture ID: 0x001A
        ///Culture Name:hr</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        public static readonly ICultureIdentifier Croatian = new CultureIdentifier(CultureIdentifiers.CultureCodes.Croatian, CultureIdentifiers.NumericIdentifiers.Croatian, CultureIdentifiers.LocalizedNames.Croatian, CultureIdentifiers.CountryRegions.Croatian);
        ///<summary>
        ///Static culture identifier instance for Croatian - Croatia.
        ///</summary>
        ///<remarks>Culture ID: 0x041A
        ///Culture Name:hr-HR</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Croatian_Croatia = new CultureIdentifier(CultureIdentifiers.CultureCodes.Croatian_Croatia, CultureIdentifiers.NumericIdentifiers.Croatian_Croatia, CultureIdentifiers.LocalizedNames.Croatian_Croatia, CultureIdentifiers.CountryRegions.Croatian_Croatia);
        ///<summary>
        ///Static culture identifier instance for Czech.
        ///</summary>
        ///<remarks>Culture ID: 0x0005
        ///Culture Name:cs</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        public static readonly ICultureIdentifier Czech = new CultureIdentifier(CultureIdentifiers.CultureCodes.Czech, CultureIdentifiers.NumericIdentifiers.Czech, CultureIdentifiers.LocalizedNames.Czech, CultureIdentifiers.CountryRegions.Czech);
        ///<summary>
        ///Static culture identifier instance for Czech - Czech Republic.
        ///</summary>
        ///<remarks>Culture ID: 0x0405
        ///Culture Name:cs-CZ</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Czech_CzechRepublic = new CultureIdentifier(CultureIdentifiers.CultureCodes.Czech_CzechRepublic, CultureIdentifiers.NumericIdentifiers.Czech_CzechRepublic, CultureIdentifiers.LocalizedNames.Czech_CzechRepublic, CultureIdentifiers.CountryRegions.Czech_CzechRepublic);
        ///<summary>
        ///Static culture identifier instance for Danish.
        ///</summary>
        ///<remarks>Culture ID: 0x0006
        ///Culture Name:da</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        public static readonly ICultureIdentifier Danish = new CultureIdentifier(CultureIdentifiers.CultureCodes.Danish, CultureIdentifiers.NumericIdentifiers.Danish, CultureIdentifiers.LocalizedNames.Danish, CultureIdentifiers.CountryRegions.Danish);
        ///<summary>
        ///Static culture identifier instance for Danish - Denmark.
        ///</summary>
        ///<remarks>Culture ID: 0x0406
        ///Culture Name:da-DK</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Danish_Denmark = new CultureIdentifier(CultureIdentifiers.CultureCodes.Danish_Denmark, CultureIdentifiers.NumericIdentifiers.Danish_Denmark, CultureIdentifiers.LocalizedNames.Danish_Denmark, CultureIdentifiers.CountryRegions.Danish_Denmark);
        ///<summary>
        ///Static culture identifier instance for Dhivehi.
        ///</summary>
        ///<remarks>Culture ID: 0x0065
        ///Culture Name:div</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        public static readonly ICultureIdentifier Dhivehi = new CultureIdentifier(CultureIdentifiers.CultureCodes.Dhivehi, CultureIdentifiers.NumericIdentifiers.Dhivehi, CultureIdentifiers.LocalizedNames.Dhivehi, CultureIdentifiers.CountryRegions.Dhivehi);
        ///<summary>
        ///Static culture identifier instance for Dhivehi - Maldives.
        ///</summary>
        ///<remarks>Culture ID: 0x0465
        ///Culture Name:div-MV</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Dhivehi_Maldives = new CultureIdentifier(CultureIdentifiers.CultureCodes.Dhivehi_Maldives, CultureIdentifiers.NumericIdentifiers.Dhivehi_Maldives, CultureIdentifiers.LocalizedNames.Dhivehi_Maldives, CultureIdentifiers.CountryRegions.Dhivehi_Maldives);
        ///<summary>
        ///Static culture identifier instance for Dutch.
        ///</summary>
        ///<remarks>Culture ID: 0x0013
        ///Culture Name:nl</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        public static readonly ICultureIdentifier Dutch = new CultureIdentifier(CultureIdentifiers.CultureCodes.Dutch, CultureIdentifiers.NumericIdentifiers.Dutch, CultureIdentifiers.LocalizedNames.Dutch, CultureIdentifiers.CountryRegions.Dutch);
        ///<summary>
        ///Static culture identifier instance for Dutch - Belgium.
        ///</summary>
        ///<remarks>Culture ID: 0x0813
        ///Culture Name:nl-BE</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Dutch_Belgium = new CultureIdentifier(CultureIdentifiers.CultureCodes.Dutch_Belgium, CultureIdentifiers.NumericIdentifiers.Dutch_Belgium, CultureIdentifiers.LocalizedNames.Dutch_Belgium, CultureIdentifiers.CountryRegions.Dutch_Belgium);
        ///<summary>
        ///Static culture identifier instance for Dutch - The Netherlands.
        ///</summary>
        ///<remarks>Culture ID: 0x0413
        ///Culture Name:nl-NL</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Dutch_TheNetherlands = new CultureIdentifier(CultureIdentifiers.CultureCodes.Dutch_TheNetherlands, CultureIdentifiers.NumericIdentifiers.Dutch_TheNetherlands, CultureIdentifiers.LocalizedNames.Dutch_TheNetherlands, CultureIdentifiers.CountryRegions.Dutch_TheNetherlands);
        ///<summary>
        ///Static culture identifier instance for English.
        ///</summary>
        ///<remarks>Culture ID: 0x0009
        ///Culture Name:en</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        public static readonly ICultureIdentifier English = new CultureIdentifier(CultureIdentifiers.CultureCodes.English, CultureIdentifiers.NumericIdentifiers.English, CultureIdentifiers.LocalizedNames.English, CultureIdentifiers.CountryRegions.English);
        ///<summary>
        ///Static culture identifier instance for English - Australia.
        ///</summary>
        ///<remarks>Culture ID: 0x0C09
        ///Culture Name:en-AU</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier English_Australia = new CultureIdentifier(CultureIdentifiers.CultureCodes.English_Australia, CultureIdentifiers.NumericIdentifiers.English_Australia, CultureIdentifiers.LocalizedNames.English_Australia, CultureIdentifiers.CountryRegions.English_Australia);
        ///<summary>
        ///Static culture identifier instance for English - Belize.
        ///</summary>
        ///<remarks>Culture ID: 0x2809
        ///Culture Name:en-BZ</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier English_Belize = new CultureIdentifier(CultureIdentifiers.CultureCodes.English_Belize, CultureIdentifiers.NumericIdentifiers.English_Belize, CultureIdentifiers.LocalizedNames.English_Belize, CultureIdentifiers.CountryRegions.English_Belize);
        ///<summary>
        ///Static culture identifier instance for English - Canada.
        ///</summary>
        ///<remarks>Culture ID: 0x1009
        ///Culture Name:en-CA</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier English_Canada = new CultureIdentifier(CultureIdentifiers.CultureCodes.English_Canada, CultureIdentifiers.NumericIdentifiers.English_Canada, CultureIdentifiers.LocalizedNames.English_Canada, CultureIdentifiers.CountryRegions.English_Canada);
        ///<summary>
        ///Static culture identifier instance for English - Caribbean.
        ///</summary>
        ///<remarks>Culture ID: 0x2409
        ///Culture Name:en-CB</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier English_Caribbean = new CultureIdentifier(CultureIdentifiers.CultureCodes.English_Caribbean, CultureIdentifiers.NumericIdentifiers.English_Caribbean, CultureIdentifiers.LocalizedNames.English_Caribbean, CultureIdentifiers.CountryRegions.English_Caribbean);
        ///<summary>
        ///Static culture identifier instance for English - Ireland.
        ///</summary>
        ///<remarks>Culture ID: 0x1809
        ///Culture Name:en-IE</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier English_Ireland = new CultureIdentifier(CultureIdentifiers.CultureCodes.English_Ireland, CultureIdentifiers.NumericIdentifiers.English_Ireland, CultureIdentifiers.LocalizedNames.English_Ireland, CultureIdentifiers.CountryRegions.English_Ireland);
        ///<summary>
        ///Static culture identifier instance for English - Jamaica.
        ///</summary>
        ///<remarks>Culture ID: 0x2009
        ///Culture Name:en-JM</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier English_Jamaica = new CultureIdentifier(CultureIdentifiers.CultureCodes.English_Jamaica, CultureIdentifiers.NumericIdentifiers.English_Jamaica, CultureIdentifiers.LocalizedNames.English_Jamaica, CultureIdentifiers.CountryRegions.English_Jamaica);
        ///<summary>
        ///Static culture identifier instance for English - New Zealand.
        ///</summary>
        ///<remarks>Culture ID: 0x1409
        ///Culture Name:en-NZ</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier English_NewZealand = new CultureIdentifier(CultureIdentifiers.CultureCodes.English_NewZealand, CultureIdentifiers.NumericIdentifiers.English_NewZealand, CultureIdentifiers.LocalizedNames.English_NewZealand, CultureIdentifiers.CountryRegions.English_NewZealand);
        ///<summary>
        ///Static culture identifier instance for English - Philippines.
        ///</summary>
        ///<remarks>Culture ID: 0x3409
        ///Culture Name:en-PH</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier English_Philippines = new CultureIdentifier(CultureIdentifiers.CultureCodes.English_Philippines, CultureIdentifiers.NumericIdentifiers.English_Philippines, CultureIdentifiers.LocalizedNames.English_Philippines, CultureIdentifiers.CountryRegions.English_Philippines);
        ///<summary>
        ///Static culture identifier instance for English - South Africa.
        ///</summary>
        ///<remarks>Culture ID: 0x1C09
        ///Culture Name:en-ZA</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier English_SouthAfrica = new CultureIdentifier(CultureIdentifiers.CultureCodes.English_SouthAfrica, CultureIdentifiers.NumericIdentifiers.English_SouthAfrica, CultureIdentifiers.LocalizedNames.English_SouthAfrica, CultureIdentifiers.CountryRegions.English_SouthAfrica);
        ///<summary>
        ///Static culture identifier instance for English - Trinidad and Tobago.
        ///</summary>
        ///<remarks>Culture ID: 0x2C09
        ///Culture Name:en-TT</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier English_TrinidadAndTobago = new CultureIdentifier(CultureIdentifiers.CultureCodes.English_TrinidadAndTobago, CultureIdentifiers.NumericIdentifiers.English_TrinidadAndTobago, CultureIdentifiers.LocalizedNames.English_TrinidadAndTobago, CultureIdentifiers.CountryRegions.English_TrinidadAndTobago);
        ///<summary>
        ///Static culture identifier instance for English - United Kingdom.
        ///</summary>
        ///<remarks>Culture ID: 0x0809
        ///Culture Name:en-GB</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier English_UnitedKingdom = new CultureIdentifier(CultureIdentifiers.CultureCodes.English_UnitedKingdom, CultureIdentifiers.NumericIdentifiers.English_UnitedKingdom, CultureIdentifiers.LocalizedNames.English_UnitedKingdom, CultureIdentifiers.CountryRegions.English_UnitedKingdom);
        ///<summary>
        ///Static culture identifier instance for English - United States.
        ///</summary>
        ///<remarks>Culture ID: 0x0409
        ///Culture Name:en-US</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier English_UnitedStates = new CultureIdentifier(CultureIdentifiers.CultureCodes.English_UnitedStates, CultureIdentifiers.NumericIdentifiers.English_UnitedStates, CultureIdentifiers.LocalizedNames.English_UnitedStates, CultureIdentifiers.CountryRegions.English_UnitedStates);
        ///<summary>
        ///Static culture identifier instance for English - Zimbabwe.
        ///</summary>
        ///<remarks>Culture ID: 0x3009
        ///Culture Name:en-ZW</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier English_Zimbabwe = new CultureIdentifier(CultureIdentifiers.CultureCodes.English_Zimbabwe, CultureIdentifiers.NumericIdentifiers.English_Zimbabwe, CultureIdentifiers.LocalizedNames.English_Zimbabwe, CultureIdentifiers.CountryRegions.English_Zimbabwe);
        ///<summary>
        ///Static culture identifier instance for Estonian.
        ///</summary>
        ///<remarks>Culture ID: 0x0025
        ///Culture Name:et</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        public static readonly ICultureIdentifier Estonian = new CultureIdentifier(CultureIdentifiers.CultureCodes.Estonian, CultureIdentifiers.NumericIdentifiers.Estonian, CultureIdentifiers.LocalizedNames.Estonian, CultureIdentifiers.CountryRegions.Estonian);
        ///<summary>
        ///Static culture identifier instance for Estonian - Estonia.
        ///</summary>
        ///<remarks>Culture ID: 0x0425
        ///Culture Name:et-EE</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Estonian_Estonia = new CultureIdentifier(CultureIdentifiers.CultureCodes.Estonian_Estonia, CultureIdentifiers.NumericIdentifiers.Estonian_Estonia, CultureIdentifiers.LocalizedNames.Estonian_Estonia, CultureIdentifiers.CountryRegions.Estonian_Estonia);
        ///<summary>
        ///Static culture identifier instance for Faroese.
        ///</summary>
        ///<remarks>Culture ID: 0x0038
        ///Culture Name:fo</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        public static readonly ICultureIdentifier Faroese = new CultureIdentifier(CultureIdentifiers.CultureCodes.Faroese, CultureIdentifiers.NumericIdentifiers.Faroese, CultureIdentifiers.LocalizedNames.Faroese, CultureIdentifiers.CountryRegions.Faroese);
        ///<summary>
        ///Static culture identifier instance for Faroese - Faroe Islands.
        ///</summary>
        ///<remarks>Culture ID: 0x0438
        ///Culture Name:fo-FO</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Faroese_FaroeIslands = new CultureIdentifier(CultureIdentifiers.CultureCodes.Faroese_FaroeIslands, CultureIdentifiers.NumericIdentifiers.Faroese_FaroeIslands, CultureIdentifiers.LocalizedNames.Faroese_FaroeIslands, CultureIdentifiers.CountryRegions.Faroese_FaroeIslands);
        ///<summary>
        ///Static culture identifier instance for Farsi.
        ///</summary>
        ///<remarks>Culture ID: 0x0029
        ///Culture Name:fa</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        public static readonly ICultureIdentifier Farsi = new CultureIdentifier(CultureIdentifiers.CultureCodes.Farsi, CultureIdentifiers.NumericIdentifiers.Farsi, CultureIdentifiers.LocalizedNames.Farsi, CultureIdentifiers.CountryRegions.Farsi);
        ///<summary>
        ///Static culture identifier instance for Farsi - Iran.
        ///</summary>
        ///<remarks>Culture ID: 0x0429
        ///Culture Name:fa-IR</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Farsi_Iran = new CultureIdentifier(CultureIdentifiers.CultureCodes.Farsi_Iran, CultureIdentifiers.NumericIdentifiers.Farsi_Iran, CultureIdentifiers.LocalizedNames.Farsi_Iran, CultureIdentifiers.CountryRegions.Farsi_Iran);
        ///<summary>
        ///Static culture identifier instance for Finnish.
        ///</summary>
        ///<remarks>Culture ID: 0x000B
        ///Culture Name:fi</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        public static readonly ICultureIdentifier Finnish = new CultureIdentifier(CultureIdentifiers.CultureCodes.Finnish, CultureIdentifiers.NumericIdentifiers.Finnish, CultureIdentifiers.LocalizedNames.Finnish, CultureIdentifiers.CountryRegions.Finnish);
        ///<summary>
        ///Static culture identifier instance for Finnish - Finland.
        ///</summary>
        ///<remarks>Culture ID: 0x040B
        ///Culture Name:fi-FI</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Finnish_Finland = new CultureIdentifier(CultureIdentifiers.CultureCodes.Finnish_Finland, CultureIdentifiers.NumericIdentifiers.Finnish_Finland, CultureIdentifiers.LocalizedNames.Finnish_Finland, CultureIdentifiers.CountryRegions.Finnish_Finland);
        ///<summary>
        ///Static culture identifier instance for French.
        ///</summary>
        ///<remarks>Culture ID: 0x000C
        ///Culture Name:fr</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        public static readonly ICultureIdentifier French = new CultureIdentifier(CultureIdentifiers.CultureCodes.French, CultureIdentifiers.NumericIdentifiers.French, CultureIdentifiers.LocalizedNames.French, CultureIdentifiers.CountryRegions.French);
        ///<summary>
        ///Static culture identifier instance for French - Belgium.
        ///</summary>
        ///<remarks>Culture ID: 0x080C
        ///Culture Name:fr-BE</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier French_Belgium = new CultureIdentifier(CultureIdentifiers.CultureCodes.French_Belgium, CultureIdentifiers.NumericIdentifiers.French_Belgium, CultureIdentifiers.LocalizedNames.French_Belgium, CultureIdentifiers.CountryRegions.French_Belgium);
        ///<summary>
        ///Static culture identifier instance for French - Canada.
        ///</summary>
        ///<remarks>Culture ID: 0x0C0C
        ///Culture Name:fr-CA</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier French_Canada = new CultureIdentifier(CultureIdentifiers.CultureCodes.French_Canada, CultureIdentifiers.NumericIdentifiers.French_Canada, CultureIdentifiers.LocalizedNames.French_Canada, CultureIdentifiers.CountryRegions.French_Canada);
        ///<summary>
        ///Static culture identifier instance for French - France.
        ///</summary>
        ///<remarks>Culture ID: 0x040C
        ///Culture Name:fr-FR</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier French_France = new CultureIdentifier(CultureIdentifiers.CultureCodes.French_France, CultureIdentifiers.NumericIdentifiers.French_France, CultureIdentifiers.LocalizedNames.French_France, CultureIdentifiers.CountryRegions.French_France);
        ///<summary>
        ///Static culture identifier instance for French - Luxembourg.
        ///</summary>
        ///<remarks>Culture ID: 0x140C
        ///Culture Name:fr-LU</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier French_Luxembourg = new CultureIdentifier(CultureIdentifiers.CultureCodes.French_Luxembourg, CultureIdentifiers.NumericIdentifiers.French_Luxembourg, CultureIdentifiers.LocalizedNames.French_Luxembourg, CultureIdentifiers.CountryRegions.French_Luxembourg);
        ///<summary>
        ///Static culture identifier instance for French - Monaco.
        ///</summary>
        ///<remarks>Culture ID: 0x180C
        ///Culture Name:fr-MC</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier French_Monaco = new CultureIdentifier(CultureIdentifiers.CultureCodes.French_Monaco, CultureIdentifiers.NumericIdentifiers.French_Monaco, CultureIdentifiers.LocalizedNames.French_Monaco, CultureIdentifiers.CountryRegions.French_Monaco);
        ///<summary>
        ///Static culture identifier instance for French - Switzerland.
        ///</summary>
        ///<remarks>Culture ID: 0x100C
        ///Culture Name:fr-CH</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier French_Switzerland = new CultureIdentifier(CultureIdentifiers.CultureCodes.French_Switzerland, CultureIdentifiers.NumericIdentifiers.French_Switzerland, CultureIdentifiers.LocalizedNames.French_Switzerland, CultureIdentifiers.CountryRegions.French_Switzerland);
        ///<summary>
        ///Static culture identifier instance for Galician.
        ///</summary>
        ///<remarks>Culture ID: 0x0056
        ///Culture Name:gl</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        public static readonly ICultureIdentifier Galician = new CultureIdentifier(CultureIdentifiers.CultureCodes.Galician, CultureIdentifiers.NumericIdentifiers.Galician, CultureIdentifiers.LocalizedNames.Galician, CultureIdentifiers.CountryRegions.Galician);
        ///<summary>
        ///Static culture identifier instance for Galician - Galician.
        ///</summary>
        ///<remarks>Culture ID: 0x0456
        ///Culture Name:gl-ES</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Galician_Galician = new CultureIdentifier(CultureIdentifiers.CultureCodes.Galician_Galician, CultureIdentifiers.NumericIdentifiers.Galician_Galician, CultureIdentifiers.LocalizedNames.Galician_Galician, CultureIdentifiers.CountryRegions.Galician_Galician);
        ///<summary>
        ///Static culture identifier instance for Georgian.
        ///</summary>
        ///<remarks>Culture ID: 0x0037
        ///Culture Name:ka</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        public static readonly ICultureIdentifier Georgian = new CultureIdentifier(CultureIdentifiers.CultureCodes.Georgian, CultureIdentifiers.NumericIdentifiers.Georgian, CultureIdentifiers.LocalizedNames.Georgian, CultureIdentifiers.CountryRegions.Georgian);
        ///<summary>
        ///Static culture identifier instance for Georgian - Georgia.
        ///</summary>
        ///<remarks>Culture ID: 0x0437
        ///Culture Name:ka-GE</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Georgian_Georgia = new CultureIdentifier(CultureIdentifiers.CultureCodes.Georgian_Georgia, CultureIdentifiers.NumericIdentifiers.Georgian_Georgia, CultureIdentifiers.LocalizedNames.Georgian_Georgia, CultureIdentifiers.CountryRegions.Georgian_Georgia);
        ///<summary>
        ///Static culture identifier instance for German.
        ///</summary>
        ///<remarks>Culture ID: 0x0007
        ///Culture Name:de</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        public static readonly ICultureIdentifier German = new CultureIdentifier(CultureIdentifiers.CultureCodes.German, CultureIdentifiers.NumericIdentifiers.German, CultureIdentifiers.LocalizedNames.German, CultureIdentifiers.CountryRegions.German);
        ///<summary>
        ///Static culture identifier instance for German - Austria.
        ///</summary>
        ///<remarks>Culture ID: 0x0C07
        ///Culture Name:de-AT</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier German_Austria = new CultureIdentifier(CultureIdentifiers.CultureCodes.German_Austria, CultureIdentifiers.NumericIdentifiers.German_Austria, CultureIdentifiers.LocalizedNames.German_Austria, CultureIdentifiers.CountryRegions.German_Austria);
        ///<summary>
        ///Static culture identifier instance for German - Germany.
        ///</summary>
        ///<remarks>Culture ID: 0x0407
        ///Culture Name:de-DE</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier German_Germany = new CultureIdentifier(CultureIdentifiers.CultureCodes.German_Germany, CultureIdentifiers.NumericIdentifiers.German_Germany, CultureIdentifiers.LocalizedNames.German_Germany, CultureIdentifiers.CountryRegions.German_Germany);
        ///<summary>
        ///Static culture identifier instance for German - Liechtenstein.
        ///</summary>
        ///<remarks>Culture ID: 0x1407
        ///Culture Name:de-LI</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier German_Liechtenstein = new CultureIdentifier(CultureIdentifiers.CultureCodes.German_Liechtenstein, CultureIdentifiers.NumericIdentifiers.German_Liechtenstein, CultureIdentifiers.LocalizedNames.German_Liechtenstein, CultureIdentifiers.CountryRegions.German_Liechtenstein);
        ///<summary>
        ///Static culture identifier instance for German - Luxembourg.
        ///</summary>
        ///<remarks>Culture ID: 0x1007
        ///Culture Name:de-LU</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier German_Luxembourg = new CultureIdentifier(CultureIdentifiers.CultureCodes.German_Luxembourg, CultureIdentifiers.NumericIdentifiers.German_Luxembourg, CultureIdentifiers.LocalizedNames.German_Luxembourg, CultureIdentifiers.CountryRegions.German_Luxembourg);
        ///<summary>
        ///Static culture identifier instance for German - Switzerland.
        ///</summary>
        ///<remarks>Culture ID: 0x0807
        ///Culture Name:de-CH</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier German_Switzerland = new CultureIdentifier(CultureIdentifiers.CultureCodes.German_Switzerland, CultureIdentifiers.NumericIdentifiers.German_Switzerland, CultureIdentifiers.LocalizedNames.German_Switzerland, CultureIdentifiers.CountryRegions.German_Switzerland);
        ///<summary>
        ///Static culture identifier instance for Greek.
        ///</summary>
        ///<remarks>Culture ID: 0x0008
        ///Culture Name:el</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        public static readonly ICultureIdentifier Greek = new CultureIdentifier(CultureIdentifiers.CultureCodes.Greek, CultureIdentifiers.NumericIdentifiers.Greek, CultureIdentifiers.LocalizedNames.Greek, CultureIdentifiers.CountryRegions.Greek);
        ///<summary>
        ///Static culture identifier instance for Greek - Greece.
        ///</summary>
        ///<remarks>Culture ID: 0x0408
        ///Culture Name:el-GR</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Greek_Greece = new CultureIdentifier(CultureIdentifiers.CultureCodes.Greek_Greece, CultureIdentifiers.NumericIdentifiers.Greek_Greece, CultureIdentifiers.LocalizedNames.Greek_Greece, CultureIdentifiers.CountryRegions.Greek_Greece);
        ///<summary>
        ///Static culture identifier instance for Gujarati.
        ///</summary>
        ///<remarks>Culture ID: 0x0047
        ///Culture Name:gu</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        public static readonly ICultureIdentifier Gujarati = new CultureIdentifier(CultureIdentifiers.CultureCodes.Gujarati, CultureIdentifiers.NumericIdentifiers.Gujarati, CultureIdentifiers.LocalizedNames.Gujarati, CultureIdentifiers.CountryRegions.Gujarati);
        ///<summary>
        ///Static culture identifier instance for Gujarati - India.
        ///</summary>
        ///<remarks>Culture ID: 0x0447
        ///Culture Name:gu-IN</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Gujarati_India = new CultureIdentifier(CultureIdentifiers.CultureCodes.Gujarati_India, CultureIdentifiers.NumericIdentifiers.Gujarati_India, CultureIdentifiers.LocalizedNames.Gujarati_India, CultureIdentifiers.CountryRegions.Gujarati_India);
        ///<summary>
        ///Static culture identifier instance for Hebrew.
        ///</summary>
        ///<remarks>Culture ID: 0x000D
        ///Culture Name:he</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        public static readonly ICultureIdentifier Hebrew = new CultureIdentifier(CultureIdentifiers.CultureCodes.Hebrew, CultureIdentifiers.NumericIdentifiers.Hebrew, CultureIdentifiers.LocalizedNames.Hebrew, CultureIdentifiers.CountryRegions.Hebrew);
        ///<summary>
        ///Static culture identifier instance for Hebrew - Israel.
        ///</summary>
        ///<remarks>Culture ID: 0x040D
        ///Culture Name:he-IL</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Hebrew_Israel = new CultureIdentifier(CultureIdentifiers.CultureCodes.Hebrew_Israel, CultureIdentifiers.NumericIdentifiers.Hebrew_Israel, CultureIdentifiers.LocalizedNames.Hebrew_Israel, CultureIdentifiers.CountryRegions.Hebrew_Israel);
        ///<summary>
        ///Static culture identifier instance for Hindi.
        ///</summary>
        ///<remarks>Culture ID: 0x0039
        ///Culture Name:hi</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        public static readonly ICultureIdentifier Hindi = new CultureIdentifier(CultureIdentifiers.CultureCodes.Hindi, CultureIdentifiers.NumericIdentifiers.Hindi, CultureIdentifiers.LocalizedNames.Hindi, CultureIdentifiers.CountryRegions.Hindi);
        ///<summary>
        ///Static culture identifier instance for Hindi - India.
        ///</summary>
        ///<remarks>Culture ID: 0x0439
        ///Culture Name:hi-IN</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Hindi_India = new CultureIdentifier(CultureIdentifiers.CultureCodes.Hindi_India, CultureIdentifiers.NumericIdentifiers.Hindi_India, CultureIdentifiers.LocalizedNames.Hindi_India, CultureIdentifiers.CountryRegions.Hindi_India);
        ///<summary>
        ///Static culture identifier instance for Hungarian.
        ///</summary>
        ///<remarks>Culture ID: 0x000E
        ///Culture Name:hu</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        public static readonly ICultureIdentifier Hungarian = new CultureIdentifier(CultureIdentifiers.CultureCodes.Hungarian, CultureIdentifiers.NumericIdentifiers.Hungarian, CultureIdentifiers.LocalizedNames.Hungarian, CultureIdentifiers.CountryRegions.Hungarian);
        ///<summary>
        ///Static culture identifier instance for Hungarian - Hungary.
        ///</summary>
        ///<remarks>Culture ID: 0x040E
        ///Culture Name:hu-HU</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Hungarian_Hungary = new CultureIdentifier(CultureIdentifiers.CultureCodes.Hungarian_Hungary, CultureIdentifiers.NumericIdentifiers.Hungarian_Hungary, CultureIdentifiers.LocalizedNames.Hungarian_Hungary, CultureIdentifiers.CountryRegions.Hungarian_Hungary);
        ///<summary>
        ///Static culture identifier instance for Icelandic.
        ///</summary>
        ///<remarks>Culture ID: 0x000F
        ///Culture Name:is</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        public static readonly ICultureIdentifier Icelandic = new CultureIdentifier(CultureIdentifiers.CultureCodes.Icelandic, CultureIdentifiers.NumericIdentifiers.Icelandic, CultureIdentifiers.LocalizedNames.Icelandic, CultureIdentifiers.CountryRegions.Icelandic);
        ///<summary>
        ///Static culture identifier instance for Icelandic - Iceland.
        ///</summary>
        ///<remarks>Culture ID: 0x040F
        ///Culture Name:is-IS</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Icelandic_Iceland = new CultureIdentifier(CultureIdentifiers.CultureCodes.Icelandic_Iceland, CultureIdentifiers.NumericIdentifiers.Icelandic_Iceland, CultureIdentifiers.LocalizedNames.Icelandic_Iceland, CultureIdentifiers.CountryRegions.Icelandic_Iceland);
        ///<summary>
        ///Static culture identifier instance for Indonesian.
        ///</summary>
        ///<remarks>Culture ID: 0x0021
        ///Culture Name:id</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        public static readonly ICultureIdentifier Indonesian = new CultureIdentifier(CultureIdentifiers.CultureCodes.Indonesian, CultureIdentifiers.NumericIdentifiers.Indonesian, CultureIdentifiers.LocalizedNames.Indonesian, CultureIdentifiers.CountryRegions.Indonesian);
        ///<summary>
        ///Static culture identifier instance for Indonesian - Indonesia.
        ///</summary>
        ///<remarks>Culture ID: 0x0421
        ///Culture Name:id-ID</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Indonesian_Indonesia = new CultureIdentifier(CultureIdentifiers.CultureCodes.Indonesian_Indonesia, CultureIdentifiers.NumericIdentifiers.Indonesian_Indonesia, CultureIdentifiers.LocalizedNames.Indonesian_Indonesia, CultureIdentifiers.CountryRegions.Indonesian_Indonesia);
        ///<summary>
        ///Static culture identifier instance for Italian.
        ///</summary>
        ///<remarks>Culture ID: 0x0010
        ///Culture Name:it</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        public static readonly ICultureIdentifier Italian = new CultureIdentifier(CultureIdentifiers.CultureCodes.Italian, CultureIdentifiers.NumericIdentifiers.Italian, CultureIdentifiers.LocalizedNames.Italian, CultureIdentifiers.CountryRegions.Italian);
        ///<summary>
        ///Static culture identifier instance for Italian - Italy.
        ///</summary>
        ///<remarks>Culture ID: 0x0410
        ///Culture Name:it-IT</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Italian_Italy = new CultureIdentifier(CultureIdentifiers.CultureCodes.Italian_Italy, CultureIdentifiers.NumericIdentifiers.Italian_Italy, CultureIdentifiers.LocalizedNames.Italian_Italy, CultureIdentifiers.CountryRegions.Italian_Italy);
        ///<summary>
        ///Static culture identifier instance for Italian - Switzerland.
        ///</summary>
        ///<remarks>Culture ID: 0x0810
        ///Culture Name:it-CH</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Italian_Switzerland = new CultureIdentifier(CultureIdentifiers.CultureCodes.Italian_Switzerland, CultureIdentifiers.NumericIdentifiers.Italian_Switzerland, CultureIdentifiers.LocalizedNames.Italian_Switzerland, CultureIdentifiers.CountryRegions.Italian_Switzerland);
        ///<summary>
        ///Static culture identifier instance for Japanese.
        ///</summary>
        ///<remarks>Culture ID: 0x0011
        ///Culture Name:ja</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        public static readonly ICultureIdentifier Japanese = new CultureIdentifier(CultureIdentifiers.CultureCodes.Japanese, CultureIdentifiers.NumericIdentifiers.Japanese, CultureIdentifiers.LocalizedNames.Japanese, CultureIdentifiers.CountryRegions.Japanese);
        ///<summary>
        ///Static culture identifier instance for Japanese - Japan.
        ///</summary>
        ///<remarks>Culture ID: 0x0411
        ///Culture Name:ja-JP</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Japanese_Japan = new CultureIdentifier(CultureIdentifiers.CultureCodes.Japanese_Japan, CultureIdentifiers.NumericIdentifiers.Japanese_Japan, CultureIdentifiers.LocalizedNames.Japanese_Japan, CultureIdentifiers.CountryRegions.Japanese_Japan);
        ///<summary>
        ///Static culture identifier instance for Kannada.
        ///</summary>
        ///<remarks>Culture ID: 0x004B
        ///Culture Name:kn</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        public static readonly ICultureIdentifier Kannada = new CultureIdentifier(CultureIdentifiers.CultureCodes.Kannada, CultureIdentifiers.NumericIdentifiers.Kannada, CultureIdentifiers.LocalizedNames.Kannada, CultureIdentifiers.CountryRegions.Kannada);
        ///<summary>
        ///Static culture identifier instance for Kannada - India.
        ///</summary>
        ///<remarks>Culture ID: 0x044B
        ///Culture Name:kn-IN</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Kannada_India = new CultureIdentifier(CultureIdentifiers.CultureCodes.Kannada_India, CultureIdentifiers.NumericIdentifiers.Kannada_India, CultureIdentifiers.LocalizedNames.Kannada_India, CultureIdentifiers.CountryRegions.Kannada_India);
        ///<summary>
        ///Static culture identifier instance for Kazakh.
        ///</summary>
        ///<remarks>Culture ID: 0x003F
        ///Culture Name:kk</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        public static readonly ICultureIdentifier Kazakh = new CultureIdentifier(CultureIdentifiers.CultureCodes.Kazakh, CultureIdentifiers.NumericIdentifiers.Kazakh, CultureIdentifiers.LocalizedNames.Kazakh, CultureIdentifiers.CountryRegions.Kazakh);
        ///<summary>
        ///Static culture identifier instance for Kazakh - Kazakhstan.
        ///</summary>
        ///<remarks>Culture ID: 0x043F
        ///Culture Name:kk-KZ</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Kazakh_Kazakhstan = new CultureIdentifier(CultureIdentifiers.CultureCodes.Kazakh_Kazakhstan, CultureIdentifiers.NumericIdentifiers.Kazakh_Kazakhstan, CultureIdentifiers.LocalizedNames.Kazakh_Kazakhstan, CultureIdentifiers.CountryRegions.Kazakh_Kazakhstan);
        ///<summary>
        ///Static culture identifier instance for Konkani.
        ///</summary>
        ///<remarks>Culture ID: 0x0057
        ///Culture Name:kok</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        public static readonly ICultureIdentifier Konkani = new CultureIdentifier(CultureIdentifiers.CultureCodes.Konkani, CultureIdentifiers.NumericIdentifiers.Konkani, CultureIdentifiers.LocalizedNames.Konkani, CultureIdentifiers.CountryRegions.Konkani);
        ///<summary>
        ///Static culture identifier instance for Konkani - India.
        ///</summary>
        ///<remarks>Culture ID: 0x0457
        ///Culture Name:kok-IN</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Konkani_India = new CultureIdentifier(CultureIdentifiers.CultureCodes.Konkani_India, CultureIdentifiers.NumericIdentifiers.Konkani_India, CultureIdentifiers.LocalizedNames.Konkani_India, CultureIdentifiers.CountryRegions.Konkani_India);
        ///<summary>
        ///Static culture identifier instance for Korean.
        ///</summary>
        ///<remarks>Culture ID: 0x0012
        ///Culture Name:ko</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        public static readonly ICultureIdentifier Korean = new CultureIdentifier(CultureIdentifiers.CultureCodes.Korean, CultureIdentifiers.NumericIdentifiers.Korean, CultureIdentifiers.LocalizedNames.Korean, CultureIdentifiers.CountryRegions.Korean);
        ///<summary>
        ///Static culture identifier instance for Korean - Korea.
        ///</summary>
        ///<remarks>Culture ID: 0x0412
        ///Culture Name:ko-KR</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Korean_Korea = new CultureIdentifier(CultureIdentifiers.CultureCodes.Korean_Korea, CultureIdentifiers.NumericIdentifiers.Korean_Korea, CultureIdentifiers.LocalizedNames.Korean_Korea, CultureIdentifiers.CountryRegions.Korean_Korea);
        ///<summary>
        ///Static culture identifier instance for Kyrgyz.
        ///</summary>
        ///<remarks>Culture ID: 0x0040
        ///Culture Name:ky</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        public static readonly ICultureIdentifier Kyrgyz = new CultureIdentifier(CultureIdentifiers.CultureCodes.Kyrgyz, CultureIdentifiers.NumericIdentifiers.Kyrgyz, CultureIdentifiers.LocalizedNames.Kyrgyz, CultureIdentifiers.CountryRegions.Kyrgyz);
        ///<summary>
        ///Static culture identifier instance for Kyrgyz - Kyrgyzstan.
        ///</summary>
        ///<remarks>Culture ID: 0x0440
        ///Culture Name:ky-KG</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Kyrgyz_Kyrgyzstan = new CultureIdentifier(CultureIdentifiers.CultureCodes.Kyrgyz_Kyrgyzstan, CultureIdentifiers.NumericIdentifiers.Kyrgyz_Kyrgyzstan, CultureIdentifiers.LocalizedNames.Kyrgyz_Kyrgyzstan, CultureIdentifiers.CountryRegions.Kyrgyz_Kyrgyzstan);
        ///<summary>
        ///Static culture identifier instance for Latvian.
        ///</summary>
        ///<remarks>Culture ID: 0x0026
        ///Culture Name:lv</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        public static readonly ICultureIdentifier Latvian = new CultureIdentifier(CultureIdentifiers.CultureCodes.Latvian, CultureIdentifiers.NumericIdentifiers.Latvian, CultureIdentifiers.LocalizedNames.Latvian, CultureIdentifiers.CountryRegions.Latvian);
        ///<summary>
        ///Static culture identifier instance for Latvian - Latvia.
        ///</summary>
        ///<remarks>Culture ID: 0x0426
        ///Culture Name:lv-LV</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Latvian_Latvia = new CultureIdentifier(CultureIdentifiers.CultureCodes.Latvian_Latvia, CultureIdentifiers.NumericIdentifiers.Latvian_Latvia, CultureIdentifiers.LocalizedNames.Latvian_Latvia, CultureIdentifiers.CountryRegions.Latvian_Latvia);
        ///<summary>
        ///Static culture identifier instance for Lithuanian.
        ///</summary>
        ///<remarks>Culture ID: 0x0027
        ///Culture Name:lt</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        public static readonly ICultureIdentifier Lithuanian = new CultureIdentifier(CultureIdentifiers.CultureCodes.Lithuanian, CultureIdentifiers.NumericIdentifiers.Lithuanian, CultureIdentifiers.LocalizedNames.Lithuanian, CultureIdentifiers.CountryRegions.Lithuanian);
        ///<summary>
        ///Static culture identifier instance for Lithuanian - Lithuania.
        ///</summary>
        ///<remarks>Culture ID: 0x0427
        ///Culture Name:lt-LT</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Lithuanian_Lithuania = new CultureIdentifier(CultureIdentifiers.CultureCodes.Lithuanian_Lithuania, CultureIdentifiers.NumericIdentifiers.Lithuanian_Lithuania, CultureIdentifiers.LocalizedNames.Lithuanian_Lithuania, CultureIdentifiers.CountryRegions.Lithuanian_Lithuania);
        ///<summary>
        ///Static culture identifier instance for Macedonian.
        ///</summary>
        ///<remarks>Culture ID: 0x002F
        ///Culture Name:mk</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        public static readonly ICultureIdentifier Macedonian = new CultureIdentifier(CultureIdentifiers.CultureCodes.Macedonian, CultureIdentifiers.NumericIdentifiers.Macedonian, CultureIdentifiers.LocalizedNames.Macedonian, CultureIdentifiers.CountryRegions.Macedonian);
        ///<summary>
        ///Static culture identifier instance for Macedonian - Former Yugoslav Republic of Macedonia.
        ///</summary>
        ///<remarks>Culture ID: 0x042F
        ///Culture Name:mk-MK</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Macedonian_FormerYugoslavRepublicOfMacedonia = new CultureIdentifier(CultureIdentifiers.CultureCodes.Macedonian_FormerYugoslavRepublicOfMacedonia, CultureIdentifiers.NumericIdentifiers.Macedonian_FormerYugoslavRepublicOfMacedonia, CultureIdentifiers.LocalizedNames.Macedonian_FormerYugoslavRepublicOfMacedonia, CultureIdentifiers.CountryRegions.Macedonian_FormerYugoslavRepublicOfMacedonia);
        ///<summary>
        ///Static culture identifier instance for Malay.
        ///</summary>
        ///<remarks>Culture ID: 0x003E
        ///Culture Name:ms</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        public static readonly ICultureIdentifier Malay = new CultureIdentifier(CultureIdentifiers.CultureCodes.Malay, CultureIdentifiers.NumericIdentifiers.Malay, CultureIdentifiers.LocalizedNames.Malay, CultureIdentifiers.CountryRegions.Malay);
        ///<summary>
        ///Static culture identifier instance for Malay - Brunei.
        ///</summary>
        ///<remarks>Culture ID: 0x083E
        ///Culture Name:ms-BN</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Malay_Brunei = new CultureIdentifier(CultureIdentifiers.CultureCodes.Malay_Brunei, CultureIdentifiers.NumericIdentifiers.Malay_Brunei, CultureIdentifiers.LocalizedNames.Malay_Brunei, CultureIdentifiers.CountryRegions.Malay_Brunei);
        ///<summary>
        ///Static culture identifier instance for Malay - Malaysia.
        ///</summary>
        ///<remarks>Culture ID: 0x043E
        ///Culture Name:ms-MY</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Malay_Malaysia = new CultureIdentifier(CultureIdentifiers.CultureCodes.Malay_Malaysia, CultureIdentifiers.NumericIdentifiers.Malay_Malaysia, CultureIdentifiers.LocalizedNames.Malay_Malaysia, CultureIdentifiers.CountryRegions.Malay_Malaysia);
        ///<summary>
        ///Static culture identifier instance for Marathi.
        ///</summary>
        ///<remarks>Culture ID: 0x004E
        ///Culture Name:mr</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        public static readonly ICultureIdentifier Marathi = new CultureIdentifier(CultureIdentifiers.CultureCodes.Marathi, CultureIdentifiers.NumericIdentifiers.Marathi, CultureIdentifiers.LocalizedNames.Marathi, CultureIdentifiers.CountryRegions.Marathi);
        ///<summary>
        ///Static culture identifier instance for Marathi - India.
        ///</summary>
        ///<remarks>Culture ID: 0x044E
        ///Culture Name:mr-IN</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Marathi_India = new CultureIdentifier(CultureIdentifiers.CultureCodes.Marathi_India, CultureIdentifiers.NumericIdentifiers.Marathi_India, CultureIdentifiers.LocalizedNames.Marathi_India, CultureIdentifiers.CountryRegions.Marathi_India);
        ///<summary>
        ///Static culture identifier instance for Mongolian.
        ///</summary>
        ///<remarks>Culture ID: 0x0050
        ///Culture Name:mn</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        public static readonly ICultureIdentifier Mongolian = new CultureIdentifier(CultureIdentifiers.CultureCodes.Mongolian, CultureIdentifiers.NumericIdentifiers.Mongolian, CultureIdentifiers.LocalizedNames.Mongolian, CultureIdentifiers.CountryRegions.Mongolian);
        ///<summary>
        ///Static culture identifier instance for Mongolian - Mongolia.
        ///</summary>
        ///<remarks>Culture ID: 0x0450
        ///Culture Name:mn-MN</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Mongolian_Mongolia = new CultureIdentifier(CultureIdentifiers.CultureCodes.Mongolian_Mongolia, CultureIdentifiers.NumericIdentifiers.Mongolian_Mongolia, CultureIdentifiers.LocalizedNames.Mongolian_Mongolia, CultureIdentifiers.CountryRegions.Mongolian_Mongolia);
        ///<summary>
        ///Static culture identifier instance for Norwegian.
        ///</summary>
        ///<remarks>Culture ID: 0x0014
        ///Culture Name:no</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        public static readonly ICultureIdentifier Norwegian = new CultureIdentifier(CultureIdentifiers.CultureCodes.Norwegian, CultureIdentifiers.NumericIdentifiers.Norwegian, CultureIdentifiers.LocalizedNames.Norwegian, CultureIdentifiers.CountryRegions.Norwegian);
        ///<summary>
        ///Static culture identifier instance for Norwegian (Bokmål) - Norway.
        ///</summary>
        ///<remarks>Culture ID: 0x0414
        ///Culture Name:nb-NO</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Norwegian_Bokmål_Norway = new CultureIdentifier(CultureIdentifiers.CultureCodes.Norwegian_Bokmål_Norway, CultureIdentifiers.NumericIdentifiers.Norwegian_Bokmål_Norway, CultureIdentifiers.LocalizedNames.Norwegian_Bokmål_Norway, CultureIdentifiers.CountryRegions.Norwegian_Bokmål_Norway);//DefaultCultureIDByCultureNumber[NumericIdentifiers.Norwegian_Bokmål_Norway];
        ///<summary>
        ///Static culture identifier instance for Norwegian (Nynorsk) - Norway.
        ///</summary>
        ///<remarks>Culture ID: 0x0814
        ///Culture Name:nn-NO</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Norwegian_Nynorsk_Norway = new CultureIdentifier(CultureIdentifiers.CultureCodes.Norwegian_Nynorsk_Norway, CultureIdentifiers.NumericIdentifiers.Norwegian_Nynorsk_Norway, CultureIdentifiers.LocalizedNames.Norwegian_Nynorsk_Norway, CultureIdentifiers.CountryRegions.Norwegian_Nynorsk_Norway);
        ///<summary>
        ///Static culture identifier instance for Polish.
        ///</summary>
        ///<remarks>Culture ID: 0x0015
        ///Culture Name:pl</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        public static readonly ICultureIdentifier Polish = new CultureIdentifier(CultureIdentifiers.CultureCodes.Polish, CultureIdentifiers.NumericIdentifiers.Polish, CultureIdentifiers.LocalizedNames.Polish, CultureIdentifiers.CountryRegions.Polish);
        ///<summary>
        ///Static culture identifier instance for Polish - Poland.
        ///</summary>
        ///<remarks>Culture ID: 0x0415
        ///Culture Name:pl-PL</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Polish_Poland = new CultureIdentifier(CultureIdentifiers.CultureCodes.Polish_Poland, CultureIdentifiers.NumericIdentifiers.Polish_Poland, CultureIdentifiers.LocalizedNames.Polish_Poland, CultureIdentifiers.CountryRegions.Polish_Poland);
        ///<summary>
        ///Static culture identifier instance for Portuguese.
        ///</summary>
        ///<remarks>Culture ID: 0x0016
        ///Culture Name:pt</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        public static readonly ICultureIdentifier Portuguese = new CultureIdentifier(CultureIdentifiers.CultureCodes.Portuguese, CultureIdentifiers.NumericIdentifiers.Portuguese, CultureIdentifiers.LocalizedNames.Portuguese, CultureIdentifiers.CountryRegions.Portuguese);
        ///<summary>
        ///Static culture identifier instance for Portuguese - Brazil.
        ///</summary>
        ///<remarks>Culture ID: 0x0416
        ///Culture Name:pt-BR</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Portuguese_Brazil = new CultureIdentifier(CultureIdentifiers.CultureCodes.Portuguese_Brazil, CultureIdentifiers.NumericIdentifiers.Portuguese_Brazil, CultureIdentifiers.LocalizedNames.Portuguese_Brazil, CultureIdentifiers.CountryRegions.Portuguese_Brazil);
        ///<summary>
        ///Static culture identifier instance for Portuguese - Portugal.
        ///</summary>
        ///<remarks>Culture ID: 0x0816
        ///Culture Name:pt-PT</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Portuguese_Portugal = new CultureIdentifier(CultureIdentifiers.CultureCodes.Portuguese_Portugal, CultureIdentifiers.NumericIdentifiers.Portuguese_Portugal, CultureIdentifiers.LocalizedNames.Portuguese_Portugal, CultureIdentifiers.CountryRegions.Portuguese_Portugal);
        ///<summary>
        ///Static culture identifier instance for Punjabi.
        ///</summary>
        ///<remarks>Culture ID: 0x0046
        ///Culture Name:pa</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        public static readonly ICultureIdentifier Punjabi = new CultureIdentifier(CultureIdentifiers.CultureCodes.Punjabi, CultureIdentifiers.NumericIdentifiers.Punjabi, CultureIdentifiers.LocalizedNames.Punjabi, CultureIdentifiers.CountryRegions.Punjabi);
        ///<summary>
        ///Static culture identifier instance for Punjabi - India.
        ///</summary>
        ///<remarks>Culture ID: 0x0446
        ///Culture Name:pa-IN</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Punjabi_India = new CultureIdentifier(CultureIdentifiers.CultureCodes.Punjabi_India, CultureIdentifiers.NumericIdentifiers.Punjabi_India, CultureIdentifiers.LocalizedNames.Punjabi_India, CultureIdentifiers.CountryRegions.Punjabi_India);
        ///<summary>
        ///Static culture identifier instance for Romanian.
        ///</summary>
        ///<remarks>Culture ID: 0x0018
        ///Culture Name:ro</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        public static readonly ICultureIdentifier Romanian = new CultureIdentifier(CultureIdentifiers.CultureCodes.Romanian, CultureIdentifiers.NumericIdentifiers.Romanian, CultureIdentifiers.LocalizedNames.Romanian, CultureIdentifiers.CountryRegions.Romanian);
        ///<summary>
        ///Static culture identifier instance for Romanian - Romania.
        ///</summary>
        ///<remarks>Culture ID: 0x0418
        ///Culture Name:ro-RO</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Romanian_Romania = new CultureIdentifier(CultureIdentifiers.CultureCodes.Romanian_Romania, CultureIdentifiers.NumericIdentifiers.Romanian_Romania, CultureIdentifiers.LocalizedNames.Romanian_Romania, CultureIdentifiers.CountryRegions.Romanian_Romania);
        ///<summary>
        ///Static culture identifier instance for Russian.
        ///</summary>
        ///<remarks>Culture ID: 0x0019
        ///Culture Name:ru</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        public static readonly ICultureIdentifier Russian = new CultureIdentifier(CultureIdentifiers.CultureCodes.Russian, CultureIdentifiers.NumericIdentifiers.Russian, CultureIdentifiers.LocalizedNames.Russian, CultureIdentifiers.CountryRegions.Russian);
        ///<summary>
        ///Static culture identifier instance for Russian - Russia.
        ///</summary>
        ///<remarks>Culture ID: 0x0419
        ///Culture Name:ru-RU</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Russian_Russia = new CultureIdentifier(CultureIdentifiers.CultureCodes.Russian_Russia, CultureIdentifiers.NumericIdentifiers.Russian_Russia, CultureIdentifiers.LocalizedNames.Russian_Russia, CultureIdentifiers.CountryRegions.Russian_Russia);
        ///<summary>
        ///Static culture identifier instance for Sanskrit.
        ///</summary>
        ///<remarks>Culture ID: 0x004F
        ///Culture Name:sa</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        public static readonly ICultureIdentifier Sanskrit = new CultureIdentifier(CultureIdentifiers.CultureCodes.Sanskrit, CultureIdentifiers.NumericIdentifiers.Sanskrit, CultureIdentifiers.LocalizedNames.Sanskrit, CultureIdentifiers.CountryRegions.Sanskrit);
        ///<summary>
        ///Static culture identifier instance for Sanskrit - India.
        ///</summary>
        ///<remarks>Culture ID: 0x044F
        ///Culture Name:sa-IN</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Sanskrit_India = new CultureIdentifier(CultureIdentifiers.CultureCodes.Sanskrit_India, CultureIdentifiers.NumericIdentifiers.Sanskrit_India, CultureIdentifiers.LocalizedNames.Sanskrit_India, CultureIdentifiers.CountryRegions.Sanskrit_India);
        ///<summary>
        ///Static culture identifier instance for Serbian (Cyrillic) - Serbia.
        ///</summary>
        ///<remarks>Culture ID: 0x0C1A
        ///Culture Name:sr-SP-Cyrl</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Serbian_Cyrillic_Serbia = new CultureIdentifier(CultureIdentifiers.CultureCodes.Serbian_Cyrillic_Serbia, CultureIdentifiers.NumericIdentifiers.Serbian_Cyrillic_Serbia, CultureIdentifiers.LocalizedNames.Serbian_Cyrillic_Serbia, CultureIdentifiers.CountryRegions.Serbian_Cyrillic_Serbia);
        ///<summary>
        ///Static culture identifier instance for Serbian (Latin) - Serbia.
        ///</summary>
        ///<remarks>Culture ID: 0x081A
        ///Culture Name:sr-SP-Latn</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Serbian_Latin_Serbia = new CultureIdentifier(CultureIdentifiers.CultureCodes.Serbian_Latin_Serbia, CultureIdentifiers.NumericIdentifiers.Serbian_Latin_Serbia, CultureIdentifiers.LocalizedNames.Serbian_Latin_Serbia, CultureIdentifiers.CountryRegions.Serbian_Latin_Serbia);
        ///<summary>
        ///Static culture identifier instance for Slovak.
        ///</summary>
        ///<remarks>Culture ID: 0x001B
        ///Culture Name:sk</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        public static readonly ICultureIdentifier Slovak = new CultureIdentifier(CultureIdentifiers.CultureCodes.Slovak, CultureIdentifiers.NumericIdentifiers.Slovak, CultureIdentifiers.LocalizedNames.Slovak, CultureIdentifiers.CountryRegions.Slovak);
        ///<summary>
        ///Static culture identifier instance for Slovak - Slovakia.
        ///</summary>
        ///<remarks>Culture ID: 0x041B
        ///Culture Name:sk-SK</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Slovak_Slovakia = new CultureIdentifier(CultureIdentifiers.CultureCodes.Slovak_Slovakia, CultureIdentifiers.NumericIdentifiers.Slovak_Slovakia, CultureIdentifiers.LocalizedNames.Slovak_Slovakia, CultureIdentifiers.CountryRegions.Slovak_Slovakia);
        ///<summary>
        ///Static culture identifier instance for Slovenian.
        ///</summary>
        ///<remarks>Culture ID: 0x0024
        ///Culture Name:sl</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        public static readonly ICultureIdentifier Slovenian = new CultureIdentifier(CultureIdentifiers.CultureCodes.Slovenian, CultureIdentifiers.NumericIdentifiers.Slovenian, CultureIdentifiers.LocalizedNames.Slovenian, CultureIdentifiers.CountryRegions.Slovenian);
        ///<summary>
        ///Static culture identifier instance for Slovenian - Slovenia.
        ///</summary>
        ///<remarks>Culture ID: 0x0424
        ///Culture Name:sl-SI</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Slovenian_Slovenia = new CultureIdentifier(CultureIdentifiers.CultureCodes.Slovenian_Slovenia, CultureIdentifiers.NumericIdentifiers.Slovenian_Slovenia, CultureIdentifiers.LocalizedNames.Slovenian_Slovenia, CultureIdentifiers.CountryRegions.Slovenian_Slovenia);
        ///<summary>
        ///Static culture identifier instance for Spanish.
        ///</summary>
        ///<remarks>Culture ID: 0x000A
        ///Culture Name:es</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        public static readonly ICultureIdentifier Spanish = new CultureIdentifier(CultureIdentifiers.CultureCodes.Spanish, CultureIdentifiers.NumericIdentifiers.Spanish, CultureIdentifiers.LocalizedNames.Spanish, CultureIdentifiers.CountryRegions.Spanish);
        ///<summary>
        ///Static culture identifier instance for Spanish - Argentina.
        ///</summary>
        ///<remarks>Culture ID: 0x2C0A
        ///Culture Name:es-AR</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Spanish_Argentina = new CultureIdentifier(CultureIdentifiers.CultureCodes.Spanish_Argentina, CultureIdentifiers.NumericIdentifiers.Spanish_Argentina, CultureIdentifiers.LocalizedNames.Spanish_Argentina, CultureIdentifiers.CountryRegions.Spanish_Argentina);
        ///<summary>
        ///Static culture identifier instance for Spanish - Bolivia.
        ///</summary>
        ///<remarks>Culture ID: 0x400A
        ///Culture Name:es-BO</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Spanish_Bolivia = new CultureIdentifier(CultureIdentifiers.CultureCodes.Spanish_Bolivia, CultureIdentifiers.NumericIdentifiers.Spanish_Bolivia, CultureIdentifiers.LocalizedNames.Spanish_Bolivia, CultureIdentifiers.CountryRegions.Spanish_Bolivia);
        ///<summary>
        ///Static culture identifier instance for Spanish - Chile.
        ///</summary>
        ///<remarks>Culture ID: 0x340A
        ///Culture Name:es-CL</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Spanish_Chile = new CultureIdentifier(CultureIdentifiers.CultureCodes.Spanish_Chile, CultureIdentifiers.NumericIdentifiers.Spanish_Chile, CultureIdentifiers.LocalizedNames.Spanish_Chile, CultureIdentifiers.CountryRegions.Spanish_Chile);
        ///<summary>
        ///Static culture identifier instance for Spanish - Colombia.
        ///</summary>
        ///<remarks>Culture ID: 0x240A
        ///Culture Name:es-CO</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Spanish_Colombia = new CultureIdentifier(CultureIdentifiers.CultureCodes.Spanish_Colombia, CultureIdentifiers.NumericIdentifiers.Spanish_Colombia, CultureIdentifiers.LocalizedNames.Spanish_Colombia, CultureIdentifiers.CountryRegions.Spanish_Colombia);
        ///<summary>
        ///Static culture identifier instance for Spanish - Costa Rica.
        ///</summary>
        ///<remarks>Culture ID: 0x140A
        ///Culture Name:es-CR</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Spanish_CostaRica = new CultureIdentifier(CultureIdentifiers.CultureCodes.Spanish_CostaRica, CultureIdentifiers.NumericIdentifiers.Spanish_CostaRica, CultureIdentifiers.LocalizedNames.Spanish_CostaRica, CultureIdentifiers.CountryRegions.Spanish_CostaRica);
        ///<summary>
        ///Static culture identifier instance for Spanish - Dominican Republic.
        ///</summary>
        ///<remarks>Culture ID: 0x1C0A
        ///Culture Name:es-DO</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Spanish_DominicanRepublic = new CultureIdentifier(CultureIdentifiers.CultureCodes.Spanish_DominicanRepublic, CultureIdentifiers.NumericIdentifiers.Spanish_DominicanRepublic, CultureIdentifiers.LocalizedNames.Spanish_DominicanRepublic, CultureIdentifiers.CountryRegions.Spanish_DominicanRepublic);
        ///<summary>
        ///Static culture identifier instance for Spanish - Ecuador.
        ///</summary>
        ///<remarks>Culture ID: 0x300A
        ///Culture Name:es-EC</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Spanish_Ecuador = new CultureIdentifier(CultureIdentifiers.CultureCodes.Spanish_Ecuador, CultureIdentifiers.NumericIdentifiers.Spanish_Ecuador, CultureIdentifiers.LocalizedNames.Spanish_Ecuador, CultureIdentifiers.CountryRegions.Spanish_Ecuador);
        ///<summary>
        ///Static culture identifier instance for Spanish - El Salvador.
        ///</summary>
        ///<remarks>Culture ID: 0x440A
        ///Culture Name:es-SV</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "El")]
        public static readonly ICultureIdentifier Spanish_ElSalvador = new CultureIdentifier(CultureIdentifiers.CultureCodes.Spanish_ElSalvador, CultureIdentifiers.NumericIdentifiers.Spanish_ElSalvador, CultureIdentifiers.LocalizedNames.Spanish_ElSalvador, CultureIdentifiers.CountryRegions.Spanish_ElSalvador);
        ///<summary>
        ///Static culture identifier instance for Spanish - Guatemala.
        ///</summary>
        ///<remarks>Culture ID: 0x100A
        ///Culture Name:es-GT</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Spanish_Guatemala = new CultureIdentifier(CultureIdentifiers.CultureCodes.Spanish_Guatemala, CultureIdentifiers.NumericIdentifiers.Spanish_Guatemala, CultureIdentifiers.LocalizedNames.Spanish_Guatemala, CultureIdentifiers.CountryRegions.Spanish_Guatemala);
        ///<summary>
        ///Static culture identifier instance for Spanish - Honduras.
        ///</summary>
        ///<remarks>Culture ID: 0x480A
        ///Culture Name:es-HN</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Spanish_Honduras = new CultureIdentifier(CultureIdentifiers.CultureCodes.Spanish_Honduras, CultureIdentifiers.NumericIdentifiers.Spanish_Honduras, CultureIdentifiers.LocalizedNames.Spanish_Honduras, CultureIdentifiers.CountryRegions.Spanish_Honduras);
        ///<summary>
        ///Static culture identifier instance for Spanish - Mexico.
        ///</summary>
        ///<remarks>Culture ID: 0x080A
        ///Culture Name:es-MX</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Spanish_Mexico = new CultureIdentifier(CultureIdentifiers.CultureCodes.Spanish_Mexico, CultureIdentifiers.NumericIdentifiers.Spanish_Mexico, CultureIdentifiers.LocalizedNames.Spanish_Mexico, CultureIdentifiers.CountryRegions.Spanish_Mexico);
        ///<summary>
        ///Static culture identifier instance for Spanish - Nicaragua.
        ///</summary>
        ///<remarks>Culture ID: 0x4C0A
        ///Culture Name:es-NI</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Spanish_Nicaragua = new CultureIdentifier(CultureIdentifiers.CultureCodes.Spanish_Nicaragua, CultureIdentifiers.NumericIdentifiers.Spanish_Nicaragua, CultureIdentifiers.LocalizedNames.Spanish_Nicaragua, CultureIdentifiers.CountryRegions.Spanish_Nicaragua);
        ///<summary>
        ///Static culture identifier instance for Spanish - Panama.
        ///</summary>
        ///<remarks>Culture ID: 0x180A
        ///Culture Name:es-PA</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Spanish_Panama = new CultureIdentifier(CultureIdentifiers.CultureCodes.Spanish_Panama, CultureIdentifiers.NumericIdentifiers.Spanish_Panama, CultureIdentifiers.LocalizedNames.Spanish_Panama, CultureIdentifiers.CountryRegions.Spanish_Panama);
        ///<summary>
        ///Static culture identifier instance for Spanish - Paraguay.
        ///</summary>
        ///<remarks>Culture ID: 0x3C0A
        ///Culture Name:es-PY</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Spanish_Paraguay = new CultureIdentifier(CultureIdentifiers.CultureCodes.Spanish_Paraguay, CultureIdentifiers.NumericIdentifiers.Spanish_Paraguay, CultureIdentifiers.LocalizedNames.Spanish_Paraguay, CultureIdentifiers.CountryRegions.Spanish_Paraguay);
        ///<summary>
        ///Static culture identifier instance for Spanish - Peru.
        ///</summary>
        ///<remarks>Culture ID: 0x280A
        ///Culture Name:es-PE</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Spanish_Peru = new CultureIdentifier(CultureIdentifiers.CultureCodes.Spanish_Peru, CultureIdentifiers.NumericIdentifiers.Spanish_Peru, CultureIdentifiers.LocalizedNames.Spanish_Peru, CultureIdentifiers.CountryRegions.Spanish_Peru);
        ///<summary>
        ///Static culture identifier instance for Spanish - Puerto Rico.
        ///</summary>
        ///<remarks>Culture ID: 0x500A
        ///Culture Name:es-PR</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Spanish_PuertoRico = new CultureIdentifier(CultureIdentifiers.CultureCodes.Spanish_PuertoRico, CultureIdentifiers.NumericIdentifiers.Spanish_PuertoRico, CultureIdentifiers.LocalizedNames.Spanish_PuertoRico, CultureIdentifiers.CountryRegions.Spanish_PuertoRico);
        ///<summary>
        ///Static culture identifier instance for Spanish - Spain.
        ///</summary>
        ///<remarks>Culture ID: 0x0C0A
        ///Culture Name:es-ES</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Spanish_Spain = new CultureIdentifier(CultureIdentifiers.CultureCodes.Spanish_Spain, CultureIdentifiers.NumericIdentifiers.Spanish_Spain, CultureIdentifiers.LocalizedNames.Spanish_Spain, CultureIdentifiers.CountryRegions.Spanish_Spain);
        ///<summary>
        ///Static culture identifier instance for Spanish - Uruguay.
        ///</summary>
        ///<remarks>Culture ID: 0x380A
        ///Culture Name:es-UY</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Spanish_Uruguay = new CultureIdentifier(CultureIdentifiers.CultureCodes.Spanish_Uruguay, CultureIdentifiers.NumericIdentifiers.Spanish_Uruguay, CultureIdentifiers.LocalizedNames.Spanish_Uruguay, CultureIdentifiers.CountryRegions.Spanish_Uruguay);
        ///<summary>
        ///Static culture identifier instance for Spanish - Venezuela.
        ///</summary>
        ///<remarks>Culture ID: 0x200A
        ///Culture Name:es-VE</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Spanish_Venezuela = new CultureIdentifier(CultureIdentifiers.CultureCodes.Spanish_Venezuela, CultureIdentifiers.NumericIdentifiers.Spanish_Venezuela, CultureIdentifiers.LocalizedNames.Spanish_Venezuela, CultureIdentifiers.CountryRegions.Spanish_Venezuela);
        ///<summary>
        ///Static culture identifier instance for Swahili.
        ///</summary>
        ///<remarks>Culture ID: 0x0041
        ///Culture Name:sw</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        public static readonly ICultureIdentifier Swahili = new CultureIdentifier(CultureIdentifiers.CultureCodes.Swahili, CultureIdentifiers.NumericIdentifiers.Swahili, CultureIdentifiers.CountryRegions.Swahili);
        ///<summary>
        ///Static culture identifier instance for Swahili - Kenya.
        ///</summary>
        ///<remarks>Culture ID: 0x0441
        ///Culture Name:sw-KE</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Swahili_Kenya = new CultureIdentifier(CultureIdentifiers.CultureCodes.Swahili_Kenya, CultureIdentifiers.NumericIdentifiers.Swahili_Kenya, CultureIdentifiers.CountryRegions.Swahili_Kenya);
        ///<summary>
        ///Static culture identifier instance for Swedish.
        ///</summary>
        ///<remarks>Culture ID: 0x001D
        ///Culture Name:sv</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        public static readonly ICultureIdentifier Swedish = new CultureIdentifier(CultureIdentifiers.CultureCodes.Swedish, CultureIdentifiers.NumericIdentifiers.Swedish, CultureIdentifiers.LocalizedNames.Swedish, CultureIdentifiers.CountryRegions.Swedish);
        ///<summary>
        ///Static culture identifier instance for Swedish - Finland.
        ///</summary>
        ///<remarks>Culture ID: 0x081D
        ///Culture Name:sv-FI</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Swedish_Finland = new CultureIdentifier(CultureIdentifiers.CultureCodes.Swedish_Finland, CultureIdentifiers.NumericIdentifiers.Swedish_Finland, CultureIdentifiers.LocalizedNames.Swedish_Finland, CultureIdentifiers.CountryRegions.Swedish_Finland);
        ///<summary>
        ///Static culture identifier instance for Swedish - Sweden.
        ///</summary>
        ///<remarks>Culture ID: 0x041D
        ///Culture Name:sv-SE</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Swedish_Sweden = new CultureIdentifier(CultureIdentifiers.CultureCodes.Swedish_Sweden, CultureIdentifiers.NumericIdentifiers.Swedish_Sweden, CultureIdentifiers.LocalizedNames.Swedish_Sweden, CultureIdentifiers.CountryRegions.Swedish_Sweden);
        ///<summary>
        ///Static culture identifier instance for Syriac.
        ///</summary>
        ///<remarks>Culture ID: 0x005A
        ///Culture Name:syr</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Syriac")]
        public static readonly ICultureIdentifier Syriac = new CultureIdentifier(CultureIdentifiers.CultureCodes.Syriac, CultureIdentifiers.NumericIdentifiers.Syriac, CultureIdentifiers.LocalizedNames.Syriac, CultureIdentifiers.CountryRegions.Syriac);
        ///<summary>
        ///Static culture identifier instance for Syriac - Syria.
        ///</summary>
        ///<remarks>Culture ID: 0x045A
        ///Culture Name:syr-SY</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Syriac")]
        public static readonly ICultureIdentifier Syriac_Syria = new CultureIdentifier(CultureIdentifiers.CultureCodes.Syriac_Syria, CultureIdentifiers.NumericIdentifiers.Syriac_Syria, CultureIdentifiers.LocalizedNames.Syriac_Syria, CultureIdentifiers.CountryRegions.Syriac_Syria);
        ///<summary>
        ///Static culture identifier instance for Tamil.
        ///</summary>
        ///<remarks>Culture ID: 0x0049
        ///Culture Name:ta</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        public static readonly ICultureIdentifier Tamil = new CultureIdentifier(CultureIdentifiers.CultureCodes.Tamil, CultureIdentifiers.NumericIdentifiers.Tamil, CultureIdentifiers.LocalizedNames.Tamil, CultureIdentifiers.CountryRegions.Tamil);
        ///<summary>
        ///Static culture identifier instance for Tamil - India.
        ///</summary>
        ///<remarks>Culture ID: 0x0449
        ///Culture Name:ta-IN</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Tamil_India = new CultureIdentifier(CultureIdentifiers.CultureCodes.Tamil_India, CultureIdentifiers.NumericIdentifiers.Tamil_India, CultureIdentifiers.LocalizedNames.Tamil_India, CultureIdentifiers.CountryRegions.Tamil_India);
        ///<summary>
        ///Static culture identifier instance for Tatar.
        ///</summary>
        ///<remarks>Culture ID: 0x0044
        ///Culture Name:tt</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        public static readonly ICultureIdentifier Tatar = new CultureIdentifier(CultureIdentifiers.CultureCodes.Tatar, CultureIdentifiers.NumericIdentifiers.Tatar, CultureIdentifiers.LocalizedNames.Tatar, CultureIdentifiers.CountryRegions.Tatar);
        ///<summary>
        ///Static culture identifier instance for Tatar - Russia.
        ///</summary>
        ///<remarks>Culture ID: 0x0444
        ///Culture Name:tt-RU</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Tatar_Russia = new CultureIdentifier(CultureIdentifiers.CultureCodes.Tatar_Russia, CultureIdentifiers.NumericIdentifiers.Tatar_Russia, CultureIdentifiers.LocalizedNames.Tatar_Russia, CultureIdentifiers.CountryRegions.Tatar_Russia);
        ///<summary>
        ///Static culture identifier instance for Telugu.
        ///</summary>
        ///<remarks>Culture ID: 0x004A
        ///Culture Name:te</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        public static readonly ICultureIdentifier Telugu = new CultureIdentifier(CultureIdentifiers.CultureCodes.Telugu, CultureIdentifiers.NumericIdentifiers.Telugu, CultureIdentifiers.LocalizedNames.Telugu, CultureIdentifiers.CountryRegions.Telugu);
        ///<summary>
        ///Static culture identifier instance for Telugu - India.
        ///</summary>
        ///<remarks>Culture ID: 0x044A
        ///Culture Name:te-IN</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Telugu_India = new CultureIdentifier(CultureIdentifiers.CultureCodes.Telugu_India, CultureIdentifiers.NumericIdentifiers.Telugu_India, CultureIdentifiers.LocalizedNames.Telugu_India, CultureIdentifiers.CountryRegions.Telugu_India);
        ///<summary>
        ///Static culture identifier instance for Thai.
        ///</summary>
        ///<remarks>Culture ID: 0x001E
        ///Culture Name:th</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        public static readonly ICultureIdentifier Thai = new CultureIdentifier(CultureIdentifiers.CultureCodes.Thai, CultureIdentifiers.NumericIdentifiers.Thai, CultureIdentifiers.LocalizedNames.Thai, CultureIdentifiers.CountryRegions.Thai);
        ///<summary>
        ///Static culture identifier instance for Thai - Thailand.
        ///</summary>
        ///<remarks>Culture ID: 0x041E
        ///Culture Name:th-TH</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Thai_Thailand = new CultureIdentifier(CultureIdentifiers.CultureCodes.Thai_Thailand, CultureIdentifiers.NumericIdentifiers.Thai_Thailand, CultureIdentifiers.LocalizedNames.Thai_Thailand, CultureIdentifiers.CountryRegions.Thai_Thailand);
        ///<summary>
        ///Static culture identifier instance for Turkish.
        ///</summary>
        ///<remarks>Culture ID: 0x001F
        ///Culture Name:tr</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        public static readonly ICultureIdentifier Turkish = new CultureIdentifier(CultureIdentifiers.CultureCodes.Turkish, CultureIdentifiers.NumericIdentifiers.Turkish, CultureIdentifiers.LocalizedNames.Turkish, CultureIdentifiers.CountryRegions.Turkish);
        ///<summary>
        ///Static culture identifier instance for Turkish - Turkey.
        ///</summary>
        ///<remarks>Culture ID: 0x041F
        ///Culture Name:tr-TR</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Turkish_Turkey = new CultureIdentifier(CultureIdentifiers.CultureCodes.Turkish_Turkey, CultureIdentifiers.NumericIdentifiers.Turkish_Turkey, CultureIdentifiers.LocalizedNames.Turkish_Turkey, CultureIdentifiers.CountryRegions.Turkish_Turkey);
        ///<summary>
        ///Static culture identifier instance for Ukrainian.
        ///</summary>
        ///<remarks>Culture ID: 0x0022
        ///Culture Name:uk</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        public static readonly ICultureIdentifier Ukrainian = new CultureIdentifier(CultureIdentifiers.CultureCodes.Ukrainian, CultureIdentifiers.NumericIdentifiers.Ukrainian, CultureIdentifiers.LocalizedNames.Ukrainian, CultureIdentifiers.CountryRegions.Ukrainian);
        ///<summary>
        ///Static culture identifier instance for Ukrainian - Ukraine.
        ///</summary>
        ///<remarks>Culture ID: 0x0422
        ///Culture Name:uk-UA</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Ukrainian_Ukraine = new CultureIdentifier(CultureIdentifiers.CultureCodes.Ukrainian_Ukraine, CultureIdentifiers.NumericIdentifiers.Ukrainian_Ukraine, CultureIdentifiers.LocalizedNames.Ukrainian_Ukraine, CultureIdentifiers.CountryRegions.Ukrainian_Ukraine);
        ///<summary>
        ///Static culture identifier instance for Urdu.
        ///</summary>
        ///<remarks>Culture ID: 0x0020
        ///Culture Name:ur</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        public static readonly ICultureIdentifier Urdu = new CultureIdentifier(CultureIdentifiers.CultureCodes.Urdu, CultureIdentifiers.NumericIdentifiers.Urdu, CultureIdentifiers.LocalizedNames.Urdu, CultureIdentifiers.CountryRegions.Urdu);
        ///<summary>
        ///Static culture identifier instance for Urdu - Pakistan.
        ///</summary>
        ///<remarks>Culture ID: 0x0420
        ///Culture Name:ur-PK</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Urdu_Pakistan = new CultureIdentifier(CultureIdentifiers.CultureCodes.Urdu_Pakistan, CultureIdentifiers.NumericIdentifiers.Urdu_Pakistan, CultureIdentifiers.LocalizedNames.Urdu_Pakistan, CultureIdentifiers.CountryRegions.Urdu_Pakistan);
        ///<summary>
        ///Static culture identifier instance for Uzbek.
        ///</summary>
        ///<remarks>Culture ID: 0x0043
        ///Culture Name:uz</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        public static readonly ICultureIdentifier Uzbek = new CultureIdentifier(CultureIdentifiers.CultureCodes.Uzbek, CultureIdentifiers.NumericIdentifiers.Uzbek, CultureIdentifiers.LocalizedNames.Uzbek, CultureIdentifiers.CountryRegions.Uzbek);
        ///<summary>
        ///Static culture identifier instance for Uzbek (Cyrillic) - Uzbekistan.
        ///</summary>
        ///<remarks>Culture ID: 0x0843
        ///Culture Name:uz-UZ-Cyrl</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Uzbek_Cyrillic_Uzbekistan = new CultureIdentifier(CultureIdentifiers.CultureCodes.Uzbek_Cyrillic_Uzbekistan, CultureIdentifiers.NumericIdentifiers.Uzbek_Cyrillic_Uzbekistan, CultureIdentifiers.LocalizedNames.Uzbek_Cyrillic_Uzbekistan, CultureIdentifiers.CountryRegions.Uzbek_Cyrillic_Uzbekistan);
        ///<summary>
        ///Static culture identifier instance for Uzbek (Latin) - Uzbekistan.
        ///</summary>
        ///<remarks>Culture ID: 0x0443
        ///Culture Name:uz-UZ-Latn</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Uzbek_Latin_Uzbekistan = new CultureIdentifier(CultureIdentifiers.CultureCodes.Uzbek_Latin_Uzbekistan, CultureIdentifiers.NumericIdentifiers.Uzbek_Latin_Uzbekistan, CultureIdentifiers.LocalizedNames.Uzbek_Latin_Uzbekistan, CultureIdentifiers.CountryRegions.Uzbek_Latin_Uzbekistan);
        ///<summary>
        ///Static culture identifier instance for Vietnamese.
        ///</summary>
        ///<remarks>Culture ID: 0x002A
        ///Culture Name:vi</remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        public static readonly ICultureIdentifier Vietnamese = new CultureIdentifier(CultureIdentifiers.CultureCodes.Vietnamese, CultureIdentifiers.NumericIdentifiers.Vietnamese, CultureIdentifiers.LocalizedNames.Vietnamese, CultureIdentifiers.CountryRegions.Vietnamese);
        /// <summary>
        /// Static culture identifier instance for Vietnamese - Vietnam
        /// </summary>
        /// <remarks>
        /// Culture ID: 0x042A
        /// Culture Name: vi-VN
        /// </remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public static readonly ICultureIdentifier Vietnamese_Vietnam = new CultureIdentifier(CultureIdentifiers.CultureCodes.Vietnamese_Vietnam, CultureIdentifiers.NumericIdentifiers.Vietnamese_Vietnam, CultureIdentifiers.LocalizedNames.Vietnamese_Vietnam, CultureIdentifiers.CountryRegions.Vietnamese_Vietnam);

        private static void AcquireCultures()
        {

            ICultureIdentifier[] cultureIdentifiers = new ICultureIdentifier[] 
	        {
		        None, 
                Afrikaans, Afrikaans_SouthAfrica, 
                Albanian, Albanian_Albania, 
                Arabic, Arabic_Algeria, Arabic_Bahrain, Arabic_Egypt, Arabic_Iraq, 
                Arabic_Jordan, Arabic_Kuwait, Arabic_Lebanon, Arabic_Libya, 
                Arabic_Morocco, Arabic_Oman, Arabic_Qatar, Arabic_SaudiArabia, 
                Arabic_Syria, Arabic_Tunisia, Arabic_UnitedArabEmirates, 
                Arabic_Yemen, 
                Armenian, Armenian_Armenia, 
                Azeri, Azeri_Cyrillic_Azerbaijan, Azeri_Latin_Azerbaijan, 
                Basque, Basque_Basque, 
                Belarusian, Belarusian_Belarus, 
                Bulgarian, Bulgarian_Bulgaria, 
                Catalan, Catalan_Catalan, 
                Chinese_HongKongSAR, Chinese_MacaoSAR, Chinese_China, 
                Chinese_Simplified, Chinese_Singapore, Chinese_Taiwan, 
                Chinese_Traditional, 
                Croatian, Croatian_Croatia, 
                Czech, Czech_CzechRepublic, 
                Danish, Danish_Denmark, 
                Dhivehi, Dhivehi_Maldives, 
                Dutch, Dutch_Belgium, Dutch_TheNetherlands, 
                English, English_Australia, English_Belize, 
                English_Canada, English_Caribbean, English_Ireland, 
                English_Jamaica, English_NewZealand, English_Philippines, 
                English_SouthAfrica, English_TrinidadAndTobago, 
                English_UnitedKingdom, English_UnitedStates, English_Zimbabwe, 
                Estonian, Estonian_Estonia, 
                Faroese, Faroese_FaroeIslands, 
                Farsi, Farsi_Iran, 
                Finnish, Finnish_Finland, 
                French, French_Belgium, French_Canada, French_France, 
                French_Luxembourg, French_Monaco, French_Switzerland, 
                Galician, Galician_Galician, 
                Georgian, Georgian_Georgia, 
                German, German_Austria, German_Germany, German_Liechtenstein, 
                German_Luxembourg, German_Switzerland, 
                Greek, Greek_Greece, 
                Gujarati, Gujarati_India, 
                Hebrew, Hebrew_Israel, 
                Hindi, Hindi_India, 
                Hungarian, 
                Hungarian_Hungary, 
                Icelandic, 
                Icelandic_Iceland, 
                Indonesian, 
                Indonesian_Indonesia, 
                Italian, 
                Italian_Italy, 
                Italian_Switzerland, 
                Japanese, 
                Japanese_Japan, 
                Kannada, 
                Kannada_India, 
                Kazakh, 
                Kazakh_Kazakhstan, 
                Konkani, 
                Konkani_India, 
                Korean, 
                Korean_Korea, 
                Kyrgyz, 
                Kyrgyz_Kyrgyzstan, 
                Latvian, 
                Latvian_Latvia, 
                Lithuanian, 
                Lithuanian_Lithuania, 
                Macedonian, 
                Macedonian_FormerYugoslavRepublicOfMacedonia, 
                Malay, 
                Malay_Brunei, 
                Malay_Malaysia, 
                Marathi, 
                Marathi_India, 
                Mongolian, 
                Mongolian_Mongolia, 
                Norwegian, 
                Norwegian_Bokmål_Norway,
                Norwegian_Nynorsk_Norway, 
                Polish, 
                Polish_Poland, 
                Portuguese, 
                Portuguese_Brazil, 
                Portuguese_Portugal, 
                Punjabi, 
                Punjabi_India, 
                Romanian, 
                Romanian_Romania, 
                Russian, 
                Russian_Russia, 
                Sanskrit, 
                Sanskrit_India, 
                Serbian_Cyrillic_Serbia, 
                Serbian_Latin_Serbia, 
                Slovak, 
                Slovak_Slovakia, 
                Slovenian, 
                Slovenian_Slovenia, 
                Spanish, Spanish_Argentina, Spanish_Bolivia, Spanish_Chile, Spanish_Colombia, 
                Spanish_CostaRica, Spanish_DominicanRepublic, Spanish_Ecuador, Spanish_ElSalvador, 
                Spanish_Guatemala, Spanish_Honduras, Spanish_Mexico, Spanish_Nicaragua, 
                Spanish_Panama, Spanish_Paraguay, Spanish_Peru, Spanish_PuertoRico, Spanish_Spain, 
                Spanish_Uruguay, Spanish_Venezuela, 
                Swahili,Swahili_Kenya,
                Swedish,Swedish_Finland, Swedish_Sweden, 
                Syriac, Syriac_Syria, 
                Tamil, Tamil_India, 
                Tatar, Tatar_Russia, 
                Telugu,Telugu_India, 
                Thai, Thai_Thailand, 
                Turkish, Turkish_Turkey, 
                Ukrainian, Ukrainian_Ukraine, 
                Urdu, Urdu_Pakistan, 
                Uzbek, Uzbek_Cyrillic_Uzbekistan, Uzbek_Latin_Uzbekistan, 
                Vietnamese, Vietnamese_Vietnam
	        };
            Func<ICultureIdentifier, CultureIdentifier> idSelector = cultureID => (CultureIdentifier)(cultureID);
            defaultCultureIDByCultureName = cultureIdentifiers.ToDictionary(cultureName => cultureName.Name, idSelector);
            defaultCultureIDByCultureNumber = cultureIdentifiers.ToDictionary(culture => culture.Culture, idSelector);
            cultureIdentifiers = null;
        }


    }
}