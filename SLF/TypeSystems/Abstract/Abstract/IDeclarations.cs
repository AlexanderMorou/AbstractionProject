﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
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
    /// Defines properties and methods for working with a series of <see cref="IDeclaration"/> instances.
    /// </summary>
    public interface IDeclarationDictionary :
        IControlledStateDictionary
    {
        /// <summary>
        /// Returns the index of the <paramref name="decl"/> provided.
        /// </summary>
        /// <param name="decl">The <see cref="IDeclaration"/> in the <see cref="IDeclarationDictionary"/> to return
        /// the index of.</param>
        /// <returns>An <see cref="Int32"/> value representing the index of the <paramref name="decl"/> in the
        /// <see cref="IDeclarationDictionary"/>, if present; -1, otherwise.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="decl"/> is null.</exception>
        int IndexOf(IDeclaration decl);
    }
}