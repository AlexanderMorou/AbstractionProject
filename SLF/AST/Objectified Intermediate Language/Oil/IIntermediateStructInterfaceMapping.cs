using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Oil.Members;
using AllenCopeland.Abstraction.Slf.Abstract;

namespace AllenCopeland.Abstraction.Slf.Oil
{
    /// <summary>
    /// Defines properties and methods for working with an 
    /// intermediate struct's interface member mapping.
    /// </summary>
    /// <remarks>Used to simplify typename associated to
    /// interface->struct signature->member mapping.</remarks>
    public interface IIntermediateStructInterfaceMapping :
        IIntermediateSignatureMemberMapping<IStructEventMember, IInterfaceEventMember, IIntermediateStructEventMember, IIntermediateInterfaceEventMember,
                                            IStructIndexerMember, IInterfaceIndexerMember, IIntermediateStructIndexerMember, IIntermediateInterfaceIndexerMember,
                                            IStructMethodMember, IInterfaceMethodMember, IIntermediateStructMethodMember, IIntermediateInterfaceMethodMember, 
                                            IStructPropertyMember, IInterfacePropertyMember, IIntermediateStructPropertyMember, IIntermediateInterfacePropertyMember,
                                            IStructType, IInterfaceType, IIntermediateStructType, IIntermediateInterfaceType>,
        IStructInterfaceMapping
    {
    }
}
