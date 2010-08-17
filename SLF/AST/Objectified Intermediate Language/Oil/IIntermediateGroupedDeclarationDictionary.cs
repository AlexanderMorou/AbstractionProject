using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
{
    /// <summary>
    /// Defines generic properties and methods for working with
    /// an intermediate series of grouped declarations.
    /// </summary>
    /// <typeparam name="TDeclaration">The type of declaration in the grouped declarations
    /// in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateDeclaration">The type of declaration as it exists in the
    /// intermediate abstract syntax tree.</typeparam>
    public interface IIntermediateGroupedDeclarationDictionary<TDeclaration, TIntermediateDeclaration> :
        IIntermediateDeclarationDictionary<TDeclaration, TIntermediateDeclaration>,
        IGroupedDeclarationDictionary<TDeclaration>
        where TDeclaration :
            IDeclaration
        where TIntermediateDeclaration :
            TDeclaration,
            IIntermediateDeclaration
    {

    }
    /// <summary>
    /// Defines properties and methods for working with an 
    /// intermediate series of grouped declarations.
    /// </summary>
    public interface IIntermediateGroupedDeclarationDictionary :
        IIntermediateDeclarationDictionary,
        IGroupedDeclarationDictionary
    {
    }
}
