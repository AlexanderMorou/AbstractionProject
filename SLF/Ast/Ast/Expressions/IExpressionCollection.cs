using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Ast.Expressions
{
    /// <summary>
    /// Defines properties and methods for working 
    /// with a series of <see cref="IExpression"/> 
    /// instances.
    /// </summary>
    public interface IExpressionCollection :
        IExpressionCollection<IExpression>
    {
    }

    public interface IExpressionCollection<T> :
        IControlledCollection<T>
        where T :
            IExpression
    {
    }
}
