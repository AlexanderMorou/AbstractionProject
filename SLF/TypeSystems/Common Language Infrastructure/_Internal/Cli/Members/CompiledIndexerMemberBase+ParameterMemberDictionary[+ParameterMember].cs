﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Members;
using AllenCopeland.Abstraction.Slf._Internal.Abstract;
using System.Reflection;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Members
{
    partial class CompiledIndexerMemberBase<TIndexer, TIndexerParent, TIndexerMethod, TMethod, TMethodParent>
        where TIndexer :
            IIndexerMember<TIndexer, TIndexerParent>
        where TIndexerParent :
            IIndexerParent<TIndexer, TIndexerParent>
        where TIndexerMethod :
            class,
            TMethod,
            IExtendedInstanceMember,
            IPropertyMethodMember
        where TMethod :
            IMethodMember<TMethod, TMethodParent>
        where TMethodParent :
            IMethodParent<TMethod, TMethodParent>
    {
        private class ParameterDictionary :
            LockedParameterMembersBase<TIndexer, IIndexerParameterMember<TIndexer, TIndexerParent>>
        {
            public ParameterDictionary(TIndexer parent, IEnumerable<ParameterInfo> parameters)
                : base(parent, parameters.Select(p => (IIndexerParameterMember<TIndexer, TIndexerParent>)new ParameterMember(p, parent)))
            {
            }

            private class ParameterMember :
                CompiledParameterMemberBase<TIndexer>,
                IIndexerParameterMember<TIndexer, TIndexerParent>
            {
                public ParameterMember(ParameterInfo memberInfo, TIndexer parent)
                    : base(memberInfo, parent)
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