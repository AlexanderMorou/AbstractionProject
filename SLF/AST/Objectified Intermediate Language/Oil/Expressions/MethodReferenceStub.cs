using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Oil;
using AllenCopeland.Abstraction.Slf.Oil.Members;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    internal class MethodReferenceStub<TSignatureParameter, TSignature, TParent> :
        MethodReferenceStubBase,
        IMethodReferenceStub<TSignatureParameter, TSignature, TParent>
        where TSignatureParameter :
            IMethodSignatureParameterMember<TSignatureParameter, TSignature, TParent>
        where TSignature :
            IMethodSignatureMember<TSignatureParameter, TSignature, TParent>
        where TParent :
            ISignatureParent<TSignature, TSignatureParameter, TParent>
    {
        private Func<MethodPointerReferenceExpression<TSignatureParameter, TSignature, TParent>.SignatureTypes> signatureTypesObtainer;

        public TSignature Member { get; private set; }
        /// <summary>
        /// Creates a new <see cref="UnboundMethodReferenceStub"/> with the 
        /// <paramref name="source"/>, <paramref name="member"/>, 
        /// <paramref name="genericParameters"/> and 
        /// <paramref name="referenceType"/> provdied.
        /// </summary>
        /// <param name="source">The <see cref="IMemberParentReferenceExpression"/>
        /// from which the <see cref="UnboundMethodReferenceStub"/> was sourced.</param>
        /// <param name="member">The <typeparamref name="TSignature"/>
        /// to reference.</param>
        /// <param name="genericParameters">The <see cref="ITypeCollection"/>
        /// of generic parameter replacements for the signature.</param>
        /// <param name="referenceType">The means to refer to
        /// the method.</param>
        /// <param name="signatureTypesObtainer">The <see cref="Func{TResult}"/> which obtains the signature
        /// of the <paramref name="member"/>.</param>
        public MethodReferenceStub(IMemberParentReferenceExpression source, TSignature member, ITypeCollectionBase genericParameters, MethodReferenceType referenceType, Func<MethodPointerReferenceExpression<TSignatureParameter, TSignature, TParent>.SignatureTypes> signatureTypesObtainer)
            : base(source, genericParameters, referenceType)
        {
            this.Member = member;
            this.signatureTypesObtainer = signatureTypesObtainer;
        }

        /// <summary>
        /// Creates a new <see cref="UnboundMethodReferenceStub"/> with the 
        /// <paramref name="source"/>, <paramref name="member"/>, and
        /// <paramref name="genericParameters"/> provdied.
        /// </summary>
        /// <param name="source">The <see cref="IMemberParentReferenceExpression"/>
        /// from which the <see cref="UnboundMethodReferenceStub"/> was sourced.</param>
        /// <param name="member">The <typeparamref name="TSignature"/>
        /// to reference.</param>
        /// <param name="genericParameters">The <see cref="ITypeCollection"/>
        /// of generic parameter replacements for the signature.</param>
        /// <param name="signatureTypesObtainer">The <see cref="Func{TResult}"/> which obtains the signature
        /// of the <paramref name="member"/>.</param>
        public MethodReferenceStub(IMemberParentReferenceExpression source, TSignature member, ITypeCollectionBase genericParameters, Func<MethodPointerReferenceExpression<TSignatureParameter, TSignature, TParent>.SignatureTypes> signatureTypesObtainer)
            : base(source, genericParameters)
        {
            this.Member = member;
            this.signatureTypesObtainer = signatureTypesObtainer;
        }

        /// <summary>
        /// Creates a new <see cref="UnboundMethodReferenceStub"/> with
        /// the <paramref name="source"/>, and <paramref name="member"/> 
        /// provided.
        /// </summary>
        /// <param name="source">The <see cref="IMemberParentReferenceExpression"/>
        /// from which the <see cref="UnboundMethodReferenceStub"/> was sourced.</param>
        /// <param name="member">The <typeparamref name="TSignature"/>
        /// to reference.</param>
        /// <param name="signatureTypesObtainer">The <see cref="Func{TResult}"/> which obtains the signature
        /// of the <paramref name="member"/>.</param>
        public MethodReferenceStub(IMemberParentReferenceExpression source, TSignature member, Func<MethodPointerReferenceExpression<TSignatureParameter, TSignature, TParent>.SignatureTypes> signatureTypesObtainer)
            : base(source)
        {
            this.Member = member;
            this.signatureTypesObtainer = signatureTypesObtainer;
        }

        /// <summary>
        /// Creates a new <see cref="UnboundMethodReferenceStub"/> with the 
        /// <paramref name="member"/>, <paramref name="genericParameters"/> 
        /// and <paramref name="referenceType"/> provdied.
        /// </summary>
        /// <param name="member">The <typeparamref name="TSignature"/>
        /// to reference.</param>
        /// <param name="genericParameters">The <see cref="ITypeCollection"/>
        /// of generic parameter replacements for the signature.</param>
        /// <param name="referenceType">The means to refer to
        /// the method.</param>
        /// <param name="signatureTypesObtainer">The <see cref="Func{TResult}"/> which obtains the signature
        /// of the <paramref name="member"/>.</param>
        public MethodReferenceStub(TSignature member, ITypeCollectionBase genericParameters, MethodReferenceType referenceType, Func<MethodPointerReferenceExpression<TSignatureParameter, TSignature, TParent>.SignatureTypes> signatureTypesObtainer)
            : base(genericParameters, referenceType)
        {
            this.Member = member;
            this.signatureTypesObtainer = signatureTypesObtainer;
        }

        /// <summary>
        /// Creates a new <see cref="UnboundMethodReferenceStub"/> with the 
        /// <paramref name="member"/>, and <paramref name="genericParameters"/> 
        /// provdied.
        /// </summary>
        /// <param name="member">The <typeparamref name="TSignature"/>
        /// to reference.</param>
        /// <param name="genericParameters">The <see cref="ITypeCollection"/>
        /// of generic parameter replacements for the signature.</param>
        /// <param name="signatureTypesObtainer">The <see cref="Func{TResult}"/> which obtains the signature
        /// of the <paramref name="member"/>.</param>
        public MethodReferenceStub(TSignature member, ITypeCollectionBase genericParameters, Func<MethodPointerReferenceExpression<TSignatureParameter, TSignature, TParent>.SignatureTypes> signatureTypesObtainer)
            : base(genericParameters)
        {
            this.Member = member;
            this.signatureTypesObtainer = signatureTypesObtainer;
        }

        public new string Name
        {
            get
            {
                return this.Member.Name;
            }
            internal set
            {
                if (this.Member is IIntermediateMember)
                    (this.Member as IIntermediateMember).Name = value;
            }
        }

        protected override string OnGetName()
        {
            return this.Name;
        }

        protected sealed override MethodPointerReferenceExpression GetPointerReference()
        {
            return new MethodPointerReferenceExpression<TSignatureParameter, TSignature, TParent>(this, this.signatureTypesObtainer());
        }

        protected sealed override MethodPointerReferenceExpression GetPointerReference(ITypeCollection signature)
        {
            return new MethodPointerReferenceExpression<TSignatureParameter, TSignature, TParent>(this, signature);
        }

        #region IMethodReferenceStub<TSignatureParameter,TSignatureParameter,TSignature,TSignature,TParent,TIntermediateParent> Members

        public new IMethodPointerReferenceExpression<TSignatureParameter, TSignature, TParent> GetPointer(ITypeCollection signature)
        {
            return (IMethodPointerReferenceExpression<TSignatureParameter, TSignature, TParent>)base.GetPointer(signature);
        }

        public new IMethodPointerReferenceExpression<TSignatureParameter, TSignature, TParent> GetPointer(params IType[] signature)
        {
            return (IMethodPointerReferenceExpression<TSignatureParameter, TSignature, TParent>)base.GetPointer(signature);
        }

        public new IMethodPointerReferenceExpression<TSignatureParameter, TSignature, TParent> GetPointer()
        {
            return (MethodPointerReferenceExpression<TSignatureParameter, TSignature, TParent>)this.GetPointerReference();
        }

        #endregion

        #region IBoundMemberReference Members

        IType IBoundMemberReference.MemberType
        {
            get { return this.Member.ReturnType; }
        }

        IMember IBoundMemberReference.Member
        {
            get { return this.Member; }
        }

        #endregion

    }

    public class UnboundMethodReferenceStub :
        MethodReferenceStubBase,
        IUnboundMethodReferenceStub
    {
        /// <summary>
        /// Data member for <see cref="Name"/>.
        /// </summary>
        private string name;

        /// <summary>
        /// Creates a new <see cref="UnboundMethodReferenceStub"/> with the 
        /// <paramref name="source"/>, <paramref name="name"/>, 
        /// <paramref name="genericParameters"/> and 
        /// <paramref name="referenceType"/> provdied.
        /// </summary>
        /// <param name="source">The <see cref="IMemberParentReferenceExpression"/>
        /// from which the <see cref="UnboundMethodReferenceStub"/> was sourced.</param>
        /// <param name="name">A <see cref="System.String"/>
        /// relative to the name of the method.</param>
        /// <param name="genericParameters">The <see cref="ITypeCollection"/>
        /// of generic parameter replacements for the signature.</param>
        /// <param name="referenceType">The means to refer to
        /// the method.</param>
        public UnboundMethodReferenceStub(IMemberParentReferenceExpression source, string name, ITypeCollectionBase genericParameters, MethodReferenceType referenceType)
            : base(source, genericParameters, referenceType)
        {
            this.name = name;
        }

        /// <summary>
        /// Creates a new <see cref="UnboundMethodReferenceStub"/> with the 
        /// <paramref name="source"/>, <paramref name="name"/>, and
        /// <paramref name="genericParameters"/> provdied.
        /// </summary>
        /// <param name="source">The <see cref="IMemberParentReferenceExpression"/>
        /// from which the <see cref="UnboundMethodReferenceStub"/> was sourced.</param>
        /// <param name="name">A <see cref="System.String"/>
        /// relative to the name of the method.</param>
        /// <param name="genericParameters">The <see cref="ITypeCollection"/>
        /// of generic parameter replacements for the signature.</param>
        public UnboundMethodReferenceStub(IMemberParentReferenceExpression source, string name, ITypeCollectionBase genericParameters)
            : base(source, genericParameters)
        {
            this.name = name;
        }

        /// <summary>
        /// Creates a new <see cref="UnboundMethodReferenceStub"/> with
        /// the <paramref name="source"/>, and <paramref name="name"/> 
        /// provided.
        /// </summary>
        /// <param name="source">The <see cref="IMemberParentReferenceExpression"/>
        /// from which the <see cref="UnboundMethodReferenceStub"/> was sourced.</param>
        /// <param name="name">A <see cref="System.String"/>
        /// relative to the name of the method.</param>
        public UnboundMethodReferenceStub(IMemberParentReferenceExpression source, string name)
            : base(source)
        {
            this.name = name;
        }

        /// <summary>
        /// Creates a new <see cref="UnboundMethodReferenceStub"/> with the 
        /// <paramref name="name"/>, <paramref name="genericParameters"/> 
        /// and <paramref name="referenceType"/> provdied.
        /// </summary>
        /// <param name="name">A <see cref="System.String"/>
        /// relative to the name of the method.</param>
        /// <param name="genericParameters">The <see cref="ITypeCollection"/>
        /// of generic parameter replacements for the signature.</param>
        /// <param name="referenceType">The means to refer to
        /// the method.</param>
        public UnboundMethodReferenceStub(string name, ITypeCollectionBase genericParameters, MethodReferenceType referenceType)
            : base(genericParameters, referenceType)
        {
            this.name = name;
        }

        /// <summary>
        /// Creates a new <see cref="UnboundMethodReferenceStub"/> with the 
        /// <paramref name="name"/>, and <paramref name="genericParameters"/> 
        /// provdied.
        /// </summary>
        /// <param name="name">A <see cref="System.String"/>
        /// relative to the name of the method.</param>
        /// <param name="genericParameters">The <see cref="ITypeCollection"/>
        /// of generic parameter replacements for the signature.</param>
        public UnboundMethodReferenceStub(string name, ITypeCollectionBase genericParameters)
            : base(genericParameters)
        {
            this.name = name;
        }

        /// <summary>
        /// Creates a new <see cref="UnboundMethodReferenceStub"/> with the 
        /// <paramref name="name"/>, and 
        /// <paramref name="referenceType"/> provdied.
        /// </summary>
        /// <param name="name">A <see cref="System.String"/>
        /// relative to the name of the method.</param>
        /// <param name="referenceType">The means to refer to
        /// the method.</param>
        public UnboundMethodReferenceStub(string name, MethodReferenceType referenceType)
            : base(source:null, referenceType:referenceType)
        {
            this.name = name;
        }

        /// <summary>
        /// Creates a new <see cref="UnboundMethodReferenceStub"/> with
        /// the  <paramref name="name"/> 
        /// provided.
        /// </summary>
        /// <param name="name">A <see cref="System.String"/>
        /// relative to the name of the method.</param>
        public UnboundMethodReferenceStub(string name)
        {
            this.name = name;
        }
        public new string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }

        public new MethodReferenceType ReferenceType
        {
            get
            {
                return base.ReferenceType;
            }
            set
            {
                base.ReferenceType = value;
            }
        }

        protected override string OnGetName()
        {
            return this.Name;
        }
    }

    /// <summary>
    /// Provides a base implementation for <see cref="IMethodReferenceStub"/>
    /// </summary>
    /// <remarks>Simpler form of 
    /// <see cref="IMethodPointerReferenceExpression"/>,
    /// used to obtain initial context data
    /// used to make a lookup.</remarks>
    public abstract class MethodReferenceStubBase :
        IMethodReferenceStub
    {
        /// <summary>
        /// Data member for <see cref="ReferenceType"/>.
        /// </summary>
        private MethodReferenceType referenceType;

        /// <summary>
        /// Data member for <see cref="GenericParameters"/>.
        /// </summary>
        private ILockedTypeCollection genericParameters;

        /// <summary>
        /// Data member for <see cref="Source"/>.
        /// </summary>
        private IMemberParentReferenceExpression source;

        /// <summary>
        /// Creates a new <see cref="UnboundMethodReferenceStub"/> with the 
        /// <paramref name="source"/>, 
        /// <paramref name="genericParameters"/> and 
        /// <paramref name="referenceType"/> provdied.
        /// </summary>
        /// <param name="source">The <see cref="IMemberParentReferenceExpression"/>
        /// from which the <see cref="UnboundMethodReferenceStub"/> was sourced.</param>
        /// <param name="genericParameters">The <see cref="ITypeCollection"/>
        /// of generic parameter replacements for the signature.</param>
        /// <param name="referenceType">The means to refer to
        /// the method.</param>
        public MethodReferenceStubBase(IMemberParentReferenceExpression source, ITypeCollectionBase genericParameters = null, MethodReferenceType referenceType = MethodReferenceType.VirtualMethodReference)
        {
            if (genericParameters != null)
                this.genericParameters = genericParameters is ILockedTypeCollection ? ((ILockedTypeCollection)(genericParameters)) : genericParameters.ToLockedCollection();
            this.referenceType = referenceType;
            this.source = source;
        }


        /// <summary>
        /// Creates a new <see cref="UnboundMethodReferenceStub"/> with
        /// the <paramref name="source"/>
        /// provided.
        /// </summary>
        /// <param name="source">The <see cref="IMemberParentReferenceExpression"/>
        /// from which the <see cref="UnboundMethodReferenceStub"/> was sourced.</param>
        public MethodReferenceStubBase(IMemberParentReferenceExpression source)
        {
            this.source = source;
        }

        /// <summary>
        /// Creates a new <see cref="UnboundMethodReferenceStub"/> with the 
        /// <paramref name="genericParameters"/> 
        /// and <paramref name="referenceType"/> provdied.
        /// </summary>
        /// <param name="genericParameters">The <see cref="ITypeCollection"/>
        /// of generic parameter replacements for the signature.</param>
        /// <param name="referenceType">The means to refer to
        /// the method.</param>
        public MethodReferenceStubBase(ITypeCollectionBase genericParameters, MethodReferenceType referenceType)
            : this(null, genericParameters, referenceType)
        {
        }

        /// <summary>
        /// Creates a new <see cref="UnboundMethodReferenceStub"/> with the 
        /// <paramref name="genericParameters"/> 
        /// provdied.
        /// </summary>
        /// <param name="genericParameters">The <see cref="ITypeCollection"/>
        /// of generic parameter replacements for the signature.</param>
        public MethodReferenceStubBase(ITypeCollectionBase genericParameters)
            : this(null, genericParameters)
        {
        }
        /// <summary>
        /// Creates a new <see cref="UnboundMethodReferenceStub"/> 
        /// initialized to its default state.
        /// </summary>
        public MethodReferenceStubBase()
        {
        }

        #region IMethodReferenceStub Members

        /// <summary>
        /// Returns the <see cref="IMemberParentReferenceExpression"/>
        /// that sourced the <see cref="IMethodReferenceStub"/>.
        /// </summary>
        public IMemberParentReferenceExpression Source
        {
            get { return this.source; }
        }

        /// <summary>
        /// Returns/sets the type of reference the 
        /// <see cref="UnboundMethodReferenceStub"/> is.
        /// </summary>
        public MethodReferenceType ReferenceType
        {
            get
            {
                return this.referenceType;
            }
            protected set
            {
                this.referenceType = value;
            }
        }

        /// <summary>
        /// Returns the <see cref="ITypeCollection"/> of 
        /// <see cref="IType"/> instances used to replace
        /// the generic parameters of the method.
        /// </summary>
        public ILockedTypeCollection GenericParameters
        {
            get { return this.genericParameters; }
        }

        /// <summary>
        /// Returns/sets the name of the method associated
        /// to the <see cref="IMethodReferenceStub"/>.
        /// </summary>
        public string Name
        {
            get
            {
                return this.OnGetName();
            }
        }

        protected abstract string OnGetName();

        /// <summary>
        /// Obtains a <see cref="IMethodInvokeExpression"/>
        /// by evaluating the <paramref name="parameters"/>
        /// provided.
        /// </summary>
        /// <param name="parameters">A series of 
        /// <see cref="IExpression"/> elements 
        /// which relate to the data of the 
        /// parameters of the invoke, and the 
        /// types of the parameters.</param>
        /// <returns>A new <see cref="IMethodInvokeExpression"/> 
        /// relative to the signature and data of 
        /// the <paramref name="parameters"/> 
        /// provided.</returns>
        public IMethodInvokeExpression Invoke(IExpressionCollection<IExpression> parameters)
        {
            return GetPointerReference().Invoke(parameters);
        }

        protected virtual MethodPointerReferenceExpression GetPointerReference()
        {
            return new MethodPointerReferenceExpression(this);
        }

        protected virtual MethodPointerReferenceExpression GetPointerReference(ITypeCollection signature)
        {
            return new MethodPointerReferenceExpression(this, signature);
        }

        /// <summary>
        /// Obtains a <see cref="IMethodInvokeExpression"/>
        /// by evaluating the <paramref name="parameters"/>
        /// provided.
        /// </summary>
        /// <param name="parameters">A series of
        /// <see cref="IExpression"/> elements 
        /// which relate to the data of the 
        /// parameters of the invoke, and the types 
        /// of the parameters.</param>
        /// <returns>A new <see cref="IMethodInvokeExpression"/> 
        /// relative to the signature and data of 
        /// the <paramref name="parameters"/> 
        /// provided.</returns>
        public IMethodInvokeExpression Invoke(params IExpression[] parameters)
        {
            if (parameters == null ||
                parameters.Length == 0)
                return this.Invoke(ExpressionCollection.Empty);
            return this.Invoke(parameters.ToCollection());
        }

        /// <summary>
        /// Obtains a <see cref="IMethodPointerReferenceExpression"/>
        /// with the <paramref name="signature"/> provided.
        /// </summary>
        /// <param name="signature">The <see cref="ITypeCollection"/>
        /// relative to the type-signature of the <see cref="IMethodPointerReferenceExpression"/>
        /// to obtain.</param>
        /// <returns>A new <see cref="IMethodPointerReferenceExpression"/>
        /// relative to the <paramref name="signature"/>
        /// provided.</returns>
        public IMethodPointerReferenceExpression GetPointer(ITypeCollection signature)
        {
            if (signature == null)
                return this.GetPointerReference();
            else
                return this.GetPointerReference(signature);
        }

        /// <summary>
        /// Obtains a <see cref="IMethodPointerReferenceExpression"/>
        /// with the <paramref name="signature"/> provided.
        /// </summary>
        /// <param name="signature">The series if <see cref="IType"/>
        /// elements relative to the type-signature of the 
        /// <see cref="IMethodPointerReferenceExpression"/>
        /// to obtain.</param>
        /// <returns>A new <see cref="IMethodPointerReferenceExpression"/>
        /// relative to the <paramref name="signature"/>
        /// provided.</returns>
        public IMethodPointerReferenceExpression GetPointer(params IType[] signature)
        {
            return this.GetPointer(signature.ToCollection());
        }

        public IMethodPointerReferenceExpression GetPointer()
        {
            return this.GetPointerReference();
        }

        #endregion

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (this.Source != null)
            {
                sb.Append(this.Source.ToString());
                sb.Append(".");
            }
            sb.Append(this.Name);
            if (this.genericParameters != null && this.genericParameters.Count > 0)
            {
                bool first = true;
                sb.Append("<");
                foreach (IType t in this.GenericParameters)
                {
                    if (first)
                        first = false;
                    else
                        sb.Append(", ");
                    sb.Append(t.Name);
                }
                sb.Append(">");
            }
            return sb.ToString();
        }
    }
}
