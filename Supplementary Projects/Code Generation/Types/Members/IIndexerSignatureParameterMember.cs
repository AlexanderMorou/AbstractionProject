using System;
using System.Collections.Generic;
using System.Text;
using System.CodeDom;

namespace AllenCopeland.Abstraction.OldCodeGen.Types.Members
{
    /// <summary>
    /// Defines properties and methods for working with the parameter member of an 
    /// indexer property.
    /// </summary>
    public interface IIndexerSignatureParameterMember :
        IIndexerSignatureParameterMember<IIndexerSignatureParameterMember, CodeMemberProperty, IIndexerSignatureMember>
    {
    }
}
