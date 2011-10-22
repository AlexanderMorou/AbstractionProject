using System.Collections.Generic;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Events;
/*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Linkers
{
    /// <summary>
    /// Provides an <see cref="IAssemblyReference"/> 
    /// implementation which represents a reference to 
    /// an assembly.
    /// </summary>
    public class AssemblyReference :
        IAssemblyReference
    {
        private IAssembly reference;
        private IAssemblyReferenceAliasCollection aliases = new AssemblyReferenceAliasCollection();
        private AssemblyReferenceCollection parent;
        internal AssemblyReference(IAssembly assembly, AssemblyReferenceCollection parent, params string[] aliases)
        {
            this.aliases.AddRange(aliases);
            this.parent = parent;
            this.Aliases.AliasAdded += new System.EventHandler<Utilities.Events.EventArgsR1<string>>(Aliases_AliasAdded);
            this.Aliases.AliasRemoved += new System.EventHandler<Utilities.Events.EventArgsR1<string>>(Aliases_AliasRemoved);
            this.reference = assembly;
        }

        void Aliases_AliasRemoved(object sender, EventArgsR1<string> e)
        {
            this.parent.ReferenceAliasRemoved(this, e.Arg1);
        }

        void Aliases_AliasAdded(object sender, EventArgsR1<string> e)
        {
            this.parent.ReferenceAliasAdded(this, e.Arg1);
        }

        #region IAssemblyReference Members

        public IAssembly Reference
        {
            get { return this.reference; }
        }

        public IAssemblyReferenceAliasCollection Aliases
        {
            get { return this.aliases; }
        }

        #endregion
    }
}
