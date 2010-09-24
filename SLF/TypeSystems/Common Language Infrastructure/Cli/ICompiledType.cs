using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Cli
{
    /// <summary>
    /// Defines properties and methods for working with a compiled <see cref="IType"/> or <see cref="System.Type"/>.
    /// </summary>
    public interface ICompiledType :
        IType
    {
        /// <summary>
        /// Returns the <see cref="System.Type"/> which the <see cref="ICompiledType"/> refers to.
        /// </summary>
        Type UnderlyingSystemType { get; }
    }
}
