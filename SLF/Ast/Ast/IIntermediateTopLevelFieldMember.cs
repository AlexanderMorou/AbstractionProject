using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using AllenCopeland.Abstraction.Slf.Ast.Modules;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
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
        /// Returns/sets the <see cref="IIntermediateModule"/> in which the 
        /// <see cref="IIntermediateTopLevelFieldMember"/> should be declared.
        /// </summary>
        new IIntermediateModule DeclaringModule { get; set; }
    }
}
