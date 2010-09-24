using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */



namespace AllenCopeland.Abstraction.Slf.Abstract.Members
{
    //[FlagsAttribute]
    //public enum NameHere
    //{
    //    /* 0000001110000001 */
    //    Static = 0x381,
    //    /* 0001110010000010 */
    //    Virtual = 0x1c82,
    //    /* 0110010100000100 */
    //    Abstract = 0x6504,
    //    /* 0010101000001000 */
    //    Override = 0x2a08,
    //    /* 1000000000010000 */
    //    HideBySignature = 0x8010,
    //    /* 1000000000100000 */
    //    HideByName = 0x8020,
    //    /* 0101000001000000 */
    //    Final = 0x5040,
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
        /* 0000001110000001 */
        Static = InstanceMemberFlags.Static,
        /* 00111001000010 */
        /// <summary>
        /// Member is a virtual (overridable) member.
        /// </summary>
        /* 0001110010000010 */
        /* *
         * virtual methods declare 'virtual' and 'newslot'.
         * */
        Virtual   = 0x1c82,
        /// <summary>
        /// Member is an abstract member.
        /// </summary>
        /* 0110010100000100 */
        Abstract  = 0x6504,
        /// <summary>
        /// Member is an overridden member.
        /// </summary>
        /* 0010101000001000 */
        /* *
         * overridden members declare 'virtual' and no
         * 'newslot'.
         * */
        Override  = 0x2a08,
        /// <summary>
        /// Member hides base's definition.
        /// </summary>
        /* 1000000000010000 */
        /* *
         * Hides the previous definition by signature.
         * Default value for instance/static members;
         * neither virtual nor newslot attributes are used
         * by default.
         * */
        HideBySignature = InstanceMemberFlags.HideBySignature,
        /* 1000000000100000 */
        HideByName = InstanceMemberFlags.HideByName,
        /// <summary>
        /// Member is final (removes the ability for 
        /// inheritors to override).
        /// </summary>
        /* 0101000001000000 */
        /* *
         * As the name implies, final specifies the final 
         * attribute along with virtual, to indicate
         * that it's a sealed override.
         * */
        Final     = 0x5040,
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
