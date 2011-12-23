using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;

namespace AllenCopeland.Abstraction.Slf.Ast.Expressions
{
    public enum CreateByteArraySerializationResult
    {
        Successful = 0,
        InconsistentDimensions,
        UnexpectedNull,
        InconsistentPrimitiveType,
    }

    public struct CreateArrayByteStream
    {
        /// <summary>
        /// Returns the <see cref="Byte"/> array which contains the primitive
        /// information from the array serialized in a sequential series of
        /// bytes.
        /// </summary>
        public byte[] ArrayBytes { get; private set; }
        /// <summary>
        /// Returns whether the conversion from a primitive array
        /// to a byte array was successful.
        /// </summary>
        public bool OperationSuccessful { get; private set; }
        /// <summary>
        /// Returns the <see cref="Type"/> of the primitive within the operation.
        /// </summary>
        public Type PrimitiveType { get; private set; }
        /// <summary>
        /// Returns the <see cref="Int32"/> array which denotes the dimensions
        /// of the array.
        /// </summary>
        /// <remarks>The number of elements is determined by the rank
        /// of the array given to the method.</remarks>
        public int[] ArrayDimensions { get; private set; }
    }
}
