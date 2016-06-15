using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Utilities.Arrays;
using AllenCopeland.Abstraction.Utilities.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Members
{
    internal class CliConstructorMemberDictionary<TCtor, TCtorParent> :
        CliMetadataDrivenDictionary<IGeneralSignatureMemberUniqueIdentifier, int, TCtor>,
        IConstructorMemberDictionary<TCtor, TCtorParent>,
        IConstructorMemberDictionary
        where TCtor :
            class,
            IConstructorMember<TCtor, TCtorParent>
        where TCtorParent :
            ICreatableParent<TCtor, TCtorParent>
    {
        private CliFullMemberDictionary master;
        private IGeneralSignatureMemberUniqueIdentifier[] identifiers;
        internal CliConstructorMemberDictionary(TCtorParent parent, CliFullMemberDictionary master) :
            base()
        {
            this.master = master;
            this.Parent = parent;
            var subset = master.ObtainSubset<IGeneralSignatureMemberUniqueIdentifier, TCtor>(CliMemberType.Constructor).SplitSet();
            this.Initialize(subset.Item1);
            this.identifiers = subset.Item2;
        }
        public TCtorParent Parent { get; private set; }

        public IMasterDictionary<IGeneralMemberUniqueIdentifier, IMember> Master
        {
            get { return this.master; }
        }

        IMemberParent IMemberDictionary.Parent
        {
            get { throw new NotImplementedException(); }
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

        protected override TCtor CreateElementFrom(int index, int metadata)
        {
            return (TCtor)this.master.Values[metadata].Entry;
        }

        protected override IGeneralSignatureMemberUniqueIdentifier GetIdentifierFrom(int index, int metadata)
        {
            return this.identifiers[index];
        }
    }
}
