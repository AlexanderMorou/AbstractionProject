﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Modules;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf._Internal.GenericLayer.Members;
using AllenCopeland.Abstraction.Slf.Abstract.Properties;

namespace AllenCopeland.Abstraction.Slf._Internal.GenericLayer
{
    internal class _TopLevelMethod :
        _MethodMemberBase<ITopLevelMethodMember, INamespaceParent>,
        ITopLevelMethodMember
    {
        public _TopLevelMethod(ITopLevelMethodMember original, ITypeCollectionBase genericReplacements)
            : base(original, genericReplacements)
        {

        }
        protected override ITopLevelMethodMember OnMakeGenericMethod(ITypeCollectionBase genericReplacements)
        {
            throw new InvalidOperationException(Resources.MakeGenericTypeError_IsGenericTypeDefFalse);
        }

        #region ITopLevelMethodMember Members

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