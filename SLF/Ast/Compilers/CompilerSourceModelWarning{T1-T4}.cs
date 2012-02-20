using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Compilers
{
    public class CompilerSourceModelWarning<T1> :
        CompilerSourceWarning,
        ICompilerSourceModelWarning<T1>
    {
        public CompilerSourceModelWarning(ICompilerReferenceWarning message, T1 item1, string fileName, LineColumnPair start, LineColumnPair end, params string[] replacements)
            : base(message, fileName, start, end, replacements)
        {
            this.Item1 = item1;
        }
        #region ICompilerModelWarning<T1> Members

        /// <summary>
        /// Returns the <typeparamref name="T1"/>
        /// which denotes the first element relative to
        /// the model warning.
        /// </summary>
        public T1 Item1 { get; private set; }

        #endregion
    }
    public class CompilerSourceModelWarning<T1, T2> :
        CompilerSourceModelWarning<T1>,
        ICompilerSourceModelWarning<T1, T2>
    {
        public CompilerSourceModelWarning(ICompilerReferenceWarning message, T1 item1, T2 item2, string fileName, LineColumnPair start, LineColumnPair end, params string[] replacements)
            : base(message, item1, fileName, start, end, replacements)
        {
            this.Item2 = item2;
        }
        #region ICompilerModelWarning<T1,T2> Members

        /// <summary>
        /// Returns the <typeparamref name="T2"/>
        /// which denotes the second element relative to 
        /// the model warning.
        /// </summary>
        public T2 Item2 { get; private set; }

        #endregion
    }
    public class CompilerSourceModelWarning<T1, T2, T3> :
        CompilerSourceModelWarning<T1, T2>,
        ICompilerSourceModelWarning<T1, T2, T3>
    {
        public CompilerSourceModelWarning(ICompilerReferenceWarning message, T1 item1, T2 item2, T3 item3, string fileName, LineColumnPair start, LineColumnPair end, params string[] replacements)
            : base(message, item1, item2, fileName, start, end, replacements)
        {
            this.Item3 = item3;
        }

        #region ICompilerModelWarning<T1,T2,T3> Members

        /// <summary>
        /// Returns the <typeparamref name="T3"/>
        /// which denotes the third element relative to 
        /// the model warning.
        /// </summary>
        public T3 Item3 { get; private set; }

        #endregion
    }
    public class CompilerSourceModelWarning<T1, T2, T3, T4> :
        CompilerSourceModelWarning<T1, T2, T3>,
        ICompilerSourceModelWarning<T1, T2, T3, T4>
    {
        public CompilerSourceModelWarning(ICompilerReferenceWarning message, T1 item1, T2 item2, T3 item3, T4 item4, string fileName, LineColumnPair start, LineColumnPair end, params string[] replacements)
            : base(message, item1, item2, item3, fileName, start, end, replacements)
        {
            this.Item4 = item4;
        }

        #region ICompilerModelWarning<T1,T2,T3,T4> Members

        /// <summary>
        /// Returns the <typeparamref name="T4"/>
        /// which denotes the fourth element relative to 
        /// the model warning.
        /// </summary>
        public T4 Item4 { get; private set; }

        #endregion
    }
}
