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
    /// Provides a base abstract implementation for 
    /// <see cref="IConstructorMember{TCtor, TCtorParent}"/>
    /// to work with a constructor member.
    /// </summary>
    /// <typeparam name="TCtor">The type of <see cref="IConstructorMember{TCtor, TCtorParent}"/>
    /// in the current implementation.</typeparam>
    /// <typeparam name="TCtorParent">The type of <see cref="ICreatableParent{TCtor, TCtorParent}"/>
    /// that contains the <typeparamref name="TCtor"/> instances.</typeparam>
    internal abstract class ConstructorMemberBase<TCtor, TCtorParent> :
        SignatureMemberBase<IGeneralSignatureMemberUniqueIdentifier, TCtor, IConstructorParameterMember<TCtor, TCtorParent>, TCtorParent>,
        IConstructorMember<TCtor, TCtorParent>
        where TCtor :
            IConstructorMember<TCtor, TCtorParent>
        where TCtorParent :
            ICreatableParent<TCtor, TCtorParent>
    {
        public ConstructorMemberBase(TCtorParent parent)
            : base(parent)
        {
        }

        protected override string OnGetName()
        {
            return ".ctor";
        }

        #region IScopedDeclaration Members

        #region IScopedDeclaration Members

        /// <summary>
        /// Returns the access level of the <see cref="ConstructorMemberBase{TCtor, TCtorParent}"/>.
        /// </summary>
        public AccessLevelModifiers AccessLevel
        {
            get { return this.AccessLevelImpl; }
        }

        #endregion

        protected abstract AccessLevelModifiers AccessLevelImpl { get; }

        #endregion

    }
}
