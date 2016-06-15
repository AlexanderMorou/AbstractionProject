using AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs;
using AllenCopeland.Abstraction.Utilities.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables
{
    public interface ICliMetadataMethodHeader
    {
        /// <summary>
        /// Returns the <see cref="ICliMetadataLocalVarSignature"/> instance which
        /// denotes the local variables defined by the method.
        /// </summary>
        ICliMetadataLocalVarSignature LocalVariables { get; }
        /// <summary>
        /// Returns the <see cref="MethodHeaderFlags"/> which denote
        /// the size of the header, whether to init locals, and 
        /// whether there are more sections after the primary header.
        /// </summary>
        MethodHeaderFlags Flags { get; }
        /// <summary>
        /// Returns the <see cref="ICliMetadataMethodExceptionTable">exception table</see> of the method.
        /// </summary>
        ICliMetadataMethodExceptionTable ExceptionTable { get; }
        /// <summary>
        /// Returns the <see cref="UInt32"/> value which denotes the
        /// count, in bytes, of the code within the method.
        /// </summary>
        uint CodeSize { get; }
        /// <summary>
        /// Returns the <see cref="UInt16"/> value which denotes the
        /// maximum number of elements upon the stack at any point.
        /// </summary>
        ushort MaxStack { get; }
        /// <summary>
        /// Returns the size of the header in bytes.
        /// </summary>
        byte HeaderSize { get; }
    }
}
