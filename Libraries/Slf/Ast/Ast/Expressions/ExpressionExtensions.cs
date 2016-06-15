using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.Abstract;
using AllenCopeland.Abstraction.Slf._Internal.Ast;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
//using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Utilities.Arrays;
using System.Collections;
using AllenCopeland.Abstraction.Slf.Ast.Statements;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Ast.Expressions
{
    public static partial class ExpressionExtensions
    {
        private static IDictionary<string, Symbol> stringSymbolCache = new Dictionary<string,Symbol>();
        #region Primitive Expression Conversions
        public static PrimitiveRepresentationExpression<string> ToStringExpression(this IExpression target) { return new PrimitiveRepresentationExpression<string>(target); }

        public static PrimitiveRepresentationExpression<T> ToPrimitive<T>(this IExpression target)
            where T :
                struct
        {
            var typeOfT = typeof(T);
            if (typeOfT.IsPrimitive || typeOfT == typeof(decimal))
                return new PrimitiveRepresentationExpression<T>(target);
            throw new InvalidOperationException("Provided type is not a primitive type");
        }
        #endregion

        #region Primitive Conversion
        /// <summary>
        /// Obtains the <see cref="PrimitiveExpression{T}"/> for the <paramref name="value"/> provided.
        /// </summary>
        /// <param name="value">The <see cref="System.Int32"/> value which is to be translated into a 
        /// <see cref="PrimitiveExpression{T}"/>.</param>
        /// <returns>A new <see cref="PrimitiveExpression{T}"/> containing the <paramref name="value"/> provided.</returns>
        public static PrimitiveExpression<int> ToPrimitive(this int value)
        {
            return new PrimitiveExpression<int>(value);
        }

        /// <summary>
        /// Obtains the <see cref="PrimitiveExpression{T}"/> for the <paramref name="value"/> provided.
        /// </summary>
        /// <param name="value">The <see cref="System.Int64"/> value which is to be translated into a 
        /// <see cref="PrimitiveExpression{T}"/>.</param>
        /// <returns>A new <see cref="PrimitiveExpression{T}"/> containing the <paramref name="value"/> provided.</returns>
        public static PrimitiveExpression<long> ToPrimitive(this long value)
        {
            return new PrimitiveExpression<long>(value);
        }

        /// <summary>
        /// Obtains the <see cref="PrimitiveExpression{T}"/> for the <paramref name="value"/> provided.
        /// </summary>
        /// <param name="value">The <see cref="System.Byte"/> value which is to be translated into a 
        /// <see cref="PrimitiveExpression{T}"/>.</param>
        /// <returns>A new <see cref="PrimitiveExpression{T}"/> containing the <paramref name="value"/> provided.</returns>
        public static PrimitiveExpression<byte> ToPrimitive(this byte value)
        {
            return new PrimitiveExpression<byte>(value);
        }

        /// <summary>
        /// Obtains the <see cref="PrimitiveExpression{T}"/> for the <paramref name="value"/> provided.
        /// </summary>
        /// <param name="value">The <see cref="System.SByte"/> value which is to be translated into a 
        /// <see cref="PrimitiveExpression{T}"/>.</param>
        /// <returns>A new <see cref="PrimitiveExpression{T}"/> containing the <paramref name="value"/> provided.</returns>
        public static PrimitiveExpression<sbyte> ToPrimitive(this sbyte value)
        {
            return new PrimitiveExpression<sbyte>(value);
        }

        /// <summary>
        /// Obtains the <see cref="PrimitiveExpression{T}"/> for the <paramref name="value"/> provided.
        /// </summary>
        /// <param name="value">The <see cref="System.Boolean"/> value which is to be translated into a 
        /// <see cref="IPrimitiveExpression{T}"/>.</param>
        /// <returns>A new <see cref="PrimitiveExpression{T}"/> containing the <paramref name="value"/> provided.</returns>
        public static PrimitiveExpression<bool> ToPrimitive(this bool value)
        {
            return new PrimitiveExpression<bool>(value);
        }

        /// <summary>
        /// Obtains the <see cref="PrimitiveExpression{T}"/> for the <paramref name="value"/> provided.
        /// </summary>
        /// <param name="value">The <see cref="System.Char"/> value which is to be translated into a 
        /// <see cref="PrimitiveExpression{T}"/>.</param>
        /// <returns>A new <see cref="PrimitiveExpression{T}"/> containing the <paramref name="value"/> provided.</returns>
        public static PrimitiveExpression<char> ToPrimitive(this char value)
        {
            return new PrimitiveExpression<char>(value);
        }

        /// <summary>
        /// Obtains the <see cref="PrimitiveExpression{T}"/> for the <paramref name="value"/> provided.
        /// </summary>
        /// <param name="value">The <see cref="System.UInt32"/> value which is to be translated into a 
        /// <see cref="PrimitiveExpression{T}"/>.</param>
        /// <returns>A new <see cref="PrimitiveExpression{T}"/> containing the <paramref name="value"/> provided.</returns>
        [CLSCompliant(false)]
        public static PrimitiveExpression<uint> ToPrimitive(this uint value)
        {
            return new PrimitiveExpression<uint>(value);
        }

        /// <summary>
        /// Obtains the <see cref="PrimitiveExpression{T}"/> for the <paramref name="value"/> provided.
        /// </summary>
        /// <param name="value">The <see cref="System.Decimal"/> value which is to be translated into a 
        /// <see cref="PrimitiveExpression{T}"/>.</param>
        /// <returns>A new <see cref="PrimitiveExpression{T}"/> containing the <paramref name="value"/> provided.</returns>
        public static PrimitiveExpression<decimal> ToPrimitive(this decimal value)
        {
            return new PrimitiveExpression<decimal>(value);
        }

        /// <summary>
        /// Obtains the <see cref="PrimitiveExpression{T}"/> for the <paramref name="value"/> provided.
        /// </summary>
        /// <param name="value">The <see cref="System.UInt64"/> value which is to be translated into a 
        /// <see cref="PrimitiveExpression{T}"/>.</param>
        /// <returns>A new <see cref="PrimitiveExpression{T}"/> containing the <paramref name="value"/> provided.</returns>
        [CLSCompliant(false)]
        public static PrimitiveExpression<ulong> ToPrimitive(this ulong value)
        {
            return new PrimitiveExpression<ulong>(value);
        }

        /// <summary>
        /// Obtains the <see cref="PrimitiveExpression{T}"/> for the <paramref name="value"/> provided.
        /// </summary>
        /// <param name="value">The <see cref="System.Int16"/> value which is to be translated into a 
        /// <see cref="PrimitiveExpression{T}"/>.</param>
        /// <returns>A new <see cref="PrimitiveExpression{T}"/> containing the <paramref name="value"/> provided.</returns>
        public static PrimitiveExpression<short> ToPrimitive(this short value)
        {
            return new PrimitiveExpression<short>(value);
        }

        /// <summary>
        /// Obtains the <see cref="PrimitiveExpression{T}"/> for the <paramref name="value"/> provided.
        /// </summary>
        /// <param name="value">The <see cref="System.UInt16"/> value which is to be translated into a 
        /// <see cref="PrimitiveExpression{T}"/>.</param>
        /// <returns>A new <see cref="PrimitiveExpression{T}"/> containing the <paramref name="value"/> provided.</returns>
        [CLSCompliant(false)]
        public static PrimitiveExpression<ushort> ToPrimitive(this ushort value)
        {
            return new PrimitiveExpression<ushort>(value);
        }

        /// <summary>
        /// Obtains the <see cref="PrimitiveExpression{T}"/> for the <paramref name="value"/> provided.
        /// </summary>
        /// <param name="value">The <see cref="System.Single"/> value which is to be translated into a 
        /// <see cref="PrimitiveExpression{T}"/>.</param>
        /// <returns>A new <see cref="PrimitiveExpression{T}"/> containing the <paramref name="value"/> provided.</returns>
        public static PrimitiveExpression<float> ToPrimitive(this float value)
        {
            return new PrimitiveExpression<float>(value);
        }

        /// <summary>
        /// Obtains the <see cref="PrimitiveExpression{T}"/> for the <paramref name="value"/> provided.
        /// </summary>
        /// <param name="value">The <see cref="System.Double"/> value which is to be translated into a 
        /// <see cref="PrimitiveExpression{T}"/>.</param>
        /// <returns>A new <see cref="PrimitiveExpression{T}"/> containing the <paramref name="value"/> provided.</returns>
        public static PrimitiveExpression<double> ToPrimitive(this double value)
        {
            return new PrimitiveExpression<double>(value);
        }

        /// <summary>
        /// Obtains a <see cref="PrimitiveExpression{T}"/> for the <paramref name="value"/> provided.
        /// </summary>
        /// <param name="value">The <see cref="String"/> value which is to be translated into a 
        /// <see cref="PrimitiveExpression{T}"/>.</param>
        /// <returns></returns>
        public static PrimitiveExpression<string> ToPrimitive(this string value)
        {
            return new PrimitiveExpression<string>(value);
        }
        #endregion 

        #region Casts

        /// <summary>
        /// Casts the <paramref name="target"/> <see cref="IExpression"/> 
        /// to the <paramref name="castType"/> provided.
        /// </summary>
        /// <param name="target">The <see cref="IExpression"/> the 
        /// <see cref="TypeCastExpression"/> casts to
        /// <paramref name="castType"/>.</param>
        /// <param name="castType">The <see cref="IType"/> the <see cref="TypeCastExpression"/>
        /// casts the <paramref name="target"/> to.</param>
        /// <returns>A new <see cref="TypeCastExpression"/>.</returns>
        public static TypeCastExpression Cast(this IExpression target, IType castType)
        {
            if (target == null)
                throw new ArgumentNullException("target");
            if (castType == null)
                throw new ArgumentNullException("castType");
            return new TypeCastExpression(castType, target);
        }

        /// <summary>
        /// Casts the <paramref name="target"/> symbol
        /// to the <paramref name="castType"/> provided.
        /// </summary>
        /// <param name="target">The <see cref="String"/> representing a
        /// symbol expression the <see cref="TypeCastExpression"/> casts to
        /// <paramref name="castType"/>.</param>
        /// <param name="castType">The <see cref="IType"/> the <see cref="TypeCastExpression"/>
        /// casts the <paramref name="target"/> to.</param>
        /// <returns>A new <see cref="TypeCastExpression"/>.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="target"/> or 
        /// <paramref name="castType"/> is null</exception>.
        public static TypeCastExpression Cast(this string target, IType castType)
        {
            if (target == null)
                throw new ArgumentNullException("target");
            if (castType == null)
                throw new ArgumentNullException("castType");
            return new TypeCastExpression(castType, target.GetSymbolExpression());
        }

        /// <summary>
        /// Casts the <paramref name="target"/> symbol
        /// to the <paramref name="symbolType"/> provided.
        /// </summary>
        /// <param name="target">The <see cref="String"/> representing a
        /// symbol expression the <see cref="TypeCastExpression"/> casts to
        /// <paramref name="symbolType"/>.</param>
        /// <param name="symbolType">The <see cref="String"/> symbol type
        /// the <see cref="TypeCastExpression"/> casts the 
        /// <paramref name="target"/> to.</param>
        /// <returns>A new <see cref="TypeCastExpression"/>.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="target"/> or <paramref name="symbolType"/> is null</exception>.
        public static TypeCastExpression Cast(this string target, string symbolType)
        {
            if (target == null)
                throw new ArgumentNullException("target");
            if (symbolType == null)
                throw new ArgumentNullException("symbolType");
            return target.Cast(symbolType.GetSymbolType());
        }
        #endregion 

        #region Miscellaneous
        /// <summary>
        /// Obtains a <see cref="IExpressionCollection"/> of the
        /// <paramref name="target"/> <see cref="IExpression"/> 
        /// elements.
        /// </summary>
        /// <param name="target">The <see cref="IExpression"/>
        /// elements for the new <see cref="IExpressionCollection"/>
        /// to contain.</param>
        /// <returns>A new <see cref="IExpressionCollection"/> containing
        /// the elements from <paramref name="target"/>.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="target"/> is null.</exception>
        public static MalleableExpressionCollection ToCollection(this IEnumerable<IExpression> target)
        {
            if (target == null)
                throw new ArgumentNullException("target");
            return new MalleableExpressionCollection(target);
        }
        #endregion

        #region TypeOf Expression
        /// <summary>
        /// Obtains a <see cref="TypeOfExpression"/> for the <paramref name="target"/> provided.
        /// </summary>
        /// <param name="target">The <see cref="IType"/> which needs a typeof expression.</param>
        /// <returns>A new <see cref="TypeOfExpression"/> instance which obtains the 
        /// <see cref="RuntimeTypeHandle"/> of the provided <paramref name="target"/> at 
        /// runtime.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="target"/> is null.</exception>
        public static TypeOfExpression TypeOf(this IType target)
        {
            if (target == null)
                throw new ArgumentNullException("target");
            return new TypeOfExpression(target);
        }

        /// <summary>
        /// Obtains a <see cref="TypeOfExpression"/> for the symbol <paramref name="target"/> provided.
        /// </summary>
        /// <param name="target">The <see cref="String"/> symbol type which needs a typeof expression.</param>
        /// <param name="identityManager">The <see cref="IIdentityManager"/> which will maintain the identity of the <paramref name="target"/>.</param>
        /// <returns>A new <see cref="TypeOfExpression"/> instance which obtains the 
        /// <see cref="RuntimeTypeHandle"/> of the provided <paramref name="target"/> at 
        /// runtime.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="target"/> is null.</exception>
        public static TypeOfExpression TypeOf(this string target, IIdentityManager identityManager)
        {
            if (target == null)
                throw new ArgumentNullException("target");
            return target.GetSymbolType(identityManager).TypeOf();
        }

        /// <summary>
        /// Obtains a <see cref="TypeOfExpression"/> for the symbol <paramref name="target"/> provided.
        /// </summary>
        /// <param name="target">The <see cref="IExpressionFusionExpression"/> which needs a typeof
        /// expression.</param>
        /// <param name="identityManager">The <see cref="IIdentityManager "/> which marshals
        /// identities of types within the type model.</param>
        /// <returns>A new <see cref="TypeOfExpression"/> instance which obtains the 
        /// <see cref="RuntimeTypeHandle"/> of the provided <paramref name="target"/> at 
        /// runtime.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="target"/> is null.</exception>
        public static TypeOfExpression TypeOf(this IExpressionFusionExpression target, IIdentityManager identityManager)
        {
            if (target == null)
                throw new ArgumentNullException("target");
            return new SymbolType(target, identityManager).TypeOf();
        }
        #endregion 

        #region Method Invocation
        public static MethodInvokeExpression Call(this IMemberParentReferenceExpression target, string methodName, params IExpression[] parameters)
        {
            return (MethodInvokeExpression)target.GetMethod(methodName).Invoke(parameters);
        }

        public static MethodInvokeExpression Call(this IMemberParentReferenceExpression target, string methodName, IExpressionCollection parameters)
        {
            return (MethodInvokeExpression)target.GetMethod(methodName).Invoke(parameters);
        }
        #endregion 

        #region Symbol Expressions

        public static MethodInvokeExpression Call(this string symbol, string methodName, params IExpression[] parameters)
        {
            return symbol.GetSymbolExpression().Call(methodName, parameters);
        }

        public static UnboundPropertyReferenceExpression GetProperty(this string symbol, string propertyName)
        {
            return (UnboundPropertyReferenceExpression)symbol.GetSymbolExpression().GetProperty(propertyName);
        }

        public static IIndexerReferenceExpression GetIndexer(this string symbol, string indexerName, params IExpression[] parameters)
        {
            return symbol.GetSymbolExpression().GetIndexer(indexerName, parameters);
        }

        public static IIndexerReferenceExpression GetIndexer(this string symbol, params IExpression[] parameters)
        {

            return symbol.GetSymbolExpression().GetIndexer(parameters);
        }

        public static IFieldReferenceExpression GetField(this string symbol, string fieldName)
        {
            return symbol.GetSymbolExpression().GetField(fieldName);
        }

        /// <summary>
        /// Obtains a symbol expression from the <paramref name="symbol"/> <see cref="String"/>
        /// provided.
        /// </summary>
        /// <param name="symbol">The <see cref="String"/> representing the symbol expression.</param>
        /// <returns>A new <see cref="ISymbolExpression"/>.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="symbol"/> is null.</exception>
        public static SymbolExpression GetSymbolExpression(this string symbol)
        {
            if (symbol == null)
                throw new ArgumentNullException("symbol");
            lock (stringSymbolCache)
            {
                if (!stringSymbolCache.ContainsKey(symbol))
                    stringSymbolCache.Add(symbol, new Symbol(symbol));
                return stringSymbolCache[symbol];
            }
        }

        public static IExpressionFusionExpression Fuse(this IFusionTargetExpression target, IFusionTermExpression term)
        {
            return new ExpressionFusionExpression(target, term);
        }

        public static IExpressionFusionExpression Fuse(this IFusionTargetExpression target, string term)
        {
            return new ExpressionFusionExpression(target, term.GetSymbolExpression());
        }

        public static IExpressionFusionExpression Fuse(this string target, string term)
        {
            return target.GetSymbolExpression().Fuse(term);
        }

        public static IExpressionFusionExpression Fuse(this IType target, string term)
        {
            return new ExpressionFusionExpression(target.GetTypeExpression(), term.GetSymbolExpression());
        }

        public static IExpressionFusionExpression Fuse(this IType target, IFusionTermExpression term)
        {
            return ((IFusionTargetExpression)target.GetTypeExpression()).Fuse(term);
        }

        public static IExpressionToCommaFusionExpression Fuse(this IFusionCommaTargetExpression target, params IExpression[] terms)
        {
            return new ExpressionToCommaFusionExpression(target, terms);
        }

        /// <summary>
        /// Fuses a <see cref="String"/> to a series of <paramref name="terms"/> which are indictive 
        /// of a message transfer of some kind, either through a method call, indexer access,
        /// array access or other undescribed means of transferring the <paramref name="terms"/>.
        /// </summary>
        /// <param name="target">The target <see cref="String"/> which denotes
        /// the name of the call to make.</param>
        /// <param name="terms">The <see cref="IExpression"/> array which denotes 
        /// the information associated to the message.</param>
        /// <returns>A <see cref="IExpressionToCommaFusionExpression"/> which fuses the 
        /// <paramref name="target"/> string as a <see cref="ISymbolExpression"/> 
        /// to the <paramref name="terms"/> provided.</returns>
        public static IExpressionToCommaFusionExpression Fuse(this string target, params IExpression[] terms)
        {
            return new ExpressionToCommaFusionExpression(target.GetSymbolExpression(), terms);
        }

        /// <summary>
        /// Fuses a <see cref="IFusionTypeCollectionTargetExpression"/>
        /// to a series of <paramref name="types"/>, notable use is when
        /// an identifier is accompanied by a series of types, which may
        /// then be accompanied by a series of expressions, denoting a
        /// message target which offers variable parameter types.
        /// </summary>
        /// <param name="target">The 
        /// <see cref="IFusionTypeCollectionTargetExpression"/> to fuse
        /// the <paramref name="types"/> to.</param>
        /// <param name="types">The <see cref="IType"/> array which denotes
        /// the types to fuse to the <paramref name="target"/>
        /// expression.</param>
        /// <returns>
        /// A <see cref="IExpressionToCommaTypeReferenceFusionExpression"/>
        /// which denotes the fusion of the <paramref name="target"/>
        /// message provider and the <paramref name="types"/> which
        /// denote the potential variable parameter types</returns>
        public static IExpressionToCommaTypeReferenceFusionExpression Fuse(this IFusionTypeCollectionTargetExpression target, params IType[] types)
        {
            return new ExpressionToCommaTypeReferenceFusionExpression(target, types);
        }

        public static IExpressionToCommaTypeReferenceFusionExpression Fuse(this string target, params IType[] terms)
        {
            return new ExpressionToCommaTypeReferenceFusionExpression(target.GetSymbolExpression(), terms);
        }

        public static IExpressionToCommaFusionExpression Fuse(this byte target, string value, params IExpression[] parameters)
        {
            return target.ToPrimitive().Fuse(value).Fuse(parameters);
        }

        public static IExpressionToCommaFusionExpression Fuse(this sbyte target, string value, params IExpression[] parameters)
        {
            return target.ToPrimitive().Fuse(value).Fuse(parameters);
        }

        public static IExpressionToCommaFusionExpression Fuse(this ushort target, string value, params IExpression[] parameters)
        {
            return target.ToPrimitive().Fuse(value).Fuse(parameters);
        }

        public static IExpressionToCommaFusionExpression Fuse(this short target, string value, params IExpression[] parameters)
        {
            return target.ToPrimitive().Fuse(value).Fuse(parameters);
        }

        public static IExpressionToCommaFusionExpression Fuse(this uint target, string value, params IExpression[] parameters)
        {
            return target.ToPrimitive().Fuse(value).Fuse(parameters);
        }

        public static IExpressionToCommaFusionExpression Fuse(this int target, string value, params IExpression[] parameters)
        {
            return target.ToPrimitive().Fuse(value).Fuse(parameters);
        }

        public static IExpressionToCommaFusionExpression Fuse(this ulong target, string value, params IExpression[] parameters)
        {
            return target.ToPrimitive().Fuse(value).Fuse(parameters);
        }

        public static IExpressionToCommaFusionExpression Fuse(this long target, string value, params IExpression[] parameters)
        {
            return target.ToPrimitive().Fuse(value).Fuse(parameters);
        }

        public static IExpressionToCommaFusionExpression Fuse(this float target, string value, params IExpression[] parameters)
        {
            return target.ToPrimitive().Fuse(value).Fuse(parameters);
        }

        public static IExpressionToCommaFusionExpression Fuse(this double target, string value, params IExpression[] parameters)
        {
            return target.ToPrimitive().Fuse(value).Fuse(parameters);
        }

        public static IExpressionToCommaFusionExpression Fuse(this decimal target, string value, params IExpression[] parameters)
        {
            return target.ToPrimitive().Fuse(value).Fuse(parameters);
        }
        #endregion

        #region Named Method Parameters
        /// <summary>
        /// Creates a new <see cref="NamedParameterExpression"/> with the
        /// <paramref name="target"/> name with the referenced 
        /// <paramref name="namedParameterExpression"/> as the passed
        /// parameter expression.
        /// </summary>
        /// <param name="target">The <see cref="String"/> which represents the 
        /// parameter name.</param>
        /// <param name="namedParameterExpression">The <see cref="IExpression"/> to pass to
        /// the call.</param>
        /// <returns>A new <see cref="NamedParameterExpression"/> which
        /// </returns>
        public static NamedParameterExpression GetNamedParameter(this string target, IExpression namedParameterExpression)
        {
            return new NamedParameterExpression(target, namedParameterExpression);
        }

        public static NamedParameterExpression AsNamedParameter(this IExpression target, string name)
        {
            return new NamedParameterExpression(name, target);
        }

        public static NamedParameterExpression AsNamedParameter(this string target, string name)
        {
            return new NamedParameterExpression(name, (Symbol)target);
        }
        #endregion 
        /// <summary>
        /// Returns a <see cref="UnaryOperationExpression"/> with the <paramref name="target"/>'s
        /// sign inverted.
        /// </summary>
        /// <param name="target">The <see cref="IExpression"/> to negate.</param>
        /// <returns>A new <see cref="UnaryOperationExpression"/> instance with
        /// the <paramref name="target"/> as a <see cref="UnaryOperationExpression.Term"/>
        /// with the <see cref="UnaryOperation.SignInversion"/> applied.</returns>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="target"/> is null.</exception>
        public static UnaryOperationExpression Negate(this IExpression target)
        {
            if (target == null)
                throw new ArgumentNullException("target cannot be null.", "target");
            return new UnaryOperationExpression((IUnaryOperationPrimaryTerm)target.AffixToUnaryTerm(), UnaryOperation.SignInversion);
        }

        public static UnaryOperationExpression Decrement(this IExpression target, bool postIncrement = true)
        {
            if (target == null)
                throw new ArgumentNullException("target cannot be null.", "target");
            if (postIncrement)
                return new UnaryOperationExpression((IUnaryOperationPrimaryTerm)target.AffixToUnaryTerm(), UnaryOperation.Decrement | UnaryOperation.PostAction);
            else
                return new UnaryOperationExpression((IUnaryOperationPrimaryTerm)target.AffixToUnaryTerm(), UnaryOperation.Decrement | UnaryOperation.PreAction);
        }

        public static UnaryOperationExpression Decrement(this ILocalMember target, bool postIncrement = true)
        {
            if (target == null)
                throw new ArgumentNullException("target cannot be null.", "target");
            if (postIncrement)
                return new UnaryOperationExpression((IUnaryOperationPrimaryTerm)target.GetReference().AffixToUnaryTerm(), UnaryOperation.Decrement | UnaryOperation.PostAction);
            else
                return new UnaryOperationExpression((IUnaryOperationPrimaryTerm)target.GetReference().AffixToUnaryTerm(), UnaryOperation.Decrement | UnaryOperation.PreAction);
        }

        public static UnaryOperationExpression Decrement(this IIntermediateParameterMember target, bool postIncrement = true)
        {
            if (target == null)
                throw new ArgumentNullException("target cannot be null.", "target");
            if (postIncrement)
                return new UnaryOperationExpression((IUnaryOperationPrimaryTerm)target.GetReference().AffixToUnaryTerm(), UnaryOperation.Decrement | UnaryOperation.PostAction);
            else
                return new UnaryOperationExpression((IUnaryOperationPrimaryTerm)target.GetReference().AffixToUnaryTerm(), UnaryOperation.Decrement | UnaryOperation.PreAction);
        }

        public static UnaryOperationExpression Decrement(this IIntermediateFieldMember target, bool postIncrement = true)
        {
            if (target == null)
                throw new ArgumentNullException("target cannot be null.", "target");
            if (postIncrement)
                return new UnaryOperationExpression((IUnaryOperationPrimaryTerm)target.GetReference().AffixToUnaryTerm(), UnaryOperation.Decrement | UnaryOperation.PostAction);
            else
                return new UnaryOperationExpression((IUnaryOperationPrimaryTerm)target.GetReference().AffixToUnaryTerm(), UnaryOperation.Decrement | UnaryOperation.PreAction);
        }

        public static UnaryOperationExpression Decrement(this IIntermediatePropertySignatureMember target, bool postIncrement = true)
        {
            if (target == null)
                throw new ArgumentNullException("target cannot be null.", "target");
            if (postIncrement)
                return new UnaryOperationExpression((IUnaryOperationPrimaryTerm)target.GetReference().AffixToUnaryTerm(), UnaryOperation.Decrement | UnaryOperation.PostAction);
            else
                return new UnaryOperationExpression((IUnaryOperationPrimaryTerm)target.GetReference().AffixToUnaryTerm(), UnaryOperation.Decrement | UnaryOperation.PreAction);
        }
        public static UnaryOperationExpression Increment(this IExpression target, bool postIncrement = true)
        {
            if (target == null)
                throw new ArgumentNullException("target cannot be null.", "target");
            if (postIncrement)
                return new UnaryOperationExpression((IUnaryOperationPrimaryTerm)target.AffixToUnaryTerm(), UnaryOperation.Increment | UnaryOperation.PostAction);
            else
                return new UnaryOperationExpression((IUnaryOperationPrimaryTerm)target.AffixToUnaryTerm(), UnaryOperation.Increment | UnaryOperation.PreAction);
        }

        public static UnaryOperationExpression Increment(this ILocalMember target, bool postIncrement = true)
        {
            if (target == null)
                throw new ArgumentNullException("target cannot be null.", "target");
            if (postIncrement)
                return new UnaryOperationExpression((IUnaryOperationPrimaryTerm)target.GetReference().AffixToUnaryTerm(), UnaryOperation.Increment | UnaryOperation.PostAction);
            else
                return new UnaryOperationExpression((IUnaryOperationPrimaryTerm)target.GetReference().AffixToUnaryTerm(), UnaryOperation.Increment | UnaryOperation.PreAction);
        }

        public static UnaryOperationExpression Increment(this IIntermediateParameterMember target, bool postIncrement = true)
        {
            if (target == null)
                throw new ArgumentNullException("target cannot be null.", "target");
            if (postIncrement)
                return new UnaryOperationExpression((IUnaryOperationPrimaryTerm)target.GetReference().AffixToUnaryTerm(), UnaryOperation.Increment | UnaryOperation.PostAction);
            else
                return new UnaryOperationExpression((IUnaryOperationPrimaryTerm)target.GetReference().AffixToUnaryTerm(), UnaryOperation.Increment | UnaryOperation.PreAction);
        }

        public static UnaryOperationExpression Increment(this IIntermediateFieldMember target, bool postIncrement = true)
        {
            if (target == null)
                throw new ArgumentNullException("target cannot be null.", "target");
            if (postIncrement)
                return new UnaryOperationExpression((IUnaryOperationPrimaryTerm)target.GetReference().AffixToUnaryTerm(), UnaryOperation.Increment | UnaryOperation.PostAction);
            else
                return new UnaryOperationExpression((IUnaryOperationPrimaryTerm)target.GetReference().AffixToUnaryTerm(), UnaryOperation.Increment | UnaryOperation.PreAction);
        }

        public static UnaryOperationExpression Increment(this IIntermediatePropertySignatureMember target, bool postIncrement = true)
        {
            if (target == null)
                throw new ArgumentNullException("target cannot be null.", "target");
            if (postIncrement)
                return new UnaryOperationExpression((IUnaryOperationPrimaryTerm)target.GetReference().AffixToUnaryTerm(), UnaryOperation.Increment | UnaryOperation.PostAction);
            else
                return new UnaryOperationExpression((IUnaryOperationPrimaryTerm)target.GetReference().AffixToUnaryTerm(), UnaryOperation.Increment | UnaryOperation.PreAction);
        }
        /// <summary>
        /// Returns a <see cref="UnaryOperationExpression"/> with the
        /// <paramref name="target"/> to setup to be logically inverted (true is false, and vice versa).
        /// </summary>
        /// <param name="target">The <see cref="IExpression"/> to boolInvert.</param>
        /// <returns>A new <see cref="UnaryOperationExpression"/> instance with
        /// the <paramref name="target"/> as a <see cref="UnaryOperationExpression.Term"/>
        /// with the <see cref="UnaryOperation.BooleanInversion"/> applied.</returns>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="target"/> is null.</exception>
        public static UnaryOperationExpression Not(this IExpression target)
        {
            if (target == null)
                throw new ArgumentNullException("target cannot be null.", "target");
            return new UnaryOperationExpression((IUnaryOperationPrimaryTerm)target.AffixToUnaryTerm(), UnaryOperation.BooleanInversion);
        }

        /// <summary>
        /// Returns a <see cref="UnaryOperationExpression"/> with the
        /// <paramref name="target"/> to be setup for the runtime value's
        /// bits to be inverted.
        /// </summary>
        /// <param name="target">The <see cref="Boolean"/> <see cref="IExpression"/> to
        /// invert the bits of.</param>
        /// <returns>A <see cref="UnaryOperationExpression"/> instance 
        /// with the <paramref name="target"/>'s bits set to be inverted.</returns>
        public static UnaryOperationExpression Invert(this IExpression target)
        {
            if (target == null)
                throw new ArgumentNullException("target cannot be null", "target");
            return new UnaryOperationExpression(((IUnaryOperationPrimaryTerm)(target.AffixToUnaryTerm())), UnaryOperation.BitwiseInversion);
        }

        private static IUnaryOperationPrimaryTerm AffixToUnaryTerm(this IExpression target)
        {
            if (target is IUnaryOperationPrimaryTerm)
                return (IUnaryOperationPrimaryTerm)target;
            else
                return new ParenthesizedExpression(target);
        }

        public static void AddRange(this IMalleableExpressionCollection<IExpression> target, IEnumerable<IExpression> parameters)
        {
            if (target == null)
                throw new ArgumentNullException("target");
            if (parameters == null)
                throw new ArgumentNullException("parameters");
            foreach (var parameter in parameters)
                target.Add(parameter);
        }

        public static IAssignmentExpression Assign(this INaryOperandExpression assignmentTarget, AssignmentOperation assignmentType, INaryOperandExpression assignmentValue)
        {
            if (assignmentTarget == null)
                throw new ArgumentNullException("assignmentTarget");
            if (assignmentValue == null)
                throw new ArgumentNullException("assignmentValue");
            return new AssignmentExpression(assignmentTarget, assignmentType, assignmentValue);
        }

        public static IAssignmentExpression Assign(this INaryOperandExpression assignmentTarget, INaryOperandExpression assignmentValue)
        {
            if (assignmentTarget == null)
                throw new ArgumentNullException("assignmentTarget");
            if (assignmentValue == null)
                throw new ArgumentNullException("assignmentValue");
            return assignmentTarget.Assign(AssignmentOperation.SimpleAssign, assignmentValue);
        }


        #region Decorations
        #region Comments
        public static IDecoratingExpression LeftComment(this IExpression target, string comment, params object[] args)
        {
            if (target == null)
                throw new ArgumentNullException("target");
            if (target is IMemberParentReferenceDecoratingExpression)
                return LeftComment((IMemberParentReferenceDecoratingExpression)target, comment, args);
            else if (target is IDecoratingExpression)
                return LeftComment((IDecoratingExpression)target, comment, args);
            return new DecoratingExpression(target, new CommentExpression(string.Format(comment, args), DecorationDisplaySide.Before));
        }

        public static IDecoratingExpression LeftComment(IDecoratingExpression target, string comment, params object[] args)
        {
            if (target == null)
                throw new ArgumentNullException("target");
            target.Decorations.Add(new CommentExpression(string.Format(comment, args), DecorationDisplaySide.Before));
            return target;
        }
        public static IDecoratingExpression RightComment(this IExpression target, string comment, params object[] args)
        {
            if (target == null)
                throw new ArgumentNullException("target");
            if (target is IMemberParentReferenceDecoratingExpression)
                return RightComment((IMemberParentReferenceDecoratingExpression)target, comment, args);
            else if (target is IDecoratingExpression)
                return RightComment((IDecoratingExpression)target, comment, args);
            return new DecoratingExpression(target, new CommentExpression(string.Format(comment, args), DecorationDisplaySide.After));
        }

        public static IDecoratingExpression RightComment(IDecoratingExpression target, string comment, params object[] args)
        {
            if (target == null)
                throw new ArgumentNullException("target");
            target.Decorations.Add(new CommentExpression(string.Format(comment, args), DecorationDisplaySide.After));
            return target;
        }

        public static IMemberParentReferenceDecoratingExpression LeftComment(this IMemberParentReferenceExpression target, string comment, params object[] args)
        {
            if (target == null)
                throw new ArgumentNullException("target");
            if (target is IMemberParentReferenceDecoratingExpression)
                return LeftComment((IMemberParentReferenceDecoratingExpression)target, comment, args);
            return new MemberParentReferenceDecoratingExpression(target, new CommentExpression(string.Format(comment, args), DecorationDisplaySide.Before));
        }

        public static IMemberParentReferenceDecoratingExpression LeftComment(IMemberParentReferenceDecoratingExpression target, string comment, params object[] args)
        {
            if (target == null)
                throw new ArgumentNullException("target");
            target.Decorations.Add(new CommentExpression(string.Format(comment, args), DecorationDisplaySide.Before));
            return target;
        }
        public static IMemberParentReferenceDecoratingExpression RightComment(this IMemberParentReferenceExpression target, string comment, params object[] args)
        {
            if (target == null)
                throw new ArgumentNullException("target");
            if (target is IMemberParentReferenceDecoratingExpression)
                return RightComment((IMemberParentReferenceDecoratingExpression)target, comment, args);
            return new MemberParentReferenceDecoratingExpression(target, new CommentExpression(string.Format(comment, args), DecorationDisplaySide.After));
        }

        public static IMemberParentReferenceDecoratingExpression RightComment(IMemberParentReferenceDecoratingExpression target, string comment, params object[] args)
        {
            if (target == null)
                throw new ArgumentNullException("target");
            target.Decorations.Add(new CommentExpression(string.Format(comment, args), DecorationDisplaySide.After));
            return target;
        }
        #endregion
        #region NewLines
        public static IDecoratingExpression LeftNewLine(this IExpression target)
        {
            if (target == null)
                throw new ArgumentNullException("target");
            if (target is IMemberParentReferenceDecoratingExpression)
                return LeftNewLine((IMemberParentReferenceDecoratingExpression)target);
            else if (target is IDecoratingExpression)
                return LeftNewLine((IDecoratingExpression)target);
            return new DecoratingExpression(target, new NewLineExpression(DecorationDisplaySide.Before));
        }

        public static IDecoratingExpression LeftNewLine(IDecoratingExpression target)
        {
            if (target == null)
                throw new ArgumentNullException("target");
            target.Decorations.Add(new NewLineExpression(DecorationDisplaySide.Before));
            return target;
        }
        public static IDecoratingExpression RightNewLine(this IExpression target)
        {
            if (target == null)
                throw new ArgumentNullException("target");
            if (target is IMemberParentReferenceDecoratingExpression)
                return RightNewLine((IMemberParentReferenceDecoratingExpression)target);
            else if (target is IDecoratingExpression)
                return RightNewLine((IDecoratingExpression)target);
            return new DecoratingExpression(target, new NewLineExpression(DecorationDisplaySide.After));
        }

        public static IDecoratingExpression RightNewLine(IDecoratingExpression target)
        {
            if (target == null)
                throw new ArgumentNullException("target");
            target.Decorations.Add(new NewLineExpression(DecorationDisplaySide.After));
            return target;
        }

        public static IMemberParentReferenceDecoratingExpression LeftNewLine(this IMemberParentReferenceExpression target)
        {
            if (target == null)
                throw new ArgumentNullException("target");
            if (target is IMemberParentReferenceDecoratingExpression)
                return LeftNewLine((IMemberParentReferenceDecoratingExpression)target);
            return new MemberParentReferenceDecoratingExpression(target, new NewLineExpression(DecorationDisplaySide.Before));
        }

        public static IMemberParentReferenceDecoratingExpression LeftNewLine(IMemberParentReferenceDecoratingExpression target)
        {
            if (target == null)
                throw new ArgumentNullException("target");
            target.Decorations.Add(new NewLineExpression(DecorationDisplaySide.Before));
            return target;
        }
        public static IMemberParentReferenceDecoratingExpression RightNewLine(this IMemberParentReferenceExpression target)
        {
            if (target == null)
                throw new ArgumentNullException("target");
            if (target is IMemberParentReferenceDecoratingExpression)
                return RightNewLine((IMemberParentReferenceDecoratingExpression)target);
            return new MemberParentReferenceDecoratingExpression(target, new NewLineExpression(DecorationDisplaySide.After));
        }

        public static IMemberParentReferenceDecoratingExpression RightNewLine(IMemberParentReferenceDecoratingExpression target)
        {
            if (target == null)
                throw new ArgumentNullException("target");
            target.Decorations.Add(new NewLineExpression(DecorationDisplaySide.After));
            return target;
        }
        #endregion
        #endregion

        public static IParameterReferenceExpression GetReference(this IParameterMember parameter)
        {
            return new ParameterReferenceExpression(parameter.Name);
        }

        public static IDirectionExpression Direct(this IExpression directed, ParameterCoercionDirection direction)
        {
            return new DirectionExpression(directed, direction);
        }

        public static IAwaitExpression Await(this IMemberReferenceExpression proffered)
        {
            return new AwaitExpression { Proffer = proffered };
        }
        public static IAwaitStatementExpression Await(this IStatementExpression proffered)
        {
            return new AwaitStatementExpression { Proffer = proffered };
        }

        public static IDecoratingExpression GetDummyLambdaExpression(this IBlockStatement block, params string[] parameters)
        {
            var blockExpression = new BlockExpression{Block=block};
            var decorator = blockExpression.LeftNewLine();
            decorator.Decorations.Add(new ExplicitStringLiteralDecorationExpression(DecorationDisplaySide.Before)
            {
                LiteralInsert = string.Format("({0}) =>", string.Join(", ", parameters)),
            });
            decorator.Decorations.Add(new NewLineExpression(DecorationDisplaySide.Before));
            return decorator;
        }


        public static IMethodReferenceStub GetMethod(this IMemberParentReferenceExpression target, string name, params IType[] typeParameters)
        {
            return target.GetMethod(name, new TypeCollection(typeParameters));
        }

        public static IFieldReferenceExpression<IStructFieldMember, IStructType> Add(this IMalleableExpressionCollection target, IStructFieldMember field)
        {
            var result = field.GetReference();
            target.Add(result);
            return result;
        }

        public static IFieldReferenceExpression<IClassFieldMember, IClassType> Add(this IMalleableExpressionCollection target, IClassFieldMember field)
        {
            var result = field.GetReference();
            target.Add(result);
            return result;
        }

        public static IPropertyReferenceExpression<IStructPropertyMember, IStructType> Add(this IMalleableExpressionCollection target, IStructPropertyMember property)
        {
            var result = property.GetReference();
            target.Add(result);
            return result;
        }

        public static IPropertyReferenceExpression<IClassPropertyMember, IClassType> Add(this IMalleableExpressionCollection target, IClassPropertyMember property)
        {
            var result = property.GetReference();
            target.Add(result);
            return result;
        }

        public static ILocalReferenceExpression Add(this IMalleableExpressionCollection target, ILocalMember local)
        {
            var result = local.GetReference();
            target.Add(result);
            return result;
        }

        public static IParameterReferenceExpression Add(this IMalleableExpressionCollection target, IParameterMember parameter)
        {
            var result = parameter.GetReference();
            target.Add(result);
            return result;
        }

        public static ICreateArrayExpression MakeArrayExpression(this IType target, params IExpression[] sizes)
        {
            if (sizes == null)
                throw new ArgumentNullException("sizes");
            return new MalleableCreateArrayExpression(target, sizes);
        }

        public static ICreateArrayExpression MakeArrayExpression(this IType target, params int[] sizes)
        {
            return target.MakeArrayExpression(sizes.Select(k => k.ToPrimitive()).ToArray());
        }

    }
}