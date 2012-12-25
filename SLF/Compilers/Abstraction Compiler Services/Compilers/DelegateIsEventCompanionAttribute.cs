using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Compilers
{
    [AttributeUsage(AttributeTargets.Delegate)]
    public class DelegateIsEventCompanionAttribute :
        Attribute
    {
    }
}
