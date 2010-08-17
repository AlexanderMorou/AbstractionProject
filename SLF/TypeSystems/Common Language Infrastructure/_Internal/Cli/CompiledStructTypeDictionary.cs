using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Collections;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    internal class CompiledStructTypeDictionary :
        CompiledTypeDictionary<IStructType>,
        IStructTypeDictionary
    {
        internal CompiledStructTypeDictionary(ICompiledTypeParent parent, MasterDictionaryBase<string, IType> master)
            : base(parent, master, parent.UnderlyingSystemTypes.Filter(p => p.IsValueType && !p.IsEnum))
        {
        }

        #region IStructTypeDictionary Members

        public ITypeParent Parent
        {
            get { return base.parent; }
        }

        #endregion

    }
}
