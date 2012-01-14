
//  Microsoft (R) .NET Framework IL Disassembler.  Version 4.0.30319.1
//  Copyright (c) Microsoft Corporation.  All rights reserved.



// Metadata version: v4.0.30319
.assembly extern 'mscorlib'
{
  .publickeytoken = (B7 7A 5C 56 19 34 E0 89 )                         // .z\V.4..
  .ver 4:0:0:0
}
.assembly extern '_abs.slf.metadata'
{
  .publickeytoken = (9F 6E C7 B8 EA 17 08 09 )                         // .n......
  .ver 1:0:0:0
}
.assembly 'TestAssembly'
{
  .hash algorithm 0x00008004
  .ver 0:0:0:0
}
.module 'TestAssembly.dll'
// MVID: {EB0D94BF-7867-4D2A-A722-3A8178A4AC1E}
.imagebase 0x00400000
.file alignment 0x00000200
.stackreserve 0x00100000
.subsystem 0x0003       // WINDOWS_CUI
.corflags 0x00000001    //  ILONLY
// Image base: 0x01CE0000


// =============== CLASS MEMBERS DECLARATION ===================

.class private explicit ansi sealed '$ArrayType$575'
       extends ['mscorlib']'System'.'ValueType'
{
  .pack 1
  .size 575
} // end of class '$ArrayType$575'

.class public abstract auto ansi sealed 'AllenCopeland.Abstraction.SupplementaryProjects.TestAssembly'.'TestType'
       extends ['mscorlib']'System'.'Object'
{
  .field public static int32 modopt(['mscorlib']'System.Runtime.CompilerServices'.'IsLong') 'testField'
  .field static privatescope valuetype '$ArrayType$575' 'TestData$PST04000002' at D_00004000
  .method public static int16[0...,0...,0...,0...,0...] 
          'get_TestProperty'() cil managed
  {
    // Code size       41 (0x29)
    .maxstack  7
    IL_0000:  ldc.i4     0x23f
    IL_0005:  ldc.i4     0x5
    IL_000a:  ldc.i4     0x6
    IL_000f:  ldc.i4     0x7
    IL_0014:  ldc.i4     0x8
    IL_0019:  ldc.i4     0x9
    IL_001e:  ldtoken    field valuetype '$ArrayType$575' 'AllenCopeland.Abstraction.SupplementaryProjects.TestAssembly'.'TestType'::'TestData$PST04000002'
    IL_0023:  call       int16[0...,0...,0...,0...,0...] ['_abs.slf.metadata']'AllenCopeland.Abstraction.Slf.Compilers'.'StandardCompilerAids'::'InitializeCompressedInt16Array'(int32,
                                                                                                                                                                                 int32,
                                                                                                                                                                                 int32,
                                                                                                                                                                                 int32,
                                                                                                                                                                                 int32,
                                                                                                                                                                                 int32,
                                                                                                                                                                                 valuetype ['mscorlib']'System'.'RuntimeFieldHandle')
    IL_0028:  ret
  } // end of method 'TestType'::'get_TestProperty'

  .property int16[0...,0...,0...,0...,0...]
          'TestProperty'()
  {
    .get int16[0...,0...,0...,0...,0...] 'AllenCopeland.Abstraction.SupplementaryProjects.TestAssembly'.'TestType'::'get_TestProperty'()
  } // end of property 'TestType'::'TestProperty'
} // end of class 'AllenCopeland.Abstraction.SupplementaryProjects.TestAssembly'.'TestType'


// =============================================================

