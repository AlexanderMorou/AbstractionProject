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
        /// The special reference refers to the type
        /// at the current hierarchy level.
        /// </summary>
        /// <remarks>
        /// C&#8902;: thisclass
        /// C&#9839;: N/A
        /// VB: N/A
        /// <para>CIL: call [instance] -
        /// To A method which accepts the current type as a type-parameter.</para>
        /// </remarks>
        CurrentClass,
        /// <summary>
        /// The special reference refers to the object itself
        /// ignoring standard virtual calling conventions.
        /// </summary>
        /// <remarks>
        /// C&#8902;: self
        /// C&#9839;: N/A
        /// VB: MyClass
        /// CIL: call [instance]
        /// </remarks>
        Self,
        /// <summary>
        /// The special reference refers to the object itself
        /// ignoring standard virtual calling conventions.
        /// </summary>
        /// <remarks>
        /// C&#8902;: base
        /// C&#9839;: base
        /// VB: MyBase</remarks>
        Base,
        /// <summary>
        /// The special reference refers to the object itself
        /// ignoring standard virtual calling conventions.
        /// </summary>
        /// <remarks>
        /// C&#8902;: this
        /// C&#9839;: this
        /// VB: Me</remarks>
        This,
    }

    public interface ISpecialReferenceExpression :
        IUnaryOperationPrimaryTerm,
        IMemberParentReferenceExpression
    {
        /// <summary>
        /// Returns the kind of special reference the reference is.
        /// </summary>
        SpecialReferenceKind Kind { get; set; }
    }
}
