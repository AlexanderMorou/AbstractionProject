using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast
{
    /// <summary>
    /// Defines properties and methods for working with a 
    /// custom attribute definition parameter.
    /// </summary>
    public interface IMetadatumDefinitionParameter :
        IExpression,
        IDisposable
    {
        object Value { get; set; }
        /// <summary>
        /// Returns the <see cref="IType"/> which denotes the kind of
        /// <see cref="Value"/>.
        /// </summary>
        /// <remarks>If the parameter value is supposed to be an
        /// expression, this can be null if binding has not occurred.</remarks>
        IType ParameterType { get; }
    }
}
