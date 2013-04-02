using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.OldCodeGen.Expression;

namespace AllenCopeland.Abstraction.OldCodeGen.Comments
{
    /// <summary>
    /// Defines properties and methods for working with a reference to an <see cref="IMember"/>
    /// </summary>
    public interface IMemberReferenceComment :
        IDocumentationCommentParticle
    {
        /// <summary>
        /// Returns/sets the reference point.
        /// </summary>
        IMemberReferenceExpression Reference { get;set; }
    }
}
