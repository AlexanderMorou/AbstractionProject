using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions.Linq
{
    /// <summary>
    /// Defines properties and methods for working with an
    /// explicitly typed range variable within a language
    /// integrated query.
    /// </summary>
    public interface ILinqTypedRangeVariable :
        ILinqRangeVariable
    {
        /// <summary>
        /// Returns/sets the <see cref="IType"/> which identifies 
        /// the kind of <see cref="ILinqTypedRangeVariable"/>.
        /// </summary>
        IType RangeType { get; set; }
    }
}
