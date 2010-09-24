using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
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
    /// delegate type declaration.
    /// </summary>
    public interface IIntermediateDelegateType :
        IIntermediateParameterParent<IDelegateType, IIntermediateDelegateType, IDelegateTypeParameterMember, IIntermediateDelegateTypeParameterMember>,
        IIntermediateGenericType<IDelegateType, IIntermediateDelegateType>,
        IDelegateType
    {
        /// <summary>
        /// Returns the <see cref="IType"/> that the <see cref="IDelegateType"/> returns.
        /// </summary>
        new IType ReturnType { get; }
        /// <summary>
        /// Returns whether the <see cref="IIntermediateDelegateType"/>'s last parameter
        /// is a parameter array.
        /// </summary>
        new bool LastIsParams { get; set; }
        /// <summary>
        /// Returns the parameter set associated to the <see cref="IIntermediateDelegateType"/>.
        /// </summary>
        new IIntermediateDelegateTypeParameterDictionary Parameters { get; }
        /// <summary>
        /// Returns the <see cref="IIntermediateAssembly"/> in which
        /// the <see cref="IIntermediateDelegateType"/> is declared
        /// </summary>
        new IIntermediateAssembly Assembly { get; }
    }
}
