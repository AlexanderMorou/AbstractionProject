using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    public class UnboundPropertyReferenceExpression :
        MemberParentReferenceExpressionBase,
        IUnboundPropertyReferenceExpression 
    {
        /// <summary>
        /// Data member for <see cref="Name"/>.
        /// </summary>
        private string name;
        /// <summary>
        /// Data member for <see cref="Source"/>.
        /// </summary>
        private IMemberParentReferenceExpression source;

        /// <summary>
        /// Creates a new <see cref="UnboundPropertyReferenceExpression"/> with the <paramref name="name"/>
        /// and <paramref name="source"/> provided.
        /// </summary>
        /// <param name="name">A <see cref="System.String"/> relative to the
        /// property to retrieve a reference to.</param>
        /// <param name="source">The <see cref="IMemberParentReferenceExpression"/>
        /// from which the <see cref="UnboundPropertyReferenceExpression"/> is to be
        /// sourced.</param>
        public UnboundPropertyReferenceExpression(string name, IMemberParentReferenceExpression source)
        {
            this.name = name;
            this.source = source;
        }

        #region IPropertyReferenceExpression Members
        /// <summary>
        /// Returns/sets the type of reference to the 
        /// <see cref="IPropertyReferenceExpression"/>,
        /// get/set methods, is.
        /// </summary>
        public MethodReferenceType ReferenceType { get; set; }

        MethodReferenceType IPropertyReferenceExpression.ReferenceType
        {
            get
            {
                return this.ReferenceType;
            }
        }

        /// <summary>
        /// Returns the <see cref="IMemberParentReferenceExpression"/>
        /// that sourced the <see cref="UnboundPropertyReferenceExpression"/>.
        /// </summary>
        public IMemberParentReferenceExpression Source
        {
            get { return this.source; }
        }

        #endregion

        #region IMemberReferenceExpression Members

        /// <summary>
        /// Returns/sets the name of the member to reference.
        /// </summary>
        public string Name
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

        #endregion

        /// <summary>
        /// Returns the type of expression the <see cref="UnboundPropertyReferenceExpression"/> is.
        /// </summary>
        /// <remarks>Returns <see cref="ExpressionKind.PropertyReference"/>.</remarks>
        public override ExpressionKind Type
        {
            get { return ExpressionKind.PropertyReference; }
        }

        public override string ToString()
        {
            return string.Format("{0}.{1}", this.source.ToString(), this.Name);
        }

        public override void Visit(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
