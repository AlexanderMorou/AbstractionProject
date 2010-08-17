using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil.Expressions;

namespace AllenCopeland.Abstraction.Slf.Oil.Statements
{
    /// <summary>
    /// Provides a condition statement which alters the flow of 
    /// execution based upon a boolean condition  within a 
    /// breakable section of code.
    /// </summary>
    public class BreakableConditionBlockStatement :
        ConditionBlockStatement,
        IBreakableConditionBlockStatement
    {
        /// <summary>
        /// Creates a new <see cref="BreakableConditionBlockStatement"/> with the
        /// <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="parent">The <see cref="IBreakableBlockStatement"/></param>
        public BreakableConditionBlockStatement(IBreakableBlockStatement parent)
            : base(parent)
        {
        }

        #region IBreakableConditionBlockStatement Members

        /// <summary>
        /// Returns the <see cref="IBreakableBlockStatement"/> which contains the 
        /// <see cref="IBreakableConditionBlockStatement"/>.
        /// </summary>
        public new IBreakableBlockStatement Parent
        {
            get { return (IBreakableBlockStatement)base.Parent; }
        }

        /// <summary>
        /// Returns/sets the <see cref="IBreakableConditionContinuationStatement"/> which continues the
        /// code flow control conditioning.
        /// </summary>
        public new IBreakableConditionContinuationStatement Next
        {
            get
            {
                return (IBreakableConditionContinuationStatement)base.Next;
            }
            set
            {
                base.Next = value;
            }
        }

        #endregion

        #region IBreakableBlockStatement Members

        public IBreakExit AssociatedJumpLabel
        {
            get {
                return this.Parent.AssociatedJumpLabel;
            }
        }

        /// <summary>
        /// Breaks the execution from its current point elsewhere.
        /// </summary>
        /// <returns>A <see cref="IBreakStatement"/> which designates the <see cref="IJumpStatement.Target"/>
        /// as necessary.</returns>
        public IBreakStatement Break()
        {
            var b = new BreakStatement(this, this.AssociatedJumpLabel);
            this.baseCollection.Add(b);
            return b;
        }

        public new IBreakableConditionBlockStatement If(IExpression condition)
        {
            return (IBreakableConditionBlockStatement)(base.If(condition));
        }

        #endregion

        internal override IConditionBlockStatement OnIf(IExpression condition)
        {
            var result = new BreakableConditionBlockStatement(this.Parent)
            {
                Condition = condition
            };
            this.baseCollection.Add(result);
            return result;
        }

        internal override void SetNext(IConditionContinuationStatement value)
        {
            if (!(value is IBreakableConditionContinuationStatement))
                throw new ArgumentException("value");
            base.SetNext(value);
        }


    }
}
