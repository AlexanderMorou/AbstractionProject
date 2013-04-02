using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.Abstract;
using AllenCopeland.Abstraction.Slf._Internal.Ast;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Utilities.Arrays;
using System.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Ast.Expressions
{
    public static partial class ExpressionExtensions
    {
        private static IDictionary<string, Symbol> stringSymbolCache = new Dictionary<string,Symbol>();
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

        #region TypeExpression Conversion
        /// <summary>
        /// Obtains a type expression from the <paramref name="target"/> provided.
        /// </summary>
        /// <param name="target">The <see cref="Type"/> to receive the type reference
        /// expression of.</param>
        /// <param name="identityManager">The <see cref="ICliManager"/>
        /// which is responsible for marshalling type and assembly identities
        /// within the current type model.</param>
        /// <returns>A new <see cref="TypeReferenceExpression"/>.</returns>
        /// <remarks>Used to obtain a type as an expression for linking a type as the
        /// origin of a primary expression.</remarks>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="target"/> is null.</exception>
        public static ITypeReferenceExpression GetTypeExpression(this Type target, ICliManager identityManager)
        {
            if (target == null)
                throw new ArgumentNullException("target");
            return identityManager.ObtainTypeReference(target).GetTypeExpression();
        }

        /// <summary>
        /// Obtains a <see cref="TypeReferenceExpression"/> for the <paramref name="target"/>
        /// provided.
        /// </summary>
        /// <param name="target">A <see cref="IExpressionFusionExpression"/> which represents 
        /// a symbolic form of a type.</param>
        /// <param name="identityManager">The <see cref="ICliManager"/>
        /// which is responsible for marshalling type and assembly identities
        /// within the current type model.</param>
        /// <returns>A new <see cref="TypeReferenceExpression"/> which wraps the <paramref name="target"/>
        /// in a <see cref="SymbolType"/>.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="target"/> is null.</exception>
        public static ITypeReferenceExpression GetTypeExpression(this IExpressionFusionExpression target, ICliManager identityManager)
        {
            if (target == null)
                throw new ArgumentNullException("target");
            return new SymbolType(target, identityManager).GetTypeExpression();
        }

        #endregion 

        #region MembetReference conversion

        /// <summary>
        /// Obtains a method expression relative to a <paramref name="target"/>
        /// <see cref="Type"/> which points to the method group
        /// provided through <paramref name="methodName"/>.
        /// </summary>
        /// <param name="target">The <see cref="Type"/> which contains
        /// the method group under the alias <paramref name="methodName"/>.</param>
        /// <param name="methodName">The alias or identifier of the method group
        /// from the <paramref name="target"/> <see cref="Type"/>.</param>
        /// <param name="identityManager">The <see cref="ICliManager"/>
        /// which is responsible for marshalling type and assembly identities
        /// within the current type model.</param>
        /// <returns>A <see cref="IMethodReferenceStub"/>
        /// which denotes a stub reference to the method group
        /// by the alias <paramref name="methodName"/>.</returns>
        public static IMethodReferenceStub GetMethodExpression(this Type target, ICliManager identityManager, string methodName)
        {
            if (target == null)
                throw new ArgumentNullException("target");
            if (string.IsNullOrEmpty(methodName))
                throw new ArgumentNullException("methodName");

            return target.GetTypeExpression(identityManager).GetMethod(methodName);
        }

        /// <summary>
        /// Obtains an <see cref="IMethodInvokeExpression"/> from the 
        /// <paramref name="target"/> <see cref="Type"/> under the 
        /// method group provided by <paramref name="methodName"/> with the
        /// <paramref name="parenters"/> that denote the information to pass.
        /// </summary>
        /// <param name="target">The <see cref="Type"/> which contains
        /// the method group under the alias <paramref name="methodName"/>.</param>
        /// <param name="identityManager">The <see cref="ICliManager"/>
        /// which is responsible for marshalling type and assembly identities
        /// within the current type model.</param>
        /// <param name="methodName">The alias or identifier of the method group
        /// from the <paramref name="target"/> <see cref="Type"/>.</param>
        /// <param name="parameters">The <see cref="IExpression"/> array of 
        /// parameters which denote the information to pass to the method group
        /// under the <paramref name="methodName"/> alias.</param>
        /// <returns>A <see cref="IMethodInvokeExpression"/> which expresses
        /// the invocation.</returns>
        public static IMethodInvokeExpression GetMethodInvokeExpression(this Type target, ICliManager identityManager, string methodName, params IExpression[] parameters)
        {
            return target.GetMethodExpression(identityManager, methodName).Invoke(parameters);
        }

        public static IMethodInvokeExpression GetMethodInvokeExpression<T1>(this Type target, ICliManager identityManager, string methodName, params IExpression[] parameters)
        {
            return (new UnboundMethodReferenceStub(target.GetTypeExpression(identityManager), methodName, new Type[] { typeof(T1) }.ToCollection(identityManager))).Invoke(parameters);
        }

        public static IMethodInvokeExpression GetMethodInvokeExpression<T1, T2>(this Type target, ICliManager identityManager, string methodName, params IExpression[] parameters)
        {
            return new UnboundMethodReferenceStub(target.GetTypeExpression(identityManager), methodName, new Type[] { typeof(T1), typeof(T2) }.ToCollection(identityManager)).Invoke(parameters);
        }

        public static IMethodInvokeExpression GetMethodInvokeExpression<T1, T2, T3>(this Type target, ICliManager identityManager, string methodName, params IExpression[] parameters)
        {
            return new UnboundMethodReferenceStub(target.GetTypeExpression(identityManager), methodName, new Type[] { typeof(T1), typeof(T2), typeof(T3) }.ToCollection(identityManager)).Invoke(parameters);
        }

        public static IMethodInvokeExpression GetMethodInvokeExpression<T1, T2, T3, T4>(this Type target, ICliManager identityManager, string methodName, params IExpression[] parameters)
        {
            return new UnboundMethodReferenceStub(target.GetTypeExpression(identityManager), methodName, new Type[] { typeof(T1), typeof(T2), typeof(T3), typeof(T4) }.ToCollection(identityManager)).Invoke(parameters);
        }

        public static IPropertyReferenceExpression GetPropertyExpression(this Type target, ICliManager identityManager, string propertyName)
        {
            if (target == null)
                throw new ArgumentNullException("target");
            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentNullException("propertyName");
            return target.GetTypeExpression(identityManager).GetProperty(propertyName);
        }

        public static IFieldReferenceExpression GetFieldExpression(this Type target, ICliManager identityManager, string fieldName)
        {
            if (target == null)
                throw new ArgumentNullException("target");
            if (string.IsNullOrEmpty(fieldName))
                throw new ArgumentNullException("fieldName");
            return target.GetTypeExpression(identityManager).GetField(fieldName);
        }

        public static IIndexerReferenceExpression GetIndexerExpression(this Type target, ICliManager identityManager, params IExpression[] parameters)
        {
            return target.GetIndexerExpression(identityManager, null, parameters);
        }

        public static IIndexerReferenceExpression GetIndexerExpression(this Type target, ICliManager identityManager, string indexerName, params IExpression[] parameters)
        {
            if (target == null)
                throw new ArgumentNullException("target");
            if (indexerName == string.Empty)
                throw new ArgumentOutOfRangeException("indexerName", "May be null, but not empty.");
            return target.GetTypeExpression(identityManager).GetIndexer(indexerName, parameters);
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
        /// <param name="identityManager">The <see cref="ICliManager "/> which marshals
        /// identities of types within the type model.</param>
        /// <returns>A new <see cref="TypeOfExpression"/> instance which obtains the 
        /// <see cref="RuntimeTypeHandle"/> of the provided <paramref name="target"/> at 
        /// runtime.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="target"/> is null.</exception>
        public static TypeOfExpression TypeOf(this Type target, ICliManager identityManager)
        {
            if (target == null)
                throw new ArgumentNullException("target");
            return identityManager.ObtainTypeReference(target).TypeOf();
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
        /// <param name="identityManager">The <see cref="ICliManager "/> which marshals
        /// identities of types within the type model.</param>
        /// <returns>A new <see cref="TypeOfExpression"/> instance which obtains the 
        /// <see cref="RuntimeTypeHandle"/> of the provided <paramref name="target"/> at 
        /// runtime.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="target"/> is null.</exception>
        public static TypeOfExpression TypeOf(this IExpressionFusionExpression target, ITypeIdentityManager identityManager)
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

        public static IExpressionFusionExpression Fuse(this Type target, string term, ICliManager identityManager)
        {
            return identityManager.ObtainTypeReference(target).Fuse(term);
        }

        public static IExpressionFusionExpression Fuse(this IType target, IFusionTermExpression term)
        {
            return ((IFusionTargetExpression)target.GetTypeExpression()).Fuse(term);
        }

        public static IExpressionFusionExpression Fuse(this Type target, IFusionTermExpression term, ICliManager identityManager)
        {
            return ((IFusionTargetExpression)target.GetTypeExpression(identityManager)).Fuse(term);
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
        public static IExpressionToCommaTypeReferenceFusionExpression Fuse(this IFusionTypeCollectionTargetExpression target, ICliManager identityManager, params Type[] types)
        {
            return new ExpressionToCommaTypeReferenceFusionExpression(target, types.ToCollection(identityManager));
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


    }
}