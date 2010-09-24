using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Members;
using System.Reflection;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    partial class CompiledInterfaceType
    {
        private class EventMember :
            CompiledEventSignatureMemberBase<IInterfaceMethodMember, IInterfaceEventMember, IInterfaceType>,
            IInterfaceEventMember
        {
            public EventMember(EventInfo source, IInterfaceType parent)
                : base(parent, source)
            {
            }

        }
    }
}
