using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Ast.Statements;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Ast.Members
{
    /// <summary>
    /// Defines generic properties and methods for working with a type-coercion member.
    /// </summary>
    /// <typeparam name="TCoercionParent">The type of parent that contains the 
    /// type coercion member in abstract type system.</typeparam>
    /// <typeparam name="TIntermediateCoercionParent">The type of parent that contains the 
    /// type coercion member in intermediate abstract syntax tree.</typeparam>
    public interface IIntermediateTypeCoercionMember<TCoercionParent, TIntermediateCoercionParent> :
        IIntermediateCoercionMember<ITypeCoercionUniqueIdentifier, ITypeCoercionMember<TCoercionParent>, IIntermediateTypeCoercionMember<TCoercionParent, TIntermediateCoercionParent>, TCoercionParent, TIntermediateCoercionParent>,
        IIntermediateTypeCoercionMember,
        ITypeCoercionMember<TCoercionParent>
        where TCoercionParent :
            ICoercibleType<ITypeCoercionUniqueIdentifier, ITypeCoercionMember<TCoercionParent>, TCoercionParent>
        where TIntermediateCoercionParent :
            IIntermediateCoercibleType<ITypeCoercionUniqueIdentifier, ITypeCoercionMember<TCoercionParent>, IIntermediateTypeCoercionMember<TCoercionParent, TIntermediateCoercionParent>, TCoercionParent, TIntermediateCoercionParent>,
            TCoercionParent
    {
        /// <summary>
        /// Returns the parent of the <see cref="IIntermediateTypeCoercionMember{TCoercionParent, TIntermediateCoercionParent}"/>.
        /// </summary>
        new TIntermediateCoercionParent Parent { get; }
    }
    /// <summary>
    /// Defines properties and methods for working with a type-coercion member.
    /// </summary>
    public interface IIntermediateTypeCoercionMember : 
        ITopBlockStatement,
        IIntermediateCoercionMember,
        ITypeCoercionMember
    {
        /// <summary>
        /// Returns the <see cref="ILocalMember"/> which denotes the parameter
        /// within the model that the input of the coercion.
        /// </summary>
        ILocalMember Incoming { get; }
        /// <summary>
        /// Returns/sets whether the conversion overload is implicit or explicit.
        /// </summary>
        new TypeConversionRequirement Requirement { get; set; }
        /// <summary>
        /// Returns/sets whether the conversion overload is from the containing type or 
        /// to the containing type.
        /// </summary>
        new TypeConversionDirection Direction { get; set; }
        /// <summary>
        /// Returns/sets the type which is coerced by the overload.
        /// </summary>
        /// <exception cref="System.ArgumentException">thrown when <paramref name="value"/> is an interface.</exception>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="value"/> is null.</exception>
        new IType CoercionType { get; set; }
    }
}
