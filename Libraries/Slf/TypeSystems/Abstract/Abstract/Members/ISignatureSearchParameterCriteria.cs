using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;

namespace AllenCopeland.Abstraction.Slf.Abstract.Members
{
    /// <summary>
    /// Defines properties and methods for working with a search criteria
    /// to find a method or series of methods within a signatured parent.
    /// </summary>
    public interface ISignatureSearchParameterCriteria :
        IEnumerable<IType>
    {
        /// <summary>
        /// Returns the <see cref="IControlledCollection{T}"/> 
        /// which denotes the types of the parameters to search for.
        /// </summary>
        IControlledCollection<IType> Types { get; }
        /// <summary>
        /// Returns the <see cref="IControlledCollection{T}"/> 
        /// which denotes the names of the parameters to search for.
        /// </summary>
        IControlledCollection<string> Names { get; }
        /// <summary>
        /// Returns the <see cref="Int32"/> value which determines the
        /// offset on which <see cref="Names"/> start.
        /// </summary>
        /// <remarks>Returns -1 when <see cref="Names"/>
        /// is empty.</remarks>
        int NamedStartIndex { get; }
    }
}
