using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.GenericLayer.Members
{
    /// <summary>
    /// Defines properties and methods for a generic method signature's 
    /// original to receive constructor verification of a generic 
    /// instantiation.
    /// </summary>
    internal interface _IGenericMethodSignatureRegistrar
    {
        void RegisterGenericMethodSignature(IMethodSignatureMember targetSignature, IControlledTypeCollection typeParameters);
        void UnregisterGenericMethodSignature(IControlledTypeCollection typeParameters);
    }
}
