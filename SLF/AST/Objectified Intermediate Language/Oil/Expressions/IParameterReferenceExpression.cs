using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Oil.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    public interface IParameterReferenceExpression :
        IMemberParentReferenceExpression,
        IAssignTargetExpression
    {
        /// <summary>
        /// Returns/sets the <see cref="String"/> associated to the unique
        /// identifier of the parameter referred to by the 
        /// <see cref="IParameterReferenceExpression"/>.
        /// </summary>
        string Name { get; set; }

    }

    public interface IParameterReferenceExpression<TParameterParent, TIntermediateParameterParent,TParameter, TIntermediateParameter> :
        IParameterReferenceExpression
        where TParameter :
            IParameterMember<TParameterParent>
        where TParameterParent :
            IParameterParent<TParameterParent, TParameter>
        where TIntermediateParameter :
            TParameter,
            IIntermediateParameterMember<TParameterParent, TIntermediateParameterParent>
        where TIntermediateParameterParent :
            TParameterParent,
            IIntermediateParameterParent<TParameterParent, TIntermediateParameterParent, TParameter, TIntermediateParameter>
    {
        /// <summary>
        /// Returns/sets the <typeparamref name="TParameter"/> to which the
        /// <see cref="IParameterReferenceExpression"/> refers.
        /// </summary>
        TIntermediateParameter ReferenceTarget { get; set; }
    }
}
