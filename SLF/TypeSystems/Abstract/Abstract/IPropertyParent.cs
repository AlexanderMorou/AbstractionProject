using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// Defines generic properties and methods for working with the parent 
    /// of a series of properties.
    /// </summary>
    /// <typeparam name="TProperty">The type of property used in the abstract 
    /// type system.</typeparam>
    /// <typeparam name="TPropertyParent">The type of parent that contains
    /// the <see cref="IPropertyMember"/> instances in the current implementation.</typeparam>
    public interface IPropertyParent<TProperty, TPropertyParent> :
        IPropertyParent
        where TProperty :
            IPropertyMember<TProperty, TPropertyParent>
        where TPropertyParent :
            IPropertyParent<TProperty, TPropertyParent>
    {
        /// <summary>
        /// Returns the <see cref="IPropertyMemberDictionary{TProperty, TPropertyParentIdentifier, TPropertyParent}"/> contained on the current <see cref="IPropertyParent{TProperty, TPropertyParent}"/>
        /// </summary>
        new IPropertyMemberDictionary<TProperty, TPropertyParent> Properties { get; }
    }
    /// <summary>
    /// Defines properties and methods for working with the parent
    /// of a series of properties.
    /// </summary>
    public interface IPropertyParent :
        IMemberParent
    {
        /// <summary>
        /// Returns the <see cref="IPropertyMemberDictionary"/> contained on the current <see cref="IPropertyParent"/>
        /// </summary>
        IPropertyMemberDictionary Properties { get; }
    }
}
