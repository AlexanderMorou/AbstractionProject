using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Abstract.Members
{
    /// <summary>
    /// Defines properties and methods for working with a unique identifier
    /// representing a unary operator.
    /// </summary>
    public interface IUnaryOperatorUniqueIdentifier :
        IGeneralMemberUniqueIdentifier,
        IEquatable<IUnaryOperatorUniqueIdentifier>
    {
        /// <summary>
        /// Returns the coerced operator represented
        /// by the unique identifier.
        /// </summary>
        CoercibleUnaryOperators Operator { get; }
    }
}
