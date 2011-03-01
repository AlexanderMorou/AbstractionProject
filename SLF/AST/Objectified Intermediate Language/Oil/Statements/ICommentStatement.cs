using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Oil.Statements
{
    /// <summary>
    /// Defines properties and methods for working with a statement
    /// which describes a block of code.
    /// </summary>
    public interface ICommentStatement :
        IStatement
    {
        /// <summary>
        /// Returns/sets the <see cref="String"/> value
        /// representing the comment.
        /// </summary>
        string Comment { get; set; }
    }
}
