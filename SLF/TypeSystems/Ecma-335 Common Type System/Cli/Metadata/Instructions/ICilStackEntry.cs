using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Cli.Metadata.Instructions
{
    /// <summary>
    /// Defines properties and methods for working with a stack entry 
    /// within the scope of 
    /// </summary>
    public interface ICilStackEntry
    {
        /// <summary>
        /// Returns the <see cref="ICilStackEntry"/> which denotes the
        /// instruction represented by the <see cref="ICilStackEntry"/>.
        /// </summary>
        CilOpcodeInstruction Instruction { get; }
        /// <summary>
        /// Returns the <see cref="Int32"/> value which denotes the start
        /// offset of the stack entry.
        /// </summary>
        int StartOffset { get; }
        /// <summary>
        /// Returns the <see cref="Int32"/> value which denotes the end
        /// offset of the stack entry.
        /// </summary>
        int EndOffset { get; }
        /// <summary>
        /// Returns the <see cref="Int32"/> value which denotes where
        /// the instruction itself starts.
        /// </summary>
        int InstructionOffset { get; }
        /// <summary>
        /// Returns the <see cref="Int32"/> value denoting the size of the 
        /// instruction and any accompanying bytes required by the instruction.
        /// </summary>
        int InstructionSize { get; }
        /// <summary>
        /// Returns the <see cref="Int32"/> value denoting the full size of 
        /// the instruction, including child instructions.
        /// </summary>
        int FullSize { get; }
    }
}
