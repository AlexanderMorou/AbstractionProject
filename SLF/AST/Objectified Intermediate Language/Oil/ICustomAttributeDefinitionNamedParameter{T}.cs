using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
{
    /// <summary>
    /// Defines properties and methdos for working with
    /// a named parameter on a custom attribute to be stored
    /// in the assembly's meta-data.
    /// </summary>
    /// <typeparam name="T">The type of value associated to the
    /// named parameter.</typeparam>
    public interface ICustomAttributeDefinitionNamedParameter<T> :
        ICustomAttributeDefinitionParameter<T>,
        ICustomAttributeDefinitionNamedParameter
    {
    }
}
