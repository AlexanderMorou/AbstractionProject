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
    /// Defines properties and methods for working with a series
    /// of structure types subordinated to a full dictionary 
    /// of types.
    /// </summary>
    public interface IStructTypeDictionary :
        ISubordinateDictionary<string, IStructType, IType>,
        IGroupedDeclarationDictionary<IStructType>
    {
        /// <summary>
        /// Returns the <see cref="ITypeParent"/>
        /// which contains the <see cref="IStructTypeDictionary"/>.
        /// </summary>
        ITypeParent Parent { get; }
    }
}