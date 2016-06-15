using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract.Modules;
using AllenCopeland.Abstraction.Slf.Ast;
//using AllenCopeland.Abstraction.Slf.Ast.Cli;
//using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using AllenCopeland.Abstraction.Slf.Ast.Modules;
using AllenCopeland.Abstraction.Slf.Ast.Statements;
//using AllenCopeland.Abstraction.Slf.Languages.CSharp.Expressions;
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
using AllenCopeland.Abstraction.Slf.Translation;
//using AllenCopeland.Abstraction.Slf.Languages.CSharp.Translation;

namespace AllenCopeland.Abstraction.Slf.Transformation
{
    public class VisitorTargetDetail :
        IEquatable<VisitorTargetDetail>
    {
        public string TargetContext { get; set; }
        public bool ContextualVisitor { get; set; }
        public bool YieldingVisitor { get; set; }
        public IType[] TypeParameterVariations { get; set; }
        public string ParameterlessName { get; set; }
        public string DerivedThroughInheriting { get; set; }
        public override bool Equals(object obj)
        {
            return this.Equals(obj as VisitorTargetDetail);
        }


        public VisitorTargetDetail()
        {
            this.TypeParameterVariations = new IType[0];
        }

        public bool Equals(VisitorTargetDetail other)
        {
            if (other == null)
                return false;
            if (other.TypeParameterVariations == null && this.TypeParameterVariations != null || other.TypeParameterVariations != null && this.TypeParameterVariations == null ||
                other.TypeParameterVariations != null && this.TypeParameterVariations != null && !other.TypeParameterVariations.SequenceEqual(this.TypeParameterVariations))
                return false;
            return other.TargetContext == this.TargetContext &&
                    other.ContextualVisitor == this.ContextualVisitor &&
                    other.YieldingVisitor == this.YieldingVisitor &&
                    other.ParameterlessName == this.ParameterlessName &&
                    other.DerivedThroughInheriting == this.DerivedThroughInheriting;
        }
        public override int GetHashCode()
        {
            return (this.TargetContext ?? string.Empty).GetHashCode() ^
                    this.ContextualVisitor.GetHashCode() ^
                    this.YieldingVisitor.GetHashCode() ^
                    (this.ParameterlessName ?? string.Empty).GetHashCode() ^
                    (this.DerivedThroughInheriting ?? string.Empty).GetHashCode();
        }

        public static VisitorTargetDetail DeriveFromMetadata(IMetadatum metadatum)
        {
            var derivedThroughInheriting = metadatum.NamedParameters.GetNamedParameter<string>("DerivedThroughInheriting");
            var targetContext = metadatum.Parameters.GetIndexedParameter<string>(0);
            var yieldingVisitor = metadatum.NamedParameters.GetNamedParameter<bool>("YieldingVisitor");
            var parameterlessName = metadatum.NamedParameters.GetNamedParameter<string>("ParameterlessName");
            var contextualVisitor = metadatum.NamedParameters.GetNamedParameter<bool>("ContextualVisitor");
            var typeParamVariations = metadatum.Parameters.GetIndexedParameter<IType[]>(1);
            return new VisitorTargetDetail
            {
                DerivedThroughInheriting    = derivedThroughInheriting.IsEmpty
                                            ? null
                                            : derivedThroughInheriting.Value,
                TargetContext               = targetContext.Value,
                ContextualVisitor           = contextualVisitor.IsEmpty
                                            ? false
                                            : contextualVisitor.Value,
                YieldingVisitor             = yieldingVisitor.IsEmpty
                                            ? false
                                            : yieldingVisitor.Value,
                ParameterlessName           = parameterlessName.IsEmpty
                                            ? null
                                            : parameterlessName.Value,
                TypeParameterVariations     = typeParamVariations.IsEmpty
                                            ? null
                                            : typeParamVariations.Value,
            };
        }
        public override string ToString()
        {
            return string.Format("{3}{0}{2}{1}", this.TargetContext, !string.IsNullOrEmpty(this.DerivedThroughInheriting) ? string.Format(" : {0}", this.DerivedThroughInheriting) : string.Empty, this.ContextualVisitor ? this.YieldingVisitor ? "<TResult, TContext>" : "<TContext>" : this.YieldingVisitor ? "<TResult>" : string.Empty, this.YieldingVisitor ? "TResult " : string.Empty);
        }


        public IEnumerable<string> InheritanceDetail
        {
            get
            {
                if (!string.IsNullOrEmpty(this.DerivedThroughInheriting))
                    yield return this.DerivedThroughInheriting;
            }
        }

        public bool HasInheritanceDetail
        {
            get { return !string.IsNullOrEmpty(this.DerivedThroughInheriting); }
        }
    }
}
