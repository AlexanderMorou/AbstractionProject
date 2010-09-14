using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Languages.Oilexer.Tokens;

namespace AllenCopeland.Abstraction.Slf.Compilers.Oilexer
{
    internal interface IUnicodeTargetPartialCategory :
        IUnicodeTargetCategory,
        IEquatable<IUnicodeTargetPartialCategory>
    {
        /// <summary>
        /// Returns the <see cref="RegularLanguageSet"/> which represents
        /// the negative-assertion to exclude when checking for the category.
        /// </summary>
        RegularLanguageSet NegativeAssertion { get; }
    }
}
