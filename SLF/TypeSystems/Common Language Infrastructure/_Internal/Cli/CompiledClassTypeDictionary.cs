using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Collections;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    internal class CompiledClassTypeDictionary :
        CompiledTypeDictionary<IClassType>,
        IClassTypeDictionary
    {
        internal CompiledClassTypeDictionary(ICompiledTypeParent parent, MasterDictionaryBase<string, IType> master)
            : base(parent, master, parent.UnderlyingSystemTypes.Filter(p => p.IsClass && (!typeof(Delegate).IsAssignableFrom(p))))
        {
        }

        #region IClassTypeDictionary Members

        public ITypeParent Parent
        {
            get { return base.parent; }
        }

        #endregion

    }
}
