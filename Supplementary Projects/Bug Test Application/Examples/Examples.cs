using System;
using AllenCopeland.Abstraction.Slf.Cst;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Languages;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using System.Windows.Forms;
using System.Linq;
using AllenCopeland.Abstraction.Slf.Compilers;
using AllenCopeland.Abstraction.Slf.Linkers;
using AllenCopeland.Abstraction.Slf.Languages.CSharp;
using AllenCopeland.Abstraction.Slf.Languages.VisualBasic;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.SupplementaryProjects.BugTestApplication.Examples
{
    internal static partial class ExampleHandler
    {
        private static TAssembly Create<TLanguage, TProvider, TRootNode, TAssembly, TVersion>(TLanguage language, string name)
            where TLanguage :
                IVersionedHighLevelLanguage<TVersion, TRootNode>
            where TProvider :
                IVersionedHighLevelLanguageProvider<TVersion, TRootNode>
            where TRootNode :
                IConcreteNode
            where TAssembly :
                IVersionedHighLevelIntermediateAssembly<TLanguage, TRootNode, TProvider, TVersion>
        {
            var result = (TAssembly)language.CreateAssembly(name);
            //Create the assembly and define its output type.
            result.References.Add(typeof(int).Assembly.GetAssemblyReference());
            result.References.Add(typeof(Form).Assembly.GetAssemblyReference(), "forms", AssemblyReferenceCollection.DefaultAlias);
            result.References.Add(typeof(Queryable).Assembly.GetAssemblyReference());
            result.CompilationContext.OutputType = AssemblyOutputType.WinFormsApplication;
            return result;
        }

        internal partial class WindowsFormsApplication
        {
            public static Tuple<IMyVisualBasicAssembly, IIntermediateTopLevelMethodMember, IIntermediateClassType, IIntermediateClassMethodMember, IIntermediateClassMethodMember, IIntermediateClassCtorMember> CreateStructureVB(IMyVisualBasicAssembly assembly)
            {
                return CreateStructure(assembly);
            }
            public static Tuple<ICSharpAssembly, IIntermediateTopLevelMethodMember, IIntermediateClassType, IIntermediateClassMethodMember, IIntermediateClassMethodMember, IIntermediateClassCtorMember> CreateStructureCSharp(ICSharpAssembly assembly)
            {
                return CreateStructure(assembly);
            }
        }

        internal partial class LanguageIntegratedQuery
        {
            public static Tuple<IMyVisualBasicAssembly, IIntermediateTopLevelMethodMember> CreateStructureVB(IMyVisualBasicAssembly assembly)
            {
                return CreateStructure(assembly);
            }
            public static Tuple<ICSharpAssembly, IIntermediateTopLevelMethodMember> CreateStructureCSharp(ICSharpAssembly assembly)
            {
                return CreateStructure(assembly);
            }
        }

        internal class ConsoleApplication
        {
            public static void CreateProject()
            {

            }
        }
    }
}
