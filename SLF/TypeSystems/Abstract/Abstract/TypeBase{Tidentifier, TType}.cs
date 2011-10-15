using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// Provides a generic base class for implementations of a type
    /// which specify the specific interface presented by all child members.
    /// </summary>
    /// <typeparam name="TType">The type of <see cref="IType{TTypeIdentifier, TType}"/> presented
    /// to child members of the <see cref="TypeBase{TIdentifier, TType}"/> in the
    /// current implementation.</typeparam>
    public abstract class TypeBase<TIdentifier, TType> :
        TypeBase<TIdentifier>,
        IType<TIdentifier, TType>
        where TIdentifier :
            ITypeUniqueIdentifier<TIdentifier>
        where TType :
            class,
            IType<TIdentifier, TType>
    {
        /// <summary>
        /// Indicates whether the current 
        /// <see cref="TypeBase{TIdentifier, TType}"/> is equal 
        /// to a given <typeparamref name="TType"/>
        /// of the same type.
        /// </summary>
        /// <param name="other">A <typeparamref name="TType"/> 
        /// to compare with this 
        /// <see cref="TypeBase{TIdentifier, TType}"/>.</param>
        /// <returns>
        /// true if the current <typeparamref name="TType"/> 
        /// is equal to the <paramref name="other"/> parameter; 
        /// otherwise, false.</returns>
        protected abstract bool Equals(TType other);

        #region IType<TType> Members

        /// <summary>
        /// Returns the element type of special classification types.
        /// </summary>
        public new TType ElementType
        {
            get
            {
                return (TType)base.ElementType;
            }
        }

        #endregion

    }
}
