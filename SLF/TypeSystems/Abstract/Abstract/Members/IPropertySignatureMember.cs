using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Abstract.Members
{
    /// <summary>
    /// Defines generic properties and methods for working with a property
    /// signature member.
    /// </summary>
    /// <typeparam name="TProperty">The type of property in the current implementation.</typeparam>
    /// <typeparam name="TPropertyParent">The type of property parent in the current implementation.</typeparam>
    public interface IPropertySignatureMember<TProperty, TPropertyParent> :
        IMember<IGeneralMemberUniqueIdentifier, TPropertyParent>,
        IPropertySignatureMember
        where TProperty :
            IPropertySignatureMember<TProperty, TPropertyParent>
        where TPropertyParent :
            IPropertySignatureParent<TProperty, TPropertyParent>
    {
    }
    /// <summary>
    /// Defines properties and methods for working
    /// with a property signature member.
    /// </summary>
    /// <remarks>
    /// The differences between a property signature 
    /// member and a property member are:
    /// <para>1. Signatures have no code.</para>
    /// <para>2. Signatures have no access
    /// modifiers and are thusly always 
    /// publically accessable.</para></remarks>
    public interface IPropertySignatureMember :
        IMember
    {
        /// <summary>
        /// Returns the type that the <see cref="IPropertySignatureMember"/> is defined as.
        /// </summary>
        IType PropertyType { get; }
        /// <summary>
        /// Returns whether the <see cref="IPropertySignatureMember"/>
        /// can be read from.
        /// </summary>
        bool CanRead { get; }
        /// <summary>
        /// Returns whether the <see cref="IPropertySignatureMember"/> 
        /// can be written to.
        /// </summary>
        bool CanWrite { get; }
        /// <summary>
        /// Returns the <see cref="IPropertySignatureMethodMember"/> 
        /// which represents the get method of the 
        /// <see cref="IPropertySignatureMember"/>.
        /// </summary>
        /// <remarks>Is null if <see cref="CanRead"/> is false.</remarks>
        IPropertySignatureMethodMember GetMethod { get; }
        /// <summary>
        /// Returns the <see cref="IPropertySignatureMethodMember"/> 
        /// which represents the set method of the 
        /// <see cref="IPropertySignatureMember"/>.
        /// </summary>
        /// <remarks>Is null if <see cref="CanWrite"/> is false.</remarks>
        IPropertySignatureMethodMember SetMethod { get; }
    }
}
