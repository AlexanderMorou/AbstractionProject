using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Compilers
{
    public interface ICompilationContext :
        IControlledDictionary<IAssemblyUniqueIdentifier, IAssemblyCompilationContext>
    {
    }
}
