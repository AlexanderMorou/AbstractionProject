using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using AllenCopeland.Abstraction.Utilities;
using AllenCopeland.Abstraction.Utilities.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImplementedInterfaceLookup = AllenCopeland.Abstraction.Utilities.Collections.IControlledDictionary<AllenCopeland.Abstraction.Slf.Abstract.IType, System.Collections.Generic.IList<AllenCopeland.Abstraction.Slf.Abstract.IType>>;
using IIntermediateClassGenericParameter = AllenCopeland.Abstraction.Slf.Ast.Members.IIntermediateGenericTypeParameter<AllenCopeland.Abstraction.Slf.Abstract.IGeneralGenericTypeUniqueIdentifier, AllenCopeland.Abstraction.Slf.Abstract.IClassType, AllenCopeland.Abstraction.Slf.Ast.IIntermediateClassType>;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Languages.Expressions;
using AllenCopeland.Abstraction.Slf.Languages;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using System.Diagnostics;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
using AllenCopeland.Abstraction.Slf.Ast.Statements;
using AllenCopeland.Abstraction.Slf.Ast.Expressions.Linq;
namespace AllenCopeland.Abstraction.Slf.Transformation
{
#if DEBUG
    internal class VisitorImplementationBuilder :
        IVisitorImplementationBuilder
    {
        private VisitorImplementationContext _context;

        public VisitorImplementationBuilder(VisitorImplementationContext context)
        {
            this._context = context;
        }

        public void BuildScaffolding()
        {
            if (!this.Context.TargetAssembly.Namespaces.PathExists(this.Context.Detail.Namespace))
                this.Context.TargetAssembly.Namespaces.Add(this.Context.Detail.Namespace);

            var @namespace = this.Context.TargetAssembly.Namespaces[this.Context.Detail.Namespace];
            var resultClass = @namespace.Parts.Add().Classes.Add(this.Context.Detail.TargetContext);
            resultClass.AccessLevel = this.Context.Detail.AccessLevelModifiers;
            this.ResultVisitorClass = resultClass;
            this.ResultVisitorClassSecondaries = this.Context.DistinctMethodSignatureNames.Select(k => new { Name = k, Part = this.ResultVisitorClass.Parts.Add() }).ToDictionary(k => k.Name, v => v.Part);
        }

        public void Build()
        {
            IIntermediateClassGenericParameter resultType = null;
            IIntermediateClassGenericParameter contextType = null;
            foreach (var variation in this.Context.Variations)
            {
                foreach (var builder in variation.RelevantBuilders)
                {
                    IType resultTypeReplacement = resultType;
                    IType contextTypeReplacement = contextType;
                    if (variation.ResultType != null && variation.ContextType != null)
                    {
                        resultTypeReplacement = variation.ResultType;
                        contextTypeReplacement = variation.ContextType;
                    }
                    else if (variation.ResultType != null && variation.ContextType == null)
                    {
                        if (contextType == null && builder.Detail.ContextualVisitor)
                            contextType = variation.Handler.CreateContextTypeParameter(this);
                        contextTypeReplacement = contextType;
                        resultTypeReplacement = variation.ResultType;
                    }
                    else if (variation.ResultType == null && variation.ContextType != null)
                    {
                        if (resultType == null && builder.Detail.YieldingVisitor)
                            resultType = variation.Handler.CreateResultTypeParameter(this);
                        resultTypeReplacement = resultType;
                        contextTypeReplacement = variation.ContextType;
                    }
                    else
                    {
                        if (resultType == null && builder.Detail.YieldingVisitor)
                            resultType = variation.Handler.CreateResultTypeParameter(this);
                        if (contextType == null && builder.Detail.ContextualVisitor)
                            contextType = variation.Handler.CreateContextTypeParameter(this);
                        resultTypeReplacement = resultType;
                        contextTypeReplacement = contextType;
                    }
                    List<IType> genericReplacements = new List<IType>();
                    if (builder.Detail.YieldingVisitor)
                        genericReplacements.Add(resultTypeReplacement);
                    if (builder.Detail.ContextualVisitor)
                        genericReplacements.Add(contextTypeReplacement);
                    IInterfaceType targetInterface = builder.VisitorInterface;
                    if (genericReplacements.Count > 0)
                        targetInterface = targetInterface.MakeGenericClosure(genericReplacements.ToArray());
                    variation.ProperInterfaces.Add(builder, targetInterface);
                }
            }
            foreach (var targetInterface in this.Context.Variations.SelectMany(k => k.ProperInterfaces.Values).OrderBy(k => k.FullName))
                this.ResultVisitorClass.ImplementedInterfaces.ImplementInterfaceQuick(targetInterface);
            /* Preferrably these will be null. */
            this.Context.TResult = resultType;
            this.Context.TContext = contextType;
            /* TODO: Build visitor props prior to building the variations and add context associated to enable them to be accessible
             * within the scope of the BuildVariation method.  This will allow one to identify the specific variation
             * from which a given type is derived, so the proper visitor property can be identified. */
            foreach (var variation in this.Context.Variations)
                variation.Handler.ImplementVariationBuilderInterfaces(variation, this);
            foreach (var variation in this.Context.Variations)
                variation.Handler.BuildVariation(variation, this);
            int offset = 0;
            foreach (var prop in this.Context.VisitorProps)
            {
                var visitorNameFormat = string.Format("ThisVisitor{{0:{0}}}", new string('0', this.Context.VisitorProps.Count.ToString().Length));
                prop.Name = string.Format(visitorNameFormat, ++offset);
            }

            var assem1 = this.ResultVisitorClass.Assembly;
            assem1.FileName = string.Format("{0} (Visitor Signatures)", assem1.FileName, "");
            foreach (var distinctName in this.Context.DistinctMethodSignatureNames)
            {
                var assem2 = this.ResultVisitorClassSecondaries[distinctName].Assembly;
                assem2.FileName = string.Format("{0} ({1} Signatures)", assem2.FileName, distinctName);
            }
        }

        public VisitorImplementationContext Context { get { return this._context; } }

        public IIntermediateClassType ResultVisitorClass { get; private set; }

        public IDictionary<string, IIntermediateClassType> ResultVisitorClassSecondaries { get; private set; }
    }

