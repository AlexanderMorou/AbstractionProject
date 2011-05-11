using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Languages
{
    internal class LanguageVendorBase :
        ILanguageVendor
    {
        internal LanguageVendorBase(string name, Guid guid)
        {
            this.Name = name;
            this.Guid = guid;
        }
        #region ILanguageVendor Members

        public string Name { get; private set; }

        public Guid Guid { get; private set; }

        #endregion

    }
}
