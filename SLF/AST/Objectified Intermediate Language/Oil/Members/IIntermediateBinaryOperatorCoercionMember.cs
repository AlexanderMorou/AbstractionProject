using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Oil.Statements;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Members
{
    /// <summary>
    /// Defines generic properties and methods for an intermediate member which coerces
    /// binary operations relative to the target in the expression.
    /// </summary>
    /// <typeparam name="TCoercionParent">The type which contains the binary operation 
    /// coercion member in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateCoercionParent">The type which contains the intermediate binary operation
    /// coercion member in the intermediate abstract syntax tree.</typeparam>
    public interface IIntermediateBinaryOperatorCoercionMember<TCoercionParentIdentifier, TCoercionParent, TIntermediateCoercionParent> :
        IIntermediateCoercionMember<IBinaryOperatorUniqueIdentifier, TCoercionParentIdentifier, IBinaryOperatorCoercionMember<TCoercionParentIdentifier, TCoercionParent>, IIntermediateBinaryOperatorCoercionMember<TCoercionParentIdentifier, TCoercionParent, TIntermediateCoercionParent>, TCoercionParent, TIntermediateCoercionParent>,
        IIntermediateBinaryOperatorCoercionMember,
        IBinaryOperatorCoercionMember<TCoercionParentIdentifier, TCoercionParent>
        where TCoercionParentIdentifier :
            ITypeUniqueIdentifier<TCoercionParentIdentifier>
        where TCoercionParent :
            ICoercibleType<IBinaryOperatorUniqueIdentifier, TCoercionParentIdentifier, IBinaryOperatorCoercionMember<TCoercionParentIdentifier, TCoercionParent>, TCoercionParent>
        where TIntermediateCoercionParent :
            IIntermediateCoercibleType<IBinaryOperatorUniqueIdentifier, TCoercionParentIdentifier, IBinaryOperatorCoercionMember<TCoercionParentIdentifier, TCoercionParent>, IIntermediateBinaryOperatorCoercionMember<TCoercionParentIdentifier, TCoercionParentIdentifier, TIntermediateCoercionParent>, TCoercionParent, TIntermediateCoercionParent>,
            TCoercionParent
    {
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
    }
}
