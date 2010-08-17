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

        public IMethodInvokeExpression Invoke(IExpressionCollection parameters)
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

        /*
        public override IType ForwardType
        {
            get { return typeof(UInt32).GetTypeReference(); }
        }
        protected override void OnLink()
        {
            if (this.Reference.Source.ForwardType is IMethodParent)
            {
                IMethodParent imp = ((IMethodParent)(this.Reference.Source.ForwardType));
                IFilteredSignatureMemberDictionary ifsms = imp.Methods.Find(this.Reference.Name, this.Reference.GenericParameters, false, this.Signature);

                if (ifsms.Count > 0)
                {
                    IDictionaryEnumerator ie = ifsms.GetEnumerator();
                    if (ie.MoveNext())
                        this.associatedMember = (IMethodSignatureMember)ie.Value;
                    ((IDisposable)ie).Dispose();
                }
            }
        }
        */


        public override void Visit(IIntermediateCodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
