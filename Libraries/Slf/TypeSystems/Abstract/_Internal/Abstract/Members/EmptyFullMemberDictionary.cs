using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Utilities.Collections;

namespace AllenCopeland.Abstraction.Slf._Internal.Abstract.Members
{
    internal class EmptyFullMemberDictionary :
        MasterDictionaryBase<IGeneralMemberUniqueIdentifier, IMember>,
        IFullMemberDictionary
    {
        public static readonly EmptyFullMemberDictionary Empty = new EmptyFullMemberDictionary();

        public override bool Contains(KeyValuePair<IGeneralMemberUniqueIdentifier, MasterDictionaryEntry<IMember>> item)
        {
            return false;
        }

        public override bool ContainsKey(IGeneralMemberUniqueIdentifier key)
        {
            return false;
        }

        public override KeyValuePair<IGeneralMemberUniqueIdentifier, MasterDictionaryEntry<IMember>>[] ToArray()
        {
            return new KeyValuePair<IGeneralMemberUniqueIdentifier, MasterDictionaryEntry<IMember>>[0];
        }

        public override int Count
        {
            get
            {
                return 0;
            }
        }

        public override bool TryGetValue(IGeneralMemberUniqueIdentifier key, out MasterDictionaryEntry<IMember> value)
        {
            value = default(MasterDictionaryEntry<IMember>);
            return false;
        }
    }
}
