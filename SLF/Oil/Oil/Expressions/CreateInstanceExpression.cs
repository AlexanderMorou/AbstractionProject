using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */
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

        public CreateInstanceExpression(IConstructorPointerReferenceExpression reference, IEnumerable<IExpression> parameters)
            : base(reference, parameters)
        {
        }

        internal static CreateInstanceExpression GetByExpressionCollection<T>(IConstructorPointerReferenceExpression reference, IExpressionCollection<T> parameters)
            where T :
                IExpression
        {
            var result = new CreateInstanceExpression(reference);
            result.Parameters.AddRange(parameters);
            return result;
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
