using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Compilers
{
    /// <summary>
    /// Defines properties and methods for working with a
    /// compiler model warning which relates to a specific 
    /// portion of the source-based object model defined 
    /// in <typeparamref name="T1"/>.
    /// </summary>
    /// <typeparam name="T1">The point of reference
    /// to which the <see cref="ICompilerSourceModelWarning{T1}"/>
    /// relates.</typeparam>
    public interface ICompilerSourceModelWarning<T1> :
        ICompilerSourceModelMessage,
        ICompilerSourceWarning,
        ICompilerModelWarning<T1>
    {
    }
    /// <summary>
    /// Defines properties and methods for working with a
    /// compiler model warning which relates to a specific 
    /// portion of the source-based object model defined
    /// in <typeparamref name="T1"/> and <typeparamref name="T2"/>.
    /// </summary>
    /// <typeparam name="T1">The type of the first point of
    /// reference to which the <see cref="ICompilerSourceModelWarning{T1, T2}"/>
    /// relates.</typeparam>
    /// <typeparam name="T2">The type of the second point of 
    /// reference to which the <see cref="ICompilerSourceModelWarning{T1, T2}"/>
    /// relates.</typeparam>
    public interface ICompilerSourceModelWarning<T1, T2> :
        ICompilerSourceModelWarning<T1>,
        ICompilerModelWarning<T1, T2>
    {
    }
    /// <summary>
    /// Defines properties and methods for working with a
    /// compiler model warning which relates to a specific 
    /// portion of the source-based object model defined in 
    /// <typeparamref name="T1"/>
    /// <typeparamref name="T2"/>, and <typeparamref name="T3"/>.
    /// </summary>
    /// <typeparam name="T1">The type of the first point of
    /// reference to which the <see cref="ICompilerSourceModelWarning{T1, T2, T3}"/>
    /// relates.</typeparam>
    /// <typeparam name="T2">The type of the second point of 
    /// reference to which the <see cref="ICompilerSourceModelWarning{T1, T2, T3}"/>
    /// relates.</typeparam>
    /// <typeparam name="T3">The type of the third point of
    /// reference to which the <see cref="ICompilerSourceModelWarning{T1, T2, T3}"/>
    /// relates.</typeparam>
    public interface ICompilerSourceModelWarning<T1, T2, T3> :
        ICompilerSourceModelWarning<T1, T2>,
        ICompilerModelWarning<T1, T2, T3>
    {
    }

    public interface ICompilerSourceModelWarning<T1, T2, T3, T4> :
        ICompilerSourceModelWarning<T1, T2, T3>,
        ICompilerModelWarning<T1, T2, T3, T4>
    {
    }
}
