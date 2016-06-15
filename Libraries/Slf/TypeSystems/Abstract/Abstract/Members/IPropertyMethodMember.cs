using System;
using System.Collections.Generic;
using System.Text;
/*----------------------------------------\
| Copyright © 2012 Allen Copeland Jr.     |
|-----------------------------------------|
| The Abstraction Project's code is prov- |
| -ided under a contract-release basis.   |
| DO NOT DISTRIBUTE and do not use beyond |
| the contract terms.                     |
\--------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Abstract.Members
{
    /// <summary>
    /// The type of property method the method is.
    /// </summary>
    public enum PropertyMethodType
    {
        /// <summary>
        /// The method returns the value of the property.
        /// </summary>
        GetMethod,
        /// <summary>
        /// The method sets the value of the property.
        /// </summary>
        SetMethod,
    }
    /// <summary>
    /// Defines properties and methods for working with the 
    /// get or set method of a property member.
    /// </summary>
    public interface IPropertyMethodMember :
        IPropertySignatureMethodMember,
        IMethodMember
    {
    }
    /// <summary>
    /// Defines properties and methods for working with the 
    /// get or set method of a property signature member.
    /// </summary>
    public interface IPropertySignatureMethodMember :
        IMethodSignatureMember
    {
        /// <summary>
        /// Returns the type of method the <see cref="IPropertySignatureMethodMember"/> is.
        /// </summary>
        PropertyMethodType MethodType { get; }
    }
}