.data D_00004000 = bytearray (
                 1F 8B 08 00 00 00 00 00 04 00 ED D1 E7 16 08 04
                 00 06 D0 4B 46 76 43 C3 68 A8 B4 D0 42 48 45 65  // ...KFvC.h...BHEe
                 94 68 90 6C 9A 42 C3 AA 54 A2 68 A8 6C 15 42 BB  // .h.l.B..T.h.l.B.
                 8C D2 30 2A 94 D5 A6 22 8A 86 CA 5E A9 90 06 CA  // ..0*..."...^....
                 39 1E C2 DF EF BE C2 2D A0 A0 83 14 52 58 11 45  // 9......-....RX.E
                 1D AC 98 E2 4A 28 A9 94 D2 CA 38 C4 A1 0E 73 B8  // ....J(....8...s.
                 B2 8E 70 A4 A3 1C AD 9C F2 2A A8 E8 18 C7 3A CE  // ..p......*....:.
                 F1 2A 39 C1 89 4E 52 D9 C9 4E 71 AA D3 9C AE 8A  // .*9..NR..Nq.....
                 AA AA 39 C3 99 CE 72 B6 73 54 57 43 4D E7 AA A5  // ..9...r.sTWCM...
                 B6 3A CE 53 D7 F9 2E 70 A1 7A EA BB C8 C5 2E D1  // .:.S...p.z......
                 40 43 8D 34 76 A9 CB 34 71 B9 A6 9A B9 C2 95 AE  // @C.4v..4q.......
                 72 B5 E6 5A B8 46 4B D7 6A A5 B5 36 DA 6A A7 BD  // r..Z.FK.j..6.j..
                 0E 3A EA E4 3A D7 BB C1 8D 6E 72 B3 CE 6E D1 45  // .:..:....nr..n.E
                 57 DD DC EA 36 B7 BB 43 77 3D F4 D4 4B 6F 77 BA  // W...6..Cw=..Kow.
                 CB DD FA B8 C7 BD EE D3 D7 FD FA E9 EF 01 0F 1A
                 60 A0 87 3C EC 11 8F 1A E4 31 8F 7B C2 60 43 0C  // `..<.....1.{.`C.
                 35 CC 70 23 8C 34 CA 93 9E F2 B4 D1 C6 18 EB 19  // 5.p#.4..........
                 E3 8C 37 C1 B3 9E F3 BC 17 BC E8 25 2F 7B C5 AB  // ..7........%/{..
                 26 9A 64 B2 29 5E F3 BA A9 DE F0 A6 B7 BC 6D 9A  // &.d.)^........m.
                 E9 66 98 E9 1D EF 7A CF 2C B3 CD F1 BE 0F CC 35  // .f....z.,......5
                 CF 7C 0B 2C F4 A1 8F 7C EC 13 9F FA CC E7 16 59  // .|.,...|.......Y
                 EC 0B 5F FA CA 12 4B 7D 6D 99 E5 BE F1 AD 15 56  // .._...K}m......V
                 FA CE F7 7E F0 A3 55 7E F2 B3 5F AC B6 C6 5A EB  // ...~..U~.._...Z.
                 AC B7 C1 46 9B 6C B6 C5 56 BF DA E6 37 BF FB C3  // ...F.l..V...7...
                 76 3B EC F4 A7 5D FE F2 B7 7F FC 6B B7 3D F6 FA  // v;...].....k.=..
                 CF FF F6 A1 40 EE 73 9F FB DC E7 3E F7 B9 CF 7D  // ....@.s....>...}
                 EE 73 9F FB DC E7 3E F7 B9 CF 7D EE 73 9F FB DC  // .s....>...}.s...
                 E7 3E F7 B9 CF 7D EE 73 9F FB DC E7 3E F7 B9 CF  // .>...}.s....>...
                 7D EE 73 9F FB DC E7 3E F7 B9 CF 7D EE 73 9F FB  // }.s....>...}.s..
                 DC E7 3E F7 B9 CF 7D EE 73 9F FB DC E7 3E F7 B9  // ..>...}.s....>..
                 CF 7D EE 73 9F FB DC E7 3E F7 B9 CF 7D EE 73 9F  // .}.s....>...}.s.
                 FB DC E7 3E F7 B9 CF 7D EE 73 9F FB DC E7 3E F7  // ...>...}.s....>.
                 B9 CF 7D EE 73 9F FB DC E7 3E F7 B9 CF 7D EE 73  // ..}.s....>...}.s
                 9F FB DC E7 3E F7 B9 CF 7D EE 73 9F FB DC E7 3E  // ....>...}.s....>
                 F7 B9 CF 7D EE 73 9F FB DC E7 3E F7 B9 CF 7D EE  // ...}.s....>...}.
                 73 9F FB 03 BA DF 0F 55 36 2E B5 20 76 00 00)    // s......U6.. v..
// *********** DISASSEMBLY COMPLETE ***********************
