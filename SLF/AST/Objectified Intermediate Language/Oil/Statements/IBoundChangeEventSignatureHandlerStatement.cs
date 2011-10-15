using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Oil.Expressions;

namespace AllenCopeland.Abstraction.Slf.Oil.Statements
{
    /// <summary>
    /// Defines generic properties and methods for working with a bound
    /// change event signature handler statement.
    /// </summary>
    /// <typeparam name="TEvent">The type of event as it exists in the
    /// abstract type system.</typeparam>
    /// <typeparam name="TEventParameter">The type of parameter
    /// contained within the events.</typeparam>
    /// <typeparam name="TEventParent">The type which owns the properties
    /// in the abstract type system.</typeparam>
    /// <typeparam name="TSignatureParameter">The type of parameter used in the <typeparamref name="TSignature"/>.</typeparam>
    /// <typeparam name="TSignature">The type of signature used as a parent of <typeparamref name="TSignatureParameter"/> instances.</typeparam>
    /// <typeparam name="TSignatureParent">The parent that contains the <typeparamref name="TSignature"/> 
    /// instances.</typeparam>
    public interface IBoundChangeEventSignatureHandlerStatement<TEvent, TEventParameter, TEventParent, TSignatureParameter, TSignature, TSignatureParent> :
        IChangeEventHandlerStatement<IEventReferenceExpression<TEvent, TEventParameter, TEventParent>, IMethodPointerReferenceExpression<TSignatureParameter, TSignature, TSignatureParent>>
        where TEvent :
            IEventSignatureMember<TEvent, TEventParameter, TEventParent>
        where TEventParent :
            IEventSignatureParent<TEvent, TEventParameter, TEventParent>
        where TEventParameter :
            IEventSignatureParameterMember<TEvent, TEventParameter, TEventParent>
        where TSignatureParameter :
            IMethodSignatureParameterMember<TSignatureParameter, TSignature, TSignatureParent>
        where TSignature :
            IMethodSignatureMember<TSignatureParameter, TSignature, TSignatureParent>
        where TSignatureParent :
            ISignatureParent<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TSignatureParameter, TSignatureParent>
    {

    }
}
