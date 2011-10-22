using System;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
/*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Members
{
    /// <summary>
    /// Provides a basic implementation of <see cref="IMethodSignatureMember{TSignatureParameter, TSignature, TSignatureParent}"/>
    /// for working with a method signature.
    /// </summary>
    /// <typeparam name="TSignatureParameter">The type of parameter used in the <typeparamref name="TSignature"/>.</typeparam>
    /// <typeparam name="TSignature">The type of signature used as a parent of <typeparamref name="TSignatureParameter"/> 
    /// instances.</typeparam>
    /// <typeparam name="TSignatureParent">The parent that contains the <typeparamref name="TSignature"/> 
    /// instances.</typeparam>
    internal abstract class MethodSignatureMemberBase<TSignatureParameter, TSignature, TSignatureParent> :
        SignatureMemberBase<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TSignatureParameter, TSignatureParent>,
        IMethodSignatureMember<TSignatureParameter, TSignature, TSignatureParent>
        where TSignatureParameter :
            IMethodSignatureParameterMember<TSignatureParameter, TSignature, TSignatureParent>
        where TSignature :
            IMethodSignatureMember<TSignatureParameter, TSignature, TSignatureParent>
        where TSignatureParent :
            ISignatureParent<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TSignatureParameter, TSignatureParent>
    {
        /// <summary>
        /// Data member for <see cref="TypeParameters"/>.
        /// </summary>
        private IGenericParameterDictionary<IMethodSignatureGenericTypeParameterMember, IMethodSignatureMember> typeParameters;

        /// <summary>
        /// Data member for <see cref="ReturnType"/>.
        /// </summary>
        private IType returnType;

        /// <summary>
        /// Data member for <see cref="GenericParameters"/> 
        /// when  <see cref="canCacheTypeParameters"/> 
        /// is true.
        /// </summary>
        private ILockedTypeCollection genericParameters;

        /// <summary>
        /// Data member used to store the state of whether
        /// the <see cref="MethodSignatureMemberBase{TSignatureParameter, TSignature, TSignatureParent}"/>
        /// can cache its generic type-parameters.
        /// </summary>
        private bool canCacheTypeParameters = false;


        protected MethodSignatureMemberBase(TSignatureParent parent)
            : base(parent)
        {
            this.canCacheTypeParameters = this.CanCacheGenericParameters;
        }

        //public override IGeneralGenericSignatureMemberUniqueIdentifier UniqueIdentifier
        //{
        //    get
        //    {
        //        return this.GetUniqueIdentifier();
        //        //return string.Format("{0} {1}({2})", this.ReturnType.BuildTypeName(true), this.Name, base.UniqueIdentifier);
        //    }
        //}

        #region IGenericParamParent<TParameter, TParent>
        /// <summary>
        /// Returns the <see cref="IGenericParameterDictionary{TGenericParameter, TParent}"/> used by the <see cref="MethodMemberBase{IInterfaceMethodMember, IInterfaceType}"/>.
        /// </summary>
        public IGenericParameterDictionary<IMethodSignatureGenericTypeParameterMember, IMethodSignatureMember> TypeParameters
        {
            get
            {
                if (this.IsGenericConstruct)
                {
                    if (this.IsGenericDefinition)
                    {
                        if (this.typeParameters == null)
                            this.typeParameters = this.InitializeTypeParameters();
                        return this.typeParameters;
                    }
                    else
                        throw new InvalidOperationException("cannot obtain the open type-paramters of a non-open or non-generic method.");
                }
                else
                    throw new InvalidOperationException("Current method isn't a generic method.");
            }
        }
        #endregion

        /// <summary>
        /// Obtains the <see cref="TypeParameters"/> property.
        /// </summary>
        /// <returns>A new <see cref="IGenericParameterDictionary{TParameter, TParent}"/> instance.</returns>
        protected abstract IGenericParameterDictionary<IMethodSignatureGenericTypeParameterMember, IMethodSignatureMember> InitializeTypeParameters();

        /// <summary>
        /// Obtains the <see cref="GenericParameters"/> property.
        /// </summary>
        /// <returns></returns>
        protected abstract ILockedTypeCollection OnGetGenericParameters();

        #region IGenericParamParent Members

        IGenericParamParent IGenericParamParent.MakeGenericClosure(ITypeCollectionBase typeParameters)
        {
            return this.MakeGenericClosure(typeParameters);
        }

        IGenericParamParent IGenericParamParent.MakeGenericClosure(params IType[] typeParameters)
        {
            return this.MakeGenericClosure(typeParameters.ToLockedCollection());
        }

        IGenericParameterDictionary IGenericParamParent.TypeParameters
        {
            get { return (IGenericParameterDictionary)this.TypeParameters; }
        }

        #endregion

        #region IMethodSignatureMember Members

        public bool IsGenericDefinition
        {
            get {
                return this.OnGetGenericDefinition() == null && this.IsGenericConstruct;
            }
        }

        IMethodSignatureMember IMethodSignatureMember.GetGenericDefinition()
        {
            return this.GetGenericDefinition();
        }

        public ILockedTypeCollection GenericParameters
        {
            get
            {
                if (this.canCacheTypeParameters)
                {
                    if (this.genericParameters == null)
                        this.genericParameters = this.OnGetGenericParameters();
                    return this.genericParameters;
                }
                else
                    return this.OnGetGenericParameters();
            }
        }

        /// <summary>
        /// Returns whether the <see cref="MethodSignatureMemberBase{TSignatureParameter, TSignature, TSignatureParent}"/> is a generic method.
        /// </summary>
        public abstract bool IsGenericConstruct { get; }

        #endregion

        /// <summary>
        /// Returns a <see cref="String"/> that represents the current <see cref="MethodSignatureMemberBase{TSignatureParameter, TSignature, TSignatureParent}"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="String"/> that represents the current <see cref="MethodSignatureMemberBase{TSignatureParameter, TSignature, TSignatureParent}"/>.
        /// </returns>
        public override string ToString()
        {
            return this.UniqueIdentifier.ToString();
        }

        #region IMethodSignatureMember<TSignatureParameter,TSignature,TSignatureParent> Members

        public IType ReturnType
        {
            get
            {
                if (!this.CanCacheReturn)
                    return this.returnType = this.OnGetReturnType();
                if (this.returnType == null)
                    this.returnType = this.OnGetReturnType();
                return this.returnType;
            }
        }

        #endregion

        /// <summary>
        /// Returns whether the <see cref="MethodSignatureMemberBase{TSignatureParameter, TSignature, TSignatureParent}"/>
        /// can cache the <see cref="ReturnType"/>.
        /// </summary>
        protected abstract bool CanCacheReturn { get; }

        /// <summary>
        /// Returns whether the <see cref="MethodSignatureMemberBase{TSignatureParameter, TSignature, TSignatureParent}"/>
        /// can cache the <see cref="GenericParameters"/>.
        /// </summary>
        /// <remarks>Generic method variants do not 
        /// call this method and always cache the 
        /// generic parameters passed to the 
        /// constructor.</remarks>
        protected abstract bool CanCacheGenericParameters { get; }

        /// <summary>
        /// Obtains the <see cref="IType"/> that the <see cref="MethodSignatureMemberBase{TSignatureParameter, TSignature, TSignatureParent}"/>
        /// yields upon return.
        /// </summary>
        protected abstract IType OnGetReturnType();

        #region IMethodSignatureMember<TSignatureParameter,TSignature,TSignatureParent> Members


        public abstract TSignature MakeGenericClosure(ITypeCollectionBase genericReplacements);

        public TSignature MakeGenericClosure(params IType[] typeParameters)
        {
            return this.MakeGenericClosure(typeParameters.ToLockedCollection());
        }

        IMethodSignatureMember IGenericParamParent<IMethodSignatureGenericTypeParameterMember, IMethodSignatureMember>.MakeGenericClosure(ITypeCollectionBase typeParameters)
        {
            return this.MakeGenericClosure(typeParameters);
        }

        IMethodSignatureMember IGenericParamParent<IMethodSignatureGenericTypeParameterMember, IMethodSignatureMember>.MakeGenericClosure(params IType[] typeParameters)
        {
            return this.MakeGenericClosure(typeParameters.ToLockedCollection());
        }
        /// <summary>
        /// Returns the original generic form of the current
        /// <see cref="MethodSignatureMemberBase{TSignatureParameter, TSignature, TSignatureParent}"/> 
        /// generic variant.
        /// </summary>
        /// <returns>A <typeparamref name="TSignature"/> 
        /// which denotes the original generic variant
        /// of the current <see cref="MethodSignatureMemberBase{TSignatureParameter, TSignature, TSignatureParent}"/>.</returns>
        /// <exception cref="System.InvalidOperationException">
        /// Thrown when the <see cref="MethodSignatureMemberBase{TSignatureParameter, TSignature, TSignatureParent}"/>
        /// is not a generic variant or is already in 
        /// generic form.</exception>
        public TSignature GetGenericDefinition()
        {
            if (!this.IsGenericConstruct)
                throw new InvalidOperationException("Cannot obtain the generic definition of a non-generic method.");
            if (this.IsGenericDefinition)
                throw new InvalidOperationException("Method is already in generic form.");
            return this.OnGetGenericDefinition();
        }
        #endregion

        protected abstract TSignature OnGetGenericDefinition();

        public bool ContainsGenericParameters
        {
            get
            {
                return this.ContainsGenericParameters();
            }
        }

    }
}
