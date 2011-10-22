using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Oil.Modules;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
{
    public interface IIntermediateGenericTypeDictionary<TTypeIdentifier, TType, TIntermediateType> :
        IIntermediateTypeDictionary<TTypeIdentifier, TType, TIntermediateType>
        where TTypeIdentifier :
            IGenericTypeUniqueIdentifier<TTypeIdentifier>
        where TType :
            IGenericType<TTypeIdentifier, TType>
        where TIntermediateType :
            IIntermediateGenericType<TTypeIdentifier, TType, TIntermediateType>,
            TType
    {
        /// <summary>
        /// Creates and adds a new <typeparamref name="TIntermediateType"/> 
        /// instance with the <paramref name="name"/> and
        /// <paramref name="typeParameters"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> name of the new
        /// <typeparamref name="TIntermediateType"/>.</param>
        /// <param name="typeParameters">The <see cref="GenericParameterData"/>
        /// series which denotes information about the generic parameters
        /// contained within the new <typeparamref name="TIntermediateType"/>.</param>
        /// <returns>A new <typeparamref name="TIntermediateType"/>, if successful.</returns>
        /// <exception cref="System.ArgumentException">thrown when a type by the <paramref name="name"/>
        /// provided already exists in the containing type parent, or <paramref name="name"/>
        /// is <see cref="String.Empty"/>.</exception>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="name"/> is null.</exception>
        TIntermediateType Add(string name, params GenericParameterData[] typeParameters);
        /// <summary>
        /// Creates and inserts a new <typeparamref name="TIntermediateType"/> instance
        /// into the current <see cref="IIntermediateTypeDictionary{TTypeIdentifier, TType, TIntermediateType}"/>
        /// with the <paramref name="name"/>, <paramref name="module"/> and
        /// <paramref name="typeParameters"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> that defines the name of the
        /// new <typeparamref name="TIntermediateType"/> to create.</param>
        /// <param name="module">The <see cref="IIntermediateModule"/> to add the 
        /// new <typeparamref name="TIntermediateType"/> to.</param>
        /// <param name="typeParameters">The <see cref="GenericParameterData"/>
        /// series which denotes information about the generic parameters
        /// contained within the new <typeparamref name="TIntermediateType"/>.</param>
        /// <returns>A new <typeparamref name="TIntermediateType"/> instance.</returns>
        /// <remarks>Using this method creates a new partial instance 
        /// of the assembly to ensure the class can be translated into
        /// a file of its own.</remarks>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="name"/>, or <paramref name="module"/>, is null.</exception>
        /// <exception cref="System.ArgumentException">thrown when a type by the <paramref name="name"/>
        /// provided already exists in the containing type parent, or <paramref name="name"/> 
        /// is <see cref="String.Empty"/>.</exception>
        TIntermediateType Add(string name, IIntermediateModule module, params GenericParameterData[] typeParameters);
    }
}
