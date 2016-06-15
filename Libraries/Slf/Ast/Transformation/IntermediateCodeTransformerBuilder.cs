using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
using AllenCopeland.Abstraction.Slf.Ast.Statements;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Transformation
{
    #if DEBUG
    public class IntermediateCodeTransformerCodeImpactHandler :
        VisitorImplementationHandlerDefault
    {
        public override IIntermediateClassMethodMember CreateVariationBuilderMethod(string methodName, IGeneralGenericSignatureMemberUniqueIdentifier methodSignature, IInterfaceMethodMember interfaceMethodMember, VisitorImplementationVariationContext variation, VisitorBuilder visitorBuilder, IInterfaceType relevantBuilderInterface, IVisitorImplementationBuilder implementationBuilder)
        {
            var result =  base.CreateVariationBuilderMethod(methodName, methodSignature, interfaceMethodMember, variation, visitorBuilder, relevantBuilderInterface, implementationBuilder);
            if (!result.IsAbstract)
                result.IsVirtual = true;
            return result;
        }

        public override IIntermediateGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, IClassType, IIntermediateClassType> CreateContextTypeParameter(IVisitorImplementationBuilder implementationBuilder)
        {
            var result = base.CreateContextTypeParameter(implementationBuilder);
            result.Constraints.Add(implementationBuilder.Context.TargetAssembly.IdentityManager.ObtainTypeReference(typeof(TransformationContext)));
            result.Constructors.Add();
            return result;
        }

        public override void ImplementMethodBody(VisitorImplementationVariationContext context, VisitorImplementationVariationPropertiesContext visitorPropertiesContext, IInterfaceType visitorInterface, IInterfaceMethodMember interfaceMethod, IIntermediateClassMethodMember concreteVariant, IVisitorImplementationBuilder implementationBuilder)
        {
            if (visitorPropertiesContext.Values.Sum(k => k.RelevantProperties.Count()) == 0)
                concreteVariant.Return(GetTypeReference(implementationBuilder, typeof(TransformationImpact)).GetTypeExpression().GetField(TransformationImpact.NoImpact.ToString()));
            else
                base.ImplementMethodBody(context, visitorPropertiesContext, visitorInterface, interfaceMethod, concreteVariant, implementationBuilder);
        }

        public override VisitorImplementationPropertyRelevance GetPropertyTypeRelevance(IPropertySignatureMember property, IType visitingType, VisitorImplementationVariationContext context, IVisitorImplementationBuilder builder)
        {

            var result = base.GetPropertyTypeRelevance(property, visitingType, context, builder);
            var itp = GetTypeReference(builder,typeof(IIntermediateTypeParent));
            var eStatements = builder.Context.CommonTypeContext.IEnumerableOfT.MakeGenericClosure(this.GetTypeReference(builder, typeof(IStatement)));
            if ((property.Name == "Parent" || property.Name == "Assembly") && result.Relevance != VisitorImplementationTypeRelevance.NotRelevant)
                return new VisitorImplementationPropertyRelevance(new VisitorImplementationAdjustedTypeRelevance(result.AdjustedPropertyType, VisitorImplementationTypeRelevance.NotRelevant), result.PropertyMember);
            return result;
        }

        private IType GetTypeReference(IVisitorImplementationBuilder builder, Type type)
        {
            return builder.Context.TargetAssembly.IdentityManager.ObtainTypeReference(type);
        }
    }

    public class IntermediateCodeTransformerTransformHandler :
        VisitorImplementationHandlerDefault
    {
        public override IIntermediateClassMethodMember CreateVariationBuilderMethod(string methodName, IGeneralGenericSignatureMemberUniqueIdentifier methodSignature, IInterfaceMethodMember interfaceMethodMember, VisitorImplementationVariationContext variation, VisitorBuilder visitorBuilder, IInterfaceType relevantBuilderInterface, IVisitorImplementationBuilder implementationBuilder)
        {
            var result = base.CreateVariationBuilderMethod(methodName, methodSignature, interfaceMethodMember, variation, visitorBuilder, relevantBuilderInterface, implementationBuilder);
            if (!result.IsAbstract)
            {
                result.IsVirtual = true;
                result.Return(result.Parameters.Values[0].GetReference());
            }
            return result;
        }
        public override void ImplementMethodBodyPropertyGroup(VisitorImplementationVariationPropertyGroupContext groupContext, VisitorImplementationVariationContext context, VisitorImplementationVariationPropertiesContext visitorPropertiesContext, IInterfaceType visitorInterface, IInterfaceMethodMember interfaceMethod, IIntermediateClassMethodMember concreteVariant, IVisitorImplementationBuilder implementationBuilder)
        {
            switch (groupContext.GroupId)
            {
                case "IntermediateGenericParameter":
                case "IntermediateInterface":
                case "IntermediateInstantiable":
                    concreteVariant.Literal(string.Format("#region {0}", groupContext.GroupId));
                    base.ImplementMethodBodyPropertyGroup(groupContext, context, visitorPropertiesContext, visitorInterface, interfaceMethod, concreteVariant, implementationBuilder);
                    concreteVariant.Literal(string.Format("#endregion //{0}", groupContext.GroupId));
                    break;
                default:
                    break;
            }
        }
    }
    #endif
}
