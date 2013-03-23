using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Utilities.Arrays;
using AllenCopeland.Abstraction.Utilities.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    internal class CliIndexerMemberDictionary<TIndexer, TIndexerParent> :
        CliMetadataDrivenDictionary<IGeneralSignatureMemberUniqueIdentifier, int, TIndexer>,
        IIndexerMemberDictionary<TIndexer, TIndexerParent>,
        IIndexerMemberDictionary
        where TIndexer :
            class,
            IIndexerMember<TIndexer, TIndexerParent>
        where TIndexerParent :
            IIndexerParent<TIndexer, TIndexerParent>
    {
        private CliFullMemberDictionary master;
        private IGeneralSignatureMemberUniqueIdentifier[] identifiers;
        private TIndexerParent parent;

        public CliIndexerMemberDictionary(TIndexerParent parent, CliFullMemberDictionary fullMembers)
            : base()
        {
            this.parent = parent;
            this.master = fullMembers;
            var set = fullMembers.ObtainSubset<IGeneralSignatureMemberUniqueIdentifier, TIndexer>(CliMemberType.Indexer).SplitSet();
            this.Initialize(set.Item1);
            this.identifiers = set.Item2;
        }

        protected override TIndexer CreateElementFrom(int index, int metadata)
        {
            return (TIndexer)this.master.Values[metadata].Entry;
        }

        protected override IGeneralSignatureMemberUniqueIdentifier GetIdentifierFrom(int index, int metadata)
        {
            return this.identifiers[index];
        }

        public IMasterDictionary<IGeneralMemberUniqueIdentifier, IMember> Master
        {
            get { return this.master; }
        }


        public TIndexerParent Parent
        {
            get { return this.parent; }
        }

        IMemberParent IMemberDictionary.Parent
        {
            get { return this.Parent; }
        }

        int IMemberDictionary.IndexOf(IMember member)
        {
            int valueIndex = this.master.Values.IndexOf(new MasterDictionaryEntry<IMember>(this, member));
            if (valueIndex == -1)
                return -1;
            return this.identifiers.GetIndexOf(master.Keys[valueIndex]);
        }

        IMasterDictionary ISubordinateDictionary.Master
        {
            get { return (IMasterDictionary)this.Master; }
        }
    }
}
