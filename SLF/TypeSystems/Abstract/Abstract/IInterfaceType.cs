﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// Defines properties and methods for working with an interface type.
    /// </summary>
    public interface IInterfaceType :
        IGenericType<IInterfaceType>,
        IMethodSignatureParent<IInterfaceMethodMember, IInterfaceType>,
        IPropertySignatureParentType<IInterfacePropertyMember, IInterfaceType>,
        IEventSignatureParent<IInterfaceEventMember, IInterfaceType>,
        IIndexerSignatureParent<IInterfaceIndexerMember, IInterfaceType>,
        IReferenceType,
        ITypeParent
    {
    }
}