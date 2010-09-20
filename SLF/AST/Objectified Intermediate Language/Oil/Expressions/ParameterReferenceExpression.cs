using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil.Members;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;

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

        public TIntermediateParameter ReferenceTarget { get; set; }

        #endregion

        public override string ToString()
        {
            return this.ReferenceTarget.Name;
        }

        protected override IType TypeLookupAid
        {
            get
            {
                return this.ReferenceTarget.ParameterType;
            }
        }

        #region IParameterReferenceExpression Members

        public string Name
        {
            get
            {
                return this.ReferenceTarget.Name;
            }
            set
            {
                this.ReferenceTarget.Name = value;
            }
        }

        #endregion
    }

    public class ParameterReferenceExpression :
        MemberParentReferenceExpressionBase,
        IParameterReferenceExpression
    {
        public ParameterReferenceExpression(string name)
        {
            this.Name = name;
        }
        public override ExpressionKind Type
        {
            get { return ExpressionKinds.ParameterReference; }
        }

        public override void Visit(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }
        public string Name { get; set; }
    }
}
