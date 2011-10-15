using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Cli
{
    public interface ICompiledGenericTypeParameter<TTypeIdentifier, TType> :
        ICompiledType,
        IGenericTypeParameter<TTypeIdentifier, TType>,
        ICompiledGenericParameter
        where TTypeIdentifier :
            IGenericTypeUniqueIdentifier<TTypeIdentifier>
        where TType :
            IGenericType<TTypeIdentifier, TType>
    {

    }
}
