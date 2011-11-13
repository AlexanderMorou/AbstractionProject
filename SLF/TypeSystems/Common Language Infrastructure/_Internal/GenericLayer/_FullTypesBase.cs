using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.GenericLayer
{
    internal class _FullTypesBase :
        _GroupedMasterBase<IGeneralTypeUniqueIdentifier, IType>,
        IFullTypeDictionary
    {
        internal _FullTypesBase()
            : base()
        {
        }

        #region IFullTypeDictionary Members

        public IType[] GetTypesByName(string name)
        {
            return (from t in this.Values
                    where t.Entry.Name == name
                    select t.Entry).ToArray();
        }

        #endregion
    }
}
