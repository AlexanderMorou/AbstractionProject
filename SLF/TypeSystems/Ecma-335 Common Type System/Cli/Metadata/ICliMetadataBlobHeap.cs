using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs;

namespace AllenCopeland.Abstraction.Slf.Cli.Metadata
{
    public interface ICliMetadataBlobHeap :
        ICliMetadataStreamHeader
    {
        /// <summary>
        /// Obtains a <see cref="ICliMetadataSignature"/> of type
        /// <typeparamref name="T"/> with the <paramref name="signatureKind"/>
        /// and <paramref name="heapIndex"/> provided.
        /// </summary>
        /// <typeparam name="T">The specific kind of <see cref="ICliMetadataSignature"/>
        /// to retrieve.</typeparam>
        /// <param name="signatureKind">The <see cref="SignatureKinds"/>
        /// prolog which identifies the proper parse path to follow.</param>
        /// <param name="heapIndex">The <see cref="UInt32"/> value which determines
        /// where within the heap the item to retrieve is.</param>
        /// <returns>A <typeparamref name="T"/> instance
        /// relative to the signature desired.</returns>
        T GetSignature<T>(SignatureKinds signatureKind, uint heapIndex)
            where T :
                ICliMetadataSignature;
        /// <summary>
        /// Obtains a <see cref="Byte"/> array blob from the
        /// <paramref name="heapIndex"/> provided.
        /// </summary>
        /// <param name="heapIndex">The <see cref="UInt32"/> value denoting the 
        /// index within the heap.</param>
        /// <returns>A <see cref="Byte"/> array relative to the blob
        /// within the heap.</returns>
        byte[] GetBlob(uint heapIndex);
    }
}
