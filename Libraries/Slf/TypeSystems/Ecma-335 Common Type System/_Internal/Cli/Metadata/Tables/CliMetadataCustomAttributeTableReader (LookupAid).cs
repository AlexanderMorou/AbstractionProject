using AllenCopeland.Abstraction.Slf.Cli.Metadata;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Utilities.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.Tables
{
    partial class CliMetadataCustomAttributeTableReader
    {
        private ControlledDictionary<CliMetadataHasCustomAttributeTag, TaggedEntryCollection> lookupTable = new ControlledDictionary<CliMetadataHasCustomAttributeTag,TaggedEntryCollection>();
        public IControlledCollection<ICliMetadataCustomAttributeTableRow> GetTaggedEntries(CliMetadataHasCustomAttributeTag hasCustomAttrTag)
        {
            lock (this.syncObject)
            {
                TaggedEntryCollection result;
                if (!lookupTable.TryGetValue(hasCustomAttrTag, out result))
                    lookupTable._Add(hasCustomAttrTag, result = new TaggedEntryCollection(this, hasCustomAttrTag));
                return result;
            }
        }
    }
}
