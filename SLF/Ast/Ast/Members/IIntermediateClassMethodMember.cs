using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Ast.Statements;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Members
{
    /// <summary>
    /// Defines properties and methods for working with a method defined
    /// on an intermediate class.
    /// </summary>
    public interface IIntermediateClassMethodMember :
        IIntermediateMethodMember<IClassMethodMember, IIntermediateClassMethodMember, IClassType, IIntermediateClassType>,
        IIntermediateExtendedInstanceMember,
        IClassMethodMember
    {
        /// <summary>
        /// Returns/sets whether the current <see cref="IIntermediateClassMethodMember"/>
        /// is an extension method.
        /// </summary>
        new bool IsExtensionMethod { get; set; }
        /// <summary>
        /// Returns whether the <see cref="IIntermediateClassMethodMember"/> is
        /// asynchronous.
        /// </summary>
        new bool IsAsynchronous { get; set; }
        /// <summary>
        /// Returns whether the <see cref="IIntermediateClassMethodMember"/> 
        /// is a candidate for asynchrony.
        /// </summary>
        bool IsAsynchronousCandidate { get; }
    }
}
