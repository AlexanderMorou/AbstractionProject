using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.Abstract
{
    internal class LockedFullTypes :
        MasterDictionaryBase<string, IType>,
        IFullTypeDictionary
    {
        //List<MasterDictionaryEntry<IType>> sourceData;
        /// <summary>
        /// Creates a new <see cref="LockedFullMembersBase"/>
        /// initialized to a default state.
        /// </summary>
        public LockedFullTypes()
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
