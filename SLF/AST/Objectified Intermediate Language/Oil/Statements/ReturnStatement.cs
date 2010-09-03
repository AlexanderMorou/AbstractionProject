using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil.Expressions;
using AllenCopeland.Abstraction.Slf.Oil.Properties;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Statements
{
    /// <summary>
    /// Provides an implementation of a return statement.
    /// </summary>
    public class ReturnStatement :
        StatementBase,
        IReturnStatement
    {
        /// <summary>
        /// Creates a new <see cref="ReturnStatement"/> with the 
        /// <paramref name="parent"/> block and <paramref name="returnValue"/> provided.
        /// </summary>
        /// <param name="parent">The <see cref="IBlockStatement"/>
        /// in which the current <see cref="ReturnStatement"/>
        /// was declared.</param>
        /// <param name="returnValue">The <see cref="IExpression"/> which represents 
        /// the <see cref="ReturnStatement"/> returns.</param>
        public ReturnStatement(IStatementParent parent, IExpression returnValue)
            : base(parent)
        {
            this.ReturnValue = returnValue;
        }

        /// <summary>
        /// Creates a new <see cref="ReturnStatement"/> with the 
        /// <paramref name="parent"/> block provided.
        /// </summary>
        /// <param name="parent">The <see cref="IBlockStatement"/>
        /// in which the current <see cref="ReturnStatement"/>
        /// was declared.</param>
        public ReturnStatement(IStatementParent parent)
            : base(parent)
        {
        }

        #region IReturnStatement Members

        /// <summary>
        /// Returns/sets the <see cref="IExpression"/> which represents the <see cref="ReturnStatement"/>
        /// returns.
        /// </summary>
        public IExpression ReturnValue { get; set; }
        #endregion

        public override string ToString()
        {
            if (this.ReturnValue == null)
                return string.Format("{0};",Resources.Statement_Return_Keyword);
            else
                return string.Format("{0} {1};", Resources.Statement_Return_Keyword, ReturnValue.ToString());
        }
        /// <summary>
        /// Visits the <paramref name="visitor"/> based upon the type of the
        /// <see cref="IStatement"/>.
        /// </summary>
        /// <param name="visitor">The <see cref="IIntermediateCodeVisitor"/> 
        /// to visit.</param>
        /// <remarks>In this instance visits the <paramref name="visitor"/>
        /// through <see cref="IIntermediateCodeVisitor.Visit(IReturnStatement)"/>.</remarks>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="visitor"/>
        /// is null.</exception>
        public override void Visit(IIntermediateCodeVisitor visitor)
        {
            if (visitor == null)
                throw new ArgumentNullException("visitor");
            visitor.Visit(this);
        }
        
    }
}
