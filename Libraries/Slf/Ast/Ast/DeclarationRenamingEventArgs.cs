using System;
using System.Collections.Generic;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast
{
    /// <summary>
    /// Provides arguments for a declaration while its
    /// name is changing.
    /// </summary>
    public class DeclarationRenamingEventArgs :
        DeclarationNameChangedEventArgs
    {
        /// <summary>
        /// Returns/sets whether the change should occur.
        /// </summary>
        public bool Change { get; set; }
        /// <summary>
        /// Creates a new <see cref="DeclarationRenamingEventArgs"/> instance with the
        /// <paramref name="oldName"/>, <paramref name="newName"/> and 
        /// <paramref name="change"/> provided.
        /// </summary>
        /// <param name="oldName">The old name of the declaration 
        /// before the change.</param>
        /// <param name="newName">The new name of the declaration 
        /// after the change.</param>
        /// <param name="change">Whether or not the name should actually be changed.</param>
        /// <remarks>If <paramref name="change"/> is omitted, it defaults to true.</remarks>
        public DeclarationRenamingEventArgs(string oldName, string newName, bool change)
            : base(oldName, newName)
        {
            this.Change = change;
        }

        /// <summary>
        /// Creates a new <see cref="DeclarationRenamingEventArgs"/> instance with the
        /// <paramref name="oldName"/>, and <paramref name="newName"/> provided.
        /// </summary>
        /// <param name="oldName">The old name of the declaration 
        /// before the change.</param>
        /// <param name="newName">The new name of the declaration 
        /// after the change.</param>
        public DeclarationRenamingEventArgs(string oldName, string newName)
            : this(oldName, newName, true)
        {
        }
    }
}
