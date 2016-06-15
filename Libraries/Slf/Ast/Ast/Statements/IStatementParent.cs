using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Statements
{
    /// <summary>
    /// Defines properties and methods for working with the parent of a
    /// statement.
    /// </summary>
    public interface IStatementParent
    {
        /// <summary>
        /// Returns the<see cref="Int32"/> value which denotes where
        /// within the set the <paramref name="target"/> is.
        /// </summary>
        /// <param name="target">The <see cref="IStatement"/>
        /// to retrieve the index of.</param>
        /// <returns>An <see cref="Int32"/> value &gt;= to zero denoting
        /// zero-based index within the set that <paramref name="target"/>
        /// is; -1 if <paramref name="target"/> isn't a part of the set.
        /// </returns>
        int IndexOf(IStatement target);
    }
}
