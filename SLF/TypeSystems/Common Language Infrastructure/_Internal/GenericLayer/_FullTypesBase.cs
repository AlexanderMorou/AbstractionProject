using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.GenericLayer
{
    internal class _FullTypesBase :
        _GroupedMasterBase<IType>,
        IFullTypeDictionary
    {
        internal _FullTypesBase()
            : base()
        {
        }

        #region IFullTypeDictionary Members

        public IType FindTypeByName(string typeName, int typeParameterCount = 0)
        {
            foreach (var type in this.Values)
            {
                var entry = type.Entry;
                if (entry.Name == typeName)
                {
                    if (typeParameterCount > 0 && entry.IsGenericConstruct)
                    {
                        var generic = entry as IGenericType;
                        if (generic == null)
                            continue;
                        if (generic.TypeParameters.Count == typeParameterCount)
                            return generic;
                    }
                    else if (typeParameterCount == 0 && !entry.IsGenericConstruct)
                        return entry;
                }
            }
            return null;
        }

        #endregion

    }
}
