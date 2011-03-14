using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Oil.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
{
    partial class IntermediateGenericTypeBase<TType, TIntermediateType>
        where TType :
            class,
            IGenericType<TType>
        where TIntermediateType :
            class,
            TType,
            IIntermediateGenericType<TType, TIntermediateType>
    {
        protected partial class TypeParameterDictionary
        {
            private class TypeParameter :
                IntermediateGenericTypeParameterBase<TType, TIntermediateType>,
                IIntermediateGenericTypeParameter<TType, TIntermediateType>
            {
                /// <summary>
                /// Creates a new <see cref="TypeParameter"/> instance
                /// with the <paramref name="name"/> and <paramref name="parent"/> provided.
                /// </summary>
                /// <param name="name">The <see cref="String"/> representing the unique
                /// name of the <see cref="TypeParameter"/>.</param>
                /// <param name="parent">The <see cref="IntermediateGenericTypeBase{TType, TIntermediateType}"/> 
                /// which contains the <see cref="TypeParameter"/>.</param>
                public TypeParameter(string name, IntermediateGenericTypeBase<TType, TIntermediateType> parent)
                    : base(name, (TIntermediateType)(object)parent)
                {
                }
            }
        }
    }
}
