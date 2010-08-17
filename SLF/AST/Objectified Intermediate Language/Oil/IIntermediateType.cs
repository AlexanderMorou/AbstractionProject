using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil.Modules;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Oil.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
{
    /// <summary>
    /// Defines properties and methods for working with an intermediate type.
    /// </summary>
    public interface IIntermediateType :
        IEquatable<IIntermediateType>,
        IIntermediateCustomAttributedDeclaration,
        IIntermediateScopedDeclaration,
        IType
    {
        /// <summary>
        /// Returns the <see cref="IIntermediateType"/> in which the current 
        /// <see cref="IIntermediateType"/> is declared.
        /// </summary>
        new IIntermediateType DeclaringType { get; }

        /// <summary>
        /// Returns the <see cref="IIntermediateAssembly"/> in which
        /// the <see cref="IIntermediateType"/> is declared
        /// </summary>
        new IIntermediateAssembly Assembly { get; }

        /// <summary>
        /// Returns/sets the <see cref="IIntermediateModule"/>
        /// in which the <see cref="IIntermediateType"/>
        /// is defined.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">thrown when 
        /// value is null.</exception>
        /// <exception cref="System.ArgumentException">thrown when 
        /// the target module is from another assembly.</exception>
        IIntermediateModule DeclaringModule { get;  set; }

        /// <summary>
        /// Returns the <see cref="IIntermediateTypeParent"/> which contains
        /// the current <see cref="IIntermediateType"/>.
        /// </summary>
        IIntermediateTypeParent Parent { get; }

        /// <summary>
        /// Returns the <see cref="IIntermediateFullMemberDictionary"/>
        /// which designates a complete listing of the members contained
        /// within the <see cref="IIntermediateType"/>
        /// </summary>
        new IIntermediateFullMemberDictionary Members { get; }

        /// <summary>
        /// Returns the <see cref="IIntermediateNamespaceDeclaration"/>
        /// in which the current type (or its parent type) is declared.
        /// </summary>
        new IIntermediateNamespaceDeclaration Namespace { get; }
    }
}
