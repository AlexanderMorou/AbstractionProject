using System;
using System.Collections.Generic;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions.Linq
{
    /// <summary>
    /// Provides an abstract base class for the body of a language
    /// integrated query expression.
    /// </summary>
    public abstract class LinqBody :
        ILinqBody
    {
        private ILinqClauseCollection clauses;
        /// <summary>
        /// Creates a new <see cref="LinqBody"/> initialized to 
        /// a default state.
        /// </summary>
        protected LinqBody()
        {

        }

        #region ILinqBody Members

        /// <summary>
        /// Returns the <see cref="ILinqClauseCollection"/>
        /// associated to the <see cref="ILinqBody"/>.
        /// </summary>
        public ILinqClauseCollection Clauses
        {
            get
            {
                if (this.clauses == null)
                    this.clauses = new LinqClauseCollection();
                return this.clauses;
            }
        }

        #endregion

        /// <summary>
        /// Returns a <see cref="String"/> value representing the current
        /// <see cref="LinqBody"/>.
        /// </summary>
        /// <returns>A <see cref="String"/> value representing the current
        /// <see cref="LinqBody"/>.</returns>
        public override string ToString()
        {
            return BodyToString();
        }

        internal string BodyToString()
        {
            StringBuilder resultBuilder = new StringBuilder();
            if (this.clauses != null)
                foreach (var clause in this.clauses)
                    resultBuilder.AppendLine(clause.ToString());
            return resultBuilder.ToString();
        }

        /// <summary>
        /// Visits the elements of the <see cref="LinqBody"/>.
        /// </summary>
        /// <param name="visitor">The <see cref="IIntermediateCodeVisitor"/>
        /// to which the <see cref="LinqBody"/> needs to repay the visit
        /// to.</param>
        public abstract void Visit(IIntermediateCodeVisitor visitor);

    }
}
