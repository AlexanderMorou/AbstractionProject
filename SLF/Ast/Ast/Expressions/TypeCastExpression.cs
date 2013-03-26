using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Ast;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Ast.Expressions
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
        /// Data member for <see cref="RequiredModifiers"/>.
        /// </summary>
        private ITypeCollection requiredModifiers;
        /// <summary>
        /// Data member for <see cref="OptionalModifiers"/>.
        /// </summary>
        private ITypeCollection optionalModifiers;

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

        public override ExpressionKind Type
        {
            get { return ExpressionKind.TypeCast; }
        }

        public override void Visit(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override TResult Visit<TResult>(IExpressionVisitor<TResult> visitor)
        {
            return visitor.Visit(this);
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

        /// <summary>
        /// Returns the <see cref="ITypeCollection"/> which denotes the
        /// required modifiers of the cast.
        /// </summary>
        public ITypeCollection RequiredModifiers
        {
            get {
                if (this.requiredModifiers == null)
                    this.requiredModifiers = new TypeCollection();
                return this.requiredModifiers;
            }
        }

        /// <summary>
        /// Returns the <see cref="ITypeCollection"/> which denotes the
        /// optional modifiers of the cast.
        /// </summary>
        public ITypeCollection OptionalModifiers
        {
            get {
                if (this.optionalModifiers == null)
                    this.optionalModifiers = new TypeCollection();
                return this.optionalModifiers;
            }
        }

        #endregion
    }
}
