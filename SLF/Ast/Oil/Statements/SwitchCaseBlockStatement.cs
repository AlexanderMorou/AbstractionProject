using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
using System.ComponentModel;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Statements
{
    public class SwitchCaseBlockStatement :
        BlockStatementBase,
        ISwitchCaseBlockStatement
    {
        private bool isDefault;

        public SwitchCaseBlockStatement(ISwitchStatement parent, params IExpression[] cases)
            : base(parent.Parent)
        {
            this.Parent = parent;
            this.Cases = new MalleableExpressionCollection(cases);
        }

        #region ISwitchCaseBlockStatement Members

        /// <summary>
        /// Returns the <see cref="IMalleableExpressionCollection"/> that relates to the current 
        /// switch case.
        /// </summary>
        public IMalleableExpressionCollection Cases { get; private set; }

        /// <summary>
        /// Returns the <see cref="ISwitchStatement"/> in which the current
        /// <see cref="ISwitchCaseBlockStatement"/> exists within.
        /// </summary>
        public new ISwitchStatement Parent { get; private set; }

        /// <summary>
        /// Returns whether the current <see cref="ISwitchCaseBlockStatement"/>
        /// represents the default case
        /// </summary>
        public bool IsDefault
        {
            get
            {
                return this.isDefault;
            }
            set
            {
                if (value && this.Parent != null)
                {
                    var parentDefault = this.Parent.DefaultBlock;
                    if (parentDefault != null)
                        parentDefault.IsDefault = false;
                }
                this.isDefault = value;
            }
        }

        #endregion

        #region IBreakableBlockStatement Members

        /// <summary>
        /// Breaks the execution from its current point to somewhere else.
        /// </summary>
        /// <returns>A <see cref="IBreakStatement"/> which designates the <see cref="IJumpStatement.Target"/>
        /// as necessary.</returns>
        public IBreakStatement Break()
        {
            return new BreakStatement(this, this.AssociatedJumpLabel);
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

        #region IBreakableStatement Members

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
            get { return this.Parent.BreakExit; }
        }

        #endregion

        public override string ToString()
        {
            return string.Format("cases {0}:", string.Join(", ", this.Cases));
        }

        public override void Visit(IStatementVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
