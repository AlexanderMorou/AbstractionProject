﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Compilers
{
    public class CompilerSourceModelError<T1> :
        CompilerSourceError,
        ICompilerSourceModelError<T1>
    {
        public CompilerSourceModelError(ICompilerReferenceError message, T1 item1, string fileName, int line, int column, params string[] replacements)
            : base(message, fileName, line, column, replacements)
        {
            this.Item1 = item1;
        }
        #region ICompilerModelError<T1> Members

        /// <summary>
        /// Returns the <typeparamref name="T1"/>
        /// which denotes the first element relative to
        /// the model error.
        /// </summary>
        public T1 Item1 { get; private set; }

        #endregion
    }
    public class CompilerSourceModelError<T1,T2> :
        CompilerSourceModelError<T1>,
        ICompilerSourceModelError<T1, T2>
    {
        public CompilerSourceModelError(ICompilerReferenceError message, T1 item1, T2 item2, string fileName, int line, int column, params string[] replacements)
            : base(message, item1, fileName, line, column, replacements)
        {
            this.Item2 = item2;
        }
        #region ICompilerModelError<T1,T2> Members

        /// <summary>
        /// Returns the <typeparamref name="T2"/>
        /// which denotes the second element relative to 
        /// the model error.
        /// </summary>
        public T2 Item2 { get; private set; }

        #endregion
    }
    public class CompilerSourceModelError<T1, T2, T3> :
        CompilerSourceModelError<T1, T2>,
        ICompilerSourceModelError<T1, T2, T3>
    {
        public CompilerSourceModelError(ICompilerReferenceError message, T1 item1, T2 item2, T3 item3, string fileName, int line, int column, params string[] replacements)
            : base(message, item1, item2, fileName, line, column, replacements)
        {
            this.Item3 = item3;
        }
        
        #region ICompilerModelError<T1,T2,T3> Members

        /// <summary>
        /// Returns the <typeparamref name="T3"/>
        /// which denotes the third element relative to 
        /// the model error.
        /// </summary>
        public T3 Item3 { get; private set; }

        #endregion
    }
    public class CompilerSourceModelError<T1, T2, T3, T4> :
        CompilerSourceModelError<T1, T2, T3>,
        ICompilerSourceModelError<T1, T2, T3, T4>
    {
        public CompilerSourceModelError(ICompilerReferenceError message, T1 item1, T2 item2, T3 item3, T4 item4, string fileName, int line, int column, params string[] replacements)
            : base(message, item1, item2, item3, fileName, line, column, replacements)
        {
            this.Item4 = item4;
        }

        #region ICompilerModelError<T1,T2,T3,T4> Members

        /// <summary>
        /// Returns the <typeparamref name="T4"/>
        /// which denotes the fourth element relative to 
        /// the model error.
        /// </summary>
        public T4 Item4 { get; private set; }

        #endregion
    }
}