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
namespace AllenCopeland.Abstraction.Utilities.Collections
{
    
    // Module: RootModule
    /// <summary>
    /// Provides a pair for the keys and values associated to a multi-keyed dictionary.
    /// </summary>
    /// <typeparam name="TKeys">The type of the keys tuple struct used in the pair.</typeparam>
    /// <typeparam name="TValue">The type of the value used in the pair.</typeparam>
    public struct KeysValuePair<TKeys, TValue>
    {
        #region KeysValuePair data members
        /// <summary>
        /// Data member for <typeparamref name="TKeys"/>.
        /// </summary>
        private TKeys _keys;
        
        /// <summary>
        /// Data member for <typeparamref name="TValue"/>.
        /// </summary>
        private TValue _value;
        #endregion // KeysValuePair data members
        #region KeysValuePair properties
        /// <summary>
        /// Returns the <typeparamref name="TKeys"/> which relate to the keys associated to the pair.
        /// </summary>
        public TKeys Keys
        {
            get
            {
                return this._keys;
            }
        }
        
        /// <summary>
        /// Returns the <typeparamref name="TValue"/> which relates to the value associated to the pair.
        /// </summary>
        public TValue Value
        {
            get
            {
                return this._value;
            }
        }
        #endregion // KeysValuePair properties

        #region KeysValuePair .ctors
        /// <summary>
        /// Creates a new <see cref="KeysValuePair{TKeys, TValue}"/> with the
        /// <paramref name="keys"/> and <paramref name="value"/> provided.
        /// </summary>
        /// <param name="keys">The <typeparamref name="TKeys"/> value which denotes the keys of the pair.</param>
        /// <param name="value">The <typeparamref name="TValue"/> value which denotes the value of the pair.</param>
        public KeysValuePair(TKeys keys, TValue value)
        {
            this._keys = keys;
            this._value = value;
        }
        #endregion // KeysValuePair .ctors

        #region KeysValuePair Methods
        public override string ToString()
        {
            return string.Format("{0} - {1}", this.Keys.ToString(), this.Value);
        }
        #endregion
    }
}
 /* ----------------------------------------------\
 |  This file took 00:00:00.0017434 to generate.  |
 |  Date generated: 11/24/2011 3:33:02 AM         |
 |  There were 2 types used by this file          |
 |  Keys, Value                                   |
 \---------------------------------------------- */
