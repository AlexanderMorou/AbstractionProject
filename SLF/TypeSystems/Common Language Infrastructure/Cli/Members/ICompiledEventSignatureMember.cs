using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
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
    /// Defines properties and methods for working 
    /// with a compiled event signature member.
    /// </summary>
    public interface ICompiledEventSignatureMember :
        IEventSignatureMember,
        ICompiledMember
    {
        /// <summary>
        /// Returns the <see cref="System.Reflection.EventInfo"/> 
        /// associated to the <see cref="ICompiledEventSignatureMember"/>.
        /// </summary>
        new EventInfo MemberInfo { get; }
    }
}
