using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.Abstract;
using AllenCopeland.Abstraction.Slf._Internal.Ast;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Oil;
using AllenCopeland.Abstraction.Slf.Oil.Members;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Utilities.Arrays;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    [CLSCompliant(false)]
    public static partial class ExpressionExtensions
    {
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
        public static MalleableExpressionCollection ToCollection(this IExpression[] target)
        {
            if (target == null)
                throw new ArgumentNullException("target");
            return new MalleableExpressionCollection(target);
        }
        #endregion

        #region TypeExpression Conversion
        /// <summary>
        /// Obtains a type expression from the <paramref name="target"/> provided.
        /// </summary>
        /// <param name="target">The <see cref="Type"/> to receive the type reference
        /// expression of.</param>
        /// <returns>A new <see cref="TypeReferenceExpression"/>.</returns>
        /// <remarks>Used to obtain a type as an expression for linking a type as the
        /// origin of a primary expression.</remarks>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="target"/> is null.</exception>
        public static ITypeReferenceExpression GetTypeExpression(this Type target)
        {
            if (target == null)
                throw new ArgumentNullException("target");
            return target.GetTypeReference().GetTypeExpression();
        }

        /// <summary>
        /// Obtains a <see cref="TypeReferenceExpression"/> for the <paramref name="target"/>
        /// provided.
        /// </summary>
        /// <param name="target">A <see cref="IExpressionFusionExpression"/> which represents 
        /// a symbolic form of a type.</param>
        /// <returns>A new <see cref="TypeReferenceExpression"/> which wraps the <paramref name="target"/>
        /// in a <see cref="SymbolType"/>.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="target"/> is null.</exception>
        public static ITypeReferenceExpression GetTypeExpression(this IExpressionFusionExpression target)
        {
            if (target == null)
                throw new ArgumentNullException("target");
            return new SymbolType(target).GetTypeExpression();
        }

        #endregion 

        #region MembetReference conversion
        public static IMethodReferenceStub GetMethodExpression(this Type target, string methodName)
        {
            if (target == null)
                throw new ArgumentNullException("target");
            if (string.IsNullOrEmpty(methodName))
                throw new ArgumentNullException("methodName");

            return target.GetTypeExpression().GetMethod(methodName);
        }

        public static IMethodInvokeExpression GetInvokeMethodExpression(this Type target, string methodName, params IExpression[] parameters)
        {
            return target.GetMethodExpression(methodName).Invoke(parameters);
        }

        public static IMethodInvokeExpression GetInvokeMethodExpression<T1>(this Type target, string methodName, params IExpression[] parameters)
        {
            var result = new MethodReferenceStub(target.GetTypeExpression(), methodName, new Type[] { typeof(T1) }.ToCollection());
            return result.Invoke(parameters);
        }

        public static IMethodInvokeExpression GetInvokeMethodExpression<T1, T2>(this Type target, string methodName, params IExpression[] parameters)
        {
            var result = new MethodReferenceStub(target.GetTypeExpression(), methodName, new Type[] { typeof(T1), typeof(T2) }.ToCollection());
            return result.Invoke(parameters);
        }

        public static IMethodInvokeExpression GetInvokeMethodExpression<T1, T2, T3>(this Type target, string methodName, params IExpression[] parameters)
        {
            var result = new MethodReferenceStub(target.GetTypeExpression(), methodName, new Type[] { typeof(T1), typeof(T2), typeof(T3) }.ToCollection());
            return result.Invoke(parameters);
        }

        public static IMethodInvokeExpression GetInvokeMethodExpression<T1, T2, T3, T4>(this Type target, string methodName, params IExpression[] parameters)
        {
            var result = new MethodReferenceStub(target.GetTypeExpression(), methodName, new Type[] { typeof(T1), typeof(T2), typeof(T3), typeof(T4) }.ToCollection());
            return result.Invoke(parameters);
        }

        public static IPropertyReferenceExpression GetPropertyExpression(this Type target, string propertyName)
        {
            if (target == null)
                throw new ArgumentNullException("target");
            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentNullException("propertyName");
            return target.GetTypeExpression().GetProperty(propertyName);
        }

        public static IFieldReferenceExpression GetFieldExpression(this Type target, string fieldName)
        {
            if (target == null)
                throw new ArgumentNullException("target");
            if (string.IsNullOrEmpty(fieldName))
                throw new ArgumentNullException("fieldName");
            return target.GetTypeExpression().GetField(fieldName);
        }

        public static IIndexerReferenceExpression GetIndexerExpression(this Type target, params IExpression[] parameters)
        {
            return target.GetIndexerExpression(null, parameters);
        }

        public static IIndexerReferenceExpression GetIndexerExpression(this Type target, string indexerName, params IExpression[] parameters)
        {
            if (target == null)
                throw new ArgumentNullException("target");
            if (indexerName == string.Empty)
                throw new ArgumentOutOfRangeException("indexerName", "May be null, but not empty.");
            return target.GetTypeExpression().GetIndexer(indexerName, parameters);
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
        /// Obtains a <see cref="TypeOfExpression"/> for the <paramref name="target"/> provided.
        /// </summary>
        /// <param name="target">The <see cref="Type"/> which needs a typeof expression.</param>
        /// <returns>A new <see cref="TypeOfExpression"/> instance which obtains the 
        /// <see cref="RuntimeTypeHandle"/> of the provided <paramref name="target"/> at 
        /// runtime.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="target"/> is null.</exception>
        public static TypeOfExpression TypeOf(this Type target)
        {
            if (target == null)
                throw new ArgumentNullException("target");
            return target.GetTypeReference().TypeOf();
        }

        /// <summary>
        /// Obtains a <see cref="TypeOfExpression"/> for the symbol <paramref name="target"/> provided.
        /// </summary>
        /// <param name="target">The <see cref="String"/> symbol type which needs a typeof expression.</param>
        /// <returns>A new <see cref="TypeOfExpression"/> instance which obtains the 
        /// <see cref="RuntimeTypeHandle"/> of the provided <paramref name="target"/> at 
        /// runtime.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="target"/> is null.</exception>
        public static TypeOfExpression TypeOf(this string target)
        {
            if (target == null)
                throw new ArgumentNullException("target");
            return target.GetSymbolType().TypeOf();
        }

        /// <summary>
        /// Obtains a <see cref="TypeOfExpression"/> for the symbol <paramref name="target"/> provided.
        /// </summary>
        /// <param name="target">The <see cref="IExpressionFusionExpression"/> which needs a typeof
        /// expression.</param>
        /// <returns>A new <see cref="TypeOfExpression"/> instance which obtains the 
        /// <see cref="RuntimeTypeHandle"/> of the provided <paramref name="target"/> at 
        /// runtime.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="target"/> is null.</exception>
        public static TypeOfExpression TypeOf(this IExpressionFusionExpression target)
        {
            if (target == null)
                throw new ArgumentNullException("target");
            return new SymbolType(target).TypeOf();
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
            return new SymbolExpression(symbol).Call(methodName, parameters);
        }

        public static PropertyReferenceExpression GetProperty(this string symbol, string propertyName)
        {
            return (PropertyReferenceExpression)new SymbolExpression(symbol).GetProperty(propertyName);
        }

        public static IIndexerReferenceExpression GetIndexer(this string symbol, string indexerName, params IExpression[] parameters)
        {
            return new SymbolExpression(symbol).GetIndexer(indexerName, parameters);
        }

        public static IIndexerReferenceExpression GetIndexer(this string symbol, params IExpression[] parameters)
        {
            return new SymbolExpression(symbol).GetIndexer(parameters);
        }

        public static IFieldReferenceExpression GetField(this string symbol, string fieldName)
        {
            return new SymbolExpression(symbol).GetField(fieldName);
        }


        /// <summary>
        /// Obtains a symbol expression from the <paramref name="target"/> <see cref="String"/>
        /// provided.
        /// </summary>
        /// <param name="target">The <see cref="String"/> representing the symbol expression.</param>
        /// <returns>A new <see cref="ISymbolExpression"/>.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="target"/> is null.</exception>
        public static SymbolExpression GetSymbolExpression(this string target)
        {
            if (target == null)
                throw new ArgumentNullException("target");
            return new SymbolExpression(target);
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
            return new ExpressionFusionExpression(target.GetSymbolExpression(), term.GetSymbolExpression());
        }

        public static IExpressionFusionExpression Fuse(this IType target, string term)
        {
            return new ExpressionFusionExpression(target.GetTypeExpression(), term.GetSymbolExpression());
        }

        public static IExpressionFusionExpression Fuse(this Type target, string term)
        {
            return target.GetTypeReference().Fuse(term);
        }

        public static IExpressionFusionExpression Fuse(this IType target, IFusionTermExpression term)
        {
            return ((IFusionTargetExpression)target.GetTypeExpression()).Fuse(term);
        }

        public static IExpressionFusionExpression Fuse(this Type target, IFusionTermExpression term)
        {
            return ((IFusionTargetExpression)target.GetTypeExpression()).Fuse(term);
        }

        public static IExpressionToCommaFusionExpression Fuse(this IFusionCommaTargetExpression target, params IExpression[] terms)
        {
            return new ExpressionToCommaFusionExpression(target, terms);
        }

        public static IExpressionToCommaFusionExpression Fuse(this string target, params IExpression[] terms)
        {
            return new ExpressionToCommaFusionExpression(target.GetSymbolExpression(), terms);
        }

        public static IExpressionToCommaTypeReferenceFusionExpression Fuse(this IFusionTypeCollectionTargetExpression target, params IType[] terms)
        {
            return new ExpressionToCommaTypeReferenceFusionExpression(target, terms);
        }
        public static IExpressionToCommaTypeReferenceFusionExpression Fuse(this IFusionTypeCollectionTargetExpression target, params Type[] terms)
        {
            return new ExpressionToCommaTypeReferenceFusionExpression(target, terms.ToCollection());
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

        #region Array ToExpression
        public static ICreateArrayExpression ToExpression(this int[] target)
        {
            return target.ToExpression<int>(ToPrimitive);
        }
        public static ICreateArrayExpression ToExpression(this int[,] target)
        {
            return target.ToExpression<int>(ToPrimitive);
        }
        public static ICreateArrayExpression ToExpression(this int[, ,] target)
        {
            return target.ToExpression<int>(ToPrimitive);
        }
        public static ICreateArrayExpression ToExpression(this int[, , ,] target)
        {
            return target.ToExpression<int>(ToPrimitive);
        }

        public static ICreateArrayExpression ToExpression(this byte[] target)
        {
            return target.ToExpression<byte>(ToPrimitive);
        }
        public static ICreateArrayExpression ToExpression(this byte[,] target)
        {
            return target.ToExpression<byte>(ToPrimitive);
        }
        public static ICreateArrayExpression ToExpression(this byte[, ,] target)
        {
            return target.ToExpression<byte>(ToPrimitive);
        }
        public static ICreateArrayExpression ToExpression(this byte[, , ,] target)
        {
            return target.ToExpression<byte>(ToPrimitive);
        }

        public static ICreateArrayExpression ToExpression(this uint[] target)
        {
            return target.ToExpression<uint>(ToPrimitive);
        }
        public static ICreateArrayExpression ToExpression(this uint[,] target)
        {
            return target.ToExpression<uint>(ToPrimitive);
        }
        public static ICreateArrayExpression ToExpression(this uint[, ,] target)
        {
            return target.ToExpression<uint>(ToPrimitive);
        }
        public static ICreateArrayExpression ToExpression(this uint[, , ,] target)
        {
            return target.ToExpression<uint>(ToPrimitive);
        }

        public static ICreateArrayExpression ToExpression(this sbyte[] target)
        {
            return target.ToExpression<sbyte>(ToPrimitive);
        }
        public static ICreateArrayExpression ToExpression(this sbyte[,] target)
        {
            return target.ToExpression<sbyte>(ToPrimitive);
        }
        public static ICreateArrayExpression ToExpression(this sbyte[, ,] target)
        {
            return target.ToExpression<sbyte>(ToPrimitive);
        }
        public static ICreateArrayExpression ToExpression(this sbyte[, , ,] target)
        {
            return target.ToExpression<sbyte>(ToPrimitive);
        }

        public static ICreateArrayExpression ToExpression(this short[] target)
        {
            return target.ToExpression<short>(ToPrimitive);
        }
        public static ICreateArrayExpression ToExpression(this short[,] target)
        {
            return target.ToExpression<short>(ToPrimitive);
        }
        public static ICreateArrayExpression ToExpression(this short[, ,] target)
        {
            return target.ToExpression<short>(ToPrimitive);
        }
        public static ICreateArrayExpression ToExpression(this short[, , ,] target)
        {
            return target.ToExpression<short>(ToPrimitive);
        }

        public static ICreateArrayExpression ToExpression(this ushort[] target)
        {
            return target.ToExpression<ushort>(ToPrimitive);
        }
        public static ICreateArrayExpression ToExpression(this ushort[,] target)
        {
            return target.ToExpression<ushort>(ToPrimitive);
        }
        public static ICreateArrayExpression ToExpression(this ushort[, ,] target)
        {
            return target.ToExpression<ushort>(ToPrimitive);
        }
        public static ICreateArrayExpression ToExpression(this ushort[, , ,] target)
        {
            return target.ToExpression<ushort>(ToPrimitive);
        }

        public static ICreateArrayExpression ToExpression(this long[] target)
        {
            return target.ToExpression<long>(ToPrimitive);
        }
        public static ICreateArrayExpression ToExpression(this long[,] target)
        {
            return target.ToExpression<long>(ToPrimitive);
        }
        public static ICreateArrayExpression ToExpression(this long[, ,] target)
        {
            return target.ToExpression<long>(ToPrimitive);
        }
        public static ICreateArrayExpression ToExpression(this long[, , ,] target)
        {
            return target.ToExpression<long>(ToPrimitive);
        }

        public static ICreateArrayExpression ToExpression(this ulong[] target)
        {
            return target.ToExpression<ulong>(ToPrimitive);
        }
        public static ICreateArrayExpression ToExpression(this ulong[,] target)
        {
            return target.ToExpression<ulong>(ToPrimitive);
        }
        public static ICreateArrayExpression ToExpression(this ulong[, ,] target)
        {
            return target.ToExpression<ulong>(ToPrimitive);
        }
        public static ICreateArrayExpression ToExpression(this ulong[, , ,] target)
        {
            return target.ToExpression<ulong>(ToPrimitive);
        }

        public static ICreateArrayExpression ToExpression(this float[] target)
        {
            return target.ToExpression<float>(ToPrimitive);
        }
        public static ICreateArrayExpression ToExpression(this float[,] target)
        {
            return target.ToExpression<float>(ToPrimitive);
        }
        public static ICreateArrayExpression ToExpression(this float[, ,] target)
        {
            return target.ToExpression<float>(ToPrimitive);
        }
        public static ICreateArrayExpression ToExpression(this float[, , ,] target)
        {
            return target.ToExpression<float>(ToPrimitive);
        }

        public static ICreateArrayExpression ToExpression(this double[] target)
        {
            return target.ToExpression<double>(ToPrimitive);
        }
        public static ICreateArrayExpression ToExpression(this double[,] target)
        {
            return target.ToExpression<double>(ToPrimitive);
        }
        public static ICreateArrayExpression ToExpression(this double[, ,] target)
        {
            return target.ToExpression<double>(ToPrimitive);
        }
        public static ICreateArrayExpression ToExpression(this double[, , ,] target)
        {
            return target.ToExpression<double>(ToPrimitive);
        }

        public static ICreateArrayExpression ToExpression(this decimal[] target)
        {
            return target.ToExpression<decimal>(ToPrimitive);
        }
        public static ICreateArrayExpression ToExpression(this decimal[,] target)
        {
            return target.ToExpression<decimal>(ToPrimitive);
        }
        public static ICreateArrayExpression ToExpression(this decimal[, ,] target)
        {
            return target.ToExpression<decimal>(ToPrimitive);
        }
        public static ICreateArrayExpression ToExpression(this decimal[, , ,] target)
        {
            return target.ToExpression<decimal>(ToPrimitive);
        }

        public static ICreateArrayExpression ToExpression(this string[] target)
        {
            return target.ToExpression<string>(ToPrimitive);
        }
        public static ICreateArrayExpression ToExpression(this string[,] target)
        {
            return target.ToExpression<string>(ToPrimitive);
        }
        public static ICreateArrayExpression ToExpression(this string[, ,] target)
        {
            return target.ToExpression<string>(ToPrimitive);
        }
        public static ICreateArrayExpression ToExpression(this string[, , ,] target)
        {
            return target.ToExpression<string>(ToPrimitive);
        }
        #endregion

        public static ICreateArrayExpression ToExpression<T>(this Array target, Func<T, IExpression> expressionConverter)
        {
            /* *
             * Instantiate a create array expression with item
             * details.
             * */
            int arrayRank = target.Rank;
            var result = new CreateArrayDetailExpression(typeof(T).GetTypeReference(), arrayRank);
            /* *
             * Since the sizes are available, provide them
             * to the array detail.
             * */
            result.Sizes.AddRange(target.Rank.Range().OnAll(p => target.GetLength(p).ToPrimitive()));
            /* *
             * Setup a series of nested detail expressions which will
             * be used to provide information about the dimensions
             * of the array.
             * *
             * Since they're cleaned out after spill-over, a single
             * dimension will do, the first item is the result.
             * */
            ICreateArrayNestedDetailExpression[] nestedDetails = new ICreateArrayNestedDetailExpression[arrayRank];
            nestedDetails[0] = result;
            for (int i = 1; i < arrayRank; i++)
                nestedDetails[i] = new CreateArrayNestedDetailExpression();
            int topRankIndex = arrayRank - 1;
            var topLevel = nestedDetails[topRankIndex];
            /* *
             * The bounds increment lambda handles dimension spillover;
             * that is, when a rank's highest element is reached,
             * it's set back to one, when this occurs, the current definition
             * of the dimension is complete, and the next needs started.
             * */
            Action<int, bool> boundsLambda = (rank, isIterationComplete) =>
                {
                    if (rank > 0)
                    {
                        nestedDetails[rank - 1].Details.Add(nestedDetails[rank]);
                        if (!isIterationComplete)
                            nestedDetails[rank] = new CreateArrayNestedDetailExpression();
                        else
                            nestedDetails[rank] = null;
                        if (rank == topRankIndex)
                            topLevel = nestedDetails[topRankIndex];
                    }
                };

            /* *
             * This part is easy, on the top level nested details
             * add the item at the current indices set. 
             * *
             * The bounds lambda handles divisioning.
             * */
            foreach (var indices in target.Iterate(boundsLambda))
                topLevel.Details.Add(expressionConverter((T)target.GetValue(indices)));
            return result;
        }
    }
}