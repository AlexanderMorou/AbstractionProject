using System;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf._Internal.Abstract.Members
{
    /// <summary>
    /// Provides a base implementation of 
    /// <see cref="ITypeCoercionMemberDictionary{TCoercionParent}"/>
    /// for working with a series of 
    /// <see cref="ITypeCoercionMember{TCoercionParent}"/>
    /// instances.
    /// </summary>
    /// <typeparam name="TCoercionParent">
    /// The type of <see cref="ICoercibleType{TType}"/>
    /// which contains the 
    /// <see cref="TypeCoercionMembers{TCoercionParent}"/>.
    /// </typeparam>
    /// <typeparam name="TCoercionParentIdentifier">The kind of unique
    /// identifier used to differentiate the container of the coercible
    /// type members from its siblings.</typeparam>
    internal class TypeCoercionMembers<TCoercionParent> :
        GroupedMembersBase<TCoercionParent, ITypeCoercionUniqueIdentifier, ITypeCoercionMember<TCoercionParent>>,
        ITypeCoercionMemberDictionary<TCoercionParent>,
        ITypeCoercionMemberDictionary
        where TCoercionParent :
            ICoercibleType<ITypeCoercionUniqueIdentifier, ITypeCoercionMember<TCoercionParent>, TCoercionParent>
    {
        /// <summary>
        /// Creates a new <see cref="TypeCoercionMembers{TCoercionParent}"/>
        /// with the <paramref name="master"/> and <paramref name="parent"/>
        /// provided.
        /// </summary>
        /// <param name="master">The <see cref="FullMembersBase"/>
        /// which contains all of the 
        /// <see cref="TypeCoercionMembers{TCoercionParent}"/> 
        /// members and other members of the <typeparamref name="TCoercionParent"/>.</param>
        /// <param name="parent">The <typeparamref name="TCoercionParent"/> which contains
        /// the <see cref="TypeCoercionMembers{TCoercionParent}"/>.</param>
        public TypeCoercionMembers(FullMembersBase master, TCoercionParent parent)
            : base(master, parent)
        {
        }

        #region ITypeCoercionMemberDictionary<TCoercionParent> Members

        /// <summary>
        /// Returns whether the <typeparamref name="TCoercionParent"/> 
        /// contains an explicit type coercion to <paramref name="target"/>.
        /// </summary>
        /// <param name="target">The <see cref="IType"/> to 
        /// coerce explicitly to.</param>
        /// <returns>True if the <typeparamref name="TCoercionParent"/>
        /// contains an explicit <see cref="ITypeCoercionMember{TCoercionParent}"/>
        /// to the <paramref name="target"/> type; false otherwise.</returns>
        public bool HasExplicitCoercionTo(IType target)
        {
            foreach (var identifier in this.Keys)
                if (identifier.CoercionType.Equals(target) && identifier.Direction == TypeConversionDirection.ToContainingType && identifier.Requirement == TypeConversionRequirement.Explicit)
                    return true;
            return false;
        }

        /// <summary>
        /// Returns whether the <typeparamref name="TCoercionParent"/> 
        /// contains an implicit type coercion to <paramref name="target"/>.
        /// </summary>
        /// <param name="target">The <see cref="IType"/> to 
        /// coerce implicitly to.</param>
        /// <returns>True if the <typeparamref name="TCoercionParent"/>
        /// contains an implicit <see cref="ITypeCoercionMember{TCoercionParent}"/>
        /// to the <paramref name="target"/> type; false otherwise.</returns>
        public bool HasImplicitCoercionTo(IType target)
        {
            foreach (var identifier in this.Keys)
                if (identifier.CoercionType.Equals(target) && identifier.Direction == TypeConversionDirection.ToContainingType && identifier.Requirement == TypeConversionRequirement.Implicit)
                    return true;
            return false;
        }

        /// <summary>
        /// Returns whether the <typeparamref name="TCoercionParent"/> 
        /// contains an explicit type coercion from <paramref name="target"/>.
        /// </summary>
        /// <param name="target">The <see cref="IType"/> to 
        /// coerce explicitly from.</param>
        /// <returns>True if the <typeparamref name="TCoercionParent"/>
        /// contains an explicit <see cref="ITypeCoercionMember{TCoercionParent}"/>
        /// from the <paramref name="target"/> type; false otherwise.</returns>
        public bool HasExplicitCoercionFrom(IType target)
        {
            foreach (var identifier in this.Keys)
                if (identifier.CoercionType.Equals(target) && identifier.Direction == TypeConversionDirection.FromContainingType && identifier.Requirement == TypeConversionRequirement.Explicit)
                    return true;
            return false;
        }

        /// <summary>
        /// Returns whether the <typeparamref name="TCoercionParent"/> 
        /// contains an implicit type coercion from <paramref name="target"/>.
        /// </summary>
        /// <param name="target">The <see cref="IType"/> to 
        /// coerce implicitly from.</param>
        /// <returns>True if the <typeparamref name="TCoercionParent"/>
        /// contains an implicit <see cref="ITypeCoercionMember{TCoercionParent}"/>
        /// from the <paramref name="target"/> type; false otherwise.</returns>
        public bool HasImplicitCoercionFrom(IType target)
        {
            foreach (var identifier in this.Keys)
                if (identifier.CoercionType.Equals(target) && identifier.Direction == TypeConversionDirection.FromContainingType && identifier.Requirement == TypeConversionRequirement.Implicit)
                    return true;
            return false;
        }

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
        public ITypeCoercionMember<TCoercionParent> this[TypeConversionRequirement requirement, TypeConversionDirection direction, IType target]
        {
            get
            {
                switch (requirement)
                {
                    case TypeConversionRequirement.Explicit:
                        switch (direction)
                        {
                            case TypeConversionDirection.ToContainingType:
                                if (!this.HasExplicitCoercionTo(target))
                                    throw ThrowHelper.ObtainArgumentException(ArgumentWithException.target, ExceptionMessageId.CoercionDoesNotExist, ThrowHelper.GetArgumentExceptionWord(ExceptionWordId.@explicit), ThrowHelper.GetArgumentExceptionWord(ExceptionWordId.to));
                                break;
                            case TypeConversionDirection.FromContainingType:
                                if (!this.HasExplicitCoercionFrom(target))
                                    throw ThrowHelper.ObtainArgumentException(ArgumentWithException.target, ExceptionMessageId.CoercionDoesNotExist, ThrowHelper.GetArgumentExceptionWord(ExceptionWordId.@explicit), ThrowHelper.GetArgumentExceptionWord(ExceptionWordId.from));
                                break;
                            default:
                                throw ThrowHelper.ObtainArgumentOutOfRangeException(ArgumentWithException.direction);
                        }
                        break;
                    case TypeConversionRequirement.Implicit:
                        switch (direction)
                        {
                            case TypeConversionDirection.ToContainingType:
                                if (!this.HasImplicitCoercionTo(target))
                                    throw ThrowHelper.ObtainArgumentException(ArgumentWithException.target, ExceptionMessageId.CoercionDoesNotExist, ThrowHelper.GetArgumentExceptionWord(ExceptionWordId.@implicit), ThrowHelper.GetArgumentExceptionWord(ExceptionWordId.to));
                                break;
                            case TypeConversionDirection.FromContainingType:
                                if (!this.HasImplicitCoercionFrom(target))
                                    throw ThrowHelper.ObtainArgumentException(ArgumentWithException.target, ExceptionMessageId.CoercionDoesNotExist, ThrowHelper.GetArgumentExceptionWord(ExceptionWordId.@implicit), ThrowHelper.GetArgumentExceptionWord(ExceptionWordId.from));
                                break;
                            default:
                                throw ThrowHelper.ObtainArgumentOutOfRangeException(ArgumentWithException.direction);
                        }
                        break;
                    default:
                        throw ThrowHelper.ObtainArgumentOutOfRangeException(ArgumentWithException.requirement);
                }
                foreach (ITypeCoercionMember<TCoercionParent> member in this.Values)
                    if (member.CoercionType.Equals(target) && member.Direction == direction && member.Requirement == requirement)
                        return member;
                return null;
            }
        }

        #endregion

        #region ITypeCoercionMemberDictionary Members

        ITypeCoercionMember ITypeCoercionMemberDictionary.this[TypeConversionRequirement requirement, TypeConversionDirection direction, IType target]
        {
            get { return this[requirement, direction, target]; }
        }

        #endregion
    }
}
