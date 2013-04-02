using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */



namespace AllenCopeland.Abstraction.Slf.Abstract.Members
{
    //[FlagsAttribute]
    //public enum NameHere
    //{
    //    /* 0000000000111000000001 */
    //    Static = 0xe01,
    //    /* 0000000111001000000010 */
    //    Virtual = 0x7202,
    //    /* 0000011001010000000100 */
    //    Abstract = 0x19404,
    //    /* 0000101010100000001000 */
    //    Override = 0x2a808,
    //    /* 0011000000000000010000 */
    //    HideBySig = 0xc0010,
    //    /* 0101000000000000100000 */
    //    HideByName = 0x140020,
    //    /* 1000000000000001000000 */
    //    Final = 0x200040,
    //    /* 1110110100000010000000 */
    //    Extension = 0x3b4080,
    //    /* 0000000000000100000000 */
    //    Async = 0x100,
    //}
    /// <summary>
    /// Flags relative to the management of 
    /// current member status and future
    /// member status, as well as indicators
    /// of whether the member requires an instance
    /// to be used.
    /// </summary>
    [Flags]
    public enum ExtendedInstanceMemberFlags :
        int
    {
        /// <summary>
        /// No flags are set relative to the instance
        /// member modifier flags.
        /// </summary>
        None      = 0x0000,
        /// <summary>
        /// Member is a static member.
        /// </summary>
        /* 0000000000111000000001 */
        Static = InstanceMemberFlags.Static,
        /* 00111001000010 */
        /// <summary>
        /// Member is a virtual (overridable) member.
        /// </summary>
        /* 0000000111001000000010 */
        /* *
         * virtual methods declare 'virtual' and 'newslot'.
         * */
        Virtual   = 0x7202,
        /// <summary>
        /// Member is an abstract member.
        /// </summary>
        /* 0000011001010000000100 */
        Abstract  = 0x19404,
        /// <summary>
        /// Member is an overridden member.
        /// </summary>
        /* 0000101010100000001000 */
        /* *
         * overridden members declare 'virtual' and no
         * 'newslot'.
         * */
        Override  = 0x2a808,
        /// <summary>
        /// Member hides base's definition
        /// by signature.
        /// </summary>
        /* 0011000000000000010000 */
        /* *
         * Hides the previous definition by signature.
         * Default value for instance/static members;
         * neither virtual nor newslot attributes are used
         * by default.
         * */
        HideBySignature = InstanceMemberFlags.HideBySignature,
        /* 0101000000000000100000 */
        /// <summary>
        /// Member hides base's definition by name.
        /// </summary>
        HideByName = InstanceMemberFlags.HideByName,
        /// <summary>
        /// Member is final (removes the ability for 
        /// inheritors to override).
        /// </summary>
        /* 1000000000000001000000 */
        /* *
         * As the name implies, final specifies the final 
         * attribute along with virtual, to indicate
         * that it's a sealed override.
         * */
        Final     = 0x200040,
        /// <summary>
        /// The mask which selects the members from the current
        /// enumeration.
        /// </summary>
        FlagsMask = InstanceMemberFlags.FlagsMask | Virtual | Abstract | Override | Final,
    }
    /// <summary>
    /// Defines properties and methods for working with an extended 
    /// member that on a type which can be instantiated.
    /// </summary>
    public interface IExtendedInstanceMember :
        IInstanceMember
    {
        /// <summary>
        /// Returns the <see cref="ExtendedInstanceMemberFlags"/> that determine how the
        /// <see cref="IExtendedInstanceMember"/> is shown in its scope and inherited scopes.
        /// </summary>
        new ExtendedInstanceMemberFlags InstanceFlags { get; }
        /// <summary>
        /// Returns whether the <see cref="IExtendedInstanceMember"/> is 
        /// abstract (must be implemented, or is not yet 
        /// implemented).
        /// </summary>
        bool IsAbstract { get; }
        /// <summary>
        /// Returns whether the <see cref="IExtendedInstanceMember"/> is
        /// virtual (can be overridden).
        /// </summary>
        bool IsVirtual { get; }
        /// <summary>
        /// Returns whether the <see cref="IExtendedInstanceMember"/>
        /// finalizes the member removing the overrideable 
        /// status.
        /// </summary>
        bool IsFinal { get; }
        /// <summary>
        /// Returns whether the <see cref="IExtendedInstanceMember"/> 
        /// is an override of a virtual member.
        /// </summary>
        bool IsOverride { get; }
    }
}
