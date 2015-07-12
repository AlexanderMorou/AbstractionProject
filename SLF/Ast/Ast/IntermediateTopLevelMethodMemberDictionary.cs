using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Ast.Members;
/*---------------------------------------------------------------------\
| Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
|----------------------------------------------------------------------|
| The Abstraction Project's code is provided under a contract-release  |
| basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
\-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast
{
    public class IntermediateTopLevelMethodMemberDictionary :
        IntermediateMethodMemberDictionary<ITopLevelMethodMember, IIntermediateTopLevelMethodMember, INamespaceParent, IIntermediateNamespaceParent>
    {
        public IntermediateTopLevelMethodMemberDictionary(IntermediateFullMemberDictionary master, IIntermediateNamespaceParent parent)
            : base(master, parent, parent.IdentityManager)
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
