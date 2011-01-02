using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Members
{
    /// <summary>
    /// Defines generic properties and methods for working 
    /// with the parameter of the signature of a method.
    /// </summary>
    /// <typeparam name="TSignature">The type of signature in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateSignature">The type of signature in the intermediate
    /// abstract syntax tree.</typeparam>
    /// <typeparam name="TSignatureParent">The type of parent which contains the 
    /// method signatures in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateSignatureParent">The type of parent
    /// which contains the method signatures in the intermediate
    /// abstract syntax tree.</typeparam>
    /// <remarks>Different from <see cref="IIntermediateMethodSignatureParameterMember{TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TSignatureParent, TIntermediateSignatureParent}"/>
    /// in that this variant has a parent which contains only the signatures of the methods,
    /// no executable code basis is established in these signatures.</remarks>
    public interface IIntermediateMethodSignatureParameterMember<TSignature, TIntermediateSignature, TSignatureParent, TIntermediateSignatureParent> :
        IIntermediateMethodSignatureParameterMember<IMethodSignatureParameterMember<TSignature, TSignatureParent>, IIntermediateMethodSignatureParameterMember<TSignature, TIntermediateSignature, TSignatureParent, TIntermediateSignatureParent>, TSignature, TIntermediateSignature, TSignatureParent, TIntermediateSignatureParent>,
        IMethodSignatureParameterMember<TSignature, TSignatureParent>
        where TSignature :
            IMethodSignatureMember<TSignature, TSignatureParent>
        where TIntermediateSignature :
            TSignature,
            IIntermediateMethodSignatureMember<TSignature, TIntermediateSignature, TSignatureParent, TIntermediateSignatureParent>
        where TSignatureParent :
            IMethodSignatureParent<TSignature, TSignatureParent>
        where TIntermediateSignatureParent :
            IIntermediateMethodSignatureParent<TSignature, TIntermediateSignature, TSignatureParent, TIntermediateSignatureParent>,
            TSignatureParent
    {
    }
    /// <summary>
    /// Defines generic properties and methods for working 
    /// with the parameter of the signature of a method.
    /// </summary>
    /// <typeparam name="TSignatureParameter">The type of parameter in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateSignatureParameter">The type of parameter in the 
    /// intermediate abstract syntax tree.</typeparam>
    /// <typeparam name="TSignature">The type of method signature in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateSignature">The type of method signature in the 
    /// intermediate abstract syntax tree.</typeparam>
    /// <typeparam name="TSignatureParent">The type of parent that contains the 
    /// method signature members in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateSignatureParent">The type of parent that
    /// contains the method signature members in the intermediate
    /// abstract syntax tree.</typeparam>
    public interface IIntermediateMethodSignatureParameterMember<TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TSignatureParent, TIntermediateSignatureParent> :
        IIntermediateSignatureParameterMember<TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter, TSignatureParent, TIntermediateSignatureParent>,
        IMethodSignatureParameterMember<TSignatureParameter,TSignature, TSignatureParent>
        where TSignatureParameter :
            IMethodSignatureParameterMember<TSignatureParameter, TSignature, TSignatureParent>
        where TIntermediateSignatureParameter :
            TSignatureParameter, 
            IIntermediateMethodSignatureParameterMember<TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TSignatureParent, TIntermediateSignatureParent>
        where TSignature :
            IMethodSignatureMember<TSignatureParameter, TSignature, TSignatureParent>
        where TIntermediateSignature :
            TSignature,
            IIntermediateMethodSignatureMember<TSignatureParameter,TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TSignatureParent, TIntermediateSignatureParent>
        where TSignatureParent :
            ISignatureParent<TSignature, TSignatureParameter, TSignatureParent>
        where TIntermediateSignatureParent :
            TSignatureParent,
            IIntermediateSignatureParent<TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter, TSignatureParent, TIntermediateSignatureParent>
    {

    }
}
