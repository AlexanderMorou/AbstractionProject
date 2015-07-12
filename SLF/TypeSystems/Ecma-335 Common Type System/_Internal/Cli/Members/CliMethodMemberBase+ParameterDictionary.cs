using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Members
{
    partial class CliMethodMemberBase<TMethod, TMethodParent>
        where TMethod :
            IMethodMember<TMethod, TMethodParent>
        where TMethodParent :
            IMethodParent<TMethod, TMethodParent>
    {
        internal class ParameterMemberDictionary :
            CliParameterMemberDictionary<TMethod, IMethodParameterMember<TMethod, TMethodParent>>
        {
            private new CliMethodMemberBase<TMethod, TMethodParent> Parent { get { return ((CliMethodMemberBase<TMethod, TMethodParent>)(object)base.Parent); } }

            public ParameterMemberDictionary(CliMethodMemberBase<TMethod, TMethodParent> signature)
                : base(signature.IdentityManager, signature.MetadataEntry.Index, signature.MetadataEntry.MetadataRoot, (TMethod)(object)signature, signature)
            {
            }

            protected override IMethodParameterMember<TMethod, TMethodParent> CreateElementFrom(int index, ICliMetadataParameterTableRow metadata)
            {
                return this.Parent.CreateParameter(index, metadata);
            }
        }

        internal abstract IMethodParameterMember<TMethod, TMethodParent> CreateParameter(int index, ICliMetadataParameterTableRow metadata);

        internal abstract class ParameterMember :
            CliParameterMember<TMethod, CliMethodMemberBase<TMethod, TMethodParent>>,
            IMethodParameterMember<TMethod, TMethodParent>
        {
            internal ParameterMember(ICliMetadataParameterTableRow metadataEntry, CliMethodMemberBase<TMethod, TMethodParent> parent, int index)
                : base(metadataEntry, parent, index)
            {
            }

            #region ISignatureParameterMember Members

            ISignatureMember ISignatureParameterMember.Parent
            {
                get { return this.Parent; }
            }

            #endregion

            #region IMethodSignatureParameterMember Members

            IMethodSignatureMember IMethodSignatureParameterMember.Parent
            {
                get { return this.Parent; }
            }

            #endregion

            IMethodMember IMethodParameterMember.Parent
            {
                get { return this.Parent; }
            }

            protected override IMethodSignatureMember ActiveMethod
            {
                get { return this.Parent; }
            }

            public override string ToString()
            {
                return this.UniqueIdentifier.ToString();
            }
        }

    }
}
