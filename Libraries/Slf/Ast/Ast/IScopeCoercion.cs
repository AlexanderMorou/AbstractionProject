using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast
{
    /// <summary>
    /// Defines properties and methods for working with a scope coercion
    /// element.
    /// </summary>
    public interface IScopeCoercion :
        ISourceElement
    {
        void Accept(IScopeCoercionVisitor visitor);
        TResult Accept<TResult, TContext>(IScopeCoercionVisitor<TResult, TContext> visitor, TContext context);
    }
}
