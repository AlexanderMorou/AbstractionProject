using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.GenericLayer.Members
{
    /// <summary>
    /// Defines properties and methods for a generic method's original
    /// to receive constructor verification of a generic instantiation
    /// of a generic method.
    /// </summary>
    internal interface _IGenericMethodRegistrar
    {
        void RegisterGenericMethod(IMethodMember targetType, IControlledTypeCollection typeParameters);
        void UnregisterGenericMethod(IControlledTypeCollection typeParameters);
    }
}
