using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Oil;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    /// <summary>
    /// Provides a base implementation of 
    /// <see cref="ITypeCastExpression"/> for working 
    /// with an expression that casts another
    /// expression to a specified type.
    /// </summary>
    public class TypeCastExpression : 
        ExpressionBase,
        ITypeCastExpression
    {
        /// <summary>
        /// Data member for <see cref="CastType"/>.
        /// </summary>
        private IType castType;
        /// <summary>
        /// Data member for <see cref="Target"/>.
        /// </summary>
        private IExpression target;

        /// <summary>
        /// Creates a new <see cref="TypeCastExpression"/>
        /// with the <paramref name="castType"/> and 
        /// <paramref name="target"/> provided.
        /// </summary>
        /// <param name="castType">The <see cref="IType"/> the <see cref="TypeCastExpression"/>
        /// casts the <paramref name="target"/> to.</param>
        /// <param name="target">The <see cref="IExpression"/> the 
        /// <see cref="TypeCastExpression"/> casts to
        /// <paramref name="castType"/>.</param>
        public TypeCastExpression(IType castType, IExpression target)
        {
            this.castType = castType;
            this.target = target;
        }

        public override string ToString()
        {
            return string.Format("({0}){1}", this.CastType, this.Target);
        }
        #region ITypeCastExpression Members

        /// <summary>
        /// Returns/sets the <see cref="IType"/> the <see cref="TypeCastExpression"/>
        /// casts the <see cref="Target"/> to.
        /// </summary>
        public IType CastType
        {
            get
            {
                return this.castType;
            }
            set
            {
                this.castType = value;
            }
        }

        /// <summary>
        /// Returns/sets the <see cref="IExpression"/> the 
        /// <see cref="TypeCastExpression"/> casts to
        /// <see cref="CastType"/>.
        /// </summary>
        public IExpression Target
        {
            get
            {
                return this.target;
            }
            set
            {
                this.target = value;
            }
        }

        #endregion

        public override ExpressionKinds Type
        {
            get { return ExpressionKinds.TypeCast; }
        }
        /*
        public override IType ForwardType
        {
            get { return castType; }
        }

        protected override void OnLink()
        {
            /* *
             * Casting requires immediate linking
             * through castType on initialization.
             * Ergo, Do: Nothing.
             * *//*
            return;
        }
        */

        public override void Visit(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
