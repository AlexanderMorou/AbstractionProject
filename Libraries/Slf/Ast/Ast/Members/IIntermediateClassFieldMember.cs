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
    /// Defines properties and methods for working with a field defined
    /// on an intermediate class.
    /// </summary>
    public interface IIntermediateClassFieldMember :
        IIntermediateFieldMember<IClassFieldMember, IIntermediateClassFieldMember, IClassType, IIntermediateClassType>,
        IIntermediateInstanceMember,
        IIntermediateScopedDeclaration,
        IClassFieldMember
    {
        /// <summary>
        /// Returns/sets whether the <see cref="IIntermediateTopLevelFieldMember"/> is read-only.
        /// </summary>
        /// <remarks>Read-only fields can only be initialized during the 
        /// constructor phase of a type or instance.</remarks>
        new bool ReadOnly { get; set; }
        /// <summary>
        /// Returns/sets whether the <see cref="IIntermediateTopLevelFieldMember"/> is a constant value.
        /// </summary>
        /// <remarks>Constant values are evaluated at compile-time and folded into
        /// a single value of the appropriate data-type.</remarks>
        new bool Constant { get; set; }
        /// <summary>
        /// Returns the implicit <see cref="InstanceFieldMemberAttributes"/> that determine how the
        /// <see cref="IIntermediateClassFieldMember"/> is shown in its scope and inherited scopes.
        /// </summary>
        InstanceFieldMemberAttributes ImplicitAttributes { get; }
    }
}
