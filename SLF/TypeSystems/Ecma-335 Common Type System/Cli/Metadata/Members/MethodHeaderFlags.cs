using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Cli.Metadata.Members
{
    /// <summary>
    /// The flags that describe the header of a 
    /// Common Language Infrastructure method.
    /// </summary>
    public enum MethodHeaderFlags :
        byte
    {
        /// <summary>
        /// <para>The method header is narrow and utilizes only
        /// six (6) bits; as a result this will be the only flag
        /// used for the header.</para>
        /// <para>This flag is equivalent to the
        /// CorILMethod_TinyFormat, name changed for convenience.</para>
        /// </summary>
        NarrowFormat = 0x2, //CorILMethod_TinyFormat
        /// <summary>
        /// <para>The method header is wide and utilizes 12 bytes, or
        /// three 4-byte integers; of which the flags portion takes
        /// 12 bits.</para>
        /// <para>This flag is equivalent to the 
        /// CorILMethod_FatFormat, name changed for convenience.</para>
        /// </summary>
        WideFormat = 0x3, //CorILMethod_FatFormat
        /// <summary>
        /// <para>The method header contains more sections following this header.</para>
        /// <para>This flag is equivalent to the 
        /// CorILMethod_MoreSects, name changed for convenience.</para>
        /// </summary>
        ContainsMoreSections = 0x8, //CorILMethod_MoreSects 
        /// <summary>
        /// <para>The default constructor on all locals is to be used to 
        /// initialize them before use.</para>
        /// <para>This flag is equivalent to the 
        /// CorILMethod_InitLocals, name changed for convenience.</para>
        /// </summary>
        InitializeLocalsToDefault = 0x10,//CorILMethod_InitLocals
    }
}
