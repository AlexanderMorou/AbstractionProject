using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Languages.Cil
{
    public enum CliWarningLevel
    {
        /// <summary>
        /// Warnings of this type relate to the infrastructure itself, items receiving this
        /// are valid, but aren't recommended.
        /// </summary>
        CliWarning = 0,
        /// <summary>
        /// Warnings of this type relate to the common language specification, when a warning of this type
        /// is received, it means that the metadata is valid by the infrastructure; however, other languages
        /// may not necessarily be able to consume the metadata effectively.
        /// </summary>
        ClsWarning = 1,
    }
}
