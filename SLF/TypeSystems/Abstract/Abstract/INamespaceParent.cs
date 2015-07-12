using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// Defines properties and methods for working
    /// with the parent of a namespace.
    /// </summary>
    /// <remarks>Namespace parents
    /// are also able to contain types.</remarks>
    public interface INamespaceParent :
        IFieldParent<ITopLevelFieldMember, INamespaceParent>,
        IMethodParent<ITopLevelMethodMember, INamespaceParent>,
        ITypeParent
    {
        /// <summary>
        /// Returns the <see cref="INamespaceDictionary"/>
        /// of <see cref="INamespaceDeclaration"/> instances
        /// contained within the <see cref="INamespaceParent"/>.
        /// </summary>
        INamespaceDictionary Namespaces { get; }
        /// <summary>
        /// Returns the <see cref="IFullMemberDictionary"/> associated to the
        /// <see cref="INamespaceParent"/> and the grouped series of members
        /// associated to the fields and methods.
        /// </summary>
        IFullMemberDictionary Members { get; }
    }
}
