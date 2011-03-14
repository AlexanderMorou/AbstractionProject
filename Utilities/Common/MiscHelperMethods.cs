using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Utilities.Common
{
    /// <summary>
    /// Provides miscellaneous helper methods.
    /// </summary>
    public static class MiscHelperMethods
    {
        /// <summary>
        /// Obtains the caller information relative to the method that called
        /// this function.
        /// </summary>
        /// <returns>A <see cref="Tuple{T1, T2}"/> with the <see cref="Type"/> and
        /// <see cref="MethodBase"/> specific to the caller; or null if the 
        /// caller cannot be identified.</returns>
        /// <remarks>See: http://haacked.com/archive/2006/08/11/HowToGetTheCallingMethodAndType.aspx
        /// </remarks>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static Tuple<Type, MethodBase> GetCallerInfo()
        {
            var sf = new StackFrame(2, false);
            var mi = sf.GetMethod();
            if (mi == null)
                return null;
            return new Tuple<Type, MethodBase>(mi.DeclaringType, mi);
        }
    }
}