using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Abstract.Members
{
    /// <summary>
    /// Defines generic properties and methods for working with a property member.
    /// </summary>
    /// <typeparam name="TProperty">The type of property used in the current implementation.</typeparam>
    /// <typeparam name="TPropertyParent">The type of parent that contains the <see cref="IPropertyMember"/> 
    /// instances in the current implementation.</typeparam>
    public interface IPropertyMember<TProperty, TPropertyParent> :
        IMember<TPropertyParent>,
        IPropertyMember
        where TProperty :
            IPropertyMember<TProperty, TPropertyParent>
        where TPropertyParent :
            IPropertyParentType<TProperty, TPropertyParent>
    {
    }

    /// <summary>
    /// Defines properties and methods for working with a standard property member.
    /// </summary>
    public interface IPropertyMember :
        IPropertySignatureMember,
        IExtendedInstanceMember,
        IScopedDeclaration
    {
        /// <summary>
        /// Returns the <see cref="IPropertyMethodMember"/> 
        /// which represents the get method of the 
        /// <see cref="IPropertyMember"/>.
        /// </summary>
        new IPropertyMethodMember GetMethod { get; }
        /// <summary>
        /// Returns the <see cref="IPropertyMethodMember"/> 
        /// which represents the set method of the 
        /// <see cref="IPropertyMember"/>.
        /// </summary>
        new IPropertyMethodMember SetMethod { get; }
    }
}
