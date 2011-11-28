using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Ast.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast
{
    public class IntermediateTopLevelFieldMemberDictionary :
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
