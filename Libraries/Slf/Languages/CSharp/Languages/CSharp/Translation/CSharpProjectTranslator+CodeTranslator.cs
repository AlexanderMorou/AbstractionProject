using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Translation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Languages.CSharp.Translation
{
    partial class CSharpProjectTranslator
    {
        private class CodeTranslator :
            CSharpCodeTranslator
        {
            private Dictionary<IIntermediateAssembly, string> fileNameLookup;
            private CSharpProjectTranslator owner;
            internal CodeTranslator(CSharpProjectTranslator owner, Dictionary<IIntermediateAssembly, string> fileNameLookup)
                : base(owner.nameProvider)
            {
                this.fileNameLookup = fileNameLookup;
                this.owner = owner;
            }

            protected override IIntermediateCodeTranslatorOptions InitializeOptions()
            {
                return this.owner.options;
            }

            protected override IIntermediateCodeNameProvider InitializeNameProvider()
            {
                return this.owner.nameProvider;
            }
        }
    }
}
