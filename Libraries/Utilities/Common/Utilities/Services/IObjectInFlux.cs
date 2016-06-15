using AllenCopeland.Abstraction.Utilities.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Utilities.Services
{
    /// <summary>
    /// Defines properties and methods for working with an object in
    /// flux, or that may change due to high volatility.
    /// </summary>
    public interface IFluxDataProvider :
        IService<IFluxDataProvider>
    {
        /// <summary>
        /// Obtains a named 'In Flux' data store for the instance.
        /// </summary>
        /// <typeparam name="TKey">The type of key used in the resultant dictionary.</typeparam>
        /// <typeparam name="TValue">The type of values stored within the dictionary.</typeparam>
        /// <param name="dictionaryName">The name of the dictionary for the influx
        /// object.</param>
        /// <returns>A <see cref="IControlledDictionary{TKey, TValue}"/> which contains 
        /// data stored during the objects various states of flux.</returns>
        IControlledDictionary<TKey, TValue> GetFluxContentDictionary<TKey, TValue>(string dictionaryName);
        /// <summary>
        /// Obtains a named 'In Flux' data store for the instance.
        /// </summary>
        /// <typeparam name="TKey1">The type of the first key used in the resultant dictionary.</typeparam>
        /// <typeparam name="TKey2">The type of the second key used in the resultant dictionary.</typeparam>
        /// <typeparam name="TValue">The type of values stored within the dictionary.</typeparam>
        /// <param name="dictionaryName">The name of the dictionary for the influx
        /// object.</param>
        /// <returns>A <see cref="IControlledDictionary{TKey, TValue}"/> which contains 
        /// data stored during the objects various states of flux.</returns>
        IMultikeyedDictionary<TKey1, TKey2, TValue> GetFluxContentDictionary<TKey1, TKey2, TValue>(string dictionaryName);
    }
}
