using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Cli.Metadata.Instructions
{
    public interface ICilReferenceInstruction :
        ICilStackInstruction
    {
        /// <summary>Returns the <see cref="ICliMetadataTableRow"/> which is referenced by the <see cref="ICliReferenceInstruction"/>.</summary>
        ICliMetadataTableRow Reference { get; }
    }
}
