﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Ast;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Ast.Expressions
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