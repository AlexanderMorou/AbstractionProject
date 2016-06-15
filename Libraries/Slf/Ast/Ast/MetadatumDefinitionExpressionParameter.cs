using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Ast
{
    public class MetadatumDefinitionExpressionParameter :
        ExpressionBase,
        IMetadatumDefinitionExpressionParameter
    {
        public IExpression Value { get; set; }
        internal MetadatumDefinitionParameterCollection Owner { get; private set; }

        public MetadatumDefinitionExpressionParameter(IExpression value, MetadatumDefinitionParameterCollection owner) 
        {
            this.Value = value;
            this.Owner = owner;
        }

        object IMetadatumDefinitionParameter.Value
        {
            get
            {
                return this.Value;
            }
            set
            {
                if (value is IExpression || value == null)
                    this.Value = (IExpression)value;
                else
                    throw ThrowHelper.ObtainArgumentException(ArgumentWithException.value, ExceptionMessageId.ValueIsWrongType, "value", value.GetType().ToString(), typeof(IExpression).ToString());
            }
        }

        public IType ParameterType
        {
            get {
                throw new NotImplementedException();
            }
        }

        public void Dispose()
        {
            this.Value = null;
        }

        public override ExpressionKind Type
        {
            get { return ExpressionKind.ConstantExpression; }
        }

        public override void Accept(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override TResult Accept<TResult, TContext>(ICommonExpressionVisitor<TResult, TContext> visitor, TContext context)
        {
            return visitor.Visit(this, context);
        }

        public override string ToString()
        {
            if (this.Value == null)
                return null;
            else
                return this.Value.ToString();
        }
    }
}
