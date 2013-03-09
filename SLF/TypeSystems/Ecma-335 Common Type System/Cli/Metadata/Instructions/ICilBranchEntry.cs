using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Cli.Metadata.Instructions
{
    internal interface ICilBranchEntry :
        ICilStackEntry
    {
        ICilStackEntry Target { get; }
        int TargetOffset { get; }
    }
}
