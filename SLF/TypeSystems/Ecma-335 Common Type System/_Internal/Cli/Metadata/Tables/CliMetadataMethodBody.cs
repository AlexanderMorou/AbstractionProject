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

        private class StandardInstruction : IStackInstruction
        {
            public StandardInstruction(CilOpcodeInstruction instruction, int offset) { this.Instruction = instruction; this.Offset = offset; }
            public CilOpcodeInstruction Instruction { get; private set; }
            public override string ToString() { return string.Format("L_{0:X5}: {1}", this.Offset, this.Instruction); }
            public int Offset { get; private set; }
        }

        private class MemberInstruction : StandardInstruction { public MemberInstruction(CilOpcodeInstruction instruction, int offset, ICliMetadataTableRow target) : base(instruction, offset) { this.Target = target; } public ICliMetadataTableRow Target { get; private set; } public override string ToString() { return string.Format("L_{0:X5}: {1} {2}", this.Offset, this.Instruction, this.Target); } }
       
        private class StandardInstructionWithInt32 :
            StandardInstruction
        {
            public StandardInstructionWithInt32(CilOpcodeInstruction instruction, int offset, int value) : base(instruction, offset) { this.Value = value; }
            public int Value { get; private set; }
            public override string ToString() { return string.Format("L_{0:X5}: {1} {2:X}", this.Offset, this.Instruction, this.Value); }
        }
        private class StandardInstructionWithInt8 :
            StandardInstruction
        {
            public StandardInstructionWithInt8(CilOpcodeInstruction instruction, int offset, sbyte value) : base(instruction, offset) { this.Value = value; }
            public sbyte Value { get; private set; }
            public override string ToString() { return string.Format("L_{0:X5}: {1} {2:X}", this.Offset, this.Instruction, this.Value); }
        }

        private class BranchInstruction32 : StandardInstructionWithInt32 { public BranchInstruction32(CilOpcodeInstruction instruction, int offset, int value) : base(instruction, offset, value) {  }
            public override string ToString() { return string.Format("L_{0:X5}: {1} L_{2:X5}", this.Offset, this.Instruction, this.Offset + 5 + this.Value); }
        }
        private class BranchInstruction8 : StandardInstructionWithInt8 { public BranchInstruction8(CilOpcodeInstruction instruction, int offset, sbyte value) : base(instruction, offset, value) {  }
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
                this.header = new CliMetadataMethodHeader(this.metadataRoot, this.rva);
                List<IStackInstruction> instructions = new List<IStackInstruction>();
                byte[] bodyData = this.header.BodyData;
                for (int i = 0; i < bodyData.Length; i++)
                {
                    byte current = bodyData[i];
                    switch ((CilOpcodeInstruction)current)
                    {
                        case CilOpcodeInstruction.NoOperation:
                        case CilOpcodeInstruction.Break:
                        case CilOpcodeInstruction.LoadArg0:
                        case CilOpcodeInstruction.LoadArg1:
                        case CilOpcodeInstruction.LoadArg2:
                        case CilOpcodeInstruction.LoadArg3:
                        case CilOpcodeInstruction.LoadLocal0:
                        case CilOpcodeInstruction.LoadLocal1:
                        case CilOpcodeInstruction.LoadLocal2:
                        case CilOpcodeInstruction.LoadLocal3:
                        case CilOpcodeInstruction.StoreLocal0:
                        case CilOpcodeInstruction.StoreLocal1:
                        case CilOpcodeInstruction.StoreLocal2:
                        case CilOpcodeInstruction.StoreLocal3:
                        case CilOpcodeInstruction.DuplicateStackItem:
                        case CilOpcodeInstruction.PopStackItem:
                        case CilOpcodeInstruction.Add:
                        case CilOpcodeInstruction.Subtract:
                        case CilOpcodeInstruction.Multiply:
                        case CilOpcodeInstruction.Divide:
                        case CilOpcodeInstruction.DivideUnSignedOrOrdered:
                        case CilOpcodeInstruction.Remainder:
                        case CilOpcodeInstruction.RemainderUnSignedOrOrdered:
                        case CilOpcodeInstruction.BitwiseAnd:
                        case CilOpcodeInstruction.BitwiseOr:
                        case CilOpcodeInstruction.BitwiseExclusiveOr:
                        case CilOpcodeInstruction.ShiftBitsLeft:
                        case CilOpcodeInstruction.ShiftBitsRight:
                        case CilOpcodeInstruction.ShiftBitsRightUnSignedOrOrdered:
                        case CilOpcodeInstruction.Negate:
                        case CilOpcodeInstruction.BitwiseComplement:
                        case CilOpcodeInstruction.UncheckedConvertToSByte:
                        case CilOpcodeInstruction.UncheckedConvertToInt16:
                        case CilOpcodeInstruction.UncheckedConvertToInt32:
                        case CilOpcodeInstruction.UncheckedConvertToInt64:
                        case CilOpcodeInstruction.UncheckedConvertToSingle:
                        case CilOpcodeInstruction.UncheckedConvertToDouble:
                        case CilOpcodeInstruction.UncheckedConvertToUInt32:
                        case CilOpcodeInstruction.UncheckedConvertToUInt64:
                        case CilOpcodeInstruction.LoadArrayElementAsSByte:
                        case CilOpcodeInstruction.LoadArrayElementAsByte:
                        case CilOpcodeInstruction.LoadArrayElementAsInt16:
                        case CilOpcodeInstruction.LoadArrayElementAsUInt16:
                        case CilOpcodeInstruction.LoadArrayElementAsInt32:
                        case CilOpcodeInstruction.LoadArrayElementAsUInt32:
                        case CilOpcodeInstruction.LoadArrayElementAsInt64:
                        case CilOpcodeInstruction.LoadArrayElementAsNativeInteger:
                        case CilOpcodeInstruction.LoadArrayElementAsSingle:
                        case CilOpcodeInstruction.LoadArrayElementAsDouble:
                        case CilOpcodeInstruction.LoadArrayElementReference:
                        case CilOpcodeInstruction.StoreNativeIntegerArrayElement:
                        case CilOpcodeInstruction.StoreSByteArrayElement:
                        case CilOpcodeInstruction.StoreInt16ArrayElement:
                        case CilOpcodeInstruction.StoreInt32ArrayElement:
                        case CilOpcodeInstruction.StoreInt64ArrayElement:
                        case CilOpcodeInstruction.StoreSingleArrayElement:
                        case CilOpcodeInstruction.StoreDoubleArrayElement:
                        case CilOpcodeInstruction.StoreReferenceArrayElement:
                        case CilOpcodeInstruction.CheckedConvertToSByteAsInt32:
                        case CilOpcodeInstruction.CheckedConvertToByteAsInt32:
                        case CilOpcodeInstruction.CheckedConvertToInt16AsInt32:
                        case CilOpcodeInstruction.CheckedConvertToUInt16AsInt32:
                        case CilOpcodeInstruction.CheckedConvertToInt32AsInt32:
                        case CilOpcodeInstruction.CheckedConvertToUInt32AsInt32:
                        case CilOpcodeInstruction.CheckedConvertToInt64:
                        case CilOpcodeInstruction.CheckedConvertToUInt64AsInt64:
                        case CilOpcodeInstruction.Return:
                        case CilOpcodeInstruction.LoadInt32ConstantOfMinusOne:
                        case CilOpcodeInstruction.LoadInt32ConstantOfZero:
                        case CilOpcodeInstruction.LoadInt32ConstantOfOne:
                        case CilOpcodeInstruction.LoadInt32ConstantOfTwo:
                        case CilOpcodeInstruction.LoadInt32ConstantOfThree:
                        case CilOpcodeInstruction.LoadInt32ConstantOfFour:
                        case CilOpcodeInstruction.LoadInt32ConstantOfFive:
                        case CilOpcodeInstruction.LoadInt32ConstantOfSix:
                        case CilOpcodeInstruction.LoadInt32ConstantOfSeven:
                        case CilOpcodeInstruction.LoadInt32ConstantOfEight:
                        case CilOpcodeInstruction.LoadNullValue:
                        case CilOpcodeInstruction.ThrowException:
                        case CilOpcodeInstruction.ConvertUnsignedIntToFloatingPoint:
                        case CilOpcodeInstruction.CheckedConvertUnsignedToSByteAsInt32:
                        case CilOpcodeInstruction.CheckedConvertUnsignedToInt16AsInt32:
                        case CilOpcodeInstruction.CheckedConvertUnsignedToInt32AsInt32:
                        case CilOpcodeInstruction.CheckedConvertUnsignedToInt64:
                        case CilOpcodeInstruction.CheckedConvertUnsignedToByteAsInt32:
                        case CilOpcodeInstruction.CheckedConvertUnsignedToUInt16AsInt32:
                        case CilOpcodeInstruction.CheckedConvertUnsignedToUInt32AsInt32:
                        case CilOpcodeInstruction.CheckedConvertUnsignedToUInt64AsInt64:
                        case CilOpcodeInstruction.CheckedConvertUnsignedToNativeInt:
                        case CilOpcodeInstruction.CheckedConvertUnsignedToNativeUInt:
                        case CilOpcodeInstruction.UncheckedConvertToUInt16:
                        case CilOpcodeInstruction.UncheckedConvertToByte:
                        case CilOpcodeInstruction.UncheckedConvertToNativeInteger:
                        case CilOpcodeInstruction.CheckedConvertToNativeInteger:
                        case CilOpcodeInstruction.CheckedConvertToUnsignedNativeInteger:
                        case CilOpcodeInstruction.CheckedAdd:
                        case CilOpcodeInstruction.CheckedAddUnSignedOrOrdered:
                        case CilOpcodeInstruction.CheckedMultiply:
                        case CilOpcodeInstruction.CheckedMultiplyUnSignedOrOrdered:
                        case CilOpcodeInstruction.CheckedSubtract:
                        case CilOpcodeInstruction.CheckedSubtractUnSignedOrOrdered:
                        case CilOpcodeInstruction.UncheckedConvertToUnsignedNativeInteger:
                            instructions.Add(new StandardInstruction((CilOpcodeInstruction)current, i));
                            break;
                        case CilOpcodeInstruction.LoadArgumentShortForm:
                            break;
                        case CilOpcodeInstruction.LoadArgumentAddressShortForm:
                            break;
                        case CilOpcodeInstruction.StoreArgumentShortForm:
                            break;
                        case CilOpcodeInstruction.LoadLocalShortForm:
                            break;
                        case CilOpcodeInstruction.LoadLocalAddressShortForm:
                            break;
                        case CilOpcodeInstruction.StoreLocalShortForm:
                            break;
                        case CilOpcodeInstruction.LoadInt32ConstantShortForm:
                            break;
                        case CilOpcodeInstruction.LoadInt64Constant:
                            break;
                        case CilOpcodeInstruction.LoadSingleConstant:
                            break;
                        case CilOpcodeInstruction.LoadDoubleConstant:
                            break;
                        case CilOpcodeInstruction.JumpTo:
                            break;
                        case CilOpcodeInstruction.UnconditionalBranchShortForm:
                        case CilOpcodeInstruction.BranchIfFalseShortForm:
                        case CilOpcodeInstruction.BranchIfTrueShortForm:
                        case CilOpcodeInstruction.BranchIfEqualToShortForm:
                        case CilOpcodeInstruction.BranchIfGreaterThanToOrEqualToShortForm:
                        case CilOpcodeInstruction.BranchIfGreaterThanShortForm:
                        case CilOpcodeInstruction.BranchIfLessThanOrEqualToShortForm:
                        case CilOpcodeInstruction.BranchIfLessThanShortForm:
                        case CilOpcodeInstruction.BranchIfNotEqualToUnSignedOrOrderedShortForm:
                        case CilOpcodeInstruction.BranchIfGreaterThanOrEqualToUnSignedOrOrderedShortForm:
                        case CilOpcodeInstruction.BranchIfGreaterThanUnSignedOrOrderedShortForm:
                        case CilOpcodeInstruction.BranchIfLessThanOrEqualToUnSignedOrOrderedShortForm:
                        case CilOpcodeInstruction.BranchIfLessThanUnSignedOrOrderedShortForm:
                        case CilOpcodeInstruction.LeaveProtectedBlockShortForm:
                            instructions.Add(new BranchInstruction8((CilOpcodeInstruction)current, i, (sbyte)bodyData[++i]));
                            break;
                        case CilOpcodeInstruction.LoadInt32Constant:
                        case CilOpcodeInstruction.UnconditionalBranch:
                        case CilOpcodeInstruction.BranchIfFalse:
                        case CilOpcodeInstruction.BranchIfTrue:
                        case CilOpcodeInstruction.BranchIfEqualTo:
                        case CilOpcodeInstruction.BranchIfGreaterThanOrEqualTo:
                        case CilOpcodeInstruction.BranchIfGreaterThan:
                        case CilOpcodeInstruction.BranchIfLessThanOrEqualTo:
                        case CilOpcodeInstruction.BranchIfLessThan:
                        case CilOpcodeInstruction.BranchIfNotEqualToUnSignedOrOrdered:
                        case CilOpcodeInstruction.BranchIfGreaterThanOrEqualToUnSignedOrOrdered:
                        case CilOpcodeInstruction.BranchIfGreaterThanUnSignedOrOrdered:
                        case CilOpcodeInstruction.BranchIfLessThanOrEqualToUnSignedOrOrdered:
                        case CilOpcodeInstruction.BranchIfLessThanUnSignedOrOrdered:
                        case CilOpcodeInstruction.LeaveProtectedBlock:
                            instructions.Add(new BranchInstruction32((CilOpcodeInstruction)current, i, bodyData[++i] | bodyData[++i] << 8 | bodyData[++i] << 16 | bodyData[++i] << 24));
                            break;
                        case CilOpcodeInstruction.Switch:
                            break;
                        case CilOpcodeInstruction.LoadIndirectSByte:
                            break;
                        case CilOpcodeInstruction.LoadIndirectByte:
                            break;
                        case CilOpcodeInstruction.LoadIndirectInt16:
                            break;
                        case CilOpcodeInstruction.LoadIndirectUInt16:
                            break;
                        case CilOpcodeInstruction.LoadIndirectInt32:
                            break;
                        case CilOpcodeInstruction.LoadIndirectUInt32:
                            break;
                        case CilOpcodeInstruction.LoadIndirectInt64OrUInt64:
                            break;
                        case CilOpcodeInstruction.LoadIndirectNativeInteger:
                            break;
                        case CilOpcodeInstruction.LoadIndirectSingle:
                            break;
                        case CilOpcodeInstruction.LoadIndirectDouble:
                            break;
                        case CilOpcodeInstruction.LoadIndirectReference:
                            break;
                        case CilOpcodeInstruction.StoreIndirectReference:
                            break;
                        case CilOpcodeInstruction.StoreIndirectSByte:
                            break;
                        case CilOpcodeInstruction.StoreIndirectInt16:
                            break;
                        case CilOpcodeInstruction.SotreIndirectInt32:
                            break;
                        case CilOpcodeInstruction.StoreIndirectInt64:
                            break;
                        case CilOpcodeInstruction.StoreIndirectSingle:
                            break;
                        case CilOpcodeInstruction.StoreIndirectDouble:
                            break;
                        case CilOpcodeInstruction.AddressCopy:
                            break;
                        case CilOpcodeInstruction.LoadObject:
                            break;
                        case CilOpcodeInstruction.LoadString:
                            break;
                        case CilOpcodeInstruction.NewObject:
                        case CilOpcodeInstruction.IsInstance:
                        case CilOpcodeInstruction.CastClass:
                        case CilOpcodeInstruction.Unbox:
                        case CilOpcodeInstruction.CallMethod:
                        case CilOpcodeInstruction.CallMethodIndirect:
                        case CilOpcodeInstruction.CallVirtualMethod:
                        case CilOpcodeInstruction.LoadField:
                        case CilOpcodeInstruction.LoadFieldAddress:
                        case CilOpcodeInstruction.StoreField:
                        case CilOpcodeInstruction.LoadStaticField:
                        case CilOpcodeInstruction.LoadStaticFieldAddress:
                        case CilOpcodeInstruction.StoreStaticField:
                        case CilOpcodeInstruction.Box:
                        case CilOpcodeInstruction.NewArray:
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
                        case CilOpcodeInstruction.StoreObject:
                            break;
                        case CilOpcodeInstruction.LoadArrayLength:
                            break;
                        case CilOpcodeInstruction.LoadArrayElementAddress:
                            break;
                        case CilOpcodeInstruction.LoadElement:
                            break;
                        case CilOpcodeInstruction.StoreElement:
                            break;
                        case CilOpcodeInstruction.GetTypedReferenceAddress:
                            break;
                        case CilOpcodeInstruction.CheckForFiniteNumber:
                            break;
                        case CilOpcodeInstruction.MakeTypedReference:
                            break;
                        case CilOpcodeInstruction.LoadToken:
                            break;
                        case CilOpcodeInstruction.EndFinallyBlock:
                            break;
                        case CilOpcodeInstruction.StoreIndirectNativeInteger:
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
                                    case CilOpcodeInstruction.GetArgumentList:
                                        break;
                                    case CilOpcodeInstruction.CompareEquality:
                                    case CilOpcodeInstruction.CompareGreaterThan:
                                    case CilOpcodeInstruction.CompareGreaterThanUnSignedOrOrdered:
                                    case CilOpcodeInstruction.CompareLessThan:
                                    case CilOpcodeInstruction.CompareLessThanUnSignedOrOrdered:
                                        instructions.Add(new StandardInstruction((CilOpcodeInstruction)nextShort, i - 1));
                                        break;
                                    case CilOpcodeInstruction.LoadArgument:
                                        break;
                                    case CilOpcodeInstruction.LoadArgumentAddress:
                                        break;
                                    case CilOpcodeInstruction.StoreArgument:
                                        break;
                                    case CilOpcodeInstruction.LoadLocal:
                                        break;
                                    case CilOpcodeInstruction.LoadLocalAddress:
                                        break;
                                    case CilOpcodeInstruction.StoreLocal:
                                        break;
                                    case CilOpcodeInstruction.AllocateLocalMemory:
                                        break;
                                    case CilOpcodeInstruction.EndFilter:
                                        break;
                                    case CilOpcodeInstruction.PointerReferenceMayBeUnaligned:
                                        break;
                                    case CilOpcodeInstruction.VolatilePointerReference:
                                        break;
                                    case CilOpcodeInstruction.TailCallModifier:
                                        break;
                                    case CilOpcodeInstruction.LoadMethodPointer:
                                    case CilOpcodeInstruction.LoadVirtualMethodPointer:
                                    case CilOpcodeInstruction.InitializeObject:
                                        {
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
                                    case CilOpcodeInstruction.ConstrainedCallVirtModifier:
                                        break;
                                    case CilOpcodeInstruction.CopyMemoryBlock:
                                        break;
                                    case CilOpcodeInstruction.InitializeMemoryBlock:
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
                foreach (var inst in instructions)
                    Console.WriteLine(inst);
                this.initialized = true;
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
