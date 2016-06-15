using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
using AllenCopeland.Abstraction.Slf.Ast.Statements;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Members
{
    /// <summary>
    /// Defines generic properties and methods for working with an intermediat
    /// method member which is a code target.
    /// </summary>
    /// <typeparam name="TMethod">The type of method in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateMethod">The type of method in the intermediate
    /// abstract syntax tree.</typeparam>
    /// <typeparam name="TMethodParent">The type which owns the <typeparamref name="TMethod"/>
    /// instances in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateMethodParent">The type which owns the <typeparamref name="TIntermediateMethod"/>
    /// in the intermediate abstract syntax tree.</typeparam>
#if DEBUG
    [VisitorTargetAttribute("IntermediateMemberVisitor", ContextualVisitor  = true)]
    [VisitorTargetAttribute("IntermediateMemberVisitor", YieldingVisitor    = true)]
    [VisitorTargetAttribute("IntermediateMemberVisitor", ContextualVisitor  = true,
                                                         YieldingVisitor    = true)]
    [VisitorTargetAttribute("IntermediateMemberVisitor")]
    [VisitorTargetAttribute("IntermediateTreeVisitor", DerivedThroughInheriting = "IntermediateMemberVisitor")]
#endif
    public interface IIntermediateMethodMember<TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent> :
        IIntermediateMethodSignatureMember<IMethodParameterMember<TMethod, TMethodParent>, IIntermediateMethodParameterMember<TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent>, TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent>,
        IIntermediateMethodMember,
        IMethodMember<TMethod, TMethodParent>
        where TMethod :
            IMethodMember<TMethod, TMethodParent>
        where TIntermediateMethod :
            IIntermediateMethodMember<TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent>,
            TMethod
        where TMethodParent :
            IMethodParent<TMethod, TMethodParent>
        where TIntermediateMethodParent :
            IIntermediateMethodParent<TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent>,
            TMethodParent
    {
        /// <summary>
        /// Returns the parent of the <see cref="IIntermediateMethodMember{TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent}"/>.
        /// </summary>
        /// <remarks>Resolves ambiguity between <see cref="IIntermediateMember{TIdentifier, TParent, TIntermediateParent}"/>
        /// and <see cref="IIntermediateMethodMember"/>.</remarks>
        new TIntermediateMethodParent Parent { get; }
    }
    /// <summary>
    /// Defines properties and methods for working with an intermediate method member
    /// which is a code target.
    /// </summary>
    public interface IIntermediateMethodMember :
        IIntermediateMethodSignatureMember,
        IIntermediateScopedDeclaration,
        ITopBlockStatement,
        IMethodMember
    {
        /// <summary>
        /// Returns the parent of the <see cref="IIntermediateMethodMember"/>.
        /// </summary>
        new IIntermediateMethodParent Parent { get; }
    }
}
