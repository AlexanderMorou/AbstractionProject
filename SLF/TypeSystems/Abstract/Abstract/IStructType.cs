using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// Defines properties and methods for working with a simple data structure.
    /// </summary>
    public interface IStructType :
        IGenericType<IStructType>,
        IInstantiableType<IStructCtorMember, IStructEventMember, IStructFieldMember, IStructIndexerMember, IStructMethodMember, IStructPropertyMember, IStructType>
    {

    }
}
