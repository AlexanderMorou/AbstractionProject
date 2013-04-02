using System;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */
namespace AllenCopeland.Abstraction.Slf.Abstract.Members
{
    /// <summary>
    /// Whether the instance member is static or hide by signature.
    /// </summary>
    [Flags]
    public enum InstanceMemberFlags
    {
        /// <summary>
        /// Member has no intance flags defined.
        /// </summary>
        None        = 0x0000,
        /// <summary>
        /// Member is a static member.
        /// </summary>
        /* 0000000000111000000001 */
        Static      = 0xe01,
        /// <summary>
        /// Member hides base's definition by signature only.
        /// </summary>
        /* 0011000000000000010000 */
        HideBySignature = 0xc0010,
        /// <summary>
        /// Member hides base's definition by name, removing all
        /// previous identities under the name.
        /// </summary>
        /* 0101000000000000100000 */
        HideByName = 0x140020,
        /// <summary>
        /// The instance member flags mask.
        /// </summary>
        FlagsMask = Static | HideBySignature | HideByName,
    }

    /// <summary>
    /// Defines properties and methods for working with an instance member.
    /// </summary>
    public interface IInstanceMember :
        IMember
    {
        /// <summary>
        /// Returns the <see cref="ExtendedInstanceMemberFlags"/> that determine how the
        /// <see cref="IExtendedInstanceMember"/> is shown in its scope and inherited scopes.
        /// </summary>
        InstanceMemberFlags InstanceFlags { get; }
        /// <summary>
        /// Returns whether the <see cref="IInstanceMember"/>
        /// hides the original definition completely.
        /// </summary>
        bool IsHideBySignature { get; }
        /// <summary>
        /// Returns whether the <see cref="IInstanceMember"/> is
        /// static.
        /// </summary>
        bool IsStatic { get; }
    }
}
