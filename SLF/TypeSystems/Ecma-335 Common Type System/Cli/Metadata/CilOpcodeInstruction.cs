using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Cli.Metadata
{

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
        /// A Common Intermediate Language Instruction which 
        /// pushes the argument, at index 0, onto the stack.
        /// </summary>
        LoadArg0 = 0x02, // ldarg.0
        /// <summary>
        /// A Common Intermediate Language Instruction which 
        /// pushes the argument, at index 1, onto the stack.
        /// </summary>
        LoadArg1 = 0x03, // ldarg.1
        /// <summary>
        /// A Common Intermediate Language Instruction which 
        /// pushes the argument, at index 2, onto the stack.
        /// </summary>
        LoadArg2 = 0x04, // ldarg.2
        /// <summary>
        /// A Common Intermediate Language Instruction which 
        /// pushes the argument, at index 3, onto the stack.
        /// </summary>
        LoadArg3 = 0x05, // ldarg.3
        /// <summary>
        /// A Common Intermediate Language Instruction which 
        /// pushes a local, at index 0, onto the stack.
        /// </summary>
        LoadLocal0 = 0x06, // ldloc.0
        /// <summary>
        /// A Common Intermediate Language Instruction which 
        /// pushes a local, at index 1, onto the stack.
        /// </summary>
        LoadLocal1 = 0x07, // ldloc.1
        /// <summary>
        /// A Common Intermediate Language Instruction which 
        /// pushes a local, at index 2, onto the stack.
        /// </summary>
        LoadLocal2 = 0x08, // ldloc.2
        /// <summary>
        /// A Common Intermediate Language Instruction which 
        /// pushes a local, at index 3, onto the stack.
        /// </summary>
        LoadLocal3 = 0x09, // ldloc.3
        /// <summary>
        /// A Common Intermediate Language Instruction which 
        /// pops the top-most element from the stack and 
        /// stores it in a local at index 0.
        /// </summary>
        StoreLocal0 = 0x0A, // stloc.0
        /// <summary>
        /// A Common Intermediate Language Instruction which 
        /// pops the top-most element from the stack and 
        /// stores it in a local at index 1.
        /// </summary>
        StoreLocal1 = 0x0B, // stloc.1
        /// <summary>
        /// A Common Intermediate Language Instruction which 
        /// pops the top-most element from the stack and 
        /// stores it in a local at index 2.
        /// </summary>
        StoreLocal2 = 0x0C, // stloc.2
        /// <summary>
        /// A Common Intermediate Language Instruction which 
        /// pops the top-most element from the stack and 
        /// stores it in a local at index 3.
        /// </summary>
        StoreLocal3 = 0x0D, // stloc.3
        /// <summary>
        /// A Common Intermediate Language Instruction which 
        /// pushes an argument onto the stack with short-form
        /// syntax using an unsigned 8-bit integer, a byte, to
        /// represent the index of the argument.
        /// </summary>
        LoadArgumentShortForm = 0x0E, // ldarg.s
        /// <summary>
        /// A Common Intermediate Language Instruction which 
        /// pushes an argument's address onto the stack with
        /// short-form syntax using an unsigned 8-bit integer,
        /// a byte, to represent the index of the argument.
        /// </summary>
        LoadArgumentAddressShortForm = 0x0F, // ldarga.s
        /// <summary>
        /// A Common Intermediate Language Instruction which 
        /// stores an argument with short-form syntax using an
        /// unsigned 8-bit integer, a byte, to represent the
        /// index of the argument.
        /// </summary>
        StoreArgumentShortForm = 0x10, // starg.s
        /// <summary>
        /// A Common Intermediate Language Instruction which 
        /// pushes a local onto the stack with short-form
        /// syntax using an unsigned 8-bit integer, a byte,
        /// to represent the index of the local.
        /// </summary>
        LoadLocalShortForm = 0x11, // ldloc.s
        /// <summary>
        /// A Common Intermediate Language Instruction which 
        /// pushes a local's address onto the stack with
        /// short-form syntax using an unsigned 8-bit integer,
        /// a byte, to represent the index of the local.
        /// </summary>
        LoadLocalAddressShortForm = 0x12, // ldloca.s
        /// <summary>
        /// A Common Intermediate Language Instruction which 
        /// stores a local with short-form syntax using an
        /// unsigned 8-bit integer, a byte, to represent the
        /// index of the local.
        /// </summary>
        StoreLocalShortForm = 0x13, // stloc.s
        /// <summary>
        /// A Common Intermediate Language Instruction which pushes
        /// a null value onto the stack.
        /// </summary>
        LoadNullValue = 0x14, // ldnull
        /// <summary>
        /// A Common Intermediate Language Instruction which pushes
        /// a 32-bit integer of -1 onto the stack.
        /// </summary>
        LoadInt32ConstantOfMinusOne = 0x15, // ldc.i4.m1
        /// <summary>
        /// A Common Intermediate Language Instruction which pushes
        /// a 32-bit integer of 0 onto the stack.
        /// </summary>
        LoadInt32ConstantOfZero = 0x16, // ldc.i4.0
        /// <summary>
        /// A Common Intermediate Language Instruction which pushes
        /// a 32-bit integer of 1 onto the stack.
        /// </summary>
        LoadInt32ConstantOfOne = 0x17, // ldc.i4.1
        /// <summary>
        /// A Common Intermediate Language Instruction which pushes
        /// a 32-bit integer of 2 onto the stack.
        /// </summary>
        LoadInt32ConstantOfTwo = 0x18, // ldc.i4.2
        /// <summary>
        /// A Common Intermediate Language Instruction which pushes
        /// a 32-bit integer of 3 onto the stack.
        /// </summary>
        LoadInt32ConstantOfThree = 0x19, // ldc.i4.3
        /// <summary>
        /// A Common Intermediate Language Instruction which pushes
        /// a 32-bit integer of 4 onto the stack.
        /// </summary>
        LoadInt32ConstantOfFour = 0x1A, // ldc.i4.4
        /// <summary>
        /// A Common Intermediate Language Instruction which pushes
        /// a 32-bit integer of 5 onto the stack.
        /// </summary>
        LoadInt32ConstantOfFive = 0x1B, // ldc.i4.5
        /// <summary>
        /// A Common Intermediate Language Instruction which pushes
        /// a 32-bit integer of 6 onto the stack.
        /// </summary>
        LoadInt32ConstantOfSix = 0x1C, // ldc.i4.6
        /// <summary>
        /// A Common Intermediate Language Instruction which pushes
        /// a 32-bit integer of 7 onto the stack.
        /// </summary>
        LoadInt32ConstantOfSeven = 0x1D, // ldc.i4.7 
        /// <summary>
        /// A Common Intermediate Language Instruction which pushes
        /// a 32-bit integer of 8 onto the stack.
        /// </summary>
        LoadInt32ConstantOfEight = 0x1E, // ldc.i4.8
        /// <summary>
        /// A Common Intermediate Language Instruction which 
        /// pushes a 32-bit integer onto the stack with short-form
        /// syntax using an unsigned 8-bit integer, a byte, to
        /// represent the 32-bit value.
        /// </summary>
        LoadInt32ConstantShortForm = 0x1F, // ldc.i4.s
        /// <summary>
        /// A Common Intermediate Language Instruction which 
        /// pushes a 32-bit integer onto the stack.
        /// </summary>
        LoadInt32Constant = 0x20, // ldc.i4
        /// <summary>
        /// A Common Intermediate Language Instruction which 
        /// pushes a 64-bit integer onto the stack.
        /// </summary>
        LoadInt64Constant = 0x21, // ldc.i8
        /// <summary>
        /// A Common Intermediate Language Instruction which 
        /// pushes a 32-bit floating point value onto the stack.
        /// </summary>
        LoadSingleConstant = 0x22, // ldc.r4
        /// <summary>
        /// A Common Intermediate Language Instruction which 
        /// pushes a 64-bit floating point value onto the stack.
        /// </summary>
        LoadDoubleConstant = 0x23, // ldc.r8
        /// <summary>
        /// A Common Intermediate Language Instruction which
        /// peeks the top element of the stack and pushes
        /// a duplicate of that value onto the stack.
        /// </summary>
        DuplicateStackItem = 0x25, // dup
        /// <summary>
        /// A Common Intermediate Language Instruction which
        /// pops the top-most element off of the stack.
        /// </summary>
        PopStackItem = 0x26, // pop
        /// <summary>
        /// A Common Intermediate Language Instruction which
        /// transfers execution to the target method.  The target
        /// method must contain the exact number of parameters and
        /// the stack must be empty at the time of the call.
        /// </summary>
        JumpTo = 0x27, // jmp
        CallMethod = 0x28, // call
        CallMethodIndirect = 0x29, // calli
        Return = 0x2A, // ret
        UnconditionalBranchShortForm = 0x2B, // br.s
        BranchIfFalseShortForm = 0x2C, // brfalse.s
        BranchIfTrueShortForm = 0x2D, // brtrue.s
        BranchIfEqualToShortForm = 0x2E, // beq.s
        BranchIfGreaterThanToOrEqualToShortForm = 0x2F, // bge.s
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
        CheckedConvertUnsignedToSByteAsInt32 = 0x82,
        CheckedConvertUnsignedToInt16AsInt32 = 0x83,
        CheckedConvertUnsignedToInt32AsInt32 = 0x84,
        CheckedConvertUnsignedToInt64 = 0x85,
        CheckedConvertUnsignedToByteAsInt32 = 0x86,
        CheckedConvertUnsignedToUInt16AsInt32 = 0x87,
        CheckedConvertUnsignedToUInt32AsInt32 = 0x88,
        CheckedConvertUnsignedToUInt64AsInt64 = 0x89,
        CheckedConvertUnsignedToNativeInt = 0x8A,
        CheckedConvertUnsignedToNativeUInt = 0x8B,
        Box = 0x8C,
        NewArray = 0x8D,
        LoadArrayLength = 0x8E,
        LoadArrayElementAddress = 0x8F,
        LoadArrayElementAsSByte = 0x90,
        LoadArrayElementAsByte = 0x91,
        LoadArrayElementAsInt16 = 0x92,
        LoadArrayElementAsUInt16 = 0x93,
        LoadArrayElementAsInt32 = 0x94,
        LoadArrayElementAsUInt32 = 0x95,
        LoadArrayElementAsInt64 = 0x96,
        LoadArrayElementAsNativeInteger = 0x97,
        LoadArrayElementAsSingle = 0x98,
        LoadArrayElementAsDouble = 0x99,
        LoadArrayElementReference = 0x9A,
        StoreNativeIntegerArrayElement = 0x9B,
        StoreSByteArrayElement = 0x9C,
        StoreInt16ArrayElement = 0x9D,
        StoreInt32ArrayElement = 0x9E,
        StoreInt64ArrayElement = 0x9F,
        StoreSingleArrayElement = 0xA0,
        StoreDoubleArrayElement = 0xA1,
        StoreReferenceArrayElement = 0xA2,
        LoadElement = 0xA3,
        StoreElement = 0xA4,
        UnboxAny = 0xA5,
        CheckedConvertToSByteAsInt32 = 0xB3,
        CheckedConvertToByteAsInt32 = 0xB4,
        CheckedConvertToInt16AsInt32 = 0xB5,
        CheckedConvertToUInt16AsInt32 = 0xB6,
        CheckedConvertToInt32AsInt32 = 0xB7,
        CheckedConvertToUInt32AsInt32 = 0xB8,
        CheckedConvertToInt64 = 0xB9,
        CheckedConvertToUInt64AsInt64 = 0xBA,
        GetTypedReferenceAddress = 0xC2,
        CheckForFiniteNumber = 0xC3,
        MakeTypedReference = 0xC6,
        LoadToken = 0xD0,
        UncheckedConvertToUInt16 = 0xD1,
        UncheckedConvertToByte = 0xD2,
        UncheckedConvertToNativeInteger = 0xD3,
        CheckedConvertToNativeInteger = 0xD4,
        CheckedConvertToUnsignedNativeInteger = 0xD5,
        CheckedAdd = 0xD6,
        CheckedAddUnSignedOrOrdered = 0xD7,
        CheckedMultiply = 0xD8,
        CheckedMultiplyUnSignedOrOrdered = 0xD9,
        CheckedSubtract = 0xDA,
        CheckedSubtractUnSignedOrOrdered = 0xDB,
        EndFinallyBlock = 0xDC,
        LeaveProtectedBlock = 0xDD,
        LeaveProtectedBlockShortForm = 0xDE,
        StoreIndirectNativeInteger = 0xDF,
        UncheckedConvertToUnsignedNativeInteger = 0xE0,
        GetArgumentList = 0xFE00,
        CompareEquality = 0xFE01,
        CompareGreaterThan = 0xFE02,
        CompareGreaterThanUnSignedOrOrdered = 0xFE03,
        CompareLessThan = 0xFE04,
        CompareLessThanUnSignedOrOrdered = 0xFE05,
        LoadMethodPointer = 0xFE06,
        LoadVirtualMethodPointer = 0xFE07,
        LoadArgument = 0xFE09,
        LoadArgumentAddress = 0xFE0A,
        StoreArgument = 0xFE0B,
        LoadLocal = 0xFE0C,
        LoadLocalAddress = 0xFE0D,
        StoreLocal = 0xFE0E,
        AllocateLocalMemory = 0xFE0F,
        EndFilter = 0xFE11,
        PointerReferenceMayBeUnaligned = 0xFE12,
        VolatilePointerReference = 0xFE13,
        TailCallModifier = 0xFE14,
        InitializeObject = 0xFE15,
        ConstrainedCallVirtModifier = 0xFE16,
        CopyMemoryBlock = 0xFE17,
        InitializeMemoryBlock = 0xFE18,
        TwoByteLeadIn = 0xFE,
    }
}
