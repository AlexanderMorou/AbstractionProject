using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil.Members;
using AllenCopeland.Abstraction.Slf.Abstract.Members;

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    public class ParameterReferenceExpression<TParameterParent, TIntermediateParameterParent, TParameter, TIntermediateParameter> :
        ExpressionBase,
        IParameterReferenceExpression<TParameterParent, TIntermediateParameterParent, TParameter, TIntermediateParameter>
        where TParameter :
            IParameterMember<TParameterParent>
        where TParameterParent :
            IParameterParent<TParameterParent, TParameter>
        where TIntermediateParameter :
            TParameter,
            IIntermediateParameterMember<TParameterParent, TIntermediateParameterParent>
        where TIntermediateParameterParent :
            TParameterParent,
            IIntermediateParameterParent<TParameterParent, TIntermediateParameterParent, TParameter, TIntermediateParameter>
    {
        private TIntermediateParameter parameter;

        public ParameterReferenceExpression(TIntermediateParameter parameter)
        {
            this.ReferenceTarget = parameter;
        }
        public override ExpressionKind Type
        {
            get { return ExpressionKinds.ParameterReference; }
        }

        public override void Visit(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }

        #region IParameterReferenceExpression Members

        IIntermediateParameterMember IParameterReferenceExpression.ReferenceTarget
        {
            get
            {
                return this.ReferenceTarget;
            }
            set
            {
                if (!(value is TIntermediateParameter))
                    throw new ArgumentException("value");
                this.ReferenceTarget = (TIntermediateParameter)value;
            }
        }

        public TIntermediateParameter ReferenceTarget { get; set; }

        #endregion

        public override string ToString()
        {
            return this.ReferenceTarget.Name;
        }
    }
}
