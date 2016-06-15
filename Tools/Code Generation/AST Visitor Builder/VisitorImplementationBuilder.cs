using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
using AllenCopeland.Abstraction.Slf.Languages.CSharp;
using AllenCopeland.Abstraction.Slf.Languages.CSharp.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AllenCopeland.Abstraction.Slf.Ast.Cli;
using AllenCopeland.Abstraction.Slf.Ast.Statements;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Transformation;

namespace AllenCopeland.Abstraction.Tools.VisitorBuilder
{
    public class VisitorImplementationBuilder
    {
        private VisitorImplementationTargetDetail _detail;
        private ICSharpAssembly _assembly;
        private List<VisitorBuilder> _builders;
        private static IInterfaceType IEnumerableOfT;
        private static IInterfaceType IControlledDictionaryOfTKeyTValue;
        private static IInterfaceType IntermediateDeclDict;
        public VisitorImplementationBuilder(VisitorImplementationTargetDetail detail, List<VisitorBuilder> builders)
        {
            this._builders = builders;
            this._detail = detail;
        }

        public VisitorImplementationTargetDetail Detail
        {
            get
            {
                return this._detail;
            }
        }

        public void BuildScaffoldingClass(ICSharpAssembly assembly)
        {
            this._assembly = assembly;
            if (!assembly.Namespaces.PathExists(this.Detail.Namespace))
                assembly.Namespaces.Add(this.Detail.Namespace);
            var ns = assembly.Namespaces[this.Detail.Namespace];
            this.VisitorResult = ns.Parts.Add().Classes.Add(this.Detail.TargetContext);
            this.VisitorResult.AccessLevel = AccessLevelModifiers.Public;
        }

