using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Compilers;
using System.ComponentModel;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Abstract.Members;

namespace AllenCopeland.Abstraction.Slf.Languages.VisualBasic.My
{
    public class MyApplicationClass :
        MyVisualBasicClass<MyApplicationClass>,
        IMyApplicationClass
    {
        public MyApplicationClass(IIntermediateTypeParent parent)
            : base("MyApplication", parent)
        {
            this.Metadata.Add(new MetadatumDefinitionParameterValueCollection(CommonVBTypeRefs.GetCommonTypeRefs((ICliManager)this.IdentityManager).GeneratedCodeAttribute) { "MyTemplate", this.Assembly.Provider.Version.GetStringVersion() });
            this.IsNameLocked = true;
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
                        return CommonVBTypeRefs.GetCommonTypeRefs((ICliManager)this.IdentityManager).ApplicationBase;
                    case AssemblyOutputType.ConsoleApplication:
                    case AssemblyOutputType.UserInterfaceApplication:
                        return CommonVBTypeRefs.GetCommonTypeRefs((ICliManager)this.IdentityManager).ConsoleApplicationBase;
                    default:
                        return base.BaseType;
                }
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        public new IMyNamespaceDeclaration Parent
        {
            get { return ((IMyNamespaceDeclaration)(base.Parent)); }
        }


        /// <summary>
        /// Returns the <see cref="IClassPropertyMember"/> which denotes the culture
        /// of the running thread.
        /// </summary>
        public IClassPropertyMember Culture
        {
            get
            {
                return this.GetProperty("Culture");
            }
        }

        /// <summary>
        /// Returns the <see cref="IClassPropertyMember"/> which denotes the entrypoint assembly's 
        /// information.
        /// </summary>
        public IClassPropertyMember Info
        {
            get
            {
                return this.GetProperty("Info");
            }
        }

        /// <summary>
        /// Returns the <see cref="IClassPropertyMember"/> which denotes the Log
        /// instance for common logging functionality.
        /// </summary>
        public IClassPropertyMember Log
        {
            get
            {
                return this.GetProperty("Log");
            }
        }

        /// <summary>
        /// Returns the <see cref="IClassPropertyMember"/> which denotes the user-interface
        /// culture of the running thread.
        /// </summary>
        public IClassPropertyMember UICulture
        {
            get
            {
                return this.GetProperty("UICulture");
            }
        }
    }
}
