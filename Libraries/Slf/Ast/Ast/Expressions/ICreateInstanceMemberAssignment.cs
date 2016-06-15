using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
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

namespace AllenCopeland.Abstraction.Slf.Ast.Expressions
{
    /// <summary>
    /// Defines properties and methods for working with a
    /// <see cref="ICreateInstanceExpression"/>'s member
    /// assignment expression.
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
    public interface ICreateInstanceUnboundMemberAssignment :
        ICreateInstanceMemberAssignment
    {
        /// <summary>
        /// Returns/sets the <see cref="String"/>
        /// that designates the name of the property to assign to
        /// <see cref="ICreateInstanceMemberAssignment.AssignValue"/>.
        /// </summary>
        string Name { get; set; }
    }
    /// <summary>
    /// Defines properties and methods for working with a
    /// <see cref="ICreateInstanceExpression"/>'s member
    /// assignment expression.
    /// </summary>
    public interface ICreateInstanceMemberAssignment
    {
        /// <summary>
        /// Returns/sets the value to assign to the target of the <see cref="ICreateInstanceMemberAssignment"/>.
        /// </summary>
        IExpression AssignValue { get; set; }
        /// <summary>
        /// Visits the <paramref name="visitor"/>.
        /// </summary>
        /// <param name="visitor">The <see cref="IExpressionVisitor"/> to visit.</param>
        void Accept(IExpressionVisitor visitor);
        TResult Accept<TResult, TContext>(ICommonExpressionVisitor<TResult, TContext> visitor, TContext context);
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
    public interface ICreateInstancePropertyAssignment<TProperty, TPropertyParent> :
        IPropertySignatureReferenceExpression<TProperty, TPropertyParent>,
        ICreateInstanceMemberAssignment
        where TProperty :
            IPropertySignatureMember<TProperty, TPropertyParent>
        where TPropertyParent :
            IPropertySignatureParent<TProperty, TPropertyParent>
    {

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
    public interface ICreateInstanceFieldAssignment<TField, TFieldParent> :
        IFieldReferenceExpression<TField, TFieldParent>,
        ICreateInstanceMemberAssignment
        where TField :
            IFieldMember<TField, TFieldParent>
        where TFieldParent :
            IFieldParent<TField, TFieldParent>
    {

    }
}
