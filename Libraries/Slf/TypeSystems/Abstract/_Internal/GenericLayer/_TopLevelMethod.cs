using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Modules;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf._Internal.GenericLayer.Members;
using AllenCopeland.Abstraction.Slf.Abstract.Properties;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.GenericLayer
{
    internal class _TopLevelMethod :
        _MethodMemberBase<ITopLevelMethodMember, INamespaceParent>,
        ITopLevelMethodMember
    {
        public _TopLevelMethod(ITopLevelMethodMember original, IControlledTypeCollection genericReplacements)
            : base(original, genericReplacements)
        {

        }
        protected override ITopLevelMethodMember OnMakeGenericMethod(IControlledTypeCollection genericReplacements)
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