    public class VisitorImplementationHandlerDefault :
        IVisitorImplementationHandler
    {
        public virtual VisitorImplementationAdjustedTypeRelevance GetTypeRelevance(IType type, VisitorImplementationVariationContext context, IVisitorImplementationBuilder builder)
        {
            var propType = type;
            VisitorImplementationTypeRelevance result;
            if ((result = IsTypeRelevant(propType, context, builder)) != VisitorImplementationTypeRelevance.NotRelevant)
                return new VisitorImplementationAdjustedTypeRelevance(propType, result);
            var enAccess = IsTypeRelevantForEnumerableAccess(propType, context, builder);
            return enAccess;
        }

        public virtual VisitorImplementationPropertyRelevance GetPropertyTypeRelevance(IPropertySignatureMember property, IType visitingType, VisitorImplementationVariationContext context, IVisitorImplementationBuilder builder)
        {
            if (property.Metadata.Contains(builder.Context.CommonTypeContext.IgnorePropertyAttribute))
                return new VisitorImplementationPropertyRelevance(new VisitorImplementationAdjustedTypeRelevance(VisitorImplementationTypeRelevance.NotRelevant, null), property);

            return new VisitorImplementationPropertyRelevance(GetTypeRelevance(property.PropertyType, context, builder), property);
        }

        protected virtual VisitorImplementationAdjustedTypeRelevance IsTypeRelevantForEnumerableAccess(IType currentlyRelevantType, VisitorImplementationVariationContext context, IVisitorImplementationBuilder builder)
        {
            var ptInterfaces = currentlyRelevantType.ImplementedInterfaces;
            var enumerableRelevance = ptInterfaces.Where(k => k.IsGenericConstruct && k.ElementClassification == TypeElementClassification.GenericTypeDefinition && k.ElementType == builder.Context.CommonTypeContext.IEnumerableOfT);
            var controlledRelevance = ptInterfaces.Where(k => k.IsGenericConstruct && k.ElementClassification == TypeElementClassification.GenericTypeDefinition && k.ElementType == builder.Context.CommonTypeContext.IControlledDictionaryOfTKeyTValue);
            var interDeclRelevance  = ptInterfaces.Where(k => k.IsGenericConstruct && k.ElementClassification == TypeElementClassification.GenericTypeDefinition && k.ElementType == builder.Context.CommonTypeContext.IIntermediateDeclarationDictionary);
            VisitorImplementationTypeRelevance result = VisitorImplementationTypeRelevance.NotRelevant;
            foreach (var relevantType in enumerableRelevance)
                if ((result = IsTypeRelevant(((IGenericType)(relevantType)).GenericParameters[0], context, builder)) != VisitorImplementationTypeRelevance.NotRelevant)
                    switch (result)
                    {
                        case VisitorImplementationTypeRelevance.AsItem:
                            return new VisitorImplementationAdjustedTypeRelevance(relevantType, VisitorImplementationTypeRelevance.AsItemSet);
                        case VisitorImplementationTypeRelevance.AsActionableItem:
                            return new VisitorImplementationAdjustedTypeRelevance(relevantType, VisitorImplementationTypeRelevance.AsActionableItemSet);
                    }
            foreach (var relevantType in interDeclRelevance)
                if ((result = IsTypeRelevant(((IGenericType)(relevantType)).GenericParameters[2], context, builder)) != VisitorImplementationTypeRelevance.NotRelevant)
                    switch (result)
                    {
                        case VisitorImplementationTypeRelevance.AsItem:
                            return new VisitorImplementationAdjustedTypeRelevance(relevantType, VisitorImplementationTypeRelevance.AsDeclValueSet);
                        case VisitorImplementationTypeRelevance.AsActionableItem:
                            return new VisitorImplementationAdjustedTypeRelevance(relevantType, VisitorImplementationTypeRelevance.AsActionableDeclValueSet);
                    }
            foreach (var relevantType in controlledRelevance)
                if ((result = IsTypeRelevant(((IGenericType)(relevantType)).GenericParameters[1], context, builder)) != VisitorImplementationTypeRelevance.NotRelevant)
                    switch (result)
                    {
                        case VisitorImplementationTypeRelevance.AsItem:
                            return new VisitorImplementationAdjustedTypeRelevance(relevantType, VisitorImplementationTypeRelevance.AsValueSet);
                        case VisitorImplementationTypeRelevance.AsActionableItem:
                            return new VisitorImplementationAdjustedTypeRelevance(relevantType, VisitorImplementationTypeRelevance.AsActionableValueSet);
                    }
            return new VisitorImplementationAdjustedTypeRelevance(null, VisitorImplementationTypeRelevance.NotRelevant);
        }

        protected virtual VisitorImplementationTypeRelevance IsTypeRelevant(IType targetType, VisitorImplementationVariationContext context, IVisitorImplementationBuilder builder)
        {
            var ptInterfaces = targetType.ImplementedInterfaces;
            VisitorImplementationTypeRelevance result;
            if ((result = IsSubTypeRelevant(targetType, context, builder)) != VisitorImplementationTypeRelevance.NotRelevant)
                return result;
            if (ptInterfaces.Any(k => (result = IsSubTypeRelevant(k, context, builder)) != VisitorImplementationTypeRelevance.NotRelevant))
                return result;

            return VisitorImplementationTypeRelevance.NotRelevant;
        }

        protected virtual VisitorImplementationTypeRelevance IsSubTypeRelevant(IType relevantType, VisitorImplementationVariationContext context, IVisitorImplementationBuilder builder)
        {
            if (relevantType.IsGenericConstruct && relevantType.ElementClassification == TypeElementClassification.GenericTypeDefinition)
                if (builder.Context.VisitorTypesByMethodName[context.VisitMethodName].Contains(relevantType.ElementType) ||
                    builder.Context.AcceptorTypesByMethodName[context.VisitMethodName].Contains(relevantType.ElementType))
                    return VisitorImplementationTypeRelevance.AsItem;
                else if (builder.Context.ActionableTypesByMethodName[context.VisitMethodName].Contains(relevantType.ElementType))
                    return VisitorImplementationTypeRelevance.AsActionableItem;
            if (builder.Context.VisitorTypesByMethodName[context.VisitMethodName].Contains(relevantType) ||
                builder.Context.AcceptorTypesByMethodName[context.VisitMethodName].Contains(relevantType))
                return VisitorImplementationTypeRelevance.AsItem;
            else if (builder.Context.ActionableTypesByMethodName[context.VisitMethodName].Contains(relevantType))
                return VisitorImplementationTypeRelevance.AsActionableItem;
            else if (relevantType.IsGenericTypeParameter)
            {
                var gp = (IGenericParameter)relevantType;
                var fod = gp.Constraints.Select(x => (VisitorImplementationTypeRelevance?)IsTypeRelevant(x, context, builder)).FirstOrDefault(p => p != VisitorImplementationTypeRelevance.NotRelevant);
                if (fod != null)
                    return fod.Value;
            }
            return VisitorImplementationTypeRelevance.NotRelevant;
        }

        public virtual void ImplementVariationBuilderInterfaces(VisitorImplementationVariationContext variation, IVisitorImplementationBuilder implementationBuilder)
        {
            var interfaceVariants = variation.ImplementationDetail;
            if (implementationBuilder.Context.VisitorProps == null)
                implementationBuilder.Context.VisitorProps = new List<IIntermediateClassPropertyMember>();
            foreach (var builder in variation.RelevantBuilders)
            {
                var builderInterface = variation.ProperInterfaces[builder];
                foreach (var properInterface in new[] { builderInterface }.Concat(builderInterface.ImplementedInterfaces.OfType<IInterfaceType>()).Distinct())
                {
                    var currentInterfaceDetail = this.ImplementVariationBuilderInterface(variation, builder, properInterface, implementationBuilder);
                    interfaceVariants.Add(builder, properInterface, currentInterfaceDetail);
                    implementationBuilder.Context.FullImplementationDetail.Add(builder, properInterface, currentInterfaceDetail);
                }
            }
            if (!variation.VisitMethodIsAbstract)
            {
                foreach (var ksvp in interfaceVariants)
                {
                    string visitorPropName = "_ThisVisitor";
                    string currentPropName = visitorPropName;
                    int offset = 0;
                    while (implementationBuilder.ResultVisitorClass.Properties.ContainsKey(TypeSystemIdentifiers.GetMemberIdentifier(currentPropName)))
                        currentPropName = string.Format("{0}{1}", visitorPropName, ++offset);
                    var currentVisitorProp = implementationBuilder.ResultVisitorClass.Properties.Add(ksvp.Keys.Key2.WithName(currentPropName), true, false);
                    implementationBuilder.Context.VisitorProps.Add(currentVisitorProp);
                    currentVisitorProp.AccessLevel = AccessLevelModifiers.Private;
                    currentVisitorProp.GetMethod.Return(new TypeCastExpression(currentVisitorProp.PropertyType, implementationBuilder.ResultVisitorClass.GetThis()));
                }
            }
        }

        public void BuildVariation(VisitorImplementationVariationContext variation, IVisitorImplementationBuilder implementationBuilder)
        {
            var interfaceVariants = variation.ImplementationDetail;

            if (!variation.VisitMethodIsAbstract)
            {
                var interfacePropertyDetail = new MultikeyedDictionary<VisitorBuilder, IInterfaceType, IType, IType, IControlledCollection<VisitorImplementationPropertyRelevance>>();
                var propertyDetails = new Dictionary<IType, ControlledCollection<VisitorImplementationPropertyRelevance>>();
                var propertyDetails2 = new MultikeyedDictionary<IType, IType, ControlledCollection<VisitorImplementationPropertyRelevance>>();
                foreach (var ksvp in interfaceVariants)
                {
                    foreach (var kvp in ksvp.Value)
                    {
                        var currentType = kvp.Value.Parameters.Values[0].ParameterType;
                        var currentTypeExpansion = new[] { currentType }.Concat(currentType.ImplementedInterfaces).ToArray();
                        foreach (var type in currentTypeExpansion)
                        {
                            ControlledCollection<VisitorImplementationPropertyRelevance> currentDetail;
                            if (!propertyDetails.TryGetValue(type, out currentDetail))
                                if (type is IPropertyParent)
                                {
                                    var currentProperties = ((IPropertyParent)type).Properties.Values.OfType<IPropertyMember>().Select(k => new { Property = k, Relevance = this.GetPropertyTypeRelevance(k, currentType, variation, implementationBuilder) }).ToArray();
                                    if (currentProperties.Length > 0)
                                    {
                                        propertyDetails.Add(type, currentDetail = new ControlledCollection<VisitorImplementationPropertyRelevance>());
                                        foreach (var propertyDetail in currentProperties)
                                            currentDetail.baseList.Add(propertyDetail.Relevance);
                                    }
                                }
                                else if (type is IPropertySignatureParent)
                                {
                                    var currentProperties = ((IPropertySignatureParent)type).Properties.Values.OfType<IPropertySignatureMember>().Select(k => new { Property = k, Relevance = this.GetPropertyTypeRelevance(k, currentType, variation, implementationBuilder) }).ToArray();
                                    if (currentProperties.Length > 0)
                                    {
                                        propertyDetails.Add(type, currentDetail = new ControlledCollection<VisitorImplementationPropertyRelevance>());
                                        foreach (var propertyDetail in currentProperties)
                                            currentDetail.baseList.Add(propertyDetail.Relevance);
                                    }
                                }
                            if (currentDetail != null)
                                propertyDetails2.Add(currentType, type, currentDetail);
                        }
                    }
                    foreach (var propKsvp in propertyDetails2)
                        interfacePropertyDetail.Add(ksvp.Keys.Key1, ksvp.Keys.Key2, propKsvp.Keys.Key1, propKsvp.Keys.Key2, propertyDetails[propKsvp.Keys.Key2]);
                }
                foreach (var ksvp in interfaceVariants)
                {
                    foreach (var kvp in ksvp.Value)
                    {
                        var acceptorType = kvp.Value.Parameters.Values[0].ParameterType;
                        var propertiesContext = VisitorImplementationVariationPropertiesContext.FromPropertyContext(
                                                    new VisitorImplementationVariationPropertyContext
                                                        {
                                                            VisitingType    = acceptorType,
                                                            VisitorType     = ksvp.Keys.Key2,
                                                            VisitorBuilder  = ksvp.Keys.Key1,
                                                            PropertyDetails = GetPropertyOriginContexts(
                                                                                interfacePropertyDetail, 
                                                                                ksvp,
                                                                                kvp)
                                                        });
                        BreakdownGroupsByExplicitCallouts(implementationBuilder, acceptorType, propertiesContext);
                        foreach (var context in propertiesContext.Values)
                            context.Relevance = this.GetPropertyGroupRelevance(context, variation, implementationBuilder);

                        this.ImplementMethodBody(variation, propertiesContext, ksvp.Keys.Key2, kvp.Key, kvp.Value, implementationBuilder);
                    }
                }
            }
        }

        private static void BreakdownGroupsByExplicitCallouts(IVisitorImplementationBuilder implementationBuilder, IType acceptorType, VisitorImplementationVariationPropertiesContext propertiesContext)
        {
            var groupMetadatum = (from type in new IType[] { acceptorType }.Concat(acceptorType.ImplementedInterfaces)
                                  let metadatum = type.GetMetadatum(implementationBuilder.Context.CommonTypeContext.GroupPropertiesAttribute, true)
                                  where metadatum != null
                                  select metadatum).FirstOrDefault();
            if (groupMetadatum != null)
            {
                var propertiesToGatherDetail =
                    groupMetadatum.NamedParameters.GetNamedParameter<string[]>("PropertyNames");
                var handleAsGroupDetail      =
                    groupMetadatum.NamedParameters.GetNamedParameter<bool>("HandleItemsAsGroup");
                var groupIdDetail            =
                    groupMetadatum.NamedParameters.GetNamedParameter<string>("GroupId");
                var groupId                  = groupIdDetail.Value;
                var propertiesToGather       = propertiesToGatherDetail.Value ?? new string[0];
                var handleAsGroup            = handleAsGroupDetail.Value;

                if (propertiesToGather.Length > 0)
                {
                    var relevantPropertyQuery =
                        (from propertyName in propertiesToGather
                         from grKvp in propertiesContext
                         let gr = grKvp.Value
                         from prop in gr.RelevantProperties.Concat(gr.NotRelevantProperties)
                         where prop.PropertyMember.Name == propertyName
                         group prop by new { GroupKey = grKvp.Key, Group = gr })
                         .ToDictionary(k => k.Key, v => v.ToArray());
                    List<HashList<IType>> groupsToPrune = new List<HashList<IType>>();
                    foreach (var key in relevantPropertyQuery.Keys)
                    {
                        var propSet = relevantPropertyQuery[key];
                        key.Group.NotRelevantProperties = key.Group.NotRelevantProperties.Except(propSet).ToArray();
                        key.Group.RelevantProperties = key.Group.RelevantProperties.Except(propSet).ToArray();
                        if (key.Group.NotRelevantProperties.Length == 0 && key.Group.RelevantProperties.Length == 0)
                            groupsToPrune.Add(key.GroupKey);
                    }
                    HashList<IType> keysForNewGroup = new HashList<IType>(relevantPropertyQuery.Keys.SelectMany(k => k.GroupKey).Distinct());
                    var notRelProps = relevantPropertyQuery.Values.SelectMany(k => k).Where(k => k.Relevance == VisitorImplementationTypeRelevance.NotRelevant).ToArray();
                    var relProps = relevantPropertyQuery.Values.SelectMany(k => k).Where(k => k.Relevance != VisitorImplementationTypeRelevance.NotRelevant).ToArray();
                    var newGroup = new VisitorImplementationVariationPropertyGroupContext
                    {
                        NotRelevantProperties   = notRelProps,
                        RelevantProperties      = relProps,
                        OriginTypes             = keysForNewGroup.ToArray(),
                        GroupId                 = groupId,
                        Relevance               = handleAsGroup 
                                                  ? VisitorImplementationPropertyGroupRelevance.HandleAsGroup
                                                  : VisitorImplementationPropertyGroupRelevance.NotSet
                    };
                    foreach (var keyToRemove in groupsToPrune)
                        propertiesContext.Remove(keyToRemove);
                    propertiesContext.Add(keysForNewGroup, newGroup);
                }
            }
        }

        public virtual VisitorImplementationPropertyGroupRelevance GetPropertyGroupRelevance(VisitorImplementationVariationPropertyGroupContext groupContext, VisitorImplementationVariationContext variation, IVisitorImplementationBuilder implementationBuilder)
        {
            if (groupContext.RelevantProperties.Count() > 0)
                if (groupContext.OriginTypes.First().Metadata.Contains(implementationBuilder.Context.CommonTypeContext.ProcessAsGroupAttribute))
                    return VisitorImplementationPropertyGroupRelevance.HandleAsGroup;
                else if (groupContext.OriginTypes.Length > 0 && groupContext.Relevance != VisitorImplementationPropertyGroupRelevance.NotSet)
                    return groupContext.Relevance;
                else
                    return VisitorImplementationPropertyGroupRelevance.HandleItemsSeparately;
            else
                return VisitorImplementationPropertyGroupRelevance.IgnoreGroup;
        }

        protected virtual VisitorImplementationVariationPropertyOriginContext[] GetPropertyOriginContexts(MultikeyedDictionary<VisitorBuilder, IInterfaceType, IType, IType, IControlledCollection<VisitorImplementationPropertyRelevance>> interfacePropertyDetail, KeysValuePair<MultikeyedDictionaryKeys<VisitorBuilder, IInterfaceType>, IControlledDictionary<IInterfaceMethodMember, IIntermediateClassMethodMember>> ksvp, KeyValuePair<IInterfaceMethodMember, IIntermediateClassMethodMember> kvp)
        {
            return (from ipd in interfacePropertyDetail
                    where ipd.Keys.Key1 == ksvp.Keys.Key1 &&
                          ipd.Keys.Key2 == ksvp.Keys.Key2 &&
                          ipd.Keys.Key3 == kvp.Value.Parameters.Values[0].ParameterType
                    from relevance in ipd.Value
                    group new { relevance, ipd } by relevance.PropertyMember.Name into relevantGroup
                    let relevantGroupDetail = relevantGroup.FirstOrDefault()
                    let ipd = relevantGroupDetail.ipd
                    let relevance = relevantGroupDetail.relevance
                    select new VisitorImplementationVariationPropertyOriginContext
                    {
                        VisitorType             = ipd.Keys.Key2,
                        PropertyOriginType      = ipd.Keys.Key4,
                        VisitingType            = ipd.Keys.Key3,
                        AdjustedPropertyType    = relevance.AdjustedPropertyType,
                        PropertyMember          = relevance.PropertyMember,
                        IsSingletonProperty     = relevantGroup.Count() == 1,
                        Relevance               = relevance.Relevance
                    }).ToArray();
        }

        public virtual IControlledDictionary<IInterfaceMethodMember, IIntermediateClassMethodMember> ImplementVariationBuilderInterface(VisitorImplementationVariationContext variation, VisitorBuilder visitorBuilder, IInterfaceType relevantBuilderInterface, IVisitorImplementationBuilder implementationBuilder)
        {
            var newMethodName = variation.VisitMethodName;
            var voidType = implementationBuilder.Context.TargetAssembly.IdentityManager.ObtainTypeReference(RuntimeCoreType.VoidType);
            var relativeImplementationLookup = new Dictionary<IInterfaceMethodMember,IIntermediateClassMethodMember>();
            foreach (var methodSignature in relevantBuilderInterface.Methods.Keys.OrderBy(k => k.ParameterCount).ThenBy(k => k.ParameterCount >= 1 ? k.Parameters.First().Name : string.Empty).ThenBy(k => k.ParameterCount >= 2 ? k.Parameters.Skip(1).First().Name : string.Empty))
            {
                var currentInterfaceMethod = relevantBuilderInterface.Methods[methodSignature];
                var interfaceImplementation = this.CreateVariationVisitorMethodImplementation(methodSignature, currentInterfaceMethod, variation, visitorBuilder, relevantBuilderInterface, implementationBuilder);
                var newMethod = this.CreateVariationBuilderMethod(newMethodName, methodSignature, currentInterfaceMethod, variation, visitorBuilder, relevantBuilderInterface, implementationBuilder);
                if (interfaceImplementation != null)
                {
                    var callExpression = newMethod.GetReference().Invoke(interfaceImplementation.Parameters.Values.Select(p => p.GetReference()).ToArray());
                    if (interfaceImplementation.ReturnType != voidType)
                        interfaceImplementation.Return(callExpression);
                    else
                        interfaceImplementation.Call(callExpression);
                }
                relativeImplementationLookup.Add(currentInterfaceMethod, newMethod);
            }

            return new ControlledDictionary<IInterfaceMethodMember, IIntermediateClassMethodMember>(relativeImplementationLookup);
        }

        public virtual IIntermediateClassMethodMember CreateVariationVisitorMethodImplementation(IGeneralGenericSignatureMemberUniqueIdentifier methodSignature, IInterfaceMethodMember interfaceMethodMember, VisitorImplementationVariationContext variation, VisitorBuilder visitorBuilder, IInterfaceType relevantBuilderInterface, IVisitorImplementationBuilder implementationBuilder)
        {
            var gpCount = interfaceMethodMember.IsGenericConstruct ? interfaceMethodMember.TypeParameters.Count : 0;
            var genericParameterData = new GenericParameterData[gpCount];
            for (int i = 0; i < gpCount; i++)
                genericParameterData[i] = new GenericParameterData(interfaceMethodMember.TypeParameters.Values[i].Name);
            TypedNameSeries parameterData = new TypedNameSeries();
            foreach (var param in interfaceMethodMember.Parameters.Values)
            {
                var pType = param.ParameterType.TurnTypeParametersIntoSymbols();
                parameterData.Add(param.Name, pType, param.Direction);
            }
            var newMethod = implementationBuilder.ResultVisitorClass.Methods.Add(interfaceMethodMember.ReturnType.TurnTypeParametersIntoSymbols().WithName("Visit"), parameterData, genericParameterData);
            newMethod.Implementations.Add(interfaceMethodMember.Parent);
            return newMethod;
        }

        public virtual IIntermediateClassMethodMember CreateVariationBuilderMethod(string methodName, IGeneralGenericSignatureMemberUniqueIdentifier methodSignature, IInterfaceMethodMember interfaceMethodMember, VisitorImplementationVariationContext variation, VisitorBuilder visitorBuilder, IInterfaceType relevantBuilderInterface, IVisitorImplementationBuilder implementationBuilder)
        {
            var gpCount = interfaceMethodMember.IsGenericConstruct ? interfaceMethodMember.TypeParameters.Count : 0;
            var genericParameterData = new GenericParameterData[gpCount];
            for (int i = 0; i < gpCount; i++)
            {
                var gp = interfaceMethodMember.TypeParameters.Values[i];
                var newGPData = genericParameterData[i] = new GenericParameterData(gp.Name);
                foreach (var constraint in gp.Constraints)
                    newGPData.Constraints.Add(constraint.TurnTypeParametersIntoSymbols());
                newGPData.SpecialConstraint = gp.SpecialConstraint;
            }
            TypedNameSeries parameterData = new TypedNameSeries();
            foreach (var param in interfaceMethodMember.Parameters.Values)
            {
                var pType = param.ParameterType.TurnTypeParametersIntoSymbols();
                parameterData.Add(param.Name, pType, param.Direction);
            }
            var result = implementationBuilder.ResultVisitorClassSecondaries[methodName].Methods.Add(interfaceMethodMember.ReturnType.TurnTypeParametersIntoSymbols().WithName(methodName), parameterData, genericParameterData);
            if (variation.VisitMethodIsAbstract)
                result.IsAbstract = true;
            result.AccessLevel = AccessLevelModifiers.Public;
            return result;
        }


        public virtual IIntermediateClassGenericParameter CreateContextTypeParameter(IVisitorImplementationBuilder visitorImplementationBuilder)
        {
            return visitorImplementationBuilder.ResultVisitorClass.TypeParameters.Add("TContext");
        }

        public virtual IIntermediateClassGenericParameter CreateResultTypeParameter(IVisitorImplementationBuilder visitorImplementationBuilder)
        {
            return visitorImplementationBuilder.ResultVisitorClass.TypeParameters.Add("TResult");
        }

        public virtual void ImplementMethodBody(VisitorImplementationVariationContext context, VisitorImplementationVariationPropertiesContext visitorPropertiesContext, IInterfaceType visitorInterface, IInterfaceMethodMember interfaceMethod, IIntermediateClassMethodMember concreteVariant, IVisitorImplementationBuilder implementationBuilder)
        {
            var expressionHelper = implementationBuilder.Context.ExpressionService;
            foreach (var groupOriginType in visitorPropertiesContext.Keys)
            {
                var groupContext = visitorPropertiesContext[groupOriginType];
                this.ImplementMethodBodyPropertyGroup(
                    groupContext,
                    context,
                    visitorPropertiesContext,
                    visitorInterface,
                    interfaceMethod,
                    concreteVariant,
                    implementationBuilder);
            }
        }

        public virtual void ImplementMethodBodyPropertyGroup(
            VisitorImplementationVariationPropertyGroupContext groupContext,
            VisitorImplementationVariationContext context,
            VisitorImplementationVariationPropertiesContext visitorPropertiesContext,
            IInterfaceType visitorInterface,
            IInterfaceMethodMember interfaceMethod,
            IIntermediateClassMethodMember concreteVariant,
            IVisitorImplementationBuilder implementationBuilder)
        {
            var groupOriginTypes = groupContext.OriginTypes;
            var groupNames = groupContext.GroupId ?? string.Join(", ", groupOriginTypes.Select(k => k.Name));
            if (groupContext.RelevantProperties.Count() > 0)
            {
                var names = string.Join(", ", groupContext.RelevantProperties.Select(k => k.PropertyMember.Name));
                switch (groupContext.Relevance)
                {
                    case VisitorImplementationPropertyGroupRelevance.HandleAsGroup:
                        concreteVariant.Comment(string.Format("Handle as Group: {1} - {{{0}}}", names, groupNames));
                        break;
                    case VisitorImplementationPropertyGroupRelevance.HandleItemsSeparately:
                        foreach (var prop in groupContext.RelevantProperties)
                            this.ImplementMethodBodyProperty(groupContext, prop, visitorPropertiesContext, context, visitorInterface, interfaceMethod, concreteVariant, implementationBuilder);
                        concreteVariant.Comment(string.Format("Handle Properties Individually: {1} - {{{0}}}", names, groupNames));
                        break;
                }
            }
        }

        public virtual void ImplementMethodBodyProperty(
            VisitorImplementationVariationPropertyGroupContext groupContext,
            VisitorImplementationPropertyRelevance propertyContext,
            VisitorImplementationVariationPropertiesContext visitorPropertiesContext,
            VisitorImplementationVariationContext context,
            IInterfaceType visitorInterface,
            IInterfaceMethodMember interfaceMethod,
            IIntermediateClassMethodMember concreteVariant,
            IVisitorImplementationBuilder implementationBuilder)
        {
            var acceptorReference = concreteVariant.Parameters.Values[0].GetReference();
            var requirementAttrDetail = propertyContext.PropertyMember.Metadata[implementationBuilder.Context.CommonTypeContext.PropertyRequirementAttribute];
            string propertyRequirement = null;
            if (requirementAttrDetail != null)
                propertyRequirement = requirementAttrDetail.Parameters.GetIndexedParameter<string>(0).Value;
            IBlockStatementParent target = concreteVariant;
            if (propertyRequirement != null)
                target = target.If(acceptorReference.GetProperty(propertyRequirement));
            else if (propertyContext.PropertyMember.PropertyType.Type == TypeKind.Interface ||
                     propertyContext.PropertyMember.PropertyType.Type == TypeKind.Class ||
                     propertyContext.PropertyMember.PropertyType.Type == TypeKind.Delegate)
                target = target.If(acceptorReference.GetProperty(propertyContext.PropertyMember.Name).InequalTo(IntermediateGateway.NullValue, implementationBuilder.Context.ExpressionService));

            switch (propertyContext.Relevance)
            {
                case VisitorImplementationTypeRelevance.AsItem:
                    var ct = context.ContextType ?? implementationBuilder.Context.TContext;
                    var rt = context.ResultType ?? implementationBuilder.Context.TResult;
                    IList<IType> genericParameters = new List<IType>();
                    if (rt != null)
                        genericParameters.Add(rt);
                    if (ct != null)
                        genericParameters.Add(ct);
                    var adjType = propertyContext.AdjustedPropertyType;
                    if (adjType.IsGenericConstruct && !((IGenericType)(adjType)).IsGenericDefinition)
                        adjType = adjType.ElementType;
                    var relevantTypes   =
                        adjType.IsGenericTypeParameter
                        ? ((IGenericParameter)(adjType)).Constraints.Concat(((IGenericParameter)(adjType)).Constraints.SelectMany(c => c.ImplementedInterfaces)).Distinct().ToArray()
                        : new IType[] { adjType }.Concat(adjType.ImplementedInterfaces).ToArray();
                    var firstBuilderContext =
                        (from variation in implementationBuilder.Context.Variations
                         where variation.AcceptorTypes.Any(p=>relevantTypes.Contains(p))
                         from properInterface in variation.ProperInterfaces
                         join visitorProp in implementationBuilder.Context.VisitorProps on properInterface.Value equals visitorProp.PropertyType
                         select new { Variation = variation, VisitorBuilder = properInterface.Key, Interface = properInterface.Value, VisitorProp = visitorProp }).FirstOrDefault();
                    var paramRefs = concreteVariant.Parameters.Values.Skip(1).Select(k => k.GetReference()).ToArray();

                    if (firstBuilderContext != null)
                        if (rt != null)
                            target.Call(acceptorReference.GetProperty(propertyContext.PropertyMember.Name).GetMethod("Accept", genericParameters.ToArray()).Invoke(new IExpression[] { firstBuilderContext.VisitorProp.GetReference() }.Concat(paramRefs).ToArray()));
                        else
                            target
                                .Call(acceptorReference.GetProperty(propertyContext.PropertyMember.Name)
                                .GetMethod("Accept")
                                .Invoke(
                                    new IExpression[] 
                                    { 
                                        firstBuilderContext.VisitorProp.GetReference()
                                    }
                                    .Concat(paramRefs)
                                    .ToArray()));
                    break;
                case VisitorImplementationTypeRelevance.AsItemSet:
                    break;
                case VisitorImplementationTypeRelevance.AsValueSet:
                    break;
                case VisitorImplementationTypeRelevance.AsDeclValueSet:
                    break;
                case VisitorImplementationTypeRelevance.AsActionableItem:
                    break;
                case VisitorImplementationTypeRelevance.AsActionableItemSet:
                    break;
                case VisitorImplementationTypeRelevance.AsActionableDeclValueSet:
                case VisitorImplementationTypeRelevance.AsActionableValueSet:
                    target.Call(implementationBuilder.ResultVisitorClass.GetThis().GetMethod(context.VisitMethodName).Invoke(acceptorReference.GetProperty(propertyContext.PropertyMember.Name).GetProperty("Values")));
                    break;
            }
        }
    }

