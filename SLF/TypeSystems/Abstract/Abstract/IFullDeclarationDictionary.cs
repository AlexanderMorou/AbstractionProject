using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// Defines generic properties and methods for 
    /// working with the full master dictionary 
    /// of a series of <see cref="IGroupedDeclarationDictionary"/>.
    /// </summary>
    /// <typeparam name="TMItem">The base type of 
    /// <see cref="IDeclaration"/>.</typeparam>
    public interface IFullDeclarationDictionary<TMItem> :
        IMasterDictionary<string, TMItem>
        where TMItem :
            class,
            IDeclaration
    {
    }

    /// <summary>
    /// Defines properties and methods for 
    /// working with the full master dictionary 
    /// of a series of <see cref="IGroupedDeclarationDictionary"/>.
    /// </summary>
    public interface IFullDeclarationDictionary :
        IMasterDictionary
    {

    }
}
