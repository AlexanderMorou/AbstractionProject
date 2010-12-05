using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Abstract
{
    public interface IAssemblyReference 
    {
        /// <summary>
        /// Returns the <see cref="IAssembly"/> associated
        /// to the <see cref="IAssemblyReference"/>.
        /// </summary>
        IAssembly Reference { get; }
        /// <summary>
        /// Returns the <see cref="IList{T}"/> of the
        /// aliases associated to the <see cref="Reference"/>.
        /// </summary>
        IList<string> Aliases { get; }
    }
}
