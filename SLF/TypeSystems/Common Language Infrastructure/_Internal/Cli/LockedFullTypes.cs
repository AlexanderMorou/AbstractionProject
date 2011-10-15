using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Collections;
using System.Linq;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    internal class LockedFullTypes :
        MasterDictionaryBase<IGeneralTypeUniqueIdentifier, IType>,
        IFullTypeDictionary
    {
        //List<MasterDictionaryEntry<IType>> sourceData;
        /// <summary>
        /// Creates a new <see cref="LockedFullTypes"/>
        /// initialized to a default state.
        /// </summary>
        public LockedFullTypes()
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
