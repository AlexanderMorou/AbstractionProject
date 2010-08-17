using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    public class InvalidInvertTargetException :
        ExpressionException
    {
        /// <summary>
        /// Initializes a new <see cref="InvalidInvertTargetException"/> with the <paramref name="CSharpoffendingExpression"/>
        /// that caused the <see cref="InvalidInvertTargetException"/>.
        /// </summary>
        /// <remarks><see cref="Exception.Message"/> defaults to: "Expression exception".</remarks>
        /// <param name="CSharpoffendingExpression">The <see cref="IExpression"/> that caused the 
        /// <see cref="InvalidInvertTargetException"/>.</param>
        public InvalidInvertTargetException(ICSharpUnaryOperationExpression offendingExpression)
            : base("Expression exception", offendingExpression)
        {
        }
        /// <summary>
        /// Initializes a new <see cref="InvalidInvertTargetException"/> with the specified error
        /// <paramref name="message"/> and <paramref name="CSharpoffendingExpression"/>.
        /// </summary>
        /// <param name="message">The <see cref="System.String"/> describing the error.</param>
        /// <param name="CSharpoffendingExpression">The <see cref="IExpression"/> that caused the 
        /// <see cref="InvalidInvertTargetException"/>.</param>
        public InvalidInvertTargetException(string message, ICSharpUnaryOperationExpression offendingExpression)
            : base(message, offendingExpression)
        {
        }
        /// <summary>
        /// Initializes a new <see cref="InvalidInvertTargetException"/> with the serialization <paramref name="info"/>
        /// and <paramref name="context"/>
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context">The <see cref="StreamingContext"/> that contains contextual information 
        /// about the source or destination.</param>
        protected InvalidInvertTargetException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }


        /// <summary>
        /// Initializes a new <see cref="InvalidInvertTargetException"/> with the specified error
        /// <paramref name="message"/> and <paramref name="innerException"/>.
        /// </summary>
        /// <param name="message">The <see cref="System.String"/> describing the error.</param>
        /// <param name="innerException">The <see cref="Exception"/> that is the cause of the current 
        /// <see cref="InvalidInvertTargetException"/>, or a null reference if none is specified.</param>
        /// <param name="CSharpoffendingExpression">The <see cref="IExpression"/> that caused the 
        /// <see cref="InvalidInvertTargetException"/>.</param>
        public InvalidInvertTargetException(string message, Exception innerException, ICSharpUnaryOperationExpression offendingExpression)
            : base(message, innerException, offendingExpression)
        {
        }

        public new ICSharpUnaryOperationExpression OffendingExpression
        {
            get
            {
                return (ICSharpUnaryOperationExpression)base.OffendingExpression;
            }
        }
    }
}
