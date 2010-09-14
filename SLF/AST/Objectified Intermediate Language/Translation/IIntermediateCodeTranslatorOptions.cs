using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Oil;

namespace AllenCopeland.Abstraction.Slf.Translation
{
    public interface IIntermediateCodeTranslatorOptions
    {
        /// <summary>
        /// Returns/sets whether the code translator allows partial
        /// instances to be written to file, or whether the full code is
        /// written in one file.
        /// </summary>
        bool AllowPartials { get; set; }
        /// <summary>
        /// Returns the <see cref="IReadOnlyCollection{T}"/>
        /// of intermediate declarations presently being built.
        /// </summary>
        IReadOnlyCollection<IIntermediateDeclaration> BuildTrail { get; }
    }
}
