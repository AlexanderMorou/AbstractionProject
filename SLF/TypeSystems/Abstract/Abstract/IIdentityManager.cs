using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Abstract
{
    public interface IIdentityManager<TTypeIdentity> :
        IIdentityManager
    {
        /// <summary>
        /// Returns the <see cref="IType"/> from the <paramref name="typeIdentity"/>
        /// from the underlying model in which it exists.
        /// </summary>
        /// <param name="typeIdentity">The <typeparamref name="TTypeIdentity"/> that represents the
        /// type's identity in the base model on which the <see cref="IType"/>
        /// is defined.</param>
        /// <returns>A <see cref="IType"/> instance relative to the
        /// <paramref name="typeIdentity"/>.</returns>
        new IType ObtainTypeReference(TTypeIdentity typeIdentity);
    }

    public interface IIdentityManager
    {
        /// <summary>
        /// Returns the <see cref="IType"/> from the <paramref name="typeIdentity"/>
        /// from the underlying model in which it exists.
        /// </summary>
        /// <param name="typeIdentity">The <see cref="Object"/> that represents the
        /// type's identity in the base model on which the <see cref="IType"/>
        /// is defined.</param>
        /// <returns>A <see cref="IType"/> instance relative to the
        /// <paramref name="typeIdentity"/>.</returns>
        IType ObtainTypeReference(object typeIdentity);


    }
}
