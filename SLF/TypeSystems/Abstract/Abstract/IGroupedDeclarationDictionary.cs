﻿using System;
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
    /// Defines properties and methods for working with a series of
    /// grouped declarations that have a larger master 
    /// <see cref="IFullDeclarationDictionary"/> which indicates verbatim 
    /// order across all declaration serii.
    /// </summary>
    /// <typeparam name="TItem">The type of <see cref="IDeclaration"/>
    /// in the current implementation.</typeparam>
    public interface IGroupedDeclarationDictionary<TItem> :
        ISubordinateDictionary<string, TItem>,
        IDeclarationDictionary<TItem>
        where TItem :
            IDeclaration
    {
    }
    /// <summary>
    /// Defines properties and methods for working 
    /// with a series of <see cref="IDeclaration"/> 
    /// instances grouped by a master dictionary.
    /// </summary>
    public interface IGroupedDeclarationDictionary :
        ISubordinateDictionary,
        IDeclarationDictionary
    {
    }
}