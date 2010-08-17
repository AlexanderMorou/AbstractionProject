using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Cli.Members
{
    /// <summary>
    /// Defines properties and methods for working with a compiled <see cref="IMember"/>.
    /// </summary>
    public interface ICompiledMember :
        IMember
    {
        /// <summary>
        /// Returns the <see cref="System.Reflection.MemberInfo"/> associated to the <see cref="ICompiledMember"/>.
        /// </summary>
        MemberInfo MemberInfo { get; }
    }
}
