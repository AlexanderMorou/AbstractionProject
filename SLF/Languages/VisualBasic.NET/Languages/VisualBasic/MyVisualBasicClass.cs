using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli;

namespace AllenCopeland.Abstraction.Slf.Languages.VisualBasic
{
    /// <summary>
    /// Provides a standard visual basic class which applies the proper
    /// attributes to the class when the <see cref="MyVisualBasicClass{TInstClass}.SpecialModifier"/>
    /// is <see cref="SpecialClassModifier.TypeExtensionSource"/>,
    /// <paramref name="SpecialClassModifier.HiddenModule"/>.
    /// </summary>
    public class MyVisualBasicClass :
        MyVisualBasicClass<MyVisualBasicClass>
    {
        public MyVisualBasicClass(string name, IIntermediateTypeParent parent)
            : base(name, parent)
        {
        }

        private MyVisualBasicClass(MyVisualBasicClass root, IIntermediateTypeParent parent)
            : base(root, parent)
        {
        }

        protected override MyVisualBasicClass GetNewPartial(MyVisualBasicClass root, IIntermediateTypeParent parent)
        {
            return new MyVisualBasicClass(root, parent);
        }
    }
    public abstract class MyVisualBasicClass<TInstClass> :
        IntermediateClassType<TInstClass>
        where TInstClass :
            MyVisualBasicClass<TInstClass>
    {

        public MyVisualBasicClass(string name, IIntermediateTypeParent parent)
            : base(name, parent)
        {
        }

        protected MyVisualBasicClass(TInstClass root, IIntermediateTypeParent parent)
            : base(root, parent)
        {
        }

        public new IMyVisualBasicAssembly Assembly { get { return (IMyVisualBasicAssembly)base.Assembly; } }

        public override SpecialClassModifier SpecialModifier
        {
            get
            {
                if (!this.IsRoot)
                    return this.GetRoot().SpecialModifier;
                return base.SpecialModifier;
            }
            set
            {
                if (!this.IsRoot)
                {
                    this.GetRoot().SpecialModifier = value;
                    return;
                }
                bool isAbstract = (value & SpecialClassModifier.Abstract) == SpecialClassModifier.Abstract;
                bool isSealed = (value & SpecialClassModifier.Sealed) == SpecialClassModifier.Sealed;
                bool isModule = (value & SpecialClassModifier.Module) == SpecialClassModifier.Module;
                bool isHidden = (value & SpecialClassModifier.Hidden) == SpecialClassModifier.Hidden;
                bool isExtensionSource = (value & SpecialClassModifier.TypeExtensionSource) == SpecialClassModifier.TypeExtensionSource;
                if (isExtensionSource && isModule)
                {
                    value = (value & ~SpecialClassModifier.TypeExtensionSource) | SpecialClassModifier.Module;
                    isExtensionSource = false;
                }
                IList<ICustomAttributeDefinition> toRemove = new List<ICustomAttributeDefinition>();
                IList<CustomAttributeDefinition.ParameterValueCollection> toAdd = new List<CustomAttributeDefinition.ParameterValueCollection>();
                if (isModule)
                {
                    if (!this.IsDefined(CommonVBTypeRefs.StandardModuleAttribute))
                        toAdd.Add(new CustomAttributeDefinition.ParameterValueCollection(CommonVBTypeRefs.StandardModuleAttribute));
                    if (isHidden && !this.IsDefined(CommonVBTypeRefs.HideModuleNameAttribute))
                        toAdd.Add(new CustomAttributeDefinition.ParameterValueCollection(CommonVBTypeRefs.HideModuleNameAttribute));
                    else if (!isHidden && this.IsDefined(CommonVBTypeRefs.HideModuleNameAttribute))
                        toRemove.Add(this.CustomAttributes[CommonVBTypeRefs.HideModuleNameAttribute]);
                }
                else if (this.IsDefined(CommonVBTypeRefs.StandardModuleAttribute))
                {
                    toRemove.Add(this.CustomAttributes[CommonVBTypeRefs.StandardModuleAttribute]);
                    if (this.IsDefined(CommonVBTypeRefs.HideModuleNameAttribute))
                        toRemove.Add(this.CustomAttributes[CommonVBTypeRefs.HideModuleNameAttribute]);
                }
                else if (this.IsDefined(CommonVBTypeRefs.HideModuleNameAttribute))
                    toRemove.Add(this.CustomAttributes[CommonVBTypeRefs.HideModuleNameAttribute]);
                if (isHidden && !isModule)
                {
                    if (!this.IsDefined(CommonTypeRefs.ClassIsHiddenAttribute))
                        toAdd.Add(new CustomAttributeDefinition.ParameterValueCollection(CommonTypeRefs.ClassIsHiddenAttribute));
                }
                else if (!(isHidden || isModule))
                {
                    if (!isHidden && this.IsDefined(CommonTypeRefs.ClassIsHiddenAttribute))
                        toRemove.Add(this.CustomAttributes[CommonTypeRefs.ClassIsHiddenAttribute]);
                }
                if (isExtensionSource)
                {
                    if (!this.IsDefined(CommonTypeRefs.ExtensionAttribute))
                        toAdd.Add(new CustomAttributeDefinition.ParameterValueCollection(CommonTypeRefs.ExtensionAttribute));
                }
                else if (this.IsDefined(CommonTypeRefs.ExtensionAttribute))
                    toRemove.Add(this.CustomAttributes[CommonTypeRefs.ExtensionAttribute]);
                if (toRemove.Count > 0)
                    this.CustomAttributes.RemoveSet(toRemove.ToArray());
                if (toAdd.Count > 0)
                    this.CustomAttributes.Add(toAdd.ToArray());
            }
        }
    }
}
