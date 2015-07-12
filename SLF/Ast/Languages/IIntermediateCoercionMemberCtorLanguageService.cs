using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Languages
{
    /// <summary>
    /// Defines properties and methods for working with a language service which
    /// provides intermediate binary operator coercion member instances.
    /// </summary>
    /// <typeparam name="TCoercionParent">The type which contains the binary operation
    /// coercion member in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateCoercionParent">The type which contains the
    /// intermediate binary operation coercion member in the intermediate abstract
    /// syntax tree.</typeparam>
    public interface IIntermediateBinaryOperatorCoercionMemberCtorLanguageService<TCoercionParent, TIntermediateCoercionParent> :
        IIntermediateMemberCtorLanguageService<IIntermediateBinaryOperatorCoercionMember<TCoercionParent, TIntermediateCoercionParent>>
        where TCoercionParent :
            ICoercibleType<IBinaryOperatorUniqueIdentifier, IBinaryOperatorCoercionMember<TCoercionParent>, TCoercionParent>
        where TIntermediateCoercionParent :
            IIntermediateCoercibleType<IBinaryOperatorUniqueIdentifier, IBinaryOperatorCoercionMember<TCoercionParent>, IIntermediateBinaryOperatorCoercionMember<TCoercionParent, TIntermediateCoercionParent>, TCoercionParent, TIntermediateCoercionParent>,
            TCoercionParent
    {
    }
    /// <summary>
    /// Defines properties and methods for working with a language service which
    /// provides intermediate type coercion member instances.
    /// </summary>
    /// <typeparam name="TCoercionParent">The type of parent that contains the 
    /// type coercion member in abstract type system.</typeparam>
    /// <typeparam name="TIntermediateCoercionParent">The type of parent that
    /// contains the  type coercion member in intermediate abstract syntax
    /// tree.</typeparam>
    public interface IIntermediateTypeCoercionMemberCtorLanguageService<TCoercionParent, TIntermediateCoercionParent> :
        IIntermediateMemberCtorLanguageService<IIntermediateTypeCoercionMember<TCoercionParent, TIntermediateCoercionParent>>
        where TCoercionParent :
            ICoercibleType<ITypeCoercionUniqueIdentifier, ITypeCoercionMember<TCoercionParent>, TCoercionParent>
        where TIntermediateCoercionParent :
            IIntermediateCoercibleType<ITypeCoercionUniqueIdentifier, ITypeCoercionMember<TCoercionParent>, IIntermediateTypeCoercionMember<TCoercionParent, TIntermediateCoercionParent>, TCoercionParent, TIntermediateCoercionParent>,
            TCoercionParent
    {
    }
    /// <summary>
    /// Defines properties and methods for working with a language service which
    /// provides intermediate unary operator coercion member instances.
    /// </summary>
    /// <typeparam name="TCoercionParent">The type of parent that contains the 
    /// unary operation coercion member in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateCoercionParent">The type of parent that
    /// contains the unary operation coercion member in the intermediate abstract
    /// syntax tree.</typeparam>
    public interface IIntermediateUnaryOperatorCoercionMemberCtorLanguageService<TCoercionParent, TIntermediateCoercionParent> :
        IIntermediateMemberCtorLanguageService<IIntermediateUnaryOperatorCoercionMember<TCoercionParent, TIntermediateCoercionParent>>
        where TCoercionParent :
            ICoercibleType<IUnaryOperatorUniqueIdentifier, IUnaryOperatorCoercionMember<TCoercionParent>, TCoercionParent>
        where TIntermediateCoercionParent :
            IIntermediateCoercibleType<IUnaryOperatorUniqueIdentifier, IUnaryOperatorCoercionMember<TCoercionParent>, IIntermediateUnaryOperatorCoercionMember<TCoercionParent, TIntermediateCoercionParent>, TCoercionParent, TIntermediateCoercionParent>,
            TCoercionParent
    {
    }
}
