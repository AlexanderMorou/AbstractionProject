using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Preprocessors
{
    /// <summary>
    /// Defines properties and methods for working with a
    /// preprocessor task.
    /// </summary>
    public interface IPreprocessorTask
    {
        /// <summary>
        /// Returns the <see cref="String"/> value representing
        /// the name of the <see cref="IPreprocessorTask"/>.
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Returns the <see cref="PreprocessorTaskPriority"/> relative to the 
        /// priority associated to the active <see cref="IPreprocessorTask"/>.
        /// </summary>
        PreprocessorTaskPriority Priority { get; }
        /// <summary>
        /// Returns the <see cref="PreprocessorTaskCategory"/> relative to the
        /// category associated to the active <see cref="IPreprocessorTask"/>.
        /// </summary>
        PreprocessorTaskCategory Category { get; }
    }
    /// <summary>
    /// Describes the task priority of an <see cref="IPreprocessorTask"/>.
    /// </summary>
    public enum PreprocessorTaskPriority
    {
        /// <summary>
        /// Preprocessor tasks with first priority describe language feature implementation
        /// which require precedence over extensions.
        /// </summary>
        First,
        /// <summary>
        /// Preprocessor tasks with higher priority describe extension behavior 
        /// which can potentially provide intermediate code rewrites.
        /// </summary>
        Higher,
        /// <summary>
        /// <para>Preprocessor tasks with normal priority describe default behavior of a
        /// preprocessor.</para>
        /// <para>Typical example is standard member/type resolution.</para>
        /// </summary>
        Normal,
        /// <summary>
        /// <para>Preprocessor tasks with lower priority suppliment the normal
        /// priority or accept what it could not.</para>
        /// <para>Typical example is extension methods which do not fall within the
        /// active scope.</para>
        /// </summary>
        Lower,
        /// <summary>
        /// <para>Preprocessor tasks with final priority are the end-all.</para>
        /// <para>Typical example is compile errors when a symbol cannot be resolved
        /// as a type or a member.</para>
        /// </summary>
        Final,
    }
}
