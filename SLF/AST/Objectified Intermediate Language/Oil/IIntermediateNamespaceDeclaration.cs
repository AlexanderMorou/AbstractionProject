using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Defines properties and methods for working 
    /// with an intermediate namespace declaration.
    /// </summary>
    public interface IIntermediateNamespaceDeclaration :
        IIntermediateNamespaceParent,
        IIntermediateDeclaration,
        INamespaceDeclaration,
        IIntermediateSegmentableDeclaration<IIntermediateNamespaceDeclaration>
    {
        /// <summary>
        /// Returns the <see cref="IIntermediateAssembly"/> associated
        /// to the <see cref="IIntermediateNamespaceDeclaration"/>.
        /// </summary>
        new IIntermediateAssembly Assembly { get; }
        /// <summary>
        /// Returns the <see cref="INamespaceParent"/>
        /// which contains the <see cref="INamespaceDeclaration"/>.
        /// </summary>
        new IIntermediateNamespaceParent Parent { get; }
    }
}
