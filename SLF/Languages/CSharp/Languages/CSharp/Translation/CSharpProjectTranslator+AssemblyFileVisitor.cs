using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Languages.CSharp.Translation
{
    partial class CSharpProjectTranslator
    {
        internal class AssemblyFileVisitor :
            IIntermediateDeclarationVisitor<CSharpAssemblyFileInfo, CSharpAssemblyFileContext>,
            IIntermediateTypeVisitor<CSharpAssemblyFileInfo, CSharpAssemblyFileContext>
        {

            public static CSharpAssemblyFileInfo GetFileInfo(IIntermediateAssembly target, CSharpProjectTranslator translator, List<string> otherFiles)
            {
                var context = new CSharpAssemblyFileContext() { RootAssembly = target.GetRoot(), OtherFiles = otherFiles, Translator = translator };
                if (!translator.options.AllowPartials)
                {
                    if (target.IsRoot)
                        context.CurrentResult = new CSharpAssemblyFileInfo() { FileName = target.Name };
                    else
                        context.CurrentResult = new CSharpAssemblyFileInfo();
                    return context.CurrentResult;
                }
                var visitor = new AssemblyFileVisitor();

                target.Visit(visitor, context);
                if (context.CurrentResult.YieldsFile)
                {
                    if (context.CurrentResult.FileName.Contains('+'))
                    {
                    }
                    if (context.OtherFiles.Contains(context.CurrentResult.FileName))
                    {
                        string fnameCurrent = context.CurrentResult.FileName;
                        var path = Path.GetDirectoryName(fnameCurrent);
                        var fileName = Path.GetFileNameWithoutExtension(fnameCurrent);
                        int offset = 0;
                        while (context.OtherFiles.Contains(fnameCurrent))
                            fnameCurrent = string.Format("{0}{1}{2}{3}{4}", path, Path.DirectorySeparatorChar, fileName, '-', ++offset);
                        context.CurrentResult.FileName = fnameCurrent;
                        context.OtherFiles.Add(fnameCurrent);
                    }
                    else
                        context.OtherFiles.Add(context.CurrentResult.FileName);
                }
                return context.CurrentResult;
            }

            public CSharpAssemblyFileInfo Visit(IIntermediateAssembly assembly, CSharpAssemblyFileContext context)
            {
                CSharpAssemblyFileInfo result = new CSharpAssemblyFileInfo();
                context.CurrentResult = result;
                VisitNamespaceParent(assembly, context);
                return result;
            }

            private void VisitNamespaceParent(IIntermediateNamespaceParent nsParent, CSharpAssemblyFileContext context)
            {
                VisitTypeParent(nsParent, context);
                foreach (var @namespace in nsParent.Namespaces.ExclusivelyOnParent())
                {
                    if (context.CurrentResult.YieldsFile)
                        return;
                    @namespace.Value.Visit(this, context);
                }
                if (!context.CurrentResult.YieldsFile)
                {
                    var fields = nsParent.Fields.ExclusivelyOnParent().Count() > 0;
                    var methods = nsParent.Methods.ExclusivelyOnParent().Count() > 0;
                    bool metadata = false;
                    var assemTarget = nsParent as IIntermediateAssembly;
                    if (assemTarget != null && assemTarget.IsRoot)
                        metadata = assemTarget.Metadata.Count > 0;
                    if (fields || methods || metadata)
                    {
                        int offset = 0;
                        if (assemTarget != null &&
                            assemTarget.IsRoot)
                            context.CurrentResult.FileName = @".\AssemblyInfo";
                        else
                            context.CurrentResult.FileName = string.Format(@".\{0}", context.RootAssembly.Name);
                    }
                }
            }

            private void VisitTypeParent(IIntermediateTypeParent typeParent, CSharpAssemblyFileContext context)
            {
                if (context.CurrentResult.YieldsFile)
                    return;
                foreach (var @class in typeParent.Classes.ExclusivelyOnParent())
                {
                    if (context.CurrentResult.YieldsFile)
                        return;
                    @class.Value.Visit(this, context);
                }
                foreach (var @delegate in typeParent.Delegates.ExclusivelyOnParent())
                {
                    if (context.CurrentResult.YieldsFile)
                        return;
                    @delegate.Value.Visit(this, context);
                }
                foreach (var @enum in typeParent.Enums.ExclusivelyOnParent())
                {
                    if (context.CurrentResult.YieldsFile)
                        return;
                    @enum.Value.Visit(this, context);
                }
                foreach (var @interface in typeParent.Interfaces.ExclusivelyOnParent())
                {
                    if (context.CurrentResult.YieldsFile)
                        return;
                    @interface.Value.Visit(this, context);
                }
                foreach (var @struct in typeParent.Structs.ExclusivelyOnParent())
                {
                    if (context.CurrentResult.YieldsFile)
                        return;
                    @struct.Value.Visit(this, context);
                }
            }

            public CSharpAssemblyFileInfo Visit(IIntermediateNamespaceDeclaration @namespace, CSharpAssemblyFileContext context)
            {
                VisitNamespaceParent(@namespace, context);
                return context.CurrentResult;
            }

            public CSharpAssemblyFileInfo Visit(IIntermediateClassType @class, CSharpAssemblyFileContext context)
            {
                VisitTypeParent(@class, context);
                if (!context.CurrentResult.YieldsFile)
                {
                    if (AnyMembers(@class) || @class.IsRoot)
                        context.CurrentResult.FileName = string.Format(@"{0}\{1}", ToBaselineNamespaceName(@class.NamespaceName, context), @class.FullName.Replace('+', '.').Substring(@class.NamespaceName.Length == 0 ? 0 : @class.NamespaceName.Length + 1));
                }
                else if (AnyMembers(@class))
                    context.CurrentResult.FileName = context.CurrentResult.FileName.Replace(@class.Name, string.Format("[{0}]", @class.Name));
                return context.CurrentResult;
            }

            private string ToBaselineNamespaceName(string namespaceName, CSharpAssemblyFileContext context)
            {

                string relativeRoot = null;
                var names = new string[] { namespaceName, context.RootAssembly.DefaultNamespace.FullName };
                var parts = (from string f in names
                             orderby f.Length descending
                             select f.ToLower()).First().Split(new[] { "." }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 1; i < parts.Length; i++)
                {
                    string currentRoot = string.Join(@".", parts, 0, parts.Length - (i-1));
                    if (names.All(p => p.ToLower().StartsWith(currentRoot)))
                    {
                        relativeRoot = currentRoot;
                        break;
                    }
                }
                if (relativeRoot == string.Empty)
                    return namespaceName.Replace('.', Path.DirectorySeparatorChar);
                if (namespaceName.Length > relativeRoot.Length)
                    return namespaceName.Substring(relativeRoot.Length + 1).Replace('.', Path.DirectorySeparatorChar);
                else
                    return ".";

            }

            private static bool AnyMembers(IIntermediateStructType @struct)
            {
                return @struct.BinaryOperatorCoercions.ExclusivelyOnParent().Count() > 0 ||
                                  @struct.Constructors.ExclusivelyOnParent().Count() > 0 ||
                                        @struct.Events.ExclusivelyOnParent().Count() > 0 ||
                                        @struct.Fields.ExclusivelyOnParent().Count() > 0 ||
                                      @struct.Indexers.ExclusivelyOnParent().Count() > 0 ||
                                       @struct.Methods.ExclusivelyOnParent().Count() > 0 ||
                                    @struct.Properties.ExclusivelyOnParent().Count() > 0 ||
                                 @struct.TypeCoercions.ExclusivelyOnParent().Count() > 0 ||
                        @struct.UnaryOperatorCoercions.ExclusivelyOnParent().Count() > 0 ||
                                                              @struct.Metadata.Count > 0 ||
                                          @struct.IsRoot && (@struct.SummaryText != null ||
                                                             @struct.RemarksText != null);
            }

            private static bool AnyMembers(IIntermediateClassType @class)
            {
                return @class.BinaryOperatorCoercions.ExclusivelyOnParent().Count() > 0 ||
                                  @class.Constructors.ExclusivelyOnParent().Count() > 0 ||
                                        @class.Events.ExclusivelyOnParent().Count() > 0 ||
                                        @class.Fields.ExclusivelyOnParent().Count() > 0 ||
                                      @class.Indexers.ExclusivelyOnParent().Count() > 0 ||
                                       @class.Methods.ExclusivelyOnParent().Count() > 0 ||
                                    @class.Properties.ExclusivelyOnParent().Count() > 0 ||
                                 @class.TypeCoercions.ExclusivelyOnParent().Count() > 0 ||
                        @class.UnaryOperatorCoercions.ExclusivelyOnParent().Count() > 0 ||
                                                              @class.Metadata.Count > 0 ||
                                           @class.IsRoot && (@class.SummaryText != null ||
                                                             @class.RemarksText != null);
            }

            private static bool AnyMembers(IIntermediateInterfaceType @interface)
            {
                return @interface.Events.ExclusivelyOnParent().Count() > 0 ||
                                  @interface.Indexers.ExclusivelyOnParent().Count() > 0 ||
                                   @interface.Methods.ExclusivelyOnParent().Count() > 0 ||
                                @interface.Properties.ExclusivelyOnParent().Count() > 0 ||
                                                          @interface.Metadata.Count > 0 ||
                                   @interface.IsRoot && (@interface.SummaryText != null ||
                                                         @interface.RemarksText != null);
            }
            public CSharpAssemblyFileInfo Visit(IIntermediateDelegateType @delegate, CSharpAssemblyFileContext context)
            {
                context.CurrentResult.FileName = string.Format(@"{0}\{1}", ToBaselineNamespaceName(@delegate.NamespaceName, context), @delegate.FullName.Substring(@delegate.NamespaceName.Length == 0 ? 0 : @delegate.NamespaceName.Length + 1));
                return context.CurrentResult;
            }

            public CSharpAssemblyFileInfo Visit(IIntermediateEnumType @enum, CSharpAssemblyFileContext context)
            {
                context.CurrentResult.FileName = string.Format(@"{0}\{1}", ToBaselineNamespaceName(@enum.NamespaceName, context), @enum.FullName.Substring(@enum.NamespaceName.Length == 0 ? 0 : @enum.NamespaceName.Length + 1));
                return context.CurrentResult;
            }

            public CSharpAssemblyFileInfo Visit(IIntermediateInterfaceType @interface, CSharpAssemblyFileContext context)
            {
                VisitTypeParent(@interface, context);
                if (!context.CurrentResult.YieldsFile)
                {
                    if (AnyMembers(@interface) || @interface.IsRoot)
                        context.CurrentResult.FileName = string.Format(@"{0}\{1}", ToBaselineNamespaceName(@interface.NamespaceName, context), @interface.FullName.Substring(@interface.NamespaceName.Length == 0 ? 0 : @interface.NamespaceName.Length + 1));
                }
                else if (AnyMembers(@interface))
                    context.CurrentResult.FileName = context.CurrentResult.FileName.Replace(@interface.Name, string.Format("[{0}]", @interface.Name));
                return context.CurrentResult;
            }

            public CSharpAssemblyFileInfo Visit(IIntermediateStructType @struct, CSharpAssemblyFileContext context)
            {
                VisitTypeParent(@struct, context);
                if (!context.CurrentResult.YieldsFile)
                {
                    if (AnyMembers(@struct) || @struct.IsRoot)
                        context.CurrentResult.FileName = string.Format(@"{0}\{1}", ToBaselineNamespaceName(@struct.NamespaceName, context), @struct.FullName.Substring(@struct.NamespaceName.Length == 0 ? 0 : @struct.NamespaceName.Length + 1));
                }
                else if (AnyMembers(@struct))
                    context.CurrentResult.FileName = context.CurrentResult.FileName.Replace(@struct.Name, string.Format("[{0}]", @struct.Name));
                return context.CurrentResult;
            }

            public CSharpAssemblyFileInfo Visit<TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent>(IIntermediateGenericParameter<TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent> parameter, CSharpAssemblyFileContext context)
                where TGenericParameter : Abstract.IGenericParameter<TGenericParameter, TParent>
                where TIntermediateGenericParameter : TGenericParameter, IIntermediateGenericParameter<TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent>
                where TParent : Abstract.IGenericParamParent<TGenericParameter, TParent>
                where TIntermediateParent : TParent, IIntermediateGenericParameterParent<TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent>
            {
                return context.CurrentResult;
            }
        }
    }
}
