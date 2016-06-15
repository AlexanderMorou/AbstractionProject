using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Cli.Metadata
{
    [DebuggerDisplay("{AllenCopeland.Abstraction.Slf.Cli.CliGateway.GetInstructionText(this),nq}")]
    public enum CilOpcodeInstruction :
        ushort
    {
        /// <summary>
        /// A Common Intermediate Language Instruction which represents a 
        /// break in processing for debuggers to step in.
        /// </summary>
        NoOperation = 0x00, // nop
        /// <summary>
        /// A Common Intermediate Language Instruction which notifies debugging
        /// software that a break has been reached.  Typically injected by debuggers
        /// directly.
        /// </summary>
        Break = 0x01, // break
        /// <summary>
        /// A Common Intermediate Language Instruction which pushes the argument, at index 0, onto the stack.
        /// </summary>
        LoadArg0 = 0x02, // ldarg.0
        /// <summary>
        /// A Common Intermediate Language Instruction which pushes the argument, at index 1, onto the stack.
        /// </summary>
        LoadArg1 = 0x03, // ldarg.1
        /// <summary>
        /// A Common Intermediate Language Instruction which pushes the argument, at index 2, onto the stack.
        /// </summary>
        LoadArg2 = 0x04, // ldarg.2
        /// <summary>
        /// A Common Intermediate Language Instruction which pushes the argument, at index 3, onto the stack.
        /// </summary>
        LoadArg3 = 0x05, // ldarg.3
        /// <summary>
        /// A Common Intermediate Language Instruction which pushes a local, at index 0, onto the stack.
        /// </summary>
        LoadLocal0 = 0x06, // ldloc.0
        /// <summary>
        /// A Common Intermediate Language Instruction which pushes a local, at index 1, onto the stack.
        /// </summary>
        LoadLocal1 = 0x07, // ldloc.1
        /// <summary>
        /// A Common Intermediate Language Instruction which pushes a local, at index 2, onto the stack.
        /// </summary>
        LoadLocal2 = 0x08, // ldloc.2
        /// <summary>
        /// A Common Intermediate Language Instruction which pushes a local, at index 3, onto the stack.
        /// </summary>
        LoadLocal3 = 0x09, // ldloc.3
        /// <summary>
        /// A Common Intermediate Language Instruction which pops the top-most element from the stack and 
        /// stores it in a local at index 0.
        /// </summary>
        StoreLocal0 = 0x0A, // stloc.0
        /// <summary>
        /// A Common Intermediate Language Instruction which pops the top-most element from the stack and 
        /// stores it in a local at index 1.
        /// </summary>
        StoreLocal1 = 0x0B, // stloc.1
        /// <summary>
        /// A Common Intermediate Language Instruction which pops the top-most element from the stack and 
        /// stores it in a local at index 2.
        /// </summary>
        StoreLocal2 = 0x0C, // stloc.2
        /// <summary>
        /// A Common Intermediate Language Instruction which pops the top-most element from the stack and 
        /// stores it in a local at index 3.
        /// </summary>
        StoreLocal3 = 0x0D, // stloc.3
        /// <summary>
        /// A Common Intermediate Language Instruction which pushes an argument onto the stack with short-form
        /// syntax using an unsigned 8-bit integer, a byte, to represent the index of the argument.
        /// </summary>
        LoadArgumentShortForm = 0x0E, // ldarg.s
        /// <summary>
        /// A Common Intermediate Language Instruction which pushes an argument's address onto the stack with
        /// short-form syntax using an unsigned 8-bit integer, a byte, to represent the index of the argument.
        /// </summary>
        LoadArgumentAddressShortForm = 0x0F, // ldarga.s
        /// <summary>
        /// A Common Intermediate Language Instruction which stores an argument with short-form syntax using an
        /// unsigned 8-bit integer, a byte, to represent the index of the argument.
        /// </summary>
        StoreArgumentShortForm = 0x10, // starg.s
        /// <summary>
        /// A Common Intermediate Language Instruction which pushes a local onto the stack with short-form
        /// syntax using an unsigned 8-bit integer, a byte, to represent the index of the local.
        /// </summary>
        LoadLocalShortForm = 0x11, // ldloc.s
        /// <summary>
        /// A Common Intermediate Language Instruction which pushes a local's address onto the stack with
        /// short-form syntax using an unsigned 8-bit integer, a byte, to represent the index of the local.
        /// </summary>
        LoadLocalAddressShortForm = 0x12, // ldloca.s
        /// <summary>
        /// A Common Intermediate Language Instruction which stores a local with short-form syntax using an
        /// unsigned 8-bit integer, a byte, to represent the index of the local.
        /// </summary>
        StoreLocalShortForm = 0x13, // stloc.s
        /// <summary>
        /// A Common Intermediate Language Instruction which pushesa null value onto the stack.
        /// </summary>
        LoadNullValue = 0x14, // ldnull
        /// <summary>
        /// A Common Intermediate Language Instruction which pushes a 32-bit integer of -1 onto the stack.
        /// </summary>
        LoadInt32ConstantOfMinusOne = 0x15, // ldc.i4.m1
        /// <summary>
        /// A Common Intermediate Language Instruction which pushes a 32-bit integer of 0 onto the stack.
        /// </summary>
        LoadInt32ConstantOfZero = 0x16, // ldc.i4.0
        /// <summary>
        /// A Common Intermediate Language Instruction which pushes a 32-bit integer of 1 onto the stack.
        /// </summary>
        LoadInt32ConstantOfOne = 0x17, // ldc.i4.1
        /// <summary>
        /// A Common Intermediate Language Instruction which pushes a 32-bit integer of 2 onto the stack.
        /// </summary>
        LoadInt32ConstantOfTwo = 0x18, // ldc.i4.2
        /// <summary>
        /// A Common Intermediate Language Instruction which pushes a 32-bit integer of 3 onto the stack.
        /// </summary>
        LoadInt32ConstantOfThree = 0x19, // ldc.i4.3
        /// <summary>
        /// A Common Intermediate Language Instruction which pushes a 32-bit integer of 4 onto the stack.
        /// </summary>
        LoadInt32ConstantOfFour = 0x1A, // ldc.i4.4
        /// <summary>
        /// A Common Intermediate Language Instruction which pushes a 32-bit integer of 5 onto the stack.
        /// </summary>
        LoadInt32ConstantOfFive = 0x1B, // ldc.i4.5
        /// <summary>
        /// A Common Intermediate Language Instruction which pushes a 32-bit integer of 6 onto the stack.
        /// </summary>
        LoadInt32ConstantOfSix = 0x1C, // ldc.i4.6
        /// <summary>
        /// A Common Intermediate Language Instruction which pushes a 32-bit integer of 7 onto the stack.
        /// </summary>
        LoadInt32ConstantOfSeven = 0x1D, // ldc.i4.7 
        /// <summary>
        /// A Common Intermediate Language Instruction which pushes a 32-bit integer of 8 onto the stack.
        /// </summary>
        LoadInt32ConstantOfEight = 0x1E, // ldc.i4.8
        /// <summary>
        /// A Common Intermediate Language Instruction which pushes a 32-bit integer onto the stack with short-form
        /// syntax using an unsigned 8-bit integer, a byte, to represent the 32-bit value.
        /// </summary>
        LoadInt32ConstantShortForm = 0x1F, // ldc.i4.s
        /// <summary>
        /// A Common Intermediate Language Instruction which pushes a 32-bit integer onto the stack.
        /// </summary>
        LoadInt32Constant = 0x20, // ldc.i4
        /// <summary>
        /// A Common Intermediate Language Instruction which pushes a 64-bit integer onto the stack.
        /// </summary>
        LoadInt64Constant = 0x21, // ldc.i8
        /// <summary>
        /// A Common Intermediate Language Instruction which pushes a 32-bit floating point value onto the stack.
        /// </summary>
        LoadSingleConstant = 0x22, // ldc.r4
        /// <summary>
        /// A Common Intermediate Language Instruction which pushes a 64-bit floating point value onto the stack.
        /// </summary>
        LoadDoubleConstant = 0x23, // ldc.r8
        /// <summary>
        /// A Common Intermediate Language Instruction which peeks the top element of the stack and pushes
        /// a duplicate of that value onto the stack.
        /// </summary>
        DuplicateStackItem = 0x25, // dup
        /// <summary>
        /// A Common Intermediate Language Instruction which pops the top-most element off of the stack.
        /// </summary>
        PopStackItem = 0x26, // pop
        /// <summary>
        /// A Common Intermediate Language Instruction which transfers execution to the target method.  The target
        /// method must contain the exact number of parameters and the stack must be empty at the time of the call.
        /// </summary>
        JumpTo = 0x27, // jmp
        CallMethod = 0x28, // call
        CallMethodIndirect = 0x29, // calli
        Return = 0x2A, // ret
        UnconditionalBranchShortForm = 0x2B, // br.s
        BranchIfFalseShortForm = 0x2C, // brfalse.s
        BranchIfTrueShortForm = 0x2D, // brtrue.s
        BranchIfEqualToShortForm = 0x2E, // beq.s
        BranchIfGreaterThanOrEqualToShortForm = 0x2F, // bge.s
        BranchIfGreaterThanShortForm = 0x30, // bgt.s
        BranchIfLessThanOrEqualToShortForm = 0x31, // ble.s
        BranchIfLessThanShortForm = 0x32, // blt.s
        BranchIfNotEqualToUnSignedOrOrderedShortForm = 0x33, // bne.un.s
        BranchIfGreaterThanOrEqualToUnSignedOrOrderedShortForm = 0x34, // bge.un.s
        BranchIfGreaterThanUnSignedOrOrderedShortForm = 0x35, // bgt.un.s
        BranchIfLessThanOrEqualToUnSignedOrOrderedShortForm = 0x36, // ble.un.s
        BranchIfLessThanUnSignedOrOrderedShortForm = 0x37, // blt.un.s
        UnconditionalBranch = 0x38, // br
        BranchIfFalse = 0x39, // brfalse
        BranchIfTrue = 0x3A, // brtrue
        BranchIfEqualTo = 0x3B, // beq
        BranchIfGreaterThanOrEqualTo = 0x3C, // bge
        BranchIfGreaterThan = 0x3D, // bgt
        BranchIfLessThanOrEqualTo = 0x3E, // ble
        BranchIfLessThan = 0x3F, // blt
        BranchIfNotEqualToUnSignedOrOrdered = 0x40, // bne.un
        BranchIfGreaterThanOrEqualToUnSignedOrOrdered = 0x41, // bge.un
        BranchIfGreaterThanUnSignedOrOrdered = 0x42, // bgt.un
        BranchIfLessThanOrEqualToUnSignedOrOrdered = 0x43, // ble.un
        BranchIfLessThanUnSignedOrOrdered = 0x44, // blt.un
        Switch = 0x45, // switch
        LoadIndirectSByte = 0x46, // ldind.i1
        LoadIndirectByte = 0x47, // ldind.u1
        LoadIndirectInt16 = 0x48, // ldind.i2
        LoadIndirectUInt16 = 0x49, // ldind.u2
        LoadIndirectInt32 = 0x4A, // ldind.i4
        LoadIndirectUInt32 = 0x4B, // ldind.u4
        LoadIndirectInt64OrUInt64 = 0x4C, // ldind.i8 | ldind.u8
        LoadIndirectNativeInteger = 0x4D, // ldind.i
        LoadIndirectSingle = 0x4E, // ldind.r4
        LoadIndirectDouble = 0x4F, // ldind.r8
        LoadIndirectReference = 0x50, // ldind.ref
        StoreIndirectReference = 0x51, // stind.ref
        StoreIndirectSByte = 0x52, // stind.i1
        StoreIndirectInt16 = 0x53, // stind.i2
        StoreIndirectInt32 = 0x54, // stind.i4
        StoreIndirectInt64 = 0x55, // stind.i8
        StoreIndirectSingle = 0x56, // stind.r4
        StoreIndirectDouble = 0x57, // stind.r8
        Add = 0x58, // add
        Subtract = 0x59, // sub
        Multiply = 0x5A, // mul 
        Divide = 0x5B, // div
        DivideUnSignedOrOrdered = 0x5C, // div.un
        Remainder = 0x5D, // rem 
        RemainderUnSignedOrOrdered = 0x5E, // rem.un 
        BitwiseAnd = 0x5F, // and 
        BitwiseOr = 0x60, // or 
        BitwiseExclusiveOr = 0x61, // xor
        ShiftBitsLeft = 0x62, // shl
        ShiftBitsRight = 0x63, // shr
        ShiftBitsRightUnSignedOrOrdered = 0x64, // shr.un
        Negate = 0x65, // neg
        BitwiseComplement = 0x66, // not
        UncheckedConvertToSByte = 0x67, // conv.i1
        UncheckedConvertToInt16 = 0x68, // conv.i2
        UncheckedConvertToInt32 = 0x69, // conv.i4
        UncheckedConvertToInt64 = 0x6A, // conv.i8
        UncheckedConvertToSingle = 0x6B, // conv.r4
        UncheckedConvertToDouble = 0x6C, // conv.r8
        UncheckedConvertToUInt32 = 0x6D, // conv.u4
        UncheckedConvertToUInt64 = 0x6E, // conv.u8
        CallVirtualMethod = 0x6F, // callvirt
        AddressCopy = 0x70, // cpobj
        LoadObject = 0x71, // ldobj
        LoadString = 0x72, // ldstr
        NewObject = 0x73, // newobj
        CastClass = 0x74, // castclass
        IsInstance = 0x75, // isinst
        ConvertUnsignedIntToFloatingPoint = 0x76, // conv.r.un
        Unbox = 0x79, // unbox
        ThrowException = 0x7A, // throw
        LoadField = 0x7B, // ldfld
        LoadFieldAddress = 0x7C, // ldflda
        StoreField = 0x7D, // stfld
        LoadStaticField = 0x7E, // ldsfld
        LoadStaticFieldAddress = 0x7F, // ldsflda
        StoreStaticField = 0x80, // stsfld
        StoreObject = 0x81, // stobj
        CheckedConvertUnsignedToSByteAsInt32 = 0x82, // conv.ovf.i1.un
        CheckedConvertUnsignedToInt16AsInt32 = 0x83, // conv.ovf.i2.un
        CheckedConvertUnsignedToInt32AsInt32 = 0x84, // conv.ovf.i4.un
        CheckedConvertUnsignedToInt64 = 0x85, // conv.ovf.i8.un
        CheckedConvertUnsignedToByteAsInt32 = 0x86, // conv.ovf.u1.un
        CheckedConvertUnsignedToUInt16AsInt32 = 0x87, // conv.ovf.u2.un
        CheckedConvertUnsignedToUInt32AsInt32 = 0x88, // conv.ovf.u4.un
        CheckedConvertUnsignedToUInt64AsInt64 = 0x89, // conv.ovf.u8.un
        CheckedConvertUnsignedToNativeInt = 0x8A, // conv.ovf.i.un
        CheckedConvertUnsignedToNativeUInt = 0x8B, // conv.ovf.u.un
        Box = 0x8C, // box
        NewArray = 0x8D, // newarr
        LoadArrayLength = 0x8E, // ldlen
        LoadArrayElementAddress = 0x8F, // ldelema
        LoadArrayElementAsSByte = 0x90, // ldelem.i1
        LoadArrayElementAsByte = 0x91, // ldelem.u1
        LoadArrayElementAsInt16 = 0x92, // ldelem.i2
        LoadArrayElementAsUInt16 = 0x93, // ldelem.u2
        LoadArrayElementAsInt32 = 0x94, // ldelem.i4
        LoadArrayElementAsUInt32 = 0x95, // ldelem.u4
        LoadArrayElementAsInt64 = 0x96, // ldelem.i8 or ldelem.u8
        LoadArrayElementAsNativeInteger = 0x97, // ldelem.i
        LoadArrayElementAsSingle = 0x98, // ldelem.r4
        LoadArrayElementAsDouble = 0x99, // ldelem.r8
        LoadArrayElementReference = 0x9A, // ldelem.ref
        StoreNativeIntegerArrayElement = 0x9B, // stelem.i
        StoreSByteArrayElement = 0x9C, // stelem.i1
        StoreInt16ArrayElement = 0x9D, // stelem.i2
        StoreInt32ArrayElement = 0x9E, // stelem.i4
        StoreInt64ArrayElement = 0x9F, // stelem.i8
        StoreSingleArrayElement = 0xA0, // stelem.r4
        StoreDoubleArrayElement = 0xA1, // stelem.r8
        StoreReferenceArrayElement = 0xA2, // stelem.ref
        LoadElement = 0xA3, // ldelem
        StoreElement = 0xA4, // stelem
        UnboxAny = 0xA5, // unbox.any
        CheckedConvertToSByteAsInt32 = 0xB3, // conv.ovf.i1
        CheckedConvertToByteAsInt32 = 0xB4, // conv.ovf.u1
        CheckedConvertToInt16AsInt32 = 0xB5, // conv.ovf.i2
        CheckedConvertToUInt16AsInt32 = 0xB6, // conv.ovf.u2
        CheckedConvertToInt32AsInt32 = 0xB7, // conv.ovf.i4
        CheckedConvertToUInt32AsInt32 = 0xB8, // conv.ovf.u4
        CheckedConvertToInt64 = 0xB9, // conv.ovf.i8
        CheckedConvertToUInt64AsInt64 = 0xBA, // conv.ovf.u8
        GetTypedReferenceAddress = 0xC2, // refanyval
        CheckForFiniteNumber = 0xC3, // Ckfinite
        MakeTypedReference = 0xC6, // mkrefany
        LoadToken = 0xD0, // ldtoken
        UncheckedConvertToUInt16 = 0xD1, // conv.u2
        UncheckedConvertToByte = 0xD2, // conv.u1
        UncheckedConvertToNativeInteger = 0xD3, // conv.i
        CheckedConvertToNativeInteger = 0xD4, // conv.ovf.i
        CheckedConvertToUnsignedNativeInteger = 0xD5, // conv.ovf.u
        CheckedAdd = 0xD6, // add.ovf
        CheckedAddUnSignedOrOrdered = 0xD7, // add.ovf.un
        CheckedMultiply = 0xD8, // mul.ovf
        CheckedMultiplyUnSignedOrOrdered = 0xD9, // mul.ovf.un
        CheckedSubtract = 0xDA, // sub.ovf
        CheckedSubtractUnSignedOrOrdered = 0xDB, // sub.ovf.un
        EndFinallyBlock = 0xDC, // endfault
        LeaveProtectedBlock = 0xDD, // endfinally
        LeaveProtectedBlockShortForm = 0xDE, // leave.s
        StoreIndirectNativeInteger = 0xDF, // stind.i
        UncheckedConvertToUnsignedNativeInteger = 0xE0, // conv.u
        GetArgumentList = 0xFE00, // arglist
        CompareEquality = 0xFE01, // Ceq
        CompareGreaterThan = 0xFE02, // Cgt
        CompareGreaterThanUnSignedOrOrdered = 0xFE03, // Cgt.un
        CompareLessThan = 0xFE04, // Clt
        CompareLessThanUnSignedOrOrdered = 0xFE05, // Clt.Un
        LoadMethodPointer = 0xFE06, // ldftn
        LoadVirtualMethodPointer = 0xFE07, // ldvirtftn
        LoadArgument = 0xFE09, // ldarg
        LoadArgumentAddress = 0xFE0A, // ldarga
        StoreArgument = 0xFE0B, // starg
        LoadLocal = 0xFE0C, // ldloc
        LoadLocalAddress = 0xFE0D, // ldloca
        StoreLocal = 0xFE0E, // stloc
        AllocateLocalMemory = 0xFE0F, // localloc
        EndFilter = 0xFE11, // Endfilter
        PointerReferenceMayBeUnaligned = 0xFE12, // unaligned.
        VolatilePointerReference = 0xFE13, // volatile.
        TailCallModifier = 0xFE14, // tail.
        InitializeObject = 0xFE15, // initobj
        ConstrainedCallVirtModifier = 0xFE16, // constrained.
        CopyMemoryBlock = 0xFE17, // cpblk
        InitializeMemoryBlock = 0xFE18, // initblk
        /// <summary>
        /// Notes an instruction will be two-bytes and the reader should read further.
        /// </summary>
        TwoByteLeadIn = 0xFE,
    }
}
