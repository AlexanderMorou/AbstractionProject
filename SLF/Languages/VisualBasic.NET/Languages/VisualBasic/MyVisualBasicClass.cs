using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil;
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
                if (base.SpecialModifier != SpecialClassModifier.None)
                {
                    if (this.IsDefined(CommonVBTypeRefs.StandardModuleAttribute, false))
                        if (this.IsDefined(CommonTypeRefs.ExtensionAttribute, false))
                            return SpecialClassModifier.TypeExtensionSource;
                        else if (this.IsDefined(CommonVBTypeRefs.HideModuleNameAttribute, false))
                            return SpecialClassModifier.HiddenModule;
                        else
                            return SpecialClassModifier.Module;
                    else if (this.IsDefined(CommonTypeRefs.ExtensionAttribute, false))
                        return SpecialClassModifier.TypeExtensionSource;
                    else
                        return SpecialClassModifier.Static;
                }
                else
                    return SpecialClassModifier.None;
            }
            set
            {
                if (!this.IsRoot)
                {
                    this.GetRoot().SpecialModifier = value;
                    return;
                }
                const int NONE = 0;
                const int STATIC = 1;
                const int MODULE = 2;
                const int EXTENSION = 4;
                const int HIDDENMODULE = 10;
                if (value == SpecialModifier)
                    return;
                if ((value == SpecialClassModifier.Static) ||
                    (value == SpecialClassModifier.None))
                {
                    int current = NONE;
                    if (this.IsDefined(CommonVBTypeRefs.StandardModuleAttribute, false))
                        if (this.IsDefined(CommonTypeRefs.ExtensionAttribute, false))
                            if (this.IsDefined(CommonVBTypeRefs.HideModuleNameAttribute, false))
                                current = EXTENSION | HIDDENMODULE;
                            else
                                current = EXTENSION | MODULE;
                        else if (this.IsDefined(CommonVBTypeRefs.HideModuleNameAttribute, false))
                            current = HIDDENMODULE;
                        else
                            current = MODULE;
                    else if (this.IsDefined(CommonTypeRefs.ExtensionAttribute, false))
                        current = EXTENSION;
                    else
                        current = STATIC;
                    List<ICustomAttributeDefinition> toRemove = new List<ICustomAttributeDefinition>();
                    if ((current & EXTENSION) == EXTENSION)
                        toRemove.Add(this.CustomAttributes[CommonTypeRefs.ExtensionAttribute]);
                    if ((current & MODULE) == MODULE)
                        toRemove.Add(this.CustomAttributes[CommonVBTypeRefs.StandardModuleAttribute]);
                    if ((current & HIDDENMODULE) == HIDDENMODULE)
                        toRemove.Add(this.CustomAttributes[CommonVBTypeRefs.HideModuleNameAttribute]);
                    if (toRemove.Count > 0)
                        this.CustomAttributes.RemoveSet(toRemove.ToArray());
                }
                else if (value == SpecialClassModifier.HiddenModule)
                {
                    List<CustomAttributeDefinition.ParameterValueCollection> toAdd = new List<CustomAttributeDefinition.ParameterValueCollection>();

                    if (!this.IsDefined(CommonVBTypeRefs.StandardModuleAttribute, false))
                        toAdd.Add(new CustomAttributeDefinition.ParameterValueCollection(CommonVBTypeRefs.StandardModuleAttribute));
                    if (!this.IsDefined(CommonVBTypeRefs.HideModuleNameAttribute, false))
                        toAdd.Add(new CustomAttributeDefinition.ParameterValueCollection(CommonVBTypeRefs.HideModuleNameAttribute));

                    /* *
                     * This ensures that both attributes are added in unison.
                     * */
                    this.CustomAttributes.Add(toAdd.ToArray());

                    if (this.IsDefined(CommonTypeRefs.ExtensionAttribute, false))
                        this.CustomAttributes.Remove(this.CustomAttributes[CommonTypeRefs.ExtensionAttribute]);
                }
                else if (value == SpecialClassModifier.Module)
                {
                    List<ICustomAttributeDefinition> toRemove = new List<ICustomAttributeDefinition>();
                    if (!this.IsDefined(CommonVBTypeRefs.StandardModuleAttribute, false))
                        this.CustomAttributes.Add(new CustomAttributeDefinition.ParameterValueCollection(CommonVBTypeRefs.StandardModuleAttribute));
                    if (this.IsDefined(CommonTypeRefs.ExtensionAttribute, false))
                        toRemove.Add(this.CustomAttributes[CommonTypeRefs.ExtensionAttribute]);
                    if (this.IsDefined(CommonVBTypeRefs.HideModuleNameAttribute, false))
                        toRemove.Add(this.CustomAttributes[CommonVBTypeRefs.HideModuleNameAttribute]);
                    if (toRemove.Count > 0)
                        this.CustomAttributes.RemoveSet(toRemove.ToArray());
                }
                else if (value == SpecialClassModifier.TypeExtensionSource)
                    if (!this.IsDefined(CommonTypeRefs.ExtensionAttribute, false))
                        this.CustomAttributes.Add(new CustomAttributeDefinition.ParameterValueCollection(CommonTypeRefs.ExtensionAttribute));
                base.SpecialModifier = value;
            }
        }
    }
}
