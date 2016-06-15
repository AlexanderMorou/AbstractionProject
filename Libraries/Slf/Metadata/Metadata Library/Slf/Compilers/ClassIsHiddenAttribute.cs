using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Compilers
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public class ClassIsHiddenAttribute :
        Attribute
    {
    }
}
