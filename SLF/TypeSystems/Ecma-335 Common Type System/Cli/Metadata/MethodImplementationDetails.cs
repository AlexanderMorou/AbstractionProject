using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Cli.Metadata
{
    public enum MethodCodeType
    {
        /// <summary>
        /// Method implementation is handled through Common Intermediate
        /// Language stack instructions.
        /// </summary>
        CommonIntermediateLanguage,
        /// <summary>
        /// Method implementation is handled through native code.
        /// </summary>
        NativeCode,
        /// <summary>
        /// Method implementation is handled through optimized IL.  Reserved
        /// conforming implementations will not utilize this code type.
        /// </summary>
        OptimizedIL,
        /// <summary>
        /// Method implementation is provided by the runtime.
        /// </summary>
        Runtime,
        Mask = Runtime,
    }

    public enum MethodImplementationFlags
    {
        /// <summary>
        /// Indicates the method is defined; primarily used
        /// in merge scenarios.
        /// </summary>
        ForwardReference    = 0x0010,
        /// <summary>
        /// Reserved; conforming implementations can ignore.
        /// </summary>
        PreserveSignature   = 0x0080,
        /// <summary>
        /// Reserved; zero conforming implementations.
        /// </summary>
        InternalCall        = 0x1000,
        /// <summary>
        /// Method is single-threaded through the body.
        /// </summary>
        Synchronized        = 0x0020,
        /// <summary>
        /// Method cannot be inlined.
        /// </summary>
        NoInlining          = 0x0008,
        /// <summary>
        /// Method will not be optimized when generating native code.
        /// </summary>
        NoOptimization      = 0x0040,
        Mask = ForwardReference | PreserveSignature | InternalCall | Synchronized | NoInlining | NoOptimization,
    }
    public struct MethodImplementationDetails
    {
        private ushort value;

        public MethodImplementationDetails(ushort value) { this.value = value; }

        public MethodCodeType CodeType
        {
            get
            {
                return ((MethodCodeType)this.value) & MethodCodeType.Mask;
            }
        }

        public bool IsManaged
        {
            get
            {
                return (this.value & 4) != 4;
            }
        }

        public MethodImplementationFlags ImplementationFlags { get { return ((MethodImplementationFlags)this.value & MethodImplementationFlags.Mask); } }

        public static implicit operator MethodImplementationDetails(ushort value)
        {
            return new MethodImplementationDetails(value);
        }
    }
}
