using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using AllenCopeland.Abstraction.OldCodeGen.Utilities.Arrays;
using System.Collections.ObjectModel;
using System.Collections;

namespace AllenCopeland.Abstraction.OldCodeGen.Utilities.Collections
{
    /// <summary>
    /// Utility collection functions which have specialized functionality.
    /// </summary>
    public static class Special
    {
        public static T GetThisAt<T>(Stack<T> col, int index)
        {
            int i = 0;
            foreach (T t in col)
                if (i++ == index)
                    return t;
            return default(T);
        }
    }
}
