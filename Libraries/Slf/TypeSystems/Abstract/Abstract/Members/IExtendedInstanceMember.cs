using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */



namespace AllenCopeland.Abstraction.Slf.Abstract.Members
{
    /* *
     *  [FlagsAttribute]
     *  public enum NameHere
     *  {
	 *      /* 0 00000000 00000000 00111100 00000001 / Static = 0x3c01,
	 *      /* 0 00000000 00001111 11000100 00000010 / Virtual = 0xfc402,
	 *      /* 0 00000001 11110000 01001000 00000100 / Abstract = 0x1f04804,
	 *      /* 0 00000110 00010000 10010000 00001000 / Override = 0x6109008,
	 *      /* 0 00011000 00000000 00000000 00010000 / HideBySignature = 0x18000010,
	 *      /* 0 01101000 00000000 00000000 00100000 / HideByName = 0x68000020,
	 *      /* 1 10000000 00100001 00100000 01000000 / Final = 0x80212040,
	 *      /* 0 10100010 01000010 00000000 10000000 / Extension = 0xa2420080,
	 *      /* 0 00000000 10000100 00000001 00000000 / Async = 0x840100,
	 *      /* 1 01010101 00001000 00000010 00000000 / Partial = 0x55080200,
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
        /* 0000 00000000 00000000 11111000 00000001 */
        Static = InstanceMemberAttributes.Static,
        /* 00111001000010 */
        /// <summary>
        /// Member is a virtual (overridable) member.
        /// </summary>
        /* 0000 00000000 00011111 00001000 00000010 */
        /* *
         * virtual methods declare 'virtual' and 'newslot'.
         * */
        Virtual   = 0xfc402,
        /// <summary>
        /// Member is an abstract member.
        /// </summary>
        /* 0000 00000011 11100001 00010000 00000100 */
        Abstract  = 0x1f04804,
        /// <summary>
        /// Member is an overridden member.
        /// </summary>
        /* 0000 00011100 00100010 00100000 00001000 */
        /* *
         * overridden members declare 'virtual' and no
         * 'newslot'.
         * */
        Override  = 0x6109008,
        /// <summary>
        /// Member hides base's definition
        /// by signature.
        /// </summary>
        /* 0 00011000 00000000 00000000 00010000 */
        /* *
         * Hides the previous definition by signature.
         * Default value for instance/static members;
         * neither virtual nor newslot attributes are used
         * by default.
         * */
        HideBySignature = InstanceMemberAttributes.HideBySignature,
        /* 010100 00000000 00100000 */
        /// <summary>
        /// Member hides base's definition by name.
        /// </summary>
        HideByName = InstanceMemberAttributes.HideByName,
        /// <summary>
        /// Member is final (removes the ability for 
        /// inheritors to override).
        /// </summary>
        /* 1 10000000 00100001 00100000 01000000 */
        /* *
         * As the name implies, final specifies the final 
         * attribute along with virtual, to indicate
         * that it's a sealed override.
         * */
        Final     = 0x80212040,
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

        /// <summary>
        /// Returns the <see cref="IEnumerable{T}"/> of <see cref="IInterfaceType"/>
        /// elements which the current <see cref="IExtendedInstanceMember"/>
        /// implements.
        /// </summary>
        IEnumerable<IInterfaceType> Implementations { get; }
    }
}
