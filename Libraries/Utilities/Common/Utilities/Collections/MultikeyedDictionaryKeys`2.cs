 /* -----------------------------------------------------------\
 |  This code was generated by Allen Copeland's Abstraction.   |
 |  Version: 1.0.0.11553                                       |
 |-------------------------------------------------------------|
 |  To ensure the code works properly,                         |
 |  please do not make any changes to the file.                |
 |-------------------------------------------------------------|
 |  The specific language is C# (Runtime version: v4.0.30319)  |
 |  Sub-tool Name: Abstraction's Old C♯ Code Translator        |
 |  Sub-tool Version: 1.0.0.11553                              |
 \----------------------------------------------------------- */
using System;

namespace AllenCopeland.Abstraction.Utilities.Collections
{
    
    // Module: RootModule
    /// <summary>
    /// Provides a tuple for the keys associated to a multi-keyed dictionary.
    /// </summary>
    /// <typeparam name="TKey1">The type of the first key.</typeparam>
    /// <typeparam name="TKey2">The type of the second key.</typeparam>
    public struct MultikeyedDictionaryKeys<TKey1, TKey2>
    {
        #region MultikeyedDictionaryKeys data members
        /// <summary>
        /// Data member for <see cref="Key1"/>.
        /// </summary>
        private TKey1 _key1;
        
        /// <summary>
        /// Data member for <see cref="Key2"/>.
        /// </summary>
        private TKey2 _key2;
        #endregion // MultikeyedDictionaryKeys data members

        public override bool Equals(object obj)
        {
            if (obj is MultikeyedDictionaryKeys<TKey1, TKey2>)
            {
                var mkdks = (MultikeyedDictionaryKeys<TKey1, TKey2>)obj;
                return this.Key1.Equals(mkdks.Key1) && 
                       this.Key2.Equals(mkdks.Key2);
            }
            else
                return false;
        }

        public static bool operator ==(MultikeyedDictionaryKeys<TKey1, TKey2> left, MultikeyedDictionaryKeys<TKey1, TKey2> right)
        {
            return left.Key1.Equals(right.Key1) && 
                   left.Key2.Equals(right.Key2);
        }

        public static bool operator !=(MultikeyedDictionaryKeys<TKey1, TKey2> left, MultikeyedDictionaryKeys<TKey1, TKey2> right)
        {
            return !left.Key1.Equals(right.Key1) ||
                   !left.Key2.Equals(right.Key2);
        }

        #region MultikeyedDictionaryKeys properties
        /// <summary>
        /// Returns the <typeparamref name="TKey1"/>; the first key of the set.
        /// </summary>
        public TKey1 Key1
        {
            get
            {
                return this._key1;
            }
        }
        
        /// <summary>
        /// Returns the <typeparamref name="TKey2"/>; the second key of the set.
        /// </summary>
        public TKey2 Key2
        {
            get
            {
                return this._key2;
            }
        }
        #endregion // MultikeyedDictionaryKeys properties
        #region MultikeyedDictionaryKeys methods
        /// <summary>
        /// Returns the string representation of the current <see cref="MultikeyedDictionaryKeys{TKey1, TKey2}"/>
        /// </summary>
        public override string ToString()
        {
            return string.Format("{{{0}, {1}}}", this.Key1, this.Key2);
        }
        #endregion // MultikeyedDictionaryKeys methods
        #region MultikeyedDictionaryKeys .ctors
        /// <summary>
        /// Creates a new <see cref="MultikeyedDictionaryKeys{TKey1, TKey2, TValue}"/>
        /// with the keys <paramref name="key1"/>, <paramref name="key2"/>
        /// provided.
        /// </summary>
        /// <param name="key1">The <typeparamref name="TKey1"/>; the first key of the set.</param>
        /// <param name="key2">The <typeparamref name="TKey2"/>; the second key of the set.</param>
        public MultikeyedDictionaryKeys(TKey1 key1, TKey2 key2)
        {
            this._key1 = key1;
            this._key2 = key2;
        }
        #endregion // MultikeyedDictionaryKeys .ctors
    }
}
 /* ----------------------------------------------\
 |  This file took 00:00:00.0005010 to generate.  |
 |  Date generated: 11/24/2011 3:33:02 AM         |
 |  There were 3 types used by this file          |
 |  Key1, Key2, String                            |
 |------------------------------------------------|
 |  There were 1 assemblies referenced:           |
 |  mscorlib                                      |
 \---------------------------------------------- */