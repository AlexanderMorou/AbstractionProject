using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Oil.Expressions.Lambda;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
{
    /// <summary>
    /// Defines properties and methods for dealing with a generalized lambda 
    /// signature that may or may not have data explicitly stated and requires 
    /// a level of inference.
    /// </summary>
    public interface ILambdaSignatureType
    {
        /// <summary>
        /// Returns/sets the return <see cref="IType"/> of the lambda expression.
        /// </summary>
        IType ReturnType { get; set; }
        /// <summary>
        /// Returns the malleable <see cref="ITypeCollection"/>
        /// of the types associated to the signature of the 
        /// lambda expression.
        /// </summary>
        ITypeCollection ParameterTypes { get; }
        /// <summary>
        /// Returns the means to which the parameter types are directed.
        /// </summary>
        IControlledStateCollection<ParameterDirection> ParameterDirections { get; }
        /// <summary>
        /// Returns the <see cref="ILambdaExpression"/>
        /// used to create the current 
        /// <see cref="ILambdaSignatureType"/>.
        /// </summary>
        /// <remarks>
        /// Used to profile the lambda expression
        /// to discern return-type to aid in type-resolution.
        /// </remarks>
        ILambdaExpression Source { get; }
    }
}