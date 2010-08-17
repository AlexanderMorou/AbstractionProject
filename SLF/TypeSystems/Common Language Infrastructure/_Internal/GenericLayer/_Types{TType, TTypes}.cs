using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.GenericLayer
{
    /// <summary>
    /// Provides an internatl generic type collection for common type 
    /// collections to derive from.
    /// </summary>
    /// <typeparam name="TType">The type of <see cref="IType{TType}"/>
    /// the <see cref="_Types{TType, TTypes}"/> contains.</typeparam>
    /// <typeparam name="TTypes">The type of dictionary which
    /// contains the original set to reference for generic
    /// instantiations.</typeparam>
    internal abstract class _Types<TType, TTypes> :
        _GroupedDeclarations<TType, IGenericType, IType, TTypes>
        where TType :
            class,
            IType<TType>
        where TTypes :
            class,
            ISubordinateDictionary<string, TType, IType>,
            IGroupedDeclarationDictionary<TType>
    {
        public _Types(_FullTypesBase master, TTypes originalSet, IGenericType parent)
            : base(master, originalSet, parent)
        {
        }
    }
}
