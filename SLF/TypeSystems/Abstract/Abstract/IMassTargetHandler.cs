using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// Defines properties and methods for working with a type
    /// which controlls lots of data.
    /// </summary>
    public interface IMassTargetHandler :
        IDisposable
    {
        /// <summary>
        /// Begins an exodus upon the <see cref="IMassTargetHandler"/>.
        /// </summary>
        void BeginExodus();
        /// <summary>
        /// Ends an exodus upon the <see cref="IMassTargetHandler"/>.
        /// </summary>
        void EndExodus();
    }
}
