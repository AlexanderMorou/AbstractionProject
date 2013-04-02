using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Expressions
{
    public class MalleableExpressionCollection :
        MalleableExpressionCollection<IExpression>,
        IMalleableExpressionCollection
    {
        public MalleableExpressionCollection()
            : base()
        {
        }

        /// <summary>
        /// Creates a new <see cref="MalleableExpressionCollection"/> with the <paramref name="expressions"/>
        /// provided.
        /// </summary>
        /// <param name="expressions">An array of <see cref="IExpression"/> instances
        /// which are to be inserted into the <see cref="MalleableExpressionCollection"/>.</param>
        public MalleableExpressionCollection(params IExpression[] expressions)
            : base(expressions)
        {
        }

        public MalleableExpressionCollection(IEnumerable<IExpression> expressions)
            : base(expressions)
        {
        }
    }

    public class MalleableExpressionCollection<T> :
        ExpressionCollection<T>,
        IMalleableExpressionCollection<T>
        where T :
            IExpression
    {
        public MalleableExpressionCollection()
            : base()
        {
        }

        /// <summary>
        /// Creates a new <see cref="MalleableExpressionCollection{T}"/> with the <paramref name="expressions"/>
        /// provided.
        /// </summary>
        /// <param name="expressions">An array of <typeparamref name="T"/> instances
        /// which are to be inserted into the <see cref="MalleableExpressionCollection{T}"/>.</param>
        public MalleableExpressionCollection(params T[] expressions)
            : base(expressions)
        {
        }

        public MalleableExpressionCollection(IEnumerable<T> expressions)
            : base(expressions)
        {
        }

        #region IMalleableExpressionCollection Members


        /// <summary>
        /// Adds a <see cref="IExpression"/> to the <see cref="IMalleableExpressionCollection"/>.
        /// </summary>
        /// <param name="expression">The <see cref="IExpression"/> 
        /// to add to the <see cref="IMalleableExpressionCollection"/>.</param>
        public void Add(T expression)
        {
            this.AddImpl(expression);
        }


        /// <summary>
        /// Inserts the <paramref name="expression"/> provided
        /// at the <paramref name="index"/> specified into the <see cref="IMalleableExpressionCollection"/>.
        /// </summary>
        /// <param name="expression">The <see cref="IExpression"/> to insert.</param>
        /// <param name="index">The zero-based <see cref="Int32"/> index to place the <paramref name="expression"/>.</param>
        public void Insert(T expression, int index)
        {
            this.InsertItem(index, expression);
        }

        public void RemoveAt(int index)
        {
            int i = 0;
            foreach (var item in this)
                if (i == index)
                {
                    this.Remove(item);
                    break;
                }
                else
                    i++;
        }

        public void Remove(T expression)
        {
            this.baseList.Remove(expression);
        }

        public void Clear()
        {
            this.baseList.Clear();
        }

        public new T this[int index]
        {
            get
            {
                return base[index];
            }
            set
            {
                if (index < 0 || index >= this.Count)
                    throw ThrowHelper.ObtainArgumentOutOfRangeException(ArgumentWithException.index);
                var item = this[index];
                if (item.Equals(value))
                    return;
                base.InsertItem(index, value);
                this.RemoveAt(index + 1);
            }
        }

        #endregion

    }
}
