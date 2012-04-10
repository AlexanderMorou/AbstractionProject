using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Cli.Metadata.Members
{
    public enum MethodSectionFlags :
        byte
    {
        ExceptionHandlerTable = 0x1,
        OptIL
    }
}
