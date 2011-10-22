using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Members
{
    /// <summary>
    /// Defines generic properties and methods for working with the parent of a series
    /// of method signatures.
    /// </summary>
    /// <typeparam name="TSignature">The type of signature in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateSignature">The type of signature in the intermediate
    /// abstract syntax tree.</typeparam>
    /// <typeparam name="TSignatureParent">The type of parent which contains the 
    /// method signatures in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateSignatureParent">The type of parent
    /// which contains the method signatures in the intermediate
    /// abstract syntax tree.</typeparam>
    /// <remarks>Different from <see cref="IIntermediateMethodSignatureParent{TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TSignatureParent, TIntermediateSignatureParent}"/>
    /// in that this variant is a parent which contains only the signatures of the methods,
    /// no executable code basis is established in these signatures.</remarks>
    public interface IIntermediateMethodSignatureParent<TSignature, TIntermediateSignature, TSignatureParent, TIntermediateSignatureParent> :
        IIntermediateMethodSignatureParent<IMethodSignatureParameterMember<TSignature, TSignatureParent>,IIntermediateMethodSignatureParameterMember<TSignature, TIntermediateSignature, TSignatureParent, TIntermediateSignatureParent>, TSignature, TIntermediateSignature, TSignatureParent, TIntermediateSignatureParent>,
        IMethodSignatureParent<TSignature, TSignatureParent>
        where TSignature :
            IMethodSignatureMember<TSignature, TSignatureParent>
        where TIntermediateSignature :
            TSignature,
            IIntermediateMethodSignatureMember<TSignature, TIntermediateSignature, TSignatureParent, TIntermediateSignatureParent>
        where TSignatureParent :
            IMethodSignatureParent<TSignature, TSignatureParent>
        where TIntermediateSignatureParent :
            TSignatureParent,
            IIntermediateMethodSignatureParent<TSignature, TIntermediateSignature, TSignatureParent, TIntermediateSignatureParent>
    {
        /// <summary>
        /// Returns the <see cref="IIntermediateMethodSignatureMemberDictionary{TSignature, TIntermediateSignature, TSignatureParent, TIntermediateSignatureParent}"/> that 
        /// contains the <typeparamref name="TIntermediateSignature"/> instances
        /// within the <typeparamref name="TIntermediateSignatureParent"/>.
        /// </summary>
        new IIntermediateMethodSignatureMemberDictionary<TSignature, TIntermediateSignature, TSignatureParent, TIntermediateSignatureParent> Methods { get; }
    }

    /// <summary>
    /// Defines generic properties and methods for working with the parent of a series
    /// of method signatures.
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
    public interface IIntermediateMethodSignatureParent<TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TSignatureParent, TIntermediateSignatureParent> :
        IIntermediateSignatureParent<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter, TSignatureParent, TIntermediateSignatureParent>,
        IIntermediateMethodSignatureParent
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
            ISignatureParent<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TSignatureParameter, TSignatureParent>
        where TIntermediateSignatureParent :
            TSignatureParent,
            IIntermediateMethodSignatureParent<TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TSignatureParent, TIntermediateSignatureParent>
    {
        /// <summary>
        /// Returns the <see cref="IIntermediateMethodSignatureMemberDictionary{TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TSignatureParent, TIntermediateSignatureParent}"/> that 
        /// contains the <typeparamref name="TIntermediateSignature"/> instances
        /// within the <typeparamref name="TIntermediateSignatureParent"/>.
        /// </summary>
        new IIntermediateMethodSignatureMemberDictionary<TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TSignatureParent, TIntermediateSignatureParent> Methods { get; }
    }

    /// <summary>
    /// Defines properties and methods for working with the parent of a series
    /// of method signatures.
    /// </summary>
    public interface IIntermediateMethodSignatureParent :
        IIntermediateSignatureParent,
        IMethodSignatureParent
    {
        /// <summary>
        /// Returns the <see cref="IIntermediateMethodSignatureMemberDictionary"/> that contain the <see cref="IIntermediateMethodSignatureMember"/> instances
        /// contained within the <see cref="IIntermediateMethodSignatureParent"/>.
        /// </summary>
        new IIntermediateMethodSignatureMemberDictionary Methods { get; }
        
    }
}
