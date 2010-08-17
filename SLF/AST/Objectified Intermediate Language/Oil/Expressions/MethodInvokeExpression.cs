using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil;
using AllenCopeland.Abstraction.Slf.Oil.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    /// <summary>
    /// Provides a base implementation of a method invocation expression.
    /// </summary>
    public class MethodInvokeExpression :
        MemberParentReferenceExpressionBase,
        IMethodInvokeExpression
    {
        #region MethodInvokeExpression Data Members
        /// <summary>
        /// Data member for <see cref="Parameters"/>.
        /// </summary>
        private IMalleableExpressionCollection parameters;
        /// <summary>
        /// Data member for <see cref="Reference"/>.
        /// </summary>
        private IMethodPointerReferenceExpression reference;
        #endregion

        #region MethodInvokeExpression Constructors
        /// <summary>
        /// Creates a new <see cref="MethodInvokeExpression"/> with 
        /// the <paramref name="parameters"/>, and <paramref name="reference"/>
        /// provided.
        /// </summary>
        /// <param name="reference">A <see cref="IMethodPointerReferenceExpression"/>
        /// which created the <see cref="MethodInvokeExpression"/> and designates
        /// the method target.</param>
        /// <param name="parameters">The series of <see cref="IExpression"/>
        /// elements which relate to the values to send
        /// the method when calling it.</param>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="reference"/> 
        /// or <paramref name="parameters"/> is null.</exception>
        public MethodInvokeExpression(IMethodPointerReferenceExpression reference, IExpressionCollection parameters)
        {
            if (reference == null)
                throw new ArgumentNullException("reference");
            if (parameters == null)
                throw new ArgumentNullException("parameters");
            this.reference = reference;
            foreach (var expression in parameters)
                this.Parameters.Add(expression);
        }

        /// <summary>
        /// Creates a new <see cref="MethodInvokeExpression"/> with 
        /// the <paramref name="parameters"/>, and <paramref name="reference"/>
        /// provided.
        /// </summary>
        /// <param name="reference">A <see cref="IMethodReferenceStub"/>
        /// which created the <see cref="MethodInvokeExpression"/> and designates
        /// the method target whose signature is inferred by the framework
        /// if needed.</param>
        /// <param name="parameters">The series of <see cref="IExpression"/>
        /// elements which relate to the values to send
        /// the method when calling it.</param>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="reference"/> 
        /// or <paramref name="parameters"/> is null.</exception>
        public MethodInvokeExpression(IMethodReferenceStub reference, IExpressionCollection parameters)
        {
            if (reference == null)
                throw new ArgumentNullException("reference");
            if (parameters == null)
                throw new ArgumentNullException("parameters");
            this.reference = new MethodPointerReferenceExpression(reference);
            foreach (var expression in parameters)
                this.Parameters.Add(expression);
        }

        /// <summary>
        /// Creates a new <see cref="MethodInvokeExpression"/> with 
        /// the <paramref name="parameters"/>, and <paramref name="reference"/>
        /// provided.
        /// </summary>
        /// <param name="reference">A <see cref="IMethodReferenceStub"/>
        /// which created the <see cref="MethodInvokeExpression"/> and designates
        /// the method target, whose signature is inferred by the framework
        /// if needed.</param>
        /// <param name="parameters">The series of <see cref="IExpression"/>
        /// elements which relate to the values to send
        /// the method when calling it.</param>
        public MethodInvokeExpression(IMethodReferenceStub reference, params IExpression[] parameters)
            : this(reference, parameters.ToCollection())
        {

        }

        /// <summary>
        /// Creates a new <see cref="MethodInvokeExpression"/> with 
        /// the <paramref name="parameters"/>, and <paramref name="reference"/>
        /// provided.
        /// </summary>
        /// <param name="reference">A <see cref="IMethodPointerReferenceExpression"/>
        /// which created the <see cref="MethodInvokeExpression"/> and designates
        /// the method target.</param>
        /// <param name="parameters">The series of <see cref="IExpression"/>
        /// elements which relate to the values to send
        /// the method when calling it.</param>
        public MethodInvokeExpression(IMethodPointerReferenceExpression reference, params IExpression[] parameters)
            : this(reference, parameters.ToCollection())
        {
        }
        #endregion

        #region IMethodInvokeExpression Members

        /// <summary>
        /// Returns the <see cref="IMethodPointerReferenceExpression"/>
        /// which identifies the name and 
        /// type-parameters of the method 
        /// to use as well as the type-signature
        /// used for the parameters.
        /// </summary>
        public IMethodPointerReferenceExpression Reference
        {
            get { return this.reference; }
        }

        /// <summary>
        /// The <see cref="IMalleableExpressionCollection"/> used
        /// to invoke the method.
        /// </summary>
        /// <remarks>Does not necessarily have to
        /// coincide with the <see cref="IMethodPointerReferenceExpression.Signature"/>
        /// exactly; however, it does need to have necessary
        /// implicit operators if it does not.</remarks>
        public IMalleableExpressionCollection Parameters
        {
            get
            {
                if (this.parameters == null)
                    this.parameters = new MalleableExpressionCollection();
                return this.parameters;
            }
        }

        #endregion
        /*
        /// <summary>
        /// Returns the type which is used as a spring
        /// point for obtaining and linking the members.
        /// </summary>
        /// <remarks>Necessary for every 
        /// <see cref="IMemberParentReferenceExpression"/>
        /// to have in order to properly link.</remarks>
        public override IType ForwardType
        {
            get { return this.Reference.AssociatedMember.ReturnType; }
        }
        /// <summary>
        /// Invoked when <see cref="Link"/> is called.
        /// </summary>
        /// <remarks>Used in case <see cref="Link"/> needs 
        /// hidden and overridden.</remarks>
        protected override void OnLink()
        {
            if (!this.Reference.IsLinked)
                this.Reference.Link();
        }
        */

        /// <summary>
        /// Returns the type of expression the <see cref="MethodInvokeExpression"/> is.
        /// </summary>
        /// <remarks>Returns <see cref="ExpressionType.MethodCall"/>.</remarks>
        public override ExpressionKind Type
        {
            get { return ExpressionKinds.MethodCall; }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (this.Reference != null && this.Reference.Reference != null)
                sb.Append(this.Reference.Reference.ToString());
            sb.Append("(");
            if (this.Parameters != null && this.Parameters.Count > 0)
            {
                bool first = true;
                foreach (IExpression t in this.Parameters)
                {
                    if (first)
                        first = false;
                    else
                        sb.Append(", ");
                    sb.Append(t);
                }
            }
            sb.Append(")");
            return sb.ToString();
        }


        public override void Visit(IIntermediateCodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
