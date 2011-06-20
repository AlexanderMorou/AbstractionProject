using System;
using AllenCopeland.Abstraction.Slf.CSharp;
using AllenCopeland.Abstraction.Slf.Cst;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Languages;
using AllenCopeland.Abstraction.Slf.Oil;
using AllenCopeland.Abstraction.Slf.Oil.Members;
using AllenCopeland.Abstraction.Slf.Oil.VisualBasic;
using System.Windows.Forms;
using System.Linq;
using AllenCopeland.Abstraction.Slf.Compilers;
using AllenCopeland.Abstraction.Slf.Linkers;

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
            public static Tuple<IVisualBasicAssembly, IIntermediateTopLevelMethodMember, IIntermediateClassType, IIntermediateClassMethodMember, IIntermediateClassMethodMember, IIntermediateClassCtorMember> CreateStructureVB(IVisualBasicAssembly assembly)
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
            public static Tuple<IVisualBasicAssembly, IIntermediateTopLevelMethodMember> CreateStructureVB(IVisualBasicAssembly assembly)
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
