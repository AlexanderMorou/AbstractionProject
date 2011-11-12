using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Abstract.Members
{
    /// <summary>
    /// Defines properties and methods for working with a signature
    /// member's unique identifier.
    /// </summary>
    /// <typeparam name="TIdentifier">The kind of identifier
    /// represented by the <see cref="ISignatureMemberUniqueIdentifier"/>.
    /// </typeparam>
    public interface ISignatureMemberUniqueIdentifier<TIdentifier> :
        IMemberUniqueIdentifier<TIdentifier>,
        ISignatureMemberUniqueIdentifier
        where TIdentifier :
            ISignatureMemberUniqueIdentifier<TIdentifier>
    {
    }

    public interface ISignatureMemberUniqueIdentifier
    {
        /// <summary>
        /// Returns the types of the parameters represented
        /// by the <see cref="ISignatureMemberUniqueIdentifier"/>.
        /// </summary>
        IEnumerable<IType> Parameters { get; }
        /// <summary>
        /// Returns the number of parameters within
        /// the signature identifier.
        /// </summary>
        int ParameterCount { get; }
    }
}
