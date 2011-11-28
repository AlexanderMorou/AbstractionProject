using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Ast;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Expressions
{
    public class SymbolExpression :
        ExpressionBase,
        ISymbolExpression
    {
        private string symbol;
        private IMemberParentReferenceExpression source;

        /// <summary>
        /// Creates a new <see cref="SymbolExpression"/>
        /// with the <paramref name="symbol"/> provided.
        /// </summary>
        /// <param name="symbol">The <see cref="System.String"/>
        /// denoting the expression.</param>
        public SymbolExpression(string symbol)
        {
            this.symbol = symbol;
        }

        /// <summary>
        /// Creates a new <see cref="SymbolExpression"/>
        /// with the <paramref name="symbol"/> and its
        /// <paramref name="source"/>.
        /// </summary>
        /// <param name="symbol">The <see cref="System.String"/>
        /// denoting the expression.</param>
        /// <param name="source">The <see cref="IMemberParentReferenceExpression"/>
        /// which leads to the <see cref="SymbolExpression"/>.</param>
        public SymbolExpression(string symbol, IMemberParentReferenceExpression source)
        {
            this.symbol = symbol;
            this.source = source;
        }
        public override ExpressionKind Type
        {
            get { return ExpressionKind.SymbolExpression; }
        }

        #region ISymbolExpression Members

        public IMemberParentReferenceExpression Source
        {
            get { return this.source; }
        }

        public string Symbol
        {
            get
            {
                return this.symbol;
            }
        }

        #endregion

        public override string ToString()
        {
            if (this.Source == null)
                return this.Symbol;
            else
                return string.Format(CultureInfo.CurrentCulture, "{0}.{1}", this.Source, this.Symbol);
        }

        /// <summary>
        /// Creates a symbol fusion with the <see cref="SymbolExpression"/> on the <paramref name="left"/> side
        /// and the <see cref="String"/> on the <paramref name="right"/> side.
        /// </summary>
        /// <param name="left">The <see cref="SymbolExpression"/> on the left to be
        /// fused with the <paramref name="right"/> <see cref="String"/>.</param>
        /// <param name="right">The <see cref="String"/> to fuse with the <paramref name="left"/>
        /// <see cref="SymbolExpression"/>.</param>
        /// <returns>A new <see cref="ExpressionFusionExpression"/> which
        /// fuses the left and right elements in a left->right member access fusion.</returns>
        public static IExpressionFusionExpression operator +(SymbolExpression left, string right)
        {
            return left.Fuse(right);
        }

        public override void Visit(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
