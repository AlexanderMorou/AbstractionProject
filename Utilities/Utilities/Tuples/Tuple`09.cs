 /* -----------------------------------------------------------\
 |  This code was generated by Oilexer.                        |
 |  Version: 1.0.0.19203                                       |
 |-------------------------------------------------------------|
 |  To ensure the code works properly,                         |
 |  please do not make any changes to the file.                |
 |-------------------------------------------------------------|
 |  The specific language is C# (Runtime version: v4.0.30319)  |
 |  Sub-tool Name: Oilexer.CSharpCodeTranslator                |
 |  Sub-tool Version: 1.0.0.19203                              |
 \----------------------------------------------------------- */
using System;
 /*---------------------------------------------------------------------\
 | Copyright � 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Utilities.Tuples
{
    // Module: RootModule
    /// <summary>
    /// Represents a 9-tuple, or a nonuple
    /// </summary>
    /// <typeparam name="T1">The type of the nonuple's primary component.</typeparam>
    /// <typeparam name="T2">The type of the nonuple's secondary component.</typeparam>
    /// <typeparam name="T3">The type of the nonuple's tertiary component.</typeparam>
    /// <typeparam name="T4">The type of the nonuple's quaternary component.</typeparam>
    /// <typeparam name="T5">The type of the nonuple's quinary component.</typeparam>
    /// <typeparam name="T6">The type of the nonuple's senary component.</typeparam>
    /// <typeparam name="T7">The type of the nonuple's septenary component.</typeparam>
    /// <typeparam name="T8">The type of the nonuple's octonary component.</typeparam>
    /// <typeparam name="T9">The type of the nonuple's nonary component.</typeparam>
    public class Tuple<T1, T2, T3, T4, T5, T6, T7, T8, T9> :
        Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9>>
    {
        #region Tuple .ctors
        /// <summary>
        /// Creates a new <see cref="Tuple{T1, T2, T3, T4, T5, T6, T7, T8, T9}"/>
        /// with the first through ninth components provided.
        /// </summary>
        /// <param name="item1">The value of the nonuple's primary component.</param>
        /// <param name="item2">The value of the nonuple's secondary component.</param>
        /// <param name="item3">The value of the nonuple's tertiary component.</param>
        /// <param name="item4">The value of the nonuple's quaternary component.</param>
        /// <param name="item5">The value of the nonuple's quinary component.</param>
        /// <param name="item6">The value of the nonuple's senary component.</param>
        /// <param name="item7">The value of the nonuple's septenary component.</param>
        /// <param name="item8">The value of the nonuple's octonary component.</param>
        /// <param name="item9">The value of the nonuple's nonary component.</param>
        public Tuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8, T9 item9)
             : base(item1, item2, item3, 
                    item4, item5, item6, 
                    item7, new Tuple<T8, T9>(item8, item9))
        {
        }
        #endregion // Tuple .ctors
    }
}
 /* ---------------------------------------------------------------------------\
 |  This file took 00:00:00.0065018 to generate.                               |
 |  Date generated: 8/31/2011 11:53:54 AM                                      |
 |  There were 11 types used by this file                                      |
 |  1, 2, 3, 4, 5, 6, 7, 8, 9, <T1,T2,T3,T4,T5,T6,T7,System.Tuple`2<T8,T9>>,   |
 |  Tuple`2<T8,T9>                                                             |
 |-----------------------------------------------------------------------------|
 |  There were 1 assemblies referenced:                                        |
 |  mscorlib                                                                   |
 \--------------------------------------------------------------------------- */