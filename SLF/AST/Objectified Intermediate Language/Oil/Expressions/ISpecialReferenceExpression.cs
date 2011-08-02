using System;
using System.Collections.Generic;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    /// <summary>
    /// The kind of reference associated to a special concept
    /// which can alter member lookup.
    /// </summary>
    public enum SpecialReferenceKind
    {
        /// <summary>
        /// The special reference doesn't refer
        /// to any of the specified reference kinds.
        /// </summary>
        /// <remarks>Used to aid auto context switching 
        /// expression for intermediate members.</remarks>
        None,
        /// <summary>
        /// The special reference refers to the object itself
        /// ignoring standard virtual calling conventions.
        /// </summary>
        /// <remarks>
        /// <para>C&#9839;: N/A</para>
        /// <para>VB: MyClass</para>
        /// <para>CIL: call [instance]</para>
        /// </remarks>
        Self,
        /// <summary>
        /// The special reference refers to the object itself
        /// ignoring standard virtual calling conventions.
        /// </summary>
        /// <remarks>
        /// <para>C&#9839;: base</para>
        /// <para>VB: MyBase</para></remarks>
        Base,
        /// <summary>
        /// The special reference refers to the object itself
        /// ignoring standard virtual calling conventions.
        /// </summary>
        /// <remarks>
        /// <para>C&#9839;: this</para>
        /// <para>VB: Me</para></remarks>
        This,
    }

    public interface ISpecialReferenceExpression :
        IUnaryOperationPrimaryTerm,
        IMemberParentReferenceExpression
    {
        /// <summary>
        /// Returns the kind of special reference the reference is.
        /// </summary>
        SpecialReferenceKind Kind { get; }
    }
}
