using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Cli.Metadata.Instructions
{
    interface ICilBinaryOperationEntry :
        ICilMultipleStackEntry
    {
        /// <summary>
        /// Returns the <see cref="ICilStackEntry"/>
        /// which denotes the left half
        /// of the operation.
        /// </summary>
        ICilStackEntry Left { get; }
        /// <summary>
        /// Returns the <see cref="ICilStackEntry"/>
        /// which denotes the right half of the operation.
        /// </summary>
        ICilStackEntry Right { get; }
    }
}
