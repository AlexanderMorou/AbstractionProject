using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Members
{
    /// <summary>
    /// Defines properties and methods for working with a field defined
    /// on an intermediate class.
    /// </summary>
    public interface IIntermediateClassFieldMember :
        IIntermediateFieldMember<IClassFieldMember, IIntermediateClassFieldMember, IClassType, IIntermediateClassType>,
        IIntermediateInstanceMember,
        IIntermediateScopedDeclaration,
        IClassFieldMember
    {
    }
}
