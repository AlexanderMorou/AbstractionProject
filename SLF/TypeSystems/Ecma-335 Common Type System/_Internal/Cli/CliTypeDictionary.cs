using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.Blobs;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    internal partial class CliTypeDictionary :
        MasterDictionaryBase<IGeneralTypeUniqueIdentifier, IType>,
        IFullTypeDictionary
    {
        private _ICliTypeParent owningParent;
        private IReadOnlyCollection<ICliMetadataTypeDefinitionTableRow> typeDefinitions;
        private IGeneralTypeUniqueIdentifier[] uniqueIdentifiers;
        private ICliType[] typeInstances;
        internal CliTypeDictionary(_ICliTypeParent owningParent)
            : base()
        {
            this.owningParent = owningParent;
            this.typeDefinitions = this.owningParent._Types;
            if (this.typeDefinitions == null)
                this.typeDefinitions = ArrayReadOnlyCollection<ICliMetadataTypeDefinitionTableRow>.Empty;
            this.uniqueIdentifiers = new IGeneralTypeUniqueIdentifier[this.typeDefinitions.Count];
            this.typeInstances = new ICliType[this.typeDefinitions.Count];
        }
        protected override ControlledDictionary<IGeneralTypeUniqueIdentifier, MasterDictionaryEntry<IType>>.KeysCollection InitializeKeysCollection()
        {
            return new KeysCollection(this);
        }

        protected override ControlledDictionary<IGeneralTypeUniqueIdentifier, MasterDictionaryEntry<IType>>.ValuesCollection InitializeValuesCollection()
        {
            return new ValuesCollection(this);
        }

        public override int Count
        {
            get
            {
                return this.typeInstances.Length;
            }
        }

        public override bool ContainsKey(IGeneralTypeUniqueIdentifier key)
        {
            for (int i = 0; i < this.uniqueIdentifiers.Length; i++)
            {
                this.CheckKeyAt(i);
                if (this.uniqueIdentifiers[i].Equals(key))
                    return true;
            }
            return false;
        }

        private void CheckKeyAt(int i)
        {

            if (uniqueIdentifiers[i] == null)
                uniqueIdentifiers[i] = CliCommon.GetUniqueIdentifier(typeDefinitions[i], this.owningParent.Assembly, this.owningParent.Manager);
        }

        #region IFullTypeDictionary Members

        public IType[] GetTypesByName(string name)
        {
            var kEnum = this.Keys.GetEnumerator();
            List<IType> result = new List<IType>();
            for (int i = 0; kEnum.MoveNext(); i++)
                if (kEnum.Current.Name == name)
                    result.Add(this.Values[i].Entry);
            return result.ToArray();
        }

        #endregion

    }
}
