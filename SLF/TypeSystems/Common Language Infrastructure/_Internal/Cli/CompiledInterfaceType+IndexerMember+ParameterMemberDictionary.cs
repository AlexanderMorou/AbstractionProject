using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf._Internal.Abstract;
using System.Reflection;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    partial class CompiledInterfaceType
    {
        partial class IndexerMember
        {
            protected sealed class ParameterMemberDictionary :
                LockedParameterMembersBase<IInterfaceIndexerMember, IIndexerSignatureParameterMember<IInterfaceIndexerMember, IInterfaceType>>,
                IParameterMemberDictionary<IInterfaceIndexerMember, IIndexerSignatureParameterMember<IInterfaceIndexerMember, IInterfaceType>>
            {
                internal ParameterMemberDictionary(IndexerMember parent, IEnumerable<ParameterInfo> parameters)
                    : base(parent, parameters.Select(p => (IIndexerSignatureParameterMember<IInterfaceIndexerMember, IInterfaceType>)new ParameterMember(p, parent)))
                {
                }
            }
        }
    }
}
