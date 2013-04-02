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
    /// <see cref="ITypeCoercionMember{TCoercionParent}"/> 
    /// for working with a type-coercion member.
    /// </summary>
    /// <typeparam name="TCoercionParent">The type of parent that contains the 
    /// type coercion member in the current implementation.</typeparam>
    internal abstract class TypeCoercionMemberBase<TCoercionParent> :
        MemberBase<ITypeCoercionUniqueIdentifier, TCoercionParent>,
        ITypeCoercionMember<TCoercionParent>
        where TCoercionParent :
            ICoercibleType<ITypeCoercionUniqueIdentifier, ITypeCoercionMember<TCoercionParent>, TCoercionParent>
    {

        /// <summary>
        /// Creates a new <see cref="TypeCoercionMemberBase{TCoercionParent}"/> 
        /// with the <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="parent">
        /// The parent of the <see cref="TypeCoercionMemberBase{TCoercionParent}"/>.
        /// </param>
        public TypeCoercionMemberBase(TCoercionParent parent)
            : base(parent)
        {

        }

        #region ITypeCoercionMember Members

        /// <summary>
        /// Returns whether the conversion coercion 
        /// is implicit or explicit.
        /// </summary>
        public abstract TypeConversionRequirement Requirement { get; }

        /// <summary>
        /// Returns whether the conversion coercion 
        /// is from the containing type or to the 
        /// containing type.
        /// </summary>
        public abstract TypeConversionDirection Direction { get; }

        /// <summary>
        /// Returns the type which is coerced by the member.
        /// </summary>
        public abstract IType CoercionType { get; }

        #endregion

        #region ICoercionMember Members

        ICoercibleType ICoercionMember.Parent
        {
            get { return this.Parent; }
        }

        #endregion

        #region IScopedDeclaration Members

        public AccessLevelModifiers AccessLevel
        {
            get { return this.OnGetAccessLevel(); }
        }

        #endregion

        /// <summary>
        /// Obtains the access level of the current 
        /// <see cref="TypeCoercionMemberBase{TCoercionParent}"/>.
        /// </summary>
        /// <returns>A <see cref="AccessLevelModifiers"/> value representing
        /// the accessibility of the current 
        /// <see cref="TypeCoercionMemberBase{TCoercionParent}"/>.</returns>
        protected abstract AccessLevelModifiers OnGetAccessLevel();
    }
}
