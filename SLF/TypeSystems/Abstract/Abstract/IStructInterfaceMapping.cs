using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Abstract.Members
{
    /// <summary>
    /// Defines properties and methods for working with an interface
    /// mapping for a data structure type declaration.
    /// </summary>
    public interface IStructInterfaceMapping :
        IInterfaceMemberMapping<IStructMethodMember, IInterfaceMethodMember, IStructPropertyMember, IInterfacePropertyMember, IStructEventMember, IInterfaceEventMember, IStructIndexerMember, IInterfaceIndexerMember, IStructType, IInterfaceType>
    {

    }
}
