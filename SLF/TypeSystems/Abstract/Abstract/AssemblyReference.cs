using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Abstract
{
    public class AssemblyReference :
        IAssemblyReference
    {
        private IAssembly reference;
        private List<string> aliases = new List<string>();

        public AssemblyReference(IAssembly assembly, params string[] aliases)
        {
            this.aliases.AddRange(aliases);
        }

        #region IAssemblyReference Members

        public IAssembly Reference
        {
            get { return this.reference; }
        }

        public IList<string> Aliases
        {
            get { return this.aliases; }
        }

        #endregion
    }
}
