using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public class VisitorImplementationTargetAttribute :
        Attribute
    {
        public string TargetContext { get; private set; }
        public bool ContextualVisitor { get; set; }
        public bool YieldingVisitor { get; set; }
        public string Namespace { get; set; }
        public int AccessLevelModifiers { get; set; }
        public VisitorImplementationTargetAttribute(string targetContext)
        {
            this.TargetContext = targetContext;
        }
    }

    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public class VisitorImplementationInheritanceAttribute :
        Attribute
    {
        public string   TargetContext           { get; private  set; }
        public bool     ContextualVisitor       { get;          set; }
        public bool     YieldingVisitor         { get;          set; }
        public Type     TContext                { get;          set; }
        public Type     TResult                 { get;          set; }
        public string[] InheritedVisitors       { get;          set; }
        public string   VisitRefactorName       { get;          set; }
        public bool     VisitRefactorAbstract   { get;          set; }
        public Type     VisitorBuilderHandler   { get;          set; }
        public VisitorImplementationInheritanceAttribute(string targetContext)
        {
            this.TargetContext = targetContext;
        }
    }
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Struct)]
    public class VisitorImplementationProcessAsGroupAttribute :
        Attribute
    {

    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Struct, Inherited = true)]
    public class VisitorImplementationGroupPropertiesAttribute :
        Attribute
    {
        public string[] PropertyNames { get; set; }
        public bool HandleItemsAsGroup { get; set; }
        public string GroupId { get; set; }
    }
    [AttributeUsage(AttributeTargets.Property)]
    public class VisitorImplementationIgnorePropertyAttribute :
        Attribute
    {
    }
    [AttributeUsage(AttributeTargets.Interface)]
    public class VisitorImplementationIgnoreLocalSetAttribute :
        Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class VisitorPropertyRequirementAttribute :
        Attribute
    {
        public string RequiredProperty { get; set; }

        public VisitorPropertyRequirementAttribute(string requiredProperty) { this.RequiredProperty = requiredProperty; }
    }
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public class VisitorImplementationActionDetailAttribute :
        Attribute
    {
        public string   TargetContext       { get; private  set; }
        public Type     TargetType          { get; private  set; }
        public string   TargetAction        { get; private  set; }
        public string   TargetPluralAction  { get; private  set; }
        public bool     ContextualVisitor   { get;          set; }
        public bool     YieldingVisitor     { get;          set; }
        public Type     TContext            { get;          set; }
        public Type     TResult             { get;          set; }
        public VisitorImplementationActionDetailAttribute(string targetContext, Type targetType, string targetAction, string targetPluralAction)
        {
            this.TargetContext = targetContext;
            this.TargetType = targetType;
            this.TargetAction = targetAction;
            this.TargetPluralAction = targetPluralAction;
        }
    }
}
