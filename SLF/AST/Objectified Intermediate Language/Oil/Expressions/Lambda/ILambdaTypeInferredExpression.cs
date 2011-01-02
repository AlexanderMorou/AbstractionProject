using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil.Members;
/*---------------------------------------------------------------------\
| Copyright © 2011 Allen Copeland Jr.                                  |
|----------------------------------------------------------------------|
| The Abstraction Project's code is provided under a contract-release  |
| basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
\-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions.Lambda
{
    /// <summary>
    /// Defines properties and methods for working with a lambda expression
    /// where the types of the parameters are inferred vs. explicitly
    /// stated.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Parameters from type inferred lambda expressions cannot have 
    /// explicit directional coercion on the parameters (Out, or Ref).
    /// </para>
    /// <para>
    /// This is due to the directional coercion being inferred
    /// from a mixture of the actual parameter type and the
    /// custom meta-data attributes applied.
    /// </para>
    /// </remarks>
    public interface ILambdaTypeInferredExpression :
        ILambdaExpression,
        IIntermediateMemberParent
    {
        /// <summary>
        /// Returns a dictionary of the parameters contained by the
        /// <see cref="ILambdaTypeInferredExpression"/>.
        /// </summary>
        ILambdaTypeInferredExpressionParameterMemberDictionary Parameters { get; }
    }
}
