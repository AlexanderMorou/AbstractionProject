using AllenCopeland.Abstraction.Slf.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Cli
{
    public interface ICliDelegateType :
        IDelegateType,
        ICliType
    {
        /// <summary>
        /// Returns the <see cref="Int32"/> value relative to the
        /// method index of the invoke method for the delegate.
        /// </summary>
        uint InvokeMethodIndex { get; }
        /// <summary>
        /// Returns the <see cref="Int32"/> value relative to the
        /// method index of the asynchronous begin invoke method for the delegate.
        /// </summary>
        uint BeginInvokeMethodIndex { get; }
        /// <summary>
        /// Returns the <see cref="Int32"/> value relative to the
        /// method index of the asynchronous end invoke method for the delegate.
        /// </summary>
        uint EndInvokeMethodIndex { get; }
    }
}
