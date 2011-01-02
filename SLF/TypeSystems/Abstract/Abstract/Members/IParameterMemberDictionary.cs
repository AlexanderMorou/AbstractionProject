using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Abstract.Members
{
    /// <summary>
    /// Defines generic properties and methods for working with a dictionary of 
    /// <typeparamref name="TParameter"/> instances.
    /// </summary>
    /// <typeparam name="TParent">The type of parent that contains the <typeparamref name="TParameter"/> instances
    /// in the current implementation.</typeparam>
    /// <typeparam name="TParameter">The type of <see cref="IParameterMember{TParent}"/> which is contained by the 
    /// <typeparamref name="TParent"/> in the current implementation.</typeparam>
    public interface IParameterMemberDictionary<TParent, TParameter> :
        IMemberDictionary<TParent, TParameter>
        where TParent :
            IParameterParent<TParent, TParameter>
        where TParameter :
            IParameterMember<TParent>
    {
    }
    /// <summary>
    /// Defines properties and methods for working with a dictionary of <see cref="IParameterMember"/> 
    /// instances.
    /// </summary>
    public interface IParameterMemberDictionary :
        IMemberDictionary
    {
        /// <summary>
        /// Returns the <see cref="IParameterParent"/> which owns the <see cref="IParameterMember"/> series.
        /// </summary>
        new IParameterParent Parent { get; }
    }
}
