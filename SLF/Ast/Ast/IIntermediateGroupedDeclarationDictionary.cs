using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast
{
    /// <summary>
    /// Defines generic properties and methods for working with
    /// an intermediate series of grouped declarations.
    /// </summary>
    /// <typeparam name="TIdentifier">The kind of identifier used
    /// to differentiate the <typeparamref name="TIntermediateDeclaration"/>
    /// instances from one another.</typeparam>
    /// <typeparam name="TDeclaration">The type of declaration in the grouped declarations
    /// in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateDeclaration">The type of declaration as it exists in the
    /// intermediate abstract syntax tree.</typeparam>
    public interface IIntermediateGroupedDeclarationDictionary<TIdentifier, TDeclaration, TIntermediateDeclaration> :
        IIntermediateDeclarationDictionary<TIdentifier, TDeclaration, TIntermediateDeclaration>,
        IGroupedDeclarationDictionary<TIdentifier, TDeclaration>
        where TIdentifier :
            IDeclarationUniqueIdentifier
        where TDeclaration :
            IDeclaration
        where TIntermediateDeclaration :
            IIntermediateDeclaration,
            TDeclaration
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
