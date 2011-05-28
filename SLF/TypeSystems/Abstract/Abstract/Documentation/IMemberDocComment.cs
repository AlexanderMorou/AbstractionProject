using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Abstract.Documentation
{
    /// <summary>
    /// Defines properties and methods for working with 
    /// a member which contains documentation comments
    /// that contains code behind its function.
    /// </summary>
    public interface ICodeMemberDocComment :
        IDocComment
    {
        /// <summary>
        /// Returns the <see cref="IDocCommentExceptionGroup"/>
        /// which details the exceptions that can be raised by the
        /// <see cref="IMemberDocComment"/>.
        /// </summary>
        IDocCommentExceptionGroup Exceptions { get; }
    }
}
