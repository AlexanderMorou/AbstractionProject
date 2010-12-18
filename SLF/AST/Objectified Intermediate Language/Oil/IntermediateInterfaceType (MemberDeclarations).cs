using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil.Members;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf._Internal.GenericLayer;

namespace AllenCopeland.Abstraction.Slf.Oil
{
    partial class IntermediateInterfaceType<TInstanceType>
        where TInstanceType :
            IntermediateInterfaceType<TInstanceType>
    {
        private class MethodMember :
            IntermediateMethodSignatureMemberBase<IInterfaceMethodMember, IIntermediateInterfaceMethodMember, IInterfaceType, IIntermediateInterfaceType>,
            IIntermediateInterfaceMethodMember
        {
            public MethodMember(IIntermediateInterfaceType parent)
                : base(parent)
            {
            }
            protected override IInterfaceMethodMember OnMakeGenericMethod(ITypeCollectionBase genericReplacements)
            {
                return new _InterfaceTypeBase._MethodsBase._Method(this, genericReplacements);
            }
        }
        private class EventMember :
            IntermediateEventSignatureMemberBase<IInterfaceEventMember, IIntermediateInterfaceEventMember, IInterfaceType, IIntermediateInterfaceType>,
            IIntermediateInterfaceEventMember
        {
            public EventMember(IIntermediateInterfaceType parent)
                : base(parent)
            {
            }
        }
    }
}
