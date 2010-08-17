using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;

namespace AllenCopeland.Abstraction.Slf.Oil.Statements
{
    /// <summary>
    /// Provides a base implementation of a switch statement.
    /// </summary>
    public class SwitchStatement :
        ControlledStateCollection<ISwitchCaseBlockStatement>,
        ISwitchStatement
    {

        public SwitchStatement(IStatementParent parent)
        {
            this.Parent = parent;
        }

        #region ISwitchStatement Members

        public ISwitchCaseBlockStatement DefaultBlock
        {
            get { throw new NotImplementedException(); }
        }

        public IBreakExit BreakExit
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region IStatement Members

        public IStatementParent Parent { get; private set; }

        /// <summary>
        /// Visits the <paramref name="visitor"/> based upon the type of the
        /// <see cref="IStatement"/>.
        /// </summary>
        /// <param name="visitor">The <see cref="IIntermediateCodeVisitor"/> 
        /// to visit.</param>
        /// <remarks>In this instance visits the <paramref name="visitor"/>
        /// through <see cref="IIntermediateCodeVisitor.Visit(ISwitchStatement)"/>.</remarks>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="visitor"/>
        /// is null.</exception>
        public void Visit(IIntermediateCodeVisitor visitor)
        {
            if (visitor == null)
                throw new ArgumentNullException("visitor");
            visitor.Visit(this);
        }

        #endregion
    }
}