    public class VisitorImplementationVariationPropertyContext
    {
        public VisitorBuilder VisitorBuilder { get; internal set; }
        public IEnumerable<VisitorImplementationVariationPropertyOriginContext> PropertyDetails { get; internal set; }
        public IInterfaceType VisitorType { get; set; }
        public IType VisitingType { get; set; }
    }

    [DebuggerTypeProxy(typeof(VisitorImplementationVariationPropertyOriginContextProxy))]
    public class VisitorImplementationVariationPropertyOriginContext :
        VisitorImplementationPropertyRelevance
    {
        public IInterfaceType VisitorType { get; set; }
        public IType VisitingType { get; set; }
        public IType PropertyOriginType { get; set; }
    }

    public class VisitorImplementationVariationPropertyGroupContext
    {
        public IType[] OriginTypes { get; internal set; }
        public VisitorImplementationPropertyGroupRelevance Relevance { get; internal set; }
        public VisitorImplementationPropertyRelevance[] RelevantProperties { get; internal set; }
        public VisitorImplementationPropertyRelevance[] NotRelevantProperties { get; internal set; }
        public string GroupId { get; internal set; }
    }
    public class VisitorImplementationVariationPropertiesContext :
        Dictionary<HashList<IType>, VisitorImplementationVariationPropertyGroupContext>
    {
        public VisitorBuilder VisitorBuilder { get; internal set; }
        public IInterfaceType VisitorType { get; set; }
        public IType VisitingType { get; set; }
        public static VisitorImplementationVariationPropertiesContext FromPropertyContext(VisitorImplementationVariationPropertyContext incomingContext)
        {
            var result = new VisitorImplementationVariationPropertiesContext();
            result.VisitorBuilder   = incomingContext.VisitorBuilder;
            result.VisitingType     = incomingContext.VisitingType  ;
            result.VisitorType      = incomingContext.VisitorType   ;

            var grouping =
                (from d in incomingContext.PropertyDetails
                 group d by d.PropertyOriginType)
                 .ToDictionary(
                    k => k.Key,
                    v => v.ToArray());
            
            foreach (var key in grouping.Keys)
            {
                var currentGroupingElements = grouping[key];
                result.Add(new HashList<IType>(new IType[] { key }), new VisitorImplementationVariationPropertyGroupContext
                              {
                                  NotRelevantProperties = currentGroupingElements
                                                          .Where(k => k.Relevance == VisitorImplementationTypeRelevance.NotRelevant)
                                                          .ToArray(),
                                  RelevantProperties    = currentGroupingElements
                                                          .Where(k => k.Relevance != VisitorImplementationTypeRelevance.NotRelevant)
                                                          .ToArray(),
                                  OriginTypes           = new IType[] { key },
                                  Relevance             = VisitorImplementationPropertyGroupRelevance.NotSet,
                              });
            }
            return result;
        }
    }
    public class VisitorImplementationVariationPropertyOriginContextProxy
    {
        private VisitorImplementationVariationPropertyOriginContext origin;

