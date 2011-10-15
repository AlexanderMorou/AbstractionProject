using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Abstract.Members
{
    /// <summary>
    /// Defines properties and methods for working with a 
    /// unique identifier that represents a binary operator coercion member.
    /// </summary>
    public interface IBinaryOperatorUniqueIdentifier :
        IMemberUniqueIdentifier<IBinaryOperatorUniqueIdentifier>,
        IGeneralMemberUniqueIdentifier
    {
        /// <summary>
        /// Returns the <see cref="CoercibleBinaryOperators"/> coerced
        /// by the <see cref="IBinaryOperatorUniqueIdentifier"/>.
        /// </summary>
        CoercibleBinaryOperators Operator { get; }

        /// <summary>
        /// Returns which side the required self reference
        /// the <see cref="IBinaryOperatorUniqueIdentifier"/>'s
        /// parent is on.
        /// </summary>
        BinaryOpCoercionContainingSide ContainingSide { get; }

        /// <summary>
        /// Returns the type of the other side of the expression
        /// used when performing the coercion.
        /// </summary>
        /// <remarks>If <see cref="ContainingSide"/>
        /// is <see cref="BinaryOpCoercionContainingSide.Both"/>
        /// <see cref="OtherSide"/> returns null.</remarks>
        IType OtherSide { get; }
    }
}
