using System;
using System.Collections.Generic;
/*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// Defines properties and methods for working with a custom attribute defined
    /// on a custom attributed declaration.
    /// </summary>
    public interface IMetadatum :
        IDisposable
    {
        /// <summary>
        /// Returns the <see cref="IType"/> the <see cref="IMetadatum"/>
        /// is.
        /// </summary>
        IType Type { get; }
        /// <summary>
        /// The <see cref="IMetadataEntity"/> on which the <see cref="IMetadatum"/> 
        /// was declared.
        /// </summary>
        IMetadataEntity DeclarationPoint { get; }

        /// <summary>
        /// Returns the <see cref="IEnumerable{T}"/> of triple elements
        /// which denote the type, name (from the constructor used), 
        /// and value of the parameters.
        /// </summary>
        IEnumerable<Tuple<IType, string, object>> Parameters { get; }
        /// <summary>
        /// Returns the <see cref="IEnumerable{T}"/> of triple elements
        /// which denote the type, name, and value of the named parameters.
        /// </summary>
        IEnumerable<Tuple<IType, string, object>> NamedParameters { get; }


    }
}
