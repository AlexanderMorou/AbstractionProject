using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Preprocessors
{
    /// <summary>
    /// The scope of the preprocessor resolution task.
    /// </summary>
    public enum PreprocessorResolutionScope
    {
        /// <summary>
        /// The scope of the resolution task involves the locals of a
        /// method.
        /// </summary>
        Local,
        /// <summary>
        /// The scope of the resolution task involves the active type
        /// and the static members of (possible) declaring type(s).
        /// </summary>
        /// <remarks>If the enclosing scope of the member is static
        /// the members of the type viewed are the static members, whereas
        /// if the scope of the member is an instance member, the members
        /// are those of both the static and instance members.</remarks>
        Type,
        /// <summary>
        /// The scope of the resolution task involves the globally defined
        /// methods and fields within the assembly.
        /// </summary>
        Global,
        /// <summary>
        /// The scope of the resolution task involves the instance 
        /// members of the target type of an object creation expression.
        /// </summary>
        ObjectCreation,
        /// <summary>
        /// The scope of the resolution task involves the 
        /// instance members of the target type of a workspace expression and
        /// the scopes of potential parent workspace expressions.
        /// </summary>
        Workspace,
        /// <summary>
        /// The scope of the resolution task involves the instance members of a 
        /// member parent expression whose type is known; -or- the static members
        /// of a member parent expression which is a type.
        /// </summary>
        Member,
        /// <summary>
        /// The scope of the resolution task involves the static extension methods
        /// available within the current scope.
        /// </summary>
        Extension,
    }
    /// <summary>
    /// What to generate in the preprocessor task.
    /// </summary>
    public enum PreprocessorGenerationKind
    {
        /// <summary>
        /// The kind of generation being performed relates to the anonymous type definitions
        /// of the project.
        /// </summary>
        AnonymousTypeDefinitions,

        AnonymousTypeReferences,
        /// <summary>
        /// The kind of generation being performed relates to the language integrated
        /// query syntax extension method binding expansion.
        /// </summary>
        Queries,
        /// <summary>
        /// The kind of generation being perofmred relates to the lambda method definition
        /// scope analysis and lifting.
        /// </summary>
        LambdaExtraction,
        /// <summary>
        /// The kind of generation being performed relates to the inline types defined within a
        /// method.
        /// </summary>
        InlineTypeDefinitions,
        /// <summary>
        /// The kind of generation being performed relates to the iterator logic associated
        /// to a given method with iterator logic that returns one of
        /// <see cref="IEnumerable"/>, <see cref="IEnumerator"/>, <see cref="IEnumerator{T}"/>
        /// or <see cref="IEnumerable{T}"/>.
        /// </summary>
        Iterators,
        DuckTypingDefinitions,
        /// <summary>
        /// 
        /// </summary>
        DuckTypingMarshalling,
    }
    /// <summary>
    /// The category the task belongs in.
    /// </summary>
    public enum PreprocessorTaskCategory
    {
        Aggregate,
        Resolution,
        Generation,
        Custom,
    }
}
