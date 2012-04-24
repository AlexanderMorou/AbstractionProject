using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using AllenCopeland.Abstraction.Slf.Platforms.WindowsNT;

namespace AllenCopeland.Abstraction.Slf.Cli.Metadata
{
    /* *
     * UnsafeValueType applied to CliHeader because it is 
     * marshalled through pointers to and from byte-arrays.
     * *
     * http://weblog.ikvm.net/CommentView.aspx?guid=6c3ae56d-7b22-40ae-96b6-ba9ce7a6b6d3
     * */
    /// <summary>
    /// Definition of the header used for the Common Language Infrastructure's
    /// metadata.
    /// </summary>
    [UnsafeValueType]
    public struct CliHeader
    {

        /// <summary>
        /// Data member for <see cref="Size"/>.
        /// </summary>
        private uint size;
        /// <summary>
        /// Data member for <see cref="RuntimeVersion"/>.
        /// </summary>
        private DWordVersion runtimeVersion;
        /// <summary>
        /// Data member for <see cref="Metadata"/>.
        /// </summary>
        private RVAndSize metadata;
        /// <summary>
        /// Data member for <see cref="Flags"/>.
        /// </summary>
        private CliRuntimeFlags flags;
        /// <summary>
        /// Data member for <see cref="EntrypointToken"/>.
        /// </summary>
        private uint entrypointToken;
        /// <summary>
        /// Data member for <see cref="Resources"/>.
        /// </summary>
        private RVAndSize resources;
        /// <summary>
        /// Data member for <see cref="StrongNameSignature"/>.
        /// </summary>
        private RVAndSize strongNameSignature;
        /// <summary>
        /// Data member for <see cref="CodeManagerTable"/>.
        /// </summary>
        private RVAndSize codeManagerTable;
        /// <summary>
        /// Data member for <see cref="VtableFixups"/>.
        /// </summary>
        private RVAndSize vtableFixups;
        /// <summary>
        /// Data member for <see cref="ExportAddressTableLookups"/>.
        /// </summary>
        private RVAndSize exportAddressTableLookups;
        /// <summary>
        /// Data member for <see cref="ManagedNativeHeader"/>.
        /// </summary>
        private RVAndSize managedNativeHeader;

        /// <summary>
        /// Creates a new <see cref="CliHeader"/> which denotes information
        /// about the managed code within the <see cref="PEImage"/>.
        /// </summary>
        /// <param name="majorRuntimeVersion">The <see cref="UInt16"/> major value of the version that determines
        /// the minimum version to run this program.</param>
        /// <param name="minorRuntimeVersion">The <see cref="UInt16"/> minor value of the version that determines
        /// the minimum version to run this program.</param>
        /// <param name="metadata">The <see cref="RVAndSize"/> of the metadata within the 
        /// <see cref="PEImage"/>.</param>
        /// <param name="flags">The <see cref="CliRuntimeFlags"/> describing the information about the
        /// image to the runtime.</param>
        /// <param name="entrypointToken">The <see cref="UInt32"/> value of the metadata token
        /// for the method that represents the entrypoint, or the file
        /// containing the module that contains the entrypoint.</param>
        /// <param name="resources">The <see cref="RVAndSize"/> of the implementation-specific resources
        /// contained within the <see cref="PEImage"/>.</param>
        /// <param name="strongNameSignature">The <see cref="RVAndSize"/> of the hash blobCacheData for the <see cref="PEImage"/>
        /// used by the CLI loader for binding and versioning.</param>
        /// <param name="codeManagerTable"></param>
        /// <param name="vtableFixups">The <see cref="RVAndSize"/> of an array representing the sizes
        /// and kinds of a series of Vrables.</param>
        /// <param name="exportAddressTableLookups">The <see cref="RVAndSize"/> of the export address table.</param>
        /// <param name="managedNativeHeader">The <see cref="RVAndSize"/> for the managed native header (usually zero).</param>
        /// <exception cref="System.ArgumentOutOfRangeException">thrown when <paramref name="flags"/> is out of
        /// range.</exception>
        public CliHeader(DWordVersion runtimeVersion, RVAndSize metadata, CliRuntimeFlags flags, uint entrypointToken, RVAndSize resources, RVAndSize strongNameSignature, RVAndSize codeManagerTable, RVAndSize vtableFixups, RVAndSize exportAddressTableLookups, RVAndSize managedNativeHeader)
        {
            const CliRuntimeFlags validFlags = unchecked((CliRuntimeFlags) ~(0xF | 1 << 16));
            if ((flags & validFlags) != (CliRuntimeFlags) 0)
                throw new ArgumentOutOfRangeException("flags");
            this.size = (uint)Marshal.SizeOf(typeof(CliHeader));
            this.runtimeVersion = runtimeVersion;
            this.metadata = metadata;
            this.flags = flags;
            this.entrypointToken = entrypointToken;
            this.resources = resources;
            this.strongNameSignature = strongNameSignature;
            this.codeManagerTable = codeManagerTable;
            this.vtableFixups = vtableFixups;
            this.exportAddressTableLookups = exportAddressTableLookups;
            this.managedNativeHeader = managedNativeHeader;
        }

