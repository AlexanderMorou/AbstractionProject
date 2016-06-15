using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using AllenCopeland.Abstraction.Slf.Abstract;

namespace AllenCopeland.Abstraction.Slf.Ast
{
    /// <summary>
    /// Defines properties and methods for working with an 
    /// intermediate struct's interface member mapping.
    /// </summary>
    /// <remarks>Used to simplify typename associated to
    /// interface->struct signature->member mapping.</remarks>
    public interface IIntermediateStructInterfaceMapping :
        IIntermediateInterfaceMemberMapping<IStructEventMember, IIntermediateStructEventMember,
                                            IStructIndexerMember, IIntermediateStructIndexerMember,
                                            IStructMethodMember, IIntermediateStructMethodMember, 
                                            IStructPropertyMember, IIntermediateStructPropertyMember,
                                            IStructType, IIntermediateStructType>,
        IStructInterfaceMapping
    {
    }
}
