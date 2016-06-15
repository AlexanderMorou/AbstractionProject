using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Cli.Metadata.Instructions
{
    /// <summary>Defines properties for working with a switch instruction defined within the Common Intermediate Language.</summary>
    public interface ICilSwitchInstruction :
        ICilStackInstruction
    {
        /// <summary>Returns the <see cref="Int32"/> <see cref="Array"/> of target instructions which are relative to the end of the current instruction (<see cref="ICilStackInstruction.Offset"/> + <see cref="ICilStackInstruction.Length"/>).</summary>
        IEnumerable<int> TargetOffests { get; }
        IEnumerable<ICilStackInstruction> Targets { get; }
    }
}
