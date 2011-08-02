using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Utilities.Collections
{
    /// <summary>
    /// Provides a base implementation of a read only collection.
    /// </summary>
    /// <typeparam name="T">The type of elements within the
    /// read only collection.</typeparam>
    public class ReadOnlyCollection<T> :
        ControlledStateCollection<T>,
        IReadOnlyCollection<T>
    {
        public ReadOnlyCollection()
            : base(new List<T>())
        {

        }
        public ReadOnlyCollection(IList<T> baseCollection)
            : base(baseCollection)
        {

        }
    }
}
