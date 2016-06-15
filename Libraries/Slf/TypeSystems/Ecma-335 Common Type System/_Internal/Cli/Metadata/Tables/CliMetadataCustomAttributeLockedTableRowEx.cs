using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.Tables
{
    partial class CliMetadataCustomAttributeLockedTableRow
    {
        #region ICliMetadataCustomAttributeTableRow Members

        public ICliMetadataCustomAttribute GetValue(ICliManager manager)
        {
            return this.MetadataRoot.BlobHeap.GetCustomAttribute(manager, this);
        }

        #endregion
    }
}