        public void BuildImplementation(IAssembly target)
        {
            if (IEnumerableOfT == null)
                IEnumerableOfT = typeof(IEnumerable<>).GetTypeReference<IInterfaceType>(this._assembly.IdentityManager);
            if (IControlledDictionaryOfTKeyTValue == null)
                IControlledDictionaryOfTKeyTValue = typeof(IControlledDictionary<,>).GetTypeReference<IInterfaceType>(this._assembly.IdentityManager);
            if (IntermediateDeclDict == null)
                IntermediateDeclDict = typeof(IIntermediateDeclarationDictionary<,,>).GetTypeReference<IInterfaceType>(this._assembly.IdentityManager);
            IType ignoreAttr = typeof(VisitorImplementationIgnorePropertyAttribute).GetTypeReference(this._assembly.IdentityManager);
            IType ignoreSetAttr = typeof(VisitorImplementationIgnoreLocalSetAttribute).GetTypeReference(this._assembly.IdentityManager);
            var requireAttr = typeof(VisitorPropertyRequirementAttribute).GetTypeReference(this._assembly.IdentityManager);
            bool visitorsDoNotNeedConstraints = !string.IsNullOrEmpty(this.Detail.VisitRefactorName);
            var builderInterfaces = this._builders.SelectMany(k => k.RelevantTypes).Distinct().OfType<IInterfaceType>().ToArray();
            //var allEnumerables = builderInterfaces.Select(k => CreateEnumerableVariant(k)).ToArray();
            var distinctInterfaces =
                builderInterfaces.Concat(builderInterfaces.SelectMany(k => k.ImplementedInterfaces)).Distinct().ToArray();
            var interfaceProperties =
                (from iFace in distinctInterfaces.OfType<IInterfaceType>()
                 from prop in iFace.Properties.Values/*.Where(k => IsPropertyRelevant(ignoreAttr, builderInterfaces, k))*/.DefaultIfEmpty()
                 group prop by iFace).ToDictionary(k => k.Key, v => (v.First() == null ? (IEnumerable<IInterfacePropertyMember>)new IInterfacePropertyMember[0] : v).ToList());
            var interfacesWithAccept = distinctInterfaces.OfType<IInterfaceType>().Where(k=>k.Methods.Values.Any(j=>j.Name=="Accept")).ToArray();
            var actionDetail = typeof(VisitorImplementationActionDetailAttribute).GetTypeReference<IClassType>(this._assembly.IdentityManager);
            var relevantActionDetail = target.Metadata.Where(k => k.Type == actionDetail && k.Parameters.GetIndexedParameter<string>(0).Item2 == this.Detail.TargetContext);
            var relevantActionDetailLookup = relevantActionDetail.ToDictionary(k => k.Parameters.GetIndexedParameter<IType>(1).Item2, v => VisitorImplementationActionDetail.DeriveFromMetadata(v));
            var relevantActionTypes = relevantActionDetailLookup.Keys.ToArray();
            //foreach (var key in relevantActionTypes)
            //    foreach (var @interface in key.ImplementedInterfaces)
            //        if (!relevantActionDetailLookup.ContainsKey(@interface))
            //            relevantActionDetailLookup.Add(@interface, relevantActionDetailLookup[key]);
            builderInterfaces = builderInterfaces.Concat(interfacesWithAccept).Distinct().ToArray();
            var inheritanceDetail = (from iFace in builderInterfaces
                                  let baseTypes = iFace.ImplementedInterfaces.OfType<IInterfaceType>()
                                  from iFace2 in new[] { iFace }.Concat(baseTypes).Distinct()
                                  group iFace2 by iFace).ToDictionary(k => k.Key, v => v.ToArray());

            bool isVoidReturn = !this.Detail.YieldingVisitor;
            if (visitorsDoNotNeedConstraints)
            {
                this.SecondaryResult = this.VisitorResult.Parts.Add();
                var relativeRoot = new string[] { this.VisitorResult.NamespaceName, this._assembly.DefaultNamespace.FullName }.GetRelativeRoot(".");
                if (!string.IsNullOrEmpty(relativeRoot))
                {
                    relativeRoot = this.VisitorResult.NamespaceName.Substring(relativeRoot.Length + 1);
                    relativeRoot = relativeRoot.Replace(".", @"\") + @"\";
                }
                
                this.SecondaryResult.Assembly.FileName = string.Format("{2}{0} ({1} Signatures)", this.Detail.TargetContext, this.Detail.VisitRefactorName, relativeRoot);
                this.VisitorResult.Assembly.FileName = string.Format("{1}{0} (Visitor Signatures)", this.Detail.TargetContext, relativeRoot);
            }
            foreach (var builder in this._builders)
            {
                if (this.Detail.InheritedVisitors.Contains(builder.Detail.TargetContext))
                    this.VisitorResult.ImplementedInterfaces.ImplementInterfaceQuick(builder.VisitorInterface);
                foreach (var method in builder.VisitorInterface.Methods.Values)
                {
                    var gpData = new GenericParameterData[method.GenericParameters.Count];
                    if (method.TypeParameters.Count > 0)
                        for (int i = 0; i < method.TypeParameters.Count; i++)
                        {
                            var gParam = method.TypeParameters.Values[i];
                            gpData[i] = new GenericParameterData(gParam.Name);
                            if (!visitorsDoNotNeedConstraints)
                            {
                                gpData[i].Constraints.AddRange(gParam.Constraints.Select(k => k.TurnTypeParametersIntoSymbols()));
                                gpData[i].SpecialConstraint = gParam.SpecialConstraint;
                            }
                        }
                    var parameterData = new TypedNameSeries(method.Parameters.Values.Select(k => k.ParameterType.WithName(k.Name)));
                    var newMethod = this.VisitorResult.Methods.Add(method.ReturnType.WithName(method.Name), parameterData, gpData);
                    if (visitorsDoNotNeedConstraints)
                    {
                        newMethod.Implementations.Add(builder.VisitorInterface);
                        foreach (var gpDetail in from gpDatum in gpData
                                                 join genericParameter in method.TypeParameters.Values on gpDatum.Name equals genericParameter.Name
                                                 select new { GenericParameterDatum = gpDatum, GenericParameter = genericParameter })
                        {
                            gpDetail.GenericParameterDatum.Constraints.AddRange(gpDetail.GenericParameter.Constraints.Select(k => k.TurnTypeParametersIntoSymbols()));
                            gpDetail.GenericParameterDatum.SpecialConstraint = gpDetail.GenericParameter.SpecialConstraint;
                        }
                        var secondNewMethod = this.SecondaryResult.Methods.Add(method.ReturnType.WithName(this.Detail.VisitRefactorName), parameterData, gpData);
                        secondNewMethod.AccessLevel = AccessLevelModifiers.Public;
                        if (this.Detail.VisitRefactorAbstract)
                            secondNewMethod.IsAbstract = true;
                        var paramRefs = parameterData.Select(k => newMethod.Parameters[k.Name].GetReference()).ToArray();
                        var invocation = newMethod.IsGenericConstruct 
                                         ? secondNewMethod.GetReference(null, newMethod.GenericParameters).Invoke(paramRefs)
                                         : secondNewMethod.GetReference(null).Invoke(paramRefs);
                        var firstParam = newMethod.Parameters.Values[0];
                        var firstParamType = firstParam.ParameterType;
                        if (firstParamType is IInterfaceType)
                        {
                            foreach (var prop in EnumerateInterfaceProperties((IInterfaceType)firstParamType, ignoreAttr, inheritanceDetail, interfaceProperties))
                            {
                                var propertyRelevanceInfo = IsPropertyRelevant(prop.Item1, builderInterfaces, ignoreAttr, relevantActionTypes);
                                var requirement = prop.Item1.Metadata[requireAttr];
                                IBlockStatementParent codeTarget = secondNewMethod;
                                if (propertyRelevanceInfo.Item2 != VisitorImplementationTypeRelevance.NotRelevant)
                                {
                                    if (requirement != null)
                                        codeTarget = codeTarget.If(firstParam.GetReference().GetProperty(requirement.Parameters.GetIndexedParameter<string>(0).Item2));
                                    else if (prop.Item1.PropertyType.Type == TypeKind.Interface || prop.Item1.PropertyType.Type == TypeKind.Class)
                                        codeTarget = codeTarget.If(firstParam.GetReference().GetProperty(prop.Item1.Name).InequalTo(IntermediateGateway.NullValue));
                                }
                                switch (propertyRelevanceInfo.Item2)
                                {
                                    case VisitorImplementationTypeRelevance.AsItem:
                                        codeTarget.Call(firstParam.GetReference().GetProperty(prop.Item1.Name).GetMethod("Accept").Invoke(new IExpression[] { this.VisitorResult.GetThis() }.Concat(newMethod.Parameters.Values.Skip(1).Select(k => k.GetReference())).ToArray()));
                                        break;
                                    case VisitorImplementationTypeRelevance.AsActionableItemSet:
                                        ProcessActionableSet(relevantActionDetailLookup, firstParam, prop, propertyRelevanceInfo, codeTarget, 0, null);
                                        break;
                                    case VisitorImplementationTypeRelevance.AsActionableValueSet:
                                        ProcessActionableSet(relevantActionDetailLookup, firstParam, prop, propertyRelevanceInfo, codeTarget, 1, "Values");
                                        break;
                                    case VisitorImplementationTypeRelevance.AsActionableDeclValueSet:
                                        ProcessActionableSet(relevantActionDetailLookup, firstParam, prop, propertyRelevanceInfo, codeTarget, 2, "Values");
                                        break;
                                    case VisitorImplementationTypeRelevance.AsActionableItem:
                                        IType ratType = propertyRelevanceInfo.Item1;
                                        if (ratType.IsGenericTypeParameter)
                                        {
                                            var gpRat = (IGenericParameter)(ratType);
                                            var interfaces = gpRat.Constraints.Concat(gpRat.Constraints.SelectMany(r => r.ImplementedInterfaces)).Distinct().ToArray();
                                            ratType = interfaces.FirstOrDefault(k => relevantActionDetailLookup.ContainsKey(k));
                                        }
                                        else if (!relevantActionDetailLookup.ContainsKey(ratType))
                                        {
                                            var interfaces = ratType.ImplementedInterfaces;
                                            ratType = interfaces.FirstOrDefault(k => relevantActionDetailLookup.ContainsKey(k));
                                        }
                                        var currentActionDetail = relevantActionDetailLookup[ratType];
                                        codeTarget.Call(
                                            this.VisitorResult
                                            .GetThis()
                                            .GetMethod(currentActionDetail.TargetAction)
                                            .Invoke(
                                                firstParam
                                                .GetReference()
                                                .GetProperty(prop.Item1.Name)));
                                        break;
                                    //case VisitorImplementationTypeRelevance.AsActionableItemSet:
                                    //    IType enumerableRatType = propertyRelevanceInfo.Item1;
                                    //    var genericParam = ((IGenericType)(enumerableRatType)).GenericParameters.FirstOrDefault();
                                    //    if (genericParam != null)
                                    //    {
                                    //        if (genericParam.IsGenericTypeParameter)
                                    //        {
                                    //            var gpRat = (IGenericParameter)(genericParam);
                                    //            var interfaces = gpRat.Constraints.Concat(gpRat.Constraints.SelectMany(r => r.ImplementedInterfaces)).Distinct().ToArray();
                                    //            genericParam = interfaces.FirstOrDefault(k => relevantActionDetailLookup.ContainsKey(k));
                                    //        }
                                    //        else if (!relevantActionDetailLookup.ContainsKey(genericParam))
                                    //        {
                                    //            var interfaces = genericParam.ImplementedInterfaces;
                                    //            genericParam = interfaces.FirstOrDefault(k => relevantActionDetailLookup.ContainsKey(k));
                                    //        }
                                    //        var currentActionSetDetail = relevantActionDetailLookup[genericParam];
                                    //        codeTarget.Call(this.VisitorResult.GetThis().GetMethod(currentActionSetDetail.TargetPluralAction).Invoke(firstParam.GetReference().GetProperty(prop.Item1.Name)));
                                    //    }
                                    //    break;

                                    case VisitorImplementationTypeRelevance.AsValueSet:
                                    case VisitorImplementationTypeRelevance.AsDeclValueSet:
                                        codeTarget.Call(this.VisitorResult.GetThis().GetMethod(this.Detail.VisitRefactorName ?? "Visit").Invoke(firstParam.GetReference().GetProperty(prop.Item1.Name).GetProperty("Values")));
                                        break;
                                    case VisitorImplementationTypeRelevance.AsItemSet:
                                        codeTarget.Call(this.VisitorResult.GetThis().GetMethod(this.Detail.VisitRefactorName ?? "Visit").Invoke(firstParam.GetReference().GetProperty(prop.Item1.Name)));
                                        break;
                                    default:
                                        break;
                                }
                            }
                            if (!firstParamType.Metadata.Contains(ignoreSetAttr))
                            {
                                var enAccess = IsTypeRelevantForEnumerableAccess(builderInterfaces, firstParamType, relevantActionTypes);
                                if (enAccess.Item2 == VisitorImplementationTypeRelevance.AsItemSet)
                                {
                                    secondNewMethod.Call(this.VisitorResult.GetThis().GetMethod(this.Detail.VisitRefactorName ?? "Visit").Invoke(firstParam.GetReference().Cast(enAccess.Item1)));
                                }
                            }
                        }
                        if (isVoidReturn)
                            newMethod.Call(invocation);
                        else
                            newMethod.Return(invocation);
                    }
                    else
                    {

                    }
                }
            }
        }

        private void ProcessActionableSet(Dictionary<IType, VisitorImplementationActionDetail> relevantActionDetailLookup, Slf.Ast.Members.IIntermediateMethodParameterMember<IClassMethodMember, Slf.Ast.Members.IIntermediateClassMethodMember, IClassType, IIntermediateClassType> firstParam, Tuple<IInterfacePropertyMember, bool> prop, Tuple<IType, VisitorImplementationTypeRelevance> propertyRelevanceInfo, IBlockStatementParent codeTarget, int gpOffset, string extraProp)
        {
            var vst = (IGenericType)propertyRelevanceInfo.Item1;
            var valueGParam = vst.GenericParameters[gpOffset];
            if (valueGParam != null)
            {
                if (valueGParam.IsGenericTypeParameter)
                {
                    var gpRat = (IGenericParameter)(valueGParam);
                    var interfaces = gpRat.Constraints.Concat(gpRat.Constraints.SelectMany(r => r.ImplementedInterfaces)).Distinct().ToArray();
                    valueGParam = interfaces.FirstOrDefault(k => relevantActionDetailLookup.ContainsKey(k));
                }
                else if (!relevantActionDetailLookup.ContainsKey(valueGParam))
                {
                    var interfaces = valueGParam.ImplementedInterfaces;
                    valueGParam = interfaces.FirstOrDefault(k => relevantActionDetailLookup.ContainsKey(k));
                }
                var currentActionSetDetail = relevantActionDetailLookup[valueGParam];
                var invokeParam = firstParam.GetReference().GetProperty(prop.Item1.Name);
                if (!string.IsNullOrEmpty(extraProp))
                    invokeParam = invokeParam.GetProperty(extraProp);
                codeTarget.Call(this.VisitorResult.GetThis().GetMethod(currentActionSetDetail.TargetPluralAction).Invoke(invokeParam));
            }
        }

        private static Tuple<IType, VisitorImplementationTypeRelevance> IsPropertyRelevant(IInterfacePropertyMember property, IInterfaceType[] relevantInterfaces, IType ignoreAttr, IType[] actionableTypes)
        {
            if (property.Metadata.Contains(ignoreAttr))
                return new Tuple<IType,VisitorImplementationTypeRelevance>(null, VisitorImplementationTypeRelevance.NotRelevant);
            return GetTypeRelevance(property, relevantInterfaces, actionableTypes);
        }

        private static Tuple<IType, VisitorImplementationTypeRelevance> GetTypeRelevance(IInterfacePropertyMember property, IInterfaceType[] relevantInterfaces, IType[] actionableTypes)
        {
            var propType = property.PropertyType;
            VisitorImplementationTypeRelevance result;
            if ((result = IsTypeRelevant(relevantInterfaces, propType, actionableTypes)) != VisitorImplementationTypeRelevance.NotRelevant)
                return Tuple.Create(propType, result);
            var enAccess = IsTypeRelevantForEnumerableAccess(relevantInterfaces, propType, actionableTypes);
            return enAccess;
        }

        private static Tuple<IType, VisitorImplementationTypeRelevance> IsTypeRelevantForEnumerableAccess(IInterfaceType[] relevantInterfaces, IType propType, IType[] actionableTypes)
        {
            var ptInterfaces = propType.ImplementedInterfaces;
            var enumerableRelevance = ptInterfaces.Where(k => k.IsGenericConstruct && k.ElementClassification == TypeElementClassification.GenericTypeDefinition && k.ElementType == IEnumerableOfT);
            var controlledRelevance = ptInterfaces.Where(k => k.IsGenericConstruct && k.ElementClassification == TypeElementClassification.GenericTypeDefinition && k.ElementType == IControlledDictionaryOfTKeyTValue);
            var interDeclRelevance = ptInterfaces.Where(k => k.IsGenericConstruct && k.ElementClassification == TypeElementClassification.GenericTypeDefinition && k.ElementType == IntermediateDeclDict);
            VisitorImplementationTypeRelevance result = VisitorImplementationTypeRelevance.NotRelevant;
            foreach (var relevantType in enumerableRelevance)
                if ((result = IsTypeRelevant(relevantInterfaces, ((IGenericType)(relevantType)).GenericParameters[0], actionableTypes)) != VisitorImplementationTypeRelevance.NotRelevant)
                    switch (result)
                    {
                        case VisitorImplementationTypeRelevance.AsItem:
                            return Tuple.Create(relevantType, VisitorImplementationTypeRelevance.AsItemSet);
                        case VisitorImplementationTypeRelevance.AsActionableItem:
                            return Tuple.Create(relevantType, VisitorImplementationTypeRelevance.AsActionableItemSet);
                    }
            foreach (var relevantType in interDeclRelevance)
                if ((result = IsTypeRelevant(relevantInterfaces, ((IGenericType)(relevantType)).GenericParameters[2], actionableTypes)) != VisitorImplementationTypeRelevance.NotRelevant)
                    switch (result)
                    {
                        case VisitorImplementationTypeRelevance.AsItem:
                            return Tuple.Create(relevantType, VisitorImplementationTypeRelevance.AsDeclValueSet);
                        case VisitorImplementationTypeRelevance.AsActionableItem:
                            return Tuple.Create(relevantType, VisitorImplementationTypeRelevance.AsActionableDeclValueSet);
                    }
            foreach (var relevantType in controlledRelevance)
                if ((result = IsTypeRelevant(relevantInterfaces, ((IGenericType)(relevantType)).GenericParameters[1], actionableTypes)) != VisitorImplementationTypeRelevance.NotRelevant)
                    switch (result)
                    {
                        case VisitorImplementationTypeRelevance.AsItem:
                            return Tuple.Create(relevantType, VisitorImplementationTypeRelevance.AsValueSet);
                        case VisitorImplementationTypeRelevance.AsActionableItem:
                            return Tuple.Create(relevantType, VisitorImplementationTypeRelevance.AsActionableValueSet);
                    }
            return new Tuple<IType, VisitorImplementationTypeRelevance>(null, VisitorImplementationTypeRelevance.NotRelevant);
        }

        private static VisitorImplementationTypeRelevance IsTypeRelevant(IInterfaceType[] relevantInterfaces, IType targetType, IType[] actionableTypes)
        {
            var ptInterfaces = targetType.ImplementedInterfaces;
            VisitorImplementationTypeRelevance result;
            if ((result = IsSubTypeRelevant(relevantInterfaces, targetType, actionableTypes)) != VisitorImplementationTypeRelevance.NotRelevant)
                return result;
            if (ptInterfaces.Any(k => (result = IsSubTypeRelevant(relevantInterfaces, k, actionableTypes)) != VisitorImplementationTypeRelevance.NotRelevant))
                return result;
            
            return VisitorImplementationTypeRelevance.NotRelevant;
        }

        private static VisitorImplementationTypeRelevance IsSubTypeRelevant(IInterfaceType[] relevantInterfaces, IType k, IType[] actionableTypes)
        {
            if (k.IsGenericConstruct && k.ElementClassification == TypeElementClassification.GenericTypeDefinition)
                if (relevantInterfaces.Contains(k.ElementType))
                    return VisitorImplementationTypeRelevance.AsItem;
                else if (actionableTypes.Contains(k.ElementType))
                    return VisitorImplementationTypeRelevance.AsActionableItem;
            if (relevantInterfaces.Contains(k))
                return VisitorImplementationTypeRelevance.AsItem;
            else if (actionableTypes.Contains(k))
                return VisitorImplementationTypeRelevance.AsActionableItem;
            else if (k.IsGenericTypeParameter)
            {
                var gp = (IGenericParameter)k;
                var fod = gp.Constraints.Select(x => (VisitorImplementationTypeRelevance?)IsTypeRelevant(relevantInterfaces, x, actionableTypes)).FirstOrDefault(p=>p != VisitorImplementationTypeRelevance.NotRelevant);
                if (fod != null)
                    return fod.Value;
            }
            return VisitorImplementationTypeRelevance.NotRelevant;
        }

        private static IInterfaceType CreateEnumerableVariant(IInterfaceType k)
        {
            return IEnumerableOfT.MakeGenericClosure(k);
        }

        private static IEnumerable<Tuple<IInterfacePropertyMember, bool>> EnumerateInterfaceProperties(IInterfaceType iFace, IType ignoreAttr, IDictionary<IInterfaceType, IInterfaceType[]> inheritanceDetail, IDictionary<IInterfaceType, List<IInterfacePropertyMember>> properties)
        {
            var props = (from @interface in inheritanceDetail[iFace]
                              from prop in properties[@interface]
                              select prop);
            return (from @prop in props
                    group prop by prop.Name into propSet
                    let prop = propSet.FirstOrDefault()
                    let propCount = propSet.Count()
                    where prop.Name != "Parent" && prop.Name != "Assembly"
                    where prop != null
                    select Tuple.Create(prop, propCount > 1)).ToArray();
            //return
            //    (from prop in EnumerateInterfacePropertiesDetail(iFace, ignoreAttr)
            //     group prop by prop.Name into grouping
            //     let prop = grouping.FirstOrDefault()
            //     let propCount = grouping.Count()
            //     where prop != null
            //     select Tuple.Create(prop, propCount > 1)).ToArray();
        }
         
        public IIntermediateClassType VisitorResult { get; set; }

        public IIntermediateClassType SecondaryResult { get; set; }
    }
}
