using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Compilers
{
    public interface ICompilerSourceModelError<T1> :
        ICompilerSourceModelMessage,
        ICompilerError,
        ICompilerModelError<T1>
    {
    }
    public interface ICompilerSourceModelError<T1, T2> :
        ICompilerSourceModelError<T1>,
        ICompilerModelError<T1, T2>
    {
    }
    public interface ICompilerSourceModelError<T1, T2, T3> :
        ICompilerSourceModelError<T1, T2>,
        ICompilerModelError<T1, T2, T3>
    {
    }
    public interface ICompilerSourceModelError<T1, T2, T3, T4> :
        ICompilerSourceModelError<T1, T2, T3>,
        ICompilerModelError<T1, T2, T3, T4>
    {
    }
}
