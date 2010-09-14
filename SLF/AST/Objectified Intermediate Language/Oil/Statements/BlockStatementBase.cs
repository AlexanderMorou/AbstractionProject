using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;

namespace AllenCopeland.Abstraction.Slf.Oil.Statements
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
        /// Creates a new <see cref="BlockStatementParent"/> instance
        /// with the <paramref name="parent"/> which contains it.
        /// </summary>
        /// <param name="parent">The <see cref="IBlockStatement"/> which
        /// contains the <see cref="BlockStatementBase"/>.</param>
        public BlockStatementBase(IBlockStatementParent parent)
            : base()
        {
            this.parent = parent;
            base.SetOwner(this);
        }

        /// <summary>
        /// Visits the <paramref name="visitor"/> based upon the type of the
        /// <see cref="IStatement"/>.
        /// </summary>
        /// <param name="visitor">The <see cref="IIntermediateCodeVisitor"/> 
        /// to visit.</param>
        /// <remarks>In this instance visits the <paramref name="visitor"/>
        /// through <see cref="IIntermediateCodeVisitor.Visit(IBlockStatement)"/>.</remarks>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="visitor"/>
        /// is null.</exception>
        public virtual void Visit(IStatementVisitor visitor)
        {
            if (visitor == null)
                throw new ArgumentNullException("visitor");
            visitor.Visit(this);
        }

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
