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
    public class CompilerSourceModelError<T1> :
        CompilerSourceError,
        ICompilerSourceModelError<T1>
    {
        /// <summary>
        /// Creates a new <see cref="CompilerSourceModelError{T1}"/> 
        /// with the <paramref name="message"/>, <paramref name="item1"/>,
        /// <paramref name="fileName"/>, <paramref name="start"/>,
        /// <paramref name="end"/>, and <paramref name="replacements"/>.
        /// </summary>
        /// <param name="message">The <see cref="ICompilerReferenceError"/>
        /// which defines the string value that the current error is
        /// built off of.</param>
        /// <param name="item1">The <typeparamref name="T1"/> value which
        /// denotes where within the model the error is derived.</param>
        /// <param name="fileName">The <see cref="String"/> value denoting
        /// the file in which the warning occurred.</param>
        /// <param name="start">The <see cref="LineColumnPair"/> which
        /// denotes the line and column of the start of the error.</param>
        /// <param name="end">The <see cref="LineColumnPair"/> which denotes
        /// the line and column of the end of the error.</param>
        /// <param name="replacements">The <see cref="String"/> array of elements
        /// which denotes the replacements within <paramref name="message"/> to build a unique
        /// message relative to the current error.</param>
        public CompilerSourceModelError(ICompilerReferenceError message, T1 item1, string fileName, LineColumnPair start, LineColumnPair end, params string[] replacements)
            : base(message, fileName, start, end, replacements)
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
        CompilerSourceError,
        ICompilerSourceModelError<T1, T2>
    {
        /// <summary>
        /// Creates a new <see cref="CompilerSourceModelError{T1, T2}"/> 
        /// with the <paramref name="message"/>, <paramref name="item1"/>,
        /// <paramref name="item2"/>, <paramref name="fileName"/>,
        /// <paramref name="start"/>, <paramref name="end"/>,
        /// and <paramref name="replacements"/>.
        /// </summary>
        /// <param name="message">The <see cref="ICompilerReferenceError"/>
        /// which defines the string value that the current error is
        /// built off of.</param>
        /// <param name="item1">The <typeparamref name="T1"/> value which
        /// denotes where within the model the error is derived.</param>
        /// <param name="item2">The <typeparamref name="T2"/> value which
        /// denotes further information on where within the model the
        /// error is derived.</param>
        /// <param name="fileName">The <see cref="String"/> value denoting
        /// the file in which the warning occurred.</param>
        /// <param name="start">The <see cref="LineColumnPair"/> which
        /// denotes the line and column of the start of the error.</param>
        /// <param name="end">The <see cref="LineColumnPair"/> which denotes
        /// the line and column of the end of the error.</param>
        /// <param name="replacements">The <see cref="String"/> array of elements
        /// which denotes the replacements within <paramref name="message"/> to build a unique
        /// message relative to the current error.</param>
        public CompilerSourceModelError(ICompilerReferenceError message, T1 item1, T2 item2, string fileName, LineColumnPair start, LineColumnPair end, params string[] replacements)
            : base(message, fileName, start, end, replacements)
        {
            this.Item1 = item1;
            this.Item2 = item2;
        }
        #region ICompilerModelError<T1,T2> Members

        /// <summary>
        /// Returns the <typeparamref name="T1"/>
        /// which denotes the first element relative to
        /// the model error.
        /// </summary>
        public T1 Item1 { get; private set; }

        /// <summary>
        /// Returns the <typeparamref name="T2"/>
        /// which denotes the second element relative to 
        /// the model error.
        /// </summary>
        public T2 Item2 { get; private set; }

        #endregion
    }
    public class CompilerSourceModelError<T1, T2, T3> :
        CompilerSourceError,
        ICompilerSourceModelError<T1, T2, T3>
    {
        /// <summary>
        /// Creates a new <see cref="CompilerSourceModelError{T1, T2, T3}"/> 
        /// with the <paramref name="message"/>, <paramref name="item1"/>,
        /// <paramref name="item2"/>, <paramref name="item3"/>, 
        /// <paramref name="fileName"/>, <paramref name="start"/>,
        /// <paramref name="end"/>, and <paramref name="replacements"/>.
        /// </summary>
        /// <param name="message">The <see cref="ICompilerReferenceError"/>
        /// which defines the string value that the current error is
        /// built off of.</param>
        /// <param name="item1">The <typeparamref name="T1"/> value which
        /// denotes where within the model the error is derived.</param>
        /// <param name="item2">The <typeparamref name="T2"/> value which
        /// denotes further information on where within the model the
        /// error is derived.</param>
        /// <param name="item3">The <typeparamref name="T3"/> value which
        /// denotes further information on where within the model the
        /// error is derived.</param>
        /// <param name="fileName">The <see cref="String"/> value denoting
        /// the file in which the warning occurred.</param>
        /// <param name="start">The <see cref="LineColumnPair"/> which
        /// denotes the line and column of the start of the error.</param>
        /// <param name="end">The <see cref="LineColumnPair"/> which denotes
        /// the line and column of the end of the error.</param>
        /// <param name="replacements">The <see cref="String"/> array of elements
        /// which denotes the replacements within <paramref name="message"/> to build a unique
        /// message relative to the current error.</param>
        public CompilerSourceModelError(ICompilerReferenceError message, T1 item1, T2 item2, T3 item3, string fileName, LineColumnPair start, LineColumnPair end, params string[] replacements)
            : base(message, fileName, start, end, replacements)
        {
            this.Item1 = item1;
            this.Item2 = item2;
            this.Item3 = item3;
        }
        
        #region ICompilerModelError<T1,T2,T3> Members

        /// <summary>
        /// Returns the <typeparamref name="T1"/>
        /// which denotes the first element relative to
        /// the model error.
        /// </summary>
        public T1 Item1 { get; private set; }

        /// <summary>
        /// Returns the <typeparamref name="T2"/>
        /// which denotes the second element relative to 
        /// the model error.
        /// </summary>
        public T2 Item2 { get; private set; }

        /// <summary>
        /// Returns the <typeparamref name="T3"/>
        /// which denotes the third element relative to 
        /// the model error.
        /// </summary>
        public T3 Item3 { get; private set; }

        #endregion
    }
    public class CompilerSourceModelError<T1, T2, T3, T4> :
        CompilerSourceError,
        ICompilerSourceModelError<T1, T2, T3, T4>
    {
        /// <summary>
        /// Creates a new <see cref="CompilerSourceModelError{T1, T2, T3, T4}"/> 
        /// with the <paramref name="message"/>, <paramref name="item1"/>,
        /// <paramref name="item2"/>, <paramref name="item3"/>,
        /// <paramref name="item4"/>, <paramref name="fileName"/>, 
        /// <paramref name="start"/>, <paramref name="end"/>,
        /// and <paramref name="replacements"/>.
        /// </summary>
        /// <param name="message">The <see cref="ICompilerReferenceError"/>
        /// which defines the string value that the current error is
        /// built off of.</param>
        /// <param name="item1">The <typeparamref name="T1"/> value which
        /// denotes where within the model the error is derived.</param>
        /// <param name="item2">The <typeparamref name="T2"/> value which
        /// denotes further information on where within the model the
        /// error is derived.</param>
        /// <param name="item3">The <typeparamref name="T3"/> value which
        /// denotes further information on where within the model the
        /// error is derived.</param>
        /// <param name="item4">The <typeparamref name="T4"/> value which
        /// denotes further information on where within the model the
        /// error is derived.</param>
        /// <param name="fileName">The <see cref="String"/> value denoting
        /// the file in which the warning occurred.</param>
        /// <param name="start">The <see cref="LineColumnPair"/> which
        /// denotes the line and column of the start of the error.</param>
        /// <param name="end">The <see cref="LineColumnPair"/> which denotes
        /// the line and column of the end of the error.</param>
        /// <param name="replacements">The <see cref="String"/> array of elements
        /// which denotes the replacements within <paramref name="message"/> to build a unique
        /// message relative to the current error.</param>
        public CompilerSourceModelError(ICompilerReferenceError message, T1 item1, T2 item2, T3 item3, T4 item4, string fileName, LineColumnPair start, LineColumnPair end, params string[] replacements)
            : base(message, fileName, start, end, replacements)
        {
            this.Item1 = item1;
            this.Item2 = item2;
            this.Item3 = item3;
            this.Item4 = item4;
        }

        #region ICompilerModelError<T1,T2,T3,T4> Members

        /// <summary>
        /// Returns the <typeparamref name="T1"/>
        /// which denotes the first element relative to
        /// the model error.
        /// </summary>
        public T1 Item1 { get; private set; }

        /// <summary>
        /// Returns the <typeparamref name="T2"/>
        /// which denotes the second element relative to 
        /// the model error.
        /// </summary>
        public T2 Item2 { get; private set; }

        /// <summary>
        /// Returns the <typeparamref name="T3"/>
        /// which denotes the third element relative to 
        /// the model error.
        /// </summary>
        public T3 Item3 { get; private set; }

        /// <summary>
        /// Returns the <typeparamref name="T4"/>
        /// which denotes the fourth element relative to 
        /// the model error.
        /// </summary>
        public T4 Item4 { get; private set; }

        #endregion
    }
}
