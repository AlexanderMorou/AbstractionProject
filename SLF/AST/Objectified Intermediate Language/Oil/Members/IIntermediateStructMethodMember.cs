using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Members
{
    /// <summary>
    /// Defines properties and methods for working with a method on an
    /// intermediate data structure.
    /// </summary>
    public interface IIntermediateStructMethodMember :
        IIntermediateMethodMember<IStructMethodMember, IIntermediateStructMethodMember, IStructType, IIntermediateStructType>,
        IIntermediateExtendedInstanceMember,
        IStructMethodMember
    {
        /// <summary>
        /// Returns/sets whether the <see cref="IIntermediateStructMethodMember"/> is asynchronous.
        /// </summary>
        bool IsAsync { get; set; }
    }
}
