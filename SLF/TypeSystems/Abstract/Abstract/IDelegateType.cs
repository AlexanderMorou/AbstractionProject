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
    /// Defines properties and methods for working with a delegate type that acts as the signature of a
    /// pointer to a function.
    /// </summary>
    public interface IDelegateType :
        IParameterParent<IDelegateType, IDelegateTypeParameterMember>,
        IGenericType<IDelegateType>,
        IReferenceType
    {
        /// <summary>
        /// Returns the <see cref="IType"/> that the <see cref="IDelegateType"/> returns.
        /// </summary>
        IType ReturnType { get; }
        /// <summary>
        /// Returns the <see cref="IDelegateTypeParameterDictionary"/>
        /// of the <see cref="IDelegateType"/>.
        /// </summary>
        new IDelegateTypeParameterDictionary Parameters { get; }
    }
}
