using AllenCopeland.Abstraction.Slf.Cli.Metadata;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Instructions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.Instructions
{
    internal class CilBinaryOperationEntry :
        CilStackEntry,
        ICilBinaryOperationEntry
    {
        private int instructionOffset;
        public CilBinaryOperationEntry(CilOpcodeInstruction instruction, int instructionOffset)
            : base(instruction)
        {
            this.instructionOffset = instructionOffset;
        }

        public override int StartOffset
        {
            get { return this.Left.StartOffset; }
        }

        public override int EndOffset
        {
            get { return this.InstructionOffset + this.InstructionSize; }
        }

        public override int InstructionOffset { get { return this.instructionOffset; } }

        public override int InstructionSize
        {
            get {
                switch (Instruction)
                {
                    case CilOpcodeInstruction.Add:
                    case CilOpcodeInstruction.BitwiseAnd:
                    case CilOpcodeInstruction.BitwiseComplement:
                    case CilOpcodeInstruction.BitwiseExclusiveOr:
                    case CilOpcodeInstruction.BitwiseOr:
                    case CilOpcodeInstruction.CheckedAdd:
                    case CilOpcodeInstruction.CheckedAddUnSignedOrOrdered:
                    case CilOpcodeInstruction.CheckedMultiply:
                    case CilOpcodeInstruction.CheckedMultiplyUnSignedOrOrdered:
                    case CilOpcodeInstruction.CheckedSubtract:
                    case CilOpcodeInstruction.CheckedSubtractUnSignedOrOrdered:
                    case CilOpcodeInstruction.Divide:
                    case CilOpcodeInstruction.DivideUnSignedOrOrdered:
                    case CilOpcodeInstruction.Multiply:
                    case CilOpcodeInstruction.Remainder:
                    case CilOpcodeInstruction.RemainderUnSignedOrOrdered:
                    case CilOpcodeInstruction.ShiftBitsLeft:
                    case CilOpcodeInstruction.ShiftBitsRight:
                    case CilOpcodeInstruction.ShiftBitsRightUnSignedOrOrdered:
                    case CilOpcodeInstruction.Subtract:
                        return 1;
                    case CilOpcodeInstruction.CompareEquality:
                    case CilOpcodeInstruction.CompareGreaterThan:
                    case CilOpcodeInstruction.CompareGreaterThanUnSignedOrOrdered:
                    case CilOpcodeInstruction.CompareLessThan:
                    case CilOpcodeInstruction.CompareLessThanUnSignedOrOrdered:
                        return 2;
                }
                throw new InvalidOperationException();
            }
        }

        public override int FullSize
        {
            get { throw new NotImplementedException(); }
        }

        public ICilStackEntry Left
        {
            get { throw new NotImplementedException(); }
        }

        public ICilStackEntry Right
        {
            get { throw new NotImplementedException(); }
        }

        public IEnumerator<ICilStackEntry> GetEnumerator()
        {
            yield return this.Left;
            yield return this.Right;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
