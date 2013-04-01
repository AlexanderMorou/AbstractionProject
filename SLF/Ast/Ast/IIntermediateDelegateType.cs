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
    /// Defines properties and methods for working with an intermediate
    /// delegate type declaration.
    /// </summary>
    public interface IIntermediateDelegateType :
        IIntermediateParameterParent<IDelegateType, IIntermediateDelegateType, IDelegateTypeParameterMember, IIntermediateDelegateTypeParameterMember>,
        IIntermediateGenericType<IGeneralGenericTypeUniqueIdentifier, IDelegateType, IIntermediateDelegateType>,
        IDelegateType
    {
        /// <summary>
        /// Returns/sets the <see cref="IType"/> that the <see cref="IDelegateType"/> returns.
        /// </summary>
        new IType ReturnType { get; set; }
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
        /// <summary>
        /// Returns the <see cref="IIntermediateDelegateTypeParameterTypeDictionary"/> which
        /// denotes the type-parameters of the delegate type.
        /// </summary>
        new IIntermediateDelegateTypeParameterTypeDictionary TypeParameters { get; }
    }
}
