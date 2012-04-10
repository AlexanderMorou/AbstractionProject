using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Cli.Metadata
{
    public struct VtableFixup
    {
        private uint relativeVirtualAddress;
        private ushort size;
        private VtableFixupKind kind;

        public uint RelativeVirtualAddress
        {
            get
            {
                return this.relativeVirtualAddress;
            }
        }

        /// <summary>
        /// Returns the <see cref="UInt16"/> value which denotes
        /// the number of entries within the vtable targeted by the 
        /// <see cref="VtableFixup"/>.
        /// </summary>
        public ushort Size
        {
            get
            {
                return this.size;
            }
        }

        /// <summary>
        /// Returns the <see cref="VtableFixupKind"/> that denotes
        /// the kind of methods described by the target vtable.
        /// </summary>
        public VtableFixupKind Kind { get { return this.kind; } }
    }
}
