using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Compilers
{
    public class CompilerModelError<T1> :
        ICompilerModelError<T1>
    {
        private ICompilerReferenceError message;
        private string[] replacements;

        public CompilerModelError(ICompilerReferenceError message, T1 item1, params string[] replacements)
        {
            this.message = message;
            this.Item1 = item1;
            this.replacements = replacements;
        }

        #region ICompilerModelError<T1> Members

        /// <summary>
        /// Returns the <typeparamref name="T1"/>
        /// which denotes the first element relative to
        /// the model error.
        /// </summary>
        public T1 Item1 { get; private set; }

        #endregion

        #region ICompilerMessage Members

        public int MessageIdentifier
        {
            get { return message.MessageIdentifier; }
        }

        #endregion

        #region ICompilerModelMessage Members

        public string Message
        {
            get { return string.Format(this.message.MessageBase, replacements); }
        }

        #endregion
    }
    public class CompilerModelError<T1, T2> :
        CompilerModelError<T1>,
        ICompilerModelError<T1, T2>
    {
        public CompilerModelError(ICompilerReferenceError message, T1 item1, T2 item2, params string[] replacements)
            : base(message, item1, replacements)
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
    public class CompilerModelError<T1, T2, T3> :
        CompilerModelError<T1, T2>,
        ICompilerModelError<T1, T2, T3>
    {
        public CompilerModelError(ICompilerReferenceError message, T1 item1, T2 item2, T3 item3, params string[] replacements)
            : base(message, item1, item2, replacements)
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
    public class CompilerModelError<T1, T2, T3, T4> :
        CompilerModelError<T1, T2, T3>,
        ICompilerModelError<T1, T2, T3, T4>
    {
        public CompilerModelError(ICompilerReferenceError message, T1 item1, T2 item2, T3 item3, T4 item4, params string[] replacements)
            : base(message, item1, item2, item3, replacements)
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
