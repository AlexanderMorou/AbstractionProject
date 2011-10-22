using System;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Members
{
    /// <summary>
    /// Provides a base implementation of 
    /// <see cref="ITypeCoercionMemberDictionary{TCoercionParentIdentifier, TCoercionParent}"/>
    /// for working with a series of 
    /// <see cref="ITypeCoercionMember{TCoercionParentIdentifier, TCoercionParent}"/>
    /// instances.
    /// </summary>
    /// <typeparam name="TCoercionParent">
    /// The type of <see cref="ICoercibleType{TType}"/>
    /// which contains the 
    /// <see cref="TypeCoercionMembers{TCoercionParent}"/>.
    /// </typeparam>
    internal class TypeCoercionMembers<TCoercionParentIdentifier, TCoercionParent> :
        GroupedMembersBase<TCoercionParent, ITypeCoercionUniqueIdentifier, ITypeCoercionMember<TCoercionParentIdentifier, TCoercionParent>>,
        ITypeCoercionMemberDictionary<TCoercionParentIdentifier, TCoercionParent>,
        ITypeCoercionMemberDictionary
        where TCoercionParentIdentifier :
            ITypeUniqueIdentifier<TCoercionParentIdentifier>
        where TCoercionParent :
            ICoercibleType<TCoercionParentIdentifier, TCoercionParent>
    {
        /// <summary>
        /// Creates a new <see cref="TypeCoercionMembers{TCoercionParent}"/>
        /// with the <paramref name="master"/> and <paramref name="parent"/>
        /// provided.
        /// </summary>
        /// <param name="master">The <see cref="FullMembersBase"/>
        /// which contains all of the 
        /// <see cref="TypeCoercionMembers{TCoercionParent}"/> 
        /// members and other <see cref="ICoercibleType{TType}"/> members 
        /// amongst others.</param>
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
        /// contains an explicit <see cref="ITypeCoercionMember{TTypeIdentifier, TCoercionParent}"/>
        /// to the <paramref name="target"/> type; false otherwise.</returns>
        public bool HasExplicitCoercionTo(IType target)
        {
            foreach (ITypeCoercionMember<TCoercionParentIdentifier, TCoercionParent> member in this.Values)
                if (member.CoercionType.Equals(target) && member.Direction == TypeConversionDirection.ToContainingType && member.Requirement == TypeConversionRequirement.Explicit)
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
        /// contains an implicit <see cref="ITypeCoercionMember{TTypeIdentifier, TCoercionParent}"/>
        /// to the <paramref name="target"/> type; false otherwise.</returns>
        public bool HasImplicitCoercionTo(IType target)
        {
            foreach (ITypeCoercionMember<TCoercionParentIdentifier, TCoercionParent> member in this.Values)
                if (member.CoercionType.Equals(target) && member.Direction == TypeConversionDirection.ToContainingType && member.Requirement == TypeConversionRequirement.Implicit)
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
        /// contains an explicit <see cref="ITypeCoercionMember{TTypeIdentifier, TCoercionParent}"/>
        /// from the <paramref name="target"/> type; false otherwise.</returns>
        public bool HasExplicitCoercionFrom(IType target)
        {
            foreach (ITypeCoercionMember<TCoercionParentIdentifier, TCoercionParent> member in this.Values)
                if (member.CoercionType.Equals(target) && member.Direction == TypeConversionDirection.FromContainingType && member.Requirement == TypeConversionRequirement.Explicit)
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
        /// contains an implicit <see cref="ITypeCoercionMember{TTypeIdentifier, TCoercionParent}"/>
        /// from the <paramref name="target"/> type; false otherwise.</returns>
        public bool HasImplicitCoercionFrom(IType target)
        {
            foreach (ITypeCoercionMember<TCoercionParentIdentifier, TCoercionParent> member in this.Values)
                if (member.CoercionType.Equals(target) && member.Direction == TypeConversionDirection.FromContainingType && member.Requirement == TypeConversionRequirement.Implicit)
                    return true;
            return false;
        }

        /// <summary>
        /// Returns the <see cref="ITypeCoercionMember{TTypeIdentifier, TCoercionParent}"/>
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
        /// <returns>The <see cref="ITypeCoercionMember{TTypeIdentifier, TCoercionParent}"/> 
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
        public ITypeCoercionMember<TCoercionParentIdentifier, TCoercionParent> this[TypeConversionRequirement requirement, TypeConversionDirection direction, IType target]
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
                                    throw new ArgumentException("No explicit coercion to target type.", "target");
                                break;
                            case TypeConversionDirection.FromContainingType:
                                if (!this.HasExplicitCoercionFrom(target))
                                    throw new ArgumentException("No explicit coercion from target type.", "target");
                                break;
                            default:
                                throw new ArgumentOutOfRangeException("direction");
                        }
                        break;
                    case TypeConversionRequirement.Implicit:
                        switch (direction)
                        {
                            case TypeConversionDirection.ToContainingType:
                                if (!this.HasImplicitCoercionTo(target))
                                    throw new ArgumentException("No explicit coercion to target type.", "target");
                                break;
                            case TypeConversionDirection.FromContainingType:
                                if (!this.HasImplicitCoercionFrom(target))
                                    throw new ArgumentException("No explicit coercion from target type.", "target");
                                break;
                            default:
                                throw new ArgumentOutOfRangeException("direction");
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("requirement");
                }
                foreach (ITypeCoercionMember<TCoercionParentIdentifier, TCoercionParent> member in this.Values)
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
