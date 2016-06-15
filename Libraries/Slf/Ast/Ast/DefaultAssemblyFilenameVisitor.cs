using AllenCopeland.Abstraction.Slf.Ast.Members;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Ast
{
    internal class DefaultAssemblyFilenameVisitorContext
    {
        /// <summary>
        /// Returns the <see cref="string"/> resulted
        /// from a visit operation.
        /// </summary>
        public string CurrentResult { get; internal set; }
        public List<string> OtherFiles { get; internal set; }

        public IIntermediateAssembly RootAssembly { get; internal set; }
    }
    internal class DefaultAssemblyFilenameVisitor :
        IIntermediateDeclarationVisitor<string, DefaultAssemblyFilenameVisitorContext>,
        IIntermediateTypeVisitor<string, DefaultAssemblyFilenameVisitorContext>
    {

        public static string GetFileInfo(IIntermediateAssembly target)
        {
            List<string> otherFiles = new List<string>();
            return GetFileInfo(target, otherFiles, target.Parts.Count > 0);
        }
        private static string GetFileInfo(IIntermediateAssembly target, List<string> otherFiles, bool allowPartials)
        {
            var context = new DefaultAssemblyFilenameVisitorContext() { RootAssembly = target.GetRoot(), OtherFiles = otherFiles };
            if (!allowPartials)
            {
                if (target.IsRoot)
                    context.CurrentResult = target.Name;
                else
                    context.CurrentResult = string.Empty;
                return context.CurrentResult;
            }
            var visitor = new DefaultAssemblyFilenameVisitor();

            var result = target.Accept(visitor, context);
            if (context.CurrentResult != null)
            {
                if (context.OtherFiles.Contains(context.CurrentResult))
                {
                    string fnameCurrent = context.CurrentResult;
                    var path = Path.GetDirectoryName(fnameCurrent);
                    var fileName = Path.GetFileNameWithoutExtension(fnameCurrent);
                    int offset = 0;
                    while (context.OtherFiles.Contains(fnameCurrent))
                        fnameCurrent = string.Format("{0}{1}{2}{3}{4}", path, Path.DirectorySeparatorChar, fileName, '-', ++offset);
                    context.CurrentResult = fnameCurrent;
                    context.OtherFiles.Add(fnameCurrent);
                }
                else
                    context.OtherFiles.Add(context.CurrentResult);
            }
            return context.CurrentResult;
        }

        public string Visit(IIntermediateAssembly assembly, DefaultAssemblyFilenameVisitorContext context)
        {
            if (!assembly.UsesDefaultFileName)
                context.CurrentResult = assembly.FileName;
            else
                VisitNamespaceParent(assembly, context);

            return context.CurrentResult;
        }

        private void VisitNamespaceParent(IIntermediateNamespaceParent nsParent, DefaultAssemblyFilenameVisitorContext context)
        {
            VisitTypeParent(nsParent, context);
            foreach (var @namespace in nsParent.Namespaces.ExclusivelyOnParent())
            {
                if (context.CurrentResult != null)
                    return;
                @namespace.Value.Accept(this, context);
            }
            if (context.CurrentResult == null)
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
                        context.CurrentResult = @".\AssemblyInfo";
                    else
                        context.CurrentResult = string.Format(@".\{0}", context.RootAssembly.Name);
                }
            }
        }

        private void VisitTypeParent(IIntermediateTypeParent typeParent, DefaultAssemblyFilenameVisitorContext context)
        {
            if (context.CurrentResult != null)
                return;
            foreach (var @class in typeParent.Classes.ExclusivelyOnParent())
            {
                if (context.CurrentResult != null)
                    return;
                @class.Value.Accept(this, context);
            }
            foreach (var @delegate in typeParent.Delegates.ExclusivelyOnParent())
            {
                if (context.CurrentResult != null)
                    return;
                @delegate.Value.Accept(this, context);
            }
            foreach (var @enum in typeParent.Enums.ExclusivelyOnParent())
            {
                if (context.CurrentResult != null)
                    return;
                @enum.Value.Accept(this, context);
            }
            foreach (var @interface in typeParent.Interfaces.ExclusivelyOnParent())
            {
                if (context.CurrentResult != null)
                    return;
                @interface.Value.Accept(this, context);
            }
            foreach (var @struct in typeParent.Structs.ExclusivelyOnParent())
            {
                if (context.CurrentResult != null)
                    return;
                @struct.Value.Accept(this, context);
            }
        }

        public string Visit(IIntermediateNamespaceDeclaration @namespace, DefaultAssemblyFilenameVisitorContext context)
        {
            VisitNamespaceParent(@namespace, context);
            return context.CurrentResult;
        }

        public string Visit(IIntermediateClassType @class, DefaultAssemblyFilenameVisitorContext context)
        {
            VisitTypeParent(@class, context);
            if (context.CurrentResult == null)
            {
                if (AnyMembers(@class) || @class.IsRoot)
                    context.CurrentResult = string.Format(@"{0}\{1}", ToBaselineNamespaceName(@class.NamespaceName, context), @class.FullName.Replace('+', '.').Substring(@class.NamespaceName.Length == 0 ? 0 : @class.NamespaceName.Length + 1));
            }
            else if (AnyMembers(@class))
                context.CurrentResult = context.CurrentResult.Replace(@class.Name, string.Format("[{0}]", @class.Name));
            return context.CurrentResult;
        }

        private string ToBaselineNamespaceName(string namespaceName, DefaultAssemblyFilenameVisitorContext context)
        {
            if (context.RootAssembly.DefaultNamespace == null)
                return ".";
            string relativeRoot = string.Empty;
            var names = new string[] { namespaceName, context.RootAssembly.DefaultNamespace.FullName };
            var parts = (from string f in names
                         orderby f.Length descending
                         select f.ToLower()).First().Split(new[] { "." }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < parts.Length; i++)
            {
                string currentRoot = string.Join(@".", parts, 0, parts.Length - i);
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
                                      @struct.IsRoot && (@struct.Metadata.Count > 0 ||
                                                         @struct.SummaryText != null ||
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
        public string Visit(IIntermediateDelegateType @delegate, DefaultAssemblyFilenameVisitorContext context)
        {
            context.CurrentResult = string.Format(@"{0}\{1}", ToBaselineNamespaceName(@delegate.NamespaceName, context), @delegate.FullName.Substring(@delegate.NamespaceName.Length == 0 ? 0 : @delegate.NamespaceName.Length + 1));
            return context.CurrentResult;
        }

        public string Visit(IIntermediateEnumType @enum, DefaultAssemblyFilenameVisitorContext context)
        {
            context.CurrentResult = string.Format(@"{0}\{1}", ToBaselineNamespaceName(@enum.NamespaceName, context), @enum.FullName.Substring(@enum.NamespaceName.Length == 0 ? 0 : @enum.NamespaceName.Length + 1));
            return context.CurrentResult;
        }

        public string Visit(IIntermediateInterfaceType @interface, DefaultAssemblyFilenameVisitorContext context)
        {
            VisitTypeParent(@interface, context);
            if (context.CurrentResult == null)
            {
                if (AnyMembers(@interface) || @interface.IsRoot)
                    context.CurrentResult = string.Format(@"{0}\{1}", ToBaselineNamespaceName(@interface.NamespaceName, context), @interface.FullName.Substring(@interface.NamespaceName.Length == 0 ? 0 : @interface.NamespaceName.Length + 1));
            }
            else if (AnyMembers(@interface))
                context.CurrentResult = context.CurrentResult.Replace(@interface.Name, string.Format("[{0}]", @interface.Name));
            return context.CurrentResult;
        }

        public string Visit(IIntermediateStructType @struct, DefaultAssemblyFilenameVisitorContext context)
        {
            VisitTypeParent(@struct, context);
            if (context.CurrentResult == null)
            {
                if (AnyMembers(@struct) || @struct.IsRoot)
                    context.CurrentResult = string.Format(@"{0}\{1}", ToBaselineNamespaceName(@struct.NamespaceName, context), @struct.FullName.Substring(@struct.NamespaceName.Length == 0 ? 0 : @struct.NamespaceName.Length + 1));
            }
            else if (AnyMembers(@struct))
                context.CurrentResult = context.CurrentResult.Replace(@struct.Name, string.Format("[{0}]", @struct.Name));
            return context.CurrentResult;
        }

        public string Visit<TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent>(IIntermediateGenericParameter<TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent> parameter, DefaultAssemblyFilenameVisitorContext context)
            where TGenericParameter : Abstract.IGenericParameter<TGenericParameter, TParent>
            where TIntermediateGenericParameter : TGenericParameter, IIntermediateGenericParameter<TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent>
            where TParent : Abstract.IGenericParamParent<TGenericParameter, TParent>
            where TIntermediateParent : TParent, IIntermediateGenericParameterParent<TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent>
        {
            return context.CurrentResult;
        }
    }
}