        public VisitorImplementationVariationPropertyOriginContextProxy(VisitorImplementationVariationPropertyOriginContext origin)
        {
            this.origin = origin;
        }

        public IInterfaceType                       VisitorType             { get { return origin.VisitorType; } }
        public IType                                VisitingType            { get { return origin.VisitingType; } }
        public IType                                PropertyOriginType      { get { return origin.PropertyOriginType; } }
        public VisitorImplementationTypeRelevance   Relevance               { get { return origin.Relevance; } }
        public IType                                AdjustedPropertyType    { get { return origin.AdjustedPropertyType; } }
        public IPropertySignatureMember             PropertyMember          { get { return origin.PropertyMember; } }
        public bool                                 IsSingletonProperty     { get { return origin.IsSingletonProperty; } }
    }

    public interface IVisitorImplementationBuilder
    {
        VisitorImplementationContext Context { get; }
        IIntermediateClassType ResultVisitorClass { get; }
        IDictionary<string, IIntermediateClassType> ResultVisitorClassSecondaries { get; }
        void BuildScaffolding();
        void Build();
    }

    public interface IVisitorImplementationHandler
    {
        /// <summary>Obtains the <see cref="VisitorImplementationTypeRelevance"/> of the <paramref name="type"/> provided.</summary>
        /// <param name="type">The <see cref="IType"/> which is relevant to the request.</param>
        /// <returns>A <see cref="VisitorImplementationTypeRelevance"/> which denotes the relevance of the <paramref name="type"/> provided.</returns>
        VisitorImplementationAdjustedTypeRelevance GetTypeRelevance(IType type, VisitorImplementationVariationContext context, IVisitorImplementationBuilder builder);
        VisitorImplementationPropertyRelevance GetPropertyTypeRelevance(IPropertySignatureMember property, IType visitingType, VisitorImplementationVariationContext context, IVisitorImplementationBuilder builder);
        void BuildVariation(VisitorImplementationVariationContext variation, IVisitorImplementationBuilder implementationBuilder);
        IControlledDictionary<IInterfaceMethodMember,IIntermediateClassMethodMember> ImplementVariationBuilderInterface(VisitorImplementationVariationContext variation, VisitorBuilder builder, IInterfaceType relevantBuilderInterface, IVisitorImplementationBuilder implementationBuilder);
        void ImplementVariationBuilderInterfaces(VisitorImplementationVariationContext variation, IVisitorImplementationBuilder implementationBuilder);
        IIntermediateClassMethodMember CreateVariationVisitorMethodImplementation(IGeneralGenericSignatureMemberUniqueIdentifier methodSignature, IInterfaceMethodMember interfaceMethodMember, VisitorImplementationVariationContext variation, VisitorBuilder builder, IInterfaceType relevantBuilderInterface, IVisitorImplementationBuilder implementationBuilder);
        IIntermediateClassMethodMember CreateVariationBuilderMethod(string methodName, IGeneralGenericSignatureMemberUniqueIdentifier methodSignature, IInterfaceMethodMember interfaceMethodMember, VisitorImplementationVariationContext variation, VisitorBuilder builder, IInterfaceType relevantBuilderInterface, IVisitorImplementationBuilder implementationBuilder);
        IIntermediateClassGenericParameter CreateContextTypeParameter(IVisitorImplementationBuilder visitorImplementationBuilder);
        IIntermediateClassGenericParameter CreateResultTypeParameter(IVisitorImplementationBuilder visitorImplementationBuilder);

