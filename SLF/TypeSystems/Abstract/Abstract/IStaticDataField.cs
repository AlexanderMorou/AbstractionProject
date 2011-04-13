using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using System.Globalization;

namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// Defines properties and methods for working with a static data
    /// field which defines immutable information within the 
    /// .sdata portion of the assembly.
    /// </summary>
    public interface IStaticDataField :
        IFieldMember<IStaticDataField, IAssembly>
    {
    }
}
