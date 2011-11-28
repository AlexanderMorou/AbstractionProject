using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Members
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
    /// <typeparam name="TType">The type of <see cref="ICreatableParent{TCtor, TCtorParent}"/>
    /// that contains the <typeparamref name="TCtor"/> instances.</typeparam>
    /// <typeparam name="TIntermediateType">The type of 
    /// <see cref="IIntermediateCreatableSignatureParent{TCtor, TIntermediateCtor, TType, TIntermediateType}"/>
    /// which contains the <typeparamref name="TIntermediateCtor"/>.</typeparam>
    public interface IIntermediateConstructorSignatureMember<TCtor, TIntermediateCtor, TType, TIntermediateType> :
        IIntermediateSignatureMember<IGeneralSignatureMemberUniqueIdentifier, TCtor, TIntermediateCtor, IConstructorParameterMember<TCtor, TType>, IIntermediateConstructorSignatureParameterMember<TCtor, TIntermediateCtor, TType, TIntermediateType>, TType, TIntermediateType>,
        IIntermediateMember<IGeneralSignatureMemberUniqueIdentifier, TType, TIntermediateType>,
        IIntermediateConstructorSignatureMember,
        IConstructorMember<TCtor, TType>
        where TCtor :
            IConstructorMember<TCtor, TType>
        where TIntermediateCtor :
            TCtor,
            IIntermediateConstructorSignatureMember<TCtor, TIntermediateCtor, TType, TIntermediateType>
        where TType :
            ICreatableParent<TCtor, TType>
        where TIntermediateType :
            TType,
            IIntermediateCreatableSignatureParent<TCtor, TIntermediateCtor, TType, TIntermediateType>
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
