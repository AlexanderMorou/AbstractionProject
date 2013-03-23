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
    internal class CliMethodMemberDictionary<TMethod, TMethodParent> :
        CliMetadataDrivenDictionary<IGeneralGenericSignatureMemberUniqueIdentifier, int, TMethod>,
        IMethodMemberDictionary<TMethod, TMethodParent>,
        IMethodMemberDictionary
        where TMethod :
            class,
            IMethodMember<TMethod, TMethodParent>
        where TMethodParent :
            IMethodParent<TMethod, TMethodParent>
    {
        private CliFullMemberDictionary master;
        private IGeneralGenericSignatureMemberUniqueIdentifier[] identifiers;
        private TMethodParent parent;
        internal CliMethodMemberDictionary(TMethodParent parent, CliFullMemberDictionary fullMembers)
            : base()
        {
            this.parent = parent;
            this.master = fullMembers;
            var set = fullMembers.ObtainSubset<IGeneralGenericSignatureMemberUniqueIdentifier, TMethod>(CliMemberType.Method).SplitSet();
            this.Initialize(set.Item1);
            this.identifiers = set.Item2;
        }

        protected override TMethod CreateElementFrom(int index, int metadata)
        {
            return (TMethod)this.master.Values[metadata].Entry;
        }

        protected override IGeneralGenericSignatureMemberUniqueIdentifier GetIdentifierFrom(int index, int metadata)
        {
            return this.identifiers[index];
        }

        public TMethodParent Parent
        {
            get { return this.parent; }
        }

        public IMasterDictionary<IGeneralMemberUniqueIdentifier, IMember> Master
        {
            get { return this.master; }
        }

        IMethodParent IMethodMemberDictionary.Parent
        {
            get { throw new NotImplementedException(); }
        }

        public int IndexOf(IMethodMember method)
        {
            int valueIndex = this.master.Values.IndexOf(new MasterDictionaryEntry<IMember>(this, method));
            if (valueIndex == -1)
                return -1;
            return this.identifiers.GetIndexOf(master.Keys[valueIndex]);
        }

        int IMethodSignatureMemberDictionary.IndexOf(IMethodSignatureMember method)
        {
            if (!(method is IMethodMember))
                return -1;
            return this.IndexOf((IMethodMember)method);
        }

        IMemberParent IMemberDictionary.Parent
        {
            get { return this.Parent; }
        }

        int IMemberDictionary.IndexOf(IMember member)
        {
            if (!(member is IMethodMember))
                return -1;
            return this.IndexOf((IMethodMember)member);
        }

        IMasterDictionary ISubordinateDictionary.Master
        {
            get { throw new NotImplementedException(); }
        }
    }
}
