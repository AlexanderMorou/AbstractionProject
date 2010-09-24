using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Cli.Members
{
    /// <summary>
    /// Defines properties and methods for working with a compiled method member.
    /// </summary>
    public interface ICompiledMethodMember :
        IMethodMember,
        ICompiledMember
    {
        /// <summary>
        /// Returns the <see cref="MethodInfo"/> associated to the <see cref="ICompiledMethodMember"/>.
        /// </summary>
        new MethodInfo MemberInfo { get; }
    }
}
