using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Members
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
        /// Returns whether the <see cref="IIntermediateStructMethodMember"/> is
        /// asynchronous.
        /// </summary>
        new bool IsAsynchronous { get; set; }
        /// <summary>
        /// Returns whether the <see cref="IIntermediateStructMethodMember"/> 
        /// is a candidate for asynchrony.
        /// </summary>
        bool IsAsynchronousCandidate { get; }
    }
}