        /// <summary>
        /// Returns the <see cref="UInt32"/> value which denotes the size
        /// of the <see cref="CliHeader"/>.
        /// </summary>
        public uint Size
        {
            get
            {
                return this.size;
            }
        }

        /// <summary>
        /// Returns the <see cref="DWordVersion"/> that determines the
        /// minimum version to run this program.
        /// </summary>
        public DWordVersion RuntimeVersion { get { return this.runtimeVersion; } }

        /// <summary>
        /// Returns the <see cref="RVAndSize"/> of the metadata within the 
        /// <see cref="PEImage"/>.
        /// </summary>
        public RVAndSize Metadata { get { return this.metadata; } }

        /// <summary>
        /// Returns the <see cref="CliRuntimeFlags"/> describing the
        /// information about the image to the runtime.
        /// </summary>
        public CliRuntimeFlags Flags { get { return this.flags; } }

        /// <summary>
        /// Returns the <see cref="UInt32"/> value of the metadata token
        /// for the method that represents the entrypoint, or the file
        /// containing the module that contains the entrypoint.
        /// </summary>
        public uint EntrypointToken { get { return this.entrypointToken; } }

        /// <summary>
        /// Returns the <see cref="RVAndSize"/> of the
        /// implementation-specific resources contained within the
        /// <see cref="PEImage"/>.
        /// </summary>
        public RVAndSize Resources { get { return this.resources; } }

        /// <summary>
        /// Returns the <see cref="RVAndSize"/> of the hash blobCacheData for
        /// the <see cref="PEImage"/> used by the CLI loader for binding and versioning.
        /// </summary>
        public RVAndSize StrongNameSignature { get { return this.strongNameSignature; } }

        /// <summary>
        /// Returns the <see cref="RVAndSize"/> of the code manager table 
        /// </summary>
        public RVAndSize CodeManagerTable { get { return this.codeManagerTable; } }

        /// <summary>
        /// Returns the <see cref="RVAndSize"/> of an array representing the sizes
        /// and kinds of a series of Vrables.
        /// </summary>
        public RVAndSize VtableFixups { get { return this.vtableFixups; } }

        /// <summary>
        /// Returns the <see cref="RVAndSize"/> of the export address table.
        /// </summary>
        public RVAndSize ExportAddressTableLookups { get { return this.exportAddressTableLookups; } }

        /// <summary>
        /// Returns the <see cref="RVAndSize"/> for the managed native header.
        /// </summary>
        /// <remarks>Usually zero.</remarks>
        public RVAndSize ManagedNativeHeader { get { return this.managedNativeHeader; } }
    }
}
