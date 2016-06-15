using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Ast.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast
{
    /// <summary>
    /// Defines properties and methods for working with an intermediate
    /// data structure type declaration.
    /// </summary>
#if DEBUG
    [VisitorTargetAttribute("IntermediateTypeVisitor", ContextualVisitor  = true)]
    [VisitorTargetAttribute("IntermediateTypeVisitor", YieldingVisitor    = true)]
    [VisitorTargetAttribute("IntermediateTypeVisitor", ContextualVisitor  = true,
                                                       YieldingVisitor    = true)]
    [VisitorTargetAttribute("IntermediateTypeVisitor")]
    [VisitorTargetAttribute("IntermediateTreeVisitor", DerivedThroughInheriting = "IntermediateTypeVisitor")]
#endif
    public interface IIntermediateStructType :
        IIntermediateInstantiableType<IStructCtorMember, IIntermediateStructCtorMember, IStructEventMember, 
            IIntermediateStructEventMember, IStructFieldMember, IIntermediateStructFieldMember, 
            IStructIndexerMember, IIntermediateStructIndexerMember, IStructMethodMember, 
            IIntermediateStructMethodMember, IStructPropertyMember, IIntermediateStructPropertyMember,
            IGeneralGenericTypeUniqueIdentifier, IStructType, IIntermediateStructType>,
        IIntermediateGenericType<IGeneralGenericTypeUniqueIdentifier, IStructType, IIntermediateStructType>,
        IStructType
    {
        /// <summary>
        /// Returns the <see cref="IIntermediateAssembly"/> in which
        /// the <see cref="IIntermediateStructType"/> is declared
        /// </summary>
#if DEBUG
        [VisitorImplementationIgnoreProperty]
#endif
        new IIntermediateAssembly Assembly { get; }

        /// <summary>
        /// Returns the <see cref="IIntermediateStructImplementedInterfaces"/>
        /// which represents the interfaces implemented by the current 
        /// <see cref="IIntermediateStructType"/>.
        /// </summary>
        new IIntermediateStructImplementedInterfaces ImplementedInterfaces { get; }
    }
}
