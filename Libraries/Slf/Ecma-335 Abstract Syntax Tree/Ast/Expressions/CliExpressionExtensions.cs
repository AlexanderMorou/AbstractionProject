using AllenCopeland.Abstraction.Slf._Internal.Ast;
using AllenCopeland.Abstraction.Slf.Cli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Ast.Expressions
{
    public static class CliExpressionExtensions
    {

        #region TypeExpression Conversion
        /// <summary>
        /// Obtains a type expression from the <paramref name="target"/> provided.
        /// </summary>
        /// <param name="target">The <see cref="Type"/> to receive the type reference
        /// expression of.</param>
        /// <param name="identityManager">The <see cref="ICliManager"/>
        /// which is responsible for marshalling type and assembly identities
        /// within the current type model.</param>
        /// <returns>A new <see cref="ITypeReferenceExpression"/>.</returns>
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
        /// <returns>A new <see cref="ITypeReferenceExpression"/> which wraps the <paramref name="target"/>
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

        #region Typeof Expression Extensions

        /// <summary>
        /// Obtains a <see cref="TypeOfExpression"/> for the <paramref name="target"/> provided.
        /// </summary>
        /// <param name="target">The <see cref="Type"/> which needs a typeof expression.</param>
        /// <param name="identityManager">The <see cref="ICliManager "/> which marshals
        /// identities of types within the type model.</param>
        /// <returns>A new <see cref="ITypeOfExpression"/> instance which obtains the 
        /// <see cref="RuntimeTypeHandle"/> of the provided <paramref name="target"/> at 
        /// runtime.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="target"/> is null.</exception>
        public static ITypeOfExpression TypeOf(this Type target, ICliManager identityManager)
        {
            if (target == null)
                throw new ArgumentNullException("target");
            return identityManager.ObtainTypeReference(target).TypeOf();
        }
        #endregion

        #region Symbol Expressions

        public static IExpressionFusionExpression Fuse(this Type target, string term, ICliManager identityManager)
        {
            return identityManager.ObtainTypeReference(target).Fuse(term);
        }

        public static IExpressionFusionExpression Fuse(this Type target, IFusionTermExpression term, ICliManager identityManager)
        {
            return ((IFusionTargetExpression)target.GetTypeExpression(identityManager)).Fuse(term);
        }
        public static IExpressionToCommaTypeReferenceFusionExpression Fuse(this IFusionTypeCollectionTargetExpression target, ICliManager identityManager, params Type[] types)
        {
            return new ExpressionToCommaTypeReferenceFusionExpression(target, types.ToCollection(identityManager));
        }

        #endregion
    }
}
