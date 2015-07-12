using AllenCopeland.Abstraction.Slf.Ast;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Languages.CSharp.Translation
{
    public class CSharpAssemblyFileInfo
    {
        public bool YieldsFile
        {
            get
            {
                return FileName != null;
            }
        }
        public string FileName { get; internal set; }
    }
    public class CSharpAssemblyFileContext
    {
        /// <summary>
        /// Returns the <see cref="CSharpAssemblyFileInfo"/> resulted
        /// from a visit operation.
        /// </summary>
        public CSharpAssemblyFileInfo CurrentResult { get; internal set; }
        public CSharpProjectTranslator Translator { get; internal set; }
        public List<string> OtherFiles { get; internal set; }

        public IIntermediateAssembly RootAssembly { get; internal set; }
    }
}
