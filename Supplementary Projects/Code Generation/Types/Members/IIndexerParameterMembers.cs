using System;
using System.Collections.Generic;
using System.Text;
using System.CodeDom;

namespace AllenCopeland.Abstraction.OldCodeGen.Types.Members
{
    public interface IIndexerParameterMembers :
        //IParameteredParameterMembers<IIndexerParameterMember, CodeMemberProperty, IIndexerMember>
        IIndexerSignatureParameterMembers<IIndexerParameterMember, CodeMemberProperty, IIndexerMember>
    {
    }
}
