using AllenCopeland.Abstraction.Slf.Cli.Metadata;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Instructions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.Instructions
{
    internal abstract class CilStackEntry :
        ICilStackEntry
    {
        public CilStackEntry(CilOpcodeInstruction instruction) { this.Instruction = instruction; }
        public CilOpcodeInstruction Instruction { get; private set; }

        public abstract int InstructionOffset { get; }

        public abstract int StartOffset { get; }

        public abstract int EndOffset { get; }

        public abstract int InstructionSize { get; }

        public abstract int FullSize { get; }
    }
}
