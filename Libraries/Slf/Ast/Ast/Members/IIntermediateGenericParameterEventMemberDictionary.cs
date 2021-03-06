﻿using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Members
{
    public interface IIntermediateGenericParameterEventMemberDictionary<TGenericParameter, TIntermediateGenericParameter> :
        IIntermediateEventSignatureMemberDictionary<IGenericParameterEventMember<TGenericParameter>, IIntermediateGenericParameterEventMember<TGenericParameter, TIntermediateGenericParameter>, TGenericParameter, TIntermediateGenericParameter>,
        IGenericParameterEventMemberDictionary<TGenericParameter>
        where TGenericParameter :
            IGenericParameter<TGenericParameter>
        where TIntermediateGenericParameter :
            IIntermediateGenericParameter<TGenericParameter, TIntermediateGenericParameter>,
            TGenericParameter
    {
        /// <summary>
        /// Returns the <typeparamref name="TIntermediateGenericParameter"/> which owns the <see cref="IIntermediateGenericParameterEventMember{TGenericParameter, TIntermediateGenericParameter}"/> series.
        /// </summary>
        new TIntermediateGenericParameter Parent { get; }
    }
    public interface IIntermediateGenericParameterEventMemberDictionary :
        IIntermediateEventSignatureMemberDictionary,
        IGenericParameterEventMemberDictionary
    {
        /// <summary>
        /// Returns the <see cref="IIntermediateGenericParameter"/> which owns the <see cref="IIntermediateGenericParameterEventMember"/> series.
        /// </summary>
        new IIntermediateGenericParameter Parent { get; }
    }
}
