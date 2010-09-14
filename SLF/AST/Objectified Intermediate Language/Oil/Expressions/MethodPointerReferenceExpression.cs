using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil;
using AllenCopeland.Abstraction.Slf.Oil.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    internal class MethodPointerReferenceExpression<TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TParent, TIntermediateParent> :
        MethodPointerReferenceExpression,
        IMethodPointerReferenceExpression<TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TParent, TIntermediateParent>
        where TSignatureParameter :
            IMethodSignatureParameterMember<TSignatureParameter, TSignature, TParent>
        where TIntermediateSignatureParameter :
            TSignatureParameter,
            IIntermediateMethodSignatureParameterMember<TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TParent, TIntermediateParent>
        where TSignature :
            IMethodSignatureMember<TSignatureParameter, TSignature, TParent>
        where TIntermediateSignature :
            TSignature,
            IIntermediateMethodSignatureMember<TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TParent, TIntermediateParent>
        where TParent :
            ISignatureParent<TSignature, TSignatureParameter, TParent>
        where TIntermediateParent :
            TParent,
            IIntermediateSignatureParent<TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter, TParent, TIntermediateParent>
    {
        /// <summary>
        /// Creates a new <see cref="MethodPointerReferenceExpression"/>
        /// with the <paramref name="reference"/> and 
        /// <paramref name="signature"/> provided.
        /// </summary>
        /// <param name="reference">The <see cref="IMethodReferenceStub"/>
        /// from which the <see cref="MethodPointerReferenceExpression"/>
        /// was created.</param>
        /// <param name="signature">The <see cref="ITypeCollection"/>
        /// of <paramref name="IType"/> elements relative to the
        /// method's signature.</param>
        internal MethodPointerReferenceExpression(IMethodReferenceStub<TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TParent, TIntermediateParent> reference, SignatureTypes signatureTypes)
            : base(reference, signatureTypes)
        {
        }

        internal MethodPointerReferenceExpression(IMethodReferenceStub<TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TParent, TIntermediateParent> reference, ITypeCollection signatureTypes)
            : base(reference, signatureTypes)
        {

        }

        internal class SignatureTypes :
            ITypeCollection
        {
            private TIntermediateSignature source;
            public SignatureTypes(TIntermediateSignature source)
            {

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
                    this.source.Parameters[index].Value.ParameterType = value;
                }
            }

            public void Insert(int index, IType item)
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

            #region ITypeCollectionBase Members

            public int IndexOf(IType item)
            {
                int index = 0;
                foreach (var element in this.source.Parameters.Values)
                    if (element.ParameterType == item)
                        return index;
                    else
                        index++;
                return -1;
            }

            #endregion

            #region IControlledStateCollection<IType> Members

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
                if (this.Count + arrayIndex > array.Length)
                    throw new ArgumentException(string.Format("array cannot contain necessary elements based off of arrayIndex ({0}) provided", arrayIndex), "array");
                if (arrayIndex < 0)
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
        }

        #region IMethodPointerReferenceExpression<TSignatureParameter,TIntermediateSignatureParameter,TSignature,TIntermediateSignature,TParent,TIntermediateParent> Members

        public new IMethodReferenceStub<TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TParent, TIntermediateParent> Reference
        {
            get { return (IMethodReferenceStub<TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TParent, TIntermediateParent>)base.Reference; }
        }

        #endregion
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
        /// of <paramref name="IType"/> elements relative to the
        /// method's signature.</param>
        public MethodPointerReferenceExpression(IMethodReferenceStub reference, ITypeCollection signature)
        {
            this.reference = reference;
            this.signature = signature == null ? ((ITypeCollection)(new TypeCollection())) : signature;
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
        public ITypeCollection Signature
        {
            get { return this.signature; }
        }

        public IMethodInvokeExpression Invoke(IExpressionCollection<IExpression> parameters)
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
        /// <remarks>Returns <see cref="ExpressionType.MethodReference"/>.</remarks>
        public override ExpressionKind Type
        {
            get { return ExpressionKinds.MethodReference; }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (this.Reference != null)
                sb.Append(this.Reference.ToString());
            sb.Append("(");
            if (this.Signature != null && this.Signature.Count > 0)
            {
                bool first = true;
                foreach (IType t in this.Signature)
                {
                    if (first)
                        first = false;
                    else
                        sb.Append(", ");
                    sb.Append(t.Name);
                }
            }
            sb.Append(")");
            return sb.ToString();
        }

        public override void Visit(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
