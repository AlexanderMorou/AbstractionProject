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


namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    /// <summary>
    /// Defines properties and methods for working 
    /// with a refernce to a method's local.
    /// </summary>
    public interface ILocalReferenceExpression :
        IMemberParentReferenceExpression,
        IMemberReferenceExpression,
        IAssignTargetExpression
    {
        /// <summary>
        /// Returns/sets the name of the local the <see cref="ILocalReferenceExpression"/>
        /// refers to.
        /// </summary>
        new string Name { get; set; }
    }
}
