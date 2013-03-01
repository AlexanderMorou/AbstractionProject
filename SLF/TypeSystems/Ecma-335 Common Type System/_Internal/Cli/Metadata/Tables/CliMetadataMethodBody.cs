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

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.Tables
{
    internal class CliMetadataMethodBody :
        ICliMetadataMethodBody
    {
        private interface IStackInstruction { CilOpcodeInstruction Instruction { get; } }
        private List<IStackInstruction> instructions;
        private class StandardInstruction : IStackInstruction
        {
            public StandardInstruction(CilOpcodeInstruction instruction, int offset) { this.Instruction = instruction; this.Offset = offset; }
            public CilOpcodeInstruction Instruction { get; private set; }
            public override string ToString() { return string.Format("L_{0:X5}: {1}", this.Offset, this.Instruction); }
            public int Offset { get; private set; }
        }

        private class MemberInstruction : StandardInstruction { public MemberInstruction(CilOpcodeInstruction instruction, int offset, ICliMetadataTableRow target) : base(instruction, offset) { this.Target = target; } public ICliMetadataTableRow Target { get; private set; } public override string ToString() { return string.Format("L_{0:X5}: {1} {2}", this.Offset, this.Instruction, this.Target); } }
        private class StandardInstructionWith<T> :
            StandardInstruction
            where T :
                struct
        {
            public StandardInstructionWith(CilOpcodeInstruction instruction, int offset, T value) : base(instruction, offset) { this.Value = value; }
            public T Value { get; private set; }
            public override string ToString() { return string.Format("L_{0:X5}: {1} 0x{2:X}", this.Offset, this.Instruction, this.Value); }
        }

        private class LoadStringInstruction : StandardInstruction
        {
            private ICliMetadataUserStringsHeaderAndHeap userStrings;
            public LoadStringInstruction(uint userStringIndex, ICliMetadataUserStringsHeaderAndHeap userStrings, int offset)
                : base(CilOpcodeInstruction.LoadString, offset)
            {
                this.userStrings = userStrings;
                this.UserStringIndex = userStringIndex;
            }
            public uint UserStringIndex { get; private set; }
            public override string ToString() { return string.Format("L_{0:X5}: {1} \"0x{2:X}\"", this.Offset, this.Instruction, this.userStrings[this.UserStringIndex]); }
        }


        private class SwitchInstruction : StandardInstruction
        {
            public SwitchInstruction(int offset, int[] targets) : base(CilOpcodeInstruction.Switch, offset) { this.Targets = targets; }
            public int[] Targets { get; private set; }
            public int Length { get { return (sizeof(int) * (Targets.Length + 1)) + 1; } }
            public override string ToString() { return string.Format("L_{0:X5}: {1} {{ {2} }}", this.Offset, this.Instruction, string.Join(", ", (from t in this.Targets select string.Format("L_{0:X5}", this.Offset + this.Length + t)).ToArray())); }
        }

        private class BranchInstruction32 : StandardInstructionWith<int>
        {
            public BranchInstruction32(CilOpcodeInstruction instruction, int offset, int value) : base(instruction, offset, value) { }
            public override string ToString() { return string.Format("L_{0:X5}: {1} L_{2:X5}", this.Offset, this.Instruction, this.Offset + 5 + this.Value); }
        }
        private class BranchInstruction8 : StandardInstructionWith<sbyte>
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

        private void BuildBody(byte[] bodyData)
        {
            List<IStackInstruction> instructions = new List<IStackInstruction>();
            for (int i = 0; i < bodyData.Length; i++)
            {
                byte current = bodyData[i];
                switch ((CilOpcodeInstruction)current)
                {
                    case CilOpcodeInstruction.Add:
                    case CilOpcodeInstruction.BitwiseAnd:
                    case CilOpcodeInstruction.BitwiseComplement:
                    case CilOpcodeInstruction.BitwiseExclusiveOr:
                    case CilOpcodeInstruction.BitwiseOr:
                    case CilOpcodeInstruction.Break:
                    case CilOpcodeInstruction.CheckedAdd:
                    case CilOpcodeInstruction.CheckedAddUnSignedOrOrdered:
                    case CilOpcodeInstruction.CheckedConvertToByteAsInt32:
                    case CilOpcodeInstruction.CheckedConvertToInt16AsInt32:
                    case CilOpcodeInstruction.CheckedConvertToInt32AsInt32:
                    case CilOpcodeInstruction.CheckedConvertToInt64:
                    case CilOpcodeInstruction.CheckedConvertToNativeInteger:
                    case CilOpcodeInstruction.CheckedConvertToSByteAsInt32:
                    case CilOpcodeInstruction.CheckedConvertToUInt16AsInt32:
                    case CilOpcodeInstruction.CheckedConvertToUInt32AsInt32:
                    case CilOpcodeInstruction.CheckedConvertToUInt64AsInt64:
                    case CilOpcodeInstruction.CheckedConvertToUnsignedNativeInteger:
                    case CilOpcodeInstruction.CheckedConvertUnsignedToByteAsInt32:
                    case CilOpcodeInstruction.CheckedConvertUnsignedToInt16AsInt32:
                    case CilOpcodeInstruction.CheckedConvertUnsignedToInt32AsInt32:
                    case CilOpcodeInstruction.CheckedConvertUnsignedToInt64:
                    case CilOpcodeInstruction.CheckedConvertUnsignedToNativeInt:
                    case CilOpcodeInstruction.CheckedConvertUnsignedToNativeUInt:
                    case CilOpcodeInstruction.CheckedConvertUnsignedToSByteAsInt32:
                    case CilOpcodeInstruction.CheckedConvertUnsignedToUInt16AsInt32:
                    case CilOpcodeInstruction.CheckedConvertUnsignedToUInt32AsInt32:
                    case CilOpcodeInstruction.CheckedConvertUnsignedToUInt64AsInt64:
                    case CilOpcodeInstruction.CheckedMultiply:
                    case CilOpcodeInstruction.CheckedMultiplyUnSignedOrOrdered:
                    case CilOpcodeInstruction.CheckedSubtract:
                    case CilOpcodeInstruction.CheckedSubtractUnSignedOrOrdered:
                    case CilOpcodeInstruction.CheckForFiniteNumber:
                    case CilOpcodeInstruction.ConvertUnsignedIntToFloatingPoint:
                    case CilOpcodeInstruction.Divide:
                    case CilOpcodeInstruction.DivideUnSignedOrOrdered:
                    case CilOpcodeInstruction.DuplicateStackItem:
                    case CilOpcodeInstruction.EndFinallyBlock:
                    case CilOpcodeInstruction.LoadArg0:
                    case CilOpcodeInstruction.LoadArg1:
                    case CilOpcodeInstruction.LoadArg2:
                    case CilOpcodeInstruction.LoadArg3:
                    case CilOpcodeInstruction.LoadArrayElementAsByte:
                    case CilOpcodeInstruction.LoadArrayElementAsDouble:
                    case CilOpcodeInstruction.LoadArrayElementAsInt16:
                    case CilOpcodeInstruction.LoadArrayElementAsInt32:
                    case CilOpcodeInstruction.LoadArrayElementAsInt64:
                    case CilOpcodeInstruction.LoadArrayElementAsNativeInteger:
                    case CilOpcodeInstruction.LoadArrayElementAsSByte:
                    case CilOpcodeInstruction.LoadArrayElementAsSingle:
                    case CilOpcodeInstruction.LoadArrayElementAsUInt16:
                    case CilOpcodeInstruction.LoadArrayElementAsUInt32:
                    case CilOpcodeInstruction.LoadArrayElementReference:
                    case CilOpcodeInstruction.LoadArrayLength:
                    case CilOpcodeInstruction.LoadIndirectByte:
                    case CilOpcodeInstruction.LoadIndirectDouble:
                    case CilOpcodeInstruction.LoadIndirectInt16:
                    case CilOpcodeInstruction.LoadIndirectInt32:
                    case CilOpcodeInstruction.LoadIndirectInt64OrUInt64:
                    case CilOpcodeInstruction.LoadIndirectNativeInteger:
                    case CilOpcodeInstruction.LoadIndirectReference:
                    case CilOpcodeInstruction.LoadIndirectSByte:
                    case CilOpcodeInstruction.LoadIndirectSingle:
                    case CilOpcodeInstruction.LoadIndirectUInt16:
                    case CilOpcodeInstruction.LoadIndirectUInt32:
                    case CilOpcodeInstruction.LoadInt32ConstantOfEight:
                    case CilOpcodeInstruction.LoadInt32ConstantOfFive:
                    case CilOpcodeInstruction.LoadInt32ConstantOfFour:
                    case CilOpcodeInstruction.LoadInt32ConstantOfMinusOne:
                    case CilOpcodeInstruction.LoadInt32ConstantOfOne:
                    case CilOpcodeInstruction.LoadInt32ConstantOfSeven:
                    case CilOpcodeInstruction.LoadInt32ConstantOfSix:
                    case CilOpcodeInstruction.LoadInt32ConstantOfThree:
                    case CilOpcodeInstruction.LoadInt32ConstantOfTwo:
                    case CilOpcodeInstruction.LoadInt32ConstantOfZero:
                    case CilOpcodeInstruction.LoadLocal0:
                    case CilOpcodeInstruction.LoadLocal1:
                    case CilOpcodeInstruction.LoadLocal2:
                    case CilOpcodeInstruction.LoadLocal3:
                    case CilOpcodeInstruction.LoadNullValue:
                    case CilOpcodeInstruction.Multiply:
                    case CilOpcodeInstruction.Negate:
                    case CilOpcodeInstruction.NoOperation:
                    case CilOpcodeInstruction.PopStackItem:
                    case CilOpcodeInstruction.Remainder:
                    case CilOpcodeInstruction.RemainderUnSignedOrOrdered:
                    case CilOpcodeInstruction.Return:
                    case CilOpcodeInstruction.ShiftBitsLeft:
                    case CilOpcodeInstruction.ShiftBitsRight:
                    case CilOpcodeInstruction.ShiftBitsRightUnSignedOrOrdered:
                    case CilOpcodeInstruction.SotreIndirectInt32:
                    case CilOpcodeInstruction.StoreDoubleArrayElement:
                    case CilOpcodeInstruction.StoreIndirectDouble:
                    case CilOpcodeInstruction.StoreIndirectInt16:
                    case CilOpcodeInstruction.StoreIndirectInt64:
                    case CilOpcodeInstruction.StoreIndirectNativeInteger:
                    case CilOpcodeInstruction.StoreIndirectReference:
                    case CilOpcodeInstruction.StoreIndirectSByte:
                    case CilOpcodeInstruction.StoreIndirectSingle:
                    case CilOpcodeInstruction.StoreInt16ArrayElement:
                    case CilOpcodeInstruction.StoreInt32ArrayElement:
                    case CilOpcodeInstruction.StoreInt64ArrayElement:
                    case CilOpcodeInstruction.StoreLocal0:
                    case CilOpcodeInstruction.StoreLocal1:
                    case CilOpcodeInstruction.StoreLocal2:
                    case CilOpcodeInstruction.StoreLocal3:
                    case CilOpcodeInstruction.StoreNativeIntegerArrayElement:
                    case CilOpcodeInstruction.StoreReferenceArrayElement:
                    case CilOpcodeInstruction.StoreSByteArrayElement:
                    case CilOpcodeInstruction.StoreSingleArrayElement:
                    case CilOpcodeInstruction.Subtract:
                    case CilOpcodeInstruction.ThrowException:
                    case CilOpcodeInstruction.UncheckedConvertToByte:
                    case CilOpcodeInstruction.UncheckedConvertToDouble:
                    case CilOpcodeInstruction.UncheckedConvertToInt16:
                    case CilOpcodeInstruction.UncheckedConvertToInt32:
                    case CilOpcodeInstruction.UncheckedConvertToInt64:
                    case CilOpcodeInstruction.UncheckedConvertToNativeInteger:
                    case CilOpcodeInstruction.UncheckedConvertToSByte:
                    case CilOpcodeInstruction.UncheckedConvertToSingle:
                    case CilOpcodeInstruction.UncheckedConvertToUInt16:
                    case CilOpcodeInstruction.UncheckedConvertToUInt32:
                    case CilOpcodeInstruction.UncheckedConvertToUInt64:
                    case CilOpcodeInstruction.UncheckedConvertToUnsignedNativeInteger:
                        instructions.Add(new StandardInstruction((CilOpcodeInstruction)current, i));
                        break;
                    case CilOpcodeInstruction.LoadArgumentShortForm:
                    case CilOpcodeInstruction.LoadInt32ConstantShortForm:
                        instructions.Add(new StandardInstructionWith<sbyte>((CilOpcodeInstruction)current, i, (sbyte)bodyData[++i]));
                        break;
                    case CilOpcodeInstruction.LoadLocalAddressShortForm:
                    case CilOpcodeInstruction.LoadLocalShortForm:
                    case CilOpcodeInstruction.LoadArgumentAddressShortForm:
                    case CilOpcodeInstruction.StoreLocalShortForm:
                    case CilOpcodeInstruction.StoreArgumentShortForm:
                        instructions.Add(new StandardInstructionWith<byte>((CilOpcodeInstruction)current, i, bodyData[++i]));
                        break;
                    case CilOpcodeInstruction.LoadInt64Constant:
                        {
                            int offset = i;
                            uint lodword = (uint)(bodyData[++i] | bodyData[++i] << 8 | bodyData[++i] << 16 | bodyData[++i] << 24);
                            uint hidword = (uint)(bodyData[++i] | bodyData[++i] << 8 | bodyData[++i] << 16 | bodyData[++i] << 24);
                            long value = ((long)hidword << 32) | (long)lodword;
                            instructions.Add(new StandardInstructionWith<long>((CilOpcodeInstruction)current, offset, value));
                            break;
                        }
                    case CilOpcodeInstruction.LoadSingleConstant:
                        instructions.Add(new StandardInstructionWith<float>((CilOpcodeInstruction)current, i, BitConverter.ToSingle(new byte[] { bodyData[++i], bodyData[++i], bodyData[++i], bodyData[++i] }, 0)));
                        break;
                    case CilOpcodeInstruction.LoadDoubleConstant:
                        instructions.Add(new StandardInstructionWith<double>((CilOpcodeInstruction)current, i, BitConverter.ToDouble(new byte[] { bodyData[++i], bodyData[++i], bodyData[++i], bodyData[++i], bodyData[++i], bodyData[++i], bodyData[++i], bodyData[++i] }, 0)));
                        break;
                    case CilOpcodeInstruction.LoadInt32Constant:
                        instructions.Add(new StandardInstructionWith<int>((CilOpcodeInstruction)current, i, bodyData[++i] | bodyData[++i] << 8 | bodyData[++i] << 16 | bodyData[++i] << 24));
                        break;
                    case CilOpcodeInstruction.BranchIfEqualToShortForm:
                    case CilOpcodeInstruction.BranchIfFalseShortForm:
                    case CilOpcodeInstruction.BranchIfGreaterThanOrEqualToUnSignedOrOrderedShortForm:
                    case CilOpcodeInstruction.BranchIfGreaterThanShortForm:
                    case CilOpcodeInstruction.BranchIfGreaterThanToOrEqualToShortForm:
                    case CilOpcodeInstruction.BranchIfGreaterThanUnSignedOrOrderedShortForm:
                    case CilOpcodeInstruction.BranchIfLessThanOrEqualToUnSignedOrOrderedShortForm:
                    case CilOpcodeInstruction.BranchIfLessThanOrEqualToShortForm:
                    case CilOpcodeInstruction.BranchIfLessThanShortForm:
                    case CilOpcodeInstruction.BranchIfLessThanUnSignedOrOrderedShortForm:
                    case CilOpcodeInstruction.BranchIfNotEqualToUnSignedOrOrderedShortForm:
                    case CilOpcodeInstruction.BranchIfTrueShortForm:
                    case CilOpcodeInstruction.LeaveProtectedBlockShortForm:
                    case CilOpcodeInstruction.UnconditionalBranchShortForm:
                        instructions.Add(new BranchInstruction8((CilOpcodeInstruction)current, i, (sbyte)bodyData[++i]));
                        break;
                    case CilOpcodeInstruction.BranchIfEqualTo:
                    case CilOpcodeInstruction.BranchIfFalse:
                    case CilOpcodeInstruction.BranchIfGreaterThan:
                    case CilOpcodeInstruction.BranchIfGreaterThanOrEqualTo:
                    case CilOpcodeInstruction.BranchIfGreaterThanOrEqualToUnSignedOrOrdered:
                    case CilOpcodeInstruction.BranchIfGreaterThanUnSignedOrOrdered:
                    case CilOpcodeInstruction.BranchIfLessThan:
                    case CilOpcodeInstruction.BranchIfLessThanOrEqualToUnSignedOrOrdered:
                    case CilOpcodeInstruction.BranchIfLessThanOrEqualTo:
                    case CilOpcodeInstruction.BranchIfLessThanUnSignedOrOrdered:
                    case CilOpcodeInstruction.BranchIfNotEqualToUnSignedOrOrdered:
                    case CilOpcodeInstruction.BranchIfTrue:
                    case CilOpcodeInstruction.LeaveProtectedBlock:
                    case CilOpcodeInstruction.UnconditionalBranch:
                        instructions.Add(new BranchInstruction32((CilOpcodeInstruction)current, i, bodyData[++i] | bodyData[++i] << 8 | bodyData[++i] << 16 | bodyData[++i] << 24));
                        break;
                    case CilOpcodeInstruction.Switch:
                        {
                            int offset = i;
                            uint numTargets = (uint)(bodyData[++i] | bodyData[++i] << 8 | bodyData[++i] << 16 | bodyData[++i] << 24);
                            int[] targets = new int[numTargets];
                            for (int k = 0; k < numTargets; k++)
                                targets[k] = bodyData[++i] | bodyData[++i] << 8 | bodyData[++i] << 16 | bodyData[++i] << 24;
                            instructions.Add(new SwitchInstruction(offset, targets));
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
                            instructions.Add(new LoadStringInstruction(++decodedIndex, this.metadataRoot.UserStringsHeap, offset));
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
                                if (index < 0 ||
                                    index > table.Count + 1)
                                    instructions.Add(new MemberInstruction((CilOpcodeInstruction)current, offset, null));
                                else
                                    instructions.Add(new MemberInstruction((CilOpcodeInstruction)current, offset, (ICliMetadataTableRow)table[index]));
                            }
                            else
                                instructions.Add(new MemberInstruction((CilOpcodeInstruction)current, offset, null));
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
                                case CilOpcodeInstruction.CompareEquality:
                                case CilOpcodeInstruction.CompareGreaterThan:
                                case CilOpcodeInstruction.CompareGreaterThanUnSignedOrOrdered:
                                case CilOpcodeInstruction.CompareLessThan:
                                case CilOpcodeInstruction.CompareLessThanUnSignedOrOrdered:
                                case CilOpcodeInstruction.CopyMemoryBlock:
                                case CilOpcodeInstruction.EndFilter:
                                case CilOpcodeInstruction.GetArgumentList:
                                case CilOpcodeInstruction.InitializeMemoryBlock:
                                case CilOpcodeInstruction.PointerReferenceMayBeUnaligned:
                                case CilOpcodeInstruction.TailCallModifier:
                                case CilOpcodeInstruction.VolatilePointerReference:
                                    instructions.Add(new StandardInstruction((CilOpcodeInstruction)nextShort, offset));
                                    break;
                                case CilOpcodeInstruction.LoadArgument:
                                case CilOpcodeInstruction.LoadArgumentAddress:
                                case CilOpcodeInstruction.LoadLocal:
                                case CilOpcodeInstruction.LoadLocalAddress:
                                case CilOpcodeInstruction.StoreArgument:
                                case CilOpcodeInstruction.StoreLocal:
                                    instructions.Add(new StandardInstructionWith<short>((CilOpcodeInstruction)nextShort, offset, (short)(bodyData[++i] | bodyData[++i] << 8)));
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
                                                instructions.Add(new MemberInstruction((CilOpcodeInstruction)current, offset, null));
                                            else
                                                instructions.Add(new MemberInstruction((CilOpcodeInstruction)current, offset, (ICliMetadataTableRow)table[index]));
                                        }
                                        else
                                            instructions.Add(new MemberInstruction((CilOpcodeInstruction)current, offset, null));
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
            this.instructions = instructions;
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
