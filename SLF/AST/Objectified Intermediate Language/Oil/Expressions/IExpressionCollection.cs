using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
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
    /// with a series of <see cref="IExpression"/> 
    /// instances.
    /// </summary>
    public interface IExpressionCollection :
        IControlledStateCollection<IExpression>
    {
        /// <summary>
        /// Returns a <see cref="IExpression"/> at the given <paramref name="index"/>.
        /// </summary>
        /// <param name="index">The <see cref="System.Int32"/> 
        /// of the <see cref="IExpression"/> element to retrieve.</param>
        /// <returns>An <see cref="IExpression"/> relative to <paramref name="index"/>.</returns>
        new IExpression this[int index] { get; }
    }
}
