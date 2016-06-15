using AllenCopeland.Abstraction.Slf._Internal.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Members
{
    partial class CliDependentParameterMemberDictionary<TParent, TParameter>
    {
        protected class ParameterMember :
            ParameterMemberBase<TParent>,
            IParameterMember<TParent>
        {
            private IDelegateTypeParameterMember sourceParameter;

            protected ParameterMember(TParent parent, IDelegateTypeParameterMember sourceParameter)
                : base(parent)
            {
                this.sourceParameter = sourceParameter;
            }

            protected override IType ParameterTypeImpl
            {
                get { return this.sourceParameter.ParameterType; }
            }

            public override ParameterCoercionDirection Direction
            {
                get { return this.sourceParameter.Direction; }
            }

            protected override IMetadataCollection InitializeMetadata()
            {
                return this.sourceParameter.Metadata;
            }

            protected override string OnGetName()
            {
                return this.sourceParameter.Name;
            }

            public override IGeneralMemberUniqueIdentifier UniqueIdentifier
            {
                get { return this.sourceParameter.UniqueIdentifier; }
            }
        }
    }
}
