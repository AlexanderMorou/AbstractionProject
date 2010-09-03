using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    public class CreateInstanceExpression :
        ConstructorInvokeExpression,
        ICreateInstanceExpression
    {
        private ICreateInstanceMemberAssignmentDictionary propertyAssignments;
        public CreateInstanceExpression(IConstructorPointerReferenceExpression reference)
            : base(reference)
        {

        }
        
        public CreateInstanceExpression(IConstructorPointerReferenceExpression reference, params IExpression[] parameters)
            : base(reference, parameters)
        {
        }

        public CreateInstanceExpression(IConstructorPointerReferenceExpression reference, IExpressionCollection parameters)
            : base(reference, parameters)
        {
        }

        #region ICreateInstanceExpression Members
        /// <summary>
        /// Returns the <see cref="ICreateInstanceMemberAssignmentDictionary"/> 
        /// which relates to the property assignment expressions for 
        /// the <see cref="CreateInstanceExpression"/>.
        /// </summary>
        public ICreateInstanceMemberAssignmentDictionary PropertyAssignments
        {
            get
            {
                if (this.propertyAssignments == null)
                    this.propertyAssignments = new CreateInstanceMemberAssignmentDictionary();
                return this.propertyAssignments;
            }
        }

        public bool HasAssignments
        {
            get
            {
                return this.propertyAssignments != null && this.propertyAssignments.Count > 0;
            }
        }

        #endregion

        public override string ToString()
        {
            if (this.propertyAssignments == null || this.propertyAssignments.Count == 0)
                return string.Format("new {0}({1})", this.Reference.Reference.InstanceType.BuildTypeName(true, typeParameterDisplayMode: TypeParameterDisplayMode.DebuggerStandard), string.Join(", ", this.Parameters));
            else
                return string.Format("new {0}({1}) {{ {2} }}", this.Reference.Reference.InstanceType.BuildTypeName(true, typeParameterDisplayMode: TypeParameterDisplayMode.DebuggerStandard), string.Join(", ", this.Parameters), string.Join(", ", this.PropertyAssignments.Values));
        }
    }
}