        void ImplementMethodBody(VisitorImplementationVariationContext context, VisitorImplementationVariationPropertiesContext visitorBuilder, IInterfaceType visitorInterface, IInterfaceMethodMember interfaceMethod, IIntermediateClassMethodMember concreteVariant, IVisitorImplementationBuilder implementationBuilder);

        VisitorImplementationPropertyGroupRelevance GetPropertyGroupRelevance(VisitorImplementationVariationPropertyGroupContext groupContext, VisitorImplementationVariationContext variation, IVisitorImplementationBuilder implementationBuilder);

        void ImplementMethodBodyPropertyGroup(VisitorImplementationVariationPropertyGroupContext groupContext, VisitorImplementationVariationContext context, VisitorImplementationVariationPropertiesContext visitorPropertiesContext, IInterfaceType visitorInterface, IInterfaceMethodMember interfaceMethod, IIntermediateClassMethodMember concreteVariant, IVisitorImplementationBuilder implementationBuilder);

        void ImplementMethodBodyProperty(VisitorImplementationVariationPropertyGroupContext groupContext, VisitorImplementationPropertyRelevance propertyContext, VisitorImplementationVariationPropertiesContext visitorPropertiesContext, VisitorImplementationVariationContext context, IInterfaceType visitorInterface, IInterfaceMethodMember interfaceMethod, IIntermediateClassMethodMember concreteVariant, IVisitorImplementationBuilder implementationBuilder);
    }

