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
using System.Collections.Generic;

#if MKD_FOUR
namespace AllenCopeland.Abstraction.Utilities.Collections
{
    // Module: RootModule
    partial class MultikeyedDictionary<TKey1, TKey2, TKey3, TKey4, TValue>
    {
        #region MultikeyedDictionary nested types
        /// <summary>
        /// The third level of the <see cref="MultikeyedDictionary{TKey1, TKey2, TKey3, TKey4, TValue}"/>.
        /// Represents a pairing between <typeparamref name="TKey4"/> and <typeparamref name="TValue"/>.
        /// </summary>
        private class LevelThree :
            Dictionary<TKey4, TValue>
        {
        }
        #endregion // MultikeyedDictionary nested types
    }
} 
#endif
/* ----------------------------------------------\
 |  This file took 00:00:00.0002787 to generate.  |
 |  Date generated: 11/24/2011 3:33:02 AM         |
 |  There were 3 types used by this file          |
 |  Key4, Value, Dictionary`2<TKey4,TValue>       |
 |------------------------------------------------|
 |  There were 1 assemblies referenced:           |
 |  mscorlib                                      |
 \---------------------------------------------- */
