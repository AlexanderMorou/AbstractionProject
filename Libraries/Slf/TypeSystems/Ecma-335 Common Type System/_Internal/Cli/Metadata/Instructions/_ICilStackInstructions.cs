using AllenCopeland.Abstraction.Slf.Cli.Metadata.Instructions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.Instructions
{
    internal interface _ICilStackInstruction :
        ICilStackInstruction
    {
        bool NeedsAssociations { get; }
        new ICilStackInstruction Previous { get; set; }
        new ICilStackInstruction Next { get; set; }
        void AssignAssociations(Func<int, ICilStackInstruction> target);
    }
}
