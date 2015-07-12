using AllenCopeland.Abstraction.Slf.Cli.Metadata;
using AllenCopeland.Abstraction.Utilities.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Ast.Cli.Metadata
{
    public interface ICliMetadataMutableGuidHeaderAndHeap :
        ICliMetadataGuidHeaderAndHeap,
        IControlledDictionary<int, Guid>
    {
        int this[Guid guid] { get; set; }

        Guid this[uint index] { get; set; }
        bool Remove(Guid guid);
        void Remove(int index);
        int Add(Guid guid);
        Tuple<int, Guid> Add();


    }
}
