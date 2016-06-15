using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Cli.Metadata.Instructions
{
    /// <summary>Defines properties for working with a basic stack instruction defined within the common intermediate language as a part of the common language infrastructure (Ecma-335).</summary>
    public interface ICilStackInstruction
    {
        CilOpcodeInstruction Instruction { get; }
        int Offset { get; }
        int Length { get; }
        int InputCount { get; }
        int OutputCount { get; }
        ICilStackInstruction Next { get; }
        ICilStackInstruction Previous { get; }
        StackInstructionTransferControlType TransfersControl { get; }
    }

    /// <summary>Defines properties for working with a basic stack instruction, that has a value of <typeparamref name="T"/>, defined within the common intermediate language as a part of the common language infrastructure (Ecma-335).</summary>
    public interface ICilStackInstruction<T> :
        ICilStackInstruction
        where T :
            struct
    {
        T Value { get; }
    }

    public enum StackInstructionTransferControlType
    {
        None,
        Unconditional,
        ConditionalSingle,
        ConditionalMultiple,
    }
}
