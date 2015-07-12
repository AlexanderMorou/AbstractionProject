using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast
{
    public interface IMetadatumDefinitionParameter<T> :
        IPrimitiveExpression<T>,
        IMetadatumDefinitionParameter
    {
        /// <summary>
        /// Returns/sets the <typeparamref name="T"/> 
        /// value defined on one of the 
        /// <see cref="IMetadatumDefinition"/>'s
        /// constructor argument(s).
        /// </summary>
        /// <remarks>Used to disambiguate from <see cref="IMetadatumDefinitionParameter.Value"/>
        /// and <see cref="IPrimitiveExpression{T}.Value"/>.</remarks>
        new T Value { get; set; }
    }
}
