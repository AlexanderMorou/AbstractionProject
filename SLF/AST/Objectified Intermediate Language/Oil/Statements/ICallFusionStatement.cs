using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil.Expressions;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Statements
{
    /// <summary>
    /// Defines properties and methods for working with a call to a
    /// fused expression wherein the results are disposed.
    /// </summary>
    public interface ICallFusionStatement :
        IStatement
    {
        /// <summary>
        /// Returns/sets the <see cref="IExpressionToCommaFusionExpression"/> which
        /// is invoked by the <see cref="ICallFusionStatement"/>.
        /// </summary>
        IExpressionToCommaFusionExpression Target { get; set; }
    }
}