using AllenCopeland.Abstraction.Slf.Compilers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Languages.Cil
{
    public interface ICliReferenceWarning :
        ICompilerReferenceWarning
    {
        /// <summary>
        /// Returns the <see cref="CliWarningLevel"/> value indicating how 
        /// severe the warning is.
        /// </summary>
        new CliWarningLevel WarningLevel { get; }
    }
}
