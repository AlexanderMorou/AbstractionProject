﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    public class ExpressionToCommaTypeReferenceFusionExpression :
        ExpressionBase,
        IExpressionToCommaTypeReferenceFusionExpression
    {
        private TypeCollection right;

        public ExpressionToCommaTypeReferenceFusionExpression(IFusionTypeCollectionTargetExpression target)
        {
            this.Left = target;
        }

        public ExpressionToCommaTypeReferenceFusionExpression(IFusionTypeCollectionTargetExpression target, params IType[] terms)
        {
            this.Left = target;
            this.Right.AddRange(terms);
        }
        public ExpressionToCommaTypeReferenceFusionExpression(IFusionTypeCollectionTargetExpression target, ITypeCollection terms)
        {
            this.Left = target;
            this.Right.AddRange(terms);
        }

        public override ExpressionKind Type
        {
            get { return ExpressionKinds.ExpressionToTypeCollectionFusion; }
        }

        #region IExpressionToCommaTypeReferenceFusionExpression Members

        /// <summary>
        /// Returns/sets the expression, which is fusable to the comma delimited
        /// series of type-references, associated to the 
        /// <see cref="ExpressionToCommaTypeReferenceFusionExpression"/>
        /// </summary>
        public IFusionTypeCollectionTargetExpression Left { get; set; }

        /// <summary>
        /// Returns the <see cref="ITypeCollection"/> which represents the
        /// type-reference series fused to the <see cref="Left"/> portion
        /// of the <see cref="ExpressionToCommaTypeReferenceFusionExpression"/>.
        /// </summary>
        public ITypeCollection Right
        {
            get
            {
                if (this.right == null)
                    this.right = new TypeCollection();
                return this.right;
            }
        }

        #endregion

        public override string ToString()
        {
            return string.Format("{0}<{1}>", this.Left, Right.ToString());
        }

        public override void Visit(IIntermediateCodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}