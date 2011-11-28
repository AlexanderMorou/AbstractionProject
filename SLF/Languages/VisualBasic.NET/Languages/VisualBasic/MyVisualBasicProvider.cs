using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Ast;

namespace AllenCopeland.Abstraction.Slf.Languages.VisualBasic
{
    internal class MyVisualBasicProvider :
        VisualBasicProvider<IMyVisualBasicAssembly, IMyVisualBasicProvider>,
        IMyVisualBasicProvider
    {
        public override IMyVisualBasicAssembly CreateAssembly(string name)
        {
            return new MyVisualBasicAssembly(name);
        }

        internal MyVisualBasicProvider(VisualBasicVersion version)
            : base(version)
        {
        }

        #region IIntermediateLanguageTypeProvider Members

        public override IIntermediateClassType CreateClass(string name, IIntermediateTypeParent parent)
        {
            return new MyVisualBasicClass(name, parent);
        }

        #endregion
    }
}
