using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Statements
{
    /// <summary>
    /// Provides a base block statement.
    /// </summary>
    public class BlockStatementBase :
        BlockStatementParentContainer,
        IBlockStatement
    {
        private IBlockStatementParent parent;
        /// <summary>
        /// Creates a new <see cref="BlockStatementBase"/> instance
        /// with the <paramref name="parent"/> which contains it.
        /// </summary>
        /// <param name="parent">The <see cref="IBlockStatement"/> which
        /// contains the <see cref="BlockStatementBase"/>.</param>
        public BlockStatementBase(IBlockStatementParent parent)
            : base()
        {
            this.parent = parent;
        }

        /// <summary>
        /// Visits the <paramref name="visitor"/> based upon the type of the
        /// <see cref="IStatement"/>.
        /// </summary>
        /// <param name="visitor">The <see cref="IIntermediateCodeVisitor"/> 
        /// to visit.</param>
        /// <remarks>In this instance visits the <paramref name="visitor"/>
        /// through <see cref="IStatementVisitor.Visit(IBlockStatement)"/>.</remarks>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="visitor"/>
        /// is null.</exception>
        public virtual void Visit(IStatementVisitor visitor)
        {
            if (visitor == null)
                throw new ArgumentNullException("visitor");
            visitor.Visit(this);
        }

        /// <summary>
        /// Returns the <see cref="IBlockStatementParent"/> which
        /// contains the <see cref="IBlockStatement"/>.
        /// </summary>
        public IBlockStatementParent Parent
        {
            get
            {
                return this.parent;
            }
        }

        #region IStatement Members

        IStatementParent IStatement.Parent
        {
            get
            {
                return this.Parent;
            }
        }

        #endregion

    }
}
