using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil.Expressions;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Statements
{
    public class CallMethodStatement :
        StatementBase,
        ICallMethodStatement
    {
        /// <summary>
        /// Creates a <see cref="CallMethodStatement"/> with the
        /// <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="parent"></param>
        public CallMethodStatement(IStatementParent parent)
            : base(parent)
        { 

        }

        #region ICallMethodStatement Members

        /// <summary>
        /// Returns/sets the <see cref="IMethodInvokeExpression"/> called by the
        /// current <see cref="CallMethodStatement"/>.
        /// </summary>
        public IMethodInvokeExpression Target { get; set; }

        #endregion

        public override string ToString()
        {
            if (Target == null)
                return string.Empty;
            return Target.ToString();
        }

        /// <summary>
        /// Visits the <paramref name="visitor"/> based upon the type of the
        /// <see cref="IStatement"/>.
        /// </summary>
        /// <param name="visitor">The <see cref="IIntermediateCodeVisitor"/> 
        /// to visit.</param>
        /// <remarks>In this instance visits the <paramref name="visitor"/>
        /// through <see cref="IIntermediateCodeVisitor.Visit(ICallMethodStatement)"/>.</remarks>
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
