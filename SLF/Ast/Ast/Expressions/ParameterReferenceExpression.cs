using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Ast.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Expressions
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
            get { return ExpressionKind.ParameterReference; }
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
            get { return ExpressionKind.ParameterReference; }
        }

        public override void Visit(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }
        public string Name { get; set; }
    }
}
