using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Ast.Cli;

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

        private new IIntermediateCliManager IdentityManager { get { return (IIntermediateCliManager)base.IdentityManager; } }

        internal CommonVBTypeRefs CommonVBTypeRefs { get { return VisualBasic.CommonVBTypeRefs.GetCommonTypeRefs(this.IdentityManager); } }

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
                var original = this.SpecialModifier;
                if (original != value)
                {
                    bool hidden = (value & SpecialClassModifier.HiddenModule) == SpecialClassModifier.HiddenModule;
                    bool extensionTarget = (value & SpecialClassModifier.TypeExtensionSource) == SpecialClassModifier.TypeExtensionSource;
                    bool wasHidden = (original & SpecialClassModifier.HiddenModule) == SpecialClassModifier.HiddenModule;
                    bool wasExtensionTarget = (original & SpecialClassModifier.TypeExtensionSource) == SpecialClassModifier.TypeExtensionSource;
                    List<IType> customAttributesToAdd = null;
                    List<IType> customAttributesToRemove = null;
                    if (hidden && !wasHidden)
                    {
                        if (!this.IsDefined(this.CommonVBTypeRefs.HideModuleNameAttribute))
                        {
                            customAttributesToAdd = new List<IType>();
                            customAttributesToAdd.Add(this.CommonVBTypeRefs.HideModuleNameAttribute);
                        }
                    }
                    else if (!hidden && wasHidden)
                    {
                        if (this.IsDefined(this.CommonVBTypeRefs.HideModuleNameAttribute))
                        {
                            customAttributesToRemove = new List<IType>();
                            customAttributesToRemove.Add(this.CommonVBTypeRefs.HideModuleNameAttribute);
                        }
                    }
                    if (extensionTarget && !wasExtensionTarget)
                    {
                        var extAttr = this.CommonVBTypeRefs.ExtensionAttribute;
                        if (extAttr != null && !this.IsDefined(extAttr))
                        {
                            customAttributesToAdd = new List<IType>();
                            customAttributesToAdd.Add(extAttr);
                        }
                    }
                    else if (!extensionTarget && wasExtensionTarget)
                    {
                        if (this.IsDefined(this.CommonVBTypeRefs.ExtensionAttribute))
                        {
                            customAttributesToRemove = new List<IType>();
                            customAttributesToRemove.Add(this.CommonVBTypeRefs.ExtensionAttribute);
                        }
                    }
                    if (customAttributesToRemove != null && customAttributesToRemove.Count > 0)
                    {
                        var removeQuery = (from mSet in this.Metadata
                                           from m in mSet
                                           join toRemove in customAttributesToRemove on m.Type equals toRemove
                                           select m).ToArray();
                        foreach (var toRemove in removeQuery)
                            this.Metadata.Remove(toRemove);
                    }
                    if (customAttributesToAdd != null && customAttributesToAdd.Count > 0)
                        this.Metadata.Add((from toAdd in customAttributesToAdd
                                           select new MetadatumDefinitionParameterValueCollection(toAdd)).ToArray());
                    base.SpecialModifier = value;
                }
            }
        }

    }
}
