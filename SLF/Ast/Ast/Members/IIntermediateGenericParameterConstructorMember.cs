using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Members
{
    /// <summary>
    /// Defines generic properties and methods for working with an intermediate
    /// generic parameter's constructor member.
    /// </summary>
    /// <typeparam name="TGenericParameter"></typeparam>
    /// <typeparam name="TIntermediateGenericParameter"></typeparam>
    public interface IIntermediateGenericParameterConstructorMember<TGenericParameter, TIntermediateGenericParameter> :
        IIntermediateConstructorSignatureMember<IGenericParameterConstructorMember<TGenericParameter>, IIntermediateGenericParameterConstructorMember<TGenericParameter, TIntermediateGenericParameter>, TGenericParameter, TIntermediateGenericParameter>,
        IGenericParameterConstructorMember<TGenericParameter>,
        IIntermediateGenericParameterConstructorMember
        where TGenericParameter :
            IGenericParameter<TGenericParameter>
        where TIntermediateGenericParameter :
            TGenericParameter,
            IIntermediateGenericParameter<TGenericParameter, TIntermediateGenericParameter>
    {
    }
    /// <summary>
    /// Defines properties and methods for working with an intermediate
    /// generic parameter's constructor member.
    /// </summary>
    public interface IIntermediateGenericParameterConstructorMember :
        IIntermediateConstructorSignatureMember,
        IGenericParameterConstructorMember
    {
        /// <summary>
        /// Returns <see cref="AccessLevelModifiers.Public"/>;
        /// additionally,
        /// </summary>
        /// <exception cref="System.InvalidOperationException">The access level cannot be set on generic parameter
        /// constructors; occurs when set through <see cref="IIntermediateScopedDeclaration.AccessLevel"/>.</exception>
        new AccessLevelModifiers AccessLevel { get; }
    }
}
