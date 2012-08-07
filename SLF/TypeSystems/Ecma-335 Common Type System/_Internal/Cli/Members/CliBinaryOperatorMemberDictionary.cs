using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Slf.Cli;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Members
{
    internal class CliBinaryOperatorMemberDictionary<TCoercionParent> :
        CliCoercionMemberDictionary<IBinaryOperatorUniqueIdentifier, IBinaryOperatorCoercionMember<TCoercionParent>, TCoercionParent>
        where TCoercionParent :
            ICoercibleType<IBinaryOperatorUniqueIdentifier, IBinaryOperatorCoercionMember<TCoercionParent>, TCoercionParent>
    {
        public CliBinaryOperatorMemberDictionary(ICliMetadataMethodDefinitionTableRow[] filteredSet, TCoercionParent parent)
            : base(filteredSet, parent)
        {
        }
        protected override IBinaryOperatorUniqueIdentifier GetIdentifierFrom(int index, ICliMetadataMethodDefinitionTableRow metadata)
        {
            return CliCommon.GetBinaryOperatorUniqueIdentifier((this.Parent as ICliType).Metadata, metadata);
        }

        protected override IBinaryOperatorCoercionMember<TCoercionParent> CreateElementFrom(int index, ICliMetadataMethodDefinitionTableRow metadata)
        {
            throw new NotImplementedException();
        }
    }
}
