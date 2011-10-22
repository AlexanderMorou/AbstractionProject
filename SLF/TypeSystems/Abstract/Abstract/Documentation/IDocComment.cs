 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Abstract.Documentation
{
    /// <summary>
    /// Defines properties and methods for working with a
    /// comment that represents a portion of a document
    /// that describes a member, type, or element of a
    /// member or type.
    /// </summary>
    public interface IDocComment
    {
        /// <summary>
        /// Returns the <see cref="IDocCommentSection"/>
        /// which describes a basic summary, about the element described,
        /// associated to the <see cref="IDocComment"/>.
        /// </summary>
        IDocCommentSection Summary { get; }
        /// <summary>
        /// Returns the <see cref="IDocCommentSection"/>
        /// which describes a basic remarks, about the element described,
        /// associated to the <see cref="IDOcumentationComment"/>
        /// </summary>
        IDocCommentSection Remarks { get; }
    }
}
