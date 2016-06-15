using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
//using AllenCopeland.Abstraction.Slf.Cli;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Expressions
{
    public enum TypeOfKind
    {
        /// <summary>
        /// Uses the standardized type system.
        /// </summary>
        /// <remarks>Uses <see cref="System.Type"/> for all results.</remarks>
        General,
        /// <summary>
        /// Uses the specific type system.
        /// </summary>
        /// <remarks><para>The type that results from the typeof expression
        /// will change based off of the kind of type passed in.</para>
        /// <list type="bullet">
        /// <item><description>Classes will yield an <see cref="IClassType"/> instance.</description></item>
        /// <item><description>Delegates will yield an <see cref="IDelegateType"/> instance.</description></item>
        /// <item><description>Enums will yield an <see cref="IEnumType"/> instance.</description></item>
        /// <item><description>Interfaces will yield an <see cref="IInterfaceType"/> instance.</description></item>
        /// <item><description>Structs will yield an <see cref="IStructType"/> instance.</description></item>
        /// </list></remarks>
        Specific
    }
    /// <summary>
    /// Defines properties and methods for working with a type of expression 
    /// which loads the meta-data token of the type in question onto the
    /// stack.
    /// </summary>
#if DEBUG
    [VisitorTargetAttribute("ExpressionVisitor", ContextualVisitor  = true)]
    [VisitorTargetAttribute("ExpressionVisitor", YieldingVisitor    = true)]
    [VisitorTargetAttribute("ExpressionVisitor", ContextualVisitor  = true,
                                                 YieldingVisitor    = true)]
    [VisitorTargetAttribute("ExpressionVisitor")]
    [VisitorTargetAttribute("IntermediateCodeVisitor", DerivedThroughInheriting = "ExpressionVisitor")]
    [VisitorTargetAttribute("IntermediateTreeVisitor", DerivedThroughInheriting = "IntermediateCodeVisitor")]
#endif
    public interface ITypeOfExpression :
        IFusionCommaTargetExpression
    {
        /// <summary>
        /// Returns the <see cref="IType"/> which is represented
        /// under the typeof expression.
        /// </summary>
        IType ReferenceType { get; }
    }
}
