using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil.Expressions;
using System.ComponentModel;
using AllenCopeland.Abstraction.Slf.Oil.Members;

namespace AllenCopeland.Abstraction.Slf.Oil.Statements
{
    /// <summary>
    /// Provides a base implementation of 
    /// <see cref="IEnumerateSetBreakableBlockStatement"/>
    /// which provides properties and methods for working with a 
    /// statement which represents the action of enumerating the
    /// elements of a set and performing a set of actions on each
    /// element.
    /// </summary>
    public class EnumerateSetBreakableBlockStatement :
        BlockStatementBase,
        IEnumerateSetBreakableBlockStatement
    {
        private IBreakExit exitPoint;

        /// <summary>
        /// Creates a new <see cref="EnumerateSetBreakableBlockStatement"/>
        /// with the <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="parent">The <see cref="IBlockStatementParent"/>
        /// which contains the <see cref="IEnumerateSetBreakableBlockStatement"/>.
        /// </param>
        public EnumerateSetBreakableBlockStatement(IBlockStatementParent parent)
            : base(parent)
        {
        }

        #region IBreakableBlockStatement Members

        /// <summary>
        /// Returns the <see cref="IBreakExit"/> for the <see cref="IBreakableStatement"/>.
        /// </summary>
        /// <remarks>In languages that natively support the break statement
        /// this is unnecessary; however in using this in the code, 
        /// the label will be emitted in the associated supporting 
        /// language as well.</remarks>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public IBreakExit AssociatedJumpLabel
        {
            get
            {
                if (this.exitPoint == null)
                    this.exitPoint = this.InitializeJumpPoint();
                return this.exitPoint;
            }
        }

        private IBreakExit InitializeJumpPoint()
        {
            return new BreakExit(this.Parent);
        }

        /// <summary>
        /// Breaks the execution from its current point to somewhere else.
        /// </summary>
        /// <returns>A <see cref="IBreakStatement"/> which designates the <see cref="IJumpStatement.Target"/>
        /// as necessary.</returns>
        public IBreakStatement Break()
        {
            var b = new BreakStatement(this, this.AssociatedJumpLabel);
            this.baseList.Add(b);
            return b;
        }

        /// <summary>
        /// Inserts and returns a new <see cref="IBreakableConditionBlockStatement"/> instance
        /// which relates to the <paramref name="condition"/> provided that can contain a 
        /// break statement.
        /// </summary>
        /// <param name="condition">The <see cref="IExpression"/> to evaluate
        /// before executing the <see cref="IBreakableConditionBlockStatement"/>'s
        /// statements.</param>
        /// <returns>A new <see cref="IBreakableConditionBlockStatement"/> with the
        /// <see cref="IExpression"/> <paramref name="condition"/> provided
        /// that can contain a break statement.</returns>
        public new IBreakableConditionBlockStatement If(IExpression condition)
        {
            return (IBreakableConditionBlockStatement)(base.If(condition));
        }

        #endregion

        internal override IConditionBlockStatement OnIf(IExpression condition)
        {
            return new BreakableConditionBlockStatement(this) { Condition = condition };
        }

        #region IEnumerateSetBreakableBlockStatement Members
        /// <summary>
        /// Returns/sets the <see cref="ILocalDeclarationStatement"/> which
        /// designates the <see cref="ILocalMember"/> to utilize within the
        /// scope of the enumeration.
        /// </summary>
        public ILocalDeclarationStatement LocalDeclaration { get; set; }

        /// <summary>
        /// Returns/sets the <see cref="IExpression"/> which provides the
        /// source set for the enumeration.
        /// </summary>
        public IExpression Source { get; set; }

        #endregion
    }
}
