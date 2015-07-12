using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Utilities.Events;
using AllenCopeland.Abstraction.Slf.Abstract;
namespace AllenCopeland.Abstraction.Slf.Ast.Expressions.Lambda
{
    public abstract class LambdaTypedExpression :
        LambdaExpression,
        ILambdaTypedExpression
    {
        public ILambdaSignatureType Signature
        {
            get { throw new NotImplementedException(); }
        }

        public ILambdaTypedExpressionParameterMemberDictionary Parameters
        {
            get { throw new NotImplementedException(); }
        }

        IIntermediateParameterMemberDictionary<ILambdaTypedExpression, ILambdaTypedExpression, ILambdaTypedExpressionParameterMember, ILambdaTypedExpressionParameterMember> IIntermediateParameterParent<ILambdaTypedExpression, ILambdaTypedExpression, ILambdaTypedExpressionParameterMember, ILambdaTypedExpressionParameterMember>.Parameters
        {
            get { throw new NotImplementedException(); }
        }

        public event EventHandler<EventArgsR1<ILambdaTypedExpressionParameterMember>> ParameterAdded;

        public event EventHandler<EventArgsR1<ILambdaTypedExpressionParameterMember>> ParameterRemoved;

        IIntermediateParameterMemberDictionary IIntermediateParameterParent.Parameters
        {
            get { throw new NotImplementedException(); }
        }

        event EventHandler<EventArgsR1<IIntermediateParameterMember>> IIntermediateParameterParent.ParameterAdded
        {
            add { throw new NotImplementedException(); }
            remove { throw new NotImplementedException(); }
        }

        event EventHandler<EventArgsR1<IIntermediateParameterMember>> IIntermediateParameterParent.ParameterRemoved
        {
            add { throw new NotImplementedException(); }
            remove { throw new NotImplementedException(); }
        }

        IParameterMemberDictionary IParameterParent.Parameters
        {
            get { throw new NotImplementedException(); }
        }

        public bool LastIsParams
        {
            get {
                throw new NotImplementedException();
            }
        }

        IParameterMemberDictionary<ILambdaTypedExpression, ILambdaTypedExpressionParameterMember> IParameterParent<ILambdaTypedExpression, ILambdaTypedExpressionParameterMember>.Parameters
        {
            get { throw new NotImplementedException(); }
        }
    }
}
