﻿using System.Reflection;
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
    /// Defines properties and methods for working 
    /// with a compiled property member.
    /// </summary>
    public interface ICompiledPropertyMember :
        IPropertyMember,
        ICompiledMember
    {
        /// <summary>
        /// Returns the <see cref="PropertyInfo"/> associated to the <see cref="ICompiledPropertyMember"/>.
        /// </summary>
        new PropertyInfo MemberInfo { get; }
    }
}