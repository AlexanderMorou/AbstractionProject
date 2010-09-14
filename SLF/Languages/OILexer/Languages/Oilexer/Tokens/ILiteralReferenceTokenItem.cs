using System;
using System.Collections.Generic;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Languages.Oilexer.Tokens
{
    public interface ILiteralReferenceTokenItem :
        ITokenItem
    {
        /// <summary>
        /// Returns the source of the literal that the <see cref="ILiteralReferenceTokenItem"/>
        /// relates to.
        /// </summary>
        ITokenEntry Source { get; }
        /// <summary>
        /// Returns the literal the <see cref="ILiteralReferenceTokenItem"/> references.
        /// </summary>
        ILiteralTokenItem Literal { get; }
    }
}
