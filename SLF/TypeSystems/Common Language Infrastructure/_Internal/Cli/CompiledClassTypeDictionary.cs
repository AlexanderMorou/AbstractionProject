using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    internal class CompiledClassTypeDictionary :
        CompiledTypeDictionary<IGeneralGenericTypeUniqueIdentifier, IClassType>,
        IClassTypeDictionary
    {
        internal CompiledClassTypeDictionary(_ICompiledTypeParent parent, MasterDictionaryBase<IGeneralTypeUniqueIdentifier, IType> master)
            : base(parent, master, parent.UnderlyingSystemTypes.Filter(p => p.IsClass && (!typeof(Delegate).IsAssignableFrom(p) || typeof(Delegate) == p)))
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
