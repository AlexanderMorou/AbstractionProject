using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Cli
{
    public interface ICompiledStructType :
        IStructType,
        ICompiledType
    {
        /// <summary>
        /// Obtains a <see cref="IStructInterfaceMapping"/> 
        /// related to the <paramref name="type"/> provided.
        /// </summary>
        /// <param name="type">The <see cref="IInterfaceType"/> 
        /// to obtain the map of.</param>
        /// <returns>A <see cref="IStructInterfaceMapping"/> relative
        /// to the properties and methods implemented
        /// by the <see cref="IStructType"/> with regards
        /// to <paramref name="type"/>.</returns>
        new IStructInterfaceMapping GetInterfaceMap(IInterfaceType type);
    }
}
