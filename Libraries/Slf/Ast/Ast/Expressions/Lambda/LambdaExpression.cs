using System;
using System.Collections.Generic;
using System.Text;
/*---------------------------------------------------------------------\
| Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
|----------------------------------------------------------------------|
| The Abstraction Project's code is provided under a contract-release  |
| basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
\-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Expressions.Lambda
{
    
    public abstract class LambdaExpression :
        ExpressionBase,
        ILambdaExpression
    {
        #region ILambdaExpression Members

        /// <summary>
        /// Returns the <see cref="ILambdaBody"/> that defines 
        /// the <see cref="LambdaExpression"/>.
        /// </summary>
        public ILambdaBody Block { get; protected set; }

        /// <summary>
        /// Returns the kind of lambda expression the
        /// <see cref="LambdaExpression"/> is.
        /// </summary>
        public abstract LambdaExpressionKind LambdaType { get; }

        #endregion

        #region IExpression Members

        /// <summary>
        /// Returns the type of expression the 
        /// <see cref="LambdaExpression"/> is.
        /// </summary>
        public override ExpressionKind Type
        {
            get { return ExpressionKind.LambdaExpression; }
        }

        #endregion

        #region IDelegateReferenceExpression Members

        /// <summary>
        /// Returns the <see cref="DelegateReferenceKind"/> the
        /// <see cref="LambdaExpression"/> is.
        /// </summary>
        /// <remarks>Returns <see cref="DelegateReferenceKind.MethodPointerReference"/>.</remarks>
        public DelegateReferenceKind ReferenceType
        {
            get { return DelegateReferenceKind.MethodPointerReference; }
        }

        #endregion
    }
}
