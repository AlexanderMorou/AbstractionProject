﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Oil.Statements
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