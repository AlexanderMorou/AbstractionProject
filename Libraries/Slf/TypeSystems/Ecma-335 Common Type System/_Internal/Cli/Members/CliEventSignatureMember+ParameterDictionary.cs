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

    partial class CliEventSignatureMember<TEvent, TEventParent>
        where TEvent :
            IEventSignatureMember<TEvent, TEventParent>
        where TEventParent :
            class,
            IType,
            IEventSignatureParent<TEvent, TEventParent>
    {

        private class DependentParameterDictionary :
            CliEventSignatureMember<TEvent, IEventSignatureParameterMember<TEvent, TEventParent>, TEventParent>.DependentParameterDictionary
        {
            internal DependentParameterDictionary(TEvent parent, IDelegateType delegateType)
                : base(parent, delegateType)
            {
            }


            protected override IEventSignatureParameterMember<TEvent, TEventParent> GetParameter(IDelegateTypeParameterMember parameterMember)
            {
                return new ParameterMember(this.Parent, parameterMember);
            }

            private class ParameterMember :
                CliEventSignatureMember<TEvent, IEventSignatureParameterMember<TEvent, TEventParent>, TEventParent>.DependentParameterDictionary.ParameterMember,
                IEventSignatureParameterMember<TEvent, TEventParent>
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
            internal Parameters(_ICliManager manager, ICliMetadataEventTableRow metadataEntry, CliEventSignatureMember<TEvent, TEventParent> parent)
                : base(manager, metadataEntry, parent)
            {
            }
            private class Parameter :
                CliParameterMember<TEvent, CliEventSignatureMember<TEvent, TEventParent>>,
                IEventSignatureParameterMember<TEvent, TEventParent>
            {
                internal Parameter(ICliMetadataParameterTableRow metadataEntry, CliEventSignatureMember<TEvent, TEventParent> parent, int index)
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

            protected override IEventSignatureParameterMember<TEvent, TEventParent> CreateElementFrom(int index, ICliMetadataParameterTableRow metadata)
            {
                return new Parameter(metadata, (CliEventSignatureMember<TEvent, TEventParent>)(object)this.Parent, index);
            }
        }
    }

    partial class CliEventSignatureMember<TEvent, TEventParameter, TEventParent>
        where TEvent :
            IEventSignatureMember<TEvent, TEventParameter, TEventParent>
        where TEventParameter :
            class,
            IEventSignatureParameterMember<TEvent, TEventParameter, TEventParent>
        where TEventParent :
            IEventSignatureParent<TEvent, TEventParameter, TEventParent>
    {
        protected abstract class ParameterDictionary :
            CliParameterMemberDictionary<TEvent, TEventParameter>
        {
            protected ParameterDictionary(_ICliManager manager, ICliMetadataEventTableRow metadataEntry, CliEventSignatureMember<TEvent, TEventParameter, TEventParent> parent)
                : base(manager, CliMemberExtensions.GetEventDelegateMethod(metadataEntry, manager), CliMemberExtensions.GetEventDelegateMetadataRoot(metadataEntry, manager), (TEvent)(object)parent)
            {
            }
        }

        protected abstract class DependentParameterDictionary :
            CliDependentParameterMemberDictionary<TEvent, TEventParameter>
        {
            protected DependentParameterDictionary(TEvent parent, IDelegateType delegateType)
                : base(parent, delegateType)
            {
            }
        }
    }
}
