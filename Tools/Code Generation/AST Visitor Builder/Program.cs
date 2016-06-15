using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Translation;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract.Modules;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Ast.Cli;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using AllenCopeland.Abstraction.Slf.Ast.Modules;
using AllenCopeland.Abstraction.Slf.Ast.Statements;
using AllenCopeland.Abstraction.Slf.Languages.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Languages.CSharp;
using AllenCopeland.Abstraction.Slf.Languages;
using System.IO;
using AllenCopeland.Abstraction.Slf.Languages.CSharp.Translation;
using AllenCopeland.Abstraction.Slf.Transformation;

namespace AllenCopeland.Abstraction.Tools.VisitorBuilder
{
    using VisitorBuilder = AllenCopeland.Abstraction.Slf.Transformation.VisitorBuilder;
    internal static class Program
    {
        static void Main(string[] args)
        {
            CustomAttributeData cad;
#if !DEBUG
            Console.WriteLine("Not in Debug Mode");
            return;
#else
            Console.WriteLine("Allen Copeland's Abstraction - Visitor Model Builder");
            BuildVisitorModel2();
#endif
        }

        private static void BuildVisitorModel2()
        {
            var identityManager = IntermediateCliGateway.CreateIdentityManager(IntermediateCliGateway.GetRuntimeEnvironmentInfo(CliFrameworkPlatform.AnyPlatform));
            var assemblyToReflect = identityManager.ObtainAssemblyReference(typeof(IExpression).Assembly);
            var assembly = CreateAssembly("_abs.slf.ast.visitors", identityManager);
            var q = (IIntermediateAssembly)assembly;
            var results = VisitorModelHelper.GetImplementationContext(assemblyToReflect, assembly, identityManager).ToArray();

            var namespaces = (from b in results
                              from variation in b.Context.Variations
                              from visitor in variation.RelevantBuilders
                              select visitor.VisitorInterface).Select(k => k.NamespaceName).Distinct().ToArray();
                             
            var relativeRoot = namespaces.GetRelativeRoot(".");
            if (!assembly.Namespaces.PathExists(relativeRoot))
                assembly.DefaultNamespace = assembly.Namespaces.Add(relativeRoot);
            else
                assembly.DefaultNamespace = assembly.Namespaces[relativeRoot];

            foreach (var builder in results)
                builder.BuildScaffolding();
            foreach (var builder in results)
                builder.Build();
            Console.WriteLine("Press any key to write the output...");
            Console.ReadKey(true);
            Console.Write("Writing Project...");

            WriteProject(Path.Combine(Path.GetDirectoryName(typeof(Program).Assembly.Location), "VisitorModel"), assembly);
            Console.WriteLine("Done.");
        }

