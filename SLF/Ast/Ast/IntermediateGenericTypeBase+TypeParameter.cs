using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Ast.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast
{
    partial class IntermediateGenericTypeBase<TTypeIdentifier, TType, TIntermediateType>
        where TTypeIdentifier :
            IGenericTypeUniqueIdentifier,
            IGeneralDeclarationUniqueIdentifier
        where TType :
            class,
            IGenericType<TTypeIdentifier, TType>
        where TIntermediateType :
            class,
            IIntermediateGenericType<TTypeIdentifier, TType, TIntermediateType>,
            TType
    {
        partial class TypeParameterDictionary
        {
            protected internal class TypeParameter :
                IntermediateGenericTypeParameterBase<TTypeIdentifier, TType, TIntermediateType>,
                IIntermediateGenericTypeParameter<TTypeIdentifier, TType, TIntermediateType>
            {
                /// <summary>
                /// Creates a new <see cref="TypeParameter"/> instance
                /// with the <paramref name="name"/> and <paramref name="parent"/> provided.
                /// </summary>
                /// <param name="name">The <see cref="String"/> representing the unique
                /// name of the <see cref="TypeParameter"/>.</param>
                /// <param name="parent">The <see cref="IntermediateGenericTypeBase{TTypeIdentifier, TType, TIntermediateType}"/> 
                /// which contains the <see cref="TypeParameter"/>.</param>
                public TypeParameter(string name, IntermediateGenericTypeBase<TTypeIdentifier, TType, TIntermediateType> parent)
                    : base(name, (TIntermediateType)(object)parent)
                {
                }
            }
        }
    }
}
