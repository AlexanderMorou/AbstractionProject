using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Cli.Members
{
    public interface ICompiledTypeCoercionMember :
        ITypeCoercionMember,
        ICompiledMember
    {
        /// <summary>
        /// Returns the <see cref="System.Reflection.MethodInfo"/> associated to the <see cref="ICompiledTypeCoercionMember"/>.
        /// </summary>
        new MethodInfo MemberInfo { get; }
    }
}
