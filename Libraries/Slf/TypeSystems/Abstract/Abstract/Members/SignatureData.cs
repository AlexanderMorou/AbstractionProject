using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Abstract.Members
{
    /// <summary>
    /// Provides a simple signature construct for parametered member insertion.
    /// </summary>
    public class SignatureData
    {
        public static readonly SignatureData Empty = new SignatureData(new TypedName[0]);
        /// <summary>
        /// Data member for <see cref="Parameters"/>.
        /// </summary>
        private TypedName[] parameters;

        /// <summary>
        /// Creates a new <see cref="SignatureData"/> with the 
        /// <paramref name="parameters"/> provided.
        /// </summary>
        /// <param name="parameters">A series of <see cref="TypedName"/> instances
        /// that defines the types and names of the parameters defined
        /// in the current <see cref="SignatureData"/>.</param>
        public SignatureData(params TypedName[] parameters)
        {
            this.parameters = parameters;
        }

        /// <summary>
        /// Returns the <see cref="IEnumerable{T}"/> for the 
        /// <see cref="TypedName"/> series of parameters for the
        /// current <see cref="SignatureData"/>.
        /// </summary>
        public IEnumerable<TypedName> Parameters
        {
            get
            {
                if (this.parameters == null)
                    yield break;
                for (int i = 0; i < this.parameters.Length; i++)
                    yield return this.parameters[i];
                yield break;
            }
        }
        /// <summary>
        /// Converts the <see cref="TypedNameSeries"/> to a <see cref="SignatureData"/>
        /// instance.
        /// </summary>
        /// <param name="series">The <see cref="TypedNameSeries"/> to convert.</param>
        /// <returns>A new <see cref="SignatureData"/> instance equivalent to
        /// <paramref name="series"/>.</returns>
        public static implicit operator SignatureData(TypedNameSeries series)
        {
            return new SignatureData(series.Data.ToArray());
        }

        /// <summary>
        /// Converts the <see cref="SignatureData"/> <paramref name="source"/>
        /// into a <see cref="TypedNameSeries"/>.
        /// </summary>
        /// <param name="source">The <see cref="SignatureData"/> to convert.</param>
        /// <returns>A new <see cref="TypedNameSeries"/>
        /// equivalent to the <see cref="SignatureData"/> <paramref name="source"/>.</returns>
        public static implicit operator TypedNameSeries(SignatureData source)
        {
            return new TypedNameSeries(source.parameters);
        }

        /* *
         * For languages that don't support implicit type conversion overloads.
         * */
        /// <summary>
        /// Converts the <see cref="SignatureData"/> <paramref name="source"/>
        /// into a <see cref="TypedNameSeries"/>.
        /// </summary>
        /// <param name="source">The <see cref="SignatureData"/> to convert.</param>
        /// <returns>A new <see cref="TypedNameSeries"/></returns>
        public static TypedNameSeries ToTypedNameSeries(SignatureData source)
        {
            return source;
        }

        /// <summary>
        /// Converts the <see cref="TypedNameSeries"/> to a <see cref="SignatureData"/>
        /// instance.
        /// </summary>
        /// <param name="series">The <see cref="TypedNameSeries"/> to convert.</param>
        /// <returns>A new <see cref="SignatureData"/> instance equivalent to
        /// <paramref name="series"/>.</returns>
        public static SignatureData FromTypedNameSeries(TypedNameSeries series)
        {
            return series;
        }
    }
}
