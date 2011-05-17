using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Cst;
using AllenCopeland.Abstraction.Slf.Oil.VisualBasic;

namespace AllenCopeland.Abstraction.Slf.Languages
{
    /// <summary>
    /// Defines properties and methods for working with the 
    /// visual basic language.
    /// </summary>
    public interface IVisualBasicLanguage :
        IVersionedHighLevelLanguage<VisualBasicVersion, IVisualBasicStart>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <returns></returns>
        IVisualBasicAssembly CreateAssembly(string assemblyName);
    }
}
