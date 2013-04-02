using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// Defines properties and methods for working with the parent of a property signature.
    /// </summary>
    /// <typeparam name="TProperty">The type of <see cref="IPropertySignatureMember"/> in the current implementation.</typeparam>
    /// <typeparam name="TPropertyParentIdentifier">The kind of unique identifier used to refer to the 
    /// parent and differentiate it from the other types.</typeparam>
    /// <typeparam name="TPropertyParent">The kind of type which contains the properties.</typeparam>
    public interface IPropertySignatureParent<TProperty, TPropertyParent> :
        IPropertySignatureParent
        where TProperty :
            IPropertySignatureMember<TProperty, TPropertyParent>
        where TPropertyParent :
            IPropertySignatureParent<TProperty, TPropertyParent>
    {
        /// <summary>
        /// Returns the <see cref="IPropertySignatureMemberDictionary{TProperty, TPropertyParent}"/>
        /// contained on the current <typeparamref name="TPropertyParent"/>
        /// </summary>
        new IPropertySignatureMemberDictionary<TProperty, TPropertyParent> Properties { get; }
    }
    /// <summary>
    /// Defines properties and methods for working with a type that contains a series of 
    /// <see cref="IPropertySignatureMember"/> instances.
    /// </summary>
    public interface IPropertySignatureParent :
        IMemberParent
    {
        /// <summary>
        /// Returns the <see cref="IPropertySignatureMemberDictionary"/> contained on the 
        /// current <see cref="IPropertySignatureParent"/>
        /// </summary>
        IPropertySignatureMemberDictionary Properties { get; }
    }
}
