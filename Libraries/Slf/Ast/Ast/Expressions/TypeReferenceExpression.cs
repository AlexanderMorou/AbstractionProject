﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal;
using AllenCopeland.Abstraction.Slf._Internal.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Ast;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Ast.Expressions
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
            get { return ExpressionKind.TypeReference; }
        }

        public override string ToString()
        {
            return this.ReferenceType.BuildTypeName(true, false, TypeParameterDisplayMode.DebuggerStandard);
        }

        public override void Accept(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override TResult Accept<TResult, TContext>(ICommonExpressionVisitor<TResult, TContext> visitor, TContext context)
        {
            return visitor.Visit(this, context);
        }
        protected override IType TypeLookupAid
        {
            get
            {
                return this.ReferenceType;
            }
        }

        protected override bool IsStaticTarget
        {
            get
            {
                return true;
            }
        }
    }
}
