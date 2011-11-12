using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Abstract.Members
{
    public enum ExtendedMethodMemberFlags
    {
        /// <summary>
        /// No flags are set relative to the instance
        /// member modifier flags.
        /// </summary>
        None = ExtendedInstanceMemberFlags.None,
        /// <summary>
        /// Member is a static member.
        /// </summary>
        /* 0000000000111000000001 */
        Static = ExtendedInstanceMemberFlags.Static,
        /* 00111001000010 */
        /// <summary>
        /// Member is a virtual (overridable) member.
        /// </summary>
        /* 0000000111001000000010 */
        /* *
         * virtual methods declare 'virtual' and 'newslot'.
         * */
        Virtual = ExtendedInstanceMemberFlags.Virtual,
        /// <summary>
        /// Member is an abstract member.
        /// </summary>
        /* 0000011001010000000100 */
        Abstract = ExtendedInstanceMemberFlags.Abstract,
        /// <summary>
        /// Member is an overridden member.
        /// </summary>
        /* 0000101010100000001000 */
        /* *
         * overridden members declare 'virtual' and no
         * 'newslot'.
         * */
        Override = ExtendedInstanceMemberFlags.Override,
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
        HideBySignature = ExtendedInstanceMemberFlags.HideBySignature,
        /* 0101000000000000100000 */
        /// <summary>
        /// Member hides base's definition by name.
        /// </summary>
        HideByName = ExtendedInstanceMemberFlags.HideByName,
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
        Final = ExtendedInstanceMemberFlags.Final,
        /// <summary>
        /// Method is asynchronous in nature.
        /// </summary>
        /* 0000000000000100000000 */
        Async = 0x100,
        FlagsMask = ExtendedInstanceMemberFlags.FlagsMask | Async,
    }
}
