using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions.Abstract
{
    public static class ExpressionAbstractions
    {
        /// <summary>
        /// Obtains an <see cref="ISymbolExpression"/> of the given <paramref name="symbol"/>.
        /// </summary>
        /// <param name="symbol">The <see cref="String"/> that represents
        /// the symbol expression.</param>
        /// <returns>A new <see cref="ISymbolExpression"/> relative to the
        /// <paramref name="symbol"/> provided.</returns>
        public static ISymbolExpression GetSymbol(this string symbol)
        {
            return new SymbolExpression(symbol);
        }
        /// <summary>
        /// Obtains a <see cref="ISymbolExpression"/> for the <paramref name="symbol"/>
        /// provided concatinating it with the <paramref name="subSymbol"/>.
        /// </summary>
        /// <param name="symbol">The <see cref="System.String"/>
        /// denoting the initial symbol.</param>
        /// <param name="subSymbol">The <see cref="System.String"/></param>
        /// <returns>A new <see cref="ISymbolExpression"/> with 
        /// <paramref name="subSymbol"/> concatinated to <paramref name="symbol"/>
        /// as a new, larger, <see cref="ISymbolExpression"/> that 
        /// references the <paramref name="symbol"/> provided.</returns>
        public static ISymbolExpression SubSymbol(this string symbol, string subSymbol)
        {
            return new SymbolExpression(symbol).SubSymbol(subSymbol);
        }

        /// <summary>
        /// Obtains a <see cref="ISymbolExpression"/> for the <paramref name="symbol"/>
        /// provided concatinating it with the <paramref name="subSymbol"/>.
        /// </summary>
        /// <param name="symbol">A <see cref="ISymbolExpression"/> that acts as the root
        /// of the resulted <see cref="ISymbolExpression"/>.</param>
        /// <param name="subSymbol">The <see cref="System.String"/></param>
        /// <returns>A new <see cref="ISymbolExpression"/> with 
        /// <paramref name="subSymbol"/> concatinated to <paramref name="symbol"/>
        /// as a new, larger, <see cref="ISymbolExpression"/> that 
        /// references the <paramref name="symbol"/> provided.</returns>
        public static ISymbolExpression SubSymbol(this ISymbolExpression symbol, string subSymbol)
        {
            return new SymbolExpression(subSymbol, symbol);
        }

        public static IMethodInvokeExpression Invoke(this ISymbolExpression symbol, IMalleableExpressionCollection args)
        {
            return new MethodInvokeExpression(new MethodReferenceStub(symbol.Source, symbol.Symbol), args);
        }

        public static IMethodInvokeExpression Invoke(this ISymbolExpression symbol, params IExpression[] args)
        {
            return new MethodInvokeExpression(new MethodReferenceStub(symbol.Source, symbol.Symbol), args);
        }

        public static IMethodInvokeExpression Call(this string symbol, IMalleableExpressionCollection args)
        {
            return new MethodInvokeExpression(new MethodReferenceStub(null, symbol), args);
        }

        public static IMethodInvokeExpression Call(this string symbol, params IExpression[] args)
        {
            return new MethodInvokeExpression(new MethodReferenceStub(null, symbol), args);
        }

        public static IPropertyReferenceExpression Prop(this string symbol, string prop)
        {
            return symbol.GetSymbol().GetProperty(prop);
        }

        public static IIndexerReferenceExpression This(this string symbol, params IExpression[] args)
        {
            return symbol.This((IEnumerable<IExpression>)args);
        }

        public static IIndexerReferenceExpression This(this string symbol, IEnumerable<IExpression> args)
        {
            return symbol.GetSymbol().GetIndexer(args.ToArray());
        }

        public static IIndexerReferenceExpression This(this string symbol, string indexerName, params IExpression[] args)
        {
            return symbol.GetSymbol().GetIndexer(indexerName, args);
        }

        public static IIndexerReferenceExpression This(this string symbol, string indexerName, IMalleableExpressionCollection args)
        {
            return symbol.GetIndexer(indexerName, args.ToArray());
        }

        public static IIndexerReferenceExpression This(this string symbol, IndexerReferenceType indexerType, params IExpression[] args)
        {
            return symbol.This(indexerType, args.ToCollection());
        }

        public static IIndexerReferenceExpression This(this string symbol, IndexerReferenceType indexerType, IMalleableExpressionCollection args)
        {
            var target = symbol.This(args) as IUnboundIndexerReferenceExpression;
            target.IndexerType = indexerType;
            return target;
        }

        public static IIndexerReferenceExpression This(this string symbol, string indexerName, IndexerReferenceType indexerType, params IExpression[] args)
        {
            return symbol.This(indexerName, indexerType, args.ToCollection());
        }

        public static IIndexerReferenceExpression This(this string symbol, string indexerName, IndexerReferenceType indexerType, IMalleableExpressionCollection args)
        {
            var target = symbol.This(indexerName, args) as IUnboundIndexerReferenceExpression;
            target.IndexerType = indexerType;
            return target;
        }

        public static IFieldReferenceExpression Field(this string symbol, string field)
        {
            return symbol.GetSymbol().GetField(field);
        }

        public static IMethodPointerReferenceExpression FuncPtr(this string startSymbol)
        {
            return new MethodPointerReferenceExpression(new MethodReferenceStub(startSymbol));
        }
    }
}