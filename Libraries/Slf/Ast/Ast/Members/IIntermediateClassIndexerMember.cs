using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Members
{
    public interface IIntermediateClassIndexerMember :
        IIntermediateIndexerMember<IClassIndexerMember, IIntermediateClassIndexerMember, IClassType, IIntermediateClassType>,
        IClassIndexerMember
    {
        /// <summary>
        /// Returns the implicit <see cref="ExtendedMemberAttributes"/> that determine how the
        /// <see cref="IIntermediateClassIndexerMember"/> is shown in its scope and inherited scopes.
        /// </summary>
        ExtendedMemberAttributes ImplicitAttributes { get; }
    }
}
