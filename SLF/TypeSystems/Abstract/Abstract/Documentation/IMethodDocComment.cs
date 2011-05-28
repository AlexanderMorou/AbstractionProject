using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;

namespace AllenCopeland.Abstraction.Slf.Abstract.Documentation
{
    /// <summary>
    /// Defines properties and methods for working with a 
    /// documentation comment associated to a method.
    /// </summary>
    public interface IMethodDocComment : 
        ISignatureMemberDocComment
    {
        /// <summary>
        /// Returns the <see cref="IDocCommentGroup{TItem, TSection}"/>
        /// associated to the type-parameters of the 
        /// <see cref="IMethodDocComment"/>.
        /// </summary>
        IDocCommentNamedGroup<IGenericParameter, IDocCommentTypeParameterSection> TypeParameters { get; }
    }
}
