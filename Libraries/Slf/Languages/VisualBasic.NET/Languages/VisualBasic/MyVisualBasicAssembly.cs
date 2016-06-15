using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Languages.VisualBasic.My;
using AllenCopeland.Abstraction.Utilities.Properties;
using AllenCopeland.Abstraction.Slf.Ast.Cli;

namespace AllenCopeland.Abstraction.Slf.Languages.VisualBasic
{
    public class MyVisualBasicAssembly :
        VisualBasicAssembly<IMyVisualBasicAssembly, IMyVisualBasicProvider, MyVisualBasicAssembly>,
        IMyVisualBasicAssembly
    {
        protected override IIntermediateNamespaceDictionary InitializeIntermediateNamespaces()
        {
            var results = base.InitializeIntermediateNamespaces();
            if (this.IsRoot)
                results.Add(new MyNamespaceDeclaration(this));
            return results;
        }
        #region IMyVisualBasicAssembly Members

        public IMyNamespaceDeclaration MyNamespace
        {
            get
            {
                if (this.IsDisposed)
                    throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
                return (IMyNamespaceDeclaration)this.Namespaces["My"];
            }
        }

        #endregion

        public MyVisualBasicAssembly(string name, IIntermediateCliRuntimeEnvironmentInfo runtimeEnvironment)
            : base(name, VisualBasicLanguage.Singleton.GetMyProvider(VisualBasicLanguage.DefaultVersion), runtimeEnvironment)
        {
        }

        public MyVisualBasicAssembly(string name, IMyVisualBasicProvider provider, IIntermediateCliRuntimeEnvironmentInfo runtimeEnvironment)
            : base(name, provider, runtimeEnvironment)
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
