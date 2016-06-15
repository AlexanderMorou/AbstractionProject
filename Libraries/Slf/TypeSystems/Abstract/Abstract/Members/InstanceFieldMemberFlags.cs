using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
	 *      /* 101010001001000010000000001000000000 / ReadOnly = 0xA89080200,
	 *      /* 110100010010000100001000010000000000 / Constant = 0xD12108400,
     *  }
     * */
    public enum FieldMemberAttributes :
        long
    {
        None = 0x0,
        /// <summary>
        /// Denotes the field as read-only.
        /// </summary>
        /* 1010 10001001 00001000 00000010 00000000 */
        ReadOnly = 0xA89080200L,
        /// <summary>
        /// Denotes the field as a constant value.
        /// </summary>
        /* 1101 00010010 00010000 10000100 00000000 */
        Constant = 0xD12108400L,
        /// <summary>
        /// The mask used for the field member flags.
        /// </summary>
        FlagsMask = ReadOnly | Constant
    }
    public enum InstanceFieldMemberAttributes :
        long
    {
        None = 0x0,
        /// <summary>
        /// The field is a static, type-level field.
        /// </summary>
        /* 0000 00000000 00000000 11111000 00000001 */
        Static = InstanceMemberAttributes.Static,
        /// <summary>
        /// Member hides base's definition by name, removing all
        /// previous identities under the name.
        /// </summary>
        /* 0000 00100000 00000000 00000000 00100000 */
        HideByName = InstanceMemberAttributes.HideByName,
        /* 1010 10001001 00001000 00000010 00000000 */
        ReadOnly = FieldMemberAttributes.ReadOnly,
        /* 1101 00010010 00010000 10000100 00000000 */
        Constant = FieldMemberAttributes.Constant,
        FlagsMask = Static | HideByName | FieldMemberAttributes.FlagsMask
    }
}
