using AllenCopeland.Abstraction.Numerics;
using AllenCopeland.Abstraction.Slf._Internal.Ast;
using AllenCopeland.Abstraction.Slf._Internal.Cli;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using AllenCopeland.Abstraction.Slf.Ast.Statements;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Slf.Compilers;
using AllenCopeland.Abstraction.Slf.Languages;
using AllenCopeland.Abstraction.Slf.Languages.Cil;
using AllenCopeland.Abstraction.Slf.Languages.CSharp;
using AllenCopeland.Abstraction.Slf.Languages.VisualBasic;
using AllenCopeland.Abstraction.Utilities;
using AllenCopeland.Abstraction.Utilities.Collections;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AllenCopeland.Abstraction.Slf.SupplementaryProjects.TestCli
{
    static class Program
    {
        static void Main() {
            VBTest();
        }

        private static void VBTest()
        {
            var vbProvider = LanguageVendors.Microsoft.GetVisualBasicLanguage().GetProvider();
            var vbAssembly = vbProvider.CreateAssembly("TestVBAssembly");
            vbAssembly.DefaultNamespace = vbAssembly.Namespaces.Add("AllenCopeland.Abstraction.Slf.SupplementaryProjects.VBTest");
            var vbProgram = vbAssembly.DefaultNamespace.Parts.Add().Classes.Add("Program");
            vbAssembly.ScopeCoercions.Add("System.Console".GetSymbolType(), false);
            var vbMain = vbProgram.Methods.Add("Main");
            vbMain.IsStatic = true;
            vbMain.Call("Console".Fuse("WriteLine").Fuse("Hello World!".ToPrimitive()));

        }
    }
}
