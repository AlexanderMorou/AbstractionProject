using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast
{
    /// <summary>
    /// Defines properties and methdos for working with
    /// a named parameter on a custom attribute to be stored
    /// in the assembly's meta-data.
    /// </summary>
    /// <typeparam name="T">The type of value associated to the
    /// named parameter.</typeparam>
    public interface IMetadatumDefinitionNamedParameter<T> :
        IMetadatumDefinitionParameter<T>,
        IMetadatumDefinitionNamedParameter
    {
    }
}
