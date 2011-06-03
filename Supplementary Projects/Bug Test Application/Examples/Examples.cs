using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Compilers;
using AllenCopeland.Abstraction.Slf.Cst;
using AllenCopeland.Abstraction.Slf.Languages;
using AllenCopeland.Abstraction.Slf.Linkers;
using AllenCopeland.Abstraction.Slf.Oil;
using AllenCopeland.Abstraction.Slf.Oil.Expressions;
using AllenCopeland.Abstraction.Slf.Oil.Expressions.CSharp;
using AllenCopeland.Abstraction.Slf.Oil.Members;
using AllenCopeland.Abstraction.Slf.Oil.VisualBasic;
using AllenCopeland.Abstraction.Slf.CSharp;

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
                IIntermediateAssembly<TLanguage, TRootNode, TProvider, TVersion>
        {
            return (TAssembly)language.CreateAssembly(name);
        }

        internal partial class WindowsFormsApplication
        {
            public static Tuple<IVisualBasicAssembly, IIntermediateTopLevelMethodMember, IIntermediateClassType, IIntermediateClassMethodMember, IIntermediateClassMethodMember, IIntermediateClassCtorMember> CreateProjectVB()
            {
                return CreateProject(name => ExampleHandler.Create<IVisualBasicLanguage, IVisualBasicProvider, IVisualBasicStart, IVisualBasicAssembly, VisualBasicVersion>(LanguageVendors.Microsoft.GetVisualBasicLanguage(), name));
            }
            public static Tuple<ICSharpAssembly, IIntermediateTopLevelMethodMember, IIntermediateClassType, IIntermediateClassMethodMember, IIntermediateClassMethodMember, IIntermediateClassCtorMember> CreateProjectCSharp()
            {
                return CreateProject(name => ExampleHandler.Create<ICSharpLanguage, ICSharpProvider, ICSharpCompilationUnit, ICSharpAssembly, CSharpLanguageVersion>(LanguageVendors.Microsoft.GetCSharpLanguage(), name));
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