    public enum VisitorImplementationPropertyGroupRelevance
    {
        NotSet,
        IgnoreGroup,
        HandleAsGroup,
        HandleItemsSeparately,
    }

    /// <summary>Denotes the relevance of a type with respect to the visitor model so one can make a rational choice about how to marshall the type.</summary>
    public enum VisitorImplementationTypeRelevance
    {
        /// <summary>Denotes that the type is not relevant to the visitor implementation.</summary>
        NotRelevant,
        /// <summary>Denotes that the type is </summary>
        AsItem,
        AsItemSet,
        AsValueSet,
        AsDeclValueSet,
        AsActionableItem,
        AsActionableItemSet,
        AsActionableDeclValueSet,
        AsActionableValueSet,
    }
    public class VisitorImplementationPropertyRelevance
    {
        public VisitorImplementationTypeRelevance Relevance { get; internal set; }
        public IType AdjustedPropertyType { get; internal set; }
        public IPropertySignatureMember PropertyMember { get; internal set; }

        public bool IsSingletonProperty { get; internal set; }

        protected VisitorImplementationPropertyRelevance() { }
        public VisitorImplementationPropertyRelevance(VisitorImplementationAdjustedTypeRelevance adjustedType, IPropertySignatureMember propertyMember)
        {
            this.Relevance = adjustedType.Relevance;
            this.AdjustedPropertyType = adjustedType.AdjustedType;
            this.PropertyMember = propertyMember;
        }

    }

    public class VisitorImplementationAdjustedTypeRelevance
    {
        public VisitorImplementationTypeRelevance Relevance     { get; internal set; }
        public IType                              AdjustedType  { get; internal set; }
        public VisitorImplementationAdjustedTypeRelevance(VisitorImplementationTypeRelevance relevance, IType adjustedType) 
        {
            this.Relevance = relevance;
            this.AdjustedType = adjustedType;
        }
        public VisitorImplementationAdjustedTypeRelevance(IType adjustedType, VisitorImplementationTypeRelevance relevance) : this(relevance, adjustedType) { }

    }


    public class VisitorModelCommonTypeContext :
        IDisposable
    {
        /// <summary>Creates a <see cref="VisitorModelCommonTypeContext"/> with the <paramref name="identityManager"/> provided.</summary>
        /// <param name="identityManager"></param>
        public VisitorModelCommonTypeContext(IIntermediateIdentityManager identityManager)
        {
            this.GroupPropertiesAttribute           = (IClassType)identityManager.ObtainTypeReference(typeof(VisitorImplementationGroupPropertiesAttribute));
            this.TargetAttribute                    = (IClassType)identityManager.ObtainTypeReference(typeof(VisitorTargetAttribute));
            this.ProcessAsGroupAttribute            = (IClassType)identityManager.ObtainTypeReference(typeof(VisitorImplementationProcessAsGroupAttribute));
            this.ImplementationTargetAttribute      = (IClassType)identityManager.ObtainTypeReference(typeof(VisitorImplementationTargetAttribute));
            this.ImplementationInheritanceAttribute = (IClassType)identityManager.ObtainTypeReference(typeof(VisitorImplementationInheritanceAttribute));
            this.IgnorePropertyAttribute            = (IClassType)identityManager.ObtainTypeReference(typeof(VisitorImplementationIgnorePropertyAttribute));
            this.IgnoreLocalSetAttribute            = (IClassType)identityManager.ObtainTypeReference(typeof(VisitorImplementationIgnoreLocalSetAttribute));
            this.PropertyRequirementAttribute       = (IClassType)identityManager.ObtainTypeReference(typeof(VisitorPropertyRequirementAttribute));
            this.ActionDetailAttribute              = (IClassType)identityManager.ObtainTypeReference(typeof(VisitorImplementationActionDetailAttribute));
            this.IEnumerableOfT                     = (IInterfaceType)identityManager.ObtainTypeReference(typeof(IEnumerable<>));
            this.IControlledDictionaryOfTKeyTValue  = (IInterfaceType)identityManager.ObtainTypeReference(typeof(IControlledDictionary<,>));
            this.IIntermediateDeclarationDictionary = (IInterfaceType)identityManager.ObtainTypeReference(typeof(IIntermediateDeclarationDictionary<,,>));
        }

        public void Dispose()
        {
            this.IgnorePropertyAttribute        = null;
            this.TargetAttribute                = null;
            this.IgnoreLocalSetAttribute        = null;
            this.PropertyRequirementAttribute   = null;
            this.ActionDetailAttribute          = null;
            this.IEnumerableOfT                 = null;
        }
        /// <summary>Returns a reference to the <see cref="VisitorImplementationProcessAsGroupAttribute"/>.</summary>
        public IClassType                   ProcessAsGroupAttribute             { get; private set; }

        /// <summary>Returns a reference to the <see cref="VisitorImplementationIgnorePropertyAttribute"/>.</summary>
        public IClassType                   IgnorePropertyAttribute             { get; private set; }
        /// <summary>Returns a reference to the <see cref="VisitorImplementationTargetAttribute"/>.</summary>
        public IClassType                   TargetAttribute                     { get; private set; }
        /// <summary>Returns a reference to the <see cref="VisitorImplementationTargetAttribute"/>.</summary>
        public IClassType                   ImplementationTargetAttribute       { get; private set; }
        /// <summary>Returns a reference to the <see cref="VisitorImplementationIgnoreLocalSetAttribute"/>.</summary>
        public IClassType                   IgnoreLocalSetAttribute             { get; private set; }
        /// <summary>Returns a reference to the <see cref="VisitorPropertyRequirementAttribute"/>.</summary>
        public IClassType                   PropertyRequirementAttribute        { get; private set; }
        /// <summary>Returns a reference to the <see cref="VisitorImplementationActionDetailAttribute"/>.</summary>
        public IClassType                   ActionDetailAttribute               { get; private set; }

        /// <summary>Returns a reference to the <see cref="VisitorImplementationGroupPropertiesAttribute"/>.</summary>
        public IClassType                   GroupPropertiesAttribute            { get; private set; }

        public IClassType                   ImplementationInheritanceAttribute  { get; private set; }

        /// <summary>Returns a reference to the <see cref="IEnumerable{T}"/> interface to help determine the <see cref="VisitorImplementationTypeRelevance"/> of types within the model.</summary>
        public IInterfaceType               IEnumerableOfT                      { get; private set; }
        /// <summary>Returns a reference to the <see cref="IControlledDictionary{TKey, TValue}"/> interface to help determine the <see cref="VisitorImplementationTypeRelevance"/> of types within the model.</summary>
        public IInterfaceType               IControlledDictionaryOfTKeyTValue   { get; private set; }
        /// <summary>Returns a reference to the <see cref="IIntermediateDeclarationDictionary{TIdentifier, TDeclaration, TIntermediateDeclaration}"/> interface to help determine the <see cref="VisitorImplementationTypeRelevance"/> of types within the model.</summary>
        public IInterfaceType               IIntermediateDeclarationDictionary  { get; private set; }

    }

