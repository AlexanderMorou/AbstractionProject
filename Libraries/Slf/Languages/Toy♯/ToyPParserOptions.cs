using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Compilers;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Ast.Cli;
using AllenCopeland.Abstraction.Slf.Parsers;
using AllenCopeland.Abstraction.Utilities.Collections;

namespace AllenCopeland.Abstraction.Slf.Languages.ToySharp
{
    public class ToySharparserOptions :
        IIntermediateParserOptions
    {
        public IControlledCollection<INamedStreamInfo> Files
        {
            get { throw new NotImplementedException(); }
        }
    }
}
