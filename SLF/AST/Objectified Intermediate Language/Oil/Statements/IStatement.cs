﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Oil.Statements
{
    /// <summary>
    /// A label to classify all statements under a common point.
    /// </summary>
    public interface IStatement
    {
        /// <summary>
        /// Returns the <see cref="IStatementParent"/> in which the current <see cref="Statement"/>
        /// was declared.
        /// </summary>
        IStatementParent Parent { get; }
        /// <summary>
        /// Visits the <paramref name="visitor"/> based upon the type of the
        /// <see cref="IStatement"/>.
        /// </summary>
        /// <param name="visitor">The <see cref="IIntermediateCodeVisitor"/> 
        /// to visit.</param>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="visitor"/>
        /// is null.</exception>
        void Visit(IIntermediateCodeVisitor visitor);
    }
}