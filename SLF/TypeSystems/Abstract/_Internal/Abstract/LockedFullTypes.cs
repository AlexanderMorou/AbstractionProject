using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
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

    }
}
