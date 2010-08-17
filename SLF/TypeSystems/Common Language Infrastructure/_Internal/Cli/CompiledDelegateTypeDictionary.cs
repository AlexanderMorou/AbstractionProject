using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Collections;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    internal class CompiledDelegateTypeDictionary :
        CompiledTypeDictionary<IDelegateType>,
        IDelegateTypeDictionary
    {
        internal CompiledDelegateTypeDictionary(ICompiledTypeParent parent, MasterDictionaryBase<string, IType> master)
            : base(parent, master, parent.UnderlyingSystemTypes.Filter(p => p.IsSubclassOf(typeof(Delegate))))
        {
        }

        #region IDelegateTypeDictionary Members

        public ITypeParent Parent
        {
            get { return base.parent; }
        }

        #endregion

    }
}
