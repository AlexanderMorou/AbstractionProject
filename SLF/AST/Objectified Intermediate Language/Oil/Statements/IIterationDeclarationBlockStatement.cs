using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Oil.Statements
{
    public interface IIterationDeclarationBlockStatement :
        IIterationBlockBaseStatement
    {
        /// <summary>
        /// Returns/sets the <see cref="ILocalDeclarationStatement"/> which executes once at the initialization
        /// of the iteration process.
        /// </summary>
        ILocalDeclarationStatement LocalDeclaration { get; set; }
    }
}
