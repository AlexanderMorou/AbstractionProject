using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;

namespace AllenCopeland.Abstraction.Slf.Oil.Members
{
    partial class IntermediateIndexerMember<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent, TMethodMember>
        where TIndexer :
            IIndexerMember<TIndexer, TIndexerParent>
        where TIntermediateIndexer :
            TIndexer,
            IIntermediateIndexerMember<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>
        where TIndexerParent :
            IIndexerParent<TIndexer, TIndexerParent>
        where TIntermediateIndexerParent :
            TIndexerParent,
            IIntermediateIndexerParent<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>
        where TMethodMember :
            class,
            IIntermediatePropertyMethodMember
    {
        partial class ParameterMembersDictionary
        {
            private class ParameterMember :
                IntermediateSignatureParameterMemberBase<TIndexer, TIntermediateIndexer, IIndexerParameterMember<TIndexer, TIndexerParent>, IIntermediateIndexerParameterMember<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>, TIndexerParent, TIntermediateIndexerParent>,
                IIntermediateIndexerParameterMember<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>
            {
                public ParameterMember(TIntermediateIndexer parent)
                    : base(parent)
                {
                }
            }
        }
    }
}
