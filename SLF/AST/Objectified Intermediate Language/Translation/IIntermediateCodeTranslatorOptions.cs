using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

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
