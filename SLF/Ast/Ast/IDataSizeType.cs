using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast
{
    /// <summary>
    /// Defines properties and methods for working with a <see cref="IStructType"/>
    /// which represents a logical placeholder for a set of bytes of
    /// a specific <see cref="IDataSizeType.DataSize">size</see>.
    /// </summary>
    public interface IDataSizeType :
        IStructType
    {
        /// <summary>
        /// Returns the <see cref="Int32"/> value representing
        /// the number of bytes represented by the
        /// <see cref="IDataSizeType"/>.
        /// </summary>
        int DataSize { get; }
    }
}
