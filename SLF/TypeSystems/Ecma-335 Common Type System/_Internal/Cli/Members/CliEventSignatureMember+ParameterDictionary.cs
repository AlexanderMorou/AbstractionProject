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
            IEventSignatureParent<TEvent, TEventParent>
    {
        
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
            protected ParameterDictionary(_ICliManager manager, ICliMetadataEventTableRow metadataEntry)
                : base(manager, CliMemberExtensions.GetEventDelegateMethod(metadataEntry, manager), metadataEntry.MetadataRoot)
            {
            }
        }
    }
}
