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
    /// Defines properties and methods for working with a method of a <see cref="IClassType"/>.
    /// </summary>
    public interface IClassMethodMember :
        IMethodMember<IClassMethodMember, IClassType>,
        IExtendedInstanceMember
    {
        /// <summary>
        /// Returns the base definition of a virtual method that is an override
        /// of the original.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">thrown when the <see cref="IClassMethodMember"/>
        /// is not an overridden member.</exception>
        IClassMethodMember BaseDefinition { get; }
        /// <summary>
        /// Returns whether the current <see cref="IClassMethodMember"/>
        /// is an extension method.
        /// </summary>
        bool IsExtensionMethod { get; }
    }
}
