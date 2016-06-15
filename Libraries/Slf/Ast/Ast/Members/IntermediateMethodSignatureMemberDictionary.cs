using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AllenCopeland.Abstraction.Slf._Internal;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
//using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Utilities.Events;
using AllenCopeland.Abstraction.Utilities.Collections;
using System.Diagnostics;
using AllenCopeland.Abstraction.Slf.Ast.Statements;
/*----------------------------------------\
| Copyright © 2012 Allen Copeland Jr.     |
|-----------------------------------------|
| The Abstraction Project's code is prov- |
| -ided under a contract-release basis.   |
| DO NOT DISTRIBUTE and do not use beyond |
| the contract terms.                     |
\--------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Members
{
    public abstract class IntermediateMethodSignatureMemberDictionary<TSignature, TIntermediateSignature, TSignatureParent, TIntermediateSignatureParent> :
        IntermediateGroupedMethodSignatureMemberDictionary<IMethodSignatureParameterMember<TSignature, TSignatureParent>, IIntermediateMethodSignatureParameterMember<TSignature, TIntermediateSignature, TSignatureParent, TIntermediateSignatureParent>, TSignature, TIntermediateSignature, TSignatureParent, TIntermediateSignatureParent>,
        IIntermediateMethodSignatureMemberDictionary<TSignature, TIntermediateSignature, TSignatureParent, TIntermediateSignatureParent>
        where TSignature :
            IMethodSignatureMember<TSignature, TSignatureParent>
        where TIntermediateSignature :
            TSignature,
            IIntermediateMethodSignatureMember<TSignature, TIntermediateSignature, TSignatureParent, TIntermediateSignatureParent>
        where TSignatureParent :
            IMethodSignatureParent<TSignature, TSignatureParent>
        where TIntermediateSignatureParent :
            class,
            IIntermediateMethodSignatureParent<TSignature, TIntermediateSignature, TSignatureParent, TIntermediateSignatureParent>,
            TSignatureParent
    {
        protected IntermediateMethodSignatureMemberDictionary(IntermediateFullMemberDictionary master, TIntermediateSignatureParent parent, ITypeIdentityManager identityManager)
            : base(master, parent, identityManager)
        {
        }
        protected IntermediateMethodSignatureMemberDictionary(IntermediateFullMemberDictionary master, TIntermediateSignatureParent parent, IntermediateMethodSignatureMemberDictionary<TSignature, TIntermediateSignature, TSignatureParent, TIntermediateSignatureParent> root)
            : base(master, parent, root)
        {
        }
    }

    public abstract class IntermediateGroupedMethodSignatureMemberDictionary<TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TSignatureParent, TIntermediateSignatureParent> :
        IntermediateGroupedSignatureMemberDictionary<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter, TSignatureParent, TIntermediateSignatureParent>,
        IIntermediateMethodSignatureMemberDictionary<TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TSignatureParent, TIntermediateSignatureParent>,
        IIntermediateMethodSignatureMemberDictionary
        where TSignatureParameter :
            IMethodSignatureParameterMember<TSignatureParameter, TSignature, TSignatureParent>
        where TIntermediateSignatureParameter :
            IIntermediateMethodSignatureParameterMember<TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TSignatureParent, TIntermediateSignatureParent>,
            TSignatureParameter
        where TSignature :
            IMethodSignatureMember<TSignatureParameter, TSignature, TSignatureParent>
        where TIntermediateSignature :
            IIntermediateMethodSignatureMember<TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TSignatureParent, TIntermediateSignatureParent>,
            TSignature
        where TSignatureParent :
            ISignatureParent<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TSignatureParameter, TSignatureParent>
        where TIntermediateSignatureParent :
            class,
            IIntermediateSignatureParent<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter, TSignatureParent, TIntermediateSignatureParent>,
            TSignatureParent
    {
        private ITypeIdentityManager identityManager;
        protected IntermediateGroupedMethodSignatureMemberDictionary(IntermediateFullMemberDictionary master, TIntermediateSignatureParent parent, ITypeIdentityManager identityManager)
            : base(master, parent)
        {
            this.identityManager = identityManager;
        }
        protected IntermediateGroupedMethodSignatureMemberDictionary(IntermediateFullMemberDictionary master, TIntermediateSignatureParent parent, IntermediateGroupedMethodSignatureMemberDictionary<TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TSignatureParent, TIntermediateSignatureParent> root)
            : base(master, parent, root)
        {
            this.identityManager = root.identityManager;
        }
        protected TIntermediateSignature GetNewMethod(string name)
        {
            return this.OnGetNewMethod(name);
        }

        protected abstract TIntermediateSignature OnGetNewMethod(string name);
        private TIntermediateSignature GetNewMethodWithParameters(string name, TypedNameSeries parameters)
        {
            return this.GetNewMethodWithParametersAndTypeParameters(name, parameters, GenericParameterData.EmptySet);
        }

        /* *
         * Lovely method name, huh?
         * */
        private TIntermediateSignature GetNewMethodWithParametersAndTypeParameters(string name, TypedNameSeries parameters, GenericParameterData[] typeParameters)
        {
            var method = this.GetNewMethod(name);
            if (typeParameters.Length > 0)
                method.TypeParameters.AddRange(typeParameters);
            if (parameters.Count > 0)
            {
                var internalParameters = method.Parameters as IntermediateParameterMemberDictionary<TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter>;
                if (parameters != null)
                {
                    /* Method hasn't been added yet, processing the event requests would be an effort in futility. */
                    internalParameters.SkipEvents = true;
                    internalParameters.AddRange(parameters.ToArray());
                    internalParameters.SkipEvents = false;
                }
                else
                    method.Parameters.AddRange(parameters.ToArray());
            }
            return method;
        }

        /// <summary>
        /// Adds a <typeparamref name="TSignature"/> instance
        /// to the <see cref="IntermediateGroupedMethodSignatureMemberDictionary{TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TSignatureParent, TIntermediateSignatureParent}"/>
        /// by the <paramref name="key"/> provided.
        /// </summary>
        /// <param name="key">The <see cref="String"/> value which represents the key to use to
        /// insert the <paramref name="value"/> provided.</param>
        /// <param name="value">The <typeparamref name="TSignature"/> 
        /// to insert.</param>
        protected internal override void _Add(IGeneralGenericSignatureMemberUniqueIdentifier key, TSignature value)
        {
            if (value == null)
                throw new ArgumentNullException("value");
            if (!(value is TIntermediateSignature))
                throw ThrowHelper.ObtainArgumentException(ArgumentWithException.value, ExceptionMessageId.ValueIsWrongType, ThrowHelper.GetArgumentName(ArgumentWithException.value), value.GetType().ToString(), typeof(TIntermediateSignature).ToString());
            base._Add(key, value);
        }

        #region IIntermediateMethodSignatureMemberDictionary<TSignatureParameter,TIntermediateSignatureParameter,TSignature,TIntermediateSignature,TSignatureParent,TIntermediateSignatureParent> Members

        /// <summary>
        /// Adds a new <typeparamref name="TIntermediateSignature"/> with the
        /// <paramref name="name"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> representing the name the method is referred to by.</param>
        /// <returns>A new <typeparamref name="TIntermediateSignature"/> instance
        /// which has a <see cref="System.Void"/> return-type.</returns>
        public TIntermediateSignature Add(string name)
        {
            var method = this.GetNewMethod(name);
            if (method is IBlockStatementParent)
                method.ReturnType = this.identityManager.ObtainTypeReference(RuntimeCoreType.VoidType, ((IBlockStatementParent)method).Assembly);
            else if (method is IIntermediateInterfaceMethodMember)
                method.ReturnType = this.identityManager.ObtainTypeReference(RuntimeCoreType.VoidType, ((IIntermediateInterfaceMethodMember)method).Parent.Assembly);
            else
                method.ReturnType = this.identityManager.ObtainTypeReference(RuntimeCoreType.VoidType);

            this.AddDeclaration(method);
            return method;
        }

        /// <summary>
        /// Adds a new <typeparamref name="TIntermediateSignature"/> with the
        /// <paramref name="name"/> and <paramref name="parameters"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> representing the name the method is referred to by.</param>
        /// <param name="parameters">The <see cref="TypedNameSeries"/> which designates
        /// the parameter names and types of the new <typeparamref name="TIntermediateSignature"/>.</param>
        /// <returns>A new <typeparamref name="TIntermediateSignature"/> instance
        /// which has a <see cref="System.Void"/> return-type.</returns>
        public TIntermediateSignature Add(string name, TypedNameSeries parameters)
        {
            var method = this.GetNewMethodWithParameters(name, parameters);
            method.ReturnType = this.identityManager.ObtainTypeReference(RuntimeCoreType.VoidType);
            this.AddDeclaration(method);
            return method;
        }

        /// <summary>
        /// Adds a new <typeparamref name="TIntermediateSignature"/> with the
        /// <paramref name="name"/>, <paramref name="parameters"/>, and
        /// <paramref name="typeParameters"/>provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> representing the name the method is referred to by.</param>
        /// <param name="parameters">The <see cref="TypedNameSeries"/> which designates
        /// the parameter names and types of the new <typeparamref name="TIntermediateSignature"/>.</param>
        /// <param name="typeParameters">An array of <see cref="GenericParameterData"/> which
        /// defines the type-parameters and their individual type and functional constraints.</param>
        /// <returns>A new <typeparamref name="TIntermediateSignature"/>
        /// which has a <see cref="System.Void"/> return-type.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="name"/> is null, or <paramref name="typeParameters"/>
        /// is null.</exception>
        /// <exception cref="System.ArgumentException">thrown when <paramref name="name"/> is <see cref="String.Empty"/>
        /// or an element of <paramref name="parameters"/>, or <paramref name="typeParameters"/> contains an invalid type-reference (<see cref="System.Void"/>).</exception>
        public TIntermediateSignature Add(string name, TypedNameSeries parameters, params GenericParameterData[] typeParameters)
        {
            var method = this.GetNewMethodWithParametersAndTypeParameters(name, parameters, typeParameters);
            method.ReturnType = this.identityManager.ObtainTypeReference(RuntimeCoreType.VoidType);
            this.AddDeclaration(method);
            return method;
        }

        /// <summary>
        /// Adds a new <typeparamref name="TIntermediateSignature"/>
        /// instance with the <paramref name="nameAndReturn"/> provided.
        /// </summary>
        /// <param name="nameAndReturn">A <see cref="TypedName"/> which designates the name of the method and its
        /// return-type.</param>
        /// <returns>A new <typeparamref name="TIntermediateSignature"/>.</returns>
        public TIntermediateSignature Add(TypedName nameAndReturn)
        {
            var method = this.GetNewMethod(nameAndReturn.Name);
            var returnType = nameAndReturn.GetTypeRef();
            if (returnType.ContainsSymbols())
                method.ReturnType = returnType.SimpleSymbolDisambiguation(method);
            else
                method.ReturnType = returnType;
            this.AddDeclaration(method);
            return method;
        }

        /// <summary>
        /// Adds a new <typeparamref name="TIntermediateSignature"/>
        /// instance with the <paramref name="parameters"/>, <paramref name="nameAndReturn"/> provided.
        /// </summary>
        /// <param name="nameAndReturn">A <see cref="TypedName"/> which designates the name of the method and its
        /// return-type.</param>
        /// <param name="parameters">The <see cref="TypedNameSeries"/> which designates
        /// the parameter names and types of the new <typeparamref name="TIntermediateSignature"/>.</param>
        /// <returns>A new <typeparamref name="TIntermediateSignature"/>.</returns>
        /// <exception cref="System.ArgumentException">thrown when the name portion of <paramref name="nameAndReturn"/> is <see cref="String.Empty"/>
        /// or an element of <paramref name="parameters"/> contains an invalid type-reference (<see cref="System.Void"/>).</exception>
        public TIntermediateSignature Add(TypedName nameAndReturn, TypedNameSeries parameters)
        {
            var method = this.GetNewMethodWithParameters(nameAndReturn.Name, parameters);
            //TIntermediateSignature method = this.Add(nameAndReturn.Name, parameters);
            var returnType = nameAndReturn.GetTypeRef();
            if (returnType.ContainsSymbols())
                method.ReturnType = returnType.SimpleSymbolDisambiguation(method);
            else
                method.ReturnType = returnType;
            this.AddDeclaration(method);
            return method;
        }

        /// <summary>
        /// Adds a new <typeparamref name="TIntermediateSignature"/>
        /// instane with the <paramref name="parameters"/>, <paramref name="typeParameters"/>, <paramref name="nameAndReturn"/>
        /// provided.
        /// </summary>
        /// <param name="nameAndReturn">A <see cref="TypedName"/> which designates the name of the method and its
        /// return-type.</param>
        /// <param name="parameters">The <see cref="TypedNameSeries"/> which designates
        /// the parameter names and types of the new <typeparamref name="TIntermediateSignature"/>.</param>
        /// <param name="typeParameters">An array of <see cref="GenericParameterData"/> which
        /// defines the type-parameters and their individual type and functional constraints.</param>
        /// <returns>A new <typeparamref name="TIntermediateSignature"/>.</returns>
        /// <exception cref="System.ArgumentException">thrown when the name portion of <paramref name="nameAndReturn"/> is <see cref="String.Empty"/>
        /// or an element of <paramref name="parameters"/>, or <paramref name="typeParameters"/> contains an invalid type-reference (<see cref="System.Void"/>).</exception>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="typeParameters"/>
        /// is null.</exception>
        public TIntermediateSignature Add(TypedName nameAndReturn, TypedNameSeries parameters, params GenericParameterData[] typeParameters)
        {
            TIntermediateSignature method = this.Add(nameAndReturn.Name, parameters, typeParameters);
            var returnType = nameAndReturn.GetTypeRef();
            if (returnType.ContainsSymbols())
                method.ReturnType = returnType.SimpleSymbolDisambiguation(method);
            else
                method.ReturnType = returnType;
            return method;
        }

        /// <summary>
        /// Adds a new <typeparamref name="TIntermediateSignature"/> with the <paramref name="name"/>
        /// and <paramref name="signature"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> value which represents the unique identifier associated
        /// to the new <typeparamref name="TIntermediateSignature"/>.</param>
        /// <param name="signature">The <see cref="IDelegateType"/> which denotes the return-type
        /// and parameters of the <typeparamref name="TIntermediateSignature"/>.</param>
        /// <returns>A new <typeparamref name="TIntermediateSignature"/> with the <paramref name="name"/> provided
        /// which has the <paramref name="signature"/> provided.</returns>
        public TIntermediateSignature Add(string name, IDelegateType signature)
        {
            /* *
             * Setup, create a new series of typed names with the type-parameters of the signature
             * replaced with a string-variant.
             * */
            return DictionaryHelpers.AddIntermediateMethodByDelegate<TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TSignatureParent, TIntermediateSignatureParent>(name, signature, this.Add, this.Add);
        }

        #endregion

        #region IIntermediateMethodSignatureMemberDictionary Members

        IIntermediateMethodSignatureMember IIntermediateMethodSignatureMemberDictionary.Add(string name)
        {
            return this.Add(name);
        }

        IIntermediateMethodSignatureMember IIntermediateMethodSignatureMemberDictionary.Add(string name, TypedNameSeries parameters)
        {
            return this.Add(name, parameters);
        }

        IIntermediateMethodSignatureMember IIntermediateMethodSignatureMemberDictionary.Add(string name, TypedNameSeries parameters, params GenericParameterData[] typeParameters)
        {
            return this.Add(name, parameters, typeParameters);
        }

        IIntermediateMethodSignatureMember IIntermediateMethodSignatureMemberDictionary.Add(TypedName nameAndReturn)
        {
            return this.Add(nameAndReturn);
        }

        IIntermediateMethodSignatureMember IIntermediateMethodSignatureMemberDictionary.Add(TypedName nameAndReturn, TypedNameSeries parameters)
        {
            return this.Add(nameAndReturn, parameters);
        }

        IIntermediateMethodSignatureMember IIntermediateMethodSignatureMemberDictionary.Add(TypedName nameAndReturn, TypedNameSeries parameters, params GenericParameterData[] typeParameters)
        {
            return this.Add(nameAndReturn, parameters, typeParameters);
        }

        IIntermediateMethodSignatureMember IIntermediateMethodSignatureMemberDictionary.Add(string name, IDelegateType signature)
        {
            return this.Add(name, signature);
        }


        #endregion

        #region IMethodSignatureMemberDictionary Members

        int IMethodSignatureMemberDictionary.IndexOf(IMethodSignatureMember method)
        {
            if (!(method is TSignature))
                return -1;
            return this.IndexOf((TSignature)(method));
        }

        #endregion

    }
}
