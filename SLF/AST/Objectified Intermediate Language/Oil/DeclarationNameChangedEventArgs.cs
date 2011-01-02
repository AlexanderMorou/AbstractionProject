using System;
using System.Collections.Generic;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
{
    /// <summary>
    /// Provides arguments for a declaration after its name has
    /// been changed.
    /// </summary>
    public class DeclarationNameChangedEventArgs :
        EventArgs
    {
        /// <summary>
        /// Returns the old name of the declaration.
        /// </summary>
        public string OldName { get; private set; }
        /// <summary>
        /// Returns the new name of the declaration.
        /// </summary>
        public string NewName { get; private set; }

        /// <summary>
        /// Creates a new <see cref="DeclarationNameChangedEventArgs"/> instance
        /// with the <paramref name="oldName"/> and
        /// <paramref name="newName"/> provided.
        /// </summary>
        /// <param name="oldName">The old name of the declaration 
        /// before the change.</param>
        /// <param name="newName">The new name of the declaration 
        /// after the change.</param>
        public DeclarationNameChangedEventArgs(string oldName, string newName)
            : base()
        {
            this.OldName = oldName;
            this.NewName = newName;
        }
    }
}
