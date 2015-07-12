using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;

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
        protected partial class ParametersDictionary :
            IntermediateParameterMemberDictionary<TIndexer, TIntermediateIndexer, IIndexerSignatureParameterMember<TIndexer, TIndexerParent>, IIntermediateIndexerSignatureParameterMember<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>>
        {
            private IIntermediateAssembly assembly;
            public ParametersDictionary(TIntermediateIndexer parent, IIntermediateAssembly assembly)
                : base(parent)
            {
                this.assembly = assembly;
            }

            protected override IIntermediateIndexerSignatureParameterMember<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent> GetNewParameter(string name, IType parameterType, ParameterCoercionDirection direction)
            {
                ParameterMember result = new ParameterMember(Parent, this.assembly) { Direction = direction, ParameterType = parameterType };
                result.AssignName(name);
                return result;
            }
        }
    }
}