        private static void BuildVisitorModel()
        {
            Console.Write("Gathering Type and Visitor Metadata Details...");
            var identityManager = IntermediateCliGateway.CreateIdentityManager(IntermediateCliGateway.GetRuntimeEnvironmentInfo(CliFrameworkPlatform.AnyPlatform));
            List<IType> ElementsToExcludeFromTranslationMethods =
                new[] { typeof(IBinaryOperationExpression<,>).GetTypeReference(identityManager) }.ToList();
            var assemblyToReflect = identityManager.ObtainAssemblyReference(typeof(IExpression).Assembly);
            var visitorTarget = typeof(VisitorTargetAttribute).GetTypeReference<IType>(identityManager);
            var visitorImplementationTarget = typeof(VisitorImplementationTargetAttribute).GetTypeReference<IType>(identityManager);
            var visitorContextualDetail = new Dictionary<VisitorTargetDetail, List<IType>>();
            var distinctImplementations = new HashSet<VisitorImplementationTargetDetail>();
            var assemblyMetadata = assemblyToReflect.Metadata.Where(k => k.Type == visitorImplementationTarget);
            foreach (var metadatum in assemblyMetadata.Where(k => k.Type == visitorImplementationTarget))
            {
                var currentDetail = VisitorImplementationTargetDetail.DeriveFromMetadata(metadatum);
                distinctImplementations.Add(currentDetail);
            }
            foreach (var type in assemblyToReflect.GetTypes())
            {
                foreach (var metadatum in type.Metadata.Where(k => k.Type == visitorTarget))
                {
                    var currentDetail = VisitorTargetDetail.DeriveFromMetadata(metadatum);
                    List<IType> seriesRelevant;
                    if (!visitorContextualDetail.TryGetValue(currentDetail, out seriesRelevant))
                        visitorContextualDetail.Add(currentDetail, seriesRelevant = new List<IType>());
                    seriesRelevant.Add(type);
                }
            }
            visitorContextualDetail = (from kvp in visitorContextualDetail
                                       orderby kvp.Key.TargetContext,
                                               kvp.Key.DerivedThroughInheriting == null
                                               ? string.Empty
                                               : kvp.Key.DerivedThroughInheriting,
                                               kvp.Key.ContextualVisitor,
                                               kvp.Key.YieldingVisitor
                                       select kvp).ToDictionary(k => k.Key, v => v.Value);
            Console.WriteLine("Done.");
            Console.Write("Building Scaffolds...");
            Dictionary<VisitorTargetDetail, VisitorBuilder> builders = new Dictionary<VisitorTargetDetail, VisitorBuilder>();
            /* Create the builders. */
            var derivedDetail = visitorContextualDetail.Keys.Where(k => !string.IsNullOrEmpty(k.DerivedThroughInheriting)).ToArray();
            var visitorDetailMinusHierarchy = visitorContextualDetail.Keys.Except(derivedDetail).ToArray();
            foreach (var contextKey in visitorDetailMinusHierarchy)
                builders.Add(contextKey, new VisitorBuilder(contextKey, visitorContextualDetail[contextKey]));

            var assembly = CreateAssembly("_abs.slf.ast.visitors", identityManager);
            var namespaces = visitorContextualDetail.Values.SelectMany(k => k).Select(k => k.NamespaceName).Distinct().ToArray();
            var relativeRoot = namespaces.GetRelativeRoot(".");
            assembly.DefaultNamespace = assembly.Namespaces.Add(relativeRoot);
            foreach (var contextKey in visitorDetailMinusHierarchy)
            {
                var builder = builders[contextKey];
                builder.ScaffoldInterface(assembly);
            }
            Console.WriteLine("Done.");
            Console.Write("Building Typeless Visitor Scaffolds...");
            var typelessBuilders = BuildInheritedVisitors(visitorContextualDetail, builders, assembly);
            Console.WriteLine("Done.");
            Console.Write("Building Visitor Interfaces...");
            foreach (var contextKey in visitorDetailMinusHierarchy)
            {
                var builder = builders[contextKey];
                builder.BuildVisitor();
            }
            Console.WriteLine("Done.");
            Console.Write("Building Typeless Visitor Interfaces...");
            foreach (var contextKey in typelessBuilders.Keys)
            {
                var builder = typelessBuilders[contextKey];
                builder.BuildVisitor();
            }
            Console.WriteLine("Done.");

            /*
            Console.Write("Building Visitor Implementation Scaffolding...");
            var visitorImplementations = BuildVisitorImplementations(visitorContextualDetail, distinctImplementations.ToList(), builders, typelessBuilders, assembly);
            Console.WriteLine("Done.");
            Console.Write("Building Visitor Implementations...");
            foreach (var visitorImplementationKey in visitorImplementations.Keys)
            {
                var builder = visitorImplementations[visitorImplementationKey];
                builder.BuildImplementation(assemblyToReflect);
            }
            Console.WriteLine("Done.");
            */
            Console.WriteLine("Press any key to write the output...");
            Console.ReadKey(true);
            Console.Write("Writing Project...");
            WriteProject(Path.Combine(Path.GetDirectoryName(typeof(Program).Assembly.Location), "VisitorModel"), assembly);
            Console.WriteLine("Done.");
        }
        /*
        private static Dictionary<VisitorImplementationTargetDetail, VisitorImplementationBuilder> BuildVisitorImplementations(Dictionary<VisitorTargetDetail, List<IType>> visitorContextualDetail, List<VisitorImplementationTargetDetail> implementationDetail, Dictionary<VisitorTargetDetail, VisitorBuilder> builders, Dictionary<Tuple<string, bool, bool>, VisitorBuilder> typelessBuilders, ICSharpAssembly assembly)
        {
            var result = new Dictionary<VisitorImplementationTargetDetail, VisitorImplementationBuilder>();
            foreach (var implementation in implementationDetail)
            {
                var firstRelevantContext = visitorContextualDetail.Keys.FirstOrDefault(k => implementation.InheritedVisitors.Contains(k.TargetContext) && implementation.ContextualVisitor == k.ContextualVisitor && implementation.YieldingVisitor == k.YieldingVisitor);
                if (firstRelevantContext != null)
                {
                    VisitorBuilder builder;
                    if (!builders.TryGetValue(firstRelevantContext, out builder))
                        typelessBuilders.TryGetValue(new Tuple<string, bool, bool>(firstRelevantContext.TargetContext, firstRelevantContext.ContextualVisitor, firstRelevantContext.YieldingVisitor), out builder);
                    if (builder == null)
                        continue;
                    var allBuilders = GetAllBuilders(builder).Distinct().ToList();
                    VisitorImplementationBuilder implementationBuilder;
                    result.Add(implementation, implementationBuilder = new VisitorImplementationBuilder(implementation, allBuilders));
                    implementationBuilder.BuildScaffoldingClass(assembly);
                }
            }
            return result;
        }
        //*/
        public static IEnumerable<VisitorBuilder> GetAllBuilders(VisitorBuilder builder)
        {
            yield return builder;
            if (builder.ChildBuilders == null)
                yield break;
            foreach (var childBuilder in builder.ChildBuilders)
                foreach (var subBuilder in GetAllBuilders(childBuilder))
                    yield return subBuilder;
        }

