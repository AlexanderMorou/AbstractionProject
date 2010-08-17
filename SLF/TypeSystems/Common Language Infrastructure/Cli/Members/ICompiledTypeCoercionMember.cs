using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli.Members;
using System.Reflection;

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
