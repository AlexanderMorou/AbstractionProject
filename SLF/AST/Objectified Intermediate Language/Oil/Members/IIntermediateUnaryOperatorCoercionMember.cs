using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Oil.Statements;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Oil.Members
{
    /// <summary>
    /// Defines generic properties and methods for a member which coerces the 
    /// interpretation of the containing type with regards to unary operator 
    /// expressions.
    /// </summary>
    /// <typeparam name="TCoercionParent">The type of parent that contains the 
    /// unary operation coercion member in the abstract type system.</typeparam>
    /// <typeparam name="TInterCoercionParent">The type of parent that contains 
    /// the unary operation coercion member in the intermediate abstract syntax tree.</typeparam>
    public interface IIntermediateUnaryOperatorCoercionMember<TCoercionParentIdentifier, TCoercionParent, TInterCoercionParent> :
        IIntermediateCoercionMember<IUnaryOperatorUniqueIdentifier, TCoercionParentIdentifier, IUnaryOperatorCoercionMember<TCoercionParentIdentifier, TCoercionParent>, IIntermediateUnaryOperatorCoercionMember<TCoercionParentIdentifier, TCoercionParent, TInterCoercionParent>, TCoercionParent, TInterCoercionParent>,
        IIntermediateUnaryOperatorCoercionMember,
        IUnaryOperatorCoercionMember<TCoercionParentIdentifier, TCoercionParent>
        where TCoercionParentIdentifier :
            ITypeUniqueIdentifier<TCoercionParentIdentifier>
        where TCoercionParent :
            ICoercibleType<IUnaryOperatorUniqueIdentifier, TCoercionParentIdentifier, IUnaryOperatorCoercionMember<TCoercionParentIdentifier, TCoercionParent>, TCoercionParent>
        where TInterCoercionParent :
            IIntermediateCoercibleType<IUnaryOperatorUniqueIdentifier, TCoercionParentIdentifier, IUnaryOperatorCoercionMember<TCoercionParentIdentifier, TCoercionParent>, IIntermediateUnaryOperatorCoercionMember<TCoercionParentIdentifier, TCoercionParent, TInterCoercionParent>, TCoercionParent, TInterCoercionParent>,
            TCoercionParent
    {
    }
    /// <summary>
    /// Defines properties and methods for a member which 
    /// coerces the interpretation of the containing type 
    /// with regards to unary operator expressions.
    /// </summary>
    public interface IIntermediateUnaryOperatorCoercionMember :
        IIntermediateCoercionMember,
        ITopBlockStatement,
        IUnaryOperatorCoercionMember
    {
        /// <summary>
        /// Returns/sets operator coerced.
        /// </summary>
        new CoercibleUnaryOperators Operator { get; set; }
        /// <summary>
        /// Returns/sets the <see cref="IType"/> which results from the
        /// unary operation.
        /// </summary>
        new IType ResultedType { get; set; }
    }
}
