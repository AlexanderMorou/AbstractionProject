using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Compilers
{
    public interface ICompilerSourceModelError<T1> :
        ICompilerError,
        ICompilerSourceModelMessage
    {
        /// <summary>
        /// Returns the <typeparamref name="T1"/>
        /// which denotes the first element relative to
        /// the model error.
        /// </summary>
        T1 Item1 { get; }
    }
    public interface ICompilerSourceModelError<T1, T2> :
        ICompilerSourceModelError<T1>
    {
        /// <summary>
        /// Returns the <typeparamref name="T2"/>
        /// which denotes the second element relative to 
        /// the model error.
        /// </summary>
        T2 Item2 { get; }
    }
    public interface ICompilerSourceModelError<T1, T2, T3> :
        ICompilerSourceModelError<T1, T2>
    {
        /// <summary>
        /// Returns the <typeparamref name="T3"/>
        /// which denotes the third element relative to 
        /// the model error.
        /// </summary>
        T3 Item3 { get; }
    }
    public interface ICompilerSourceModelError<T1, T2, T3, T4> :
        ICompilerSourceModelError<T1, T2, T3>
    {
        /// <summary>
        /// Returns the <typeparamref name="T4"/>
        /// which denotes the fourth element relative to 
        /// the model error.
        /// </summary>
        T4 Item4 { get; }
    }
}
