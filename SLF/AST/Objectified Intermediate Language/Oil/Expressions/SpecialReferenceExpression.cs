using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    public class SpecialReferenceExpression :
        MemberParentReferenceExpressionBase,
        ISpecialReferenceExpression
    {
        public SpecialReferenceExpression(SpecialReferenceKind referenceKind)
        {
            this.Kind = referenceKind;
        }


        #region ISpecialReferenceExpression Members

        public SpecialReferenceKind Kind { get; set; }

        #endregion

        #region IExpression Members

        public override ExpressionKind Type
        {
            get
            {
                switch (Kind)
                {
                    case SpecialReferenceKind.CurrentClass:
                        return ExpressionKinds.CurrentTypeReference;
                    case SpecialReferenceKind.Self:
                        return ExpressionKinds.SelfReference;
                    case SpecialReferenceKind.Base:
                        return ExpressionKinds.BaseReference;
                    case SpecialReferenceKind.This:
                        return ExpressionKinds.ThisReference;
                    default:
                        return ExpressionKinds.None;
                }
            }
        }

        public override void Visit(IExpressionVisitor visitor)
        {
            if (visitor == null)
                throw new ArgumentNullException("visitor");
            visitor.Visit(this);
        }

        #endregion

        public override string ToString()
        {
            switch (this.Kind)
            {
                case SpecialReferenceKind.CurrentClass:
                    return "class";
                case SpecialReferenceKind.Self:
                    return "self";
                case SpecialReferenceKind.Base:
                    return "base";
                case SpecialReferenceKind.This:
                    return "this";
                default:
                    return string.Empty;
            }
        }
    }
}
