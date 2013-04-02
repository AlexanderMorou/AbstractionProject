using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.GenericLayer
{
    internal interface _IGenericType
    {
        /// <summary>
        /// A parameter was shifted <paramref name="from"/> a specified
        /// index <paramref name="to"/> another index.
        /// </summary>
        /// <param name="from">The <see cref="Int32"/> index from which the
        /// type-parameter started.</param>
        /// <param name="to">The <see cref="Int32"/> index at which the 
        /// type-parameter ended.</param>
        void PositionalShift(int from, int to);
    }
}
