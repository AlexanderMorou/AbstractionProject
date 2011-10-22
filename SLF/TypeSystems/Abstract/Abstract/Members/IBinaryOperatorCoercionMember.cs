using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Abstract.Members
{
    /// <summary>
    /// The type of operator that is overloaded by the <see cref="IBinaryOperatorCoercionMember"/>.
    /// </summary>
    public enum CoercibleBinaryOperators :
        byte
    {
        /// <summary>
        /// Addition binary operator, often '+'.
        /// </summary>
        /// <remarks>CLI method: op_Addition</remarks>
        Add,
        /// <summary>
        /// Subtraction binary operator, often '-'.
        /// </summary>
        /// <remarks>CLI method: op_Subtraction</remarks>
        Subtract,
        /// <summary>
        /// Multiplication binary operator, often '*'.
        /// </summary>
        /// <remarks>CLI method: op_Multiply</remarks>
        Multiply,
        /// <summary>
        /// Division binary operator, often '/'.
        /// </summary>
        /// <remarks>CLI method: op_Division</remarks>
        Divide,
        /// <summary>
        /// Modulus binary operator, often '%' or 'Mod'.
        /// </summary>
        /// <remarks>CLI method: op_Modulus</remarks>
        Modulus,
        /// <summary>
        /// Binary AND operator, often '&amp;' or 'And'.
        /// </summary>
        /// <remarks>CLI method: op_BitwiseAnd</remarks>
        BitwiseAnd,
        /// <summary>
        /// Binary OR operator, often '|' or 'Or'.
        /// </summary>
        /// <remarks>CLI method: op_BitwiseOr</remarks>
        BitwiseOr,
        /// <summary>
        /// Exclusive Or binary operator, often '^' or 'XOr'.
        /// </summary>
        /// <remarks>CLI method: op_ExclusiveOr.</remarks>
        ExclusiveOr,
        /// <summary>
        /// Left-shift binary operator, often '&lt;&lt;'.
        /// </summary>
        /// <remarks>CLI method: op_LeftShift</remarks>
        LeftShift,
        /// <summary>
        /// Right-shift binary operator, often '&gt;&gt;'.
        /// </summary>
        /// <remarks>CLI method: op_RightShift</remarks>
        RightShift,
        /// <summary>
        /// Equal to binary operator, often '==' or '='.
        /// </summary>
        /// <remarks>CLI method: op_Equality</remarks>
        IsEqualTo,
        /// <summary>
        /// Not equal to binary operator, often '!=' or '&lt;&gt;'.
        /// </summary>
        /// <remarks>CLI method: op_Inequality</remarks>
        IsNotEqualTo,
        /// <summary>
        /// Less than binary operator, often '&lt;'.
        /// </summary>
        /// <remarks>CLI method: op_LessThan</remarks>
        LessThan,
        /// <summary>
        /// Greater than binary operator, often '&gt;'.
        /// </summary>
        /// <remarks>CLI method: op_GreaterThan</remarks>
        GreaterThan,
        /// <summary>
        /// Less than or equal to binary operator, often '&lt;='.
        /// </summary>
        /// <remarks>CLI method: op_LessThanOrEqual</remarks>
        LessThanOrEqualTo,
        /// <summary>
        /// Greater than or equal to binary operator, often '&gt;='.
        /// </summary>
        /// <remarks>CLI method: op_GreaterThanOrEqual</remarks>
        GreaterThanOrEqualTo
    }
    /// <summary>
    /// Indicates which side the containing type of the <see cref="IBinaryOperatorCoercionMember"/>
    /// resides on in the overload.
    /// </summary>
    public enum BinaryOpCoercionContainingSide :
        byte
    {
        /// <summary>
        /// The containing type matches the left side.
        /// </summary>
        LeftSide,
        /// <summary>
        /// The containing type matches the right side.
        /// </summary>
        RightSide,
        /// <summary>
        /// The containing type matches both sides.
        /// </summary>
        Both
    }

    /// <summary>
    /// Defines generic properties and methods for a member which coerces
    /// binary operations relative to the target in the expression.
    /// </summary>
    /// <typeparam name="TCoercionParentIdentifier">The type of the identifier that represents
    /// the parent's uniqueness from the other types.</typeparam>
    /// <typeparam name="TCoercionParent">
    /// The type of parent that contains the binary operation 
    /// coercion member in the current implementation.</typeparam>
    public interface IBinaryOperatorCoercionMember<TCoercionParentIdentifier, TCoercionParent> :
        ICoercionMember<IBinaryOperatorUniqueIdentifier, TCoercionParentIdentifier, IBinaryOperatorCoercionMember<TCoercionParentIdentifier, TCoercionParent>, TCoercionParent>,
        IBinaryOperatorCoercionMember
        where TCoercionParentIdentifier :
            ITypeUniqueIdentifier<TCoercionParentIdentifier>
        where TCoercionParent :
            ICoercibleType<IBinaryOperatorUniqueIdentifier, TCoercionParentIdentifier, IBinaryOperatorCoercionMember<TCoercionParentIdentifier, TCoercionParent>, TCoercionParent>
    {
        /// <summary>
        /// Returns the <see cref="IBinaryOperatorUniqueIdentifier"/> which
        /// represents the <see cref="IBinaryOperatorCoercionMember{TCoercionParentIdentifier, TCoercionParent}"/>.
        /// </summary>
        new IBinaryOperatorUniqueIdentifier UniqueIdentifier { get; }
    }

    /// <summary>
    /// Defines properties and methods for a member which coerces
    /// binary operations relative to the target in the expression.
    /// </summary>
    public interface IBinaryOperatorCoercionMember :
        ICoercionMember
    {
        /// <summary>
        /// Returns the <see cref="CoercibleBinaryOperators"/> coerced
        /// by the <see cref="IBinaryOperatorCoercionMember"/>.
        /// </summary>
        CoercibleBinaryOperators Operator { get; }

        /// <summary>
        /// Returns which side the required self reference
        /// the <see cref="IBinaryOperatorCoercionMember"/>'s
        /// parent is on.
        /// </summary>
        BinaryOpCoercionContainingSide ContainingSide { get; }

        /// <summary>
        /// Returns the type of the other side of the expression
        /// used when performing the coercion.
        /// </summary>
        IType OtherSide { get; }

        /// <summary>
        /// Returns the <see cref="IType"/> yielded after coercing the 
        /// <see cref="Operator"/>.
        /// </summary>
        IType ReturnType { get; }

        /// <summary>
        /// Returns the <see cref="IBinaryOperatorUniqueIdentifier"/> which
        /// represents the <see cref="IBinaryOperatorCoercionMember"/>.
        /// </summary>
        new IBinaryOperatorUniqueIdentifier UniqueIdentifier { get; }
    }
}
