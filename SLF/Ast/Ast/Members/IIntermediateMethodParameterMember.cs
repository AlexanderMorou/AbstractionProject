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
    /// Defines properties and methods for working with an intermediate method parameter
    /// member.
    /// </summary>
    /// <typeparam name="TMethod">The type of method in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateMethod">The type of the method in the intermediate
    /// abstract syntax tree.</typeparam>
    /// <typeparam name="TMethodParent">The type which contains a series of the <typeparamref name="TMethod"/>
    /// in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateMethodParent">The type which contains a series of <typeparamref name="TIntermediateMethod"/>
    /// in the intermediate abstract syntax tree.</typeparam>
    public interface IIntermediateMethodParameterMember<TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent> :
        IIntermediateMethodSignatureParameterMember<IMethodParameterMember<TMethod, TMethodParent>, IIntermediateMethodParameterMember<TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent>, TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent>,
        IIntermediateMethodParameterMember,
        IMethodParameterMember<TMethod, TMethodParent>
        where TMethod :
            IMethodMember<TMethod, TMethodParent>
        where TIntermediateMethod :
            TMethod,
            IIntermediateMethodMember<TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent>
        where TMethodParent :
            IMethodParent<TMethod, TMethodParent>
        where TIntermediateMethodParent :
            TMethodParent,
            IIntermediateMethodParent<TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent>
    {
    }
    /// <summary>
    /// Defines properties and methods for working with an intermediate
    /// method parameter member.
    /// </summary>
    public interface IIntermediateMethodParameterMember :
        IIntermediateSignatureParameterMember,
        IMethodParameterMember
    {
    }
}