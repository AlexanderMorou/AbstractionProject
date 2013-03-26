using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */
namespace AllenCopeland.Abstraction.Slf.Languages.CSharp.Expressions
{
    /// <summary>
    /// Provides a base implementation of <see cref="ICSharpConditionalExpression"/>
    /// for working with a ternary operator conditional expression.
    /// </summary>
    public sealed class CSharpConditionalExpression :
        ExpressionBase,
        ICSharpConditionalExpression
    {
        /// <summary>
        /// Data member for <see cref="CheckPart"/>.
        /// </summary>
        private ICSharpLogicalOrExpression checkPart;
        /// <summary>
        /// Data member for <see cref="TruePart"/>.
        /// </summary>
        private ICSharpConditionalExpression truePart;
        /// <summary>
        /// Data member for <see cref="FalsePart"/>.
        /// </summary>
        private ICSharpConditionalExpression falsePart;

        public override ExpressionKind Type
        {
            get
            {
                if (this.TruePart == null && this.FalsePart == null)
                    return ExpressionKind.ConditionalForwardTerm;
                else
                    return ExpressionKind.ConditionalOperation;
            }
        }

        /// <summary>
        /// Creates a new <see cref="CSharpConditionalExpression"/>
        /// with the <paramref name="termPart"/> provided.
        /// </summary>
        /// <param name="termPart">The <see cref="ICSharpLogicalOrExpression"/>
        /// which makes the <see cref="CSharpConditionalExpression"/> a pass-through.</param>
        public CSharpConditionalExpression(ICSharpLogicalOrExpression termPart)
        {
            this.checkPart = termPart;
        }

        /// <summary>
        /// Creates a new <see cref="CSharpConditionalExpression"/> with 
        /// the <paramref name="checkPart"/>, <paramref name="truePart"/>
        /// and <paramref name="falsePart"/>
        /// provided.
        /// </summary>
        /// <param name="checkPart">The <see cref="ICSharpLogicalOrExpression"/> which relates to the 
        /// condition to check.</param>
        /// <param name="truePart">The <see cref="ICSharpConditionalExpression"/> that is 
        /// evaluated when <paramref name="checkPart"/> evaluates to true.</param>
        /// <param name="falsePart">The <see cref="ICSharpConditionalExpression"/>
        /// that is evaluated when <paramref name="checkPart"/> evaluates to false.</param>
        public CSharpConditionalExpression(ICSharpLogicalOrExpression checkPart, ICSharpConditionalExpression truePart, ICSharpConditionalExpression falsePart)
        {
            this.checkPart = checkPart;
            this.falsePart = falsePart;
            this.truePart = truePart;
        }

        #region ICSharpConditionalExpression Members

        /// <summary>
        /// Returns whether the <see cref="CSharpConditionalExpression"/>
        /// is a pass-through and only <see cref="CheckPart"/> is set.
        /// </summary>
        /// <remarks><para>A pass-through implies that the <see cref="CSharpConditionalExpression"/>
        /// is functionally non-operational and translates to the <see cref="CheckPart"/>
        /// only.</para><para>If <see cref="FalsePart"/> OR <see cref="TruePart"/>
        /// are null and <see cref="CheckPart"/> is not null, then this returns true;
        /// otherwise, this returns false.</para></remarks>
        public bool IsTerm
        {
            get { return ((this.falsePart == null) || (this.truePart == null)) && (this.checkPart != null); }
        }

        /// <summary>
        /// Returns/sets the check part of the conditional.
        /// </summary>
        public ICSharpLogicalOrExpression CheckPart
        {
            get
            {
                return this.checkPart;
            }
            set
            {
                if (value == checkPart)
                    return;
                this.checkPart = value;
            }
        }

        /// <summary>
        /// Returns/sets the true part of the conditional.
        /// </summary>
        public ICSharpConditionalExpression TruePart
        {
            get
            {
                return this.truePart;
            }
            set
            {
                if (value == this.truePart)
                    return;
                this.truePart = value;
                //this.RemoveLinkStatus();
            }
        }

        /// <summary>
        /// Returns/sets the false part of the conditional.
        /// </summary>
        public ICSharpConditionalExpression FalsePart
        {
            get
            {
                return this.falsePart;
            }
            set
            {
                if (value == this.falsePart)
                    return;
                this.falsePart = value;
                //this.RemoveLinkStatus();
            }
        }
        #endregion
        /*

        private void RemoveLinkStatus()
        {
            if (this.IsLinked)
                base.Unlink();
        }

        public override IType ForwardType
        {
            get
            {
                if (!this.IsLinked)
                    this.OnLink();
                return this.forwardType;
            }
        }

        protected override void OnLink()
        {
            if (this.IsTerm)
            {
                if (this.checkPart is ICSharpLinkableExpression && !((ICSharpLinkableExpression)this.checkPart).IsLinked)
                    ((ICSharpLinkableExpression)this.checkPart).Link();
            }
            else
            {
                if (this.TruePart == null)
                    return;
                if (this.falsePart == null)
                    return;
                IType trueType = null;
                IType falseType = null;
                trueType = TruePart.GetEvaluationType();
                falseType = FalsePart.GetEvaluationType();
                IImplicitChain iic = null;
                if (trueType.CanConvertTo(falseType))
                    this.forwardType = trueType;
                else if (falseType.CanConvertTo(trueType))
                    this.forwardType = falseType;
            }
        }
        */
        //public override CSharpOperatorPrecedences Precedence
        //{
        //    get { return CSharpOperatorPrecedences.ConditionalOperation; }
        //}

        #region IConditionalExpression Members


        IExpression IConditionalExpression.CheckPart
        {
            get
            {
                return this.CheckPart;
            }
            set
            {
                if (value is ICSharpLogicalOrExpression)
                    this.CheckPart = (ICSharpLogicalOrExpression)value;
                else
                {
                    var result = value.AffixTo(CSharpOperatorPrecedences.LogicalOrOperation);
                    if (result is ICSharpLogicalOrExpression)
                        this.CheckPart = (ICSharpLogicalOrExpression)result;
                    else
                        throw ThrowHelper.ObtainArgumentException(ArgumentWithException.value, ExceptionMessageId.ProvidedExpressionCannotBe, ThrowHelper.GetArgumentExceptionWord(ExceptionWordId.cs_logical_or));
                }
            }
        }

        IExpression IConditionalExpression.TruePart
        {
            get
            {
                return this.TruePart;
            }
            set
            {
                if (value is ICSharpConditionalExpression)
                    this.TruePart = (ICSharpConditionalExpression)value;
                else
                {
                    var result = value.AffixTo(CSharpOperatorPrecedences.ConditionalOperation);
                    if (result is ICSharpConditionalExpression)
                        this.TruePart = (ICSharpConditionalExpression)result;
                    else
                        throw ThrowHelper.ObtainArgumentException(ArgumentWithException.value, ExceptionMessageId.ProvidedExpressionCannotBe, ThrowHelper.GetArgumentExceptionWord(ExceptionWordId.cs_conditional));
                }
            }
        }

        IExpression IConditionalExpression.FalsePart
        {
            get
            {
                return this.FalsePart;
            }
            set
            {
                if (value is ICSharpConditionalExpression)
                    this.FalsePart = (ICSharpConditionalExpression)value;
                else
                {
                    var result = value.AffixTo(CSharpOperatorPrecedences.ConditionalOperation);
                    if (result is ICSharpConditionalExpression)
                        this.FalsePart = (ICSharpConditionalExpression)result;
                    else
                        throw ThrowHelper.ObtainArgumentException(ArgumentWithException.value, ExceptionMessageId.ProvidedExpressionCannotBe, ThrowHelper.GetArgumentExceptionWord(ExceptionWordId.cs_conditional));
                }
            }
        }

        #endregion

        public override string ToString()
        {
            if (this.TruePart == null && this.falsePart == null)
                return string.Format(CultureInfo.CurrentCulture, "{0}", this.CheckPart);
            else
                return string.Format(CultureInfo.CurrentCulture, "{0} ? {1} : {2}", this.CheckPart, this.TruePart, this.FalsePart);

        }

        public override void Visit(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override TResult Visit<TResult>(IExpressionVisitor<TResult> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
