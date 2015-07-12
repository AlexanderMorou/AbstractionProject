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
        /* 0000000000111000000001 */
        Static = ExtendedMemberAttributes.Static,
        /* 00111001000010 */
        /// <summary>
        /// Member is a virtual (overridable) member.
        /// </summary>
        /* 0000000111001000000010 */
        /* *
         * virtual methods declare 'virtual' and 'newslot'.
         * */
        Virtual = ExtendedMemberAttributes.Virtual,
        /// <summary>
        /// Member is an abstract member.
        /// </summary>
        /* 0000011001010000000100 */
        Abstract = ExtendedMemberAttributes.Abstract,
        /// <summary>
        /// Member is an overridden member.
        /// </summary>
        /* 0000101010100000001000 */
        /* *
         * overridden members declare 'virtual' and no
         * 'newslot'.
         * */
        Override = ExtendedMemberAttributes.Override,
        /// <summary>
        /// Member hides base's definition
        /// by signature.
        /// </summary>
        /* 0000 00100000 00000000 00000000 00010000 */
        /* *
         * Hides the previous definition by signature.
         * Default value for instance/static members;
         * neither virtual nor newslot attributes are used
         * by default.
         * */
        HideBySignature = ExtendedMemberAttributes.HideBySignature,
        /* 0101000000000000100000 */
        /// <summary>
        /// Member hides base's definition by name.
        /// </summary>
        HideByName = ExtendedMemberAttributes.HideByName,
        /// <summary>
        /// Member is final (removes the ability for 
        /// inheritors to override).
        /// </summary>
        /* 000001000000010000000100000001000000 */
        /* *
         * As the name implies, final specifies the final 
         * attribute along with virtual, to indicate
         * that it's a sealed override.
         * */
        Final = ExtendedMemberAttributes.Final,
        /// <summary>
        /// Method is asynchronous in nature.
        /// </summary>
        /* 011000000000000000000000000100000000 */
        Async = 0x600000100,
        FlagsMask = ExtendedMemberAttributes.FlagsMask | Async,
    }
}
