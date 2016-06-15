using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Ast.Members;
//using AllenCopeland.Abstraction.Slf.Languages.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Transformation
{
    public class VisitorBuilder
    {
        private VisitorTargetDetail _detail;
        private List<IType> _relevantTypes;
        private IIntermediateAssembly _assembly;
        private List<IType> _skippedTypes;

        public VisitorBuilder(VisitorTargetDetail detail) { this._relevantTypes = new List<IType>(); this._detail = detail; }

        public VisitorBuilder(VisitorTargetDetail detail, List<IType> relevantTypes)
        {
            this._detail = detail;
            if (this._detail.TypeParameterVariations.Length > 0)
            {
                var resultRelevantTypes = new List<IType>();
                foreach (var type in relevantTypes)
                    foreach (var typeParamVariation in this._detail.TypeParameterVariations)
                        resultRelevantTypes.Add(((IGenericType)(type)).MakeGenericClosure(typeParamVariation));
                this._relevantTypes = resultRelevantTypes;
            }
            else
                this._relevantTypes = relevantTypes;
            this._skippedTypes = new List<IType>();
        }

        public List<IType> SkippedTypes { get { return this._skippedTypes; } set { this._skippedTypes = value; } }
        public List<IType> RelevantTypes { get { return this._relevantTypes; } set { this._relevantTypes = value; } }

        public void ScaffoldInterface(IIntermediateAssembly assembly)
        {
            this._assembly = assembly;
            var namespaceNames = this._relevantTypes.Select(k => k.NamespaceName).Distinct().ToArray();
            /* We're going to throw-out cases where there's only one type, as it's likely a special case, not the standard. */
            var totalTypeCount = this._relevantTypes.Count;
            var namespaceNamesTrimmed =
                (from name in namespaceNames
                 join type in this._relevantTypes on name equals type.NamespaceName into set
                 where (((float)set.Count()) / (float)totalTypeCount) * 100 > 20
                 select name).Distinct().ToArray();
            if (namespaceNamesTrimmed.Length > 0)
                namespaceNames = namespaceNamesTrimmed;
            var relativeRoot = namespaceNames.GetRelativeRoot(".");
            if (!assembly.Namespaces.PathExists(relativeRoot))
                assembly.Namespaces.Add(relativeRoot);
            var ns = assembly.Namespaces[relativeRoot];
            GenericParameterData[] gpData = new GenericParameterData[(this._detail.ContextualVisitor ? 1 : 0) + (this._detail.YieldingVisitor ? 1 : 0)];
            if (this._detail.YieldingVisitor)
                gpData[0] = new GenericParameterData("TResult");
            if (this._detail.ContextualVisitor)
                gpData[this._detail.YieldingVisitor ? 1 : 0] = new GenericParameterData("TContext");
            var nameParts = this._detail.TargetContext.TitleCaseIdentifierSplit();
            this.NameParts = nameParts;
            if (!this._detail.YieldingVisitor && this._detail.ContextualVisitor)
            {
                var npList = new List<string>(nameParts);
                npList.Insert(npList.Count - 1, "Contextual");
                nameParts = npList.ToArray();
            }

            var visitorInterface = ns.Parts.Add().Interfaces.Add(string.Format("I{0}", string.Join(string.Empty, nameParts)), gpData);
            this.VisitorInterface = visitorInterface;
            this.VisitorInterface.AccessLevel = AccessLevelModifiers.Public;
        }

        public void BuildVisitor()
        {
            var nameWithoutTail = string.Join("", this.NameParts.Take(this.NameParts.Length - 1)).LowerFirstCharacter();
            var namePartsWithoutTail = this.NameParts.Take(this.NameParts.Length - 1).Select(k => k.ToLower()).ToArray();
            var lastItem = namePartsWithoutTail.Last();
            if (lastItem == "code")
#pragma warning disable 642
                ;//Do nothing, keep it the same.
#pragma warning restore 642
            else if (lastItem.EndsWith("y"))
                namePartsWithoutTail[namePartsWithoutTail.Length - 1] = lastItem.Substring(0, lastItem.Length - 1) + "ies";
            else if (!lastItem.EndsWith("s"))
                namePartsWithoutTail[namePartsWithoutTail.Length - 1] = lastItem + "s";
            
            IGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, IInterfaceType> tResult;
            IGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, IInterfaceType> tContext;
            this.VisitorInterface.TypeParameters.TryGetValue(TypeSystemIdentifiers.GetGenericParameterIdentifier("TResult"), out tResult);
            this.VisitorInterface.TypeParameters.TryGetValue(TypeSystemIdentifiers.GetGenericParameterIdentifier("TContext"), out tContext);
            this.VisitorInterface.SummaryText = string.Format("Represents a basic visitor for {2}{0}{1}.", tResult != null ? " which yields a result of @t:TResult;" : string.Empty, tContext != null ? string.Format("{0} has a @t:TContext; relevant to the visit", tResult != null ? " and also" : " which") : string.Empty, string.Join(" ", namePartsWithoutTail));
            if (tResult != null)
                ((IIntermediateGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, IInterfaceType, IIntermediateInterfaceType>)(tResult)).SummaryText = string.Format("Denotes the type of result the members of the @s:{0}; should yield.", this.VisitorInterface.MakeGenericClosure(this.VisitorInterface.GenericParameters).BuildTypeName(true, typeParameterDisplayMode: TypeParameterDisplayMode.CommentStandard));
            if (tContext != null)
                ((IIntermediateGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, IInterfaceType, IIntermediateInterfaceType>)(tContext)).SummaryText = string.Format("Denotes the type of context the members of the @s:{0}; should receive along with the types that accept the visitor.", this.VisitorInterface.MakeGenericClosure(this.VisitorInterface.GenericParameters).BuildTypeName(true, typeParameterDisplayMode: TypeParameterDisplayMode.CommentStandard));
            IType resultType = tResult ?? this._assembly.IdentityManager.ObtainTypeReference(RuntimeCoreType.VoidType);
            var methodLookup = new Dictionary<IType, IIntermediateInterfaceMethodMember>();
            /* Skipped types are for the notion of inheritance-based visitors which are derived from multiple other visitors, we skip the types associated to those inherited visitors to avoid repeating their signatures. */
            foreach (var type in this.RelevantTypes.Except(this.SkippedTypes).OrderBy(k => k.FullName))
            {
                var gpData = new List<GenericParameterData>();
                if (type.IsGenericConstruct)
                {
                    var genericType = (IGenericType)type;
                    if (genericType.IsGenericDefinition)
                    {
                        foreach (IGenericTypeParameter typeParam in genericType.TypeParameters.Values)
                            gpData.Add(new GenericParameterData(typeParam.Name, typeParam.Constraints.Select(k => k.TurnTypeParametersIntoSymbols())/*.Select(constraint=>constraint.IsGenericConstruct ? ((IGenericType)(constraint)).MakeGenericClosure(((IGenericType)(constraint)).GenericParameters) : constraint.IsGenericTypeParameter ? constraint.Name.GetSymbolType() : constraint)*/.ToArray()) { SpecialConstraint = typeParam.SpecialConstraint });
                    }
                }
                TypedNameSeries paramData = new TypedNameSeries();
                paramData.Add(type.WithName(nameWithoutTail));
                
                if (tContext != null)
                    paramData.Add(tContext.WithName("context"));
                var method = this.VisitorInterface.Methods.Add(resultType.WithName("Visit"), paramData, gpData.ToArray());
                methodLookup.Add(type, method);
                if (tContext != null)
                {
                    var contextParam = method.Parameters["context"];
                    contextParam.SummaryText = "The @t:TContext; relevant to the visit.";
                }
                var inputParam = method.Parameters[nameWithoutTail];
                if (tResult != null)
                {
                    method.SummaryText = string.Format("Returns a @t:{1}; after it visits the @p:{0}; provided{2}.", inputParam.Name, tResult.Name, tContext == null ? string.Empty : " with the given @p:context;");
                    method.ReturnsText = @"Returns the value of @t:TResult; relative to the implementation of the visitor.";
                }
                else
                    method.SummaryText = string.Format("Visits the @p:{0}; provided{1}.", inputParam.Name, tContext == null ? string.Empty : " with the given @p:context;");
                var commentType = ((IGenericType)(type));
                string ofText = string.Empty;
                if (commentType.IsGenericConstruct && !commentType.IsGenericDefinition && this._detail.TypeParameterVariations != null && this._detail.TypeParameterVariations.Length > 0)
                {
                    ofText = string.Format(" of @s:{0};", commentType.GenericParameters.First().BuildTypeName());
                    commentType = (IGenericType)commentType.ElementType;
                }
                if (commentType.IsGenericConstruct && commentType.IsGenericDefinition)
                    commentType = commentType.MakeGenericClosure(commentType.GenericParameters);
                inputParam.SummaryText = string.Format("The @s:{0};{1} relevant to the visit.", commentType.BuildTypeName(true, typeParameterDisplayMode: TypeParameterDisplayMode.CommentStandard), ofText);

            }
            this.TypeToMethodLookup = methodLookup;
        }

        public IIntermediateInterfaceType VisitorInterface { get; set; }

        public string[] NameParts { get; set; }

        public Dictionary<IType, IIntermediateInterfaceMethodMember> TypeToMethodLookup { get; set; }

        public VisitorTargetDetail Detail { get { return this._detail; } }

        public VisitorBuilder[] ChildBuilders { get; set; }
    }
}
