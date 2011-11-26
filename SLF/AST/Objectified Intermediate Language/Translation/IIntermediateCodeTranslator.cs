using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Translation
{
    public interface IIntermediateCodeTranslator :
        IIntermediateCodeVisitor
    {
        /// <summary>
        /// Returns the <see cref="IIntermediateCodeTranslatorOptions"/> associated
        /// to the current <see cref="IIntermediateCodeTranslator"/>.
        /// </summary>
        IIntermediateCodeTranslatorOptions Options { get; }
        /// <summary>
        /// Returns the <see cref="IReadOnlyCollection{T}"/>
        /// of intermediate declarations presently being built.
        /// </summary>
        IReadOnlyCollection<IIntermediateDeclaration> BuildTrail { get; }
    }
}
