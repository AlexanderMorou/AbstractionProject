using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
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
