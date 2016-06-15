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
    internal class CliPropertyMemberDictionary<TProperty, TPropertyParent> :
        CliMetadataDrivenDictionary<IGeneralMemberUniqueIdentifier, int, TProperty>,
        IPropertyMemberDictionary<TProperty, TPropertyParent>,
        IPropertyMemberDictionary
        where TProperty :
            class,
            IPropertyMember<TProperty, TPropertyParent>
        where TPropertyParent :
            IPropertyParent<TProperty, TPropertyParent>
    {
        private CliFullMemberDictionary master;
        private IGeneralMemberUniqueIdentifier[] identifiers;
        private TPropertyParent parent;

        public CliPropertyMemberDictionary(TPropertyParent parent, CliFullMemberDictionary fullMembers)
            : base()
        {
            this.parent = parent;
            this.master = fullMembers;
            var set = fullMembers.ObtainSubset<IGeneralMemberUniqueIdentifier, TProperty>(CliMemberType.Property).SplitSet();
            this.Initialize(set.Item1);
            this.identifiers = set.Item2;
        }
        protected override TProperty CreateElementFrom(int index, int metadata)
        {
            return (TProperty)this.master.Values[metadata].Entry;
        }

        protected override IGeneralMemberUniqueIdentifier GetIdentifierFrom(int index, int metadata)
        {
            return this.identifiers[index];
        }

        public IMasterDictionary<IGeneralMemberUniqueIdentifier, IMember> Master
        {
            get { return this.master; }
        }

        public TPropertyParent Parent
        {
            get { return this.parent; }
        }

        IPropertyParent IPropertyMemberDictionary.Parent
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

        IMemberParent IMemberDictionary.Parent { get { return this.Parent; } }
    }
}
