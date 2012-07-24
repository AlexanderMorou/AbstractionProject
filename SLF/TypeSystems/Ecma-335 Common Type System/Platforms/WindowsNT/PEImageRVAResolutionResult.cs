using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Platforms.WindowsNT
{
    public struct PEImageRVAResolutionResult 
    {
        private uint offset;
        private CoffSection section;

        /// <summary>
        /// The default result of a resolution should there be no section
        /// relative to the virtual address provided.
        /// </summary>
        public static readonly PEImageRVAResolutionResult ResolutionFailure;
        
        /// <summary>
        /// Creates a new <see cref="PEImageRVAResolutionResult"/> with the
        /// <paramref name="offset"/> from the start of the section and the
        /// <paramref name="section"/> in which the relative virtual address
        /// resides.
        /// </summary>
        /// <param name="offset">The <see cref="UInt32"/> value which denotes the
        /// offset, relative to the start of 
        /// <see cref="CoffSection.SectionData"/>,
        /// that the relative virtual address represented.</param>
        /// <param name="section">The <see cref="CoffSection"/> which denotes
        /// which section within the <see cref="PEImage"/> the
        /// relative virtual address was in.</param>
        internal PEImageRVAResolutionResult(uint offset, CoffSection section)
        {
            this.offset = offset;
            this.section = section;
        }

        /// <summary>
        /// Returns a <see cref="Boolean"/> value denoting whether the
        /// resolution was successful.
        /// </summary>
        public bool Resolved { get { return this.offset != uint.MaxValue && this.section != null; } }

        /// <summary>
        /// Returns the <see cref="UInt32"/> value which denotes the
        /// offset, relative to the start of 
        /// <see cref="CoffSection.SectionData"/>,
        /// that the relative virtual address represented.
        /// </summary>
        public uint Offset
        {
            get
            {
                return this.offset;
            }
        }

        /// <summary>
        /// Returns the <see cref="CoffSection"/> which denotes
        /// which section within the <see cref="PEImage"/> the
        /// relative virtual address was in.
        /// </summary>
        public CoffSection Section
        {
            get
            { return this.section; }
        }
    }
}