        private static Dictionary<Tuple<string, bool, bool>, VisitorBuilder> BuildInheritedVisitors(Dictionary<VisitorTargetDetail, List<IType>> visitorContextualDetail, Dictionary<VisitorTargetDetail, VisitorBuilder> builders, ICSharpAssembly assembly)
        {
            var derivedContextDetail = new MultikeyedDictionary<string, VisitorTargetDetail, List<IType>>();
            var derivedDetail = visitorContextualDetail.Keys.Where(k => !string.IsNullOrEmpty(k.DerivedThroughInheriting)).ToArray();
            foreach (var contextKey in derivedDetail)
                derivedContextDetail.Add(contextKey.TargetContext, contextKey, visitorContextualDetail[contextKey]);
            var inheritanceTree = (from ksvp in derivedContextDetail
                                   orderby ksvp.Keys.Key1, ksvp.Keys.Key2.DerivedThroughInheriting
                                   group ksvp.Keys.Key2.DerivedThroughInheriting by Tuple.Create(ksvp.Keys.Key1, ksvp.Keys.Key2.ContextualVisitor, ksvp.Keys.Key2.YieldingVisitor))
                                  .ToDictionary(
                                    k => k.Key,
                                    v => v.Distinct()
                                          .ToArray());
            var typelessVisitors = new Dictionary<Tuple<string, bool, bool>, VisitorBuilder>();
            foreach (var hierarchyKey in inheritanceTree.Keys)
            {
                bool addedBuilder = false;
                var inheritanceDetail = inheritanceTree[hierarchyKey];
                var firstContext = visitorContextualDetail.Keys.FirstOrDefault(k => k.ContextualVisitor == hierarchyKey.Item2 && k.YieldingVisitor == hierarchyKey.Item3 && k.TargetContext == hierarchyKey.Item1 && string.IsNullOrEmpty(k.DerivedThroughInheriting));
                if (firstContext == null)
                {
                    var builder = GetVisitorBuilderWith(hierarchyKey);
                    firstContext = builder.Detail;
                    typelessVisitors.Add(hierarchyKey, builder);
                    addedBuilder = true;
                }
                var children =
                    (from child in inheritanceDetail.Select(k => Tuple.Create(k, hierarchyKey.Item2, hierarchyKey.Item3))
                     let childContext = visitorContextualDetail.Keys.FirstOrDefault(k => k.ContextualVisitor == child.Item2 && k.YieldingVisitor == child.Item3 && k.TargetContext == child.Item1)
                     let actualChildContext =
                         childContext == null
                         ? GetVisitorBuilderWith(child)
                         : builders.ContainsKey(childContext)
                           ? builders[childContext]
                           : typelessVisitors[new Tuple<string, bool, bool>(childContext.TargetContext, childContext.ContextualVisitor, childContext.YieldingVisitor)]
                     select actualChildContext).ToArray();
                foreach (var childBuilder in children.Where(actualChildContext => actualChildContext.VisitorInterface == null).Select(childBuilder => new { Builder = childBuilder, Key = Tuple.Create(childBuilder.Detail.TargetContext, childBuilder.Detail.ContextualVisitor, childBuilder.Detail.YieldingVisitor) }).Where(x => !typelessVisitors.ContainsKey(x.Key)))
                {
                    typelessVisitors.Add(childBuilder.Key, childBuilder.Builder);
                    childBuilder.Builder.ScaffoldInterface(assembly);
                }

                if (addedBuilder)
                {
                    var currentBuilder = typelessVisitors[hierarchyKey];
                    if (currentBuilder.VisitorInterface == null)
                    {
                        currentBuilder.SkippedTypes =
                            currentBuilder.RelevantTypes =
                            children
                            .Select(k => k.RelevantTypes)
                            .SelectMany(k => k)
                            .Distinct()
                            .ToList();
                        currentBuilder.ScaffoldInterface(assembly);
                        foreach (var childBuilder in children)
                            currentBuilder.VisitorInterface.ImplementedInterfaces.Add(childBuilder.VisitorInterface);
                        currentBuilder.ChildBuilders = children;
                    }
                }
                else
                {
                    var currentBuilder = builders[firstContext];
                    currentBuilder.ChildBuilders = children;
                    foreach (var childBuilder in children)
                        currentBuilder.VisitorInterface.ImplementedInterfaces.Add(childBuilder.VisitorInterface);
                }
            }
            return typelessVisitors;
        }

