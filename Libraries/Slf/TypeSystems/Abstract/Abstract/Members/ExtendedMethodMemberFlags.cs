using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Abstract.Members
{
    [Flags]
    public enum ExtendedMethodAttributes :
        long
    {
        /// <summary>
        /// No flags are set relative to the instance
        /// member modifier flags.
        /// </summary>
        None = ExtendedMemberAttributes.None,
        /// <summary>
        /// Member is a static member.
        /// </summary>
        /* 0 00000000 00000000 00111100 00000001 */
        Static = ExtendedMemberAttributes.Static,
        /// <summary>
        /// Member is a virtual (overridable) member.
        /// </summary>
        /* 0 00000000 00001111 11000100 00000010 */
        /* *
         * virtual methods declare 'virtual' and 'newslot'.
         * */
        Virtual = ExtendedMemberAttributes.Virtual,
        /// <summary>
        /// Member is an abstract member.
        /// </summary>
        /* 0 00000001 11110000 01001000 00000100 */
        Abstract = ExtendedMemberAttributes.Abstract,
        /// <summary>
        /// Member is an overridden member.
        /// </summary>
        /* 0 00000110 00010000 10010000 00001000 */
        /* *
         * overridden members declare 'virtual' and no
         * 'newslot'.
         * */
        Override = ExtendedMemberAttributes.Override,
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
        HideBySignature = ExtendedMemberAttributes.HideBySignature,
        /* 0 01101000 00000000 00000000 00100000 */
        /// <summary>
        /// Member hides base's definition by name.
        /// </summary>
        HideByName = ExtendedMemberAttributes.HideByName,
        /// <summary>
        /// Member is final (removes the ability for 
        /// inheritors to override).
        /// </summary>
        /* 0000 01000000 01000000 01000000 01000000 */
        /* *
         * As the name implies, final specifies the final 
         * attribute along with virtual, to indicate
         * that it's a sealed override.
         * */
        Final = ExtendedMemberAttributes.Final,
        /// <summary>
        /// Method is asynchronous in nature.
        /// </summary>
        /* 0 00000000 10000100 00000001 00000000 */
        Async = 0x840100,
        /// <summary>
        /// Method is a partial method.
        /// </summary>
        /* 1 01010101 00001000 00000010 00000000 */
        Partial = 0x55080200,
        /// <summary>
        /// The method is a partial method definition.
        /// </summary>
        /* 11 00000000 00000000 00000000 00000000 */
        PartialDefinition = 0x300000000,
        /// <summary>
        /// The mask which selects the members from the current
        /// enumeration.
        /// </summary>
        FlagsMask = ExtendedMemberAttributes.FlagsMask | Async | Partial | PartialDefinition,
    }
}
