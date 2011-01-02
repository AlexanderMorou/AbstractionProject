using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// Provides a full declaration dictionary base class.
    /// </summary>
    /// <typeparam name="TMItem">The base type of 
    /// <see cref="IDeclaration"/>.</typeparam>
    internal class FullDeclarationDictionaryBase<TMItem> :
        MasterDictionaryBase<string, TMItem>,
        IFullDeclarationDictionary<TMItem>,
        IFullDeclarationDictionary
        where TMItem :
            class,
            IDeclaration
    {
        /// <summary>
        /// Creates a new <see cref="FullDeclarationDictionaryBase{TMItem}"/> 
        /// initialized to a default state.
        /// </summary>
        public FullDeclarationDictionaryBase()
            : base()
        {
        }
    }
}
