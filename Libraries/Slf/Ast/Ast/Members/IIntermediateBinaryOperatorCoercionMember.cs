using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
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
    /// Defines generic properties and methods for an intermediate member which coerces
    /// binary operations relative to the target in the expression.
    /// </summary>
    /// <typeparam name="TCoercionParent">The type which contains the binary operation 
    /// coercion member in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateCoercionParent">The type which contains the intermediate binary operation
    /// coercion member in the intermediate abstract syntax tree.</typeparam>
#if DEBUG
    [VisitorTargetAttribute("IntermediateMemberVisitor", ContextualVisitor  = true)]
    [VisitorTargetAttribute("IntermediateMemberVisitor", YieldingVisitor    = true)]
    [VisitorTargetAttribute("IntermediateMemberVisitor", ContextualVisitor  = true,
                                                         YieldingVisitor    = true)]
    [VisitorTargetAttribute("IntermediateMemberVisitor")]
    [VisitorTargetAttribute("IntermediateTreeVisitor", DerivedThroughInheriting = "IntermediateMemberVisitor")]
#endif
    public interface IIntermediateBinaryOperatorCoercionMember<TCoercionParent, TIntermediateCoercionParent> :
        IIntermediateCoercionMember<IBinaryOperatorUniqueIdentifier, IBinaryOperatorCoercionMember<TCoercionParent>, IIntermediateBinaryOperatorCoercionMember<TCoercionParent, TIntermediateCoercionParent>, TCoercionParent, TIntermediateCoercionParent>,
        IIntermediateBinaryOperatorCoercionMember,
        IBinaryOperatorCoercionMember<TCoercionParent>
        where TCoercionParent :
            ICoercibleType<IBinaryOperatorUniqueIdentifier, IBinaryOperatorCoercionMember<TCoercionParent>, TCoercionParent>
        where TIntermediateCoercionParent :
            IIntermediateCoercibleType<IBinaryOperatorUniqueIdentifier, IBinaryOperatorCoercionMember<TCoercionParent>, IIntermediateBinaryOperatorCoercionMember<TCoercionParent, TIntermediateCoercionParent>, TCoercionParent, TIntermediateCoercionParent>,
            TCoercionParent
    {
        /// <summary>
        /// Returns the parent of the <see cref="IIntermediateBinaryOperatorCoercionMember{TCoercionParent, TIntermediateCoercionParent}"/>.
        /// </summary>
#if DEBUG
        [VisitorImplementationIgnoreProperty]
#endif
        new TIntermediateCoercionParent Parent { get; }
    }

    /// <summary>
    /// Defines properties and methods for an intermediate member which coerces
    /// binary operations relative to the target in the expression.
    /// </summary>
    public interface IIntermediateBinaryOperatorCoercionMember :
        IIntermediateCoercionMember,
        ITopBlockStatement,
        IBinaryOperatorCoercionMember
    {
        /// <summary>
        /// Returns/sets the <see cref="CoercibleBinaryOperators"/> coerced
        /// by the <see cref="IIntermediateBinaryOperatorCoercionMember"/>.
        /// </summary>
        new CoercibleBinaryOperators Operator { get; set; }

        /// <summary>
        /// Returns/sets which side the required self reference
        /// the <see cref="IIntermediateBinaryOperatorCoercionMember"/>'s
        /// parent is on.
        /// </summary>
        new BinaryOpCoercionContainingSide ContainingSide { get; set; }

        /// <summary>
        /// Returns/sets the type of the other side of the expression
        /// used when performing the coercion.
        /// </summary>
        new IType OtherSide { get; set; }

        /// <summary>
        /// Returns/sets the <see cref="IType"/> yielded after coercing the 
        /// <see cref="Operator"/>.
        /// </summary>
        new IType ReturnType { get; set; }

        /// <summary>
        /// Returns the <see cref="ILocalMember"/> which denotes the parameter
        /// within the model that is on the left-hand-side of the operation.
        /// </summary>
        ILocalMember LeftSide { get; }

        /// <summary>
        /// Returns the <see cref="ILocalMember"/> which denotes the parameter
        /// within the model that is on the right-hand-side of the operation.
        /// </summary>
        ILocalMember RightSide { get; }
    }
}
