using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    /// <summary>
    /// Represents errors that occur when trying to 
    /// translate/understand an expression and the 
    /// logic or values of the expression are invalid.
    /// </summary>
    public class ExpressionException :
        Exception
    {
        /// <summary>
        /// Data member for <see cref="OffendingExpression"/>.
        /// </summary>
        private IExpression offendingExpression;

        /// <summary>
        /// Initializes a new <see cref="ExpressionException"/> with the <paramref name="offendingExpression"/>
        /// that caused the <see cref="ExpressionException"/>.
        /// </summary>
        /// <remarks><see cref="Exception.Message"/> defaults to: "Expression exception".</remarks>
        /// <param name="offendingExpression">The <see cref="IExpression"/> that caused the 
        /// <see cref="ExpressionException"/>.</param>
        public ExpressionException(IExpression offendingExpression)
            : base("Expression exception")
        {
            this.offendingExpression = offendingExpression;
        }
        /// <summary>
        /// Initializes a new <see cref="ExpressionException"/> with the specified error
        /// <paramref name="message"/> and <paramref name="offendingExpression"/>.
        /// </summary>
        /// <param name="message">The <see cref="System.String"/> describing the error.</param>
        /// <param name="offendingExpression">The <see cref="IExpression"/> that caused the 
        /// <see cref="ExpressionException"/>.</param>
        public ExpressionException(string message, IExpression offendingExpression)
            : base(message)
        {
            this.offendingExpression = offendingExpression;
        }
        /// <summary>
        /// Initializes a new <see cref="ExpressionException"/> with the serialization <paramref name="info"/>
        /// and <paramref name="context"/>
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context">The <see cref="StreamingContext"/> that contains contextual information 
        /// about the source or destination.</param>
        protected ExpressionException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }


        /// <summary>
        /// Initializes a new <see cref="ExpressionException"/> with the specified error
        /// <paramref name="message"/> and <paramref name="innerException"/>.
        /// </summary>
        /// <param name="message">The <see cref="System.String"/> describing the error.</param>
        /// <param name="innerException">The <see cref="Exception"/> that is the cause of the current 
        /// <see cref="ExpressionException"/>, or a null reference if none is specified.</param>
        /// <param name="offendingExpression">The <see cref="IExpression"/> that caused the 
        /// <see cref="ExpressionException"/>.</param>
        public ExpressionException(string message, Exception innerException, IExpression offendingExpression)
            : base(message, innerException)
        {
            this.offendingExpression = offendingExpression;
        }

        /// <summary>
        /// Returns the <see cref="IExpression"/> that caused the <see cref="ExpressionException"/>.
        /// </summary>
        public IExpression OffendingExpression
        {
            get
            {
                return this.offendingExpression;
            }
        }
    }
}
