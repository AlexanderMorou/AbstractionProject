using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;

namespace AllenCopeland.Abstraction.Slf.Oil
{
    public interface IDataSizeType :
        IStructType
    {
        /// <summary>
        /// Returns the <see cref="Int32"/> value representing
        /// the number of bytes represented by the
        /// <see cref="IDataSizeType"/>.
        /// </summary>
        int DataSize { get; }
    }
}
