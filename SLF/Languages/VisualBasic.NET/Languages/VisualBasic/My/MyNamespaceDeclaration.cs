using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil;
using AllenCopeland.Abstraction.Slf.Abstract;

namespace AllenCopeland.Abstraction.Slf.Languages.VisualBasic.My
{
    internal class MyNamespaceDeclaration :
        IntermediateNamespaceDeclaration,
        IMyNamespaceDeclaration
    {
        private MyNamespaceDeclaration(MyNamespaceDeclaration rootDeclaration, IVisualBasicAssembly parent)
            : base(rootDeclaration, parent)
        {
        }

        internal MyNamespaceDeclaration(IVisualBasicAssembly parent)
            : base("My", parent)
        {
        }

        protected override IntermediateNamespaceDeclaration GetNewPartial()
        {
            return new MyNamespaceDeclaration(this, (IVisualBasicAssembly)ObtainParentPartial());
        }

        protected override IntermediateClassTypeDictionary InitializeClasses()
        {
            var result = base.InitializeClasses();
            //ToDo: Add code here to initialize base classes of the my namespace.
            return result;
        }

        #region IMyNamespaceDeclaration Members

        public IMyApplicationClass MyApplication
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

    }
}
