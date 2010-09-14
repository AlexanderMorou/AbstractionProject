using System;
using System.Collections.Generic;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Statements
{
    /// <summary>
    /// Provides a base class for statements.
    /// </summary>
    public abstract class StatementBase :
        IStatement
    {
        /// <summary>
        /// Creates a new <see cref="StatementBase"/> with the
        /// <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="parent">The <see cref="IBlockStatement"/> which 
        /// defined the current <see cref="StatementBase"/>.</param>
        protected StatementBase(IStatementParent parent)
        {
            this.Parent = parent;
        }
        #region IStatement Members

        public IStatementParent Parent { get; internal set; }

        /// <summary>
        /// Visits the <paramref name="visitor"/> based upon the type of the
        /// <see cref="IStatement"/>.
        /// </summary>
        /// <param name="visitor">The <see cref="IIntermediateCodeVisitor"/> 
        /// to visit.</param>
        public abstract void Visit(IStatementVisitor visitor);

        #endregion
    }
}
