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
    /// <summary>
    /// Defines properties and methods for working with a property defined
    /// on an intermediate class.
    /// </summary>
    public interface IIntermediateClassPropertyMember :
        IIntermediatePropertyMember<IClassPropertyMember, IIntermediateClassPropertyMember, IClassType, IIntermediateClassType>,
        IClassPropertyMember
    {
        /// <summary>
        /// Returns the implicit <see cref="ExtendedMemberAttributes"/> that determine how the
        /// <see cref="IIntermediateClassPropertyMember"/> is shown in its scope and inherited scopes.
        /// </summary>
        ExtendedMemberAttributes ImplicitAttributes { get; }
    }
}
