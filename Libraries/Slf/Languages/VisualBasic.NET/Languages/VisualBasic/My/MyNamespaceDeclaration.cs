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
            IIntermediateClassType myComputerClass = new MyComputerClass(this);
            IIntermediateClassType myProjectClass = new MyProjectClass(this);
            result._Add(myApplicationClass.UniqueIdentifier, myApplicationClass);
            result._Add(myComputerClass.UniqueIdentifier, myComputerClass);
            result._Add(myProjectClass.UniqueIdentifier, myProjectClass);
            return result;
        }

        protected override void OnSetName(string value)
        {
            throw new NotSupportedException();
        }

        #region IMyNamespaceDeclaration Members

        public IMyApplicationClass MyApplication
        {
            get {
                return (IMyApplicationClass)this.Classes[this.Assembly.UniqueIdentifier.GetTypeIdentifier("My", "MyApplication", 0)];
            }
        }

        #endregion



        public IMyComputerClass MyComputer
        {
            get
            {
                return (IMyComputerClass)this.Classes[this.Assembly.UniqueIdentifier.GetTypeIdentifier("My", "MyComputer", 0)];
            }
        }

        public IMyProjectClass MyProject
        {
            get {
                return (IMyProjectClass)this.Classes[this.Assembly.UniqueIdentifier.GetTypeIdentifier("My", "MyProject", 0)];
            }
        }
    }
}
