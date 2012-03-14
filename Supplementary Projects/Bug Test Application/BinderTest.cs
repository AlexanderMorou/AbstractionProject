using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Languages.Cil;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Languages.VisualBasic;
using AllenCopeland.Abstraction.Slf.Languages.CSharp;
using System.Diagnostics;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Languages;
using System.IO;
using AllenCopeland.Abstraction.IO;
using AllenCopeland.Abstraction.Numerics;
//using AllenCopeland.Abstraction.Slf.Languages.Java;
internal class ClassTypeParameterReplacement { };
internal class MethodTypeParameterReplacement { };

namespace AllenCopeland.Abstraction.SupplementaryProjects.BugTestApplication
{
    internal class BinderTest
    {
        public static void TestMethod(int a1, int a2, int a3 = 3, int a4 = 9, int a5 = 29, byte c3 = 1)
        {

        }
        static void Main()
        {
            //string s = @"test.class";
            //FileStream fs = new FileStream(s, FileMode.Create, FileAccess.Write);
            //EndianAwareBinaryWriter eabw = new EndianAwareBinaryWriter(fs, Endianness.BigEndian, false);
            ////EndianAwareBinaryReader eabr = new EndianAwareBinaryReader(fs, Endianness.BigEndian, false);
            //eabw.Write(ByteCodeCompiledFile.FormatIdentityValue);
            //eabw.Close();
        }
    }
}
