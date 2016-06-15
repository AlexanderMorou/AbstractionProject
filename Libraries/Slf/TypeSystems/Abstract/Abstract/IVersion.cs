using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// Defines properties and methods for working with an object
    /// that represents a version.
    /// </summary>
    public interface IVersion
    {
        /// <summary>
        /// Returns the <see cref="Int32"/> value denoting the major
        /// version.
        /// </summary>
        int Major { get; }
        /// <summary>
        /// Returns the <see cref="Int32"/> value denoting the minor
        /// version.
        /// </summary>
        int Minor { get; }
        /// <summary>
        /// Returns the <see cref="Int32"/> value denoting the build
        /// version.
        /// </summary>
        int Build { get; }
        /// <summary>
        /// Returns the <see cref="Int32"/> value denoting the revision
        /// version.
        /// </summary>
        int Revision { get; }
    }
}
