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
    /// Provides a base implementation of <see cref="IMember{TParent}"/> which provides information about
    /// a member of a specific <typeparamref name="TParent"/>.
    /// </summary>
    /// <typeparam name="TParent">The type of <see cref="IMemberParent"/>, in the current implementation, which contains the <see cref="IMember{TParent}"/>.</typeparam>
    internal abstract partial class MemberBase<TIdentifier, TParent> :
        DeclarationBase<TIdentifier>,
        IMember<TIdentifier, TParent>
        where TIdentifier :
            IMemberUniqueIdentifier
        where TParent :
            IMemberParent
    {
        /// <summary>
        /// Data member for <see cref="Parent"/>.
        /// </summary>
        private TParent parent;

        /// <summary>
        /// Creates a new <see cref="MemberBase{TParent}"/> with the <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="parent">The parent of the <see cref="MemberBase{TParent}"/>.</param>
        protected MemberBase(TParent parent)
        {
            this.parent = parent;
        }

        #region IMember<TParent> Members

        /// <summary>
        /// Returns the parent of the <see cref="MemberBase{TParent}"/>.
        /// </summary>
        public TParent Parent
        {
            get { return this.parent; }
        }

        #endregion

        #region IMember Members

        IMemberParent IMember.Parent
        {
            get { return this.Parent; }
        }

        IMemberUniqueIdentifier IMember.UniqueIdentifier { get { return this.UniqueIdentifier; } }

        #endregion

        public override void Dispose()
        {
            try
            {
                this.parent = default(TParent);
            }
            finally
            {
                this.OnDisposed();
            }
        }
    }
}
