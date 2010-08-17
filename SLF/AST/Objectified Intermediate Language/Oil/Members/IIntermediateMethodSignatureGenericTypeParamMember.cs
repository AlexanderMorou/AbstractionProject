using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Members
{
    public interface IIntermediateMethodSignatureGenericTypeParameterMember :
        IIntermediateGenericParameter<IMethodSignatureGenericTypeParameterMember, IIntermediateMethodSignatureGenericTypeParameterMember, IMethodSignatureMember, IIntermediateMethodSignatureMember>,
        IMethodSignatureGenericTypeParameterMember
    {
    }
}
