using System;
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
    public class CSharpUnaryOperationExpression :
        ExpressionBase,
        ICSharpUnaryOperationExpression
    {

        /// <summary>
        /// Data member for <see cref="Term"/>.
        /// </summary>
        private IUnaryOperationPrimaryTerm term;
        /// <summary>
        /// Data member for <see cref="Operation"/>.
        /// </summary>
        private CSharpUnaryOperation operation = CSharpUnaryOperation.None;
        /// <summary>
        /// Returns the type of expression the <see cref="CSharpUnaryOperationExpression"/> is.
        /// </summary>
        public override ExpressionKind Type
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
                DiscernOpFlags(this.Operation, out bitInvert, out boolInvert, out negate, out postOp, out preOp, out decrement, out increment);
                ExpressionKind result = ExpressionKinds.None;
                if (bitInvert)
                    result = ExpressionKinds.UnaryBitwiseInversion;
                else if (boolInvert)
                    result = ExpressionKinds.UnaryBooleanInversion;
                if (negate)
                    result |= ExpressionKinds.UnarySignInversionOperation;
                if (preOp)
                    if (increment)
                        result |= ExpressionKinds.UnaryPreincrement;
                    else if (decrement)
                        result |= ExpressionKinds.UnaryPredecrement;
                if (postOp)
                    if (increment)
                        result |= ExpressionKinds.UnaryPostincrement;
                    else if (decrement)
                        result |= ExpressionKinds.UnaryPostdecrement;
                if (result == ExpressionKinds.None)
                    result = ExpressionKinds.UnaryForwardTerm;
                return result;
            }
        }

        public CSharpUnaryOperationExpression(IUnaryOperationPrimaryTerm term)
        {
            this.operation = CSharpUnaryOperation.None;
            this.term = term;
        }

        public CSharpUnaryOperationExpression(IUnaryOperationPrimaryTerm term, CSharpUnaryOperation operation)
            : this(term)
        {
            this.operation = BreakdownUnopFlags(operation);
        }

        #region ICSharpUnaryOperationExpression Members

        /// <summary>
        /// Returns/sets the unary operation to be performed on the <see cref="Term"/>.
        /// </summary>
        public CSharpUnaryOperation Operation
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
        /*
        private void RemoveLinkStatus()
        {
            if (this.IsLinked)
                base.Unlink();
        }
        */
        private static CSharpUnaryOperation BreakdownUnopFlags(CSharpUnaryOperation original)
        {
            bool bitInvert;
            bool boolInvert;
            bool negate;
            bool postOp;
            bool preOp;
            bool decrement;
            bool increment;
            DiscernOpFlags(original, out bitInvert, out boolInvert, out negate, out postOp, out preOp, out decrement, out increment);
            return 
                (bitInvert  ? CSharpUnaryOperation.BitwiseInversion : CSharpUnaryOperation.None) | 
                (boolInvert ? CSharpUnaryOperation.BooleanInversion : CSharpUnaryOperation.None) | 
                (negate     ? CSharpUnaryOperation.SignInversion    : CSharpUnaryOperation.None) | 
                (postOp     ? CSharpUnaryOperation.PostAction       : CSharpUnaryOperation.None) | 
                (preOp      ? CSharpUnaryOperation.PreAction        : CSharpUnaryOperation.None) |
                (decrement  ? CSharpUnaryOperation.Decrement        : CSharpUnaryOperation.None) | 
                (increment  ? CSharpUnaryOperation.Increment        : CSharpUnaryOperation.None) ;
        }

        private static void DiscernOpFlags(CSharpUnaryOperation original, out bool bitInvert, out bool boolInvert, out bool negate, out bool postOp, out bool preOp, out bool decrement, out bool increment)
        {
            /* *
             * Rediscern the flags based upon logic that cannot be expressed
             * in mere bits.
             * */
            bitInvert =     ((original & CSharpUnaryOperation.BitwiseInversion) == CSharpUnaryOperation.BitwiseInversion);
            boolInvert =    ((original & CSharpUnaryOperation.BooleanInversion) == CSharpUnaryOperation.BooleanInversion) 
                                && !bitInvert;
            negate =        ((original & CSharpUnaryOperation.SignInversion)    == CSharpUnaryOperation.SignInversion) 
                                && !boolInvert;
            postOp =        ((original & CSharpUnaryOperation.PostAction)       == CSharpUnaryOperation.PostAction);
            preOp =         ((original & CSharpUnaryOperation.PreAction)        == CSharpUnaryOperation.PreAction) 
                                && !postOp;
            decrement =     ((original & CSharpUnaryOperation.Decrement)        == CSharpUnaryOperation.Decrement) 
                                && (preOp || postOp);
            increment =     ((original & CSharpUnaryOperation.Increment)        == CSharpUnaryOperation.Increment) 
                                && !decrement 
                                && (preOp || postOp);
        }

        /// <summary>
        /// Returns/sets the <see cref="IUnaryOperationPrimaryTerm"/> that the <see cref="CSharpUnaryOperationExpression"/>
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
                    result += "--";
                else if (increment)
                    result += "++";
            result += this.Term.ToString();
            if (postOp)
                if (decrement)
                    result += "--";
                else if (increment)
                    result += "++";
            return result;
        }
        /*
        public override IType ForwardType
        {
            get {
                if (!this.IsLinked)
                    this.Link();
                return this.forwardType;
            }
        }

        protected override void OnLink()
        {
            IType t = this.Term.GetEvaluationType();
            bool isOverride = false;
            if (this.Operation == CSharpUnaryOperation.None)
            {
                this.SetNonUnaryOpOverloadState();
            }
            else
            {
                if (t.IsPrimitive())
                {
                    switch (t.GetTypeCode())
                    {
                        case TypeCode.Boolean:
                            if (((operation & CSharpUnaryOperation.Increment) != CSharpUnaryOperation.None) ||
                                ((operation & CSharpUnaryOperation.Decrement) != CSharpUnaryOperation.None) ||
                                ((operation & CSharpUnaryOperation.Negate) != CSharpUnaryOperation.None))
                            {
                            }
                            goto case TypeCode.Byte;
                        case TypeCode.Byte:
                        case TypeCode.Char:
                        case TypeCode.DateTime:
                        case TypeCode.Double:
                        case TypeCode.Int16:
                        case TypeCode.Int32:
                        case TypeCode.Int64:
                        case TypeCode.SByte:
                        case TypeCode.Single:
                        case TypeCode.String:
                        case TypeCode.UInt16:
                        case TypeCode.UInt32:
                        case TypeCode.UInt64:
                            isOverride = false;
                            break;
                        default:
                            isOverride = true;
                            break;
                    }
                }
                else
                    isOverride = true;
                if (isOverride)
                {
                    //Start looking for the unary operation.
                    ICoercibleType ict = null;
                    if (t is IStructType)
                        ict = ((IStructType)(t));
                    else if (t is IClassType)
                        ict = ((IClassType)(t));
                    //First: Crement operation

                    //Second: Negate operation
                    //Third: BooleanInversion operation

                    this.isUnaryOpOverride = true;
                }
                else
                    this.SetNonUnaryOpOverloadState();
            }
            this.forwardType = t;
        }
        */

        //public override CSharpOperatorPrecedences Precedence
        //{
        //    get { return CSharpOperatorPrecedences.UnaryOperation; }
        //}

        public override void Visit(IIntermediateCodeVisitor visitor)
        {
            visitor.Visit(this);
        }

    }
}
