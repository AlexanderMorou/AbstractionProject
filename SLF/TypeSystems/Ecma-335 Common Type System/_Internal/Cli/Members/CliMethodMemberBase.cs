using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Members
{
    internal abstract partial class CliMethodMemberBase<TMethod, TMethodParent> :
        CliMethodSignatureBase<IMethodParameterMember<TMethod, TMethodParent>, TMethod, TMethodParent>,
        IMethodMember<TMethod, TMethodParent>
        where TMethod :
            IMethodMember<TMethod, TMethodParent>
        where TMethodParent :
            IMethodParent<TMethod, TMethodParent>
    {
        protected CliMethodMemberBase(ICliMetadataMethodDefinitionTableRow metadataEntry, _ICliAssembly assembly, TMethodParent parent, IGeneralGenericSignatureMemberUniqueIdentifier uniqueIdentifier)
            : base(metadataEntry, assembly, parent, uniqueIdentifier)
        {
        }

        public AccessLevelModifiers AccessLevel
        {
            get { return CliCommon.GetMethodAccessLevel((MethodAttributes)this.MetadataEntry.UsageDetails.Accessibility); }
        }
        protected override sealed CliParameterMemberDictionary<TMethod, IMethodParameterMember<TMethod, TMethodParent>> InitializeParameters()
        {
            return new ParameterMemberDictionary(this);
        }

    }
}
