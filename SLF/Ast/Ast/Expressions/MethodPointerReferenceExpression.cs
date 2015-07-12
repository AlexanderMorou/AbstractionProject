using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Ast.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Ast.Expressions
{
    internal class MethodPointerReferenceExpression<TSignatureParameter, TSignature, TParent> :
        MethodPointerReferenceExpression,
        IMethodPointerReferenceExpression<TSignatureParameter, TSignature, TParent>
        where TSignatureParameter :
            IMethodSignatureParameterMember<TSignatureParameter, TSignature, TParent>
        where TSignature :
            IMethodSignatureMember<TSignatureParameter, TSignature, TParent>
        where TParent :
            ISignatureParent<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TSignatureParameter, TParent>
    {
        /// <summary>
        /// Creates a new <see cref="MethodPointerReferenceExpression"/>
        /// with the <paramref name="reference"/> and 
        /// <paramref name="signatureTypes"/> provided.
        /// </summary>
        /// <param name="reference">The <see cref="IMethodReferenceStub"/>
        /// from which the <see cref="MethodPointerReferenceExpression"/>
        /// was created.</param>
        /// <param name="signatureTypes">The <see cref="SignatureTypes"/>
        /// of the <paramref name="reference"/> parameter types.</param>
        internal MethodPointerReferenceExpression(IMethodReferenceStub<TSignatureParameter, TSignature, TParent> reference, SignatureTypes signatureTypes)
            : base(reference, signatureTypes)
        {
        }

        internal MethodPointerReferenceExpression(IMethodReferenceStub<TSignatureParameter, TSignature, TParent> reference, ITypeCollection signatureTypes)
            : base(reference, signatureTypes)
        {

        }

        internal class SignatureTypes :
            ITypeCollection
        {
            private TSignature source;
            public SignatureTypes(TSignature source)
            {
                this.source = source;
            }

            #region ITypeCollection Members

            public void AddRange(IType[] types)
            {
                throw new NotSupportedException();
            }

            public void AddRange(IEnumerable<IType> types)
            {
                throw new NotSupportedException();
            }

            public IType this[int index]
            {
                get
                {
                    return this.source.Parameters[index].Value.ParameterType;
                }
                set
                {
                    if (this.source is IIntermediateMethodMember)
                        ((this.source as IIntermediateMethodMember).Parameters.Values[index] as IIntermediateParameterMember).ParameterType = value;
                }
            }

            public void Insert(int index, IType item)
            {
                throw new NotSupportedException();
            }

            public void RemoveRange(int index, int count)
            {
                throw new NotSupportedException();
            }

            public void RemoveAt(int index)
            {
                throw new NotSupportedException();
            }

            public void Add(IType item)
            {
                throw new NotSupportedException();
            }

            public bool Remove(IType item)
            {
                throw new NotSupportedException();
            }

            public void Clear()
            {
                throw new NotSupportedException();
            }

            #endregion

            #region IControlledTypeCollection Members

            public int IndexOf(IType type)
            {
                int index = 0;
                foreach (var element in this.source.Parameters.Values)
                    if (element.ParameterType == type)
                        return index;
                    else
                        index++;
                return -1;
            }

            #endregion

            #region IControlledCollection<IType> Members

            public int Count
            {
                get { return this.source.Parameters.Count; }
            }

            public bool Contains(IType item)
            {
                foreach (var element in this.source.Parameters.Values)
                    if (element.ParameterType == item)
                        return true;
                return false;
            }

            public void CopyTo(IType[] array, int arrayIndex = 0)
            {
                if (array == null)
                    throw new ArgumentNullException("array");
                if (arrayIndex < 0 || this.Count + arrayIndex > array.Length)
                    throw new ArgumentOutOfRangeException("arrayIndex");
                for (int i = 0; i < this.source.Parameters.Count; i++)
                    array[i + arrayIndex] = this.source.Parameters.Values[i].ParameterType;
            }

            public IType[] ToArray()
            {
                IType[] result = new IType[this.Count];
                for (int i = 0; i < this.source.Parameters.Count; i++)
                    result[i] = this.source.Parameters.Values[i].ParameterType;
                return result;
            }

            #endregion

            #region IEnumerable<IType> Members

            public IEnumerator<IType> GetEnumerator()
            {
                foreach (var parameter in this.source.Parameters.Values)
                    yield return parameter.ParameterType;
            }

            #endregion

            #region IEnumerable Members

            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }

            #endregion

            #region IEquatable<IControlledTypeCollection> Members

            public bool Equals(IControlledTypeCollection other)
            {
                if (other == null)
                    return false;
                if (object.ReferenceEquals(other, this))
                    return true;
                return this.SequenceEqual(other);
            }

            #endregion

            public override bool Equals(object obj)
            {
                if (obj is IControlledTypeCollection)
                    return this.Equals((IControlledTypeCollection)(obj));
                return false;
            }

            public override int GetHashCode()
            {
                return this.Count.GetHashCode();
            }
        }

        #region IMethodPointerReferenceExpression<TSignatureParameter,TIntermediateSignatureParameter,TSignature,TIntermediateSignature,TParent,TIntermediateParent> Members

        public new IMethodReferenceStub<TSignatureParameter, TSignature, TParent> Reference
        {
            get { return (IMethodReferenceStub<TSignatureParameter, TSignature, TParent>)base.Reference; }
        }

        #endregion

        protected override IType TypeLookupAid
        {
            get
            {
                if (this.Reference != null && this.Reference.Member != null)
                    return this.Reference.Member.ReturnType;
                return base.TypeLookupAid;
            }
        }

        public new IMethodInvokeExpression<TSignatureParameter, TSignature, TParent> Invoke(IExpressionCollection<IExpression> parameters)
        {
            return (IMethodInvokeExpression<TSignatureParameter, TSignature, TParent>)base.Invoke(parameters);
        }

        public new IMethodInvokeExpression<TSignatureParameter, TSignature, TParent> Invoke(params IExpression[] parameters)
        {
            return this.Invoke(parameters.ToCollection());
        }

        protected override sealed IMethodInvokeExpression InvokeImpl(IExpressionCollection<IExpression> parameters)
        {
            return new MethodInvokeExpression<TSignatureParameter, TSignature, TParent>(this, parameters);
        }

    }

    public class MethodPointerReferenceExpression :
        ExpressionBase,
        IMethodPointerReferenceExpression
    {
        #region MethodPointerReferenceExpression Data Members
        /// <summary>
        /// Data member for <see cref="Reference"/>.
        /// </summary>
        private IMethodReferenceStub reference;
        /// <summary>
        /// Data member for <see cref="Signature"/>.
        /// </summary>
        private ITypeCollection signature;
        #endregion

        #region MehtodPointerReferenceExpression Constructors
        /// <summary>
        /// Creates a new <see cref="MethodPointerReferenceExpression"/>
        /// with the <paramref name="reference"/> and 
        /// <paramref name="signature"/> provided.
        /// </summary>
        /// <param name="reference">The <see cref="IMethodReferenceStub"/>
        /// from which the <see cref="MethodPointerReferenceExpression"/>
        /// was created.</param>
        /// <param name="signature">The <see cref="ITypeCollection"/>
        /// of <see cref="IType"/> elements relative to the
        /// method's signature.</param>
        public MethodPointerReferenceExpression(IMethodReferenceStub reference, ITypeCollection signature)
        {
            this.reference = reference;
            this.signature = signature;
        }
        /// <summary>
        /// Creates a new <see cref="MethodPointerReferenceExpression"/>
        /// with the <paramref name="reference"/> provided.
        /// </summary>
        /// <param name="reference">The <see cref="IMethodReferenceStub"/>
        /// from which the <see cref="MethodPointerReferenceExpression"/>
        /// was created.</param>
        /// <remarks>The signature of the method will be inferred in this case.</remarks>
        public MethodPointerReferenceExpression(IMethodReferenceStub reference)
            : this(reference, null)
        {
        }
        #endregion

        #region IMethodPointerReferenceExpression Members
        /*
        /// <summary>
        /// Returns the member associated to the 
        /// <see cref="IMemberReferenceExpression"/>.
        /// </summary>
        public IMethodSignatureMember AssociatedMember
        {
            get {
                if (!this.IsLinked)
                    this.Link();
                return this.associatedMember;
            }
        }
        */
        /// <summary>
        /// Returns the <see cref="IMethodReferenceStub"/>
        /// associated to the <see cref="IMethodPointerReferenceExpression"/>.
        /// </summary>
        /// <remarks>Used to provide initial context data 
        /// for the lookup.</remarks>
        public IMethodReferenceStub Reference
        {
            get { return this.reference; }
        }

        /// <summary>
        /// Returns the <see cref="ITypeCollection"/> of
        /// <see cref="IType"/> instances which relate to 
        /// the types of parameters used to
        /// bind to the method.
        /// </summary>
        /// <remarks><para>Can be null if there is no signature associated.</para>
        /// <para>Typical case would be when the method is unbound because the source
        /// has not been resolved, and expression-type resolution has not been performed.</para></remarks>
        public ITypeCollection Signature
        {
            get { return this.signature; }
        }

        public IMethodInvokeExpression Invoke(IExpressionCollection<IExpression> parameters)
        {
            return InvokeImpl(parameters);
        }

        protected virtual IMethodInvokeExpression InvokeImpl(IExpressionCollection<IExpression> parameters)
        {
            return new MethodInvokeExpression(this, parameters);
        }

        public IMethodInvokeExpression Invoke(params IExpression[] parameters)
        {
            return this.Invoke(parameters.ToCollection());
        }

        #endregion

        /// <summary>
        /// Returns the type of expression the <see cref="MethodPointerReferenceExpression"/> is.
        /// </summary>
        /// <remarks>Returns <see cref="ExpressionKind.MethodReference"/>.</remarks>
        public override ExpressionKind Type
        {
            get { return ExpressionKind.MethodReference; }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (this.Reference != null)
                sb.Append(this.Reference.ToString());
            if (this.Signature != null && this.Signature.Count > 0)
            {
                sb.Append("(");
                bool first = true;
                foreach (IType t in this.Signature)
                {
                    if (first)
                        first = false;
                    else
                        sb.Append(", ");
                    sb.Append(t.Name);
                }
                sb.Append(")");
            }
            else
            {
                sb.Append("()");
            }
            return sb.ToString();
        }

        public override TResult Visit<TContext, TResult>(IExpressionVisitor<TResult, TContext> visitor, TContext context)
        {
            return visitor.Visit(this, context);
        }

        public override void Visit(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
