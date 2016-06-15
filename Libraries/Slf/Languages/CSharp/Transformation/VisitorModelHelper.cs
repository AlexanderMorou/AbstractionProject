using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Ast.Cli;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using AllenCopeland.Abstraction.Utilities;
using AllenCopeland.Abstraction.Utilities.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AllenCopeland.Abstraction.Slf.Cli;
using ImplementedInterfaceLookup = AllenCopeland.Abstraction.Utilities.Collections.IControlledDictionary<AllenCopeland.Abstraction.Slf.Abstract.IType, System.Collections.Generic.IList<AllenCopeland.Abstraction.Slf.Abstract.IType>>;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Languages.CSharp;
namespace AllenCopeland.Abstraction.Slf.Transformation
{
#if DEBUG
    public static class VisitorModelHelper
    {
        internal static IEnumerable<VisitorBuilder> GetVisitorContexts(IAssembly assembly, VisitorModelCommonTypeContext attributeContext, ICSharpAssembly resultAssembly, IIntermediateCliManager identityManager)
        {
            var visitorRelevantTypes = assembly.GetTypes().Where(t => t.Metadata.Contains(attributeContext.TargetAttribute)).ToArray();
            /* Using the metadata supplied on the relevant types, group them by their distinct visitor targets.
             * We'll use the distinct targets to generate concrete visitor interfaces.
             * The VisitorTargetAttribute allows multiple applications, so we may have multiple possible visitors for a single type. */
            var visitorContextualDetail = (from type in visitorRelevantTypes
                                           from metadatum in type.Metadata.Where(k => k.Type == attributeContext.TargetAttribute)
                                           let currentDetail = VisitorTargetDetail.DeriveFromMetadata(metadatum)
                                           group type by currentDetail into distinctVisitorDetail
                                           orderby distinctVisitorDetail.Key.TargetContext,
                                                   string.IsNullOrEmpty(distinctVisitorDetail.Key.DerivedThroughInheriting)
                                                   ? string.Empty
                                                   : distinctVisitorDetail.Key.DerivedThroughInheriting,
                                                   distinctVisitorDetail.Key.ContextualVisitor,
                                                   distinctVisitorDetail.Key.YieldingVisitor
                                           select distinctVisitorDetail).ToDictionary(k => k.Key, v => new HashSet<IType>(v));
            /* Break the sets into two variations:
             * 1. Visitors which are derived from other visitors */
            var inheritanceBasedDetail =
                visitorContextualDetail.Keys.Where(k => !string.IsNullOrEmpty(k.DerivedThroughInheriting)).ToArray();
            /* 2. The concrete visitors which aren't representative of derivation.  There may be overlap. */
            var nonDerivedVisitors =
                visitorContextualDetail.Keys.Where(k => string.IsNullOrEmpty(k.DerivedThroughInheriting)).ToArray();

            var directVisitors = (from context in nonDerivedVisitors
                                  let builder = GetVisitorFrom(context, visitorContextualDetail[context], resultAssembly)
                                  select builder).ToDictionary(k => k.Detail, v => v);

            var inheritanceTree =
                (from detail in inheritanceBasedDetail
                 orderby detail.TargetContext, detail.DerivedThroughInheriting
                 /* We group by a *new* TargetVisitorDetail since we're not interested in the DerivedThroughInheriting, which would fragment the visitors. */
                 group detail.DerivedThroughInheriting by new VisitorTargetDetail { TargetContext = detail.TargetContext, ContextualVisitor = detail.ContextualVisitor, YieldingVisitor = detail.YieldingVisitor })
                 .ToDictionary(k => k.Key, v => v.Distinct().ToArray());

            var inheritanceOnlyVisitors = new Dictionary<VisitorTargetDetail, VisitorBuilder>();
            foreach (var inheritanceKey in inheritanceTree.Keys)
            {
                var inheritedVisitorNames = inheritanceTree[inheritanceKey];
                var iovKey = new VisitorTargetDetail { TargetContext = inheritanceKey.TargetContext, ContextualVisitor = inheritanceKey.ContextualVisitor, YieldingVisitor = inheritanceKey.YieldingVisitor };
                VisitorBuilder mainBuilder;
                if (!directVisitors.TryGetValue(inheritanceKey, out mainBuilder) && !inheritanceOnlyVisitors.TryGetValue(iovKey, out mainBuilder))
                    inheritanceOnlyVisitors.Add(iovKey, mainBuilder = new VisitorBuilder(inheritanceKey));

                var childBuilders =
                    (from childDetail in inheritedVisitorNames
                                         .Select(k =>
                                             GetRelevantDetail(nonDerivedVisitors, inheritanceKey, k))
                     select directVisitors.ContainsKey(childDetail)
                            ? directVisitors[childDetail]
                            : inheritanceOnlyVisitors.ContainsKey(childDetail)
                              ? inheritanceOnlyVisitors[childDetail]
                              : new VisitorBuilder(childDetail)).ToArray();
                foreach (var childBuilder in childBuilders.Where(childBuilder => childBuilder.VisitorInterface == null))
                {
                    inheritanceOnlyVisitors.Add(childBuilder.Detail, childBuilder);
                    childBuilder.ScaffoldInterface(resultAssembly);
                }
                if (mainBuilder.VisitorInterface == null)
                {
                    mainBuilder.SkippedTypes =
                        mainBuilder.RelevantTypes =
                            childBuilders
                            .SelectMany(k => k.RelevantTypes)
                            .Distinct()
                            .ToList();
                    mainBuilder.ScaffoldInterface(resultAssembly);
                }
                mainBuilder.ChildBuilders = childBuilders;
                foreach (var childBuilder in childBuilders)
                    mainBuilder.VisitorInterface.ImplementedInterfaces.Add(childBuilder.VisitorInterface);
            }

            foreach (var builder in inheritanceOnlyVisitors.Values.Concat(directVisitors.Values))
                builder.BuildVisitor();

            return inheritanceOnlyVisitors
                .Values
                .Concat(directVisitors.Values)
                .OrderBy(k => k.Detail.TargetContext)
                .ThenBy(k => k.Detail.ContextualVisitor)
                .ThenBy(k => k.Detail.YieldingVisitor)
                .ThenBy(k => string.IsNullOrEmpty(k.Detail.DerivedThroughInheriting) ? string.Empty : k.Detail.DerivedThroughInheriting)
                .ToArray();
        }

