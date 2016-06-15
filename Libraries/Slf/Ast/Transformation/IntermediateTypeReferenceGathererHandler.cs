using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Languages.Expressions;
using AllenCopeland.Abstraction.Slf.Ast.Statements;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
using AllenCopeland.Abstraction.Slf.Translation;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AllenCopeland.Abstraction.Slf.Ast;

namespace AllenCopeland.Abstraction.Slf.Transformation
{
    #if DEBUG
    public class IntermediateTypeReferenceGathererHandler :
        VisitorImplementationHandlerDefault
    {
        public override void ImplementMethodBodyPropertyGroup(
            VisitorImplementationVariationPropertyGroupContext groupContext, 
            VisitorImplementationVariationContext context, 
            VisitorImplementationVariationPropertiesContext visitorPropertiesContext, 
            IInterfaceType visitorInterface, 
            IInterfaceMethodMember interfaceMethod, 
            IIntermediateClassMethodMember concreteVariant, 
            IVisitorImplementationBuilder implementationBuilder)
        {
            switch (groupContext.GroupId)
            {
                case "IntermediateGenericParameter":
                    concreteVariant.Literal(string.Format("#region {0}", groupContext.GroupId));
                    base.ImplementMethodBodyPropertyGroup(groupContext, context, visitorPropertiesContext, visitorInterface, interfaceMethod, concreteVariant, implementationBuilder);
                    concreteVariant.Literal(string.Format("#endregion //{0}", groupContext.GroupId));
                    break;
                case "IntermediateInterface":
                case "IntermediateInstantiable":
                    concreteVariant.Literal(string.Format("#region {0}", groupContext.GroupId));
                    var target = concreteVariant.If(implementationBuilder.ResultVisitorClass.GetThis().GetField("allowPartials"));
                    var initParam = concreteVariant.Parameters.Values[0].GetReference();
                    target.CreateNext(initParam.GetProperty("IsRoot"));
                    var targetNext = target.Next;
                    target = target.If(initParam.GetProperty("HasMembers"));
                    var targetEnumerable = target.Enumerate("member", initParam.GetProperty("Members").GetMethod("ExclusivelyOnParent").Invoke(initParam));
                    var targetEnumerableNullCheck = targetEnumerable.If(targetEnumerable.Local.GetReference().GetProperty("Value").GetProperty("Entry").InequalTo(IntermediateGateway.NullValue, implementationBuilder.Context.ExpressionService));
                    targetEnumerableNullCheck.Call(targetEnumerable.Local.GetReference().GetProperty("Value").GetProperty("Entry").GetMethod("Accept").Invoke(implementationBuilder.ResultVisitorClass.GetThis()));
                    targetEnumerable = targetNext.Enumerate("member", initParam.GetProperty("Members").GetProperty("Values"));
                    targetEnumerableNullCheck = targetEnumerable.If(targetEnumerable.Local.GetReference().GetProperty("Entry").InequalTo(IntermediateGateway.NullValue, implementationBuilder.Context.ExpressionService));
                    targetEnumerableNullCheck.Call(targetEnumerable.Local.GetReference().GetProperty("Entry").GetMethod("Accept").Invoke(implementationBuilder.ResultVisitorClass.GetThis()));
                    concreteVariant.Literal(string.Format("#endregion //{0}", groupContext.GroupId));
                    break;
                default:
                    base.ImplementMethodBodyPropertyGroup(groupContext, context, visitorPropertiesContext, visitorInterface, interfaceMethod, concreteVariant, implementationBuilder);
                    break;
            }
        }
    }
    #endif
}
