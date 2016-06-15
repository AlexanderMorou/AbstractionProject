using System;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
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
    public enum InstanceMemberAttributes
    {
        /// <summary>
        /// Member has no intance flags defined.
        /// </summary>
        None                = 0x0000,
        /// <summary>
        /// Member is a static member.
        /// </summary>
        /* 0 00000000 00000000 00111100 00000001 */
        Static              = 0x3c01,
        /// <summary>
        /// Member hides base's definition by signature only.
        /// </summary>
        /* 0 00011000 00000000 00000000 00010000 */
        HideBySignature     = 0x18000010,
        /// <summary>
        /// Member hides base's definition by name, removing all
        /// previous identities under the name.
        /// </summary>
        /* 0000 00100000 00000000 00000000 00100000 */
        HideByName          = 0x68000020,
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
        /// Returns the <see cref="InstanceMemberAttributes"/> that determine how the
        /// <see cref="IInstanceMember"/> is shown in its scope and inherited scopes.
        /// </summary>
        InstanceMemberAttributes Attributes { get; }
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