        private static VisitorTargetDetail GetRelevantDetail(VisitorTargetDetail[] nonDerivedVisitors, VisitorTargetDetail inheritanceKey, string k)
        {
            var ndvc = nonDerivedVisitors
                       .FirstOrDefault(j => j.TargetContext == k &&
                                            j.ContextualVisitor == inheritanceKey.ContextualVisitor &&
                                            j.YieldingVisitor == inheritanceKey.YieldingVisitor);
            return ndvc ?? new VisitorTargetDetail
                           {
                               TargetContext = k,
                               ContextualVisitor = inheritanceKey.ContextualVisitor,
                               YieldingVisitor = inheritanceKey.YieldingVisitor,
                           };
        }

        public static VisitorBuilder GetVisitorFrom(VisitorTargetDetail detail, IEnumerable<IType> types, ICSharpAssembly assembly)
        {
            var builder = new VisitorBuilder(detail, types.ToList());
            builder.ScaffoldInterface(assembly);
            return builder;
        }
        public static IEnumerable<IVisitorImplementationBuilder> GetImplementationContext(IAssembly assembly, ICSharpAssembly resultAssembly, IIntermediateCliManager identityManager)
        {
            var attributeContext = new VisitorModelCommonTypeContext(identityManager);
            var visitorBuilders = GetVisitorContexts(assembly, attributeContext, resultAssembly, identityManager);
            var distinctImplementations = new HashSet<VisitorImplementationTargetDetail>(
                from metadatum in assembly.Metadata.Where(k=>k.Type == attributeContext.ImplementationTargetAttribute)
                select VisitorImplementationTargetDetail.DeriveFromMetadata(metadatum));
            var inheritanceDetails =
                new HashSet<VisitorImplementationInheritanceDetail>(
                    assembly
                    .Metadata
                    .Where(k  => k.Type == attributeContext.ImplementationInheritanceAttribute)
                    .Select(k => VisitorImplementationInheritanceDetail.DeriveFromMetadata(k)));
            var implementationDetail =
                (from inheritanceDetail in inheritanceDetails
                 orderby inheritanceDetail.TargetContext,
                         inheritanceDetail.VisitRefactorName,
                         inheritanceDetail.VisitRefactorAbstract,
                         inheritanceDetail.ContextualVisitor,
                         inheritanceDetail.YieldingVisitor
                 join implementation in distinctImplementations on inheritanceDetail.ImplementationDetailMatch equals implementation
                 group inheritanceDetail by implementation).ToDictionary(k => k.Key, v => v.ToArray());
            foreach (var detail in implementationDetail.Keys)
                detail.InheritanceDetail = new ControlledCollection<VisitorImplementationInheritanceDetail>(implementationDetail[detail]);
            var implementationBuilders =
                (from implementation in implementationDetail.Keys
                 let visitorBuilderQuery =
                     from inheritanceDetail in implementationDetail[implementation]
                     from visitorDetail in inheritanceDetail.VisitorTargets
                     join visitor in visitorBuilders on new { visitorDetail.TargetContext, visitorDetail.ContextualVisitor, visitorDetail.YieldingVisitor } equals new { visitor.Detail.TargetContext, visitor.Detail.ContextualVisitor, visitor.Detail.YieldingVisitor }
                     group visitor by GetInheritanceDetailKey(inheritanceDetail)
                 select GetImplementationBuilder(resultAssembly, attributeContext, implementation, visitorBuilderQuery, assembly))
                .ToArray();
            foreach (var builder in implementationBuilders)
                yield return builder;
        }

