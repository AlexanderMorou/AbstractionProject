using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Languages.VisualBasic.My
{
    public class MyProjectClass :
        MyVisualBasicClass<MyProjectClass>,
        IMyProjectClass
    {
        public MyProjectClass(IIntermediateTypeParent parent)
            : base("MyProject", parent)
        {
            var commonTypeRefs = CommonVBTypeRefs.GetCommonTypeRefs((ICliManager)this.IdentityManager);
            this.Metadata.Add(new MetadatumDefinitionParameterValueCollection(commonTypeRefs.GeneratedCodeAttribute) { "MyTemplate", this.Assembly.Provider.Version.GetStringVersion() },
                              new MetadatumDefinitionParameterValueCollection(commonTypeRefs.HideModuleNameAttribute),
                              new MetadatumDefinitionParameterValueCollection(commonTypeRefs.StandardModuleAttribute),
                              new MetadatumDefinitionParameterValueCollection(commonTypeRefs.EditorBrowsableAttribute) { commonTypeRefs.EditorBrowsableState.Fields[TypeSystemIdentifiers.GetMemberIdentifier("Never")].GetReference() });
            this.AccessLevel = AccessLevelModifiers.Internal;
            base.SpecialModifier = SpecialClassModifier.Static | SpecialClassModifier.HiddenModule;

        }

        private MyProjectClass(MyProjectClass root, IIntermediateTypeParent parent)
            : base(root, parent)
        {

        }

        protected override IntermediateClassTypeDictionary InitializeClasses()
        {
            var baseInit = base.InitializeClasses();

            return baseInit;
        }

        protected override PropertyDictionary InitializeProperties()
        {
            var baseInit = base.InitializeProperties();
            AddStatic(baseInit, "Application", ((IMyNamespaceDeclaration)(this.Parent)).MyApplication);
            AddStatic(baseInit, "Computer", ((IMyNamespaceDeclaration)(this.Parent)).MyComputer);
            AddStatic(baseInit, "WebServices", this.MyWebServices);
            return baseInit;
        }

        private void AddStatic(PropertyDictionary propertyDictionary, string name, IType type)
        {
            IntermediateClassFieldMember<MyProjectClass> staticFieldMember = (IntermediateClassFieldMember<MyProjectClass>)this.Fields.Add(new TypedName("m_{0}", type, name));
            staticFieldMember.IsStatic = true;
            staticFieldMember.AccessLevel = AccessLevelModifiers.Private;
            staticFieldMember.IsNameLocked = true;

            PropertyMember staticPropertyMember = (PropertyMember)propertyDictionary.Add(new TypedName(name, type), true, false);
            staticPropertyMember.IsStatic = true;
            staticPropertyMember.AccessLevel = AccessLevelModifiers.Public;
            staticPropertyMember.IsNameLocked = true;
            /* *
             * private static ThreadSafeObjectProvider<{type}> m_{name};
             * 
             * public static {type} {name}
             * {
             *     get
             *     {
             *         return this.m_{name}.GetInstance;
             *     }
             * }
             * */
            staticPropertyMember.GetMethod.Return(staticFieldMember.GetReference());
        }

        protected override MyProjectClass GetNewPartial(MyProjectClass root, IIntermediateTypeParent parent)
        {
            return new MyProjectClass(root, parent);
        }

        public IIntermediatePropertyMember Application
        {
            get { throw new NotImplementedException(); }
        }

        public IIntermediatePropertyMember Computer
        {
            get { throw new NotImplementedException(); }
        }

        public IIntermediatePropertyMember WebServices
        {
            get { throw new NotImplementedException(); }
        }

        public IMyWebservicesClass MyWebServices
        {
            get { throw new NotImplementedException(); }
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
