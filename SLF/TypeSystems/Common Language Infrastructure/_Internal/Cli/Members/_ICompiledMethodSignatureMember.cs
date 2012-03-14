using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli.Members;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Members
{
    internal interface _ICompiledMethodSignatureMember :
        IMethodSignatureMember
    {
        /// <summary>
        /// Obtains the <see cref="ICompiledGenericParameter"/> for the
        /// <paramref name="underlyingSystemType"/> provided.
        /// </summary>
        /// <param name="underlyingSystemType">The <see cref="Type"/>
        /// from which to retrieve the <see cref="ICompiledGenericParameter"/>
        /// for.</param>
        /// <returns>A <see cref="ICompiledGenericParameter"/>
        /// from the <paramref name="underlyingSystemType"/> provided.</returns>
        ICompiledGenericParameter GetGenericParameterFor(Type underlyingSystemType);
    }
}
