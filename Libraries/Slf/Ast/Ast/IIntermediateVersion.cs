using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;

namespace AllenCopeland.Abstraction.Slf.Ast
{
    /// <summary>
    /// Defines properties and methods describing an intermediate version
    /// which is malleable.
    /// </summary>
    public interface IIntermediateVersion :
        IVersion
    {
        /// <summary>
        /// Returns/sets whether the <see cref="Build"/> portion is 
        /// automatically incremented.
        /// </summary>
        bool AutoIncrementBuild { get; set; }
        /// <summary>
        /// Returns/sets whether the <see cref="Revision"/> portion is
        /// automatically incremented.
        /// </summary>
        bool AutoIncrementRevision { get; set; }
        /// <summary>
        /// Returns/sets the <see cref="Int32"/> value denoting the major
        /// version.
        /// </summary>
        new int Major { get; set; }
        /// <summary>
        /// Returns/sets the <see cref="Int32"/> value denoting the minor
        /// version.
        /// </summary>
        new int Minor { get; set; }
        /// <summary>
        /// Returns/sets the <see cref="Int32"/> value denoting the build
        /// version.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">
        /// thrown on set when <see cref="AutoIncrementBuild"/> is true.</exception>
        new int Build { get; set; }
        /// <summary>
        /// Returns/sets the <see cref="Int32"/> value denoting the revision
        /// version.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">
        /// thrown on set when <see cref="AutoIncrementBuild"/> is true.</exception>
        new int Revision { get; set; }
    }
}
