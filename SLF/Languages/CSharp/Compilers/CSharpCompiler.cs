using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Cst;
using AllenCopeland.Abstraction.Slf.Languages;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Utilities.Collections;

namespace AllenCopeland.Abstraction.Slf.Compilers
{
    public class CSharpCompiler :
        IntermediateCompiler<ICSharpCompilationUnit>,
        ICSharpCompiler
    {
        private ICSharpProvider provider;
        internal CSharpCompiler (ICSharpProvider provider)
        {
            this.provider = provider;
        }

        public CSharpCompiler()
            : base()
        {
        }

        public override IHighLevelLanguageProvider<ICSharpCompilationUnit> Provider
        {
            get { return this.provider; }
        }
    }
}
