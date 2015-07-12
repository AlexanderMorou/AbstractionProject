using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Abstract.Members
{
    /// <summary>
    /// Defines generic properties and methods for 
    /// working with a series of <see cref="ITypeCoercionMember{TCoercionParent}"/>
    /// instances capable of implicitly or explicitly coercing
    /// <typeparamref name="TCoercionParent"/>.
    /// </summary>
    /// <typeparam name="TCoercionParent">The type of parent that contains the 
    /// type coercion members in the current implementation.</typeparam>
    public interface ITypeCoercionMemberDictionary<TCoercionParent> :
        IGroupedMemberDictionary<TCoercionParent, ITypeCoercionUniqueIdentifier, ITypeCoercionMember<TCoercionParent>>
        where TCoercionParent :
            ICoercibleType<ITypeCoercionUniqueIdentifier, ITypeCoercionMember<TCoercionParent>, TCoercionParent>
    {
        /// <summary>
        /// Returns whether the <typeparamref name="TCoercionParent"/> 
        /// contains an explicit type coercion to <paramref name="target"/>.
        /// </summary>
        /// <param name="target">The <see cref="IType"/> to 
        /// coerce explicitly to.</param>
        /// <returns>True if the <typeparamref name="TCoercionParent"/>
        /// contains an explicit <see cref="ITypeCoercionMember{TCoercionParent}"/>
        /// to the <paramref name="target"/> type; false otherwise.</returns>
        bool HasExplicitCoercionTo(IType target);
        /// <summary>
        /// Returns whether the <typeparamref name="TCoercionParent"/> 
        /// contains an implicit type coercion to <paramref name="target"/>.
        /// </summary>
        /// <param name="target">The <see cref="IType"/> to 
        /// coerce implicitly to.</param>
        /// <returns>True if the <typeparamref name="TCoercionParent"/>
        /// contains an implicit <see cref="ITypeCoercionMember{TCoercionParent}"/>
        /// to the <paramref name="target"/> type; false otherwise.</returns>
        bool HasImplicitCoercionTo(IType target);
        /// <summary>
        /// Returns whether the <typeparamref name="TCoercionParent"/> 
        /// contains an explicit type coercion from <paramref name="target"/>.
        /// </summary>
        /// <param name="target">The <see cref="IType"/> to 
        /// coerce explicitly from.</param>
        /// <returns>True if the <typeparamref name="TCoercionParent"/>
        /// contains an explicit <see cref="ITypeCoercionMember{TCoercionParent}"/>
        /// from the <paramref name="target"/> type; false otherwise.</returns>
        bool HasExplicitCoercionFrom(IType target);
        /// <summary>
        /// Returns whether the <typeparamref name="TCoercionParent"/> 
        /// contains an implicit type coercion from <paramref name="target"/>.
        /// </summary>
        /// <param name="target">The <see cref="IType"/> to 
        /// coerce implicitly from.</param>
        /// <returns>True if the <typeparamref name="TCoercionParent"/>
        /// contains an implicit <see cref="ITypeCoercionMember{TCoercionParent}"/>
        /// from the <paramref name="target"/> type; false otherwise.</returns>
        bool HasImplicitCoercionFrom(IType target);
        /// <summary>
        /// Returns the <see cref="ITypeCoercionMember{TCoercionParent}"/>
        /// which meets the <paramref name="requirement"/> in
        /// the <paramref name="direction"/> for
        /// the <paramref name="target"/> specified.
        /// </summary>
        /// <param name="requirement">The <see cref="TypeConversionRequirement"/>
        /// necessary for the coercion, either 
        /// explicit or implicit.</param>
        /// <param name="direction">The coercion direction;
        /// either from or to the type.</param>
        /// <param name="target">
        /// The <see cref="IType"/> which is coerced.</param>
        /// <returns>The <see cref="ITypeCoercionMember{TCoercionParent}"/> 
        /// which met the <paramref name="requirement"/> in
        /// the <paramref name="direction"/> for
        /// the <paramref name="target"/> specified</returns>
        /// <exception cref="System.ArgumentException">
        /// thrown when there is no explicit/implicit coercion
        /// from/to <paramref name="target"/>.
        /// </exception>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// thrown when either <paramref name="requirement"/> or 
        /// <paramref name="direction"/> is out of range.</exception>
        ITypeCoercionMember<TCoercionParent> this[TypeConversionRequirement requirement, TypeConversionDirection direction, IType target] { get; }
    }
}
