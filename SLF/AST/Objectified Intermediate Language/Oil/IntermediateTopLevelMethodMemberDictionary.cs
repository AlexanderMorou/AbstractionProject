using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Oil.Members;

namespace AllenCopeland.Abstraction.Slf.Oil
{
    public class IntermediateTopLevelMethodMemberDictionary :
        IntermediateMethodMemberDictionary<ITopLevelMethodMember, IIntermediateTopLevelMethodMember, INamespaceParent, IIntermediateNamespaceParent>
    {
        public IntermediateTopLevelMethodMemberDictionary(IntermediateFullMemberDictionary master, IIntermediateNamespaceParent parent)
            : base(master, parent)
        {

        }

        internal IntermediateTopLevelMethodMemberDictionary(IntermediateFullMemberDictionary master, IIntermediateNamespaceParent parent, IntermediateTopLevelMethodMemberDictionary sibling)
            : base(master, parent, sibling)
        {

        }

        protected override IIntermediateTopLevelMethodMember OnGetNewMethod(string name)
        {
            return new IntermediateTopLevelMethodMember(name, this.Parent);
        }
    }
}
