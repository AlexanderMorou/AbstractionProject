using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Cli.Members
{
    /// <summary>
    /// Defines properties and methods for wroking with a compiled coercion member.
    /// </summary>
    public interface ICompiledCoercionMember :
        ICoercionMember,
        ICompiledMember
    {
        /// <summary>
        /// Returns the <see cref="MethodInfo"/> associated to the <see cref="ICompiledCoercionMember"/> 
        /// from which it is derived.
        /// </summary>
        new MethodInfo MemberInfo { get; }
    }
}
