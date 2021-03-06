﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Compilers
{
    /// <summary>
    /// Provides a base implementation of a 
    /// compiler model warning which relates to a specific 
    /// portion of the source-based object model defined 
    /// in <typeparamref name="T1"/>.
    /// </summary>
    /// <typeparam name="T1">The point of reference
    /// to which the <see cref="CompilerSourceModelWarning{T1}"/>
    /// relates.</typeparam>
    public class CompilerSourceModelWarning<T1> :
        CompilerSourceWarning,
        ICompilerSourceModelWarning<T1>
    {
        /// <summary>
        /// Creates a new <see cref="CompilerSourceModelWarning{T1}"/>
        /// with the <paramref name="message"/>, <paramref name="item1"/>,
        /// <paramref name="source"/>, <paramref name="start"/>, 
        /// <paramref name="end"/>, and <paramref name="replacements"/>.
        /// </summary>
        /// <param name="message">The <see cref="ICompilerReferenceWarning"/> 
        /// which defines the string value that the current warning is built
        /// off of.</param>
        /// <param name="item1">The <typeparamref name="T1"/> value which
        /// denotes where within the model the warning is derived.</param>
        /// <param name="source">The <see cref="Uri"/> instance denoting
        /// the file in which the warning occurred.</param>
        /// <param name="start">The <see cref="LineColumnPair"/> which denotes
        /// the line and column of the start of the warning.</param>
        /// <param name="end">The <see cref="LineColumnPair"/> which denotes
        /// the line and column of the end of the warning.</param>
        /// <param name="replacements">The <see cref="String"/> array of
        /// elements which denotes the replacements within 
        /// <paramref name="message"/> to build a unique message relative
        /// to the current warning.</param>
        public CompilerSourceModelWarning(ICompilerReferenceWarning message, T1 item1, Uri source, LineColumnPair start, LineColumnPair end, params string[] replacements)
            : base(message, source, start, end, replacements)
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

    /// <summary>
    /// Provides a base implementation of a
    /// compiler model warning which relates to a specific 
    /// portion of the source-based object model defined
    /// in <typeparamref name="T1"/> and <typeparamref name="T2"/>.
    /// </summary>
    /// <typeparam name="T1">The type of the first point of
    /// reference to which the <see cref="CompilerSourceModelWarning{T1, T2}"/>
    /// relates.</typeparam>
    /// <typeparam name="T2">The type of the second point of 
    /// reference to which the <see cref="CompilerSourceModelWarning{T1, T2}"/>
    /// relates.</typeparam>
    public class CompilerSourceModelWarning<T1, T2> :
        CompilerSourceWarning,
        ICompilerSourceModelWarning<T1, T2>
    {
        /// <summary>
        /// Creates a new <see cref="CompilerSourceModelWarning{T1, T2}"/> 
        /// with the <paramref name="message"/>, <paramref name="item1"/>,
        /// <paramref name="item2"/>, <paramref name="source"/>, 
        /// <paramref name="start"/>, <paramref name="end"/>, and 
        /// <paramref name="replacements"/>.
        /// </summary>
        /// <param name="message">The <see cref="ICompilerReferenceWarning"/> which defines
        /// the string value that the current warning is built off of.</param>
        /// <param name="item1">The <typeparamref name="T1"/> value which denotes
        /// where within the model the warning is derived.</param>
        /// <param name="item2">The <typeparamref name="T2"/> value which
        /// denotes further information on where within the model the
        /// warning is derived.</param>
        /// <param name="source">The <see cref="Uri"/> instance denoting
        /// the file in which the warning occurred.</param>
        /// <param name="start">The <see cref="LineColumnPair"/> which denotes
        /// the line and column of the start of the warning.</param>
        /// <param name="end">The <see cref="LineColumnPair"/> which denotes
        /// the line and column of the end of the warning.</param>
        /// <param name="replacements">The <see cref="String"/> array of elements which denotes
        /// the replacements within <paramref name="message"/> to build a unique
        /// message relative to the current warning.</param>
        public CompilerSourceModelWarning(ICompilerReferenceWarning message, T1 item1, T2 item2, Uri source, LineColumnPair start, LineColumnPair end, params string[] replacements)
            : base(message, source, start, end, replacements)
        {
            this.Item1 = item1;
            this.Item2 = item2;
        }
        #region ICompilerModelWarning<T1,T2> Members

        /// <summary>
        /// Returns the <typeparamref name="T1"/>
        /// which denotes the first element relative to
        /// the model warning.
        /// </summary>
        public T1 Item1 { get; private set; }
        
        /// <summary>
        /// Returns the <typeparamref name="T2"/>
        /// which denotes the second element relative to 
        /// the model warning.
        /// </summary>
        public T2 Item2 { get; private set; }

        #endregion
    }

    /// <summary>
    /// Provides a base implementation of a
    /// compiler model warning which relates to a specific 
    /// portion of the source-based object model defined in 
    /// <typeparamref name="T1"/>
    /// <typeparamref name="T2"/>, and <typeparamref name="T3"/>.
    /// </summary>
    /// <typeparam name="T1">The type of the first point of
    /// reference to which the <see cref="CompilerSourceModelWarning{T1, T2, T3}"/>
    /// relates.</typeparam>
    /// <typeparam name="T2">The type of the second point of 
    /// reference to which the <see cref="CompilerSourceModelWarning{T1, T2, T3}"/>
    /// relates.</typeparam>
    /// <typeparam name="T3">The type of the third point of
    /// reference to which the <see cref="CompilerSourceModelWarning{T1, T2, T3}"/>
    /// relates.</typeparam>
    public class CompilerSourceModelWarning<T1, T2, T3> :
        CompilerSourceWarning,
        ICompilerSourceModelWarning<T1, T2, T3>
    {
        /// <summary>
        /// Creates a new <see cref="CompilerSourceModelWarning{T1, T2, T3}"/> 
        /// with the <paramref name="message"/>, <paramref name="item1"/>,
        /// <paramref name="item2"/>, <paramref name="item3"/>,
        /// <paramref name="source"/>, <paramref name="start"/>,
        /// <paramref name="end"/>, and <paramref name="replacements"/>.
        /// </summary>
        /// <param name="message">The <see cref="ICompilerReferenceWarning"/> which defines
        /// the string value that the current warning is built off of.</param>
        /// <param name="item1">The <typeparamref name="T1"/> value which denotes
        /// where within the model the warning is derived.</param>
        /// <param name="item2">The <typeparamref name="T2"/> value which
        /// denotes further information on where within the model the
        /// warning is derived.</param>
        /// <param name="item3">The <typeparamref name="T3"/> value which
        /// denotes further information on where within the model the
        /// warning is derived.</param>
        /// <param name="source">The <see cref="Uri"/> instance denoting
        /// the file in which the warning occurred.</param>
        /// <param name="start">The <see cref="LineColumnPair"/> which denotes
        /// the line and column of the start of the warning.</param>
        /// <param name="end">The <see cref="LineColumnPair"/> which denotes
        /// the line and column of the end of the warning.</param>
        /// <param name="replacements">The <see cref="String"/> array of elements which denotes
        /// the replacements within <paramref name="message"/> to build a unique
        /// message relative to the current warning.</param>
        public CompilerSourceModelWarning(ICompilerReferenceWarning message, T1 item1, T2 item2, T3 item3, Uri source, LineColumnPair start, LineColumnPair end, params string[] replacements)
            : base(message, source, start, end, replacements)
        {
            this.Item1 = item1;
            this.Item2 = item2;
            this.Item3 = item3;
        }

        #region ICompilerModelWarning<T1,T2,T3> Members

        /// <summary>
        /// Returns the <typeparamref name="T1"/>
        /// which denotes the first element relative to
        /// the model warning.
        /// </summary>
        public T1 Item1 { get; private set; }
        
        /// <summary>
        /// Returns the <typeparamref name="T2"/>
        /// which denotes the second element relative to 
        /// the model warning.
        /// </summary>
        public T2 Item2 { get; private set; }
        
        /// <summary>
        /// Returns the <typeparamref name="T3"/>
        /// which denotes the third element relative to 
        /// the model warning.
        /// </summary>
        public T3 Item3 { get; private set; }

        #endregion
    }

    /// <summary>
    /// Provides a base implementation of a
    /// compiler model warning which relates to a specific 
    /// portion of the source-based object model defined in 
    /// <typeparamref name="T1"/> <typeparamref name="T2"/>, 
    /// <typeparamref name="T3"/> and <typeparamref name="T4"/>.
    /// </summary>
    /// <typeparam name="T1">The type of the first point of
    /// reference to which the <see cref="ICompilerSourceModelWarning{T1, T2, T3, T4}"/>
    /// relates.</typeparam>
    /// <typeparam name="T2">The type of the second point of 
    /// reference to which the <see cref="ICompilerSourceModelWarning{T1, T2, T3, T4}"/>
    /// relates.</typeparam>
    /// <typeparam name="T3">The type of the third point of
    /// reference to which the <see cref="ICompilerSourceModelWarning{T1, T2, T3, T4}"/>
    /// relates.</typeparam>
    /// <typeparam name="T4">The type of the fourth point of 
    /// reference to which the <see cref="ICompilerSourceModelWarning{T1, T2, T3, T4}"/>
    /// relates.</typeparam>
    public class CompilerSourceModelWarning<T1, T2, T3, T4> :
        CompilerSourceWarning,
        ICompilerSourceModelWarning<T1, T2, T3, T4>
    {
        /// <summary>
        /// Creates a new <see cref="CompilerSourceModelWarning{T1, T2, T3, T4}"/> 
        /// with the <paramref name="message"/>, <paramref name="item1"/>,
        /// <paramref name="item2"/>, <paramref name="item3"/>,
        /// <paramref name="item4"/>, <paramref name="source"/>, 
        /// <paramref name="start"/>, <paramref name="end"/>,
        /// and <paramref name="replacements"/>.
        /// </summary>
        /// <param name="message">The <see cref="ICompilerReferenceWarning"/>
        /// which defines the string value that the current warning is
        /// built off of.</param>
        /// <param name="item1">The <typeparamref name="T1"/> value which
        /// denotes where within the model the warning is derived.</param>
        /// <param name="item2">The <typeparamref name="T2"/> value which
        /// denotes further information on where within the model the
        /// warning is derived.</param>
        /// <param name="item3">The <typeparamref name="T3"/> value which
        /// denotes further information on where within the model the
        /// warning is derived.</param>
        /// <param name="item4">The <typeparamref name="T4"/> value which
        /// denotes further information on where within the model the
        /// warning is derived.</param>
        /// <param name="source">The <see cref="Uri"/> instance denoting
        /// the file in which the warning occurred.</param>
        /// <param name="start">The <see cref="LineColumnPair"/> which
        /// denotes the line and column of the start of the warning.</param>
        /// <param name="end">The <see cref="LineColumnPair"/> which denotes
        /// the line and column of the end of the warning.</param>
        /// <param name="replacements">The <see cref="String"/> array of elements
        /// which denotes the replacements within <paramref name="message"/> to build a unique
        /// message relative to the current warning.</param>
        public CompilerSourceModelWarning(ICompilerReferenceWarning message, T1 item1, T2 item2, T3 item3, T4 item4, Uri source, LineColumnPair start, LineColumnPair end, params string[] replacements)
            : base(message, source, start, end, replacements)
        {
            this.Item1 = item1;
            this.Item2 = item2;
            this.Item3 = item3;
            this.Item4 = item4;
        }

        #region ICompilerModelWarning<T1,T2,T3,T4> Members
        /// <summary>
        /// Returns the <typeparamref name="T1"/>
        /// which denotes the first element relative to
        /// the model warning.
        /// </summary>
        public T1 Item1 { get; private set; }
        
        /// <summary>
        /// Returns the <typeparamref name="T2"/>
        /// which denotes the second element relative to 
        /// the model warning.
        /// </summary>
        public T2 Item2 { get; private set; }

        /// <summary>
        /// Returns the <typeparamref name="T3"/>
        /// which denotes the third element relative to 
        /// the model warning.
        /// </summary>
        public T3 Item3 { get; private set; }

        /// <summary>
        /// Returns the <typeparamref name="T4"/>
        /// which denotes the fourth element relative to 
        /// the model warning.
        /// </summary>
        public T4 Item4 { get; private set; }

        #endregion
    }
}
