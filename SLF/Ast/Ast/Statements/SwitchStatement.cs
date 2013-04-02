using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Statements
{
    /// <summary>
    /// Provides a base implementation of a switch statement.
    /// </summary>
    public class SwitchStatement :
        ControlledCollection<ISwitchCaseBlockStatement>,
        ISwitchStatement
    {
        private IBreakExit exitPoint;
        public SwitchStatement(IBlockStatementParent parent)
        {
            this.Parent = parent;
        }

        #region ISwitchStatement Members

        public ISwitchCaseBlockStatement DefaultBlock
        {
            get {
                foreach (var block in this)
                    if (block.IsDefault)
                        return block;
                return null;
            }
        }

        public IBreakExit BreakExit
        {
            get
            {
                if (this.exitPoint == null)
                    this.exitPoint = new BreakExit(this.Parent);
                return this.exitPoint;
            }
        }
        public ILocalMemberDictionary Locals
        {
            get { return this.Parent.Locals; }
        }

        public ISwitchCaseBlockStatement Case(params IExpression[] conditions)
        {
            var result = new SwitchCaseBlockStatement(this, conditions);
            this.AddImpl(result);
            return result;
        }

        public ISwitchCaseBlockStatement Case(bool isDefault, params IExpression[] conditions)
        {
            var result = new SwitchCaseBlockStatement(this, conditions);
            result.IsDefault = isDefault;
            this.AddImpl(result);
            return result;
        }

        /// <summary>
        /// The <see cref="IExpression"/> which selects the target
        /// for the constant jump table.
        /// </summary>
        public IExpression Selection { get; set; }

        #endregion

        #region IStatement Members

        public IBlockStatementParent Parent { get; private set; }

        IStatementParent IStatement.Parent { get { return this.Parent; } }

        /// <summary>
        /// Visits the <paramref name="visitor"/> based upon the type of the
        /// <see cref="IStatement"/>.
        /// </summary>
        /// <param name="visitor">The <see cref="IIntermediateCodeVisitor"/> 
        /// to visit.</param>
        /// <remarks>In this instance visits the <paramref name="visitor"/>
        /// through <see cref="IStatementVisitor.Visit(ISwitchStatement)"/>.</remarks>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="visitor"/>
        /// is null.</exception>
        public void Visit(IStatementVisitor visitor)
        {
            if (visitor == null)
                throw new ArgumentNullException("visitor");
            visitor.Visit(this);
        }

        public int Index
        {
            get { return this.Parent.IndexOf(this); }
        }

        #endregion

        public override string ToString()
        {
            return string.Format("switch ({0}) {{...", this.Selection);
        }
    }
}
