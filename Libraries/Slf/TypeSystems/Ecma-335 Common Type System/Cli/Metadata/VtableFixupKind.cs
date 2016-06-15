using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Cli.Metadata
{

    public enum VtableFixupKind :
        ushort
    {
        /// <summary>
        /// The size of the slots in the target Vtable
        /// are 32-bits.
        /// </summary>
        SlotSize32,
        /// <summary>
        /// The size of the slots in the target Vtable
        /// are 64-bits.
        /// </summary>
        SlotSize64,
        /// <summary>
        /// The call represents a transition from unmanaged
        /// to managed code.
        /// </summary>
        FromUnmanaged,
        /// <summary>
        /// The Vtable entries target virtual methods, which when
        /// the resulted unmanaged method that gets generated is called
        /// the most derived version of the method at the token described
        /// is invoked.
        /// </summary>
        CallVirtual,
    }
}
