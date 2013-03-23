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

        private new IIntermediateCliManager IdentityManager { get { return (IIntermediateCliManager)base.IdentityManager; } }

        private CommonVBTypeRefs CommonVBTypeRefs { get { return VisualBasic.CommonVBTypeRefs.GetCommonTypeRefs(this.IdentityManager); } }

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
                base.SpecialModifier = value;
            }
        }

    }
}
