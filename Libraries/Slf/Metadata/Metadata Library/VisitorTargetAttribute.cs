using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = true)]
    public class VisitorTargetAttribute :
        Attribute
    {
        public string TargetContext { get; private set; }
        public bool ContextualVisitor { get; set; }
        public bool YieldingVisitor { get; set; }
        public Type[] TypeParameterVariations { get; set; }
        public string ParameterlessName { get; set; }
        public string DerivedThroughInheriting { get; set; }
        public VisitorTargetAttribute(string targetContext, params Type[] typeParameterVariations)
        {
            this.TargetContext = targetContext;
            this.TypeParameterVariations = typeParameterVariations;
        }
    }
}
