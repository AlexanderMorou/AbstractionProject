using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
{
    public interface ICustomAttributeDefinitionParameter<T> :
        ICustomAttributeDefinitionParameter
    {
        /// <summary>
        /// Returns/sets the <typeparam name="T"/> 
        /// value defined on one of the 
        /// <see cref="ICustomAttributeDefinition"/>'s
        /// constructor argument(s).
        /// </summary>
        new T Value { get; set; }
    }
}
