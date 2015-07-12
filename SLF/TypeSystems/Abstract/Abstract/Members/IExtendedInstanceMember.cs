using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */



namespace AllenCopeland.Abstraction.Slf.Abstract.Members
{
    /* *
     * [FlagsAttribute]
     *  public enum NameHere
     *  {
	 *      /* 000000000000000000001111100000000001 / Static = 0xf801,
	 *      /* 000000000000000111110000100000000010 / Virtual = 0x1f0802,
	 *      /* 000000000011111000010001000000000100 / Abstract = 0x3e11004,
	 *      /* 000000011100001000100010000000001000 / Override = 0x1c222008,
	 *      /* 000000100000000000000000000000010000 / HideBySig = 0x20000010,
	 *      /* 000000100000000000000000000000100000 / HideByName = 0x20000020,
	 *      /* 000001000000010000000100000001000000 / Final = 0x40404040,
	 *      /* 000111000100100001000000000010000000 / Extension = 0x1C4840080,
	 *      /* 011000000000000000000000000100000000 / Async = 0x600000100,
     *  }
     * */
    /// <summary>
    /// Flags relative to the management of 
    /// current member status and future
    /// member status, as well as indicators
    /// of whether the member requires an instance
    /// to be used.
    /// </summary>
    [Flags]
    public enum ExtendedMemberAttributes :
        long
    {
        /// <summary>
        /// No flags are set relative to the instance
        /// member modifier flags.
        /// </summary>
        None      = 0x0000,
        /// <summary>
        /// Member is a static member.
        /// </summary>
        /* 000000000000000000001111100000000001 */
        Static = InstanceMemberAttributes.Static,
        /* 00111001000010 */
        /// <summary>
        /// Member is a virtual (overridable) member.
        /// </summary>
        /* 000000000000000111110000100000000010 */
        /* *
         * virtual methods declare 'virtual' and 'newslot'.
         * */
        Virtual   = 0x1f0802,
        /// <summary>
        /// Member is an abstract member.
        /// </summary>
        /* 000000000011111000010001000000000100 */
        Abstract  = 0x3e11004,
        /// <summary>
        /// Member is an overridden member.
        /// </summary>
        /* 000000011100001000100010000000001000 */
        /* *
         * overridden members declare 'virtual' and no
         * 'newslot'.
         * */
        Override  = 0x1c222008,
        /// <summary>
        /// Member hides base's definition
        /// by signature.
        /// </summary>
        /* 00100000 00000000 00000000 00010000 */
        /* *
         * Hides the previous definition by signature.
         * Default value for instance/static members;
         * neither virtual nor newslot attributes are used
         * by default.
         * */
        HideBySignature = InstanceMemberAttributes.HideBySignature,
        /* 0101000000000000100000 */
        /// <summary>
        /// Member hides base's definition by name.
        /// </summary>
        HideByName = InstanceMemberAttributes.HideByName,
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
        Final     = 0x40404040,
        /// <summary>
        /// The mask which selects the members from the current
        /// enumeration.
        /// </summary>
        FlagsMask = InstanceMemberAttributes.FlagsMask | Virtual | Abstract | Override | Final,
    }
    /// <summary>
    /// Defines properties and methods for working with an extended 
    /// member that on a type which can be instantiated.
    /// </summary>
    public interface IExtendedInstanceMember :
        IInstanceMember
    {
        /// <summary>
        /// Returns the <see cref="ExtendedMemberAttributes"/> that determine how the
        /// <see cref="IExtendedInstanceMember"/> is shown in its scope and inherited scopes.
        /// </summary>
        new ExtendedMemberAttributes Attributes { get; }
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
