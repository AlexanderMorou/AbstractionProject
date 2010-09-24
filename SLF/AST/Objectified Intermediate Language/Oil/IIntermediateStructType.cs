using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Oil.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
{
    /// <summary>
    /// Defines properties and methods for working with an intermediate
    /// data structure type declaration.
    /// </summary>
    public interface IIntermediateStructType :
        IIntermediateGenericType<IStructType, IIntermediateStructType>,
        IIntermediateInstantiableType<IStructCtorMember, IIntermediateStructCtorMember, IStructEventMember, 
            IIntermediateStructEventMember, IStructFieldMember, IIntermediateStructFieldMember, 
            IStructIndexerMember, IIntermediateStructIndexerMember, IStructMethodMember, 
            IIntermediateStructMethodMember, IStructPropertyMember, IIntermediateStructPropertyMember, 
            IStructType, IIntermediateStructType>,
        IStructType
    {
        /// <summary>
        /// Returns the <see cref="IIntermediateAssembly"/> in which
        /// the <see cref="IIntermediateStructType"/> is declared
        /// </summary>
        new IIntermediateAssembly Assembly { get; }
    }
}
