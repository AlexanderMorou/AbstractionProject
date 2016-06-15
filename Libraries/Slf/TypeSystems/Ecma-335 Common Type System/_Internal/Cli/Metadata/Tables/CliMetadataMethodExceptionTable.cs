using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.Tables
{
    internal class CliMetadataMethodExceptionTable :
        CliMetadataMethodHeaderSection,
        ICliMetadataMethodExceptionTable
    {
        internal CliMetadataMethodExceptionTable(byte[] data)
            : base(MethodHeaderSectionFlags.ExceptionHandlerTable) { }
    }
}
