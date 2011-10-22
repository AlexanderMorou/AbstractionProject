using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Oil.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
{
    /// <summary>
    /// Defines properties and methods for working with the parent of a series of
    /// property signatures.
    /// </summary>
    /// <typeparam name="TProperty">The type of property signature used in the
    /// abstract type system.</typeparam>
    /// <typeparam name="TIntermediateProperty">The type of property signature used in the intermediate
    /// abstract syntax tree.</typeparam>
    /// <typeparam name="TPropertyParent">The type which acts as the parent of the properties
    /// in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediatePropertyParent">The type which acts as the parent of the 
    /// properties in the intermediate abstract syntax tree.</typeparam>
    public interface IIntermediatePropertySignatureParent<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent> :
        IIntermediateType,
        IIntermediatePropertySignatureParent,
        IPropertySignatureParent<TProperty, TPropertyParent>
        where TProperty :
            IPropertySignatureMember<TProperty, TPropertyParent>
        where TIntermediateProperty :
            TProperty,
            IIntermediatePropertySignatureMember<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent>
        where TPropertyParent :
            IPropertySignatureParent<TProperty, TPropertyParent>
        where TIntermediatePropertyParent :
            TPropertyParent,
            IIntermediatePropertySignatureParent<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent>
    {
        /// <summary>
        /// Returns the <see cref="IIntermediatePropertySignatureMemberDictionary{TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent}"/> contained on the 
        /// current <see cref="IIntermediatePropertySignatureParent{TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent}"/>
        /// </summary>
        new IIntermediatePropertySignatureMemberDictionary<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent> Properties { get; }
    }
    /// <summary>
    /// Defines properties and methods for working with the parent of a series of
    /// property signatures.
    /// </summary>
    public interface IIntermediatePropertySignatureParent :
        IIntermediateMemberParent,
        IIntermediateType,
        IPropertySignatureParent
    {
        /// <summary>
        /// Returns the <see cref="IIntermediatePropertySignatureMemberDictionary"/> contained on the 
        /// current <see cref="IIntermediatePropertySignatureParent"/>
        /// </summary>
        new IIntermediatePropertySignatureMemberDictionary Properties { get; }
    }
}
