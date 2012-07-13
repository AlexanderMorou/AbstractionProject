using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Utilities.Collections;

namespace AllenCopeland.Abstraction.Slf._Internal.Abstract
{
    internal class EmptyFullMemberDictionary :
        MasterDictionaryBase<IGeneralMemberUniqueIdentifier, IMember>,
        IFullMemberDictionary
    {
        public static readonly EmptyFullMemberDictionary Empty = new EmptyFullMemberDictionary();
    }
}
