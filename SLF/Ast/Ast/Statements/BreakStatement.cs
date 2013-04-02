using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Statements
{
    /// <summary>
    /// Provides an implementation of a break statement; which, in
    /// turn, is a goto statement where the target is the exit-point
    /// for break-capable block parents.
    /// </summary>
    public class BreakStatement :
        StatementBase,
        IBreakStatement
    {
        private IBreakExit target;

        public BreakStatement(IBlockStatement parent, IBreakExit target)
            : base(parent)
        {
            this.target = target;
        }

        #region IBreakStatement Members

        public IBreakExit Target
        {
            get { return this.target; }
        }

        #endregion

        #region IJumpStatement Members

        IJumpTarget IJumpStatement.Target
        {
            get
            {
                return this.Target;
            }
            set
            {
                throw new NotSupportedException("Breaks only target the break target of a given region.");
            }
        }

        #endregion

        /// <summary>
        /// Visits the <paramref name="visitor"/> based upon the type of the
        /// <see cref="IStatement"/>.
        /// </summary>
        /// <param name="visitor">The <see cref="IIntermediateCodeVisitor"/> 
        /// to visit.</param>
        /// <remarks>In this instance visits the <paramref name="visitor"/>
        /// through <see cref="IStatementVisitor.Visit(IBreakStatement)"/>.</remarks>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="visitor"/>
        /// is null.</exception>
        public override void Visit(IStatementVisitor visitor)
        {
            if (visitor == null)
                throw new ArgumentNullException("visitor");
            visitor.Visit(this);
        }
    }
}
