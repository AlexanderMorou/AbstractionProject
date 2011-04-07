using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    public class BoundSpecialReferenceExpression :
        MemberParentReferenceExpressionBase,
        IBoundSpecialReferenceExpression
    {
        internal BoundSpecialReferenceExpression(IType type, SpecialReferenceKind referenceKind)
        {
            this.Kind = referenceKind;
            this.ReferenceType = type;
        }

        protected override Slf.Abstract.IType TypeLookupAid
        {
            get
            {
                return this.ReferenceType;
            }
        }

        public override ExpressionKinds Type
        {
            get {
                switch (this.Kind)
                {
                    case SpecialReferenceKind.Self:
                        return ExpressionKinds.SelfReference;
                    case SpecialReferenceKind.Base:
                        return ExpressionKinds.BaseReference;
                    case SpecialReferenceKind.This:
                        return ExpressionKinds.ThisReference;
                }
                throw new InvalidOperationException();
            }
        }

        public override void Visit(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }

        #region IBoundSpecialReferenceExpression Members

        /// <summary>
        /// Returns the <see cref="IType"/> associated to the special reference used for further
        /// member binding.
        /// </summary>
        public IType ReferenceType { get; private set; }

        #endregion

        #region ISpecialReferenceExpression Members

        public SpecialReferenceKind Kind { get; private set; }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            this.ReferenceType = null;
        }

        #endregion

        public override string ToString()
        {
            switch (this.Kind)
            {
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
