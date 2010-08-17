using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.Abstract.Members
{
    internal abstract partial class GenericParameterConstructorMemberBase<TGenericParameter> :
        ConstructorMemberBase<IGenericParameterConstructorMember<TGenericParameter>, TGenericParameter>,
        IGenericParameterConstructorMember<TGenericParameter>
        where TGenericParameter :
            IGenericParameter<TGenericParameter>
    {
        /// <summary>
        /// Creates a new <see cref="GenericParameterConstructorMemberBase{TGenericParameter}"/>
        /// instance with the <paramref name="parent"/>
        /// provided.
        /// </summary>
        /// <param name="parent">The <typeparamref name="TGenericParameter"/> 
        /// to which the <see cref="GenericParameterConstructorMemberBase{TGenericParameter}"/> belongs.</param>
        public GenericParameterConstructorMemberBase(TGenericParameter parent)
            : base(parent)
        {
        }
    }
}
