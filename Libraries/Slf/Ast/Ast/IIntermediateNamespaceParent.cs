using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Ast.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast
{
    public interface IIntermediateNamespaceParent :
        IIntermediateFieldParent<ITopLevelFieldMember, IIntermediateTopLevelFieldMember, INamespaceParent, IIntermediateNamespaceParent>,
        IIntermediateMethodParent<ITopLevelMethodMember, IIntermediateTopLevelMethodMember, INamespaceParent, IIntermediateNamespaceParent>,
        IIntermediateTypeParent,
        INamespaceParent
    {
        /// <summary>
        /// Returns the namespaces associated to the
        /// <see cref="IIntermediateNamespaceParent"/>.
        /// </summary>
        new IIntermediateNamespaceDictionary Namespaces { get; }
        /// <summary>
        /// Returns the <see cref="IIntermediateFullMemberDictionary"/> associated to the
        /// <see cref="IIntermediateNamespaceParent"/> and the grouped series of members
        /// associated to the fields and members.
        /// </summary>
        new IIntermediateFullMemberDictionary Members { get; }

    }
}
