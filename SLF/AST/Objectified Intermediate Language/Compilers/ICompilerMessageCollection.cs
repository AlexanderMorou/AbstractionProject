using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Compilers
{
    /// <summary>
    /// Defines properties and methods for working with a series of <see cref="ICompilerMessage"/>
    /// instances as either warnings or errors.
    /// </summary>
    public interface ICompilerErrorCollection :
        IControlledStateCollection<ICompilerMessage>
    {
        /// <summary>
        /// Creates and inserts a <see cref="ICompilerSourceWarning"/>
        /// with the <paramref name="message"/>, <paramref name="column"/>, <paramref name="line"/>, 
        /// and <paramref name="fileName"/> provided.
        /// </summary>
        /// <param name="message">The <see cref="ICompilerReferenceWarning"/> instance from
        /// which the <see cref="ICompilerSourceWarning"/> obtains its message.</param>
        /// <param name="fileName">The <see cref="String"/> value relative to where the file 
        /// can be found associated to the warning.</param>
        /// <param name="line">The <see cref="Int32"/> value relative to the line on which the
        /// warning relates to.</param>
        /// <param name="column">The <see cref="Int32"/> value relative to the column on which the
        /// warning relates to.</param>
        /// <param name="replacements">A sequence of <see cref="String"/> values
        /// which denote the data points to replace within the <paramref name="message"/> provided.</param>
        /// <returns>A <see cref="ICompilerSourceWarning"/> instance
        /// which denotes the details of the compiler warning.</returns>
        ICompilerSourceWarning Warning(ICompilerReferenceWarning message, string fileName, int line, int column, params string[] replacements);
        /// <summary>
        /// Creates and inserts a <see cref="ICompilerSourceError"/> 
        /// with the <paramref name="message"/>, <paramref name="column"/>, <paramref name="line"/>, 
        /// and <paramref name="fileName"/> provided.
        /// </summary>
        /// <param name="message">The <see cref="String"/> value associated to the error text.</param>
        /// <param name="fileName">The <see cref="String"/> value relative to where the file 
        /// can be found.</param>
        /// <param name="line">The <see cref="Int32"/> value relative to the line on which the
        /// error occurred.</param>
        /// <param name="column">The <see cref="Int32"/> value relative to the column on which the
        /// error occurred.</param>
        /// <param name="replacements">A sequence of <see cref="String"/> values
        /// which denote the data points to replace within the <paramref name="message"/> provided.</param>
        /// <returns>A <see cref="ICompilerSourceError"/> instance
        /// which denotes the details of the compiler error.</returns>
        ICompilerSourceError Error(ICompilerReferenceError message, string fileName, int line, int column, params string[] replacements);

        /// <summary>
        /// Returns whether the <see cref="ICompilerErrorCollection"/> 
        /// has an <see cref="ICompilerSourceError"/>.
        /// </summary>
        bool HasErrors { get; }

    }
}
