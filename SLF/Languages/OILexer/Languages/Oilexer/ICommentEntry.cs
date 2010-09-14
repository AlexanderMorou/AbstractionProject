using System;
using System.Collections.Generic;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Languages.Oilexer
{
    public interface ICommentEntry :
        IEntry
    {
        /// <summary>
        /// Returns the comment the <see cref="ICommentEntry"/> represents.
        /// </summary>
        string Comment { get; }
    }
}
