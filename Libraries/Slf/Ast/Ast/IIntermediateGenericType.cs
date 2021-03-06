using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using AllenCopeland.Abstraction.Utilities.Events;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast
{
    /// <summary>
    /// Defines generic properties and methods for working with an <see cref="IIntermediateType"/>
    /// which contains generic parameters.
    /// </summary>
    /// <typeparam name="TTypeIdentifier">The kind of type identifier used
    /// to differentiate the <typeparamref name="TIntermediateType"/>
    /// instance from its siblings.</typeparam>
    /// <typeparam name="TType">The generic type as it exists 
    /// in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateType">The type of generic type as it
    /// exists in the intermediate abstract syntax tree.</typeparam>
    public interface IIntermediateGenericType<TTypeIdentifier, TType, TIntermediateType> :
        IIntermediateType<TTypeIdentifier, TType, TIntermediateType>,
        IIntermediateGenericParameterParent<IGenericTypeParameter<TTypeIdentifier, TType>, IIntermediateGenericTypeParameter<TTypeIdentifier, TType, TIntermediateType>, TType, TIntermediateType>,
        IIntermediateGenericType,
        IGenericType<TTypeIdentifier, TType>
        where TTypeIdentifier :
            IGenericTypeUniqueIdentifier,
            IGeneralDeclarationUniqueIdentifier
        where TType :
            IGenericType<TTypeIdentifier, TType>
        where TIntermediateType :
            IIntermediateGenericType<TTypeIdentifier, TType, TIntermediateType>,
            TType
    {
    }

    /// <summary>
    /// Defines properties and methods for working with an <see cref="IIntermediateType"/>
    /// which contains generic parameters.
    /// </summary>
    public interface IIntermediateGenericType :
        IIntermediateType,
        IIntermediateGenericParameterParent,
        IGenericType
    {
    }
}
