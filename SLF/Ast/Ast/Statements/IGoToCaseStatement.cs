using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Ast.Statements
{
    public interface IGoToCaseStatement :
        IJumpStatement
    {
        /// <summary>
        /// Returns/sets the <see cref="ISwitchCaseBlockStatement"/>
        /// of the <see cref="IGoToCaseStatement"/>.
        /// </summary>
        new ISwitchCaseBlockStatement Target { get; set; }
    }
}
