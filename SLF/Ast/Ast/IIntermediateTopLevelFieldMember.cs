using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using AllenCopeland.Abstraction.Slf.Ast.Modules;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast
{
    public interface IIntermediateTopLevelFieldMember :
        IIntermediateFieldMember<ITopLevelFieldMember, IIntermediateTopLevelFieldMember, INamespaceParent, IIntermediateNamespaceParent>,
        ITopLevelFieldMember
    {
        /// <summary>
        /// Returns/sets the <see cref="IIntermediateModule"/> in which the 
        /// <see cref="IIntermediateTopLevelFieldMember"/> should be declared.
        /// </summary>
        new IIntermediateModule DeclaringModule { get; set; }
    }
}
