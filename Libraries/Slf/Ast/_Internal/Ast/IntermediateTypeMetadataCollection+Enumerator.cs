//using AllenCopeland.Abstraction.Slf._Internal.Cli;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Utilities.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf._Internal.Ast
{
    partial class IntermediateTypeMetadataCollection
    {
        private class Enumerator :
            IEnumerable<IMetadatum>
        {
            private IIntermediateType currentType;
            private bool parentChanged = false;
            private object syncObject = new object();
            private List<IIntermediateType> listenedTypes = new List<IIntermediateType>();

            public Enumerator(IIntermediateType parent)
            {
                this.currentType = parent;
            }

            public IEnumerator<IMetadatum> GetEnumerator()
            {
                Dictionary<IType, MetadatumInfo> usageInformation = new Dictionary<IType, MetadatumInfo>();
                List<IType> singleUseMetadata = new List<IType>();
                List<IIntermediateType> listenedTypes = new List<IIntermediateType>();
                if (currentType == null)
                    yield break;
                try
                {
                    while (currentType != null)
                    {
                        currentType.BaseTypeChanged += currentType_BaseTypeChanged;
                        lock (syncObject)
                            listenedTypes.Add(currentType);
                        ChangeCheck();
                        var metadataSet = currentType.Metadata;
                        foreach (var metadata in metadataSet)
                        {
                            ChangeCheck();
                            bool first = true;
                            foreach (var metadatum in metadata)
                            {
                                if (first)
                                    first = false;
                                else
                                    ChangeCheck();
                                MetadatumInfo usage;
                                if (!usageInformation.TryGetValue(metadatum.Type, out usage))
                                    usageInformation.Add(metadatum.Type, usage = currentType.IdentityManager.MetadatumHandler.GetMetadatumInfo(metadatum.Type));
                                if (usage.MetadatumRepresentation == TypeMetadatumRepresentation.IsNotMetadata)
                                    continue;
                                if (!usage.Repeatable)
                                {
                                    if (singleUseMetadata.Contains(metadatum.Type))
                                        continue;
                                    singleUseMetadata.Add(metadatum.Type);
                                }
                                yield return metadatum;
                            }
                        }
                        var nextType = currentType.BaseType;
                        currentType = nextType as IIntermediateType;
                    }
                }
                finally
                {
                    Unlisten();
                }
            }

            private void Unlisten()
            {
                foreach (var currentType in listenedTypes)
                    currentType.BaseTypeChanged -= currentType_BaseTypeChanged;
            }

            private void ChangeCheck()
            {
                lock (syncObject)
                    if (parentChanged)
                        throw new InvalidOperationException("Object hierarchy changed, metadatum enumeration is not possible.");
            }

            void currentType_BaseTypeChanged(object sender, EventArgs<IType, IType> e)
            {
                lock (syncObject)
                    this.parentChanged = true;
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }
        }
    }
}
