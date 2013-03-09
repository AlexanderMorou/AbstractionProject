using AllenCopeland.Abstraction.Slf.Cli.Metadata;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.Tables
{
    partial class CliMetadataMethodBody :
        ICliMetadataMethodBody,
        ICilInstructionVisitor
    {
        public void VisitAdd(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.Add, instructionOffset));
        }

        public void VisitBitwiseAnd(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.BitwiseAnd, instructionOffset));
        }

        public void VisitBitwiseComplement(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.BitwiseComplement, instructionOffset));
        }

        public void VisitBitwiseExclusiveOr(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.BitwiseExclusiveOr, instructionOffset));
        }

        public void VisitBitwiseOr(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.BitwiseOr, instructionOffset));
        }

        public void VisitBreak(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.Break, instructionOffset));
        }

        public void VisitCheckedAdd(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.CheckedAdd, instructionOffset));
        }

        public void VisitCheckedAddUnSignedOrOrdered(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.CheckedAddUnSignedOrOrdered, instructionOffset));
        }

        public void VisitCheckedConvertToByteAsInt32(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.CheckedConvertToByteAsInt32, instructionOffset));
        }

        public void VisitCheckedConvertToInt16AsInt32(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.CheckedConvertToInt16AsInt32, instructionOffset));
        }

        public void VisitCheckedConvertToInt32AsInt32(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.CheckedConvertToInt32AsInt32, instructionOffset));
        }

        public void VisitCheckedConvertToInt64(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.CheckedConvertToInt64, instructionOffset));
        }

        public void VisitCheckedConvertToNativeInteger(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.CheckedConvertToNativeInteger, instructionOffset));
        }

        public void VisitCheckedConvertToSByteAsInt32(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.CheckedConvertToSByteAsInt32, instructionOffset));
        }

        public void VisitCheckedConvertToUInt16AsInt32(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.CheckedConvertToUInt16AsInt32, instructionOffset));
        }

        public void VisitCheckedConvertToUInt32AsInt32(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.CheckedConvertToUInt32AsInt32, instructionOffset));
        }

        public void VisitCheckedConvertToUInt64AsInt64(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.CheckedConvertToUInt64AsInt64, instructionOffset));
        }

        public void VisitCheckedConvertToUnsignedNativeInteger(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.CheckedConvertToUnsignedNativeInteger, instructionOffset));
        }

        public void VisitCheckedConvertUnsignedToByteAsInt32(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.CheckedConvertUnsignedToByteAsInt32, instructionOffset));
        }

        public void VisitCheckedConvertUnsignedToInt16AsInt32(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.CheckedConvertUnsignedToInt16AsInt32, instructionOffset));
        }

        public void VisitCheckedConvertUnsignedToInt32AsInt32(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.CheckedConvertUnsignedToInt32AsInt32, instructionOffset));
        }

        public void VisitCheckedConvertUnsignedToInt64(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.CheckedConvertUnsignedToInt64, instructionOffset));
        }

        public void VisitCheckedConvertUnsignedToNativeInt(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.CheckedConvertUnsignedToNativeInt, instructionOffset));
        }

        public void VisitCheckedConvertUnsignedToNativeUInt(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.CheckedConvertUnsignedToNativeUInt, instructionOffset));
        }

        public void VisitCheckedConvertUnsignedToSByteAsInt32(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.CheckedConvertUnsignedToSByteAsInt32, instructionOffset));
        }

        public void VisitCheckedConvertUnsignedToUInt16AsInt32(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.CheckedConvertUnsignedToUInt16AsInt32, instructionOffset));
        }

        public void VisitCheckedConvertUnsignedToUInt32AsInt32(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.CheckedConvertUnsignedToUInt32AsInt32, instructionOffset));
        }

        public void VisitCheckedConvertUnsignedToUInt64AsInt64(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.CheckedConvertUnsignedToUInt64AsInt64, instructionOffset));
        }

        public void VisitCheckedMultiply(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.CheckedMultiply, instructionOffset));
        }

        public void VisitCheckedMultiplyUnSignedOrOrdered(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.CheckedMultiplyUnSignedOrOrdered, instructionOffset));
        }

        public void VisitCheckedSubtract(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.CheckedSubtract, instructionOffset));
        }

        public void VisitCheckedSubtractUnSignedOrOrdered(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.CheckedSubtractUnSignedOrOrdered, instructionOffset));
        }

        public void VisitCheckForFiniteNumber(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.CheckForFiniteNumber, instructionOffset));
        }

        public void VisitConvertUnsignedIntToFloatingPoint(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.ConvertUnsignedIntToFloatingPoint, instructionOffset));
        }

        public void VisitDivide(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.Divide, instructionOffset));
        }

        public void VisitDivideUnSignedOrOrdered(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.DivideUnSignedOrOrdered, instructionOffset));
        }

        public void VisitDuplicateStackItem(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.DuplicateStackItem, instructionOffset));
        }

        public void VisitEndFinallyBlock(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.EndFinallyBlock, instructionOffset));
        }

        public void VisitLoadArg0(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.LoadArg0, instructionOffset));
        }

        public void VisitLoadArg1(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.LoadArg1, instructionOffset));
        }

        public void VisitLoadArg2(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.LoadArg2, instructionOffset));
        }

        public void VisitLoadArg3(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.LoadArg3, instructionOffset));
        }

        public void VisitLoadArrayElementAsByte(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.LoadArrayElementAsByte, instructionOffset));
        }

        public void VisitLoadArrayElementAsDouble(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.LoadArrayElementAsDouble, instructionOffset));
        }

        public void VisitLoadArrayElementAsInt16(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.LoadArrayElementAsInt16, instructionOffset));
        }

        public void VisitLoadArrayElementAsInt32(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.LoadArrayElementAsInt32, instructionOffset));
        }

        public void VisitLoadArrayElementAsInt64(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.LoadArrayElementAsInt64, instructionOffset));
        }

        public void VisitLoadArrayElementAsNativeInteger(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.LoadArrayElementAsNativeInteger, instructionOffset));
        }

        public void VisitLoadArrayElementAsSByte(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.LoadArrayElementAsSByte, instructionOffset));
        }

        public void VisitLoadArrayElementAsSingle(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.LoadArrayElementAsSingle, instructionOffset));
        }

        public void VisitLoadArrayElementAsUInt16(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.LoadArrayElementAsUInt16, instructionOffset));
        }

        public void VisitLoadArrayElementAsUInt32(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.LoadArrayElementAsUInt32, instructionOffset));
        }

        public void VisitLoadArrayElementReference(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.LoadArrayElementReference, instructionOffset));
        }

        public void VisitLoadArrayLength(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.LoadArrayLength, instructionOffset));
        }

        public void VisitLoadIndirectByte(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.LoadIndirectByte, instructionOffset));
        }

        public void VisitLoadIndirectDouble(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.LoadIndirectDouble, instructionOffset));
        }

        public void VisitLoadIndirectInt16(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.LoadIndirectInt16, instructionOffset));
        }

        public void VisitLoadIndirectInt32(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.LoadIndirectInt32, instructionOffset));
        }

        public void VisitLoadIndirectInt64OrUInt64(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.LoadIndirectInt64OrUInt64, instructionOffset));
        }

        public void VisitLoadIndirectNativeInteger(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.LoadIndirectNativeInteger, instructionOffset));
        }

        public void VisitLoadIndirectReference(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.LoadIndirectReference, instructionOffset));
        }

        public void VisitLoadIndirectSByte(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.LoadIndirectSByte, instructionOffset));
        }

        public void VisitLoadIndirectSingle(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.LoadIndirectSingle, instructionOffset));
        }

        public void VisitLoadIndirectUInt16(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.LoadIndirectUInt16, instructionOffset));
        }

        public void VisitLoadIndirectUInt32(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.LoadIndirectUInt32, instructionOffset));
        }

        public void VisitLoadInt32ConstantOfEight(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.LoadInt32ConstantOfEight, instructionOffset));
        }

        public void VisitLoadInt32ConstantOfFive(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.LoadInt32ConstantOfFive, instructionOffset));
        }

        public void VisitLoadInt32ConstantOfFour(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.LoadInt32ConstantOfFour, instructionOffset));
        }

        public void VisitLoadInt32ConstantOfMinusOne(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.LoadInt32ConstantOfMinusOne, instructionOffset));
        }

        public void VisitLoadInt32ConstantOfOne(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.LoadInt32ConstantOfOne, instructionOffset));
        }

        public void VisitLoadInt32ConstantOfSeven(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.LoadInt32ConstantOfSeven, instructionOffset));
        }

        public void VisitLoadInt32ConstantOfSix(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.LoadInt32ConstantOfSix, instructionOffset));
        }

        public void VisitLoadInt32ConstantOfThree(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.LoadInt32ConstantOfThree, instructionOffset));
        }

        public void VisitLoadInt32ConstantOfTwo(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.LoadInt32ConstantOfTwo, instructionOffset));
        }

        public void VisitLoadInt32ConstantOfZero(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.LoadInt32ConstantOfZero, instructionOffset));
        }

        public void VisitLoadLocal0(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.LoadLocal0, instructionOffset));
        }

        public void VisitLoadLocal1(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.LoadLocal1, instructionOffset));
        }

        public void VisitLoadLocal2(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.LoadLocal2, instructionOffset));
        }

        public void VisitLoadLocal3(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.LoadLocal3, instructionOffset));
        }

        public void VisitLoadNullValue(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.LoadNullValue, instructionOffset));
        }

        public void VisitMultiply(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.Multiply, instructionOffset));
        }

        public void VisitNegate(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.Negate, instructionOffset));
        }

        public void VisitNoOperation(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.NoOperation, instructionOffset));
        }

        public void VisitPopStackItem(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.PopStackItem, instructionOffset));
        }

        public void VisitRemainder(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.Remainder, instructionOffset));
        }

        public void VisitRemainderUnSignedOrOrdered(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.RemainderUnSignedOrOrdered, instructionOffset));
        }

        public void VisitReturn(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.Return, instructionOffset));
        }

        public void VisitShiftBitsLeft(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.ShiftBitsLeft, instructionOffset));
        }

        public void VisitShiftBitsRight(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.ShiftBitsRight, instructionOffset));
        }

        public void VisitShiftBitsRightUnSignedOrOrdered(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.ShiftBitsRightUnSignedOrOrdered, instructionOffset));
        }

        public void VisitStoreIndirectInt32(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.StoreIndirectInt32, instructionOffset));
        }

        public void VisitStoreDoubleArrayElement(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.StoreDoubleArrayElement, instructionOffset));
        }

        public void VisitStoreIndirectDouble(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.StoreIndirectDouble, instructionOffset));
        }

        public void VisitStoreIndirectInt16(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.StoreIndirectInt16, instructionOffset));
        }

        public void VisitStoreIndirectInt64(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.StoreIndirectInt64, instructionOffset));
        }

        public void VisitStoreIndirectNativeInteger(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.StoreIndirectNativeInteger, instructionOffset));
        }

        public void VisitStoreIndirectReference(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.StoreIndirectReference, instructionOffset));
        }

        public void VisitStoreIndirectSByte(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.StoreIndirectSByte, instructionOffset));
        }

        public void VisitStoreIndirectSingle(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.StoreIndirectSingle, instructionOffset));
        }

        public void VisitStoreInt16ArrayElement(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.StoreInt16ArrayElement, instructionOffset));
        }

        public void VisitStoreInt32ArrayElement(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.StoreInt32ArrayElement, instructionOffset));
        }

        public void VisitStoreInt64ArrayElement(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.StoreInt64ArrayElement, instructionOffset));
        }

        public void VisitStoreLocal0(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.StoreLocal0, instructionOffset));
        }

        public void VisitStoreLocal1(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.StoreLocal1, instructionOffset));
        }

        public void VisitStoreLocal2(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.StoreLocal2, instructionOffset));
        }

        public void VisitStoreLocal3(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.StoreLocal3, instructionOffset));
        }

        public void VisitStoreNativeIntegerArrayElement(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.StoreNativeIntegerArrayElement, instructionOffset));
        }

        public void VisitStoreReferenceArrayElement(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.StoreReferenceArrayElement, instructionOffset));
        }

        public void VisitStoreSByteArrayElement(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.StoreSByteArrayElement, instructionOffset));
        }

        public void VisitStoreSingleArrayElement(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.StoreSingleArrayElement, instructionOffset));
        }

        public void VisitSubtract(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.Subtract, instructionOffset));
        }

        public void VisitThrowException(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.ThrowException, instructionOffset));
        }

        public void VisitUncheckedConvertToByte(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.UncheckedConvertToByte, instructionOffset));
        }

        public void VisitUncheckedConvertToDouble(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.UncheckedConvertToDouble, instructionOffset));
        }

        public void VisitUncheckedConvertToInt16(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.UncheckedConvertToInt16, instructionOffset));
        }

        public void VisitUncheckedConvertToInt32(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.UncheckedConvertToInt32, instructionOffset));
        }

        public void VisitUncheckedConvertToInt64(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.UncheckedConvertToInt64, instructionOffset));
        }

        public void VisitUncheckedConvertToNativeInteger(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.UncheckedConvertToNativeInteger, instructionOffset));
        }

        public void VisitUncheckedConvertToSByte(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.UncheckedConvertToSByte, instructionOffset));
        }

        public void VisitUncheckedConvertToSingle(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.UncheckedConvertToSingle, instructionOffset));
        }

        public void VisitUncheckedConvertToUInt16(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.UncheckedConvertToUInt16, instructionOffset));
        }

        public void VisitUncheckedConvertToUInt32(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.UncheckedConvertToUInt32, instructionOffset));
        }

        public void VisitUncheckedConvertToUInt64(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.UncheckedConvertToUInt64, instructionOffset));
        }

        public void VisitUncheckedConvertToUnsignedNativeInteger(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.UncheckedConvertToUnsignedNativeInteger, instructionOffset));
        }

        public void VisitLoadArgumentShortForm(sbyte value, int instructionOffset)
        {
            this.instructions.Add(new StackInstructionWith<sbyte>(CilOpcodeInstruction.LoadArgumentShortForm, instructionOffset, value));
        }

        public void VisitLoadInt32ConstantShortForm(sbyte value, int instructionOffset)
        {
            this.instructions.Add(new StackInstructionWith<sbyte>(CilOpcodeInstruction.LoadInt32ConstantShortForm, instructionOffset, value));
        }

        public void VisitLoadLocalAddressShortForm(byte value, int instructionOffset)
        {
            this.instructions.Add(new StackInstructionWith<byte>(CilOpcodeInstruction.LoadLocalAddressShortForm, instructionOffset, value));
        }

        public void VisitLoadLocalShortForm(byte value, int instructionOffset)
        {
            this.instructions.Add(new StackInstructionWith<byte>(CilOpcodeInstruction.LoadLocalShortForm, instructionOffset, value));
        }

        public void VisitLoadArgumentAddressShortForm(byte value, int instructionOffset)
        {
            this.instructions.Add(new StackInstructionWith<byte>(CilOpcodeInstruction.LoadArgumentAddressShortForm, instructionOffset, value));
        }

        public void VisitStoreLocalShortForm(byte value, int instructionOffset)
        {
            this.instructions.Add(new StackInstructionWith<byte>(CilOpcodeInstruction.StoreLocalShortForm, instructionOffset, value));
        }

        public void VisitStoreArgumentShortForm(byte value, int instructionOffset)
        {
            this.instructions.Add(new StackInstructionWith<byte>(CilOpcodeInstruction.StoreArgumentShortForm, instructionOffset, value));
        }

        public void VisitLoadInt64Constant(long value, int instructionOffset)
        {
            this.instructions.Add(new StackInstructionWith<long>(CilOpcodeInstruction.LoadInt64Constant, instructionOffset, value));
        }

        public void VisitLoadSingleConstant(float value, int instructionOffset)
        {
            this.instructions.Add(new StackInstructionWith<float>(CilOpcodeInstruction.LoadSingleConstant, instructionOffset, value, hexable: false));
        }

        public void VisitLoadDoubleConstant(double value, int instructionOffset)
        {
            this.instructions.Add(new StackInstructionWith<double>(CilOpcodeInstruction.LoadDoubleConstant, instructionOffset, value, hexable: false));
        }

        public void VisitLoadInt32Constant(int value, int instructionOffset)
        {
            this.instructions.Add(new StackInstructionWith<int>(CilOpcodeInstruction.LoadInt32Constant, instructionOffset, value));
        }

        public void VisitBranchIfEqualToShortForm(sbyte relativeInstructionOffset, int instructionOffset)
        {
            this.instructions.Add(new BranchInstruction8(CilOpcodeInstruction.BranchIfEqualToShortForm, instructionOffset, relativeInstructionOffset));
        }

        public void VisitBranchIfFalseShortForm(sbyte relativeInstructionOffset, int instructionOffset)
        {
            this.instructions.Add(new BranchInstruction8(CilOpcodeInstruction.BranchIfFalseShortForm, instructionOffset, relativeInstructionOffset));
        }

        public void VisitBranchIfGreaterThanOrEqualToUnSignedOrOrderedShortForm(sbyte relativeInstructionOffset, int instructionOffset)
        {
            this.instructions.Add(new BranchInstruction8(CilOpcodeInstruction.BranchIfGreaterThanOrEqualToUnSignedOrOrderedShortForm, instructionOffset, relativeInstructionOffset));
        }

        public void VisitBranchIfGreaterThanShortForm(sbyte relativeInstructionOffset, int instructionOffset)
        {
            this.instructions.Add(new BranchInstruction8(CilOpcodeInstruction.BranchIfGreaterThanShortForm, instructionOffset, relativeInstructionOffset));
        }

        public void VisitBranchIfGreaterThanToOrEqualToShortForm(sbyte relativeInstructionOffset, int instructionOffset)
        {
            this.instructions.Add(new BranchInstruction8(CilOpcodeInstruction.BranchIfGreaterThanToOrEqualToShortForm, instructionOffset, relativeInstructionOffset));
        }

        public void VisitBranchIfGreaterThanUnSignedOrOrderedShortForm(sbyte relativeInstructionOffset, int instructionOffset)
        {
            this.instructions.Add(new BranchInstruction8(CilOpcodeInstruction.BranchIfGreaterThanUnSignedOrOrderedShortForm, instructionOffset, relativeInstructionOffset));
        }

        public void VisitBranchIfLessThanOrEqualToUnSignedOrOrderedShortForm(sbyte relativeInstructionOffset, int instructionOffset)
        {
            this.instructions.Add(new BranchInstruction8(CilOpcodeInstruction.BranchIfLessThanOrEqualToUnSignedOrOrderedShortForm, instructionOffset, relativeInstructionOffset));
        }

        public void VisitBranchIfLessThanOrEqualToShortForm(sbyte relativeInstructionOffset, int instructionOffset)
        {
            this.instructions.Add(new BranchInstruction8(CilOpcodeInstruction.BranchIfLessThanOrEqualToShortForm, instructionOffset, relativeInstructionOffset));
        }

        public void VisitBranchIfLessThanShortForm(sbyte relativeInstructionOffset, int instructionOffset)
        {
            this.instructions.Add(new BranchInstruction8(CilOpcodeInstruction.BranchIfLessThanShortForm, instructionOffset, relativeInstructionOffset));
        }

        public void VisitBranchIfLessThanUnSignedOrOrderedShortForm(sbyte relativeInstructionOffset, int instructionOffset)
        {
            this.instructions.Add(new BranchInstruction8(CilOpcodeInstruction.BranchIfLessThanUnSignedOrOrderedShortForm, instructionOffset, relativeInstructionOffset));
        }

        public void VisitBranchIfNotEqualToUnSignedOrOrderedShortForm(sbyte relativeInstructionOffset, int instructionOffset)
        {
            this.instructions.Add(new BranchInstruction8(CilOpcodeInstruction.BranchIfNotEqualToUnSignedOrOrderedShortForm, instructionOffset, relativeInstructionOffset));
        }

        public void VisitBranchIfTrueShortForm(sbyte relativeInstructionOffset, int instructionOffset)
        {
            this.instructions.Add(new BranchInstruction8(CilOpcodeInstruction.BranchIfTrueShortForm, instructionOffset, relativeInstructionOffset));
        }

        public void VisitLeaveProtectedBlockShortForm(sbyte relativeInstructionOffset, int instructionOffset)
        {
            this.instructions.Add(new BranchInstruction8(CilOpcodeInstruction.LeaveProtectedBlockShortForm, instructionOffset, relativeInstructionOffset));
        }

        public void VisitUnconditionalBranchShortForm(sbyte relativeInstructionOffset, int instructionOffset)
        {
            this.instructions.Add(new BranchInstruction8(CilOpcodeInstruction.UnconditionalBranchShortForm, instructionOffset, relativeInstructionOffset));
        }

        public void VisitBranchIfEqualTo(int relativeInstructionOffset, int instructionOffset)
        {
            this.instructions.Add(new BranchInstruction32(CilOpcodeInstruction.BranchIfEqualTo, instructionOffset, relativeInstructionOffset));
        }

        public void VisitBranchIfFalse(int relativeInstructionOffset, int instructionOffset)
        {
            this.instructions.Add(new BranchInstruction32(CilOpcodeInstruction.BranchIfFalse, instructionOffset, relativeInstructionOffset));
        }

        public void VisitBranchIfGreaterThan(int relativeInstructionOffset, int instructionOffset)
        {
            this.instructions.Add(new BranchInstruction32(CilOpcodeInstruction.BranchIfGreaterThan, instructionOffset, relativeInstructionOffset));
        }

        public void VisitBranchIfGreaterThanOrEqualTo(int relativeInstructionOffset, int instructionOffset)
        {
            this.instructions.Add(new BranchInstruction32(CilOpcodeInstruction.BranchIfGreaterThanOrEqualTo, instructionOffset, relativeInstructionOffset));
        }

        public void VisitBranchIfGreaterThanOrEqualToUnSignedOrOrdered(int relativeInstructionOffset, int instructionOffset)
        {
            this.instructions.Add(new BranchInstruction32(CilOpcodeInstruction.BranchIfGreaterThanOrEqualToUnSignedOrOrdered, instructionOffset, relativeInstructionOffset));
        }

        public void VisitBranchIfGreaterThanUnSignedOrOrdered(int relativeInstructionOffset, int instructionOffset)
        {
            this.instructions.Add(new BranchInstruction32(CilOpcodeInstruction.BranchIfGreaterThanUnSignedOrOrdered, instructionOffset, relativeInstructionOffset));
        }

        public void VisitBranchIfLessThan(int relativeInstructionOffset, int instructionOffset)
        {
            this.instructions.Add(new BranchInstruction32(CilOpcodeInstruction.BranchIfLessThan, instructionOffset, relativeInstructionOffset));
        }

        public void VisitBranchIfLessThanOrEqualToUnSignedOrOrdered(int relativeInstructionOffset, int instructionOffset)
        {
            this.instructions.Add(new BranchInstruction32(CilOpcodeInstruction.BranchIfLessThanOrEqualToUnSignedOrOrdered, instructionOffset, relativeInstructionOffset));
        }

        public void VisitBranchIfLessThanOrEqualTo(int relativeInstructionOffset, int instructionOffset)
        {
            this.instructions.Add(new BranchInstruction32(CilOpcodeInstruction.BranchIfLessThanOrEqualTo, instructionOffset, relativeInstructionOffset));
        }

        public void VisitBranchIfLessThanUnSignedOrOrdered(int relativeInstructionOffset, int instructionOffset)
        {
            this.instructions.Add(new BranchInstruction32(CilOpcodeInstruction.BranchIfLessThanUnSignedOrOrdered, instructionOffset, relativeInstructionOffset));
        }

        public void VisitBranchIfNotEqualToUnSignedOrOrdered(int relativeInstructionOffset, int instructionOffset)
        {
            this.instructions.Add(new BranchInstruction32(CilOpcodeInstruction.BranchIfNotEqualToUnSignedOrOrdered, instructionOffset, relativeInstructionOffset));
        }

        public void VisitBranchIfTrue(int relativeInstructionOffset, int instructionOffset)
        {
            this.instructions.Add(new BranchInstruction32(CilOpcodeInstruction.BranchIfTrue, instructionOffset, relativeInstructionOffset));
        }

        public void VisitLeaveProtectedBlock(int relativeInstructionOffset, int instructionOffset)
        {
            this.instructions.Add(new BranchInstruction32(CilOpcodeInstruction.LeaveProtectedBlock, instructionOffset, relativeInstructionOffset));
        }

        public void VisitUnconditionalBranch(int relativeInstructionOffset, int instructionOffset)
        {
            this.instructions.Add(new BranchInstruction32(CilOpcodeInstruction.UnconditionalBranch, instructionOffset, relativeInstructionOffset));
        }

        public void VisitSwitch(int[] targets, int instructionOffset)
        {
            this.instructions.Add(new SwitchInstruction(instructionOffset, targets));
        }

        public void VisitLoadString(uint userStringOffset, int instructionOffset)
        {
            this.instructions.Add(new LoadStringInstruction(userStringOffset, this.metadataRoot.UserStringsHeap, instructionOffset));
        }

        public void VisitAddressCopy(ICliMetadataTableRow targetMetadataRow, int instructionOffset)
        {

        }

        public void VisitBox(ICliMetadataTableRow targetMetadataRow, int instructionOffset)
        {
            var typeRow = targetMetadataRow as ICliMetadataTypeDefOrRefRow;
            if (typeRow != null)
            {

            }
        }

        public void VisitCallMethod(ICliMetadataTableRow targetMetadataRow, int instructionOffset)
        {

        }

        public void VisitCallMethodIndirect(ICliMetadataTableRow targetMetadataRow, int instructionOffset)
        {

        }

        public void VisitCallVirtualMethod(ICliMetadataTableRow targetMetadataRow, int instructionOffset)
        {

        }

        public void VisitCastClass(ICliMetadataTableRow targetMetadataRow, int instructionOffset)
        {

        }

        public void VisitGetTypedReferenceAddress(ICliMetadataTableRow targetMetadataRow, int instructionOffset)
        {

        }

        public void VisitIsInstance(ICliMetadataTableRow targetMetadataRow, int instructionOffset)
        {

        }

        public void VisitJumpTo(ICliMetadataTableRow targetMetadataRow, int instructionOffset)
        {

        }

        public void VisitLoadArrayElementAddress(ICliMetadataTableRow targetMetadataRow, int instructionOffset)
        {

        }

        public void VisitLoadElement(ICliMetadataTableRow targetMetadataRow, int instructionOffset)
        {

        }

        public void VisitLoadField(ICliMetadataTableRow targetMetadataRow, int instructionOffset)
        {

        }

        public void VisitLoadFieldAddress(ICliMetadataTableRow targetMetadataRow, int instructionOffset)
        {

        }

        public void VisitLoadObject(ICliMetadataTableRow targetMetadataRow, int instructionOffset)
        {

        }

        public void VisitLoadStaticField(ICliMetadataTableRow targetMetadataRow, int instructionOffset)
        {

        }

        public void VisitLoadStaticFieldAddress(ICliMetadataTableRow targetMetadataRow, int instructionOffset)
        {

        }

        public void VisitLoadToken(ICliMetadataTableRow targetMetadataRow, int instructionOffset)
        {

        }

        public void VisitMakeTypedReference(ICliMetadataTableRow targetMetadataRow, int instructionOffset)
        {

        }

        public void VisitNewArray(ICliMetadataTableRow targetMetadataRow, int instructionOffset)
        {

        }

        public void VisitNewObject(ICliMetadataTableRow targetMetadataRow, int instructionOffset)
        {

        }

        public void VisitStoreElement(ICliMetadataTableRow targetMetadataRow, int instructionOffset)
        {

        }

        public void VisitStoreField(ICliMetadataTableRow targetMetadataRow, int instructionOffset)
        {

        }

        public void VisitStoreObject(ICliMetadataTableRow targetMetadataRow, int instructionOffset)
        {

        }

        public void VisitStoreStaticField(ICliMetadataTableRow targetMetadataRow, int instructionOffset)
        {

        }

        public void VisitUnbox(ICliMetadataTableRow targetMetadataRow, int instructionOffset)
        {

        }

        public void VisitUnboxAny(ICliMetadataTableRow targetMetadataRow, int instructionOffset)
        {

        }

        public void VisitAllocateLocalMemory(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.AllocateLocalMemory, instructionOffset));
        }

        public void VisitCompareEquality(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.CompareEquality, instructionOffset));
        }

        public void VisitCompareGreaterThan(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.CompareGreaterThan, instructionOffset));
        }

        public void VisitCompareGreaterThanUnSignedOrOrdered(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.CompareGreaterThanUnSignedOrOrdered, instructionOffset));
        }

        public void VisitCompareLessThan(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.CompareLessThan, instructionOffset));
        }

        public void VisitCompareLessThanUnSignedOrOrdered(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.CompareLessThanUnSignedOrOrdered, instructionOffset));
        }

        public void VisitCopyMemoryBlock(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.CopyMemoryBlock, instructionOffset));
        }

        public void VisitEndFilter(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.EndFilter, instructionOffset));
        }

        public void VisitGetArgumentList(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.GetArgumentList, instructionOffset));
        }

        public void VisitInitializeMemoryBlock(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.InitializeMemoryBlock, instructionOffset));
        }

        public void VisitPointerReferenceMayBeUnaligned(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.PointerReferenceMayBeUnaligned, instructionOffset));
        }

        public void VisitTailCallModifier(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.TailCallModifier, instructionOffset));
        }

        public void VisitVolatilePointerReference(int instructionOffset)
        {
            this.instructions.Add(new StackInstruction(CilOpcodeInstruction.VolatilePointerReference, instructionOffset));
        }

        public void VisitLoadArgument(short index, int instructionOffset)
        {
            this.instructions.Add(new StackInstructionWith<short>(CilOpcodeInstruction.LoadArgument, instructionOffset, index));
        }

        public void VisitLoadArgumentAddress(short index, int instructionOffset)
        {
            this.instructions.Add(new StackInstructionWith<short>(CilOpcodeInstruction.LoadArgumentAddress, instructionOffset, index));
        }

        public void VisitLoadLocal(short index, int instructionOffset)
        {
            this.instructions.Add(new StackInstructionWith<short>(CilOpcodeInstruction.LoadLocal, instructionOffset, index));
        }

        public void VisitLoadLocalAddress(short index, int instructionOffset)
        {
            this.instructions.Add(new StackInstructionWith<short>(CilOpcodeInstruction.LoadLocalAddress, instructionOffset, index));
        }

        public void VisitStoreArgument(short index, int instructionOffset)
        {
            this.instructions.Add(new StackInstructionWith<short>(CilOpcodeInstruction.StoreArgument, instructionOffset, index));
        }

        public void VisitStoreLocal(short index, int instructionOffset)
        {
            this.instructions.Add(new StackInstructionWith<short>(CilOpcodeInstruction.StoreLocal, instructionOffset, index));
        }

        public void VisitConstrainedCallVirtModifier(ICliMetadataTableRow targetMetadataRow, int instructionOffset)
        {

        }

        public void VisitInitializeObject(ICliMetadataTableRow targetMetadataRow, int instructionOffset)
        {

        }

        public void VisitLoadMethodPointer(ICliMetadataTableRow targetMetadataRow, int instructionOffset)
        {

        }

        public void VisitLoadVirtualMethodPointer(ICliMetadataTableRow targetMetadataRow, int instructionOffset)
        {

        }
    }
}
