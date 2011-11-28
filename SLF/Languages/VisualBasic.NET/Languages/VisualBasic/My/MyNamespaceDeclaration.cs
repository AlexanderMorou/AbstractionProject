using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Abstract;

namespace AllenCopeland.Abstraction.Slf.Languages.VisualBasic.My
{
    internal class MyNamespaceDeclaration :
        IntermediateNamespaceDeclaration,
        IMyNamespaceDeclaration
    {
        private MyNamespaceDeclaration(MyNamespaceDeclaration rootDeclaration, IMyVisualBasicAssembly parent)
            : base(rootDeclaration, parent)
        {
        }

        internal MyNamespaceDeclaration(IMyVisualBasicAssembly parent)
            : base("My", parent)
        {
        }

        protected override IntermediateNamespaceDeclaration GetNewPartial()
        {
            return new MyNamespaceDeclaration(this, (IMyVisualBasicAssembly)ObtainParentPartial());
        }

        protected override IntermediateClassTypeDictionary InitializeClasses()
        {
            var result = base.InitializeClasses();
            IIntermediateClassType myApplicationClass = new MyApplicationClass(this);
            result._Add(myApplicationClass.UniqueIdentifier, myApplicationClass);
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
