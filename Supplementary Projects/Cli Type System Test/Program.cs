using AllenCopeland.Abstraction.Globalization;
using AllenCopeland.Abstraction.Numerics;
using AllenCopeland.Abstraction.OwnerDrawnControls;
using AllenCopeland.Abstraction.Slf._Internal.Abstract;
using AllenCopeland.Abstraction.Slf._Internal.Ast;
using AllenCopeland.Abstraction.Slf._Internal.Cli;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Members;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Slf._Internal.Cli.TypeIdParser;
using AllenCopeland.Abstraction.Slf._Internal.Cli.TypeIdParser.Rules;
using AllenCopeland.Abstraction.Slf._Internal.GenericLayer;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Ast.Cli;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
using AllenCopeland.Abstraction.Slf.Ast.Expressions.Linq;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using AllenCopeland.Abstraction.Slf.Ast.Statements;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Metadata;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Slf.Compilers;
using AllenCopeland.Abstraction.Slf.Languages;
using AllenCopeland.Abstraction.Slf.Languages.Cil;
using AllenCopeland.Abstraction.Slf.Languages.CSharp;
using AllenCopeland.Abstraction.Slf.Languages.CSharp.Translation;
using AllenCopeland.Abstraction.Slf.Languages.ToySharp;
using AllenCopeland.Abstraction.Slf.Languages.VisualBasic;
using AllenCopeland.Abstraction.Slf.Translation;
using AllenCopeland.Abstraction.Utilities;
using AllenCopeland.Abstraction.Utilities.Arrays;
using AllenCopeland.Abstraction.Utilities.Collections;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AllenCopeland.Abstraction.Slf.SupplementaryProjects.TestCli
{
    [type: AttributeUsage(validOn: AttributeTargets.Constructor | AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class TestAttribute :
        Attribute
    {
        private AccessLevelModifiers[] modifiers;
        public TestAttribute(params AccessLevelModifiers[] modifiers)
        {
            this.modifiers = modifiers;
        }

        public object[] TestAttributeValue { get; set; }
        public object M { get; set; }
        public override string ToString()
        {
            return base.ToString();
        }
    }

    [Test(AccessLevelModifiers.Public, AccessLevelModifiers.ProtectedOrInternal, TestAttributeValue = new object[] { AttributeTargets.Class | AttributeTargets.GenericParameter, CliMetadataNativeTypes.Class })]//TestAttributeValue = new object[] { "Test", typeof(___3.___3), new AttributeTargets[] { AttributeTargets.Class, AttributeTargets.Method, AttributeTargets.Property }, new object[] { new string[] { "Test", "Decimal" }, new int[] { 81, 82, 83, 84, 85, 86 } } }, 
    static class Program
    {
        private const string Version1String = "v1.0.3705";
        private const string Version1_1String = "v1.1.4322";
        private const string Version2String = "v2.0.50727";
        private const string Version3String = "v3.0";
        private const string Version3_5String = "v3.5";
        private const string Version4String = "v4.0.30319";
        private static IIntermediateCliManager identityManager = CreateIDM();

        private static IIntermediateCliManager CreateIDM() { return IntermediateCliGateway.CreateIdentityManager(CliGateway.CurrentPlatform, CliFrameworkVersion.CurrentVersion, true, false); }
        private static Tuple<AssemblyBuilder, ModuleBuilder> CreateAssemblyBuilder(string assemblyName)
        {
            AssemblyName buildName = new AssemblyName(assemblyName);
            buildName.Version = new Version(1, 0);
            AssemblyBuilder kd = AppDomain.CurrentDomain.DefineDynamicAssembly(buildName, AssemblyBuilderAccess.Save);
            var rootModule = kd.DefineDynamicModule(assemblyName + ".dll");
            return Tuple.Create(kd, rootModule);
        }

        static void Main(string[] args)
        {
            //ReprintExpressionVisitor();
            //return;
            //OldTest2();
            //return;
            //CliCommon.mscorlibIdentifierv4
            var timedDCAT = MiscHelperMethods.CreateActionOfTime(DoCreateAssemblyTest);
            Console.WriteLine("Press any key to perform the initial test. . .");
            Console.ReadKey(true);
            var initial = timedDCAT();
            Console.WriteLine("Press any key to perform the second test. . .");
            Console.ReadKey(true);
            var secondary = timedDCAT();
            Console.WriteLine();
            Console.WriteLine("Results:\r\n\tInitial: {0}ms.\r\n\r\n\tSecondary: {1}ms.", initial.TotalMilliseconds, secondary.TotalMilliseconds);
            identityManager.Dispose();
        }

        private static void DoCreateAssemblyTest()
        {
            //var dd = 3

            var resultProject = GenerateAssembly("testAssembly");
            MyVisualBasicClass testClass = (MyVisualBasicClass)resultProject.Classes.Add("TestClass1");
            //var m = MicrosoftLanguageVendor.StandardLibraryIdentifiers.GetVersionedMicrosoftVisualBasicLibrary(VisualBasicVersion.Version09);
            //var compressedArray = new System.Byte[1216] { 0x1F, 0x8B, 0x08, 0x00, 0x00, 0x00, 0x00, 0x00, 0x04, 0x00, 0xED, 0xD5, 0x65, 0x77, 0x16, 0x04, 0x18, 0x87, 0xF1, 0x67, 0x8C, 0x6E, 0x46, 0x37, 0x8C, 0x8E, 0x51, 0x63, 0x74, 0x33, 0x46, 0x8E, 0xD1, 0x39, 0xBA, 0x06, 0xDB, 0xE8, 0xEE, 0x8E, 0xD1, 0x28, 0x28, 0x61, 0x4B, 0x28, 0x98, 0x34, 0x06, 0x28, 0x8D, 0x1D, 0x60, 0x82, 0x41, 0x2A, 0x61, 0xD0, 0xED, 0xF5, 0x9C, 0xE3, 0x17, 0xE0, 0xFD, 0xF5, 0xE2, 0x77, 0xFE, 0x5F, 0xE0, 0xBE, 0xCF, 0x15, 0x08, 0x04, 0x02, 0x21, 0x48, 0x85, 0x50, 0xA4, 0x46, 0x1A, 0xA4, 0x45, 0x3A, 0xA4, 0x47, 0x06, 0x64, 0x44, 0x26, 0x64, 0x46, 0x16, 0x64, 0x45, 0x36, 0x64, 0x47, 0x0E, 0x84, 0x21, 0x27, 0x72, 0x21, 0x37, 0xF2, 0x20, 0x2F, 0xF2, 0x21, 0x3F, 0x0A, 0xA0, 0x20, 0x0A, 0xA1, 0x30, 0x8A, 0xA0, 0x28, 0x8A, 0xA1, 0x38, 0xC2, 0x51, 0x02, 0x25, 0x51, 0x0A, 0xA5, 0x51, 0x06, 0x65, 0x51, 0x0E, 0xE5, 0x51, 0x01, 0x15, 0x11, 0x81, 0x4A, 0xA8, 0x8C, 0x2A, 0xA8, 0x8A, 0x6A, 0x88, 0x44, 0x75, 0x44, 0xA1, 0x06, 0x6A, 0xA2, 0x16, 0x6A, 0xA3, 0x0E, 0xEA, 0xA2, 0x1E, 0xEA, 0xA3, 0x01, 0x1A, 0xA2, 0x11, 0x1A, 0xA3, 0x09, 0x9A, 0x22, 0x1A, 0xCD, 0x10, 0x83, 0xE6, 0x68, 0x81, 0x96, 0x68, 0x85, 0xD6, 0x68, 0x83, 0x58, 0xB4, 0x45, 0x1C, 0xDA, 0xA1, 0x3D, 0x3A, 0xA0, 0x23, 0x3A, 0xA1, 0x33, 0xBA, 0xA0, 0x2B, 0xBA, 0xA1, 0x3B, 0x7A, 0xA0, 0x27, 0xE2, 0xD1, 0x0B, 0xBD, 0xD1, 0x07, 0x7D, 0xD1, 0x0F, 0xFD, 0x31, 0x00, 0x03, 0x31, 0x08, 0x83, 0x31, 0x04, 0x09, 0x18, 0x8A, 0x61, 0x48, 0x44, 0x12, 0x92, 0x31, 0x1C, 0x23, 0x30, 0x12, 0xA3, 0x30, 0x1A, 0x63, 0x30, 0x16, 0xE3, 0x30, 0x1E, 0x13, 0x30, 0x11, 0x93, 0x30, 0x19, 0x53, 0x30, 0x15, 0xD3, 0x30, 0x1D, 0x33, 0x30, 0x13, 0xB3, 0x30, 0x1B, 0x73, 0x30, 0x17, 0xF3, 0x30, 0x1F, 0x0B, 0xB0, 0x10, 0x8B, 0x90, 0x82, 0xC5, 0x58, 0x82, 0xA5, 0x58, 0x86, 0xE5, 0x58, 0x81, 0x95, 0x58, 0x85, 0xA7, 0xF0, 0x34, 0x56, 0x63, 0x0D, 0x9E, 0xC1, 0xB3, 0x58, 0x8B, 0x75, 0x58, 0x8F, 0x0D, 0x78, 0x0E, 0xCF, 0xE3, 0x05, 0xBC, 0x88, 0x97, 0xF0, 0x32, 0x5E, 0xC1, 0xAB, 0xD8, 0x88, 0x4D, 0xD8, 0x8C, 0x2D, 0x78, 0x0D, 0xAF, 0x63, 0x2B, 0xB6, 0xE1, 0x0D, 0xBC, 0x89, 0xB7, 0xF0, 0x36, 0xDE, 0xC1, 0xBB, 0xD8, 0x8E, 0x1D, 0xD8, 0x89, 0x5D, 0xD8, 0x8D, 0x3D, 0xD8, 0x8B, 0x7D, 0x78, 0x0F, 0xEF, 0xE3, 0x03, 0x7C, 0x88, 0xFD, 0x38, 0x80, 0x8F, 0xF0, 0x31, 0x0E, 0xE2, 0x10, 0x0E, 0xE3, 0x08, 0x8E, 0xE2, 0x18, 0x8E, 0xE3, 0x04, 0x3E, 0xC1, 0xA7, 0xF8, 0x0C, 0x9F, 0xE3, 0x0B, 0x7C, 0x89, 0xAF, 0xF0, 0x35, 0xBE, 0xC1, 0xB7, 0x38, 0x89, 0x53, 0xF8, 0x0E, 0xDF, 0xE3, 0x07, 0xFC, 0x88, 0x9F, 0xF0, 0x33, 0x4E, 0xE3, 0x0C, 0x7E, 0xC1, 0xAF, 0xF8, 0x0D, 0xBF, 0xE3, 0x2C, 0xCE, 0xE1, 0x3C, 0x2E, 0xE0, 0x22, 0x2E, 0xE1, 0x0F, 0xFC, 0x89, 0xCB, 0xB8, 0x82, 0xAB, 0xB8, 0x86, 0xBF, 0xF0, 0x37, 0xFE, 0xC1, 0xBF, 0xB8, 0x8E, 0x1B, 0xB8, 0x89, 0x5B, 0xB8, 0x8D, 0x3B, 0xB8, 0x8B, 0x7B, 0xB8, 0x8F, 0x07, 0x78, 0x88, 0x47, 0x78, 0x8C, 0xE0, 0xF3, 0x87, 0x20, 0x15, 0x42, 0x91, 0x1A, 0x69, 0x90, 0x16, 0xE9, 0x90, 0x1E, 0x19, 0x90, 0x11, 0x99, 0x90, 0x19, 0x59, 0x90, 0x15, 0xD9, 0x90, 0x1D, 0x39, 0x10, 0x86, 0x9C, 0xC8, 0x85, 0xDC, 0xC8, 0x83, 0xBC, 0xC8, 0x87, 0xFC, 0x28, 0x80, 0x82, 0x28, 0x84, 0xC2, 0x28, 0x82, 0xA2, 0x28, 0x86, 0xE2, 0x08, 0x47, 0x09, 0x94, 0x44, 0x29, 0x94, 0x46, 0x19, 0x94, 0x45, 0x39, 0x94, 0x47, 0x05, 0x54, 0x44, 0x04, 0x2A, 0xA1, 0x32, 0xAA, 0xA0, 0x2A, 0xAA, 0x21, 0x12, 0xD5, 0x11, 0x85, 0x1A, 0xA8, 0x89, 0x5A, 0xA8, 0x8D, 0x3A, 0xA8, 0x8B, 0x7A, 0xA8, 0x8F, 0x06, 0x68, 0x88, 0x46, 0x68, 0x8C, 0x26, 0x68, 0x8A, 0x68, 0x34, 0x43, 0x0C, 0x9A, 0xA3, 0x05, 0x5A, 0xA2, 0x15, 0x5A, 0xA3, 0x0D, 0x62, 0xD1, 0x16, 0x71, 0x68, 0x87, 0xF6, 0xE8, 0x80, 0x8E, 0xE8, 0x84, 0xCE, 0xE8, 0x82, 0xAE, 0xE8, 0x86, 0xEE, 0xE8, 0x81, 0x9E, 0x88, 0x47, 0x2F, 0xF4, 0x46, 0x1F, 0xF4, 0x45, 0x3F, 0xF4, 0xC7, 0x00, 0x0C, 0xC4, 0x20, 0x0C, 0xC6, 0x10, 0x24, 0x60, 0x28, 0x86, 0x21, 0x11, 0x49, 0x48, 0xC6, 0x70, 0x8C, 0xC0, 0x48, 0x8C, 0xC2, 0x68, 0x8C, 0xC1, 0x58, 0x8C, 0xC3, 0x78, 0x4C, 0xC0, 0x44, 0x4C, 0xC2, 0x64, 0x4C, 0xC1, 0x54, 0x4C, 0xC3, 0x74, 0xCC, 0xC0, 0x4C, 0xCC, 0xC2, 0x6C, 0xCC, 0xC1, 0x5C, 0xCC, 0xC3, 0x7C, 0x2C, 0xC0, 0x42, 0x2C, 0x42, 0x0A, 0x16, 0x63, 0x09, 0x96, 0x62, 0x19, 0x96, 0x87, 0x04, 0x02, 0xFF, 0x9F, 0x90, 0xFD, 0x08, 0xD8, 0x0F, 0xFB, 0x61, 0x3F, 0xEC, 0x87, 0xFD, 0xB0, 0x1F, 0xF6, 0xC3, 0x7E, 0xD8, 0x0F, 0xFB, 0x61, 0x3F, 0xEC, 0x87, 0xFD, 0xB0, 0x1F, 0xF6, 0xC3, 0x7E, 0xD8, 0x0F, 0xFB, 0x61, 0x3F, 0xEC, 0x87, 0xFD, 0xB0, 0x1F, 0xF6, 0xC3, 0x7E, 0xD8, 0x0F, 0xFB, 0x61, 0x3F, 0xEC, 0x87, 0xFD, 0xB0, 0x1F, 0xF6, 0xC3, 0x7E, 0xD8, 0x0F, 0xFB, 0x61, 0x3F, 0xEC, 0x87, 0xFD, 0xB0, 0x1F, 0xF6, 0xC3, 0x7E, 0xD8, 0x0F, 0xFB, 0x61, 0x3F, 0xEC, 0x87, 0xFD, 0xB0, 0x1F, 0xF6, 0xC3, 0x7E, 0xD8, 0x0F, 0xFB, 0x61, 0x3F, 0xEC, 0x87, 0xFD, 0xB0, 0x1F, 0xF6, 0xC3, 0x7E, 0xD8, 0x0F, 0xFB, 0x61, 0x3F, 0xEC, 0x87, 0xFD, 0x08, 0x8E, 0xFD, 0xB0, 0x1F, 0xF6, 0xC3, 0x7E, 0xD8, 0x0F, 0xFB, 0x61, 0x3F, 0xEC, 0x87, 0xFD, 0xB0, 0x1F, 0xF6, 0xC3, 0x7E, 0xD8, 0x0F, 0xFB, 0x61, 0x3F, 0xEC, 0x87, 0xFD, 0xB0, 0x1F, 0xF6, 0xC3, 0x7E, 0xD8, 0x0F, 0xFB, 0x61, 0x3F, 0xEC, 0x87, 0xFD, 0xB0, 0x1F, 0xF6, 0xC3, 0x7E, 0xD8, 0x0F, 0xFB, 0x61, 0x3F, 0xEC, 0x87, 0xFD, 0xB0, 0x1F, 0xF6, 0xC3, 0x7E, 0xD8, 0x0F, 0xFB, 0x61, 0x3F, 0xEC, 0x87, 0xFD, 0xB0, 0x1F, 0xF6, 0xC3, 0x7E, 0xD8, 0x0F, 0xFB, 0x61, 0x3F, 0xEC, 0x87, 0xFD, 0xB0, 0x1F, 0xF6, 0xC3, 0x7E, 0xD8, 0x0F, 0xFB, 0x61, 0x3F, 0xEC, 0x87, 0xFD, 0xB0, 0x1F, 0xF6, 0xC3, 0x7E, 0xD8, 0x0F, 0xFB, 0x11, 0x1C, 0xFB, 0x61, 0x3F, 0xEC, 0x87, 0xFD, 0xB0, 0x1F, 0xF6, 0xC3, 0x7E, 0xD8, 0x0F, 0xFB, 0x61, 0x3F, 0xEC, 0x87, 0xFD, 0xB0, 0x1F, 0xF6, 0xC3, 0x7E, 0xD8, 0x0F, 0xFB, 0x61, 0x3F, 0xEC, 0x87, 0xFD, 0xB0, 0x1F, 0xF6, 0xC3, 0x7E, 0xD8, 0x0F, 0xFB, 0x61, 0x3F, 0xEC, 0x87, 0xFD, 0xB0, 0x1F, 0xF6, 0xC3, 0x7E, 0xD8, 0x0F, 0xFB, 0x61, 0x3F, 0xEC, 0x87, 0xFD, 0xB0, 0x1F, 0xF6, 0xC3, 0x7E, 0xD8, 0x0F, 0xFB, 0x61, 0x3F, 0xEC, 0x87, 0xFD, 0xB0, 0x1F, 0xF6, 0xC3, 0x7E, 0xD8, 0x0F, 0xFB, 0x61, 0x3F, 0xEC, 0x87, 0xFD, 0xB0, 0x1F, 0xF6, 0xC3, 0x7E, 0xD8, 0x0F, 0xFB, 0x61, 0x3F, 0xEC, 0x87, 0xFD, 0xB0, 0x1F, 0xF6, 0x23, 0x38, 0xF6, 0xC3, 0x7E, 0xD8, 0x0F, 0xFB, 0x61, 0x3F, 0xEC, 0x87, 0xFD, 0xB0, 0x1F, 0xF6, 0xC3, 0x7E, 0xD8, 0x0F, 0xFB, 0x61, 0x3F, 0xEC, 0x87, 0xFD, 0xB0, 0x1F, 0xF6, 0xC3, 0x7E, 0xD8, 0x0F, 0xFB, 0x61, 0x3F, 0xEC, 0x87, 0xFD, 0xB0, 0x1F, 0xF6, 0xC3, 0x7E, 0xD8, 0x0F, 0xFB, 0x61, 0x3F, 0xEC, 0x87, 0xFD, 0xB0, 0x1F, 0xF6, 0xC3, 0x7E, 0xD8, 0x0F, 0xFB, 0x61, 0x3F, 0xEC, 0x87, 0xFD, 0xB0, 0x1F, 0xF6, 0xC3, 0x7E, 0xD8, 0x0F, 0xFB, 0x61, 0x3F, 0xEC, 0x87, 0xFD, 0xB0, 0x1F, 0xF6, 0xC3, 0x7E, 0xD8, 0x0F, 0xFB, 0x61, 0x3F, 0xEC, 0x87, 0xFD, 0xB0, 0x1F, 0xF6, 0xC3, 0x7E, 0xD8, 0x0F, 0xFB, 0x61, 0x3F, 0xEC, 0x47, 0x70, 0xEC, 0x87, 0xFD, 0xB0, 0x1F, 0xF6, 0xC3, 0x7E, 0xD8, 0x0F, 0xFB, 0x61, 0x3F, 0xEC, 0x87, 0xFD, 0xB0, 0x1F, 0xF6, 0xC3, 0x7E, 0xD8, 0x0F, 0xFB, 0x61, 0x3F, 0xEC, 0x87, 0xFD, 0xB0, 0x1F, 0xF6, 0xC3, 0x7E, 0xD8, 0x0F, 0xFB, 0x61, 0x3F, 0xEC, 0x87, 0xFD, 0xB0, 0x1F, 0xF6, 0xC3, 0x7E, 0xD8, 0x0F, 0xFB, 0x61, 0x3F, 0xEC, 0x87, 0xFD, 0xB0, 0x1F, 0xF6, 0xC3, 0x7E, 0xD8, 0x0F, 0xFB, 0x61, 0x3F, 0xEC, 0x87, 0xFD, 0xB0, 0x1F, 0xF6, 0xC3, 0x7E, 0xD8, 0x0F, 0xFB, 0x61, 0x3F, 0xEC, 0x87, 0xFD, 0xB0, 0x1F, 0xF6, 0xC3, 0x7E, 0xD8, 0x0F, 0xFB, 0x61, 0x3F, 0xEC, 0x87, 0xFD, 0xB0, 0x1F, 0xF6, 0xC3, 0x7E, 0xD8, 0x8F, 0xE0, 0xD8, 0x0F, 0xFB, 0x61, 0x3F, 0xEC, 0x87, 0xFD, 0x78, 0xB2, 0x7E, 0xFC, 0x07, 0xEC, 0xE5, 0x41, 0x29, 0x00, 0x44, 0x01, 0x00 };
            //Random r = new Random();
            var keda = resultProject.MyNamespace.MyApplication;
            var kdea = resultProject.MyNamespace.MyComputer;
            var screenOverload = kdea.Properties.Add(new TypedName("Screen", RuntimeCoreType.Int32, identityManager, resultProject), true, false);
            screenOverload.GetMethod.Return(0.ToPrimitive());
            var kdeaS = kdea.Screen;
            screenOverload.AccessLevel = AccessLevelModifiers.Public;
            var kedaR = keda.Culture.GetPropertyReference(new SpecialReferenceExpression(SpecialReferenceKind.Self));
            var kdeaSR = kdeaS.GetPropertyReference(new SpecialReferenceExpression(SpecialReferenceKind.Self));

            kdea.DeclaringModule = resultProject.Modules.Add("TestModule1");
            //object indexLocker = new object();
            //var decomp = StandardCompilerAids.ConvertByteArrayToInt32Array(12, 12, 12, 12, compressedArray);
            //var dke = decomp[11, 11, 11, 11];
            //var version9 = identityManager.ObtainAssemblyReference(m);
            //version9.AssemblyInformation.Copyright.ToString();
            //var ms = (from m in new[] { VisualBasicVersion.Version07, VisualBasicVersion.Version08, VisualBasicVersion.Version09, VisualBasicVersion.Version10, VisualBasicVersion.Version11 }
            //          let id = MicrosoftLanguageVendor.StandardLibraryIdentifiers.GetVersionedMicrosoftVisualBasicLibrary(m)
            //          select new { LanguageVersion = m, Identifier = id, Assembly = identityManager.ObtainAssemblyReference(id) }).ToArray();
            //var k = testClass.CommonVBTypeRefs.StandardModuleAttribute;
            testClass.SpecialModifier = SpecialClassModifier.HiddenModule | SpecialClassModifier.TypeExtensionSource;
            var ue = testClass.IdentityManager;
            var tcParam = testClass.TypeParameters.Add("TCParam01");
            var tcParamProperty = tcParam.Properties.Add(new TypedName("__Test_", RuntimeCoreType.Int32, identityManager, resultProject), true, false);
            resultProject.Dispose();
        }



        private static IMyVisualBasicAssembly GenerateAssembly(string name)
        {
            var resultProject = LanguageVendors.Microsoft.GetVisualBasicLanguage().GetMyProvider(identityManager).CreateAssembly(name);
            resultProject.References.Add(identityManager.ObtainAssemblyReference(CliGateway.GetCoreLibraryIdentifier(identityManager.RuntimeEnvironment.Version)));
            resultProject.AssemblyInformation.Culture = CultureIdentifiers.English_UnitedKingdom;
            resultProject.AssemblyInformation.AssemblyVersion = new IntermediateVersion(1, 0, 0) { AutoIncrementRevision = true };
            resultProject.AssemblyInformation.Company = "None";
            resultProject.AssemblyInformation.Copyright = "Copyright © 2013 Allen C. Copeland, Jr.";
            return resultProject;
        }

        private static void MTExamp()
        {
            var folderBase = @"C:\Windows\Microsoft.NET\Framework\";
            //var folderBase64 = @"C:\Windows\Microsoft.NET\Framework\";

            var data = new[] { Tuple.Create(CliFrameworkVersion.v1_0_3705, CliFrameworkPlatform.x86Platform, folderBase + Version1String), Tuple.Create(CliFrameworkVersion.v1_1_4322, CliFrameworkPlatform.x86Platform, folderBase + Version1_1String), Tuple.Create(CliFrameworkVersion.v2_0_50727, CliFrameworkPlatform.x86Platform, folderBase + Version2String), Tuple.Create(CliFrameworkVersion.v3_0, CliFrameworkPlatform.x86Platform, folderBase + Version3String), Tuple.Create(CliFrameworkVersion.v3_5, CliFrameworkPlatform.x86Platform, folderBase + Version3_5String), Tuple.Create(CliFrameworkVersion.v4_0_30319, CliFrameworkPlatform.x86Platform, folderBase + Version4String) };
            
            //var data64 = new[] { Tuple.Create(CliFrameworkVersion.v2_0_50727, CliFrameworkPlatform.x64Platform, folderBase64 + Version2String), Tuple.Create(CliFrameworkVersion.v3_0, CliFrameworkPlatform.x64Platform, folderBase64 + Version3String), Tuple.Create(CliFrameworkVersion.v3_5, CliFrameworkPlatform.x64Platform, folderBase64 + Version3_5String), Tuple.Create(CliFrameworkVersion.v4_0_30319, CliFrameworkPlatform.x64Platform, folderBase64 + Version4String) };
            //var mvassem = (from folder in data//.Concat(data64)
            //               let filename = folder.Item3 + Path.DirectorySeparatorChar + "mscorlib.dll"
            //               where File.Exists(filename)
            //               let assem = identityManager.ObtainAssemblyReference(filename)
            //               orderby folder.Item1, folder.Item2
            //               select Tuple.Create(folder.Item1, assem)).ToArray();

            string typeString = "AllenCopeland.Abstraction.Slf.Ast.Expressions.IExpressionVisitor`2[[System.Boolean, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089], [AllenCopeland.Abstraction.Slf.Translation.IIntermediateCodeTranslatorOptions, _abs.slf.ast, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]][], _abs.slf.ast, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
            var type = (IArrayType)CliGateway.ParseTypeIdentifier(typeString, identityManager);
            var tet = type.ElementType;
            var dkt = ((IInterfaceType)tet).Methods[27].Value.Parameters[0].Value;
            var dktT = dkt.ParameterType;
            //var keddee = tet.Declarations
            var qued = type.ElementType.Members.Values;
            var im = identityManager.ObtainTypeReference(RuntimeCoreType.Decimal, type.Assembly).ImplementedInterfaces.ToArray();
            var im2 = identityManager.GetCoreTypeInterfaces(RuntimeCoreType.Decimal, type.Assembly).ToArray();
            var dedd = type.ImplementedInterfaces.ToArray();
            var m = Type.GetType(typeString).GetInterfaces();
            var deee = Type.GetType("System.Array").GetInterfaces();
            //Console.WriteLine(mvauid);
        }

        private static void ReprintExpressionVisitor()
        {

            var types = (from t in new[] { typeof(IExpressionVisitor), typeof(IExpressionVisitor<bool, IIntermediateCodeTranslatorOptions>), typeof(ILinqVisitor), typeof(IStatementVisitor), typeof(IPrimitiveVisitor), typeof(IIntermediateCodeVisitor), typeof(IIntermediateMemberVisitor), typeof(IIntermediateTypeVisitor), typeof(IIntermediateDeclarationVisitor), typeof(IIntermediateInclusionVisitor) }
                         select (IInterfaceType)identityManager.ObtainTypeReference(t)).ToArray();
            var expAssem = types[0].Assembly;
            var intermediateExpAssem = LanguageVendors.Microsoft.GetCSharpLanguage().CreateAssembly(expAssem.Name);
            intermediateExpAssem.AssemblyInformation.AssemblyVersion.Major = 2;
            Dictionary<string, IIntermediateNamespaceDeclaration> nsLookup = new Dictionary<string, IIntermediateNamespaceDeclaration>();
            for (int i = 0; i < types.Length; i++)
            {
                var currentType = types[i];
                var currentNSName = currentType.NamespaceName;
                IIntermediateNamespaceDeclaration currentNamespace;
                if (!nsLookup.TryGetValue(currentNSName, out currentNamespace))
                    nsLookup.Add(currentNSName, currentNamespace = intermediateExpAssem.Namespaces.PathExists(currentNSName) ? intermediateExpAssem.Namespaces[currentNSName] : intermediateExpAssem.Namespaces.Add(currentNSName));
                RecreateInterface(currentType, currentNamespace);
            }
            WriteProject(intermediateExpAssem, extension: ".cs");
        }

        private static void RecreateInterface(IInterfaceType @interface, IIntermediateNamespaceDeclaration targetNamespace)
        {
            var orderedMembers = (from meth in @interface.Methods.Values
                                  let id = meth.UniqueIdentifier
                                  orderby id.Name, id.ParameterCount, string.Join(", ", from t in meth.Parameters.ParameterTypes
                                                                                        select t.Name)
                                  group meth by meth.Name).ToDictionary(k => k.Key, v => v.ToArray());
            bool isNonGeneric = !(@interface.IsGenericConstruct && @interface.IsGenericDefinition);
            var genericTypeParamSymbols = GetGenericParameterSymbols(from tp in isNonGeneric ? (IEnumerable<IGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, IInterfaceType>>)(new IGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, IInterfaceType>[0]) : @interface.TypeParameters.Values
                                                                     select tp);
            var genericTypeParameterDefs = isNonGeneric ? new GenericParameterData[0] :
                                           from typeParam in @interface.TypeParameters.Values
                                           select new GenericParameterData(typeParam.Name, (from c in typeParam.Constraints
                                                                                            select c.Disambiguify(genericTypeParamSymbols, null, TypeParameterSources.Type)).ToArray());
            string name = @interface.Name;
            if (@interface.IsGenericConstruct && !@interface.IsGenericDefinition)
            {
                name = string.Format("{0}_Of_{1}", name, string.Join("_", from gp in @interface.GenericParameters
                                                                          select gp.Name));
            }
            var interInterface = targetNamespace.Interfaces.Add(name, genericTypeParameterDefs.ToArray());
            foreach (var methodName in orderedMembers.Keys)
                foreach (var method in orderedMembers[methodName])
                {
                    bool isMethodNonGeneric = !method.IsGenericConstruct;
                    var genericMethodParamSymbols = GetGenericParameterSymbols(from tp in isNonGeneric ? (IEnumerable<IMethodSignatureGenericTypeParameterMember>)(new IMethodSignatureGenericTypeParameterMember[0]) : method.TypeParameters.Values
                                                                               select tp);

                    var genericMethodParameterDefs = isMethodNonGeneric ? new GenericParameterData[0] :
                                                     from typeParam in method.TypeParameters.Values
                                                     select new GenericParameterData(typeParam.Name, (from c in typeParam.Constraints
                                                                                                      select c.Disambiguify(genericTypeParamSymbols, genericMethodParamSymbols, TypeParameterSources.Both)).ToArray());
                    var retType = method.ReturnType.Disambiguify(genericTypeParamSymbols, genericMethodParamSymbols, TypeParameterSources.Both);
                    var methodParameterDefs = new TypedNameSeries((from parameter in method.Parameters.Values
                                                                   select new TypedName(parameter.Name, parameter.ParameterType.Disambiguify(genericTypeParamSymbols, genericMethodParamSymbols, TypeParameterSources.Both))).ToArray());
                    interInterface.Methods.Add(new TypedName(methodName, retType), methodParameterDefs, genericMethodParameterDefs.ToArray());
                }
        }

        private static ITypeCollection GetGenericParameterSymbols(IEnumerable<IGenericParameter> parameters)
        {
            return (from parameter in parameters
                    select parameter.Name.GetSymbolType()).ToCollection();
        }

        private static void Test3()
        {
            //CreateAssemblyBuilder("TestAssembly").Split(out ab, out rm);
            //var testType = rm.DefineType("test", TypeAttributes.Class | TypeAttributes.AnsiClass | TypeAttributes.BeforeFieldInit | TypeAttributes.AutoLayout | TypeAttributes.Public | TypeAttributes.Sealed);

            //var testMethod = testType.DefineMethod("testMethod", MethodAttributes.Public | MethodAttributes.Virtual | MethodAttributes.NewSlot | MethodAttributes.HideBySig, CallingConventions.Standard | CallingConventions.HasThis, typeof(void), new Type[1] { typeof(IsConst) }, null, null, null, null);
            //var tmILG = testMethod.GetILGenerator();
            //tmILG.Emit(OpCodes.Ret);
            //testType.CreateType();
            //rm.CreateGlobalFunctions();
            //ab.Save("TestAssembly.dll", PortableExecutableKinds.ILOnly | PortableExecutableKinds.Required32Bit, ImageFileMachine.IA64);

            var tdmt = MiscHelperMethods.CreateFunctionOfTime(DiscernModifiedTypes);
            var tr1 = tdmt();
            identityManager.Dispose();
            identityManager = CreateIDM(); //IntermediateGateway.CreateIdentityManager(CliGateway.CurrentPlatform, CliGateway.CurrentVersion, true);
            var tr2 = tdmt();
            var tr3 = tdmt();
            var r1 = tr1.Item2;
            var r2 = tr2.Item2;
            var r3 = tr3.Item2;
            Console.WriteLine("Iterating all types and all methods with thier parameters (initial) took {0}ms.\r\nLibrary: {1}\r\nTypes: {2}, Methods: {3}, Parameters: {4}\r\nAverage Parameters per Method: {5}\r\nAverage method count on types containing methods: {6}", tr1.Item1.TotalMilliseconds, r1.Item1, r1.Item2, r1.Item3, r1.Item4, ((double)r1.Item4 / (double)r1.Item3), ((double)r1.Item3 / (double)r1.Item5));
            Console.WriteLine();
            Console.WriteLine("Iterating all types and all methods with thier parameters (secondary, fresh) took {0}ms.\r\nLibrary: {1}\r\nTypes: {2}, Methods: {3}, Parameters: {4}\r\nAverage Parameters per Method: {5}\r\nAverage method count on types containing methods: {6}", tr2.Item1.TotalMilliseconds, r2.Item1, r2.Item2, r2.Item3, r2.Item4, ((double)r2.Item4 / (double)r2.Item3), ((double)r2.Item3 / (double)r2.Item5));
            Console.WriteLine();
            Console.WriteLine("Iterating all types and all methods with thier parameters (tertiary, memory) took {0}ms.\r\nLibrary: {1}\r\nTypes: {2}, Methods: {3}, Parameters: {4}\r\nAverage Parameters per Method: {5}\r\nAverage method count on types containing methods: {6}", tr3.Item1.TotalMilliseconds, r3.Item1, r3.Item2, r3.Item3, r3.Item4, ((double)r3.Item4 / (double)r3.Item3), ((double)r3.Item3 / (double)r3.Item5));
            return;
            //TestMethod01();
            //return;
        }

        private static void Extract001()
        {
            //AssemblyBuilder ab;
            //ModuleBuilder rm;
            var timedArrayCreator = MiscHelperMethods.CreateFunctionOfTime(ArrayCreator);
            var createdArrayAndTime = timedArrayCreator();
            var ar = createdArrayAndTime.Item2;
            var m = ar.ToExpression(identityManager);
            var originalStream = m.ConvertToByteArray(identityManager);
            var compressedOriginal = originalStream.CompressByteStream();
            var decompressedData = compressedOriginal.DecompressByteStream(originalStream.Length);
            var data = StandardCompilerAids.ConvertByteArrayToInt32Array(ar.GetLength(0), ar.GetLength(1), compressedOriginal);
            Action k1 = () =>
            {
                bool b = ar.Cast<int>().SequenceEqual(data.Cast<int>());
            };
            Action k2 = () =>
            {
                bool b = ar.SequenceEqual(data);
            };
            Action k3 = () =>
            {
                bool b = ar.SequenceEqualA(data);
            };
            var bnds = ar.GetBounds();
            var bnds2 = data.GetBounds();
            var bndsEq = bnds.Equals(bnds2);
            var tAk1 = MiscHelperMethods.CreateActionOfTime(k1);
            var tAk2 = MiscHelperMethods.CreateActionOfTime(k2);
            var tAk3 = MiscHelperMethods.CreateActionOfTime(k3);
            var tk1 = tAk1();
            var tk2 = tAk2();
            var tk3 = tAk3();
            Console.WriteLine("Cast with standard sequence equal: {0}\r\nManually written sequence equal: {1}\r\nExplicitly written sequence equal: {2}", tk1, tk2, tk3);
            return;
            TestDialog();
        }

        private static void TestDialog()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(true);
            Application.Run(new AbstractionTestDialog());
        }

        private static void WriteProject(IIntermediateAssembly target, IIntermediateCodeTranslatorFormatterProvider formatterProvider = null, string extension = null)
        {
            string relativeRoot = Path.GetDirectoryName(typeof(Program).Assembly.Location);

            var cstr = new CSharpCodeTranslator();
            var fileOut = string.Format("{0}{1}{2}.{3}", relativeRoot, Path.DirectorySeparatorChar.ToString(), "results", extension);
            var fileStream = new FileStream(fileOut, FileMode.Create, FileAccess.Write, FileShare.Read);
            var streamWriter = new StreamWriter(fileStream);
            streamWriter.AutoFlush = true;
            if (formatterProvider != null)
                cstr.FormatProvider = formatterProvider;
            cstr.Options.ElementOrderingMethod = TranslationOrderKind.Specific | TranslationOrderKind.Alphabetic;
            cstr.Options.TranslationOrder.Add(DeclarationTranslationOrder.Enums);
            cstr.Options.TranslationOrder.Add(DeclarationTranslationOrder.Classes);
            cstr.Options.TranslationOrder.Add(DeclarationTranslationOrder.Interfaces);
            cstr.Target = new IndentedTextWriter(streamWriter);
            cstr.Translate(target);
            streamWriter.Flush();
            streamWriter.Dispose();
            fileStream.Close();
            fileStream.Dispose();
        }

        private static Tuple<IAssemblyUniqueIdentifier, int, int, int, int> DiscernModifiedTypes()
        {
            var mc = 0;
            var pc = 0;
            var tc = 0;
            var mpt = 0;
            var coreAssem = identityManager.ObtainAssemblyReference(TypeSystemIdentifiers.GetAssemblyIdentifier("mscorlib", TypeSystemIdentifiers.GetVersion(1, 0, 3300), CultureIdentifiers.None, StrongNameKeyPairHelper.StandardPublicKeyToken));
            foreach (var type in coreAssem.GetTypes())
            {
                tc++;
                if (type.Type == TypeKind.Modified)
                {

                }
                else if (type is IMethodSignatureParent)
                {
                    mpt++;
                    //Console.WriteLine("Type: {0}", type.UniqueIdentifier);
                    var msParent = (IMethodSignatureParent)type;
                    foreach (IMethodSignatureMember method in msParent.Methods.Values)
                    {
                        mc++;
                        //Console.WriteLine("\tMethod:{0}", method.UniqueIdentifier);
                        foreach (IMethodSignatureParameterMember mP in method.Parameters.Values)
                        {
                            pc++;
                            if (mP.ParameterType.Type == TypeKind.Modified)
                            {

                            }
                        }
                    }
                }
                else if (type is IMethodParent)
                {
                    mpt++;
                    //Console.WriteLine("Type: {0}", type.UniqueIdentifier);
                    var msParent = (IMethodParent)type;
                    foreach (IMethodSignatureMember method in msParent.Methods.Values)
                    {
                        mc++;
                        //Console.WriteLine("\tMethod:{0}", method.UniqueIdentifier);
                        foreach (IMethodSignatureParameterMember mP in method.Parameters.Values)
                        {
                            pc++;
                            if (mP.ParameterType.Type == TypeKind.Modified)
                            {
                            }
                        }
                        if (method.ReturnType.Type == TypeKind.Modified)
                        {
                            Console.WriteLine(method.ReturnType);
                        }
                    }
                }
                if (type is ICreatableParent)
                {
                    if (!(type is IMethodSignatureParent ||
                          type is IMethodParent))
                        mpt++;
                    var cp = (ICreatableParent)type;
                    foreach (IConstructorMember ctor in cp.Constructors.Values)
                    {
                        mc++;
                        foreach (IConstructorParameterMember cParam in ctor.Parameters.Values)
                        {
                            if (cParam.ParameterType.Type == TypeKind.Modified)
                            {
                            }
                            pc++;
                        }
                    }
                }
            }
            var r = Tuple.Create(coreAssem.UniqueIdentifier, tc, mc, pc, mpt);
            return r;
        }

        private static int[,] ArrayCreator()
        {
            var ar = new int[1024, 1024];
            int i = 0;
            object ilock = new object();

            Parallel.ForEach(ar.Iterate(false).ToArray().Shuffle().AsParallel(), indices =>
            {
                var lr = indices.Breakdown();
                var a0 = lr.Item1;
                var a1 = lr.Item2;
                lock (ilock)
                    ar[a0, a1] = i++;
            });
            return ar;
        }

        private static bool SequenceEqualA<T>(this T[,] source, T[,] compare)
        {
            if (source == null && compare != null ||
                source != null && compare == null)
                return false;
            if (source == null && compare == null)
                return true;
            var sourceBounds = source.GetBounds();
            var compareBounds = compare.GetBounds();
            if (!sourceBounds.Equals(compareBounds))
                return false;
            for (int i = sourceBounds.LowerBounds[0]; i < sourceBounds.UpperBounds[0]; i++)
                for (int j = sourceBounds.LowerBounds[1]; j < sourceBounds.UpperBounds[1]; j++)
                    if (!source[i, j].Equals(compare[i, j]))
                        return false;
            return true;
        }

        private static bool SequenceEqual<T>(this T[,] source, T[,] compare)
        {
            if (!ArraysAreNotEquivalent(source, compare))
                return false;
            foreach (var indices in source.Iterate())
                if (!source[indices[0], indices[1]].Equals(compare[indices[0], indices[1]]))
                    return false;
            return true;
        }

        private static bool ArraysAreNotEquivalent(this Array source, Array compare)
        {
            if (source == null && compare != null ||
                source != null && compare == null)
                return false;
            if (source == null && compare == null)
                return true;
            if (!source.GetBounds().Equals(compare.GetBounds()))
                return false;
            return true;
        }

        private static bool SequenceEqual(this Array source, Array compare)
        {
            if (!ArraysAreNotEquivalent(source, compare))
                return false;
            foreach (var indices in source.Iterate())
                if (!source.GetValue(indices).Equals(compare.GetValue(indices)))
                    return false;
            return true;
        }

        private static Tuple<T, T> Breakdown<T>(this T[] target)
        {
            if (target == null)
                throw new ArgumentNullException("target");
            if (target.Length < 2)
                throw new ArgumentOutOfRangeException("target");
            return new Tuple<T, T>(target[0], target[1]);
        }

        private static void OldTest2()
        {
        ReTest:
            Console.WriteLine("Running Abstraction Test...");
            CliTypeSystemTest();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            Console.ReadKey(true);
            Console.WriteLine();
            Console.WriteLine("Running Reflection Test...");
            ReflectionTest();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            Console.ReadKey(true);
            Console.Clear();
            goto ReTest;
        }

        private static void SimpleTest()
        {
            var timeGetInterfaces = MiscHelperMethods.CreateActionOfTime<RuntimeCoreType>(GetInterfaces);
            var initial = timeGetInterfaces(RuntimeCoreType.Array);
            var secondary = timeGetInterfaces(RuntimeCoreType.Int64);
            var tertiary = timeGetInterfaces(RuntimeCoreType.Int32);
            var quaternary = timeGetInterfaces(RuntimeCoreType.Int32);
            var baseDefTestTime = MiscHelperMethods.CreateActionOfTime(GetBaseDefinitionTest);
            var baseDef1 = baseDefTestTime();
            var baseDef2 = baseDefTestTime();
            Console.WriteLine("Base definition on a generic type's (_DelegateTypeBase) method (OnGetUniqueIdentifier) took {0}ms", baseDef1.TotalMilliseconds);
            Console.WriteLine("Second base definition on a generic type's (_DelegateTypeBase) method (OnGetUniqueIdentifier) took {0}ms", baseDef2.TotalMilliseconds);
            Console.WriteLine("{0} ms initial (Array), {1} ms secondary (Int64), {2} ms tertiarally (Int32, 1), {3} ms quaternarily (Int32, 2).", initial.TotalMilliseconds, secondary.TotalMilliseconds, tertiary.TotalMilliseconds, quaternary.TotalMilliseconds);
            var disposeTime = MiscHelperMethods.CreateActionOfTime(identityManager.Dispose)();
            Console.WriteLine("Identity manager disposal took {0} ms.", disposeTime.TotalMilliseconds);
            return;
        }

        private static void GetBaseDefinitionTest()
        {
            var cra = (IClassType)typeof(_DelegateTypeBase).GetTypeReference(identityManager);
            var topLevel = cra.Methods[TypeSystemIdentifiers.GetGenericSignatureIdentifier("OnGetUniqueIdentifier")];
            var baseDef = topLevel.BaseDefinition;
        }

        private static void GetInterfaces(RuntimeCoreType target)
        {
            var ke = identityManager.GetCoreTypeInterfaces(target).ToArray();
        }

        private static void TestMethod01()
        {
            var coreId = identityManager.RuntimeEnvironment.CoreLibraryIdentifier;
            var tiair = new TIAssemblyIdentityRule(coreId.Name, new TIVersionRule(coreId.Version.Major, coreId.Version.Minor, coreId.Version.Build, coreId.Version.Revision), coreId.Culture, coreId.PublicKeyToken);
            Console.WriteLine("Creating types.");
            var typeNames = (from t in typeof(int).Assembly.GetTypes()
                             from q in typeof(IType).Assembly.GetTypes()
                             where !(t.IsGenericParameter || q.IsGenericParameter)
                             where !(t.IsGenericTypeDefinition || q.IsGenericTypeDefinition)
                             where !(t.IsSealed || q.IsSealed)
                             select typeof(Tuple<,>).MakeGenericType(t, q).AssemblyQualifiedName).ToArray();
            ITIQualifiedTypeNameRule[] parsedTypeNames = new ITIQualifiedTypeNameRule[typeNames.Length];
            Console.WriteLine("Parsing assembly qualified names.");
            var sum = (from n in typeNames
                       select n.Length).Sum();
            Stopwatch sw = Stopwatch.StartNew();
            for (int i = 0; i < typeNames.Length; i++)
            {
                var type = typeNames[i];
                TypeIdentityParser tip = new TypeIdentityParser(type, tiair);
                parsedTypeNames[i] = tip.ParseQualifiedTypeName();
            }
            sw.Stop();
            Console.WriteLine("Took {0} to parse {1:#,#} names in {2:#,#} bytes.", sw.Elapsed, typeNames.Length, sum);
        }

        private static void MemberTest()
        {
            var identityManager = CliGateway.CreateIdentityManager(CliGateway.CurrentPlatform, CliGateway.CurrentVersion);
            var m = (IClassType)identityManager.ObtainTypeReference(typeof(IntermediateClassType));
            Console.WriteLine(m.Members.Count);
            var gmTF = MiscHelperMethods.CreateFunctionOfTime<IClassType, IScopedDeclaration[]>(GetMembers);
            var first = gmTF(m);
            var second = gmTF(m);
            foreach (IScopedDeclaration decl in first.Item2)
                Console.WriteLine("{0} {1}\n", decl.AccessLevel, decl);

            Console.WriteLine("Pre JIT-retrieval: {0}ms", first.Item1.TotalMilliseconds);
            Console.WriteLine("Post JIT-retrieval: {0}ms", second.Item1.TotalMilliseconds);
        }

        private static IScopedDeclaration[] GetMembers(IClassType m)
        {
            var query = (from IScopedDeclaration member in m.GetAvailableMembersFor(AccessLevelModifiers.ProtectedAndInternal)
                         orderby member.AccessLevel ascending
                         select member).ToArray();
            return query;
        }

        private static void TypeParamsQuery()
        {
            var identityManager = IntermediateCliGateway.CreateIdentityManager(CliGateway.CurrentPlatform, CliGateway.CurrentVersion);
            var assemblies = (from t in new[] { typeof(IType), typeof(ICliType), typeof(IIntermediateAssembly), typeof(IControlledCollection), typeof(Program) }
                              select identityManager.ObtainAssemblyReference(t.Assembly)).ToArray();

            var typesQuery = (from a in assemblies
                              from t in a.GetTypes()
                              where t.IsGenericConstruct && t is IGenericType
                              let genericT = (IGenericType)t
                              from IGenericTypeParameter g in genericT.TypeParameters.Values
                              group g by t);
            foreach (var genericType in typesQuery)
            {
                Console.Write(genericType.Key.Name);
                Console.Write('<');
                bool first = true;
                foreach (var genericParameter in genericType)
                {
                    if (first)
                        first = false;
                    else
                        Console.Write(", ");
                    Console.Write(genericParameter.Name);
                }
                Console.WriteLine('>');
                foreach (var genericParameter in genericType)
                {
                    if (genericParameter.Constraints.Count > 0)
                    {
                        first = true;
                        Console.WriteLine("\twhere {0} :", genericParameter);
                        foreach (var constraint in genericParameter.Constraints)
                        {
                            if (first)
                                first = false;
                            else
                                Console.WriteLine(',');
                            Console.Write("\t\t{0}", constraint);
                        }
                        Console.WriteLine();
                    }
                }
            }
        }

        private static void ReflectionTest()
        {
            /* *
             * Don't try to beat reflection on the assemblies it has loaded already:
             * the default libraries are likely loaded upon runtime load.
             * */
            var assemblies = (from t in new[] { typeof(IControlledCollection), typeof(IType), typeof(ICliType), typeof(IIntermediateAssembly), typeof(ImageComboBox), typeof(Program), typeof(Form) }
                              select t.Assembly).ToArray();
            Stopwatch sw = Stopwatch.StartNew();
            var assemblyTypes = (from a in assemblies.AsParallel()
                                 from t in a.GetTypes()
                                 select t).ToArray().AsParallel();
            var typesQuery = (from t in assemblyTypes
                              where !typeof(Delegate).IsAssignableFrom(t)
                              from m in t.GetMembers(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly)
                              select m).Concat(from a in assemblies.AsParallel()
                                               from t in a.GetTypes()
                                               select (MemberInfo)t).Distinct();
            var types = typesQuery.ToArray().AsParallel();
            sw.Stop();
            var mainTime = sw.Elapsed;
            sw.Restart();
            var excluded = new[] { typeof(StructLayoutAttribute), typeof(MarshalAsAttribute) };
            var attributes = (from t in typesQuery
                              let tca = t.GetCustomAttributesData().ToArray()
                              from c in tca
                              where !excluded.Contains(c.Constructor.DeclaringType)
                              select new { c, P = c.ConstructorArguments, NP = c.NamedArguments, Type = c.Constructor.DeclaringType }).ToArray();
            sw.Stop();
            //Type current = null;
            //foreach (var member in types)
            //{
            //    if (member.ReflectedType != current &&
            //        current != null)
            //    {
            //        Console.WriteLine();
            //        current = member.ReflectedType;
            //        Console.WriteLine(current);
            //    }
            //    else if (current == null)
            //    {
            //        current = member.ReflectedType;
            //        Console.WriteLine(current);
            //    }
            //    Console.WriteLine("\t{0}", member);
            //}
            Console.WriteLine("{0} members at {1}ms", types.Count(), mainTime.TotalMilliseconds);
            Console.WriteLine("{0} custom attributes at {1}ms", attributes.Length, sw.Elapsed.TotalMilliseconds);
            Console.WriteLine("Reflection took {0}ms to process query a second time.", MiscHelperMethods.CreateFunctionOfTime(new Func<int>(typesQuery.Count))().Item1.TotalMilliseconds);
        }

        internal static void CliTypeSystemTest()
        {
            Stopwatch sw = Stopwatch.StartNew();
            var identityManager = Program.identityManager;
            var assemblies = (from t in new[] { typeof(IControlledCollection), typeof(IType), typeof(ICliType), typeof(IIntermediateAssembly), typeof(ImageComboBox), typeof(Program), typeof(Form) }
                              select identityManager.ObtainAssemblyReference(t.Assembly)).ToArray();

            //foreach (var table in from ICliAssembly a in assemblies
            //                      from t in a.MetadataRoot.TableStream.Values
            //                      select t)
            //    table.Read();


            var assemblyTypes = (from a in assemblies.AsParallel()
                                 from t in a.GetTypes()
                                 select t).ToArray().AsParallel();
            var typesQuery = (from t in assemblyTypes
                              from m in t.Members.Values
                              select (IDeclaration)m.Entry);
            var memberQuery = typesQuery.ToArray().AsParallel();
            typesQuery = typesQuery.Concat(from m in memberQuery
                                           where m is IPropertySignatureMember
                                           let p = (IPropertySignatureMember)m
                                           from pm in (
                                            p.CanRead ?
                                                p.CanWrite ? new[] { p.GetMethod, p.SetMethod } :
                                                             new[] { p.GetMethod } :
                                            p.CanWrite ? new[] { p.SetMethod } : new IPropertySignatureMethodMember[0])
                                           select (IDeclaration)pm)
                                   .Concat(from m in memberQuery
                                           where m is IEventSignatureMember
                                           let e = (IEventSignatureMember)m
                                           let ee = e as IEventMember
                                           from em in ee == null ? new[] { e.OnAddMethod, e.OnRemoveMethod } : ee.CanRaise ? new[] { e.OnAddMethod, e.OnRemoveMethod, ee.OnRaiseMethod } : new[] { e.OnAddMethod, e.OnRemoveMethod }
                                           select (IDeclaration)em)
                                   .Concat(from t in assemblyTypes
                                           select (IDeclaration)t);
            var types = typesQuery.ToArray();
            var nonMetadataEntities = (from k in types
                                       where !(k is IMetadataEntity)
                                       select k.GetType()).Distinct().ToArray();
            sw.Stop();
            var mainTime = sw.Elapsed;
            sw.Restart();

            var metadataQuery = (from IMetadataEntity d in
                                     from q in types.AsParallel()
                                     where q is IMetadataEntity
                                     select q
                                 let md = d.Metadata
                                 from m in md
                                 select new { m, P = m.Parameters.ToArray(), NP = m.NamedParameters.ToArray(), Type = m.Type }).ToArray();
            sw.Stop();
            Console.WriteLine("{0} members at {1}ms", types.Length, mainTime.TotalMilliseconds);
            Console.WriteLine("{0} custom attributes at {1}ms", metadataQuery.Length, sw.Elapsed.TotalMilliseconds);
            Console.WriteLine("Abstraction took {0}ms to process query a second time.", MiscHelperMethods.CreateFunctionOfTime(new Func<int>(typesQuery.Count))().Item1.TotalMilliseconds);
        }
    }
}
