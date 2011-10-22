using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Compilers
{
    public interface ICompilerModelError<T1> :
        ICompilerError,
        ICompilerModelMessage
    {
        /// <summary>
        /// Returns the <typeparamref name="T1"/>
        /// which denotes the first element relative to
        /// the model error.
        /// </summary>
        T1 Item1 { get; }
    }
    public interface ICompilerModelError<T1, T2> :
        ICompilerModelError<T1>
    {
        /// <summary>
        /// Returns the <typeparamref name="T2"/>
        /// which denotes the second element relative to 
        /// the model error.
        /// </summary>
        T2 Item2 { get; }
    }
    public interface ICompilerModelError<T1, T2, T3> :
        ICompilerModelError<T1, T2>
    {
        /// <summary>
        /// Returns the <typeparamref name="T3"/>
        /// which denotes the third element relative to 
        /// the model error.
        /// </summary>
        T3 Item3 { get; }
    }
    public interface ICompilerModelError<T1, T2, T3, T4> :
        ICompilerModelError<T1, T2, T3>
    {
        /// <summary>
        /// Returns the <typeparamref name="T4"/>
        /// which denotes the fourth element relative to 
        /// the model error.
        /// </summary>
        T4 Item4 { get; }
    }
}
