using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Ast;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Ast.Expressions
{
    /// <summary>
    /// Defines properties and methods for working 
    /// with an expression that casts another
    /// expression to a specified type.
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
    public interface ITypeCastExpression :
        IUnaryOperationPrimaryTerm
    {
        /// <summary>
        /// Returns/sets the <see cref="IType"/> the <see cref="ITypeCastExpression"/>
        /// casts the <see cref="Target"/> to.
        /// </summary>
        IType CastType { get; set; }
        /// <summary>
        /// Returns the <see cref="ITypeCollection"/> which denotes the
        /// required modifiers of the cast.
        /// </summary>
#if DEBUG
        [VisitorPropertyRequirement("RequiredModifiers != null")]
#endif
        ITypeCollection RequiredModifiers { get; }
        /// <summary>
        /// Returns the <see cref="ITypeCollection"/> which denotes the
        /// optional modifiers of the cast.
        /// </summary>
#if DEBUG
        [VisitorPropertyRequirement("OptionalModifiers != null")]
#endif
        ITypeCollection OptionalModifiers { get; }
        /// <summary>
        /// Returns/sets the <see cref="IExpression"/> the 
        /// <see cref="ITypeCastExpression"/> casts to
        /// <see cref="CastType"/>.
        /// </summary>
        IExpression Target { get; set; }
    }
}
