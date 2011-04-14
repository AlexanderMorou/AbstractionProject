using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Oil.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
{
    public interface IIntermediateNamespaceParent :
        IIntermediateFieldParent<ITopLevelField, IIntermediateTopLevelField, INamespaceParent, IIntermediateNamespaceParent>,
        IIntermediateMethodParent<ITopLevelMethod, IIntermediateTopLevelMethod, INamespaceParent, IIntermediateNamespaceParent>,
        IIntermediateTypeParent,
        INamespaceParent
    {
        /// <summary>
        /// Returns the namespaces associated to the
        /// <see cref="IIntermediateNamespaceParent"/>.
        /// </summary>
        new IIntermediateNamespaceDictionary Namespaces { get; }
    }
}
