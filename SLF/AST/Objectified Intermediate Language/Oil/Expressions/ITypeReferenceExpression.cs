﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    public interface ITypeReferenceExpression :
        IMemberParentReferenceExpression,
        IFusionCommaTargetExpression
    {
        /// <summary>
        /// Returns the <see cref="IType"/> relative to the 
        /// <see cref="ITypeReferenceExpression"/>.
        /// </summary>
        IType ReferenceType { get; }
    }
}