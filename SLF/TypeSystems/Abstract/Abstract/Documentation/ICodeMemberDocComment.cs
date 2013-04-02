using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

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
