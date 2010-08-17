using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/*----------------------------------------\
| Copyright © 2009 Allen Copeland Jr.     |
|-----------------------------------------|
| The Abstraction Project's code is prov- |
| -ided under a contract-release basis.   |
| DO NOT DISTRIBUTE and do not use beyond |
| the contract terms.                     |
\--------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Abstract.Members
{
    /// <summary>
    /// Defines a shell interface used to filter auto-types out during translation
    /// to languages (such as Visual Basic.NET).
    /// </summary>
    /// <remarks>Type should -never- appear as a <see cref="IEventSignatureMember.SignatureType"/>
    /// value of a compiled event member.</remarks>
    public interface IEventMemberSignatureType :
        IDelegateType
    {
    }
}