    public class VisitorImplementationContext
    {
        private IControlledDictionary<string, IEnumerable<VisitorImplementationVariationContext>> _variationsByMethodName;
        private IControlledDictionary<string, IType[]> _acceptorTypesByMethodName;
        private MultikeyedDictionary<VisitorBuilder, IInterfaceType, IControlledDictionary<IInterfaceMethodMember, IIntermediateClassMethodMember>> _fullImplementationDetail;

        private IEnumerable<VisitorImplementationVariationContext> _buildContext;
        private IControlledDictionary<string, IType[]> _actionableTypesByMethodName;
        private IControlledDictionary<string, IType[]> _visitorTypesByMethodName;

        public List<IIntermediateClassPropertyMember> VisitorProps                  { get; internal set; }

        private IEnumerable<string> _visitorMethodNames;

        public ImplementedInterfaceLookup           QuickImplementationLookup       { get; private  set; }

        public VisitorImplementationTargetDetail    Detail                          { get; private  set; }

        public VisitorModelCommonTypeContext        CommonTypeContext               { get; private  set; }

        public IIntermediateAssembly                TargetAssembly                  { get; private  set; }

        public IExpressionService                   ExpressionService               { get; private  set; }

        public IIntermediateClassGenericParameter   TResult                         { get; internal set; }

        public IIntermediateClassGenericParameter   TContext                        { get; internal set; }

        public IEnumerable<string>                  VisitorMethodNames              { get { return this._visitorMethodNames ?? (this._visitorMethodNames = this.Variations.Select(k=>k.VisitMethodName).Distinct().ToArray()); } }

        public IControlledDictionary<string, IEnumerable<VisitorImplementationVariationContext>> VariationsByMethodName { get { return this._variationsByMethodName ?? (this._variationsByMethodName = this.InitializeVariationsByMethodName()); } }

        public IMultikeyedDictionary<VisitorBuilder, IInterfaceType, IControlledDictionary<IInterfaceMethodMember, IIntermediateClassMethodMember>> FullImplementationDetail { get { return this._fullImplementationDetail ?? (this._fullImplementationDetail = new MultikeyedDictionary<VisitorBuilder, IInterfaceType, IControlledDictionary<IInterfaceMethodMember, IIntermediateClassMethodMember>>()); } }

        private ControlledDictionary<string, IEnumerable<VisitorImplementationVariationContext>> InitializeVariationsByMethodName()
        {
            return
                new ControlledDictionary<string, IEnumerable<VisitorImplementationVariationContext>>((
                    from variation in this.Variations
                    group variation by variation.VisitMethodName).ToDictionary(k=>k.Key, v=>(IEnumerable<VisitorImplementationVariationContext>)v.ToArray()));
        }

        public IControlledDictionary<string, IType[]> AcceptorTypesByMethodName { get { return this._acceptorTypesByMethodName ?? (this._acceptorTypesByMethodName = this.InitializeAcceptorTypesByMethodName()); } }

        public IControlledDictionary<string, IType[]> ActionableTypesByMethodName { get { return this._actionableTypesByMethodName ?? (this._actionableTypesByMethodName = this.InitializeActionableTypesByMethodName()); } }

        public IControlledDictionary<string, IType[]> VisitorTypesByMethodName { get { return this._visitorTypesByMethodName ?? (this._visitorTypesByMethodName = this.InitializeVisitorTypesByMethodName()); } }

        private IControlledDictionary<string, IType[]> InitializeAcceptorTypesByMethodName()
        {
            var data = (
                from methodName in this.VariationsByMethodName.Keys
                from variation in this.VariationsByMethodName[methodName]
                from type in variation.AcceptorTypes
                group type by methodName).ToDictionary(k => k.Key, v => v.ToArray());
            foreach (var methodName in this.VisitorMethodNames)
                if (!data.ContainsKey(methodName))
                    data.Add(methodName, new IType[0]);
            return new ControlledDictionary<string, IType[]>(data);
        }

        private IControlledDictionary<string, IType[]> InitializeActionableTypesByMethodName()
        {
            var data = (
                from methodName in this.VariationsByMethodName.Keys
                from variation in this.VariationsByMethodName[methodName]
                from type in variation.ActionableTypes
                group type by methodName).ToDictionary(k => k.Key, v => v.ToArray());
            foreach (var methodName in this.VisitorMethodNames)
                if (!data.ContainsKey(methodName))
                    data.Add(methodName, new IType[0]);
            return new ControlledDictionary<string, IType[]>(data);
        }

        private IControlledDictionary<string, IType[]> InitializeVisitorTypesByMethodName()
        {
            var data = (
                from methodName in this.VariationsByMethodName.Keys
                from variation in this.VariationsByMethodName[methodName]
                from type in variation.VisitorTypes
                group type by methodName).ToDictionary(k => k.Key, v => v.ToArray());
            foreach (var methodName in this.VisitorMethodNames)
                if (!data.ContainsKey(methodName))
                    data.Add(methodName, new IType[0]);
            return new ControlledDictionary<string, IType[]>(data);
        }
        public IEnumerable<VisitorImplementationVariationContext> Variations
        {
            get
            {
                foreach (var element in _buildContext)
                    yield return element;
            }
        }

        public IEnumerable<string> DistinctMethodSignatureNames { get { return this.Variations.Select(k => k.VisitMethodName).Distinct().OrderBy(k => k); } }

        public VisitorImplementationContext(VisitorImplementationTargetDetail detail, VisitorModelCommonTypeContext attributeContext, IEnumerable<VisitorImplementationVariationContext> buildContext, IIntermediateAssembly targetAssembly)
        {
            if (detail == null)
                throw new ArgumentNullException("detail");
            if (attributeContext == null)
                throw new ArgumentNullException("attributeContext");
            if (buildContext == null)
                throw new ArgumentNullException("buildContext");
            if (targetAssembly == null)
                throw new ArgumentNullException("targetAssembly");
            if (targetAssembly.Provider == null)
                throw new ArgumentOutOfRangeException("targetAssembly", "The target assembly provider is null; however, it is mandatory for this builder to recognize expressions.");
            if (!targetAssembly.Provider.SupportsService(LanguageGuids.Services.ExpressionService))
                throw new NotSupportedException("The targetAssembly provided does not support an expression service, which is mandatory for this builder to recognize expressions.");
            this.CommonTypeContext = attributeContext;
            this.Detail = detail;
            this._buildContext = buildContext.ToArray();
            this.TargetAssembly = targetAssembly;
            this.ExpressionService = targetAssembly.Provider.GetService<IExpressionService>(LanguageGuids.Services.ExpressionService);
        }
    }

