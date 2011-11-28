using System;
using System.Collections.Generic;
using System.Text;
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
    /// Defines properties and methods for working with an 
    /// intermediate extended instance member.
    /// </summary>
    public interface IIntermediateExtendedInstanceMember :
        IIntermediateInstanceMember,
        IExtendedInstanceMember 
    {
        /// <summary>
        /// Returns/sets whether the <see cref="IIntermediateExtendedInstanceMember"/> is 
        /// abstract (must be implemented, or is not yet 
        /// implemented).
        /// </summary>
        new bool IsAbstract { get; set; }
        /// <summary>
        /// Returns/sets whether the <see cref="IIntermediateExtendedInstanceMember"/> is
        /// virtual (can be overridden).
        /// </summary>
        new bool IsVirtual { get; set; }
        /// <summary>
        /// Returns/sets whether the <see cref="IIntermediateExtendedInstanceMember"/>
        /// finalizes the member removing the overrideable 
        /// status.
        /// </summary>
        new bool IsFinal { get; set; }
        /// <summary>
        /// Returns/sets whether the <see cref="IIntermediateExtendedInstanceMember"/> 
        /// is an override of a virtual member.
        /// </summary>
        new bool IsOverride { get; set; }
    }
}
