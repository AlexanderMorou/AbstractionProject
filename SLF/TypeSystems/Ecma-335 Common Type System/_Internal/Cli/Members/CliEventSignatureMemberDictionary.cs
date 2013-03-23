using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Utilities.Arrays;
using AllenCopeland.Abstraction.Utilities.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Members
{
    internal class CliEventSignatureMemberDictionary<TEvent, TEventParent> :
        CliMetadataDrivenDictionary<IGeneralSignatureMemberUniqueIdentifier, int, TEvent>,
        IEventSignatureMemberDictionary<TEvent, TEventParent>,
        IEventSignatureMemberDictionary,
        ISubordinateDictionary
        where TEvent :
            class,
            IEventSignatureMember<TEvent, TEventParent>
        where TEventParent :
            IEventSignatureParent<TEvent, TEventParent>
    {
        private IGeneralSignatureMemberUniqueIdentifier[] identifiers;
        private CliFullMemberDictionary master;
        private TEventParent parent;
        internal CliEventSignatureMemberDictionary(TEventParent parent, CliFullMemberDictionary fullMembers)
            : base()
        {
            this.parent = parent;
            this.master = fullMembers;
            var set = fullMembers.ObtainSubset<IGeneralSignatureMemberUniqueIdentifier, TEvent>(CliMemberType.Event).SplitSet();
            this.Initialize(set.Item1);
            this.identifiers = set.Item2;
        }

        int IMemberDictionary.IndexOf(IMember member)
        {
            int valueIndex = this.master.Values.IndexOf(new MasterDictionaryEntry<IMember>(this, member));
            if (valueIndex == -1)
                return -1;
            return this.identifiers.GetIndexOf(master.Keys[valueIndex]);
        }

        public TEvent Find(string eventName, IDelegateType searchCriteria)
        {
            foreach (var key in this.Keys)
            {
                TEvent current = null;
                if (key.Name == eventName &&
                    (current = this[key]).SignatureType == searchCriteria)
                    return current;
            }
            return null;
        }

        public TEventParent Parent
        {
            get { return this.parent; }
        }

        public IMasterDictionary<IGeneralMemberUniqueIdentifier, IMember> Master
        {
            get
            {
                return this.master;
            }
        }

        protected override TEvent CreateElementFrom(int index, int metadata)
        {
            return (TEvent)this.master.Values[metadata].Entry;
        }

        protected override IGeneralSignatureMemberUniqueIdentifier GetIdentifierFrom(int index, int metadata)
        {
            return this.identifiers[index];
        }

        IMemberParent IMemberDictionary.Parent
        {
            get { return this.parent; }
        }

        IMasterDictionary ISubordinateDictionary.Master
        {
            get { return (IMasterDictionary)this.Master; }
        }

        IEventSignatureMember IEventSignatureMemberDictionary.Find(string eventName, IDelegateType searchCriteria)
        {
            return this.Find(eventName, searchCriteria);
        }
    }
}