        private static VisitorImplementationBuilder GetImplementationBuilder(ICSharpAssembly resultAssembly, VisitorModelCommonTypeContext attributeContext, VisitorImplementationTargetDetail implementation, IEnumerable<IGrouping<InheritanceDetailKey, VisitorBuilder>> visitorBuilderQuery, IAssembly assembly)
        {
            var result = new VisitorImplementationBuilder(
                new VisitorImplementationContext(
                    implementation,
                    attributeContext,
                    visitorBuilderQuery.Select(
                        v => new VisitorImplementationVariationContext(
                            v.ToArray(),
                            v.Key.TContext,
                            v.Key.TResult,
                            v.Key.TargetContext,
                            v.Key.TargetAbstract,
                            v.Key.VisitorBuilderHandler,
                            assembly)),
                    resultAssembly));
            foreach (var variation in result.Context.Variations)
                variation.ImplementationBuilder = result;
            return result;
        }

        private static InheritanceDetailKey GetInheritanceDetailKey(VisitorImplementationInheritanceDetail inheritanceDetail)
        {
            return new InheritanceDetailKey(
                inheritanceDetail.VisitorBuilderHandler,
                inheritanceDetail.TContext,
                inheritanceDetail.TResult,
                inheritanceDetail.VisitRefactorName ?? string.Empty,
                inheritanceDetail.VisitRefactorAbstract);
        }
        private class InheritanceDetailKey
        {
            public IType TContext { get; private set; }
            public IType TResult { get; private set; }
            public string TargetContext { get; private set; }
            public bool TargetAbstract { get; private set; }
            public IType VisitorBuilderHandler { get; private set; }
            public InheritanceDetailKey(IType visitorBuilderHandler, IType contextType, IType resultType, string targetContext, bool targetAbstract)
            {
                this.TContext = contextType;
                this.TResult = resultType;
                this.VisitorBuilderHandler = visitorBuilderHandler;
                this.TargetContext = targetContext;
                this.TargetAbstract = targetAbstract;
            }
        }
    }
#endif
}
