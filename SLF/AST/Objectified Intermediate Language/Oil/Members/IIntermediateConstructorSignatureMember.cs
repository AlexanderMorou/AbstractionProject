﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;

namespace AllenCopeland.Abstraction.Slf.Oil.Members
{
    /// <summary>
    /// Defines generic properties and methods for working with an intermediate constructor member which functions
    /// as a simple signature of a constructor.
    /// </summary>
    /// <typeparam name="TCtor">The type of <see cref="IConstructorMember{TCtor, TCtorParent}"/>
    /// in the current implementation.</typeparam>
    /// <typeparam name="TIntermediateCtor">The type of
    /// <see cref="IIntermediateConstructorSignatureMember{TCtor, TIntermediateCtor, TType, TIntermediateType}"/>
    /// in the current implementation.</typeparam>
    /// <typeparam name="TType">The type of <see cref="ICreatableType{TCtor, TType}"/>
    /// that contains the <typeparamref name="TCtor"/> instances.</typeparam>
    /// <typeparam name="TIntermediateType">The type of 
    /// <see cref="IIntermediateCreatableSignatureType{TCtor, TIntermediateCtor, TType, TIntermediateType}"/>
    /// which contains the <typeparamref name="TIntermediateType"/></typeparam>
    public interface IIntermediateConstructorSignatureMember<TCtor, TIntermediateCtor, TType, TIntermediateType> :
        IIntermediateSignatureMember<TCtor, TIntermediateCtor, IConstructorParameterMember<TCtor, TType>, IIntermediateConstructorSignatureParameterMember<TCtor, TIntermediateCtor, TType, TIntermediateType>, TType, TIntermediateType>,
        IIntermediateMember<TType, TIntermediateType>,
        IIntermediateConstructorSignatureMember,
        IConstructorMember<TCtor, TType>
        where TCtor :
            IConstructorMember<TCtor, TType>
        where TIntermediateCtor :
            TCtor,
            IIntermediateConstructorSignatureMember<TCtor, TIntermediateCtor, TType, TIntermediateType>
        where TType :
            ICreatableType<TCtor, TType>
        where TIntermediateType :
            TType,
            IIntermediateCreatableSignatureType<TCtor, TIntermediateCtor, TType, TIntermediateType>
    {
    }
    /// <summary>
    /// Defines properties and methods for working with an intermediate constructor member which functions
    /// as a simple signature of a constructor.
    /// </summary>
    public interface IIntermediateConstructorSignatureMember :
        IIntermediateSignatureMember,
        IIntermediateMember,
        IIntermediateScopedDeclaration,
        IConstructorMember
    {
    }
}