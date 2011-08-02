using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Oil;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions.Lambda
{
    /// <summary>
    /// Provides extension methods for lambda expressions and
    /// other helper functions for lambdas.
    /// </summary>
    public static class LambdaHelper
    {
        /// <summary>
        /// Obtains an <see cref="ILambdaSimpleExpression"/> with the 
        /// <paramref name="expression"/> body, and 
        /// <paramref name="paramNames"/> as its parameter
        /// names.
        /// </summary>
        /// <param name="expression">A <see cref="IExpression"/> which
        /// denotes the body of the simple lambda expression.</param>
        /// <param name="paramNames">A series of <see cref="String"/> 
        /// elements that make up the names of the parameters for the
        /// lambda expression.</param>
        /// <returns>A new <see cref="ILambdaSimpleExpression"/> 
        /// instance with the parameters as provided.</returns>
        public static ILambdaSimpleExpression GetSimpleLambda(IExpression expression, params string[] paramNames)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Obtains an <see cref="ILambdaStatementExpression"/> with the 
        /// <paramref name="paramNames"/> as its parameters.
        /// </summary>
        /// <param name="paramNames">A series of <see cref="String"/>
        /// elementsthat make up the names of the parameters for the 
        /// lambda expression.</param>
        /// <returns>A new <see cref="ILambdaStatementExpression"/> 
        /// instance  with the parameters as provided.</returns>
        public static ILambdaStatementExpression GetStatementLambda(params string[] paramNames)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Obtains a <see cref="ILambdaTypedSimpleExpression"/> 
        /// using the <paramref name="params"/> provided to create
        /// its signature.
        /// </summary>
        /// <param name="expression">The <see cref="IExpression"/>
        /// which defines the body of the resulted simple typed
        /// lambda expression.</param>
        /// <param name="params">A series of <see cref="TypedName"/>
        /// instances that relates to the parameter types and names of the
        /// <see cref="ILambdaTypedSimpleExpression"/> to create.
        /// </param>
        /// <returns>An <see cref="ILambdaTypedSimpleExpression"/> using
        /// the <paramref name="params"/> provided to create its 
        /// signature.</returns>
        public static ILambdaTypedSimpleExpression GetSimpleLambda(IExpression expression, params TypedName[] @params)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Obtains a <see cref="ILambdaTypedStatementExpression"/> 
        /// using the <paramref name="params"/> provided to create its 
        /// signature.
        /// </summary>
        /// <param name="params">A series of <see cref="TypedName"/>
        /// instances that relates to the parameter types and names of the
        /// <see cref="ILambdaTypedStatementExpression"/> to create.
        /// </param>
        /// <returns>An <see cref="ILambdaTypedStatementExpression"/> using
        /// the <paramref name="params"/> provided to create its signature.
        /// </returns>
        public static ILambdaTypedStatementExpression GetStatementLambda(params TypedName[] @params)
        {
            throw new NotImplementedException();
        }
    }
}
