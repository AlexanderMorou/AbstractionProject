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
    internal class CliFieldMemberDictionary<TField, TFieldParent> :
        CliMetadataDrivenDictionary<IGeneralMemberUniqueIdentifier, int, TField>,
        IFieldMemberDictionary<TField, TFieldParent>,
        IFieldMemberDictionary
        where TField :
            class,
            IFieldMember<TField, TFieldParent>
        where TFieldParent :
            IFieldParent<TField, TFieldParent>
    {
        private IGeneralMemberUniqueIdentifier[] identifiers;
        private CliFullMemberDictionary master;
        private TFieldParent parent;
        internal CliFieldMemberDictionary(TFieldParent parent, CliFullMemberDictionary fullMembers)
        {
            this.parent = parent;
            this.master = fullMembers;
            var set = fullMembers.ObtainSubset<IGeneralMemberUniqueIdentifier, TField>(CliMemberType.Field).SplitSet();
            this.Initialize(set.Item1);
            this.identifiers = set.Item2;
        }

        protected override TField CreateElementFrom(int index, int metadata)
        {
            return (TField)this.master.Values[metadata].Entry;
        }

        protected override IGeneralMemberUniqueIdentifier GetIdentifierFrom(int index, int metadata)
        {
            return this.identifiers[index];
        }

        public IMasterDictionary<IGeneralMemberUniqueIdentifier, IMember> Master
        {
            get { return this.master; }
        }

        public TFieldParent Parent
        {
            get { return this.parent; }
        }

        IFieldParent IFieldMemberDictionary.Parent
        {
            get { return this.Parent; }
        }

        IMemberParent IMemberDictionary.Parent
        {
            get { return this.Parent; }
        }

        int IMemberDictionary.IndexOf(IMember member)
        {
            if (member is ICliMetadataMember)
            {
                var metadata = ((ICliMetadataMember)(member)).MetadataEntry;
                int index = this.master.GetMetadataIndex(metadata);
                if (index == -1)
                    return -1;
            }
            return -1;
        }

        public override int IndexOf(TField field)
        {
            if (field is ICliMetadataMember)
            {
                var metadata = ((ICliMetadataMember)(field)).MetadataEntry;
                int index = this.master.GetMetadataIndex(metadata);
                if (index == -1)
                    return -1;
            }
            return -1;
        }

        int IControlledCollection.IndexOf(object element)
        {
            if (element is TField)
                return this.IndexOf((TField)element);
            else if (element is IMember)
                return ((IFieldMemberDictionary)(this)).IndexOf(element);
            return -1;
        }

        IMasterDictionary ISubordinateDictionary.Master
        {
            get { return this.master; }
        }

    }
}
