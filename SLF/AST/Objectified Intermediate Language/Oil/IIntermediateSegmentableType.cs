using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
{
    /// <summary>
    /// Defines generic properties and methods for working with an intermediate
    /// type which can span multiple instances.
    /// </summary>
    /// <typeparam name="TType">The type of <see cref="IType{TTypeIdentifier, TType}"/> in the
    /// abstract type sytem.</typeparam>
    /// <typeparam name="TIntermediateType">The type of <see cref="IIntermediateType"/>
    /// which needs segmentation functionality.</typeparam>
    public interface IIntermediateSegmentableType<TTypeIdentifier, TType, TIntermediateType> :
        IIntermediateSegmentableDeclaration<TTypeIdentifier, TIntermediateType>,
        IIntermediateType,
        IType<TTypeIdentifier, TType>,
        IIntermediateSegmentableType
        where TTypeIdentifier :
            ITypeUniqueIdentifier<TTypeIdentifier>
        where TType :
            IType<TTypeIdentifier, TType>
        where TIntermediateType :
            IIntermediateSegmentableType<TTypeIdentifier, TType, TIntermediateType>,
            TType
    {

    }

    /// <summary>
    /// Defines generic properties and methods for working with an intermediate
    /// type which can span multiple instances.
    /// </summary>
    /// <example>Classes, Interfaces, and Structs.</example>
    public interface IIntermediateSegmentableType :
        IIntermediateType,
        IIntermediateSegmentableDeclaration
    {
    }
}
