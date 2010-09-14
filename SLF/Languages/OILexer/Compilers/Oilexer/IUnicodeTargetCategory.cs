using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace AllenCopeland.Abstraction.Slf.Compilers.Oilexer
{
    internal interface IUnicodeTargetCategory :
        IEquatable<IUnicodeTargetCategory>
    {
        /// <summary>
        /// Returns the <see cref="UnicodeCategory"/> 
        /// referred to by the <see cref="IUnicodeTargetCategory"/>.
        /// </summary>
        UnicodeCategory TargetedCategory { get; }
    }
}
