using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Cli.Metadata;
using AllenCopeland.Abstraction.Slf.Platforms.WindowsNT;
using AllenCopeland.Abstraction.IO;
using AllenCopeland.Abstraction.Numerics;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using System.Diagnostics;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.Tables
{
    internal partial class CliMetadataMethodBody :
        ICliMetadataMethodBody,
        ICilInstructionVisitor
    {
        private interface IStackInstruction { CilOpcodeInstruction Instruction { get; } }
        private List<IStackInstruction> instructions;
        private class StackInstruction : IStackInstruction
        {
            public StackInstruction(CilOpcodeInstruction instruction, int offset) { this.Instruction = instruction; this.Offset = offset; }
            public CilOpcodeInstruction Instruction { get; private set; }
            public override string ToString() { return string.Format("L_{0:X5}: {1}", this.Offset, this.Instruction); }
            public int Offset { get; private set; }
        }

        private class ReferenceInstruction : StackInstruction { public ReferenceInstruction(CilOpcodeInstruction instruction, int offset, ICliMetadataTableRow target) : base(instruction, offset) { this.Target = target; } public ICliMetadataTableRow Target { get; private set; } public override string ToString() { return string.Format("L_{0:X5}: {1} {2}", this.Offset, this.Instruction, this.Target); } }
        private class StackInstructionWith<T> :
            StackInstruction
            where T :
                struct
        {
            private bool hexable;
            public StackInstructionWith(CilOpcodeInstruction instruction, int offset, T value, bool hexable = true) : base(instruction, offset) { this.Value = value; this.hexable = hexable; }
            public T Value { get; private set; }
            public override string ToString() { return this.hexable ? string.Format("L_{0:X5}: {1} 0x{2:X}", this.Offset, this.Instruction, this.Value) : string.Format("L_{0:X5}: {1} {2}", this.Offset, this.Instruction, this.Value); }
        }

        private class LoadStringInstruction : StackInstruction
        {
            private ICliMetadataUserStringsHeaderAndHeap userStrings;
            public LoadStringInstruction(uint userStringIndex, ICliMetadataUserStringsHeaderAndHeap userStrings, int offset)
                : base(CilOpcodeInstruction.LoadString, offset)
            {
                this.userStrings = userStrings;
                this.UserStringIndex = userStringIndex;
            }
            public uint UserStringIndex { get; private set; }
            public override string ToString() { return string.Format("L_{0:X5}: {1} \"{2}\"", this.Offset, this.Instruction, this.userStrings[this.UserStringIndex]); }
        }


        private class SwitchInstruction : StackInstruction
        {
            public SwitchInstruction(int offset, int[] targets) : base(CilOpcodeInstruction.Switch, offset) { this.Targets = targets; }
            public int[] Targets { get; private set; }
            public int Length { get { return (sizeof(int) * (Targets.Length + 1)) + 1; } }
            public override string ToString() { return string.Format("L_{0:X5}: {1} {{ {2} }}", this.Offset, this.Instruction, string.Join(", ", (from t in this.Targets select string.Format("L_{0:X5}", this.Offset + this.Length + t)).ToArray())); }
        }

        private class BranchInstruction32 : StackInstructionWith<int>
        {
            public BranchInstruction32(CilOpcodeInstruction instruction, int offset, int value) : base(instruction, offset, value) { }
            public override string ToString() { return string.Format("L_{0:X5}: {1} L_{2:X5}", this.Offset, this.Instruction, this.Offset + 5 + this.Value); }
        }
        private class BranchInstruction8 : StackInstructionWith<sbyte>
        {
            public BranchInstruction8(CilOpcodeInstruction instruction, int offset, sbyte value) : base(instruction, offset, value) { }
            public override string ToString() { return string.Format("L_{0:X5}: {1} L_{2:X5}", this.Offset, this.Instruction, this.Offset + 2 + this.Value); }
        }
        ICliMetadataRoot metadataRoot;
        private uint rva;
        private CliMetadataMethodHeader header;
        private bool initialized;
        private object syncObject = new object();
        public CliMetadataMethodBody(ICliMetadataRoot metadataRoot, uint rva)
        {
            this.metadataRoot = metadataRoot;
            this.rva = rva;
        }

        private void Initialize()
        {
            lock (syncObject)
            {
                if (this.initialized)
                    return;
                this.header = new CliMetadataMethodHeader(this.metadataRoot, this.rva, this.BuildBody);
                this.initialized = true;
            }
        }

        private unsafe void BuildBody(byte[] bodyData)
        {
            this.instructions = new List<IStackInstruction>();
            /* *
             * Keep it simple.
             * */
            for (int i = 0; i < bodyData.Length; i++)
            {
                byte current = bodyData[i];
                switch ((CilOpcodeInstruction)current)
                {
                    case CilOpcodeInstruction.Add:
                        this.VisitAdd(i);
                        break;
                    case CilOpcodeInstruction.BitwiseAnd:
                        this.VisitBitwiseAnd(i);
                        break;
                    case CilOpcodeInstruction.BitwiseComplement:
                        this.VisitBitwiseComplement(i);
                        break;
                    case CilOpcodeInstruction.BitwiseExclusiveOr:
                        this.VisitBitwiseExclusiveOr(i);
                        break;
                    case CilOpcodeInstruction.BitwiseOr:
                        this.VisitBitwiseOr(i);
                        break;
                    case CilOpcodeInstruction.Break:
                        this.VisitBreak(i);
                        break;
                    case CilOpcodeInstruction.CheckedAdd:
                        this.VisitCheckedAdd(i);
                        break;
                    case CilOpcodeInstruction.CheckedAddUnSignedOrOrdered:
                        this.VisitCheckedAddUnSignedOrOrdered(i);
                        break;
                    case CilOpcodeInstruction.CheckedConvertToByteAsInt32:
                        this.VisitCheckedConvertToByteAsInt32(i);
                        break;
                    case CilOpcodeInstruction.CheckedConvertToInt16AsInt32:
                        this.VisitCheckedConvertToInt16AsInt32(i);
                        break;
                    case CilOpcodeInstruction.CheckedConvertToInt32AsInt32:
                        this.VisitCheckedConvertToInt32AsInt32(i);
                        break;
                    case CilOpcodeInstruction.CheckedConvertToInt64:
                        this.VisitCheckedConvertToInt64(i);
                        break;
                    case CilOpcodeInstruction.CheckedConvertToNativeInteger:
                        this.VisitCheckedConvertToNativeInteger(i);
                        break;
                    case CilOpcodeInstruction.CheckedConvertToSByteAsInt32:
                        this.VisitCheckedConvertToSByteAsInt32(i);
                        break;
                    case CilOpcodeInstruction.CheckedConvertToUInt16AsInt32:
                        this.VisitCheckedConvertToUInt16AsInt32(i);
                        break;
                    case CilOpcodeInstruction.CheckedConvertToUInt32AsInt32:
                        this.VisitCheckedConvertToUInt32AsInt32(i);
                        break;
                    case CilOpcodeInstruction.CheckedConvertToUInt64AsInt64:
                        this.VisitCheckedConvertToUInt64AsInt64(i);
                        break;
                    case CilOpcodeInstruction.CheckedConvertToUnsignedNativeInteger:
                        this.VisitCheckedConvertToUnsignedNativeInteger(i);
                        break;
                    case CilOpcodeInstruction.CheckedConvertUnsignedToByteAsInt32:
                        this.VisitCheckedConvertUnsignedToByteAsInt32(i);
                        break;
                    case CilOpcodeInstruction.CheckedConvertUnsignedToInt16AsInt32:
                        this.VisitCheckedConvertUnsignedToInt16AsInt32(i);
                        break;
                    case CilOpcodeInstruction.CheckedConvertUnsignedToInt32AsInt32:
                        this.VisitCheckedConvertUnsignedToInt32AsInt32(i);
                        break;
                    case CilOpcodeInstruction.CheckedConvertUnsignedToInt64:
                        this.VisitCheckedConvertUnsignedToInt64(i);
                        break;
                    case CilOpcodeInstruction.CheckedConvertUnsignedToNativeInt:
                        this.VisitCheckedConvertUnsignedToNativeInt(i);
                        break;
                    case CilOpcodeInstruction.CheckedConvertUnsignedToNativeUInt:
                        this.VisitCheckedConvertUnsignedToNativeUInt(i);
                        break;
                    case CilOpcodeInstruction.CheckedConvertUnsignedToSByteAsInt32:
                        this.VisitCheckedConvertUnsignedToSByteAsInt32(i);
                        break;
                    case CilOpcodeInstruction.CheckedConvertUnsignedToUInt16AsInt32:
                        this.VisitCheckedConvertUnsignedToUInt16AsInt32(i);
                        break;
                    case CilOpcodeInstruction.CheckedConvertUnsignedToUInt32AsInt32:
                        this.VisitCheckedConvertUnsignedToUInt32AsInt32(i);
                        break;
                    case CilOpcodeInstruction.CheckedConvertUnsignedToUInt64AsInt64:
                        this.VisitCheckedConvertUnsignedToUInt64AsInt64(i);
                        break;
                    case CilOpcodeInstruction.CheckedMultiply:
                        this.VisitCheckedMultiply(i);
                        break;
                    case CilOpcodeInstruction.CheckedMultiplyUnSignedOrOrdered:
                        this.VisitCheckedMultiplyUnSignedOrOrdered(i);
                        break;
                    case CilOpcodeInstruction.CheckedSubtract:
                        this.VisitCheckedSubtract(i);
                        break;
                    case CilOpcodeInstruction.CheckedSubtractUnSignedOrOrdered:
                        this.VisitCheckedSubtractUnSignedOrOrdered(i);
                        break;
                    case CilOpcodeInstruction.CheckForFiniteNumber:
                        this.VisitCheckForFiniteNumber(i);
                        break;
                    case CilOpcodeInstruction.ConvertUnsignedIntToFloatingPoint:
                        this.VisitConvertUnsignedIntToFloatingPoint(i);
                        break;
                    case CilOpcodeInstruction.Divide:
                        this.VisitDivide(i);
                        break;
                    case CilOpcodeInstruction.DivideUnSignedOrOrdered:
                        this.VisitDivideUnSignedOrOrdered(i);
                        break;
                    case CilOpcodeInstruction.DuplicateStackItem:
                        this.VisitDuplicateStackItem(i);
                        break;
                    case CilOpcodeInstruction.EndFinallyBlock:
                        this.VisitEndFinallyBlock(i);
                        break;
                    case CilOpcodeInstruction.LoadArg0:
                        this.VisitLoadArg0(i);
                        break;
                    case CilOpcodeInstruction.LoadArg1:
                        this.VisitLoadArg1(i);
                        break;
                    case CilOpcodeInstruction.LoadArg2:
                        this.VisitLoadArg2(i);
                        break;
                    case CilOpcodeInstruction.LoadArg3:
                        this.VisitLoadArg3(i);
                        break;
                    case CilOpcodeInstruction.LoadArrayElementAsByte:
                        this.VisitLoadArrayElementAsByte(i);
                        break;
                    case CilOpcodeInstruction.LoadArrayElementAsDouble:
                        this.VisitLoadArrayElementAsDouble(i);
                        break;
                    case CilOpcodeInstruction.LoadArrayElementAsInt16:
                        this.VisitLoadArrayElementAsInt16(i);
                        break;
                    case CilOpcodeInstruction.LoadArrayElementAsInt32:
                        this.VisitLoadArrayElementAsInt32(i);
                        break;
                    case CilOpcodeInstruction.LoadArrayElementAsInt64:
                        this.VisitLoadArrayElementAsInt64(i);
                        break;
                    case CilOpcodeInstruction.LoadArrayElementAsNativeInteger:
                        this.VisitLoadArrayElementAsNativeInteger(i);
                        break;
                    case CilOpcodeInstruction.LoadArrayElementAsSByte:
                        this.VisitLoadArrayElementAsSByte(i);
                        break;
                    case CilOpcodeInstruction.LoadArrayElementAsSingle:
                        this.VisitLoadArrayElementAsSingle(i);
                        break;
                    case CilOpcodeInstruction.LoadArrayElementAsUInt16:
                        this.VisitLoadArrayElementAsUInt16(i);
                        break;
                    case CilOpcodeInstruction.LoadArrayElementAsUInt32:
                        this.VisitLoadArrayElementAsUInt32(i);
                        break;
                    case CilOpcodeInstruction.LoadArrayElementReference:
                        this.VisitLoadArrayElementReference(i);
                        break;
                    case CilOpcodeInstruction.LoadArrayLength:
                        this.VisitLoadArrayLength(i);
                        break;
                    case CilOpcodeInstruction.LoadIndirectByte:
                        this.VisitLoadIndirectByte(i);
                        break;
                    case CilOpcodeInstruction.LoadIndirectDouble:
                        this.VisitLoadIndirectDouble(i);
                        break;
                    case CilOpcodeInstruction.LoadIndirectInt16:
                        this.VisitLoadIndirectInt16(i);
                        break;
                    case CilOpcodeInstruction.LoadIndirectInt32:
                        this.VisitLoadIndirectInt32(i);
                        break;
                    case CilOpcodeInstruction.LoadIndirectInt64OrUInt64:
                        this.VisitLoadIndirectInt64OrUInt64(i);
                        break;
                    case CilOpcodeInstruction.LoadIndirectNativeInteger:
                        this.VisitLoadIndirectNativeInteger(i);
                        break;
                    case CilOpcodeInstruction.LoadIndirectReference:
                        this.VisitLoadIndirectReference(i);
                        break;
                    case CilOpcodeInstruction.LoadIndirectSByte:
                        this.VisitLoadIndirectSByte(i);
                        break;
                    case CilOpcodeInstruction.LoadIndirectSingle:
                        this.VisitLoadIndirectSingle(i);
                        break;
                    case CilOpcodeInstruction.LoadIndirectUInt16:
                        this.VisitLoadIndirectUInt16(i);
                        break;
                    case CilOpcodeInstruction.LoadIndirectUInt32:
                        this.VisitLoadIndirectUInt32(i);
                        break;
                    case CilOpcodeInstruction.LoadInt32ConstantOfEight:
                        this.VisitLoadInt32ConstantOfEight(i);
                        break;
                    case CilOpcodeInstruction.LoadInt32ConstantOfFive:
                        this.VisitLoadInt32ConstantOfFive(i);
                        break;
                    case CilOpcodeInstruction.LoadInt32ConstantOfFour:
                        this.VisitLoadInt32ConstantOfFour(i);
                        break;
                    case CilOpcodeInstruction.LoadInt32ConstantOfMinusOne:
                        this.VisitLoadInt32ConstantOfMinusOne(i);
                        break;
                    case CilOpcodeInstruction.LoadInt32ConstantOfOne:
                        this.VisitLoadInt32ConstantOfOne(i);
                        break;
                    case CilOpcodeInstruction.LoadInt32ConstantOfSeven:
                        this.VisitLoadInt32ConstantOfSeven(i);
                        break;
                    case CilOpcodeInstruction.LoadInt32ConstantOfSix:
                        this.VisitLoadInt32ConstantOfSix(i);
                        break;
                    case CilOpcodeInstruction.LoadInt32ConstantOfThree:
                        this.VisitLoadInt32ConstantOfThree(i);
                        break;
                    case CilOpcodeInstruction.LoadInt32ConstantOfTwo:
                        this.VisitLoadInt32ConstantOfTwo(i);
                        break;
                    case CilOpcodeInstruction.LoadInt32ConstantOfZero:
                        this.VisitLoadInt32ConstantOfZero(i);
                        break;
                    case CilOpcodeInstruction.LoadLocal0:
                        this.VisitLoadLocal0(i);
                        break;
                    case CilOpcodeInstruction.LoadLocal1:
                        this.VisitLoadLocal1(i);
                        break;
                    case CilOpcodeInstruction.LoadLocal2:
                        this.VisitLoadLocal2(i);
                        break;
                    case CilOpcodeInstruction.LoadLocal3:
                        this.VisitLoadLocal3(i);
                        break;
                    case CilOpcodeInstruction.LoadNullValue:
                        this.VisitLoadNullValue(i);
                        break;
                    case CilOpcodeInstruction.Multiply:
                        this.VisitMultiply(i);
                        break;
                    case CilOpcodeInstruction.Negate:
                        this.VisitNegate(i);
                        break;
                    case CilOpcodeInstruction.NoOperation:
                        this.VisitNoOperation(i);
                        break;
                    case CilOpcodeInstruction.PopStackItem:
                        this.VisitPopStackItem(i);
                        break;
                    case CilOpcodeInstruction.Remainder:
                        this.VisitRemainder(i);
                        break;
                    case CilOpcodeInstruction.RemainderUnSignedOrOrdered:
                        this.VisitRemainderUnSignedOrOrdered(i);
                        break;
                    case CilOpcodeInstruction.Return:
                        this.VisitReturn(i);
                        break;
                    case CilOpcodeInstruction.ShiftBitsLeft:
                        this.VisitShiftBitsLeft(i);
                        break;
                    case CilOpcodeInstruction.ShiftBitsRight:
                        this.VisitShiftBitsRight(i);
                        break;
                    case CilOpcodeInstruction.ShiftBitsRightUnSignedOrOrdered:
                        this.VisitShiftBitsRightUnSignedOrOrdered(i);
                        break;
                    case CilOpcodeInstruction.StoreIndirectInt32:
                        this.VisitStoreIndirectInt32(i);
                        break;
                    case CilOpcodeInstruction.StoreDoubleArrayElement:
                        this.VisitStoreDoubleArrayElement(i);
                        break;
                    case CilOpcodeInstruction.StoreIndirectDouble:
                        this.VisitStoreIndirectDouble(i);
                        break;
                    case CilOpcodeInstruction.StoreIndirectInt16:
                        this.VisitStoreIndirectInt16(i);
                        break;
                    case CilOpcodeInstruction.StoreIndirectInt64:
                        this.VisitStoreIndirectInt64(i);
                        break;
                    case CilOpcodeInstruction.StoreIndirectNativeInteger:
                        this.VisitStoreIndirectNativeInteger(i);
                        break;
                    case CilOpcodeInstruction.StoreIndirectReference:
                        this.VisitStoreIndirectReference(i);
                        break;
                    case CilOpcodeInstruction.StoreIndirectSByte:
                        this.VisitStoreIndirectSByte(i);
                        break;
                    case CilOpcodeInstruction.StoreIndirectSingle:
                        this.VisitStoreIndirectSingle(i);
                        break;
                    case CilOpcodeInstruction.StoreInt16ArrayElement:
                        this.VisitStoreInt16ArrayElement(i);
                        break;
                    case CilOpcodeInstruction.StoreInt32ArrayElement:
                        this.VisitStoreInt32ArrayElement(i);
                        break;
                    case CilOpcodeInstruction.StoreInt64ArrayElement:
                        this.VisitStoreInt64ArrayElement(i);
                        break;
                    case CilOpcodeInstruction.StoreLocal0:
                        this.VisitStoreLocal0(i);
                        break;
                    case CilOpcodeInstruction.StoreLocal1:
                        this.VisitStoreLocal1(i);
                        break;
                    case CilOpcodeInstruction.StoreLocal2:
                        this.VisitStoreLocal2(i);
                        break;
                    case CilOpcodeInstruction.StoreLocal3:
                        this.VisitStoreLocal3(i);
                        break;
                    case CilOpcodeInstruction.StoreNativeIntegerArrayElement:
                        this.VisitStoreNativeIntegerArrayElement(i);
                        break;
                    case CilOpcodeInstruction.StoreReferenceArrayElement:
                        this.VisitStoreReferenceArrayElement(i);
                        break;
                    case CilOpcodeInstruction.StoreSByteArrayElement:
                        this.VisitStoreSByteArrayElement(i);
                        break;
                    case CilOpcodeInstruction.StoreSingleArrayElement:
                        this.VisitStoreSingleArrayElement(i);
                        break;
                    case CilOpcodeInstruction.Subtract:
                        this.VisitSubtract(i);
                        break;
                    case CilOpcodeInstruction.ThrowException:
                        this.VisitThrowException(i);
                        break;
                    case CilOpcodeInstruction.UncheckedConvertToByte:
                        this.VisitUncheckedConvertToByte(i);
                        break;
                    case CilOpcodeInstruction.UncheckedConvertToDouble:
                        this.VisitUncheckedConvertToDouble(i);
                        break;
                    case CilOpcodeInstruction.UncheckedConvertToInt16:
                        this.VisitUncheckedConvertToInt16(i);
                        break;
                    case CilOpcodeInstruction.UncheckedConvertToInt32:
                        this.VisitUncheckedConvertToInt32(i);
                        break;
                    case CilOpcodeInstruction.UncheckedConvertToInt64:
                        this.VisitUncheckedConvertToInt64(i);
                        break;
                    case CilOpcodeInstruction.UncheckedConvertToNativeInteger:
                        this.VisitUncheckedConvertToNativeInteger(i);
                        break;
                    case CilOpcodeInstruction.UncheckedConvertToSByte:
                        this.VisitUncheckedConvertToSByte(i);
                        break;
                    case CilOpcodeInstruction.UncheckedConvertToSingle:
                        this.VisitUncheckedConvertToSingle(i);
                        break;
                    case CilOpcodeInstruction.UncheckedConvertToUInt16:
                        this.VisitUncheckedConvertToUInt16(i);
                        break;
                    case CilOpcodeInstruction.UncheckedConvertToUInt32:
                        this.VisitUncheckedConvertToUInt32(i);
                        break;
                    case CilOpcodeInstruction.UncheckedConvertToUInt64:
                        this.VisitUncheckedConvertToUInt64(i);
                        break;
                    case CilOpcodeInstruction.UncheckedConvertToUnsignedNativeInteger:
                        this.VisitUncheckedConvertToUnsignedNativeInteger(i);
                        break;
                    case CilOpcodeInstruction.LoadArgumentShortForm:
                        this.VisitLoadArgumentShortForm((sbyte)bodyData[i + 1], i++);
                        break;
                    case CilOpcodeInstruction.LoadInt32ConstantShortForm:
                        this.VisitLoadInt32ConstantShortForm((sbyte)bodyData[i + 1], i++);
                        break;
                    case CilOpcodeInstruction.LoadLocalAddressShortForm:
                        this.VisitLoadLocalAddressShortForm(bodyData[i + 1], i++);
                        break;
                    case CilOpcodeInstruction.LoadLocalShortForm:
                        this.VisitLoadLocalShortForm(bodyData[i + 1], i++);
                        break;
                    case CilOpcodeInstruction.LoadArgumentAddressShortForm:
                        this.VisitLoadArgumentAddressShortForm(bodyData[i + 1], i++);
                        break;
                    case CilOpcodeInstruction.StoreLocalShortForm:
                        this.VisitStoreLocalShortForm(bodyData[i + 1], i++);
                        break;
                    case CilOpcodeInstruction.StoreArgumentShortForm:
                        this.VisitStoreArgumentShortForm(bodyData[i + 1], i++);
                        break;
                    case CilOpcodeInstruction.LoadInt64Constant:
                        fixed (byte* bodyDataPtr = bodyData)
                            this.VisitLoadInt64Constant((*(long*)&bodyDataPtr[i + 1]).EndianChange(Endianness.LittleEndian), i);
                        i += 8;
                        break;
                    case CilOpcodeInstruction.LoadSingleConstant:
                        fixed (byte* bodyDataPtr = bodyData)
                            this.VisitLoadSingleConstant((*(float*)&bodyDataPtr[i + 1]).EndianChange(Endianness.LittleEndian), i);
                        i += 4;
                        break;
                    case CilOpcodeInstruction.LoadDoubleConstant:
                        fixed (byte* bodyDataPtr = bodyData)
                            this.VisitLoadDoubleConstant((*(double*)&bodyDataPtr[i + 1]).EndianChange(Endianness.LittleEndian), i);
                        i += 8;
                        break;
                    case CilOpcodeInstruction.LoadInt32Constant:
                        {
                            int offset = i;
                            this.VisitLoadInt32Constant(bodyData[++i] | bodyData[++i] << 8 | bodyData[++i] << 16 | bodyData[++i] << 24, offset);
                        }
                        break;
                    case CilOpcodeInstruction.BranchIfEqualToShortForm:
                        this.VisitBranchIfEqualToShortForm((sbyte)bodyData[i + 1], i++);
                        break;
                    case CilOpcodeInstruction.BranchIfFalseShortForm:
                        this.VisitBranchIfFalseShortForm((sbyte)bodyData[i + 1], i++);
                        break;
                    case CilOpcodeInstruction.BranchIfGreaterThanOrEqualToUnSignedOrOrderedShortForm:
                        this.VisitBranchIfGreaterThanOrEqualToUnSignedOrOrderedShortForm((sbyte)bodyData[i + 1], i++);
                        break;
                    case CilOpcodeInstruction.BranchIfGreaterThanShortForm:
                        this.VisitBranchIfGreaterThanShortForm((sbyte)bodyData[i + 1], i++);
                        break;
                    case CilOpcodeInstruction.BranchIfGreaterThanToOrEqualToShortForm:
                        this.VisitBranchIfGreaterThanToOrEqualToShortForm((sbyte)bodyData[i + 1], i++);
                        break;
                    case CilOpcodeInstruction.BranchIfGreaterThanUnSignedOrOrderedShortForm:
                        this.VisitBranchIfGreaterThanUnSignedOrOrderedShortForm((sbyte)bodyData[i + 1], i++);
                        break;
                    case CilOpcodeInstruction.BranchIfLessThanOrEqualToUnSignedOrOrderedShortForm:
                        this.VisitBranchIfLessThanOrEqualToUnSignedOrOrderedShortForm((sbyte)bodyData[i + 1], i++);
                        break;
                    case CilOpcodeInstruction.BranchIfLessThanOrEqualToShortForm:
                        this.VisitBranchIfLessThanOrEqualToShortForm((sbyte)bodyData[i + 1], i++);
                        break;
                    case CilOpcodeInstruction.BranchIfLessThanShortForm:
                        this.VisitBranchIfLessThanShortForm((sbyte)bodyData[i + 1], i++);
                        break;
                    case CilOpcodeInstruction.BranchIfLessThanUnSignedOrOrderedShortForm:
                        this.VisitBranchIfLessThanUnSignedOrOrderedShortForm((sbyte)bodyData[i + 1], i++);
                        break;
                    case CilOpcodeInstruction.BranchIfNotEqualToUnSignedOrOrderedShortForm:
                        this.VisitBranchIfNotEqualToUnSignedOrOrderedShortForm((sbyte)bodyData[i + 1], i++);
                        break;
                    case CilOpcodeInstruction.BranchIfTrueShortForm:
                        this.VisitBranchIfTrueShortForm((sbyte)bodyData[i + 1], i++);
                        break;
                    case CilOpcodeInstruction.LeaveProtectedBlockShortForm:
                        this.VisitLeaveProtectedBlockShortForm((sbyte)bodyData[i + 1], i++);
                        break;
                    case CilOpcodeInstruction.UnconditionalBranchShortForm:
                        this.VisitUnconditionalBranchShortForm((sbyte)bodyData[i + 1], i++);
                        break;
                    case CilOpcodeInstruction.BranchIfEqualTo:
                        this.VisitBranchIfEqualTo(bodyData[++i] | bodyData[++i] << 8 | bodyData[++i] << 16 | bodyData[++i] << 24, i - 4);
                        break;
                    case CilOpcodeInstruction.BranchIfFalse:
                        this.VisitBranchIfFalse(bodyData[++i] | bodyData[++i] << 8 | bodyData[++i] << 16 | bodyData[++i] << 24, i - 4);
                        break;
                    case CilOpcodeInstruction.BranchIfGreaterThan:
                        this.VisitBranchIfGreaterThan(bodyData[++i] | bodyData[++i] << 8 | bodyData[++i] << 16 | bodyData[++i] << 24, i - 4);
                        break;
                    case CilOpcodeInstruction.BranchIfGreaterThanOrEqualTo:
                        this.VisitBranchIfGreaterThanOrEqualTo(bodyData[++i] | bodyData[++i] << 8 | bodyData[++i] << 16 | bodyData[++i] << 24, i - 4);
                        break;
                    case CilOpcodeInstruction.BranchIfGreaterThanOrEqualToUnSignedOrOrdered:
                        this.VisitBranchIfGreaterThanOrEqualToUnSignedOrOrdered(bodyData[++i] | bodyData[++i] << 8 | bodyData[++i] << 16 | bodyData[++i] << 24, i - 4);
                        break;
                    case CilOpcodeInstruction.BranchIfGreaterThanUnSignedOrOrdered:
                        this.VisitBranchIfGreaterThanUnSignedOrOrdered(bodyData[++i] | bodyData[++i] << 8 | bodyData[++i] << 16 | bodyData[++i] << 24, i - 4);
                        break;
                    case CilOpcodeInstruction.BranchIfLessThan:
                        this.VisitBranchIfLessThan(bodyData[++i] | bodyData[++i] << 8 | bodyData[++i] << 16 | bodyData[++i] << 24, i - 4);
                        break;
                    case CilOpcodeInstruction.BranchIfLessThanOrEqualToUnSignedOrOrdered:
                        this.VisitBranchIfLessThanOrEqualToUnSignedOrOrdered(bodyData[++i] | bodyData[++i] << 8 | bodyData[++i] << 16 | bodyData[++i] << 24, i - 4);
                        break;
                    case CilOpcodeInstruction.BranchIfLessThanOrEqualTo:
                        this.VisitBranchIfLessThanOrEqualTo(bodyData[++i] | bodyData[++i] << 8 | bodyData[++i] << 16 | bodyData[++i] << 24, i - 4);
                        break;
                    case CilOpcodeInstruction.BranchIfLessThanUnSignedOrOrdered:
                        this.VisitBranchIfLessThanUnSignedOrOrdered(bodyData[++i] | bodyData[++i] << 8 | bodyData[++i] << 16 | bodyData[++i] << 24, i - 4);
                        break;
                    case CilOpcodeInstruction.BranchIfNotEqualToUnSignedOrOrdered:
                        this.VisitBranchIfNotEqualToUnSignedOrOrdered(bodyData[++i] | bodyData[++i] << 8 | bodyData[++i] << 16 | bodyData[++i] << 24, i - 4);
                        break;
                    case CilOpcodeInstruction.BranchIfTrue:
                        this.VisitBranchIfTrue(bodyData[++i] | bodyData[++i] << 8 | bodyData[++i] << 16 | bodyData[++i] << 24, i - 4);
                        break;
                    case CilOpcodeInstruction.LeaveProtectedBlock:
                        this.VisitLeaveProtectedBlock(bodyData[++i] | bodyData[++i] << 8 | bodyData[++i] << 16 | bodyData[++i] << 24, i - 4);
                        break;
                    case CilOpcodeInstruction.UnconditionalBranch:
                        this.VisitUnconditionalBranch(bodyData[++i] | bodyData[++i] << 8 | bodyData[++i] << 16 | bodyData[++i] << 24, i - 4);
                        break;
                    case CilOpcodeInstruction.Switch:
                        {
                            int offset = i;
                            fixed (byte* bodyDataPtr = bodyData)
                            {
                                uint numTargets = *(uint*)&bodyDataPtr[i + 1];
                                int[] targets = new int[numTargets];
                                fixed (int* targetsPtr = targets)
                                {
                                    for (int k = 0; k < numTargets; k++)
                                        targetsPtr[k] = *(int*)&bodyDataPtr[i + (sizeof(int) * (k + 1)) + 1];
                                }
                                i += (int)(4 * (numTargets + 1));
                                this.VisitSwitch(targets, offset);
                            }
                        }
                        break;
                    case CilOpcodeInstruction.LoadString:
                        {
                            int offset = i;
                            uint decodedIndex = (uint)(bodyData[++i] | bodyData[++i] << 8 | bodyData[++i] << 16 | bodyData[++i] << 24);
                            var prolog = (((CliMetadataTableKinds)decodedIndex) & CliMetadataTableKinds.UserStringMask) == CliMetadataTableKinds.UserStringMask;
                            if (!prolog)
                                throw new BadImageFormatException();
                            decodedIndex ^= ((uint)CliMetadataTableKinds.UserStringMask);
                            this.VisitLoadString(++decodedIndex, offset);
                        }
                        break;
                    case CilOpcodeInstruction.AddressCopy:
                    case CilOpcodeInstruction.Box:
                    case CilOpcodeInstruction.CallMethod:
                    case CilOpcodeInstruction.CallMethodIndirect:
                    case CilOpcodeInstruction.CallVirtualMethod:
                    case CilOpcodeInstruction.CastClass:
                    case CilOpcodeInstruction.GetTypedReferenceAddress:
                    case CilOpcodeInstruction.IsInstance:
                    case CilOpcodeInstruction.JumpTo:
                    case CilOpcodeInstruction.LoadArrayElementAddress:
                    case CilOpcodeInstruction.LoadElement:
                    case CilOpcodeInstruction.LoadField:
                    case CilOpcodeInstruction.LoadFieldAddress:
                    case CilOpcodeInstruction.LoadObject:
                    case CilOpcodeInstruction.LoadStaticField:
                    case CilOpcodeInstruction.LoadStaticFieldAddress:
                    case CilOpcodeInstruction.LoadToken:
                    case CilOpcodeInstruction.MakeTypedReference:
                    case CilOpcodeInstruction.NewArray:
                    case CilOpcodeInstruction.NewObject:
                    case CilOpcodeInstruction.StoreElement:
                    case CilOpcodeInstruction.StoreField:
                    case CilOpcodeInstruction.StoreObject:
                    case CilOpcodeInstruction.StoreStaticField:
                    case CilOpcodeInstruction.Unbox:
                    case CilOpcodeInstruction.UnboxAny:
                        {
                            int offset = i;
                            var metadataToken = bodyData[++i] | bodyData[++i] << 8 | bodyData[++i] << 16 | bodyData[++i] << 24;
                            var tableKind = (CliMetadataTableKinds)(1UL << (int)((metadataToken & 0xFF000000) >> 24));
                            var index = metadataToken & 0x00FFFFFF;
                            ICliMetadataTable table;
                            if (this.metadataRoot.TableStream.TryGetValue(tableKind, out table))
                            {
                                if (index < 0 || index > table.Count + 1)
                                    this.VisitMetadataInstruction((CilOpcodeInstruction)current, null, offset);
                                else
                                    this.VisitMetadataInstruction((CilOpcodeInstruction)current, (ICliMetadataTableRow)table[index], offset);
                            }
                            else
                                this.VisitMetadataInstruction((CilOpcodeInstruction)current, null, offset);
                        }
                        break;
                    case CilOpcodeInstruction.TwoByteLeadIn:
                        {
                            int offset = i;
                            if (bodyData.Length == i + 1)
                                break;
                            byte next = bodyData[++i];
                            ushort nextShort = (ushort)((((int)CilOpcodeInstruction.TwoByteLeadIn) << 8) | next);
                            switch ((CilOpcodeInstruction)nextShort)
                            {
                                case CilOpcodeInstruction.AllocateLocalMemory:
                                    this.VisitAllocateLocalMemory(offset);
                                    break;
                                case CilOpcodeInstruction.CompareEquality:
                                    this.VisitCompareEquality(offset);
                                    break;
                                case CilOpcodeInstruction.CompareGreaterThan:
                                    this.VisitCompareGreaterThan(offset);
                                    break;
                                case CilOpcodeInstruction.CompareGreaterThanUnSignedOrOrdered:
                                    this.VisitCompareGreaterThanUnSignedOrOrdered(offset);
                                    break;
                                case CilOpcodeInstruction.CompareLessThan:
                                    this.VisitCompareLessThan(offset);
                                    break;
                                case CilOpcodeInstruction.CompareLessThanUnSignedOrOrdered:
                                    this.VisitCompareLessThanUnSignedOrOrdered(offset);
                                    break;
                                case CilOpcodeInstruction.CopyMemoryBlock:
                                    this.VisitCopyMemoryBlock(offset);
                                    break;
                                case CilOpcodeInstruction.EndFilter:
                                    this.VisitEndFilter(offset);
                                    break;
                                case CilOpcodeInstruction.GetArgumentList:
                                    this.VisitGetArgumentList(offset);
                                    break;
                                case CilOpcodeInstruction.InitializeMemoryBlock:
                                    this.VisitInitializeMemoryBlock(offset);
                                    break;
                                case CilOpcodeInstruction.PointerReferenceMayBeUnaligned:
                                    this.VisitPointerReferenceMayBeUnaligned(offset);
                                    break;
                                case CilOpcodeInstruction.TailCallModifier:
                                    this.VisitTailCallModifier(offset);
                                    break;
                                case CilOpcodeInstruction.VolatilePointerReference:
                                    this.VisitVolatilePointerReference(offset);
                                    break;
                                case CilOpcodeInstruction.LoadArgument:
                                    this.VisitLoadArgument((short)(bodyData[++i] | bodyData[++i] << 8), offset);
                                    break;
                                case CilOpcodeInstruction.LoadArgumentAddress:
                                    this.VisitLoadArgumentAddress((short)(bodyData[++i] | bodyData[++i] << 8), offset);
                                    break;
                                case CilOpcodeInstruction.LoadLocal:
                                    this.VisitLoadLocal((short)(bodyData[++i] | bodyData[++i] << 8), offset);
                                    break;
                                case CilOpcodeInstruction.LoadLocalAddress:
                                    this.VisitLoadLocalAddress((short)(bodyData[++i] | bodyData[++i] << 8), offset);
                                    break;
                                case CilOpcodeInstruction.StoreArgument:
                                    this.VisitStoreArgument((short)(bodyData[++i] | bodyData[++i] << 8), offset);
                                    break;
                                case CilOpcodeInstruction.StoreLocal:
                                    this.VisitStoreLocal((short)(bodyData[++i] | bodyData[++i] << 8), offset);
                                    break;
                                case CilOpcodeInstruction.ConstrainedCallVirtModifier:
                                case CilOpcodeInstruction.InitializeObject:
                                case CilOpcodeInstruction.LoadMethodPointer:
                                case CilOpcodeInstruction.LoadVirtualMethodPointer:
                                    {
                                        var metadataToken = bodyData[++i] | bodyData[++i] << 8 | bodyData[++i] << 16 | bodyData[++i] << 24;
                                        var tableKind = (CliMetadataTableKinds)(1UL << (int)((metadataToken & 0xFF000000) >> 24));
                                        var index = metadataToken & 0x00FFFFFF;
                                        ICliMetadataTable table;
                                        if (this.metadataRoot.TableStream.TryGetValue(tableKind, out table))
                                        {
                                            if (index < 0 || index > table.Count + 1)
                                                this.VisitMetadataInstruction((CilOpcodeInstruction)nextShort, null, offset);
                                            else
                                                this.VisitMetadataInstruction((CilOpcodeInstruction)nextShort, (ICliMetadataTableRow)table[index], offset);
                                        }
                                        else
                                            this.VisitMetadataInstruction((CilOpcodeInstruction)nextShort, null, offset);
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        private void VisitMetadataInstruction(CilOpcodeInstruction instruction, ICliMetadataTableRow metadataEntryRow, int offset)
        {
            switch (instruction)
            {
                case CilOpcodeInstruction.AddressCopy:
                    this.VisitAddressCopy(metadataEntryRow, offset);
                    break;
                case CilOpcodeInstruction.Box:
                    this.VisitBox(metadataEntryRow, offset);
                    break;
                case CilOpcodeInstruction.CallMethod:
                    this.VisitCallMethod(metadataEntryRow, offset);
                    break;
                case CilOpcodeInstruction.CallMethodIndirect:
                    this.VisitCallMethodIndirect(metadataEntryRow, offset);
                    break;
                case CilOpcodeInstruction.CallVirtualMethod:
                    this.VisitCallVirtualMethod(metadataEntryRow, offset);
                    break;
                case CilOpcodeInstruction.CastClass:
                    this.VisitCastClass(metadataEntryRow, offset);
                    break;
                case CilOpcodeInstruction.GetTypedReferenceAddress:
                    this.VisitGetTypedReferenceAddress(metadataEntryRow, offset);
                    break;
                case CilOpcodeInstruction.IsInstance:
                    this.VisitIsInstance(metadataEntryRow, offset);
                    break;
                case CilOpcodeInstruction.JumpTo:
                    this.VisitJumpTo(metadataEntryRow, offset);
                    break;
                case CilOpcodeInstruction.LoadArrayElementAddress:
                    this.VisitLoadArrayElementAddress(metadataEntryRow, offset);
                    break;
                case CilOpcodeInstruction.LoadElement:
                    this.VisitLoadElement(metadataEntryRow, offset);
                    break;
                case CilOpcodeInstruction.LoadField:
                    this.VisitLoadField(metadataEntryRow, offset);
                    break;
                case CilOpcodeInstruction.LoadFieldAddress:
                    this.VisitLoadFieldAddress(metadataEntryRow, offset);
                    break;
                case CilOpcodeInstruction.LoadObject:
                    this.VisitLoadObject(metadataEntryRow, offset);
                    break;
                case CilOpcodeInstruction.LoadStaticField:
                    this.VisitLoadStaticField(metadataEntryRow, offset);
                    break;
                case CilOpcodeInstruction.LoadStaticFieldAddress:
                    this.VisitLoadStaticFieldAddress(metadataEntryRow, offset);
                    break;
                case CilOpcodeInstruction.LoadToken:
                    this.VisitLoadToken(metadataEntryRow, offset);
                    break;
                case CilOpcodeInstruction.MakeTypedReference:
                    this.VisitMakeTypedReference(metadataEntryRow, offset);
                    break;
                case CilOpcodeInstruction.NewArray:
                    this.VisitNewArray(metadataEntryRow, offset);
                    break;
                case CilOpcodeInstruction.NewObject:
                    this.VisitNewObject(metadataEntryRow, offset);
                    break;
                case CilOpcodeInstruction.StoreElement:
                    this.VisitStoreElement(metadataEntryRow, offset);
                    break;
                case CilOpcodeInstruction.StoreField:
                    this.VisitStoreField(metadataEntryRow, offset);
                    break;
                case CilOpcodeInstruction.StoreObject:
                    this.VisitStoreObject(metadataEntryRow, offset);
                    break;
                case CilOpcodeInstruction.StoreStaticField:
                    this.VisitStoreStaticField(metadataEntryRow, offset);
                    break;
                case CilOpcodeInstruction.Unbox:
                    this.VisitUnbox(metadataEntryRow, offset);
                    break;
                case CilOpcodeInstruction.UnboxAny:
                    this.VisitUnboxAny(metadataEntryRow, offset);
                    break;
                case CilOpcodeInstruction.ConstrainedCallVirtModifier:
                    this.VisitConstrainedCallVirtModifier(metadataEntryRow, offset);
                    break;
                case CilOpcodeInstruction.InitializeObject:
                    this.VisitInitializeObject(metadataEntryRow, offset);
                    break;
                case CilOpcodeInstruction.LoadMethodPointer:
                    this.VisitLoadMethodPointer(metadataEntryRow, offset);
                    break;
                case CilOpcodeInstruction.LoadVirtualMethodPointer:
                    this.VisitLoadVirtualMethodPointer(metadataEntryRow, offset);
                    break;
            }
        }

        public ICliMetadataMethodHeader Header
        {
            get
            {
                lock (syncObject)
                    if (!this.initialized)
                        this.Initialize();
                return this.header;
            }
        }
    }
}
