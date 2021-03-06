using System;
using System.Collections.Generic;
using System.Text;
using System.CodeDom;

namespace AllenCopeland.Abstraction.OldCodeGen.Types.Members
{
    /// <summary>
    /// Defines properties and methods for working with a parameter or an indexer.
    /// </summary>
    public interface IIndexerParameterMember :
        IIndexerSignatureParameterMember<IIndexerParameterMember, CodeMemberProperty, IIndexerMember>
    {
    }
}
