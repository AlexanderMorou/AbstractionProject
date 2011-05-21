using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Abstract.Documentation
{
    public interface IDocCommentTypeParameterSection :
        IDocCommentNamedSection
    {
        /// <summary>
        /// Returns the <see cref="IGenericParameter"/>
        /// associated to the <see cref="IDocCommentTypeParameterSection"/>
        /// </summary>
        IGenericParameter TypeParameter { get; }
    }
}
