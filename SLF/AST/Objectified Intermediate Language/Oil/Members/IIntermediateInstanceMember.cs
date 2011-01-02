using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Members
{
    /// <summary>
    /// Defines properties and methods for working 
    /// with an intermediate instance member.
    /// </summary>
    public interface IIntermediateInstanceMember : 
        IIntermediateMember,
        IInstanceMember
    {
        /// <summary>
        /// Returns/sets whether the <see cref="IIntermediateInstanceMember"/>
        /// hides the original definition completely.
        /// </summary>
        new bool IsHideBySignature { get; set; }
        /// <summary>
        /// Returns/sets whether the <see cref="IIntermediateInstanceMember"/> is
        /// static.
        /// </summary>
        new bool IsStatic { get; set; }
        /// <summary>
        /// Returns whether the <see cref="IIntermediateInstanceMember"/>
        /// is explicitly declared as static.
        /// </summary>
        /// <remarks>
        /// Important in langauges such as C# where a non-static method
        /// is a syntactically legal declaration, but it will not compile.</remarks>
        bool IsExplicitStatic { get; }
    }
}
