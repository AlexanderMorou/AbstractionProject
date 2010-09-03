using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Oil.Members;

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    public interface IParameterReferenceExpression :
        IMemberParentReferenceExpression
    {
        /// <summary>
        /// Returns/sets the <see cref="IIntermediateParameterMember"/> to which the
        /// <see cref="IParameterReferenceExpression"/> refers.
        /// </summary>
        IIntermediateParameterMember ReferenceTarget { get; set; }
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
        new TIntermediateParameter ReferenceTarget { get; set; }
    }
}
