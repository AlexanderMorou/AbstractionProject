﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Properties;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Languages.CSharp.Expressions
{
    
    public abstract partial class CSharpBinaryOperationExpressionBase<TLeft, TRight> :
        CSharpExpressionBase,
        IBinaryOperationExpression<TLeft, TRight>
        where TLeft :
            INaryOperandExpression
        where TRight :
            INaryOperandExpression
    {
        /// <summary>
        /// Data member for <see cref="LeftSide"/>.
        /// </summary>
        private TLeft leftSide;
        /// <summary>
        /// Data member for <see cref="RightSide"/>.
        /// </summary>
        private TRight rightSide;

        protected CSharpBinaryOperationExpressionBase(TLeft leftSide, TRight rightSide)
        {
            this.leftSide = leftSide;
            this.rightSide = rightSide;
        }

        protected CSharpBinaryOperationExpressionBase(TLeft term)
        {
            this.leftSide = term;
        }

        protected CSharpBinaryOperationExpressionBase(TRight term)
        {
            this.rightSide = term;
        }

        #region IBinaryOperationExpression<TLeft,TRight> Members

        /// <summary>
        /// Returns the left side of the <see cref="CSharpBinaryOperationExpressionBase{TLeft, TRight}"/>.
        /// </summary>
        /// <remarks>Can be null if precedence is Left</remarks>
        public TLeft LeftSide
        {
            get
            {
                return this.leftSide;
            }
            set
            {
                this.leftSide = value;
            }
        }

        /// <summary>
        /// Returns the right side of the <see cref="CSharpBinaryOperationExpressionBase{TLeft, TRight}"/>.
        /// </summary>
        /// <remarks>Can be null if precedence is Right</remarks>
        public TRight RightSide
        {
            get
            {
                return this.rightSide;
            }
            set
            {
                this.rightSide = value;
            }
        }

        /// <summary>
        /// Returns the <see cref="BinaryOperationAssociativity"/> associated to the 
        /// <see cref="CSharpBinaryOperationExpressionBase{TLeft, TRight}"/>.
        /// </summary>
        public abstract BinaryOperationAssociativity Associativity { get; }

        #endregion


        protected override MemberParentReferenceExpressionBase ObtainRelativeGetMemberTarget()
        {
            switch (this.Associativity)
            {
                case BinaryOperationAssociativity.Left:
                    if (this.LeftSide == null && this.RightSide != null)
                        return (MemberParentReferenceExpressionBase)(IExpression)this.RightSide;
                    else if (this.LeftSide != null && this.RightSide != null)
                        return new ParenthesizedExpression(this);
                    break;
                case BinaryOperationAssociativity.Right:
                    if (this.LeftSide != null && this.RightSide == null)
                        return (MemberParentReferenceExpressionBase)(IExpression)this.LeftSide;
                    else if (this.LeftSide != null && this.RightSide != null)
                        return new ParenthesizedExpression(this);
                    break;
            }
            throw new InvalidOperationException("BinaryOperationExpression in invalid state.");
        }

        #region IBinaryOperationExpression Members

        INaryOperandExpression IBinaryOperationExpression.LeftSide
        {
            get
            {
                return this.LeftSide;
            }
            set
            {
                if (value == null || value is TLeft)
                    this.LeftSide = (TLeft)value;
                throw ThrowHelper.ObtainArgumentException(ArgumentWithException.value, ExceptionMessageId.ValueIsWrongType, ThrowHelper.GetArgumentName(ArgumentWithException.value), value.GetType().ToString(), typeof(TLeft).ToString());
            }
        }

        INaryOperandExpression IBinaryOperationExpression.RightSide
        {
            get
            {
                return this.RightSide;
            }
            set
            {
                if (value == null || value is TRight)
                    this.RightSide = (TRight)value;
                throw ThrowHelper.ObtainArgumentException(ArgumentWithException.value, ExceptionMessageId.ValueIsWrongType, ThrowHelper.GetArgumentName(ArgumentWithException.value), value.GetType().ToString(), typeof(TRight).ToString());
            }
        }

        public abstract BinaryOperationKind OperationKind { get; }

        #endregion

        public override ExpressionKind Type
        {
            get { throw new NotImplementedException(); }
        }

        public override void Accept(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override TResult Accept<TResult, TContext>(ICommonExpressionVisitor<TResult, TContext> visitor, TContext context)
        {
            return visitor.Visit(this, context);
        }
    }
}
