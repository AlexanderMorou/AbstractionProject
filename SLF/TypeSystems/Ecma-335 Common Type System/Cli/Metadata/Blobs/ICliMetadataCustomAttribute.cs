using AllenCopeland.Abstraction.Utilities.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs
{
    public interface ICliMetadataCustomAttribute
    {
        IControlledCollection<ICliMetadataCustomAttributeParameter> FixedParameters { get; }
        IControlledCollection<ICliMetadataCustomAttributeNamedParameter> NamedParameters { get; }
    }
}
