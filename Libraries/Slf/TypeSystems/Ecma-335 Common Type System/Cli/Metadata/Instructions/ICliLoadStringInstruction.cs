using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Cli.Metadata.Instructions
{
    public interface ICilLoadStringInstruction :
        ICilStackInstruction
    {
        /// <summary>Returns the <see cref="Int32"/> value denoting the index within the user string table to load the user string from.</summary>
        uint UserStringIndex { get; }
    }
}
