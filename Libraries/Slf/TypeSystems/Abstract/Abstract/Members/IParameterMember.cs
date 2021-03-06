﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Abstract.Members
{
    /// <summary>
    /// Defines generic properties and methods for working with a parameter.
    /// </summary>
    /// <typeparam name="TParent">The type of parent that contains the <see cref="IParameterMember{TParent}"/> instances
    /// in the current implementation.</typeparam>
    public interface IParameterMember<TParent> :
        IMember<IGeneralMemberUniqueIdentifier, TParent>,
        IParameterMember
        where TParent :
            IParameterParent
    {
    }
    /// <summary>
    /// Defines properties and methods for working with a parameter member.
    /// </summary>
    public interface IParameterMember :
        IMetadataEntity,
        IMember
    {
        /// <summary>
        /// Returns the parent of the <see cref="IParameterMember"/>.
        /// </summary>
        new IParameterParent Parent { get; }
        /// <summary>
        /// Returns the type that the <see cref="IParameterMember"/> is defined as.
        /// </summary>
        IType ParameterType { get; }
        /// <summary>
        /// Returns the direction the parameter is coerced.
        /// </summary>
        ParameterCoercionDirection Direction { get; }
    }
}
