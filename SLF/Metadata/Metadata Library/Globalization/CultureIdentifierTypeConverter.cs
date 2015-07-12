using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Globalization;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Globalization
{
    /// <summary>
    /// Provides a string->culture identifier type converter.
    /// </summary>
    public class CultureIdentifierTypeConverter :
        TypeConverter
    {
        /// <summary>
        /// Creates a new <see cref="CultureIdentifierTypeConverter"/>
        /// initialized to a default state.
        /// </summary>
        public CultureIdentifierTypeConverter()
            : base()
        {
        }

        /// <summary>
        /// Returns whether <see cref="CultureIdentifierTypeConverter"/> can convert an object of 
        /// the <paramref name="sourceType"/> to the type of this converter (<see cref="ICultureIdentifier"/>), 
        /// using the specified <paramref name="context"/>.
        /// </summary>
        /// <param name="context">
        /// An <see cref="ITypeDescriptorContext"/> that provides a format context.
        /// </param>
        /// <param name="sourceType">
        /// A <see cref="Type"/> that represents the type you want to convert from.
        /// </param>
        /// <returns>true if this converter can perform the conversion; otherwise, false.</returns>
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(System.String))
                return (context.PropertyDescriptor.PropertyType.IsAssignableFrom(typeof(ICultureIdentifier)));
            return base.CanConvertFrom(context, sourceType);
        }


        /// <summary>
        /// Converts the given <paramref name="value"/> to the type of this converter (<see cref="ICultureIdentifier"/>), 
        /// using the specified <paramref name="context"/> and <paramref name="culture"/> information.
        /// </summary>
        /// <param name="context">
        /// An <see cref="ITypeDescriptorContext"/> that provides a format context.
        /// </param>
        /// <param name="culture">
        /// The <see cref="CultureInfo"/> to use as the current culture.
        /// </param>
        /// <param name="value">The <see cref="Object"/> to convert.</param>
        /// <returns>An <see cref="Object"/> that represents the converted value.</returns>
        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            if (value.GetType() == typeof(System.String) && context.PropertyDescriptor != null && context.PropertyDescriptor.PropertyType.IsAssignableFrom(typeof(ICultureIdentifier)))
            {
                string actualValue = value.ToString();
                if (actualValue == CultureIdentifiers.CountryRegions.Spanish_Spain_TraditionalSort)
                    return CultureIdentifiers.DefaultCultureIDByCultureNumber[CultureIdentifiers.NumericIdentifiers.Spanish_Spain];
                else if (actualValue == CultureIdentifiers.CountryRegions.Spanish_Spain_InternationalSort)
                    return CultureIdentifiers.DefaultCultureIDByCultureNumber[CultureIdentifiers.NumericIdentifiers.Spanish_Spain];
                foreach (CultureIdentifier c in CultureIdentifiers.DefaultCultureIDByCultureNumber.Values)
                {
                    if (c.CountryRegion == actualValue)
                        return c;
                }
            }
            return base.ConvertFrom(context, culture, value);
        }

        /// <summary>
        /// Returns whether this converter can convert the object to the specified type,
        /// using the specified context.
        /// </summary>
        /// <param name="context">An <see cref="ITypeDescriptorContext"/> that provides a format context.</param>
        /// <param name="destinationType">A <see cref="Type"/> that represents the type you want to convert to.</param>
        /// <returns>true if this converter can perform the conversion; otherwise, false.</returns>
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(string))
                return (context.PropertyDescriptor.PropertyType.IsAssignableFrom(typeof(ICultureIdentifier)));
            return base.CanConvertTo(context, destinationType);
        }

        /// <summary>
        /// Returns whether the resultant set of values returned by
        /// <see cref="TypeConverter.GetStandardValues()"/> is an 
        /// exclusive list of values with the <paramref name="context"/> provided.
        /// </summary>
        /// <param name="context">The <see cref="ITypeDescriptorContext"/>
        /// that provides a format context.</param>
        /// <returns>true if the values returned from 
        /// <see cref="TypeConverter.GetStandardValues()"/> is an exhaustive
        /// list of possible values; false, if other values are possible.
        /// </returns>
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            if (context != null && context.PropertyDescriptor != null)
                return (context.PropertyDescriptor.PropertyType.IsAssignableFrom(typeof(ICultureIdentifier)));
            else
                return base.GetStandardValuesExclusive(context);
        }
        
        /// <summary>
        /// Converts the <paramref name="value"/> to the <paramref name="destinationType"/>
        /// given the <paramref name="culture"/> and <paramref name="context"/> provided.
        /// </summary>
        /// <param name="context">The <see cref="ITypeDescriptorContext"/> that
        /// provides a format context.</param>
        /// <param name="culture">The <see cref="CultureInfo"/> that may influence
        /// the conversion; if null is passed, the current culture is used.</param>
        /// <param name="value">The <see cref="System.Object"/> to convert.</param>
        /// <param name="destinationType">The <see cref="Type"/> to convert
        /// the <paramref name="value"/> to.</param>
        /// <returns>An <see cref="Object"/> that represents the converted <paramref name="value"/>.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="destinationType"/>
        /// is null.</exception>
        /// <exception cref="System.NotSupportedException">The conversion cannot be performed.</exception>
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(String) && value is ICultureIdentifier)
                return ((ICultureIdentifier)value).CountryRegion;
            return base.ConvertTo(context, culture, value, destinationType);
        }

        /// <summary>
        /// Returns a collection of standard values for the data type this type converter
        /// is designed for when provided with a format <paramref name="context"/>.
        /// </summary>
        /// <param name="context">
        /// An <see cref="ITypeDescriptorContext"/> that provides a format context
        /// that can be used to extract additional information about the environment
        /// from which <see cref="CultureIdentifierTypeConverter"/> is invoked. 
        /// This parameter or properties of this parameter can be null.
        /// </param>
        /// <returns>
        /// A <see cref="TypeConverter.StandardValuesCollection"/> that holds
        /// a standard set of valid values, or null if the data type does not support
        /// a standard set of values.
        /// </returns>
        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            if (context != null && context.PropertyDescriptor != null && context.PropertyDescriptor.PropertyType.IsAssignableFrom(typeof(ICultureIdentifier)))
                return new StandardValuesCollection(new List<ICultureIdentifier>(CultureIdentifiers.DefaultCultureIDByCultureNumber.Values.ToArray().Cast<ICultureIdentifier>()));
            return base.GetStandardValues(context);
        }

        /// <summary>
        /// Returns whether this object supports a standard set of values that can be
        /// picked from a list, using the specified <paramref name="context"/>.
        /// </summary>
        /// <param name="context">An <see cref="ITypeDescriptorContext"/> that provides a format context.</param>
        /// <returns>
        /// true if <see cref="TypeConverter.GetStandardValues()"/> should be called to find a common set 
        /// of values the object supports; otherwise, false.
        /// </returns>
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            if (context != null && context.PropertyDescriptor != null)
                return (context.PropertyDescriptor.PropertyType.IsAssignableFrom(typeof(ICultureIdentifier)));
            return base.GetStandardValuesSupported(context);
        }
    }
}
