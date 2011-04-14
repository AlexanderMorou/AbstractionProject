using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Oil.Members;

namespace AllenCopeland.Abstraction.Slf.Oil
{
    internal class IntermediateTopLevelFieldMemberDictionary :
        IntermediateFieldMemberDictionary<ITopLevelFieldMember, IIntermediateTopLevelFieldMember, INamespaceParent, IIntermediateNamespaceParent>
    {
        public IntermediateTopLevelFieldMemberDictionary(IntermediateFullMemberDictionary master, IIntermediateNamespaceParent parent)
            : base(master, parent)
        {
        }

        internal IntermediateTopLevelFieldMemberDictionary(IntermediateFullMemberDictionary master, IIntermediateNamespaceParent parent, IntermediateTopLevelFieldMemberDictionary sibling)
            : base(master, parent, sibling)
        {

        }
        protected override IIntermediateTopLevelFieldMember GetField(TypedName nameAndType)
        {
            return new IntermediateTopLevelFieldMember(nameAndType.Name, this.Parent) { FieldType = nameAndType.GetTypeRef() };
        }
    }
}
