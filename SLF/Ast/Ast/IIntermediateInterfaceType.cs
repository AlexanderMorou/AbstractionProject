using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Ast.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast
{
    /// <summary>
    /// Defines properties and methods for working with an 
    /// intermediate interface type.
    /// </summary>
    public interface IIntermediateInterfaceType :
        IIntermediateMethodSignatureParent<IInterfaceMethodMember, IIntermediateInterfaceMethodMember, IInterfaceType, IIntermediateInterfaceType>,
        IIntermediatePropertySignatureParent<IInterfacePropertyMember, IIntermediateInterfacePropertyMember, IInterfaceType, IIntermediateInterfaceType>,
        IIntermediateEventSignatureParent<IInterfaceEventMember, IIntermediateInterfaceEventMember, IInterfaceType, IIntermediateInterfaceType>,
        IIntermediateIndexerSignatureParent<IInterfaceIndexerMember, IIntermediateInterfaceIndexerMember, IInterfaceType, IIntermediateInterfaceType>,
        IIntermediateGenericType<IGeneralGenericTypeUniqueIdentifier, IInterfaceType, IIntermediateInterfaceType>,
        IIntermediateSegmentableType<IGeneralGenericTypeUniqueIdentifier, IInterfaceType, IIntermediateInterfaceType>,
        IIntermediateTypeParent,
        IInterfaceType
    {
        /// <summary>
        /// Suspends the duality in the type layout where members 
        /// inserted in methods, properties, events and so on are 
        /// dually inserted in a verbatim-order master set.
        /// </summary>
        /// <remarks>Incremental function, all resumes must
        /// be invoked prior to resuming the duality.</remarks>
        void SuspendDualLayout();
        /// <summary>
        /// Resumes the duality in the type layout where members
        /// inserted in methods, properties, events, and so on are
        /// dually inserted in a verbatim-order master set.
        /// </summary>
        /// <remarks>Incremental function, all resumes must
        /// be invoked prior to resuming the duality.</remarks>
        void ResumeDualLayout();
        /// <summary>
        /// The <see cref="ITypeCollection"/> which represents the interfaces implemented by the
        /// <see cref="IIntermediateInterfaceType"/>.
        /// </summary>
        new ITypeCollection ImplementedInterfaces { get; }
        /// <summary>
        /// Returns the <see cref="ITypeIdentityManager"/> which is responsible for marshalling
        /// type identities in the current type model.
        /// </summary>
        new ITypeIdentityManager IdentityManager { get; }
        /// <summary>
        /// Returns the <see cref="IIntermediateAssembly"/> in which
        /// the <see cref="IIntermediateInterfaceType"/> is declared
        /// </summary>
        new IIntermediateAssembly Assembly { get; }

    }
    /* *
     *    /// <summary>
     *    /// Returns the <see cref="ITypeCollection"/> of
     *    /// <see cref="IType"/>s which the 
     *    /// <see cref="IIntermediateInterfaceType"/> implements.
     *    /// </summary>
     *    ITypeCollection ImplementedInterfaces { get; }
     * */
}
