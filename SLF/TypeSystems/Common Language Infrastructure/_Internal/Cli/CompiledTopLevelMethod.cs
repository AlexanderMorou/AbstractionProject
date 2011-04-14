using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf._Internal.GenericLayer.Members;
using AllenCopeland.Abstraction.Slf.Abstract.Properties;
using AllenCopeland.Abstraction.Slf.Abstract.Modules;
using System.Reflection;
namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    internal class CompiledTopLevelMethod :
        CompiledMethodMemberBase<ITopLevelMethod, INamespaceParent>,
        ITopLevelMethod
    {

        public CompiledTopLevelMethod(MethodInfo memberInfo, INamespaceParent parent)
            : base(memberInfo, parent)
        {
        }

        protected override ITopLevelMethod OnMakeGenericClosure(ITypeCollectionBase genericReplacements)
        {
            return new _TopLevelMethod(this, genericReplacements);
        }
        #region ITopLevelMethod Members

        public IModule DeclaringModule
        {
            get {
                throw new NotImplementedException();
                //return this.MemberInfo.Module;
            }
        }

        public string FullName
        {
            get { return this.MemberInfo.Name; }
        }

        #endregion

        private class _TopLevelMethod :
            _MethodMemberBase<ITopLevelMethod, INamespaceParent>,
            ITopLevelMethod
        {
            public _TopLevelMethod(CompiledTopLevelMethod original, ITypeCollectionBase genericReplacements)
                : base(original, genericReplacements)
            {

            }
            protected override ITopLevelMethod OnMakeGenericMethod(ITypeCollectionBase genericReplacements)
            {
                throw new InvalidOperationException(Resources.MakeGenericTypeError_IsGenericTypeDefFalse);
            }

            #region ITopLevelMethod Members

            public IModule DeclaringModule
            {
                get { return this.Original.DeclaringModule; }
            }

            public string FullName
            {
                get { return this.Original.FullName; }
            }

            #endregion

        }


    }
}
