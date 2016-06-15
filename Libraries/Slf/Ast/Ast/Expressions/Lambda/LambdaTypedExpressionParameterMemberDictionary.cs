using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Utilities.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Ast.Expressions.Lambda
{
    public class LambdaTypedExpressionParameterMemberDictionary :
        IntermediateParameterMemberDictionary<ILambdaTypedExpression, ILambdaTypedExpression, ILambdaTypedExpressionParameterMember, ILambdaTypedExpressionParameterMember>,
        ILambdaTypedExpressionParameterMemberDictionary,
        ILambdaTypeInferredExpressionParameterMemberDictionary
    {
        private EventHandler<EventArgsR1<ILambdaTypeInferredExpressionParameterMember>> _itemAdded;
        private EventHandler<EventArgsR1<ILambdaTypeInferredExpressionParameterMember>> _itemRemoved;
        //private CovariantReadOnlyCollection<ILambdaTypeInferredExpressionParameterMember, ILambdaTypedExpressionParameterMember>
        protected override ILambdaTypedExpressionParameterMember GetNewParameter(string name, Abstract.IType parameterType, ParameterCoercionDirection direction)
        {
            throw new NotImplementedException();
        }

        ILambdaTypeInferredExpression ILambdaTypeInferredExpressionParameterMemberDictionary.Parent
        {
            get { return this.Parent; }
        }

        KeyValuePair<IGeneralMemberUniqueIdentifier, ILambdaTypeInferredExpressionParameterMember> IIntermediateDeclarationDictionary<IGeneralMemberUniqueIdentifier, ILambdaTypeInferredExpressionParameterMember, ILambdaTypeInferredExpressionParameterMember>.this[int index]
        {
            get {
                if (index < 0 || index >= this.Count)
                    throw new ArgumentOutOfRangeException("index");
                var current = this[index];
                return new KeyValuePair<IGeneralMemberUniqueIdentifier, ILambdaTypeInferredExpressionParameterMember>(current.Key, current.Value);
            }
        }

        event EventHandler<EventArgsR1<ILambdaTypeInferredExpressionParameterMember>> IIntermediateDeclarationDictionary<IGeneralMemberUniqueIdentifier, ILambdaTypeInferredExpressionParameterMember, ILambdaTypeInferredExpressionParameterMember>.ItemAdded
        {
            add { _itemAdded += value; }
            remove { _itemAdded -= value; }
        }

        event EventHandler<EventArgsR1<ILambdaTypeInferredExpressionParameterMember>> IIntermediateDeclarationDictionary<IGeneralMemberUniqueIdentifier, ILambdaTypeInferredExpressionParameterMember, ILambdaTypeInferredExpressionParameterMember>.ItemRemoved
        {
            add { _itemRemoved += value; }
            remove { _itemRemoved -= value; }
        }

        KeyValuePair<IGeneralMemberUniqueIdentifier, ILambdaTypeInferredExpressionParameterMember> Utilities.Collections.IControlledCollection<KeyValuePair<IGeneralMemberUniqueIdentifier, ILambdaTypeInferredExpressionParameterMember>>.this[int index]
        {
            get {
                if (index < 0 || index >= this.Count)
                    throw new ArgumentOutOfRangeException("index");
                var current = this[index];
                return new KeyValuePair<IGeneralMemberUniqueIdentifier, ILambdaTypeInferredExpressionParameterMember>(current.Key, current.Value);
            }
        }


        IControlledCollection<ILambdaTypeInferredExpressionParameterMember> IControlledDictionary<IGeneralMemberUniqueIdentifier, ILambdaTypeInferredExpressionParameterMember>.Values
        {
            get { throw new NotImplementedException(); }
        }

        ILambdaTypeInferredExpressionParameterMember IControlledDictionary<IGeneralMemberUniqueIdentifier, ILambdaTypeInferredExpressionParameterMember>.this[IGeneralMemberUniqueIdentifier key]
        {
            get { throw new NotImplementedException(); }
        }

        bool IControlledDictionary<IGeneralMemberUniqueIdentifier, ILambdaTypeInferredExpressionParameterMember>.TryGetValue(IGeneralMemberUniqueIdentifier key, out ILambdaTypeInferredExpressionParameterMember value)
        {
            throw new NotImplementedException();
        }
        bool IControlledCollection<KeyValuePair<IGeneralMemberUniqueIdentifier, ILambdaTypeInferredExpressionParameterMember>>.Contains(KeyValuePair<IGeneralMemberUniqueIdentifier, ILambdaTypeInferredExpressionParameterMember> item)
        {
            throw new NotImplementedException();
        }

        void IControlledCollection<KeyValuePair<IGeneralMemberUniqueIdentifier, ILambdaTypeInferredExpressionParameterMember>>.CopyTo(KeyValuePair<IGeneralMemberUniqueIdentifier, ILambdaTypeInferredExpressionParameterMember>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        KeyValuePair<IGeneralMemberUniqueIdentifier, ILambdaTypeInferredExpressionParameterMember>[] IControlledCollection<KeyValuePair<IGeneralMemberUniqueIdentifier, ILambdaTypeInferredExpressionParameterMember>>.ToArray()
        {
            throw new NotImplementedException();
        }

        int IControlledCollection<KeyValuePair<IGeneralMemberUniqueIdentifier, ILambdaTypeInferredExpressionParameterMember>>.IndexOf(KeyValuePair<IGeneralMemberUniqueIdentifier, ILambdaTypeInferredExpressionParameterMember> element)
        {
            throw new NotImplementedException();
        }

        IEnumerator<KeyValuePair<IGeneralMemberUniqueIdentifier, ILambdaTypeInferredExpressionParameterMember>> IEnumerable<KeyValuePair<IGeneralMemberUniqueIdentifier, ILambdaTypeInferredExpressionParameterMember>>.GetEnumerator()
        {
            throw new NotImplementedException();
        }


    }
}