        private static VisitorBuilder GetVisitorBuilderWith(Tuple<string, bool, bool> hierarchyKey)
        {
            return new VisitorBuilder(new VisitorTargetDetail { ContextualVisitor = hierarchyKey.Item2, YieldingVisitor = hierarchyKey.Item3, TargetContext = hierarchyKey.Item1 });
        }

        private static string[] WriteProject(string resultPath, IIntermediateAssembly newAssembly)
        {
            var opts = new IntermediateCodeTranslatorOptions(/*HtmlCodeFormatterProvider.Singleton/**/new DefaultCodeTranslatorFormatterProvider()/**/);
            opts.AllowPartials = true;
            /* Below controls the emission order of the elements.  The default would be verbatim order (the order they were inserted.) */
            CSharpProjectTranslator cspt = new CSharpProjectTranslator(opts);
            opts.TranslationOrder.Add(DeclarationTranslationOrder.Fields);
            opts.TranslationOrder.Add(DeclarationTranslationOrder.Constructors);
            opts.TranslationOrder.Add(DeclarationTranslationOrder.UnaryOperatorCoercions);
            opts.TranslationOrder.Add(DeclarationTranslationOrder.BinaryOperatorCoercions);
            opts.TranslationOrder.Add(DeclarationTranslationOrder.TypeCoercions);
            opts.TranslationOrder.Add(DeclarationTranslationOrder.Properties);
            opts.TranslationOrder.Add(DeclarationTranslationOrder.Methods);
            opts.TranslationOrder.Add(DeclarationTranslationOrder.Classes);
            opts.TranslationOrder.Add(DeclarationTranslationOrder.Delegates);
            opts.TranslationOrder.Add(DeclarationTranslationOrder.Enums);
            opts.TranslationOrder.Add(DeclarationTranslationOrder.Interfaces);
            /* Make it follow the preferred two-space format. */
            opts.IndentationSpaceCount = 2;
            opts.TranslationOrder.Add(DeclarationTranslationOrder.Structs);
            opts.ElementOrderingMethod = TranslationOrderKind.Specific | TranslationOrderKind.Alphabetic;
            return cspt.WriteProject(newAssembly, resultPath);
        }
        private static ICSharpAssembly CreateAssembly(string assemName, IIntermediateCliManager identityManager)
        {
            IntermediateCliGateway.AddGlobalResolutionPath(Path.GetDirectoryName(typeof(Program).Assembly.Location));
            return LanguageVendors.Microsoft.GetCSharpLanguage().GetProvider(CSharpLanguageVersion.Version5, identityManager).CreateAssembly(assemName);
        }

    }
}
