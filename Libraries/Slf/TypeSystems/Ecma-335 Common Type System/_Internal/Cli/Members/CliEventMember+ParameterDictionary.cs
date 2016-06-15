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

    partial class CliEventMember<TEvent, TEventParent>
        where TEvent :
            IEventMember<TEvent, TEventParent>
        where TEventParent :
            class,
            IType,
            IEventParent<TEvent, TEventParent>
    {

        private class DependentParameterDictionary :
            CliEventSignatureMember<TEvent, IEventParameterMember<TEvent, TEventParent>, TEventParent>.DependentParameterDictionary
        {
            internal DependentParameterDictionary(TEvent parent, IDelegateType delegateType)
                : base(parent, delegateType)
            {
            }


            protected override IEventParameterMember<TEvent, TEventParent> GetParameter(IDelegateTypeParameterMember parameterMember)
            {
                return new ParameterMember(this.Parent, parameterMember);
            }

            private class ParameterMember :
                CliEventSignatureMember<TEvent, IEventParameterMember<TEvent, TEventParent>, TEventParent>.DependentParameterDictionary.ParameterMember,
                IEventParameterMember<TEvent, TEventParent>
            {
                internal ParameterMember(TEvent parent, IDelegateTypeParameterMember sourceParameter)
                    : base(parent, sourceParameter)
                {

                }

                ISignatureMember ISignatureParameterMember.Parent
                {
                    get { return this.Parent; }
                }
            }

        }

        private class Parameters :
            ParameterDictionary
        {
            internal Parameters(_ICliManager manager, ICliMetadataEventTableRow metadataEntry, CliEventMember<TEvent, TEventParent> parent)
                : base(manager, metadataEntry, parent)
            {
            }
            private class Parameter :
                CliParameterMember<TEvent, CliEventMember<TEvent, TEventParent>>,
                IEventParameterMember<TEvent, TEventParent>
            {
                internal Parameter(ICliMetadataParameterTableRow metadataEntry, CliEventMember<TEvent, TEventParent> parent, int index)
                    : base(metadataEntry, parent, index)
                {
                }
                protected override IMethodSignatureMember ActiveMethod
                {
                    get { return null; }
                }

                protected override IType ActiveType
                {
                    get { return this.Parent.Parent; }
                }

                ISignatureMember ISignatureParameterMember.Parent
                {
                    get { return this.Parent; }
                }

            }

            protected override IEventParameterMember<TEvent, TEventParent> CreateElementFrom(int index, ICliMetadataParameterTableRow metadata)
            {
                return new Parameter(metadata, (CliEventMember<TEvent, TEventParent>)(object)this.Parent, index);
            }
        }
    }

}
