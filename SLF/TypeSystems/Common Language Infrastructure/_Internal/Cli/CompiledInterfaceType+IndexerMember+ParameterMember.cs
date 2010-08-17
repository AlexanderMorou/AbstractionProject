using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf._Internal.Abstract.Members;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Members;
using System.Reflection;
using AllenCopeland.Abstraction.Slf.Abstract;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    partial class CompiledInterfaceType
    {
        partial class IndexerMember
        {
            private class ParameterMember :
                CompiledParameterMemberBase<IInterfaceIndexerMember>,
                IIndexerSignatureParameterMember<IInterfaceIndexerMember, IInterfaceType>
            {
                internal ParameterMember(ParameterInfo paramInfo, IInterfaceIndexerMember parent)
                    : base(paramInfo, parent)
                {
                }

                #region ISignatureParameterMember Members

                ISignatureMember ISignatureParameterMember.Parent
                {
                    get { return base.Parent; }
                }

                #endregion
            }
        }
    }
}
