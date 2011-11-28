using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Statements
{
    public class CommentStatement :
        StatementBase,
        ICommentStatement
    {
        public CommentStatement(IStatementParent parent, string comment)
            : base(parent)
        {
            this.Comment = comment;
        }
        #region ICommentStatement Members

        /// <summary>
        /// Returns/sets the <see cref="String"/> value
        /// representing the comment.
        /// </summary>
        public string Comment { get; set; }

        #endregion

        public override void Visit(IStatementVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override string ToString()
        {
            return string.Format("//{0}", this.Comment);
        }
    }
}
