using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace AllenCopeland.Abstraction.Slf.Languages.VisualBasic.My
{
    public class MyComputerClass :
        MyVisualBasicClass<MyComputerClass>,
        IMyComputerClass
    {
        public MyComputerClass(IIntermediateTypeParent parent)
            : base("MyComputer", parent)
        {
            this.Metadata.Add(new MetadatumDefinitionParameterValueCollection(CommonVBTypeRefs.GetCommonTypeRefs((ICliManager)this.IdentityManager).GeneratedCodeAttribute) { "MyTemplate", this.Assembly.Provider.Version.GetStringVersion() });
        }

        private MyComputerClass(MyComputerClass root, IIntermediateTypeParent parent)
            : base(root, parent)
        {
        }

        protected override MyComputerClass GetNewPartial(MyComputerClass root, Ast.IIntermediateTypeParent parent)
        {
            return new MyComputerClass(root, parent);
        }


        public IClassPropertyMember Audio
        {
            get { return this.GetProperty("Audio"); }
        }

        public IClassPropertyMember Clipboard
        {
            get { return this.GetProperty("Clipboard"); }
        }

        public IClassPropertyMember Keyboard
        {
            get { return this.GetProperty("Keyboard"); }
        }

        public IClassPropertyMember Mouse
        {
            get { return this.GetProperty("Mouse"); }
        }

        public IClassPropertyMember Ports
        {
            get { return this.GetProperty("Ports"); }
        }

        public IClassPropertyMember Screen
        {
            get { return this.GetProperty("Screen"); }
        }

        public override IClassType BaseType
        {
            get
            {
                return CommonVBTypeRefs.GetCommonTypeRefs((ICliManager)this.IdentityManager).Computer;
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        protected override void OnSetName(string value)
        {
            throw new NotSupportedException();
        }

        public new IMyNamespaceDeclaration Parent
        {
            get { return (IMyNamespaceDeclaration)base.Parent; }
        }
    }
}
