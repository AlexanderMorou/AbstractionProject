using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Abstract.Members
{
    /// <summary>
    /// Defines generic properties and methods for working with a dictionary of 
    /// constructor members defined on a generic parameter member.
    /// </summary>
    /// <typeparam name="TGenericParameter">The type of generic parameter member 
    /// in the current implementation.</typeparam>
    public interface IGenericParameterConstructorMemberDictionary<TGenericParameter> :
        IConstructorMemberDictionary<IGenericParameterConstructorMember<TGenericParameter>, TGenericParameter>
        where TGenericParameter :
            IGenericParameter<TGenericParameter>
    {
    }
    /// <summary>
    /// Defines properties and methods for working with a series of constructor
    /// members for a generic parameter.
    /// </summary>
    public interface IGenericParameterConstructorMemberDictionary :
        IMemberDictionary
    {
        /// <summary>
        /// Returns the <see cref="IGenericParameter"/> which owns the <see cref="IGenericParameterConstructorMember"/> series.
        /// </summary>
        new IGenericParameter Parent { get; }
    }
}
