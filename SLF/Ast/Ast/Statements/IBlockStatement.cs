﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Ast.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Ast.Statements
{

    /// <summary>
    /// Defines properties and methods for working with a 
    /// statement that is a block of statements.
    /// </summary>
    public interface IBlockStatement :
        IStatement,
        IBlockStatementParent,
        IIntermediateMemberParent
    {
        /// <summary>
        /// Returns the <see cref="IBlockStatementParent"/> which
        /// contains the <see cref="IBlockStatement"/>.
        /// </summary>
        new IBlockStatementParent Parent { get; }
    }
}