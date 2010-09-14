using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    /// <summary>
    /// Provides a root implementation of <see cref="IExpressionCollection"/>
    /// </summary>
    public class ExpressionCollection<T> :
        ControlledStateCollection<T>,
        IExpressionCollection<T>
        where T :
            IExpression
    {
        public static readonly IExpression[] EmptyExpressionArray = new IExpression[0];
        /// <summary>
        /// Creates a new <see cref="ExpressionCollection"/> initialized
        /// to a default state.
        /// </summary>
        public ExpressionCollection()
            : base()
        {

        }

        /// <summary>
        /// Creates a new <see cref="ExpressionCollection"/> 
        /// with the <paramref name="expressions"/> provided.
        /// </summary>
        /// <param name="expressions">The series of <see cref="IExpression"/>
        /// elements to initially contain.</param>
        public ExpressionCollection(params T[] expressions)
            : base(new List<T>(expressions))
        {
        }

        public ExpressionCollection(IEnumerable<T> expressions)
            : base(new List<T>(expressions))
        {
        }
    }

    public class ExpressionCollection :
        ExpressionCollection<IExpression>,
        IExpressionCollection
    {
        /// <summary>
        /// Creates a new <see cref="ExpressionCollection"/> initialized
        /// to a default state.
        /// </summary>
        public ExpressionCollection()
            : base()
        {

        }

        /// <summary>
        /// Creates a new <see cref="ExpressionCollection"/> 
        /// with the <paramref name="expressions"/> provided.
        /// </summary>
        /// <param name="expressions">The series of <see cref="IExpression"/>
        /// elements to initially contain.</param>
        public ExpressionCollection(params IExpression[] expressions)
            : base(expressions)
        {
        }

        public ExpressionCollection(IEnumerable<IExpression> expressions)
            : base(expressions)
        {
        }
    }
}
