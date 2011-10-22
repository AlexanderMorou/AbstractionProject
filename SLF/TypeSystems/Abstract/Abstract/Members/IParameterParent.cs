using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Abstract.Members
{
    /// <summary>
    /// Defines properties and methods for working with a <see cref="IParameterParent{TParent, TParameter}"/>
    /// which contains a series of <typeparamref name="TParameter"/> instances.
    /// </summary>
    /// <typeparam name="TParent">The type of parent that contains the <typeparamref name="TParameter"/> instances
    /// in the current implementation.</typeparam>
    /// <typeparam name="TParameter">The type of <see cref="IParameterMember{TParent}"/> which is contained by the 
    /// <typeparamref name="TParent"/> in the current implementation.</typeparam>
    public interface IParameterParent<TParent, TParameter> :
        IParameterParent
        where TParent :
            IParameterParent<TParent, TParameter>
        where TParameter :
            IParameterMember<TParent>
    {
        /// <summary>
        /// Returns the dictionary of <typeparamref name="TParameter"/> instances for the current <typeparamref name="TParent"/>
        /// implementation.
        /// </summary>
        new IParameterMemberDictionary<TParent, TParameter> Parameters { get; }
    }

    /// <summary>
    /// Defines properties and methods for working with the parent of one or more
    /// <see cref="IParameterMember"/> instances.
    /// </summary>
    public interface IParameterParent :
        IMemberParent
    {
        /// <summary>
        /// Returns the dictionary of <see cref="IParameterMember"/> instances for the current <see cref="IParameterParent"/>
        /// implementation.
        /// </summary>
        IParameterMemberDictionary Parameters { get; }
        /// <summary>
        /// Returns whether the last <see cref="ISignatureParameterMember"/> of the
        /// <see cref="ISignatureMember"/> is a params parameter.
        /// </summary>
        bool LastIsParams { get; }
    }
}
