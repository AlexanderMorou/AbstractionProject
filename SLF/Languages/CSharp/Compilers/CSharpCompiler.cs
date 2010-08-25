using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Cst;
using AllenCopeland.Abstraction.Slf.Languages;
using AllenCopeland.Abstraction.Slf.Ast;

namespace AllenCopeland.Abstraction.Slf.Compilers
{
    public class CSharpCompiler :
        IntermediateCompiler<ICSharpCompilationUnit>
    {
        public CSharpCompiler()
            : base()
        {
        }

        public override IHighLevelLanguage<ICSharpCompilationUnit> Language
        {
            get { return CSharpGateway.Language; }
        }
    }
}
