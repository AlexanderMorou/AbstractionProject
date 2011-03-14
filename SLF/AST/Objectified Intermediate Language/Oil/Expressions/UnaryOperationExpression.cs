using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Oil;
using AllenCopeland.Abstraction.Slf.Oil.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    public class UnaryOperationExpression :
        ExpressionBase,
        IUnaryOperationExpression
    {

        /// <summary>
        /// Data member for <see cref="Term"/>.
        /// </summary>
        private IUnaryOperationPrimaryTerm term;
        /// <summary>
        /// Data member for <see cref="Operation"/>.
        /// </summary>
        private UnaryOperation operation = UnaryOperation.None;
        /// <summary>
        /// Returns the type of expression the <see cref="UnaryOperationExpression"/> is.
        /// </summary>
        public override ExpressionKinds Type
        {
            get
            {
                if (operation == UnaryOperation.None)
                    return ExpressionKinds.UnaryForwardTerm;
                else
                    return ExpressionKinds.UnaryOperation;
            }
        }

        public UnaryOperationExpression(IUnaryOperationPrimaryTerm term)
        {
            this.operation = UnaryOperation.None;
            this.term = term;
        }

        public UnaryOperationExpression(IUnaryOperationPrimaryTerm term, UnaryOperation operation)
            : this(term)
        {
            this.operation = BreakdownUnopFlags(operation);
        }

        #region IUnaryOperationExpression Members

        /// <summary>
        /// Returns/sets the unary operation to be performed on the <see cref="Term"/>.
        /// </summary>
        public UnaryOperation Operation
        {
            get {
                return BreakdownUnopFlags(this.operation);
            }
            set
            {
                this.operation = BreakdownUnopFlags(value);
                //this.RemoveLinkStatus();
            }
        }
        private static UnaryOperation BreakdownUnopFlags(UnaryOperation original)
        {
            bool bitInvert, boolInvert, negate, postOp, preOp, decrement, increment;
            DiscernOpFlags(original, out bitInvert, out boolInvert, out negate, out postOp, out preOp, out decrement, out increment);
            return 
                (bitInvert  ? UnaryOperation.BitwiseInversion : UnaryOperation.None) | 
                (boolInvert ? UnaryOperation.BooleanInversion : UnaryOperation.None) | 
                (negate     ? UnaryOperation.SignInversion    : UnaryOperation.None) | 
                (postOp     ? UnaryOperation.PostAction       : UnaryOperation.None) | 
                (preOp      ? UnaryOperation.PreAction        : UnaryOperation.None) |
                (decrement  ? UnaryOperation.Decrement        : UnaryOperation.None) | 
                (increment  ? UnaryOperation.Increment        : UnaryOperation.None) ;
        }

        private static void DiscernOpFlags(UnaryOperation original, out bool bitInvert, out bool boolInvert, out bool negate, out bool postOp, out bool preOp, out bool decrement, out bool increment)
        {
            /* *
             * Rediscern the flags based upon logic that cannot be expressed
             * in mere bits.
             * */
            bitInvert     = ((original & UnaryOperation.BitwiseInversion) == UnaryOperation.BitwiseInversion);
            boolInvert    = ((original & UnaryOperation.BooleanInversion) == UnaryOperation.BooleanInversion) 
                                && !bitInvert;
            negate        =    ((original & UnaryOperation.SignInversion) == UnaryOperation.SignInversion) 
                                && !boolInvert;
            postOp        =       ((original & UnaryOperation.PostAction) == UnaryOperation.PostAction);
            preOp         =        ((original & UnaryOperation.PreAction) == UnaryOperation.PreAction) 
                                && !postOp;
            decrement     =        ((original & UnaryOperation.Decrement) == UnaryOperation.Decrement) 
                                && (preOp || postOp);
            increment     =        ((original & UnaryOperation.Increment) == UnaryOperation.Increment) 
                                && !decrement 
                                && (preOp || postOp);
        }

        /// <summary>
        /// Returns/sets the <see cref="IUnaryOperationPrimaryTerm"/> that the <see cref="UnaryOperationExpression"/>
        /// operates on.
        /// </summary>
        public IUnaryOperationPrimaryTerm Term
        {
            get { return this.term; }
            set
            {
                this.term = value;
                //this.RemoveLinkStatus();
            }
        }

        #endregion

        public override string ToString()
        {
            bool bitInvert;
            bool boolInvert;
            bool negate;
            bool postOp;
            bool preOp;
            bool decrement;
            bool increment;
            DiscernOpFlags(this.operation, out bitInvert, out boolInvert, out negate, out postOp, out preOp, out decrement, out increment);
            string result = string.Empty;
            if (bitInvert)
                if (negate)
                    result = "~-";
                else
                    result = "~";
            else if (boolInvert)
                result = "!";
            else if (negate)
                result = "-";
            if (preOp)
                if (decrement)
                    result = "--" + result;
                else if (increment)
                    result = "++" + result;
            result += this.Term.ToString();
            if (postOp)
                if (decrement)
                    result += "--";
                else if (increment)
                    result += "++";
            return result;
        }

        //public override OperatorPrecedences Precedence
        //{
        //    get { return OperatorPrecedences.UnaryOperation; }
        //}

        public override void Visit(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }

        #region IStatementExpression Members

        public bool ValidAsStatement
        {
            get
            {
                bool bitInvert;
                bool boolInvert;
                bool negate;
                bool postOp;
                bool preOp;
                bool decrement;
                bool increment;
                DiscernOpFlags(Operation, out bitInvert, out boolInvert, out negate, out postOp, out preOp, out decrement, out increment);
                if (!(bitInvert || boolInvert || negate))
                    if (increment || decrement)
                        return true;
                return false;
            }
        }

        #endregion
    }
}