    public class VisitorInterfaceContext :
        ControlledCollection<IType>
    {

        private IControlledCollection<IType> __visitorTypes;
        private IControlledCollection<IType> _visitorTypes;

        internal VisitorInterfaceContext(IType contextType, IType resultType, IIntermediateInterfaceType intermediateVisitorType, IEnumerable<IType> visitorTypes, IEnumerable<VisitorInterfaceContext> subVisitors, VisitorBuilder builder)
        {
            this.TContext                   = contextType;
            this.TResult                    = resultType;
            this.IntermediateVisitorType    = intermediateVisitorType;
            this.__visitorTypes             = new ControlledCollection<IType>(visitorTypes.ToList());
            this.SubVisitors                = new ControlledCollection<VisitorInterfaceContext>(subVisitors.ToArray());
            this.Builder = builder;
        }

        /// <summary>Returns the <see cref="IType"/> which is used in place of the TContext type parameter.</summary>
        public IType TContext { get; private set; }
        /// <summary>Returns the <see cref="IType"/> which is used in place of the TResult type parameter.</summary>
        public IType TResult { get; private set; }

        /// <summary>Returns the <see cref="IIntermediateInterfaceType"/> which denotes the visitor relevant for this context.</summary>
        public IIntermediateInterfaceType IntermediateVisitorType { get; private set; }

        /// <summary>Returns the <see cref="IEnumerable{T}"/> of <see cref="IType"/> elements which denote the full range of types that the <see cref="VisitorInterfaceContext"/> covers.</summary>
        public IControlledCollection<IType> VisitorTypes { get { return this._visitorTypes ?? (this._visitorTypes = new ControlledCollection<IType>(this.__visitorTypes.Concat(this.SubVisitors.SelectMany(k => k.VisitorTypes)).OrderBy(k => k.NamespaceName).Distinct().ToList())); } }

        public IControlledCollection<VisitorInterfaceContext> SubVisitors { get; private set; }

        public VisitorBuilder Builder { get; set; }
    }

    public class VisitorImplementationVariationContext
    {
        private MultikeyedDictionary<VisitorBuilder, IInterfaceType, IControlledDictionary<IInterfaceMethodMember, IIntermediateClassMethodMember>> _implementationDetail;
        private string _visitMethodName;
        private IControlledCollection<IType> _acceptorTypes;
        private IControlledCollection<IType> _actionableTypes;
        private IControlledCollection<IType> _visitorTypes;
        private IAssembly _originAssembly;
        private IControlledCollection<VisitorBuilder> _allVisitors;

        public IEnumerable<VisitorBuilder> RelevantBuilders { get; private set; }
        public IType ContextType { get; private set; }
        public IType ResultType { get; private set; }
        public string VisitMethodName { get { return this._visitMethodName ?? "Visit"; } }
        public bool VisitMethodIsAbstract { get; private set; }
        public IControlledCollection<IType> AcceptorTypes { get { return this._acceptorTypes ?? (this._acceptorTypes = this.InitializeAcceptorTypes()); } }
        public IControlledCollection<IType> ActionableTypes { get { return this._actionableTypes ?? (this._actionableTypes = this.InitializeActionableTypes()); } }
        public IControlledCollection<IType> VisitorTypes { get { return this._visitorTypes ?? (this._visitorTypes = this.InitializeVisitorTypes()); } }
        public IMultikeyedDictionary<VisitorBuilder, IInterfaceType, IControlledDictionary<IInterfaceMethodMember, IIntermediateClassMethodMember>> ImplementationDetail { get { return this._implementationDetail ?? (this._implementationDetail = new MultikeyedDictionary<VisitorBuilder, IInterfaceType, IControlledDictionary<IInterfaceMethodMember, IIntermediateClassMethodMember>>()); } }

        public IVisitorImplementationHandler Handler { get; private set; }

        public Dictionary<VisitorBuilder, IInterfaceType> ProperInterfaces { get; private set; }

        internal VisitorImplementationVariationContext(IEnumerable<VisitorBuilder> builders, IType contextType, IType resultType, string visitMethodName, bool visitMethodAbstract, IType handler, IAssembly originAssembly)
        {
            this._originAssembly = originAssembly;
            this._visitMethodName = visitMethodName;
            this.ResultType = resultType;
            this.ContextType = contextType;
            this.RelevantBuilders = builders.SinglePass();
            this.VisitMethodIsAbstract = visitMethodAbstract;
            var fullType = (handler == null ? null : Type.GetType(handler.FullName, false)) ?? typeof(VisitorImplementationHandlerDefault);
            this.Handler = (Activator.CreateInstance(fullType) as IVisitorImplementationHandler) ?? new VisitorImplementationHandlerDefault();
            this.ProperInterfaces = new Dictionary<VisitorBuilder, IInterfaceType>();
        }

        public IVisitorImplementationBuilder ImplementationBuilder { get; internal set; }

        private IControlledCollection<IType> InitializeVisitorTypes()
        {
            return new ControlledCollection<IType>(InitializeVisitorTypesInternal().Distinct().ToArray());
        }

        private IEnumerable<IType> InitializeVisitorTypesInternal()
        {
            foreach (var visitorBuilder in this.AllVisitors)
                foreach (var type in visitorBuilder.RelevantTypes.Except(visitorBuilder.SkippedTypes))
                    yield return type;
        }

        public IControlledCollection<VisitorBuilder> AllVisitors
        {
            get
            {
                return this._allVisitors ?? (this._allVisitors = new ControlledCollection<VisitorBuilder>(this.GetAllVisitors(this.RelevantBuilders).ToArray()));
            }
        }

        private IEnumerable<VisitorBuilder> GetAllVisitors(IEnumerable<VisitorBuilder> enumerable)
        {
            if (enumerable == null)
                yield break;
            foreach (var visitor in enumerable)
            {
                yield return visitor;
                foreach (var childVisitor in GetAllVisitors(visitor.ChildBuilders))
                    yield return childVisitor;
            }
        }

        private IControlledCollection<IType> InitializeActionableTypes()
        {
            var originMetadata = this._originAssembly.Metadata.Where(k => k.Type == this.ImplementationBuilder.Context.CommonTypeContext.ActionDetailAttribute).Select(k => VisitorImplementationActionDetail.DeriveFromMetadata(k));
            var filteredMetadata =
                originMetadata
                .Where(k =>
                    (this.ImplementationBuilder.Context.Detail.TargetContext == k.TargetContext) && 
                    (this.ContextType != null && this.ContextType.Equals(k.TContext) || this.ContextType == null && k.TContext == null)         &&
                    (this.ResultType != null && this.ResultType.Equals(k.TResult) || this.ResultType == null && k.TResult == null)              &&
                    (k.ContextualVisitor == (this.ContextType != null ? true : this.ImplementationBuilder.Context.Detail.ContextualVisitor))    &&
                    (k.YieldingVisitor == (this.ResultType != null ? true : this.ImplementationBuilder.Context.Detail.YieldingVisitor)))
                .ToArray();
            return new ControlledCollection<IType>(filteredMetadata.Select(k => k.TargetType).ToArray());
        }

        private IControlledCollection<IType> InitializeAcceptorTypes()
        {
            var acceptorQueryA = VisitorTypes.SelectMany(k => new[] { k }.Concat(k.ImplementedInterfaces)).OfType<IInterfaceType>().Distinct().Where(k => k.Methods.Values.Any(j => j.Name == "Accept"));
            var mainMethodParameters = this.ProperInterfaces.Values.Concat(this.ProperInterfaces.Values.SelectMany(i=>i.ImplementedInterfaces)).OfType<IInterfaceType>().SelectMany(k => k.Methods.Values).Select(k => k.Parameters.Values.FirstOrDefault()).Where(k => k != null).Select(k => k.ParameterType).Concat(acceptorQueryA).OrderBy(k => k.Name).Distinct();
            return new ControlledCollection<IType>(mainMethodParameters.ToArray());
        }
    }

#endif
}
