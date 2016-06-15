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
    /// <summary>
    /// Defines properties and methods for working with a
    /// compiler model warning which relates to a specific 
    /// portion of the object model defined in <typeparamref name="T1"/>.
    /// </summary>
    /// <typeparam name="T1">The point of reference
    /// to which the <see cref="ICompilerModelWarning{T1}"/>
    /// relates.</typeparam>
    public interface ICompilerModelWarning<T1> :
        ICompilerModelMessage
    {
        /// <summary>
        /// Returns the <typeparamref name="T1"/>
        /// which denotes the first element relative to
        /// the model warning.
        /// </summary>
        T1 Item1 { get; }
    }
    /// <summary>
    /// Defines properties and methods for working with a
    /// compiler model warning which relates to a specific 
    /// portion of the object model defined in <typeparamref name="T1"/>
    /// and <typeparamref name="T2"/>.
    /// </summary>
    /// <typeparam name="T1">The type of the first point of
    /// reference to which the <see cref="ICompilerModelWarning{T1, T2}"/>
    /// relates.</typeparam>
    /// <typeparam name="T2">The type of the second point of 
    /// reference to which the <see cref="ICompilerModelWarning{T1, T2}"/>
    /// relates.</typeparam>
    public interface ICompilerModelWarning<T1, T2> :
        ICompilerModelMessage
    {
        /// <summary>
        /// Returns the <typeparamref name="T1"/>
        /// which denotes the first element relative to
        /// the model warning.
        /// </summary>
        T1 Item1 { get; }
        /// <summary>
        /// Returns the <typeparamref name="T2"/>
        /// which denotes the second element relative to 
        /// the model warning.
        /// </summary>
        T2 Item2 { get; }
    }
    /// <summary>
    /// Defines properties and methods for working with a
    /// compiler model warning which relates to a specific 
    /// portion of the object model defined in <typeparamref name="T1"/>
    /// <typeparamref name="T2"/>, and <typeparamref name="T3"/>.
    /// </summary>
    /// <typeparam name="T1">The type of the first point of
    /// reference to which the <see cref="ICompilerModelWarning{T1, T2, T3}"/>
    /// relates.</typeparam>
    /// <typeparam name="T2">The type of the second point of 
    /// reference to which the <see cref="ICompilerModelWarning{T1, T2, T3}"/>
    /// relates.</typeparam>
    /// <typeparam name="T3">The type of the third point of
    /// reference to which the <see cref="ICompilerModelWarning{T1, T2, T3}"/>
    /// relates.</typeparam>
    public interface ICompilerModelWarning<T1, T2, T3> :
        ICompilerModelMessage
    {
        /// <summary>
        /// Returns the <typeparamref name="T1"/>
        /// which denotes the first element relative to
        /// the model warning.
        /// </summary>
        T1 Item1 { get; }
        /// <summary>
        /// Returns the <typeparamref name="T2"/>
        /// which denotes the second element relative to 
        /// the model warning.
        /// </summary>
        T2 Item2 { get; }
        /// <summary>
        /// Returns the <typeparamref name="T3"/>
        /// which denotes the third element relative to 
        /// the model warning.
        /// </summary>
        T3 Item3 { get; }
    }
    public interface ICompilerModelWarning<T1, T2, T3, T4> :
        ICompilerModelMessage
    {
        /// <summary>
        /// Returns the <typeparamref name="T1"/>
        /// which denotes the first element relative to
        /// the model warning.
        /// </summary>
        T1 Item1 { get; }
        /// <summary>
        /// Returns the <typeparamref name="T2"/>
        /// which denotes the second element relative to 
        /// the model warning.
        /// </summary>
        T2 Item2 { get; }
        /// <summary>
        /// Returns the <typeparamref name="T3"/>
        /// which denotes the third element relative to 
        /// the model warning.
        /// </summary>
        T3 Item3 { get; }
        /// <summary>
        /// Returns the <typeparamref name="T4"/>
        /// which denotes the fourth element relative to 
        /// the model warning.
        /// </summary>
        T4 Item4 { get; }
    }
}
