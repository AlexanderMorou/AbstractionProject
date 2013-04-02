using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Abstract.Members
{
    /// <summary>
    /// Defines properties and methods for working with a method of a data structure.
    /// </summary>
    public interface IStructMethodMember :
        IMethodMember<IStructMethodMember, IStructType>,
        IExtendedMethodMember
    {
        /// <summary>
        /// Returns the base definition of a virtual method that is an override
        /// of the original.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">thrown when the <see cref="IStructMethodMember"/>
        /// is not an overridden member.</exception>
        /// <remarks>Returns a class method member because all structures derive
        /// from the class <see cref="ValueType"/>, but are inherently sealed upon creation;
        /// therefore, all members that have a base definition will ultimately be from 
        /// <see cref="ValueType"/>, since all of <see cref="Object"/>'s virtual members
        /// are overridden by <see cref="ValueType"/>.</remarks>
        IClassMethodMember BaseDefinition { get; }
    }
}
