using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Collections;
namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    internal class CompiledEnumTypeDictionary :
        CompiledTypeDictionary<IEnumType>,
        IEnumTypeDictionary
    {
        internal CompiledEnumTypeDictionary(ICompiledTypeParent parent, MasterDictionaryBase<string, IType> master)
            : base(parent, master, parent.UnderlyingSystemTypes.Filter(p => p.IsEnum))
        {
        }

        #region IEnumTypeDictionary Members

        public ITypeParent Parent
        {
            get { return base.parent; }
        }

        #endregion

    }
}
