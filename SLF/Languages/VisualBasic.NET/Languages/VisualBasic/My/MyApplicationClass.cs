using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Compilers;
using System.ComponentModel;
using AllenCopeland.Abstraction.Slf.Cli;

namespace AllenCopeland.Abstraction.Slf.Languages.VisualBasic.My
{
    public class MyApplicationClass :
        MyVisualBasicClass<MyApplicationClass>
    {
        public MyApplicationClass(IIntermediateTypeParent parent)
            : base("MyApplication", parent)
        {
            this.CustomAttributes.Add(new MetadatumDefinitionParameterValueCollection(CommonTypeRefs.EditorBrowsableAttribute) { (int)EditorBrowsableState.Never }, new MetadatumDefinitionParameterValueCollection(CommonVBTypeRefs.GeneratedCodeAttribute) { "MyTemplate", string.Format("{0}.0.0.0", (int)this.Assembly.Provider.Version) });
        }

        private MyApplicationClass(MyApplicationClass root, IIntermediateTypeParent parent)
            : base(root, parent)
        {
        }

        protected override MyApplicationClass GetNewPartial(MyApplicationClass root, IIntermediateTypeParent parent)
        {
            return new MyApplicationClass(root, parent);
        }
        public override IClassType BaseType
        {
            get
            {
                var assembly = this.Assembly;
                switch (assembly.CompilationContext.OutputType)
                {
                    case AssemblyOutputType.ClassLibrary:
                        return CommonVBTypeRefs.ApplicationBase;
                    case AssemblyOutputType.ConsoleApplication:
                    case AssemblyOutputType.WinFormsApplication:
                        return CommonVBTypeRefs.ConsoleApplicationBase;
                    default:
                        return base.BaseType;
                }
            }
            set
            {
                throw new NotSupportedException();
            }
        }
    }
}
