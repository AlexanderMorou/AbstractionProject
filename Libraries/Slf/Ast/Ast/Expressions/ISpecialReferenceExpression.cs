using AllenCopeland.Abstraction.Slf.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Expressions
{
    /// <summary>
    /// The kind of reference associated to a special concept
    /// which can alter member lookup.
    /// </summary>
    public enum SpecialReferenceKind
    {
        /// <summary>
        /// The special reference doesn't refer to any of the specified reference kinds.
        /// </summary>
        /// <remarks>Used to aid auto context switching expression for intermediate members.</remarks>
        None,
        /// <summary>
        /// The special reference refers to the object itself ignoring standard virtual calling conventions.
        /// </summary>
        /// <remarks>
        /// <para>C&#9839;: N/A</para>
        /// <para>VB: MyClass</para>
        /// <para>CIL: call [instance]  //With semantics to denote the active type.</para>
        /// </remarks>
        Self,
        /// <summary>
        /// The special reference refers to the object's base-type ignoring standard virtual calling conventions.
        /// </summary>
        /// <remarks>
        /// <para>C&#9839;: base</para>
        /// <para>VB: MyBase</para>
        /// <para>CIL: call [instance] //With semantics to denote the base type.</para></remarks>
        Base,
        /// <summary>
        /// The special reference refers to the object itself ignoring standard virtual calling conventions.
        /// </summary>
        /// <remarks>
        /// <para>C&#9839;: this</para>
        /// <para>VB: Me</para>
        /// <para>CIL: callvirt [instance] //With semantics to denote the current type.</para></remarks>
        This,
    }

#if DEBUG
    [VisitorTargetAttribute("ExpressionVisitor", ContextualVisitor  = true)]
    [VisitorTargetAttribute("ExpressionVisitor", YieldingVisitor    = true)]
    [VisitorTargetAttribute("ExpressionVisitor", ContextualVisitor  = true,
                                                 YieldingVisitor    = true)]
    [VisitorTargetAttribute("ExpressionVisitor")]
    [VisitorTargetAttribute("IntermediateCodeVisitor", DerivedThroughInheriting = "ExpressionVisitor")]
    [VisitorTargetAttribute("IntermediateTreeVisitor", DerivedThroughInheriting = "IntermediateCodeVisitor")]
#endif
    public interface ISpecialReferenceExpression :
        IUnaryOperationPrimaryTerm,
        IMemberParentReferenceExpression,
        IFusionTargetExpression
    {
        /// <summary>
        /// Returns the kind of special reference the reference is.
        /// </summary>
        SpecialReferenceKind Kind { get; }
    }
}
