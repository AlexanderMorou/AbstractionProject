﻿using System;
using System.Collections.Generic;
using System.Linq;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Oil.Members;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Oil
{
    partial class IntermediateInterfaceType<TInstanceType>
        where TInstanceType :
            IntermediateInterfaceType<TInstanceType>
    {
        private class IndexerMembers :
            IntermediateIndexerSignatureMemberDictionary<IInterfaceIndexerMember, IIntermediateInterfaceIndexerMember, IInterfaceType, IIntermediateInterfaceType>
        {
            public IndexerMembers(IntermediateFullMemberDictionary master, TInstanceType parent)
                : base(master, parent)
            {

            }
            public IndexerMembers(IntermediateFullMemberDictionary master, TInstanceType parent, IndexerMembers root)
                : base(master, parent, root)
            {

            }


        }

        private class PropertyMembers :
            IntermediatePropertySignatureMemberDictionary<IInterfacePropertyMember, IIntermediateInterfacePropertyMember, IInterfaceType, IIntermediateInterfaceType>
        {
            public PropertyMembers(IntermediateFullMemberDictionary master, TInstanceType parent)
                : base(master, parent)
            {

            }
            public PropertyMembers(IntermediateFullMemberDictionary master, TInstanceType parent, PropertyMembers root)
                : base(master, parent, root)
            {

            }

            public override IIntermediateInterfacePropertyMember Add(TypedName nameAndType)
            {
                throw new NotImplementedException();
            }
        }

        private class EventMembers :
            IntermediateEventSignatureMemberDictionary<IInterfaceEventMember, IIntermediateInterfaceEventMember, IInterfaceType, IIntermediateInterfaceType>
        {
            public EventMembers(IntermediateFullMemberDictionary master, TInstanceType parent)
                : base(master, parent)
            {

            }
            public EventMembers(IntermediateFullMemberDictionary master, TInstanceType parent, EventMembers root)
                : base(master, parent, root)
            {

            }

            protected override IIntermediateInterfaceEventMember GetEvent(TypedName typedName)
            {
                if (typedName.Source == TypedNameSource.TypeReference &&
                    typedName.Reference is IDelegateType)
                    return new EventMember(this.Parent) { Name = typedName.Name, SignatureType = (IDelegateType)typedName.Reference };
                else
                    throw new ArgumentException("typedName");
            }
        }
        private class MethodMembers :
            IntermediateGroupedMethodSignatureMemberDictionary<IMethodSignatureParameterMember<IInterfaceMethodMember, IInterfaceType>, IIntermediateMethodSignatureParameterMember<IInterfaceMethodMember, IIntermediateInterfaceMethodMember, IInterfaceType, IIntermediateInterfaceType>, IInterfaceMethodMember, IIntermediateInterfaceMethodMember, IInterfaceType, IIntermediateInterfaceType>,
            IIntermediateMethodSignatureMemberDictionary<IInterfaceMethodMember, IIntermediateInterfaceMethodMember, IInterfaceType, IIntermediateInterfaceType>
        {

            public MethodMembers(IntermediateFullMemberDictionary master, TInstanceType parent)
                : base(master, parent)
            {
            }

            public MethodMembers(IntermediateFullMemberDictionary master, TInstanceType parent, MethodMembers root)
                : base(master, parent, root)
            {
            }

            protected override IIntermediateInterfaceMethodMember OnGetNewMethod(string name)
            {
                return new MethodMember(this.Parent) { Name = name };
            }

        }
    }
}