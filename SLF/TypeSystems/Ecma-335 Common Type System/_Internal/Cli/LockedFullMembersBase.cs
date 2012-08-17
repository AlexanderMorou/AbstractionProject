using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    /// <summary>
    /// Provides a locked form of the full 
    /// members dictionary for a type or 
    /// member parent.
    /// </summary>
    internal class LockedFullMembersBase :
        LockedFullDeclarations<IGeneralMemberUniqueIdentifier, IMember>,
        IFullMemberDictionary
    {
        /// <summary>
        /// An empty locked full member dictionary.
        /// </summary>
        internal static IFullMemberDictionary Empty = new LockedFullMembersBase();
        /// <summary>
        /// Creates a new <see cref="LockedFullMembersBase"/>
        /// initialized to a default state.
        /// </summary>
        public LockedFullMembersBase()
            : base()
        {
        }
    }
}
