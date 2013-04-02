using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.GenericLayer.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.GenericLayer
{
    partial class _InterfaceTypeBase
    {
        internal protected class _MethodsBase
            : _MethodSignatureMembersBase<IInterfaceMethodMember, IInterfaceType>
        {
            internal _MethodsBase(_FullMembersBase master, IMethodSignatureMemberDictionary<IInterfaceMethodMember, IInterfaceType> originalSet, _InterfaceTypeBase parent)
                : base(master, originalSet, parent)
            {

            }
            protected override IInterfaceMethodMember ObtainWrapper(IInterfaceMethodMember item)
            {
                return new _Method(this.Parent, item);
            }
            internal class _Method
                : _MethodSignatureMemberBase<IInterfaceMethodMember, IInterfaceType>,
                IInterfaceMethodMember
            {
                internal _Method(IInterfaceType parent, IInterfaceMethodMember original)
                    : base(parent, original)
                {
                }

                internal _Method(IInterfaceMethodMember original, IControlledTypeCollection genericParameters)
                    : base(original, genericParameters)
                {
                }


                protected override IInterfaceMethodMember OnMakeGenericMethod(IControlledTypeCollection genericReplacements)
                {
                    return new _Method(this, genericReplacements);
                }
            }
        }

    }
}
