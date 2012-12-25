using AllenCopeland.Abstraction.Utilities.Arrays;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AllenCopeland.Abstraction.Utilities.Collections;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Members
{
    internal class CliEventMemberDictionary<TEvent, TEventParent> :
        CliMetadataDrivenDictionary<IGeneralSignatureMemberUniqueIdentifier, int, TEvent>,
        IEventMemberDictionary<TEvent, TEventParent>,
        IEventMemberDictionary
        where TEvent :
            class,
            IEventMember<TEvent, TEventParent>
        where TEventParent :
            IEventParent<TEvent, TEventParent>
    {
        private IGeneralSignatureMemberUniqueIdentifier[] identifiers;
        private CliFullMemberDictionary master;
        private TEventParent parent;
        internal CliEventMemberDictionary(TEventParent parent, CliFullMemberDictionary fullMembers)
            : base()
        {
            this.parent = parent;
            this.master = fullMembers;
            var set = fullMembers.ObtainSubset<IGeneralSignatureMemberUniqueIdentifier, TEvent>(CliMemberType.Event).SplitSet();
            this.Initialize(set.Item1);
            this.identifiers = set.Item2;
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

        IMasterDictionary ISubordinateDictionary.Master
        {
            get { return (IMasterDictionary)this.Master; }
        }
    }
}
