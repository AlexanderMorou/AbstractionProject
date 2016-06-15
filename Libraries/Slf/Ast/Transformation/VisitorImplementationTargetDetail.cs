using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AllenCopeland.Abstraction.Slf.Transformation
{
    public class VisitorImplementationTargetDetail :
        IEquatable<VisitorImplementationTargetDetail>
    {
        public string TargetContext { get; set; }
        public bool ContextualVisitor { get; set; }
        public bool YieldingVisitor { get; set; }
        public string Namespace { get; set; }
        public AccessLevelModifiers AccessLevelModifiers { get; set; }
        public override bool Equals(object obj)
        {
            return this.Equals(obj as VisitorImplementationTargetDetail);
        }

        public bool Equals(VisitorImplementationTargetDetail other)
        {
            if (other == null)
                return false;
            return other.TargetContext == this.TargetContext &&
                    other.ContextualVisitor == this.ContextualVisitor &&
                    other.YieldingVisitor == this.YieldingVisitor;
        }
        public override int GetHashCode()
        {
            return (this.TargetContext ?? string.Empty).GetHashCode() ^
                    this.ContextualVisitor.GetHashCode() ^
                    this.YieldingVisitor.GetHashCode();
        }

        public static VisitorImplementationTargetDetail DeriveFromMetadata(IMetadatum metadatum)
        {
            var targetContext = metadatum.Parameters.GetIndexedParameter<string>(0);
            var yieldingVisitor = metadatum.NamedParameters.GetNamedParameter<bool>("YieldingVisitor");
            var contextualVisitor = metadatum.NamedParameters.GetNamedParameter<bool>("ContextualVisitor");
            var accessLevelModifiers = metadatum.NamedParameters.GetNamedParameter<int>("VisitRefactorAbstract");
            var @namespace = metadatum.NamedParameters.GetNamedParameter<string>("Namespace");

            return new VisitorImplementationTargetDetail
            {
                TargetContext               = targetContext.Value,
                ContextualVisitor           = contextualVisitor.IsEmpty
                                              ? false
                                              : contextualVisitor.Value,
                YieldingVisitor             = yieldingVisitor.IsEmpty
                                              ? false
                                              : yieldingVisitor.Value,
                Namespace                     = @namespace.IsEmpty
                                              ? null
                                              : @namespace.Value,
                AccessLevelModifiers          = accessLevelModifiers.IsEmpty
                                                ? AccessLevelModifiers.Public
                                                : (AccessLevelModifiers)accessLevelModifiers.Value,
            };
        }

        public override string ToString()
        {
            return string.Format("{3}{0}{2}", this.TargetContext, this.ContextualVisitor ? this.YieldingVisitor ? "<TResult, TContext>" : "<TContext>" : this.YieldingVisitor ? "<TResult>" : string.Empty, this.YieldingVisitor ? "TResult " : string.Empty);
        }

        public IControlledCollection<VisitorImplementationInheritanceDetail> InheritanceDetail { get; internal set; }
    }

    public class VisitorImplementationActionDetail
    {
        public string TargetContext { get; private set; }
        public IType TargetType { get; private set; }
        public string TargetAction { get; private set; }
        public string TargetPluralAction { get; private set; }
        public IType TContext { get; private set; }
        public IType TResult { get; private set; }
        public bool ContextualVisitor { get; private set; }
        public bool YieldingVisitor { get; private set; }
        public static VisitorImplementationActionDetail DeriveFromMetadata(IMetadatum metadatum)
        {
            var targetContext           = metadatum.Parameters.GetIndexedParameter<string>(0);
            var targetType              = metadatum.Parameters.GetIndexedParameter<IType> (1);
            var targetAction            = metadatum.Parameters.GetIndexedParameter<string>(2);
            var targetPluralAction      = metadatum.Parameters.GetIndexedParameter<string>(3);
            var contextualVisitor       = metadatum.NamedParameters.GetNamedParameter<bool>     ("ContextualVisitor");
            var yieldingVisitor         = metadatum.NamedParameters.GetNamedParameter<bool>     ("YieldingVisitor");
            var contextType             = metadatum.NamedParameters.GetNamedParameter<IType>    ("TContext");
            var resultType              = metadatum.NamedParameters.GetNamedParameter<IType>    ("TResult");
            return new VisitorImplementationActionDetail
            {
                TargetAction                = !targetAction.IsEmpty
                                              ? targetAction.Value
                                              : null,
                TargetType                  = !targetType.IsEmpty
                                              ? targetType.Value
                                              : null,
                TargetContext               = !targetContext.IsEmpty
                                              ? targetContext.Value
                                              : null,
                TargetPluralAction          = !targetPluralAction.IsEmpty
                                              ? targetPluralAction.Value
                                              : null,
                ContextualVisitor           = contextualVisitor.IsEmpty
                                              ? false
                                              : contextualVisitor.Value,
                YieldingVisitor             = yieldingVisitor.IsEmpty
                                              ? false
                                              : yieldingVisitor.Value,
            };
        }
    }

    public class VisitorImplementationInheritanceDetail :
        IEquatable<VisitorImplementationInheritanceDetail>
    {
        private static readonly string[] emptyStringSet = new string[0];
        public string TargetContext { get; private set; }
        public bool ContextualVisitor { get; private set; }
        public bool YieldingVisitor { get; private set; }
        public IType TContext { get; private set; }
        public IType TResult { get; private set; }
        public string[] InheritedVisitors { get; private set; }
        public string VisitRefactorName { get; private set; }
        public bool VisitRefactorAbstract { get; private set; }
        public IType VisitorBuilderHandler { get; private set; }
        public static VisitorImplementationInheritanceDetail DeriveFromMetadata(IMetadatum metadatum)
        {
            var targetContext           = metadatum.     Parameters.GetIndexedParameter<string> (0);
            var contextualVisitor       = metadatum.NamedParameters.GetNamedParameter<bool>     ("ContextualVisitor");
            var yieldingVisitor         = metadatum.NamedParameters.GetNamedParameter<bool>     ("YieldingVisitor");
            var contextType             = metadatum.NamedParameters.GetNamedParameter<IType>    ("TContext");
            var resultType              = metadatum.NamedParameters.GetNamedParameter<IType>    ("TResult");
            var inheritedVisitors       = metadatum.NamedParameters.GetNamedParameter<string[]> ("InheritedVisitors");
            var visitRefactorName       = metadatum.NamedParameters.GetNamedParameter<string>   ("VisitRefactorName");
            var visitRefactorAbstract   = metadatum.NamedParameters.GetNamedParameter<bool>     ("VisitRefactorAbstract");
            var visitorBuilderHandler   = metadatum.NamedParameters.GetNamedParameter<IType>    ("VisitorBuilderHandler");

            return new VisitorImplementationInheritanceDetail
            {
                TargetContext               = !targetContext.IsEmpty
                                              ? targetContext.Value
                                              : null,
                ContextualVisitor           = contextualVisitor.IsEmpty
                                              ? false
                                              : contextualVisitor.Value,
                YieldingVisitor             = yieldingVisitor.IsEmpty
                                              ? false
                                              : yieldingVisitor.Value,
                VisitRefactorName           = visitRefactorName.IsEmpty
                                              ? null
                                              : visitRefactorName.Value,
                VisitRefactorAbstract       = visitRefactorAbstract.IsEmpty
                                              ? false
                                              : visitRefactorAbstract.Value,
                InheritedVisitors           = inheritedVisitors.IsEmpty
                                              ? null
                                              : inheritedVisitors.Value,
                TContext                    = contextType.IsEmpty
                                              ? null
                                              : contextType.Value,
                TResult                     = resultType.IsEmpty
                                              ? null
                                              : resultType.Value,
                VisitorBuilderHandler       = visitorBuilderHandler.IsEmpty
                                              ? null
                                              : visitorBuilderHandler.Value,
            };
        }

        public VisitorImplementationTargetDetail ImplementationDetailMatch
        {
            get
            {
                return new VisitorImplementationTargetDetail
                {
                    TargetContext = this.TargetContext,
                    ContextualVisitor = this.TContext == null ? this.ContextualVisitor : false,
                    YieldingVisitor = this.TResult == null ? this.YieldingVisitor : false,
                };
            }
        }

        public IEnumerable<VisitorTargetDetail> VisitorTargets
        {
            get
            {
                foreach (var visitor in this.InheritedVisitors)
                    yield return new VisitorTargetDetail()
                    {
                        TargetContext       = visitor,
                        ContextualVisitor   = this.ContextualVisitor,
                        YieldingVisitor     = this.YieldingVisitor,
                    };
            }
        }

        public bool Equals(VisitorImplementationInheritanceDetail other)
        {
            return other.TargetContext == this.TargetContext                            &&
                   other.ContextualVisitor == this.ContextualVisitor                    &&
                   other.YieldingVisitor == this.YieldingVisitor                        &&
                  (other.InheritedVisitors == null && this.InheritedVisitors == null    ||
                   other.InheritedVisitors != null && this.InheritedVisitors != null    &&
                   other.InheritedVisitors.SequenceEqual(this.InheritedVisitors))       &&
                   other.VisitRefactorAbstract == this.VisitRefactorAbstract            &&
                   other.VisitRefactorName == this.VisitRefactorName                    &&
                  (other.TContext != null && this.TContext != null                      && 
                   other.TContext.Equals(this.TContext)                                 ||
                   other.TContext == null && this.TContext == null)                     &&
                  (other.TResult != null && this.TResult != null                        &&
                   other.TResult.Equals(this.TResult)                                   ||
                   other.TResult == null && this.TResult == null)                       ;
        }

        public override int GetHashCode()
        {
            return (this.TargetContext ?? string.Empty).GetHashCode() ^
                    this.ContextualVisitor.GetHashCode() ^
                    this.YieldingVisitor.GetHashCode() ^
                   ((this.InheritedVisitors ?? emptyStringSet).Select(k => (int?)k.GetHashCode()).Average() ?? 0).GetHashCode() ^
                   (this.TContext           != null ? this.TContext.GetHashCode()           : 0) ^
                   (this.TResult            != null ? this.TResult.GetHashCode()            : 0) ^
                   (this.VisitRefactorName  != null ? this.VisitRefactorName.GetHashCode()  : 0) ^
                   this.VisitRefactorAbstract.GetHashCode();
        }


    }
}
