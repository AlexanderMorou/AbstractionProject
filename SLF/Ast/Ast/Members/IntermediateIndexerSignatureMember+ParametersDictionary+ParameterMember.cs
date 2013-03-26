using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;

namespace AllenCopeland.Abstraction.Slf.Ast.Members
{
    partial class IntermediateIndexerSignatureMember<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent, TMethodMember>
        where TIndexer :
            IIndexerSignatureMember<TIndexer, TIndexerParent>
        where TIntermediateIndexer :
            TIndexer,
            IIntermediateIndexerSignatureMember<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>
        where TIndexerParent :
            IIndexerSignatureParent<TIndexer, TIndexerParent>
        where TIntermediateIndexerParent :
            TIndexerParent,
            IIntermediateIndexerSignatureParent<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>
        where TMethodMember :
            class,
            IIntermediatePropertySignatureMethodMember
    {
        partial class ParametersDictionary
        {
            protected class ParameterMember :
                IntermediateParameterMemberBase<TIndexer, TIntermediateIndexer, IIndexerSignatureParameterMember<TIndexer, TIndexerParent>, IIntermediateIndexerSignatureParameterMember<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>>,
                IIntermediateIndexerSignatureParameterMember<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>
            {
                public ParameterMember(TIntermediateIndexer parent, ITypeIdentityManager identityManager)
                    : base(parent, identityManager)
                {
                }
                public ParameterMember(string name, TIntermediateIndexer parent, ITypeIdentityManager identityManager)
                    : base(parent, identityManager)
                {
                    this.AssignName(name);
                }

                #region IIntermediateSignatureParameterMember Members

                IIntermediateSignatureMember IIntermediateSignatureParameterMember.Parent
                {
                    get { return this.Parent; }
                }

                #endregion

                #region ISignatureParameterMember Members

                ISignatureMember ISignatureParameterMember.Parent
                {
                    get { return this.Parent; }
                }

                #endregion
            }
        }
    }
}
