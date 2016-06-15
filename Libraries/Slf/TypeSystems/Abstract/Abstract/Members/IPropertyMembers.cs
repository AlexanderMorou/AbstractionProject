using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Abstract.Members
{
    /// <summary>
    /// Defines generic properties and methods for working with a series of <typeparamref name="TProperty"/> 
    /// members.
    /// </summary>
    /// <typeparam name="TProperty">The type of property used in the current implementation.</typeparam>
    /// <typeparam name="TPropertyParent">The type of parent that contains the <see cref="IPropertyMember"/> 
    /// instances in the current implementation.</typeparam>
    public interface IPropertyMemberDictionary<TProperty, TPropertyParent> :
        IGroupedMemberDictionary<TPropertyParent, IGeneralMemberUniqueIdentifier, TProperty>
        where TProperty :
            IPropertyMember<TProperty, TPropertyParent>
        where TPropertyParent :
            IPropertyParent<TProperty, TPropertyParent>
    {

    }
    /// <summary>
    /// Defines properties and methods for working with a series of <see cref="IPropertyMember"/> 
    /// instances.
    /// </summary>
    public interface IPropertyMemberDictionary :
        IPropertySignatureMemberDictionary
    {
        /// <summary>
        /// Returns the <see cref="IPropertyParent"/> which owns
        /// the <see cref="IPropertyMemberDictionary"/>.
        /// </summary>
        new IPropertyParent Parent { get; }
    }
}
