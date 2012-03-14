using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using System.Reflection;
using AllenCopeland.Abstraction.Slf.Cli;

namespace AllenCopeland.Abstraction.Slf.Cli
{
    /// <summary>
    /// Defines properties and methods for handling the internal
    /// representations of <see cref="IType"/>, and <see cref="IAssembly"/>
    /// instances for the common language infrastructure.
    /// </summary>
    public interface ICliManager :
        IIdentityManager<Type, Assembly, ICompiledAssembly>
    {

    }
}
