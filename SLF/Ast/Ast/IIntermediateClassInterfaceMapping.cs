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
    /// intermediate class's interface member mapping.
    /// </summary>
    /// <remarks>Used to simplify typename associated to
    /// interface->class signature->member mapping.</remarks>
    public interface IIntermediateClassInterfaceMapping :
        IIntermediateInterfaceMemberMapping<IClassEventMember, IIntermediateClassEventMember,
                                            IClassIndexerMember, IIntermediateClassIndexerMember,
                                            IClassMethodMember, IIntermediateClassMethodMember, 
                                            IClassPropertyMember, IIntermediateClassPropertyMember,
                                            IClassType, IIntermediateClassType>,
        IClassInterfaceMapping
    {
    }
}
