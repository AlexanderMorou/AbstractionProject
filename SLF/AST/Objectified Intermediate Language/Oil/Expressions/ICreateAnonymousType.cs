﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2011 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    /// <summary>
    /// Defines properties and methods for working with an expression which
    /// constructs an anonymous type.
    /// </summary>
    public interface ICreateAnonymousTypeExpression :
        IMemberParentReferenceExpression,
        IUnaryOperationPrimaryTerm,
        IFusionTargetExpression,
        IFusionTermExpression
    {
        /// <summary>
        /// Returns the <see cref="ICreateAnonymousTypeMemberAssignmentDictionary"/> 
        /// which relates to the property assignment expressions for 
        /// the <see cref="ICreateAnonymousTypeExpression"/>.
        /// </summary>
        ICreateAnonymousTypeMemberAssignmentDictionary PropertyAssignments { get; }
    }
}