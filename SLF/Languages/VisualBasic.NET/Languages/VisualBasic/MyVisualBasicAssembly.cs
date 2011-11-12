using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil;
using AllenCopeland.Abstraction.Slf.Languages.VisualBasic.My;

namespace AllenCopeland.Abstraction.Slf.Languages.VisualBasic
{
    public class MyVisualBasicAssembly :
        VisualBasicAssembly<IMyVisualBasicAssembly, IMyVisualBasicProvider, MyVisualBasicAssembly>,
        IMyVisualBasicAssembly
    {
        protected override IntermediateNamespaceDictionary InitializeIntermediateNamespaces()
        {
            var results = base.InitializeIntermediateNamespaces();
            if (this.IsRoot)
                results.Add(new MyNamespaceDeclaration(this));
            return results;
        }
        #region IMyVisualBasicAssembly Members

        public IMyNamespaceDeclaration MyNamespace
        {
            get { return (IMyNamespaceDeclaration)this.Namespaces["My"]; }
        }

        #endregion

        public MyVisualBasicAssembly(string name)
            : base(name, VisualBasicLanguage.Singleton.GetMyProvider(VisualBasicLanguage.DefaultVersion))
        {
        }

        public MyVisualBasicAssembly(MyVisualBasicAssembly root)
            : base(root)
        {
        }

        protected override MyVisualBasicAssembly GetNewPart()
        {
            return new MyVisualBasicAssembly(this);
        }
    }
}
