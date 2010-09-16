using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal;
using AllenCopeland.Abstraction.Slf._Internal.Abstract;
using AllenCopeland.Abstraction.Slf.Oil;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    public class TypeReferenceExpression :
        MemberParentReferenceExpressionBase,
        ITypeReferenceExpression,
        IUnaryOperationPrimaryTerm
    {

        /// <summary>
        /// Creates a new <see cref="TypeReferenceExpression"/>
        /// with the <paramref name="referenceType"/>
        /// provided.
        /// </summary>
        /// <param name="referenceType">The <see cref="IType"/>
        /// represented by the <see cref="TypeReferenceExpression"/>.</param>
        internal protected TypeReferenceExpression(IType referenceType)
        {
            this.ReferenceType = referenceType;
        }
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
            get { return this.forwardType; }
        }

        protected override void OnLink()
        {
            return;
        }
        */

        public IType ReferenceType { get; set; }
        public override ExpressionKind Type
        {
            get { return ExpressionKinds.TypeReference; }
        }

        public override string ToString()
        {
            return this.ReferenceType.CSharpToString();
        }

        public override void Visit(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }

        protected override IType TypeLookupAid
        {
            get
            {
                return this.ReferenceType;
            }
        }
    }
}
